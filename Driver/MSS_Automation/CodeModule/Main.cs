using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using Excel = Microsoft.Office.Interop.Excel;
using WinForms = System.Windows.Forms;
using ng_mss_automation.CodeModule;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Reporting;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    [TestModule("1F9D053D-26A4-46EA-945D-58ED85FFEDD1", ModuleType.UserCode, 1)]
    public class Main : ITestModule
    {
        private string currentTestPlan;
        public Keywords actionKeyords;
        public MethodInfo[] method;
        public Type type;
        public String sActionKeyword;
        public String sPageObject;
        public String sScreenName;
        public String sBlocking;
        public int iTestStep;
        public int iTestLastStep;
        public String sTestCaseID;
        public String sTestCaseTitle;
        public String sTestCaseDesc;
        public Text fieldText = null;
        public Button fieldButton = null;
        public ComboBox fieldCombo = null;
        public String scrName= null;
        public String elementName=null;
        public String elementValue=null;
        public static String sElement;
        public String sTestStepDesc;
        public Boolean SheetExecutionStatus;
        
        public static Dictionary <Object, Object> elementcoll = null;
        public int PID = 0;
        
        public static List<string> InputData = new List<string>();
        public static List<string> OutputData = new List<string>();
        public static Dictionary<string, string> GlobalData = new Dictionary<string, string>();
        public List<Tuple<string, string>> TestStepStatus = new List<Tuple<string,string>>();
        public Dictionary<string,ReportRowData> ReportData = new Dictionary<string, ReportRowData>();
        public List<string> testPlanReportData = new List<string>();
        private System.DateTime TestScriptActualStartTime ;
        public List<string> ReturnData = new List<string>();
        public String sInputData;
        public String sOutputData;
        public String testScriptPath;
        public static string appFlag = String.Empty;
        public static string abortFlag = String.Empty;
        List<string> skiplistAdd = new List<string>();
        public string testPlanReportRowData2 = String.Empty;
        public string testPlanReportRowData3 = String.Empty;
        public string currentSheet = string.Empty;
        public bool skipTestCase = false;
        public bool revertTestCaseFlag = false;
        string _TestPlan = Constants.Default_TestPlan;
        [TestVariable("35b106bd-06ad-4c0d-8e03-6e93af9a7f89")]
        public string TestPlan
        {
            get { return _TestPlan; }
            set { _TestPlan = value; }
        }
        
        public Main()
        {
            
        }

        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 50;
            Keyboard.DefaultKeyPressTime = 25;
            Delay.SpeedFactor = 1;
            Duration durationAdpt = TimeSpan.FromMilliseconds(double.Parse(Settings.getInstance().get("ELEMENT_SEARCH_TIMEOUT")));
            Adapter.DefaultSearchTimeout=durationAdpt;
            initialSetup();
            End2End_TestScenarios();
        }
        
        
        public object initialSetup() {

            actionKeyords = new Keywords();
            type = actionKeyords.GetType();
            method = type.GetMethods();
            
            elementcoll=CacheRepository.Load();
            
            if(elementcoll == null || elementcoll.Count == 0 )
            {
                Report.Info("Started Populating Element Collection Map");
                elementcoll = new Dictionary<object, object>();
                ExcelUtility.SetExcelFile(Constants.Path_ORData);
                
                int COUNTER = 1;
                
                while (true) {
                    if(ExcelUtility.GetCellData(COUNTER,Constants.Col_ID,Constants.Sheet_ORPropname).Equals("")){
                        break;
                    }
                    else{
                        elementName = ExcelUtility.GetCellData(COUNTER,6,Constants.Sheet_ORPropname);
                        //Report.Info("elementName is " + elementName);
                        elementValue = ExcelUtility.GetCellData(COUNTER,7,Constants.Sheet_ORPropname);
                        //Report.Info("elementValue is " + elementValue);
                        
                        elementcoll[elementName]=elementValue;
                    }
                    COUNTER=COUNTER+1;
                }
                ExcelUtility.CloseExcelFile();
                CacheRepository.Save(elementcoll);
            }
            return elementcoll;
        }
        
        public void End2End_TestScenarios()
        {
          string skipTestPlan = Path.Combine(Constants.projectDir,"TestPlans",Constants.Skip_TestPlan);
          string []allSkipMainLines = System.IO.File.ReadAllLines(skipTestPlan);
          char sep=',';
          string []lineData;
          foreach (string item in allSkipMainLines)
          {
            lineData=item.Split(sep);
            skiplistAdd.Add(lineData[0].Trim());
          }
          string []testPlans= _TestPlan.Split(',');
          int counter=1;
          foreach (string tp in testPlans)
          {
            Report.Info("Start executing test-plan [" + counter++ +"/"+testPlans.Length+"]="+tp);
            currentTestPlan=tp;
            CsvUtility testPlan = new CsvUtility(Path.Combine(Constants.projectDir,"TestPlans",currentTestPlan));
            string [][]testPlanData=testPlan.getAllData();
            BeforeTestPlan();
            int scriptCounter=1;
            foreach(string[] testScript in testPlan.getAllData())
            {
                Report.Info("Start executing ["+currentTestPlan+"] --> test-script [" + scriptCounter++ +"/"+testPlan.getAllData().Length+"]="+testScript[0]);                
                if(!skiplistAdd.Contains(Regex.Match(testScript[0], @"\d+").Value.Replace(".", string.Empty).Trim())){
                testScriptPath=Path.Combine(Constants.projectDir,"TestCases",testScript[0]);
                Execute_TestCase();
              }
              else{
                AddSkippedMainTestPlan(testScript[0]);
              }
            }
            AfterTestPlan();
          }
          
          FinalCleanUp();
        }

        private void Execute_TestCase()
        {
            
            
            try {
                BeforeTestScript();
                if(abortFlag!=Constants.appAbort){
	                string run_mode=Settings.getInstance().get("RUN_MODE");
	                run_mode=run_mode.Trim();
	                if(Constants.RUN_MODE_PREQ.Equals(run_mode))
	                {
	                    Report.Info("Execute Script in PreReq Mode");
	                    Execute_TestCase_Sheet(Constants.Sheet_PreReqSteps);
	                }
	                else if(Constants.RUN_MODE_TEST.Equals(run_mode))
	                {
	                    Report.Info("Execute Script in Run Mode");
	                    Execute_TestCase_Sheet(Constants.Sheet_TestCases);
	                }
	                else
	                {
	                    Report.Info("Execute Script in Development Mode");
	                    //Default to run both sheets
	                    Execute_TestCase_Sheet(Constants.Sheet_PreReqSteps);
	                    Delay.Seconds(3);
	                    Execute_TestCase_Sheet(Constants.Sheet_TestCases);
	                }
                }
            }catch(Exception e){
                Report.Error("Test script level uncaught exceptions."+e.Message);
                SheetExecutionStatus=false;
                GlobalData[Constants.TC_GLOBAL_STATUS] = Constants.TS_STATUS_FAIL;
            }finally {
                ForceCloseApp();
                if(skipTestCase==false){
                    if(currentSheet.Equals(Constants.Sheet_TestCases) && SheetExecutionStatus==false){
                        if(ExcelUtility.SheetExists(Constants.Sheet_RevertTestCaseSystemSettings)){
                            try{
                                Report.Info("Execute RevertTestCaseSystemSettings--");
                                Execute_TestCase_Sheet(Constants.Sheet_RevertTestCaseSystemSettings);
                                revertTestCaseFlag=true;
                            }catch(Exception e){
                                Report.Info("Exception in RevertTestCaseSettings Steps--"+e.Message);
                                ForceCloseApp();
                            }
                        }
                    }
                }
                AfterTestScript();
                currentSheet = string.Empty;
                skipTestCase=false;
                revertTestCaseFlag=false;
            }
        }
        
        private void Execute_TestCase_Sheet(string sheet)
        {
            string run_mode=Settings.getInstance().get("RUN_MODE");
            run_mode=run_mode.Trim();
            currentSheet = sheet;
            bool prevSheetExecStatus = SheetExecutionStatus;
            if(run_mode.Equals("3") && !prevSheetExecStatus)
            {
                if(!currentSheet.Equals(Constants.Sheet_RevertTestCaseSystemSettings)){
                    //If last executed sheet - normally pre-req sheet is a failure then no need to proceed further.
                    Report.Warn("RunMode["+run_mode+"], No need to execute ["+sheet+"], Previous Test-Sheet ["+Constants.Sheet_PreReqSteps+"] execution failed.");
                    skipTestCase = true;
                    return;
                }
                
            }
            
            int rowIndex = 1;
            sTestCaseID = ExcelUtility.GetCellData(rowIndex, Constants.Col_ID, sheet);
            iTestStep = ExcelUtility.GetRowContains(sTestCaseID, Constants.Col_TestCaseID, sheet);
            iTestLastStep = ExcelUtility.GetTestStepsCount(sheet, sTestCaseID, iTestStep);
            string testStepId;
            //Report.Info("the last step information is " + iTestLastStep);
            ReportRowData reportRowData ;
            for (; iTestStep < iTestLastStep; iTestStep++) {
                BeforeTestStep();
                sTestCaseID = ExcelUtility.GetCellData(rowIndex, Constants.Col_ID, sheet);
                testStepId = ExcelUtility.GetCellData(rowIndex, Constants.Col_TestStepID, sheet);
                sActionKeyword = ExcelUtility.GetCellData(iTestStep, Constants.Col_ActionKeyword, sheet);
                sPageObject = ExcelUtility.GetCellData(iTestStep, Constants.Col_Logical_Object_Name,sheet);
                sScreenName = ExcelUtility.GetCellData(iTestStep, Constants.Col_TestScreenName, sheet);
                sBlocking= ExcelUtility.GetCellData(iTestStep, Constants.Col_Blocking, sheet);
                
                if(!(sPageObject.Equals(""))){
                    string elementName = sScreenName+"_"+sPageObject;
                    try {
                        sElement = actionKeyords.fetchElement(elementName);
                        actionKeyords.sPageObj=sElement;
                    } catch (Exception e) {
                        //No need to proceed - if object required not found. Go to next test-script.
                        reportRowData = new ReportRowData(rowIndex,false,System.DateTime.Now,"0",String.Empty,e.Message);
                        ReportData.Add(sheet+"_"+rowIndex,reportRowData);
                        SheetExecutionStatus=false;
                        break;
                    }
                }
                sTestStepDesc = ExcelUtility.GetCellData(iTestStep, Constants.Col_TestStepDesc, sheet);
                if(!sTestCaseID.Equals("")){
                    Report.Info("TC="+sTestCaseID+" , TS="+testStepId+" ==> " + sTestStepDesc);
                }
                
                if(! String.IsNullOrWhiteSpace(sActionKeyword))
                {
                    sInputData = ExcelUtility.GetCellData(iTestStep, Constants.Col_InputDataSet, sheet);
                    sOutputData = ExcelUtility.GetCellData(iTestStep, Constants.Col_OutputDataSet, sheet);
                    
                    
                    if(!sInputData.Equals("")){
                        EvaluateDataElements(sInputData);
                    }
                    //Capture Report Start time.
                    System.DateTime testStepStartTime = System.DateTime.Now;
                    string s1 = testStepStartTime.ToString(Settings.getInstance().get("TEST_PLAN_REPORT_DURATION_FORMAT"));
                    System.DateTime testStepStartTimebegin = System.DateTime.Parse(s1);
                    
                    //Perform TestStep
                    Boolean stepStatus=Execute_Actions();
                    System.DateTime stepend = System.DateTime.Now;
                    string s2 = stepend.ToString(Settings.getInstance().get("TEST_PLAN_REPORT_DURATION_FORMAT"));
                    System.DateTime testStepEndTime = System.DateTime.Parse(s2);
                    
                    TimeSpan duration = testStepEndTime.Subtract(testStepStartTimebegin);
                    
                    //Collect reporting Data (While Reporting - do not use stepStatus as it decides whether we can continue or not.
                    //Better to use data directly from OutputData
                    Boolean tsStatusFromKeyword=stepStatus;
                    
                    
                    if(Main.OutputData.Count>0)
                    {
                        if(Constants.TS_STATUS_PASS.Equals(Main.OutputData[0]))
                            tsStatusFromKeyword=true;
                        else if(Constants.TS_STATUS_FAIL.Equals(Main.OutputData[0]))
                            tsStatusFromKeyword=false;
                    }
                    
                    reportRowData = new ReportRowData(rowIndex,tsStatusFromKeyword,testStepStartTimebegin,duration.TotalSeconds.ToString(),String.Join(",",Main.InputData),String.Join(",",Main.OutputData));
                    ReportData.Add(sheet+"_"+rowIndex,reportRowData);
                    
                    
                    if( !stepStatus )
                    {
                        GlobalData[Constants.TC_GLOBAL_STATUS] = Constants.TS_STATUS_FAIL;
                        Report.Screenshot();
                        Report.Error("Blocking test step failed. TC="+sTestCaseID+" , TS="+testStepId+" ==> " + sTestStepDesc);
                        SheetExecutionStatus=stepStatus;
                        break; //Do not proceed if blocking test step failed.
                    }
                }
                rowIndex++;
            }
            
        }

        
        public Boolean Execute_Actions(){
            
            Report.Info("Start performing Action-"+sActionKeyword);
            Report.Info("Keyword Input Params -"+String.Join(",",InputData));
            MethodInfo methodActual=null;
            //Handle keyword level exceptions here only.
            try
            {
                if("EXPORT_DATA".Equals(sActionKeyword.Trim()))
                {
                    
                    ReturnData=ExportData();
                    
                }else if("IMPORT_DATA".Equals(sActionKeyword.Trim()))
                {
                    ReturnData=ImportData();
                }
                else{
                    for (int i = 0; i < method.Length; i++)
                    {
                        
                        if (method[i].Name.Equals(sActionKeyword))
                        {
                            OutputData.Clear();
                            methodActual = type.GetMethod(sActionKeyword, BindingFlags.Instance | BindingFlags.Public);
                            Capture_Screenshots_Keywords(sActionKeyword, Constants.BEFORE_KEYWORD);
                            ReturnData = (List<string>) methodActual.Invoke(actionKeyords, null);
                            Capture_Screenshots_Keywords(sActionKeyword, Constants.AFTER_KEYWORD);
                            if (sOutputData != "") //Evaluate output only if it is expected
                            {
                                EvaluateOutDataElements(sOutputData, ReturnData);
                            }
                            break;
                        }
                    }
                    if(methodActual==null)
                    {
                        Report.Error("Keyword ["+sActionKeyword+"] not implemented yet. Coming soon.");
                        OutputData.Clear();
                        OutputData.Add(Constants.TS_STATUS_FAIL);
                        OutputData.Add("Keyword ["+sActionKeyword+"] not implemented yet. Coming soon.");
                        return false;
                    }
                    else
                    {
                        //Check for test step status(default is blocking)
                        String tStatus=ReturnData[0];
                        Report.Info("Keyword Output Params -"+String.Join(",",ReturnData));
                        if(Constants.TS_STATUS_FAIL.Equals(tStatus))//In case test-step is failed. Take screenshot. - Always
                        {
                            Report.Error(Constants.SUMMIT_REPORT_LEVEL,"Test Step failed. Grab Screenshot");
                            Report.Screenshot();
                        }
                        if ((String.IsNullOrWhiteSpace(sBlocking) || sBlocking=="Yes" || sBlocking=="True" || sBlocking=="Y") && Constants.TS_STATUS_FAIL.Equals(tStatus))
                        {
                            return false;
                        }
                        
                    }
                    
                    
                }
            } catch (Exception e) {
                OutputData.Clear();
                OutputData.Add(Constants.TS_STATUS_FAIL);
                OutputData.Add(e.Message);
                return false;
            }
            return true;
        }
        
        public void EvaluateDataElements(string sDataValue){
            string[] DataElements;
            if(sDataValue[0]=='#'){
                DataElements=sDataValue.Substring(1).Split('#');
            }else{
                DataElements=sDataValue.Split(',');
            }
            
            foreach( string de in DataElements){
                string val = EvaluateDataElement(de);
                InputData.Add(val);
            }
            
        }
        
        private string EvaluateDataElement(string element){
            string DataType;
            string outDataValue;
            element=element.Trim();
            DataType = IdentifyDataElement(element);
            
            switch (DataType)
            {
                case "IN_VAR":
                    
                    try {
                        if(!String.IsNullOrEmpty(element)){
                            outDataValue = GlobalData[element];
                        }else{
                            outDataValue = String.Empty;
                        }
                    } catch (Exception) {
                        Report.Error("Variable ["+element+"] missing.Check data file or previous steps.");
                        throw;
                    }
                    
                    break;
                case "ABS":
                    outDataValue = element.Trim('"');
                    break;
                default:
                    outDataValue = String.Empty;
                    Report.Info("Unknown datatype " + DataType);
                    break;
            }
            return outDataValue;
        }
        
        private string IdentifyDataElement(string element){
            string DataType;
            if(element.LastIndexOf('"') > 0){
                DataType="ABS";
            }else{
                DataType="IN_VAR";
            }
            return DataType;
        }
        
        private void EvaluateOutDataElements(string element, List<string> returnData){
            if(returnData[0].Equals(Constants.TS_STATUS_FAIL)){
                //No need to evaluate output variables if test-step failed.
                string[] dataElements;
                dataElements = element.Split(',');
                string status = dataElements[0];
                
                if(returnData.Count==1)
                {
                    Report.Error("TS_STATUS:"+returnData[0]);
                }else if(returnData.Count>1){
                    Report.Error("TS_STATUS:"+returnData[0]+" , Message=["+returnData[1]+"]");
                }
                
            }else{
                string[] dataElements;
                dataElements = element.Split(',');
                int i=0;
                foreach( string de in dataElements){
                    string val = returnData[i];
                    GlobalData[de.Trim()]= val;
                    Report.Info("Output-Variable-Store:"+de.Trim()+"->"+val);
                    i++;
                    
                }
            }
            
        }
        
        private List<string> ExportData()
        {
            List<string> output = new List<string>();
            char sep=',';
            string[] varNames=this.sInputData.Split(sep);
            string key,data;
            string dataFile=Path.Combine(Path.GetDirectoryName(testScriptPath),Path.GetFileNameWithoutExtension(testScriptPath)+"_data.csv");
            //            Report.Info("DataFile:"+dataFile);
            string []content= new String[varNames.Length];
            for(int i=0;i<varNames.Length;i++)
            {
                key=varNames[i].Trim();
                data=Main.InputData[i];
                content[i]=String.Format("{0}{1}{2}",key,sep,data);
                //                Report.Info(key+"-->"+data);
                
            }
            try
            {
                File.WriteAllLines(dataFile,content);
            }catch(IOException e)
            {
                Report.Error("Exception while writing data file."+e.Message);
                throw e;
            }
            
            return output;
        }
        
        private List<string> ImportData()
        {
            List<string> output = new List<string>();
            string existingFileName = null;
            string dataFile=null;
            int inputLen = Main.InputData.Count;
            if(inputLen>0){
                if(!String.IsNullOrEmpty(Main.InputData[0])){
                    existingFileName = Main.InputData[0];
                    dataFile=Path.Combine(Path.GetDirectoryName(Path.Combine(Constants.projectDir,"TestCases",existingFileName)),Path.GetFileName(existingFileName));
                }
            }
            else{
                dataFile=Path.Combine(Path.GetDirectoryName(testScriptPath),Path.GetFileNameWithoutExtension(testScriptPath)+"_data.csv");
            }
            
            try
            {
                string []allLines = System.IO.File.ReadAllLines(dataFile);
                char sep=',';
                string []lineData;
                foreach(string line in allLines)
                {
                    lineData=line.Split(sep);
                    GlobalData[lineData[0].Trim()]=line.Substring(lineData[0].Length+1).Trim();
                }
            }
            catch(IOException e)
            {
                Report.Error("IOException source: "+e.Source+" and error message: "+e.Message);
                throw e;
            }
            
            
            return output;
        }
        
        
        private void PublishReport()
        {
            //Generate ReportName
            string run_mode=Settings.getInstance().get("RUN_MODE");
            string date_time=System.DateTime.Now.ToString("_yyyyMMdd_hhmmss");
            string ReportName=Path.Combine(Constants.Path_Report,Path.GetFileNameWithoutExtension(testScriptPath)+"_R"+run_mode+date_time+Path.GetExtension(testScriptPath));
            
            if(File.Exists(testScriptPath)){
                //Copy Script to Report directory with correct name.
                File.Copy(testScriptPath,ReportName,true);
                
                
                //Open Excel
                Excel.Application excelApp=new Excel.ApplicationClass();
                excelApp.Visible=false;
                Excel.Workbook reportWB = excelApp.Workbooks.Open(ReportName);
                
                if(run_mode.Equals(Constants.RUN_MODE_PREQ))
                {
                    PublishSubReport(Constants.Sheet_PreReqSteps,reportWB);
                }
                if(run_mode.Equals(Constants.RUN_MODE_TEST))
                {
                    PublishSubReport(Constants.Sheet_TestCases,reportWB);
                    if(revertTestCaseFlag){
                        PublishSubReport(Constants.Sheet_RevertTestCaseSystemSettings,reportWB);
                    }
                }
                if(run_mode.Equals(Constants.RUN_MODE_DEV))
                {
                    PublishSubReport(Constants.Sheet_PreReqSteps,reportWB);
                    PublishSubReport(Constants.Sheet_TestCases,reportWB);
                    if(revertTestCaseFlag){
                        PublishSubReport(Constants.Sheet_RevertTestCaseSystemSettings,reportWB);
                    }
                }
                
                reportWB.Save();
                excelApp.Quit();
            }
            
        }
        
        private void PublishSubReport(string sheet,Excel.Workbook reportWB)
        {
            Excel.Worksheet reportWS=reportWB.Sheets[sheet] as Excel.Worksheet;
            reportWS.Cells[1,Constants.Col_StepStatus]="Status";
            reportWS.Cells[1,Constants.Col_StartTime]="Started at";
            reportWS.Cells[1,Constants.Col_Duration]="Duration(sec)";
            reportWS.Cells[1,Constants.Col_ReportInputData]="Action Input";
            reportWS.Cells[1,Constants.Col_ReportOutputData]="Action Output";
            
            int rowCount = reportWS.UsedRange.Rows.Count;
            ReportRowData rowData;
            int rowIndex=0;
            for(int i=0;i<rowCount;i++)
            {
                try {
                    rowData= ReportData[sheet+"_"+i];
                    rowIndex=i+1;
                    if(rowData.testStepStatus)
                    {
                        reportWS.Cells[rowIndex,Constants.Col_StepStatus]=Constants.TS_STATUS_PASS.ToUpper();
                    }else{
                        reportWS.Cells[rowIndex,Constants.Col_StepStatus]=Constants.TS_STATUS_FAIL.ToUpper();
                    }
                    reportWS.Cells[rowIndex,Constants.Col_StartTime]=rowData.testStepStartTime;
                    reportWS.Cells[rowIndex,Constants.Col_Duration]=rowData.durationtotal;
                    reportWS.Cells[rowIndex,Constants.Col_ReportInputData]=rowData.inputData;
                    reportWS.Cells[rowIndex,Constants.Col_ReportOutputData]=rowData.outputData;
                } catch (Exception) {
                    //Nothing to process
                }
                
            }
        }

        private void BeforeTestStep(){
            InputData.Clear();
            OutputData.Clear();
        }
        private void BeforeTestScript()
        {
            //Start new Ranorex Report
            if(!Constants.Default_TestPlan.Equals(currentTestPlan))
            {
                Ranorex.Core.Reporting.TestReport.BeginTestModule("Test-Script:"+Path.GetFileNameWithoutExtension(testScriptPath),1,ActivityExecType.DataIteration);
            }
            Report.Info("Start executing test-script: "+testScriptPath);
            
            if(abortFlag==Constants.appAbort){
                Report.Info("Aborting the rest of the test cases from the testplan: "+testScriptPath);
                try{
                    ExcelUtility.SetExcelFile(testScriptPath);
                    //Clear data for custom excel-report
                    ReportData.Clear();
                    
                    SheetExecutionStatus=false;
                    
                    //Reset status - : Mark Default status as ABORT for each test-script.
                    GlobalData[Constants.TC_GLOBAL_STATUS] = Constants.TS_STATUS_ABORT;

                    TestScriptActualStartTime=System.DateTime.Now;
                }catch(Exception e)
                {
                    GlobalData[Constants.TC_GLOBAL_STATUS] = Constants.TS_STATUS_FAIL;
                    throw e;
                }
                
            }
            else{
                
                try{
                    ExcelUtility.SetExcelFile(testScriptPath);
                    //Clear data for custom excel-report
                    ReportData.Clear();
                    
                    SheetExecutionStatus=true;
                    
                    //Reset status - : Mark Default status as PASS for each test-script.
                    GlobalData[Constants.TC_GLOBAL_STATUS] = Constants.TS_STATUS_PASS;
                    
                    TestScriptActualStartTime=System.DateTime.Now;
                }catch(Exception e)
                {
                    GlobalData[Constants.TC_GLOBAL_STATUS] = Constants.TS_STATUS_FAIL;
                    throw e;
                }
            }
        }
        private void AddSkippedMainTestPlan(string testScriptName){
          Report.Info("*** Skip test-script: "+testScriptName);
          TestScriptActualStartTime=System.DateTime.Now;
          System.DateTime TestScriptEndTimeActual=System.DateTime.Now;
          System.DateTime TestScriptEndTimeWithExcelReport=System.DateTime.Now;
          string durationFormat=Settings.getInstance().get("TEST_PLAN_REPORT_DURATION_FORMAT");
          System.TimeSpan duration2=TestScriptEndTimeActual.Subtract(TestScriptActualStartTime);
          System.TimeSpan durationForExcelReport=TestScriptEndTimeWithExcelReport.Subtract(TestScriptEndTimeActual);
            testPlanReportRowData3 = String.Format("{0},Status=[{1}],Duration({2}),{3},{4},{5},ExDuration({6})",testScriptName,Constants.TS_STATUS_SKIP,duration2.ToString(durationFormat),
                                                   TestScriptActualStartTime,TestScriptEndTimeActual,TestScriptEndTimeWithExcelReport,durationForExcelReport.ToString(durationFormat));
            testPlanReportData.Add(testPlanReportRowData3);
            
        }
        
        private void AddSkippedCasesReportData(){
          TestScriptActualStartTime=System.DateTime.Now;
          System.DateTime TestScriptEndTimeActual=System.DateTime.Now;
          System.DateTime TestScriptEndTimeWithExcelReport=System.DateTime.Now;
          string durationFormat=Settings.getInstance().get("TEST_PLAN_REPORT_DURATION_FORMAT");
          System.TimeSpan duration2=TestScriptEndTimeActual.Subtract(TestScriptActualStartTime);
          System.TimeSpan durationForExcelReport=TestScriptEndTimeWithExcelReport.Subtract(TestScriptEndTimeActual);
          
          CsvUtility skippedData = new CsvUtility(Path.Combine(Constants.projectDir,"TestPlans",currentTestPlan));
          List<string> list = new List<string>();
          string [][]allSkippedData=skippedData.getSkippedAllData();
          for(int i=0; i<allSkippedData.Count();i++){
            if(!allSkippedData[i][0].Contains("##")){
            testPlanReportRowData2 = String.Format("{0},Status=[{1}],Duration({2}),{3},{4},{5},ExDuration({6})",allSkippedData[i][0],Constants.TS_STATUS_SKIP,duration2.ToString(durationFormat),
                                                   TestScriptActualStartTime,TestScriptEndTimeActual,TestScriptEndTimeWithExcelReport,durationForExcelReport.ToString(durationFormat));
            testPlanReportData.Add(testPlanReportRowData2);
               }
          }
        }
        
        private void AfterTestScript()
        {
            Report.Info("*** Stop executing test-script: "+testScriptPath);
            
            System.DateTime TestScriptEndTimeActual=System.DateTime.Now;
            //Close Excel Sheet.
            try{
                ExcelUtility.CloseExcelFile();
            }catch (Exception){
                Report.Error("Exception while closing excel in Main.AfterTestScript");
            }
            //Close Ranorex Report
            if(!Constants.Default_TestPlan.Equals(currentTestPlan))
            {
                Ranorex.Core.Reporting.TestReport.EndTestModule();
            }
            
            //Publish custom excel Report
            try {
                PublishReport();
            } catch (Exception) {
                Report.Error("Excpetion while publishing excel results."+Path.GetFileName(testScriptPath));
            }
            
            
            //Gather information for test plan report.
            System.DateTime TestScriptEndTimeWithExcelReport=System.DateTime.Now;
            
            
            string testScriptName=Path.GetFileName(testScriptPath);
            
            string testScriptStatus;
            try {
                testScriptStatus=GlobalData[Constants.TC_GLOBAL_STATUS];                
            } catch (Exception) {
                testScriptStatus=Constants.TS_STATUS_FAIL;
            }
            
            
            string durationFormat=Settings.getInstance().get("TEST_PLAN_REPORT_DURATION_FORMAT");
            System.TimeSpan duration=TestScriptEndTimeActual.Subtract(TestScriptActualStartTime);
            System.TimeSpan durationForExcelReport=TestScriptEndTimeWithExcelReport.Subtract(TestScriptEndTimeActual);
            string testPlanReportRowData = String.Format("{0},Status=[{1}],Duration({2}),{3},{4},{5},ExDuration({6})",testScriptName,testScriptStatus,duration.ToString(durationFormat),
                                                         TestScriptActualStartTime,TestScriptEndTimeActual,TestScriptEndTimeWithExcelReport,durationForExcelReport.ToString(durationFormat));
            Report.Info(testPlanReportRowData);
            testPlanReportData.Add(testPlanReportRowData);
            GlobalData.Clear();//Remove variables stored in GlobalData once the test-script is over.
        }
        
        private void ForceCloseApp(){
            string forceCloseFlag = Settings.getInstance().get(Constants.FORCE_CLOSE_FLAG);
            if("Y".Equals(forceCloseFlag))
            {
                try {
                    if(Constants.appSummit.Equals(appFlag)){
                        actionKeyords.closeAppSummit();
                        CloseProcessByName("JAVAW");
                    }
                    else if(Constants.appActivate.Equals(appFlag)){
                        
                        actionKeyords.closeAppActivate();
                        //Windows Error Reporting - to close error window Activate Crashes - and causes full test-plan in hang situation.
                        CloseProcessByName("WerFault");
                        Report.Info("Called Close ProcessByName for WerFault");
                        
                        CloseProcessByName("sam");
                        Report.Info("Called Close ProcessByName for sam");
                        
                    }
                } catch (Exception e) {
                    Report.Error("Exception while closing app in Main.AfterTestScript."+e.Message);
                }
            }
        }
        
        
        private void BeforeTestPlan()
        {
            //To avoid system level settings - sometimes user have French as Language or Region and it causes unwanted issues like Date Parins/Formating
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Report.Info("Updating Culture to " + Thread.CurrentThread.CurrentCulture.EnglishName);
            testPlanReportData.Clear();
        }
        
        private void AfterTestPlan()
        {
          AddSkippedCasesReportData();
          PublishTestPlanReport();
        }
        
        private void FinalCleanUp()
        {
            try {
                Settings.getInstance().writeAllSettings();
                OracleUtility.Instance().CloseConnection();
            } catch (Exception) {
                
            }
        }
        private void PublishTestPlanReport()
        {
            string run_mode=Settings.getInstance().get("RUN_MODE");
            string date_time=System.DateTime.Now.ToString("_yyyyMMdd_hhmmss");
            string ReportName=Path.Combine(Constants.Path_Report,Path.GetFileNameWithoutExtension(currentTestPlan)+"_R"+run_mode+date_time+Path.GetExtension(currentTestPlan));
            string summitVersion;
            try {
                List<string[]> patchHistory=actionKeyords.oraUtility.executeQuery("select ph_patch_no,ph_installed_date from  patch_histories order by 2 desc");
                summitVersion=patchHistory[0][0];
            } catch (Exception) {
                
                summitVersion="Default";
            }
            
            try {
                //Activate version placeholder added here and replaced using jenkin in Test_Plan_Launcher
                string envInfo="#User ["+Environment.UserName+"], Host ["+Environment.MachineName+"], Environment ["+Settings.getInstance().get("SUMMIT_DB_HOST_SERVICE")+"], ArtifactURL ["+Environment.GetEnvironmentVariable("RUN_ARTIFACTS_DISPLAY_URL")+"], Summit_Version["+summitVersion+"], Activate_Version[Latest]"+Environment.NewLine;
                File.WriteAllText(ReportName,envInfo);
            } catch (Exception) {
            }
            
            try
            {
                File.AppendAllLines(ReportName,testPlanReportData);
            }catch(IOException e)
            {
                Report.Error("Exception while generating Test-Plan-Report."+e.Message);
                Report.Info("Display Test Plan report over console logs");
                foreach (string line in testPlanReportData){
                    Report.Info(line);
                }
            }
        }
        
        private void Capture_Screenshots_Keywords(string action, string executionflag){
            if(Constants.captureScreenshot){
                string keywordsToCapture=String.Empty;
                
                if(executionflag.Equals(Constants.BEFORE_KEYWORD)){
                    keywordsToCapture=Settings.getInstance().get("CAPTURE_SCREENSHOTS_BEFORE_KEYWORDS");
                }else if(executionflag.Equals(Constants.AFTER_KEYWORD)){
                    keywordsToCapture=Settings.getInstance().get("CAPTURE_SCREENSHOTS_AFTER_KEYWORDS");
                }
                
                if(keywordsToCapture.Contains("ALL") || keywordsToCapture.Contains(action) ){
                    Utility.Capture_Screenshot();
                }
            }
        }
        
        private void CloseProcessByName(string processName){
            foreach (var process in Process.GetProcessesByName(processName))
            {
                try {
                    int tmpPid=process.Id;
                    int sessionId = process.SessionId;
                    //Kill process for the same UserSession only.where current Automation process is running.
                    if(sessionId==Process.GetCurrentProcess().SessionId)
                    {
                        try{
                            Boolean flag = process.CloseMainWindow();
                            Report.Info("Called process.CloseMainWindow() flag value --> " + flag);
                        }
                        catch(Exception){
                        }
                        
                        try{
                            process.Close();
                            Report.Info("Called process.close()");
                        }
                        catch(Exception){
                        }
                        
                        try{
                            process.Kill();
                            Report.Info("Called process.kill()");
                        }
                        catch(Exception){
                        }
                        
                        try{
                            Process.Start("taskkill", "/F /T /PID "+tmpPid);
                            Report.Info("Called Task.kill()");
                        }
                        catch(Exception){
                        }
                        
                        Report.Info("Killing explicitly ["+processName+"] application [PID="+tmpPid+"]");
                    }
                } catch (Exception ) {
                    //Report.Error("Exception while killing app in Main.AfterTestScript."+e.Message);
                }
            }
        }
    }
}
