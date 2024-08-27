/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 12/07/2022
 * Time: 15:05
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of JobRun.
    /// </summary>
    public partial class Keywords
    {
        public List<string> JobRun()
        {
            List<string> output = new List<string>();
            string testCaseReference = Main.InputData[0];
            string LaunchJobMultipleTimesStr=Main.InputData[1];
            int NumOfTimesToLaunchJob=1;
            string sqlQuery = "select  *  from [Job] where [TestCaseReference] = '"+testCaseReference+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            
            string reportName=dbUtility.GetAccessFieldValue(TestDataConstants.Job_ReportName);
            string paramStr=dbUtility.GetAccessFieldValue(TestDataConstants.Job_NumParam);
            string reportMenuPrompt=dbUtility.GetAccessFieldValue(TestDataConstants.Job_Title);
            string listValue=dbUtility.GetAccessFieldValue(TestDataConstants.Job_ListValue);
            string socValue = dbUtility.GetAccessFieldValue(TestDataConstants.Job_SocietyNo);
            int param = Int32.Parse(paramStr);
            int variableCounter=0;
            string fVal;
            for(int i=0;i<param;i++)
            {
                fVal=dbUtility.GetAccessFieldValue("Field"+(i+1));
                
                if("VARIABLE".Equals(fVal))
                {
                    variableCounter++;
                }
            }
            int inputVariableCount=Main.InputData.Count-2;//2 field for reference and no_of_time to launch
            if(inputVariableCount!=variableCounter)
            {
                Report.Error ("VARIABLE mismatch - expected="+variableCounter+",Actual="+inputVariableCount);
            }
            
            
            
            variableCounter=2;
            Boolean currentJobStatus=true;
            try {
                NumOfTimesToLaunchJob=Int32.Parse(LaunchJobMultipleTimesStr);
                
                for(int jobCounter=0;currentJobStatus==true && jobCounter<NumOfTimesToLaunchJob;jobCounter++)
                {
                    Report.Info("Launching job :"+reportMenuPrompt +"("+(jobCounter+1)+"/"+NumOfTimesToLaunchJob+")");
                    
                    string jobPromptCode = dbUtility.GetAccessFieldValue(TestDataConstants.Job_Prompt_Code);
                    if(string.IsNullOrEmpty(jobPromptCode)){
                        jobPromptCode=reportMenuPrompt;
                    }
                    
                    //CheckBackgroundJobMonitor()
                    CheckBackgroundJobMonitor(jobPromptCode);
                    
                    MenuPromptInternal(reportMenuPrompt);
                    if(!String.Empty.Equals(listValue))
                    {
                        //Perform Selection from DropDown
                        ComboBox listValueCbx=fetchElement("Summit_Job_Run_COMBX_ListValue_Checkbox");
                        selectValue(listValueCbx,listValue);
                        Utility.Capture_Screenshot();
                        Button okBtn=fetchElement("Summit_Job_Run_BTN_Ok");
                        okBtn.Click();
                        Report.Info("Select from List:"+listValue);
                    }
                    
                    String fieldValue;
                    Text textParam;
                    string functionCode;
                    
                    Text fieldValueToUpdate=fetchElement("Summit_Job_Run_TXT_FieldValue_To_Update");
                    ScrollBar scrollObjActual = fetchElement("Summit_Job_Run_SCRL_Object_Actual");
                    Button scrollDownBtn = fetchElement("Summit_Job_Run_SCRL_Down_Button");
                    Button scrollUpBtn = fetchElement("Summit_Job_Run_SCRL_Up_Button");
                    int visibleEntries = Int32.Parse(scrollObjActual.Element.GetAttributeValueText("VisibleAmount"));
                    string Job_Function_Name = dbUtility.GetAccessFieldValue(TestDataConstants.Job_Function_Name);
                    if(!string.IsNullOrEmpty(Job_Function_Name)){
                        functionCode=Job_Function_Name;
                    }
                    else{
                        Text functionTxt= fetchElement("Summit_Job_Run_TXT_Function");
                        functionCode=functionTxt.TextValue;
                    }
                    
                    for( int counter=0;counter<param;counter++)
                    {
                        if(counter<visibleEntries)
                        {
                            textParam="/form[@title='"+Settings.getInstance().get("SUMMIT_TITLE")+"']//text[@name~'TM_PARAMETER_DETAILS_NBT_TMPD_DESCRIPTION_"+counter+"']";
                        }else{
                            textParam="/form[@title='"+Settings.getInstance().get("SUMMIT_TITLE")+"']//text[@name~'TM_PARAMETER_DETAILS_NBT_TMPD_DESCRIPTION_"+(visibleEntries-1)+"']";
                        }
                        
                        fieldValue=dbUtility.GetAccessFieldValue("Field"+(counter+1));
                        
                        //                        textParam="/form[@title~'MRHNAUT']//text[@name~'TM_PARAMETER_DETAILS_NBT_TMPD_VALUE_"+i+"']";
                        Report.Info("Screen field:"+textParam.TextValue+" ==>  DB-Field["+counter+"]="+fieldValue);
                        
                        //SetValue for the specified field
                        if("VARIABLE".Equals(fieldValue))
                        {
                            fieldValueToUpdate.PressKeys(Main.InputData[variableCounter]);
                            variableCounter++;
                        }else if("DEFAULT".Equals(fieldValue.Trim())){
                            //Do not set any value if DEFAULT is set in test-data
                        }
                        else
                        {
                            fieldValueToUpdate.PressKeys(fieldValue);
                        }

                        Keyboard.Press("{DOWN}");
                    }
                    Utility.Capture_Screenshot();
                    Button runBtn=fetchElement("Summit_Job_Run_BTN_Run");
                    runBtn.Click();
                    
                    
                    Text popupTitle=fetchElement("Summit_Job_Run_FORM_Popup_Title");
                    Button infoBtn=fetchElement("Summit_Job_Run_BTN_Info");
                    string notificationTitle=popupTitle.TextValue;
                    Report.Screenshot();
                    infoBtn.Click();
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SENDKEY_WAIT_PRE")));
                    Keyboard.Press("{F4}");
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SENDKEY_WAIT_POST")));
                    if(!notificationTitle.Contains("Information"))
                    {
                        Report.Error("Error in launching Job."+reportMenuPrompt);
                        currentJobStatus=false;
                    }else{
                        currentJobStatus=CheckJobStatus(jobPromptCode,functionCode);
                    }
                    
                }
            } catch (Exception e) {
                Report.Error("JobRun failed."+e.Message);
                currentJobStatus=false;
            }
            
            if (currentJobStatus) {
                output.Add(Constants.TS_STATUS_PASS);
            }else
            {
                if (reportName=="EODBAR" && socValue=="1"){
                    Main.abortFlag = Constants.appAbort;                    
                }

                //Try to capture error reason.
                MenuPromptInternal("BJM");
                Text promptCode=fetchElement("Summit_Job_Run_TXT_Prompt_Code");
                string jobPromptCode = dbUtility.GetAccessFieldValue(TestDataConstants.Job_Prompt_Code);
                if(string.IsNullOrEmpty(jobPromptCode)){
                    jobPromptCode=reportMenuPrompt;
                }
                promptCode.PressKeys(jobPromptCode);
                Keyboard.Press("{F11}");
                Delay.Seconds(1);
                Report.Error("JobRun failed. Capture Screenshot");
                Report.Screenshot();
                Keyboard.Press("{F4}");//Exit from BJM screen
                output.Add(Constants.TS_STATUS_FAIL);
            }
            return output;
        }
            
        private Boolean CheckJobStatus(string jobName,string functionCode)
        {
            //Check JLE for max-timout for Job
            Report.Info("Check status for Job="+jobName+",Function="+functionCode);
            //Check JLE Status for some time till timeout happens
            Boolean returnStatus=false;
            MenuPromptInternal("JLE");
            
            Keyboard.Press("{F12}");
            Text FirstRowFunction=fetchElement("Summit_Job_Run_TXT_First_Row_Function");
            Text FirstRowPrompt=fetchElement("Summit_Job_Run_TXT_First_Row_Prompt");
            Text FirstRowStartDateTime=fetchElement("Summit_Job_Run_TXT_First_Row_StartDateTime");
            Text FirstRowJobStatus=fetchElement("Summit_Job_Run_TXT_First_Row_JobStatus");
            
            setText(FirstRowFunction,functionCode);
            setText(FirstRowPrompt,jobName);
            //In case of slow environment job submitted is not immediately available in JLE.
            Delay.Seconds(Constants.defaultCheckJobStatusRetryTime);
            Keyboard.Press("{F11}");
            Utility.Capture_Screenshot();
            string jobStartTime=FirstRowStartDateTime.TextValue;
            string status=FirstRowJobStatus.TextValue;
            //Search using prompt + function  and look for first row. check status Y/N
            if("Y".Equals(status)){
                returnStatus=true;
            }else{
                //N means recheck again using start date of first row + prompt + function   retry in every 10-sec till max 5 minute configurable
                
                String jobSpecificTimeoutStr;
                try {
                    jobSpecificTimeoutStr=Settings.getInstance().get("JOB_TIMEOUT_"+jobName);
                } catch (Exception) {
                    jobSpecificTimeoutStr=String.Empty;
                }
                
                String jobDefaultTimeoutStr=Settings.getInstance().get("JOB_TIMEOUT");
                
                int timeout;
                if(!String.IsNullOrWhiteSpace(jobSpecificTimeoutStr))
                {
                    timeout=Int32.Parse(jobSpecificTimeoutStr);
                }else if (!String.IsNullOrWhiteSpace(jobDefaultTimeoutStr))
                {
                    timeout=Int32.Parse(jobDefaultTimeoutStr);
                }else
                {
                    timeout=Constants.defaultJobTimeOut;
                }
                string previousStatus=String.Empty;
                
                while(timeout>=0)
                {
                    previousStatus=status;
                    Keyboard.Press("{F12}");
                    setText(FirstRowFunction,functionCode);
                    setText(FirstRowPrompt,jobName);
                    setText(FirstRowStartDateTime,jobStartTime);
                    Keyboard.Press("{F11}");
                    Utility.Capture_Screenshot();
                    status=FirstRowJobStatus.TextValue;
                    if(!status.Equals("N") && !status.Equals(String.Empty) && previousStatus.Equals(status))
                    {
                        break;
                    }
                    Delay.Seconds(Constants.defaultCheckJobStatusRetryTime);
                    timeout=timeout-Constants.defaultCheckJobStatusRetryTime;
                }
                
                if("Y".Equals(status))
                {
                    returnStatus=true;
                }else
                {
                    returnStatus=false;
                    //Additional handling where function name is having small-case-capital-case issue. e.g in job FSCG
                    //function code on Summit-application side in Database is SUM0fscg whereas from UI input is always capital.
                    Keyboard.Press("{F12}");
                    setText(FirstRowFunction,"  ");
                    setText(FirstRowPrompt,jobName);
                    Keyboard.Press("{F11}");
                    //Expecting first row is for that job and return status based actual status returned
                    status=FirstRowJobStatus.TextValue;
                    jobStartTime=FirstRowStartDateTime.TextValue;
                    Utility.Capture_Screenshot();
                    if("Y".Equals(status))
                    {
                        returnStatus=true;
                    }
                }
            }
            Keyboard.Press("{F4}");
            Report.Info("Job["+jobName+"] Completed., started at "+jobStartTime+" , STATUS="+status);
            return returnStatus;
        }
        
        private Boolean CheckBackgroundJobMonitor(string PromptCode)
        {
            Boolean status;
            MenuPromptInternal("BJM");
            Text promptCode=fetchElement("Summit_Job_Run_TXT_Prompt_Code");
            
            promptCode.PressKeys(PromptCode);
            Keyboard.Press("{F11}");
            Delay.Seconds(1);
            Utility.Capture_Screenshot();
            Text lastJobStatus=fetchElement("Summit_Job_Run_TXT_Last_Job_Status");
            if(String.IsNullOrWhiteSpace(lastJobStatus.TextValue)){
                Report.Info(PromptCode+" is not running in background. Good to go");
                status=true;//True :- Job is not running in background and not failed.WE can launch new job instance.
                
            }else{
                status=false; //False :- Job is already running in background - do not proceed.
                Keyboard.Press("{F4}");
                throw new Exception(PromptCode+" is alreadyrunning in background or failed. JobStatus=["+lastJobStatus.TextValue+"] Try again after some time.");
            }
            
            Keyboard.Press("{F4}");
            return status;
        }
    }
}
