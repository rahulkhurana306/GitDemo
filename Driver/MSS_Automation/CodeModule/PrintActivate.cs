/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 05/01/2023
 * Time: 10:48
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of PrintActivate.
    /// </summary>
    public partial class Keywords
    {
        private string drivingTableDB = string.Empty;
        private string printVariableDB = string.Empty;
        private string longDescriptionDB = string.Empty;
        private string typeDB = string.Empty;
        private string columnNameDB = string.Empty;
        private string columnFormattingDB = string.Empty;
        private string accessPath = string.Empty;
        private string documentName = string.Empty;
        private string docTableName = string.Empty;
        private string docColNum = string.Empty;
        
        public List<string> ProceedToPrint(){
            try
            {
                string printFlag = Settings.getInstance().get("PRINT_ACTIVATE_FLAG").ToUpper();
                if("Y".Equals(printFlag)){
                    sPageObj = Main.sElement;
                    RxPath btnRx = sPageObj;
                    Button btn = Host.Local.FindSingle(btnRx, duration);
                    btn.Click();
                }else{
                    Report.Info("Step Surpassed as Print FLAG is N");
                }
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }
            catch(Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> PrintDocument(){
            try
            {
                string printFlag = Settings.getInstance().get("PRINT_ACTIVATE_FLAG").ToUpper();
                if("Y".Equals(printFlag)){
                    PrintHandleOk();
                }else{
                    Report.Info("Step Surpassed as Print FLAG is N");
                }
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }
            catch(Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void PrintPackageReviewDocumentOk()
        {
            string btnOKRx = fetchElement("Activate_Print Package Review Document_BTN_OK");
            Button btnOK = btnOKRx;
            btnOK.Click();
        }
        
        private void LetterProductionLetterReviewOk()
        {
            string btnOKRx = fetchElement("Activate_Letter Production Letter Review_BTN_OK");
            Button btnOK = btnOKRx;
            btnOK.Click();
        }
        
        private void PrintHandleOk()
        {
            string btnOKRx = fetchElement("Activate_Summit_Activate_Module_BTN_OK");
            Button btnOK = btnOKRx;
            btnOK.Click();
        }
        
        private void LetterProductionParagraphSelectionOk()
        {
            string btnOKRx = fetchElement("Activate_Letter Production Paragraph Selection_BTN_OK");
            Button btnOK = btnOKRx;
            btnOK.Click();
        }
        
        public List<string> PrintVariableSetup(){
            try
            {
                string sqlQuery = "select  *  from [ACT_Print_Variable] where [Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                drivingTableDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_DrivingTable);
                printVariableDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_PrintVariable);
                longDescriptionDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_LongDescription);
                typeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_Type);
                columnNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_ColumnName);
                columnFormattingDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_ColumnFormatting);
                accessPath = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_AccessPath);
                documentName = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_DocumentName);
                
                AddPrintVariableMaintainence();
                
                SearchPrintVariable();
                
                AddPrintVariableInDocumentSQL();
                
                StandardDocumentPreparation();
                
                ComipleDocument();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void AddPrintVariableMaintainence(){
            try{
            Main.InputData.Clear();
            Main.InputData.Add("Top Menu");
            Main.InputData.Add("Static Data/ System Utilities");
            Main.InputData.Add("Document Production Maintenance");
            Main.InputData.Add("Activate_Document_Production_Maintenance_RAWTXT_PrintVariableMaintenance");
            NavigateToActivateScreen();
            Main.OutputData.Clear();
            RxPath btnAddRx = fetchElement("Activate_Print_Variable_Maintenance_BTN_Add");
            ClickButtonRx(btnAddRx);
            
            RxPath drivingTableRx = fetchElement("Activate_Print_Variable_Maintenance_Add_LST_DrivingTable");
            selectValueListDropDown(drivingTableRx, drivingTableDB);
            
            RxPath printVariableRx = fetchElement("Activate_Print_Variable_Maintenance_Add_TXT_PrintVariable");
            setTextValue(printVariableRx, printVariableDB);
            
            RxPath longDescRx = fetchElement("Activate_Print_Variable_Maintenance_Add_TXT_LongDescription");
            setTextValue(longDescRx, longDescriptionDB);
            
            RxPath typeRx = fetchElement("Activate_Print_Variable_Maintenance_Add_LST_Type");
            selectValueListDropDown(typeRx, typeDB);
            
            Main.InputData.Clear();
            string[] arrStr = accessPath.Split(',');
            for(int i=0; i<arrStr.Length; i++){
                Main.InputData.Add(arrStr[i]);
            }
            TREE();
            Main.OutputData.Clear();
            RxPath columnNameRx = fetchElement("Activate_Print_Variable_Maintenance_Add_LST_ColName");
            selectValueListDropDown(columnNameRx, columnNameDB);
            
            RxPath columnFormattingRx = fetchElement("Activate_Print_Variable_Maintenance_Add_TXT_ColFormatting");
            setTextValue(columnFormattingRx, columnFormattingDB);
            RxPath btnOkRx = fetchElement("Activate_Print_Variable_Maintenance_Add_BTN_Ok");
            ClickButtonRx(btnOkRx);
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void SearchPrintVariable(){
            try{
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));
                string query1="select * from pcm35 where printvar='"+printVariableDB+"'";
                List<string[]> data=oraUtility.executeQuery(query1);
                if(data.Count==1){
                    Report.Info("Print variable Created Successfully");
                }else{
                    throw new Exception("Print Variable Not Created Successfully");
                }
                
                RxPath btnCloseRx = fetchElement("Activate_Print_Variable_Maintenance_BTN_Close");
                ClickButtonRx(btnCloseRx);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void AddPrintVariableInDocumentSQL(){
            try{
                string query1="select VARNODE from pcm35 where printvar='"+printVariableDB+"'";
                List<string[]> data1=oraUtility.executeQuery(query1 );
                string varnode = data1[0][0];
                
                string query2="insert into pcm37 (docname,printvar,varnode) values ('"+documentName+"','"+printVariableDB+"','"+varnode+"')";
                oraUtility.executeQuery(query2);
                
                string query3="select * from pcm37 where printvar='"+printVariableDB+"' and docname='"+documentName+"'";
                List<string[]> data2=oraUtility.executeQuery(query3);
                if(data2.Count==1){
                    Report.Info("Print variable added in document");
                }else{
                    throw new Exception("Unable to add Print variable in the document.");
                }
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void StandardDocumentPreparation(){
            try{
                RawText stdDocumentPreparation = fetchElement("Activate_Document_Production_Maintenance_RAWTXT_StdDocumentPreparation");
                stdDocumentPreparation.Click();
                
                RxPath docNameTemplateRx = fetchElement("Activate_Document_Template_Preparation_TXT_DocName");
                setTextValue(docNameTemplateRx, documentName);
                
                RxPath btnOkTemplateRx = fetchElement("Activate_Document_Template_Preparation_BTN_OK");
                ClickButtonRx(btnOkTemplateRx);
                
                RxPath btnNormalRx = fetchElement("Activate_Summit_Activate_Module_BTN_Normal");
                Button btnNormal = Host.Local.FindSingle(btnNormalRx, duration);
                if(btnNormal.Visible){
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Mouse.Click();
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Keyboard.Press("{Home}");
                    Keyboard.Press("{Enter}");
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Keyboard.Press("{Up}");
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                }else{
                    throw new Exception("Button Normal not Displayed in Given time");
                }
                btnNormal.Click();
                RxPath listRx = fetchElement("Activate_Insert_Bookmark_Normal_TBL_COL_PrintVariable");
                IList<Element> listTextEle = Host.Local.Find(listRx);
                int count = listTextEle.Count;
                Report.Info("COunt--"+count.ToString());
                int index = 1;
                bool eleFoundNormal = false;
                foreach (var element in listTextEle) {
                    if(element.GetAttributeValueText("Text").Equals(printVariableDB, StringComparison.OrdinalIgnoreCase)){
                        eleFoundNormal = true;
                        break;
                    }
                    index ++;
                }
                Report.Info("index----"+index.ToString());
                if(eleFoundNormal){
                    string elementStr = fetchElement("Activate_Insert_Bookmark_Normal_TBL_COL_PrintVariable")+"["+index+"]";
                    if(index <=19){
                        Text actualElement = elementStr;
                        actualElement.Click();
                    }else{
                        int counter=0;
                        int intIndex = index;
                        while(!(intIndex<=19)){
                            intIndex = intIndex-19;
                            counter++;
                        }
                        BookmarkNormalScrollDown(counter);
                        Text actualElement = elementStr;
                        actualElement.Click();
                    }
                    
                }else{
                    throw new Exception("Print variable is not Present in Normal Table to select");
                }
                Button btnOKNormal = fetchElement("Activate_Insert_Bookmark_Normal_BTN_OK");
                btnOKNormal.Click();
                
                RxPath btnOKRx = fetchElement("Activate_Summit_Activate_Module_BTN_MSG_OK");
                Button btnOK = Host.Local.FindSingle(btnOKRx, duration);
                btnOK.Click();
                
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void ComipleDocument(){
            try{
                RawText generateDocStoredProcedure = fetchElement("Activate_Document_Production_Maintenance_RAWTXT_Generate_Document_Stored_Procedures");
                generateDocStoredProcedure.Click();
                
                RxPath docNameGDSPRx = fetchElement("Activate_Generate_Document_Stored_Procedures_TXT_Document_Name");
                setTextValue(docNameGDSPRx, documentName);
                
                Button btnCompile = fetchElement("Activate_Generate_Document_Stored_Procedures_BTN_Compile");
                btnCompile.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));
                RawText compilerProgres = fetchElement("Activate_Generate_Document_Stored_Procedures_RAWTXT_Compiler_Progress");
                string textComplier = compilerProgres.RawTextValue;
                int counterWhile = 0;
                while(!textComplier.Equals("Compilation Completed Successfully")){
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                    compilerProgres = fetchElement("Activate_Generate_Document_Stored_Procedures_RAWTXT_Compiler_Progress");
                    textComplier = compilerProgres.RawTextValue;
                    counterWhile++;
                    if(counterWhile==1000){
                        throw new Exception("Compilation has not been completed");
                    }
                }
                
                Button btnCloseGDSP = fetchElement("Activate_Generate_Document_Stored_Procedures_BTN_Close");
                btnCloseGDSP.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void BookmarkNormalScrollDown(int times){
            Button btnLD = fetchElement("Activate_Insert_Bookmark_Normal_BTN_LineDown");
            for(int i=0;i<times;i++){
                btnLD.Click(System.Windows.Forms.MouseButtons.Right);
                BookMarkNormalContextMenuPageDown();
                Delay.Seconds(1);
            }
            
        }
        
        private void BookMarkNormalContextMenuPageDown(){
            string contextMenuPageDownString = "/contextmenu[@ProcessId='"+PID+"']//menuitem[@accessiblename='Page Down']";
            RxPath contextMenuPageDown = contextMenuPageDownString;
            Ranorex.MenuItem pageDown = Host.Local.FindSingle(contextMenuPageDown, duration).As<Ranorex.MenuItem>();
            pageDown.Click();
        }
        
        
        public List<string> PrintVariableSetup_List(){
            try
            {
                string sqlQuery = "select  *  from [ACT_Print_Variable_List] where [Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string printVariablRef = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_PrintVariableReference);
                docTableName = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_ListName);
                string[] printVarArr = printVariablRef.Split(',');
                int len = printVarArr.Length;
                for(int i=0;i<len;i++){
                    string sqlQuery2 = "select  *  from [ACT_Print_Variable] where [Reference] = '"+printVarArr[i]+"'";
                    dbUtility.ReadDBResultMS(sqlQuery2);
                    drivingTableDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_DrivingTable);
                    printVariableDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_PrintVariable);
                    longDescriptionDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_LongDescription);
                    typeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_Type);
                    columnNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_ColumnName);
                    columnFormattingDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_ColumnFormatting);
                    accessPath = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_AccessPath);
                    documentName = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_PrintVariable_DocumentName);
                    int colNum = i+1;
                    docColNum = colNum.ToString();
                    
                    AddPrintVariableMaintainence();
                    
                    SearchPrintVariable();
                    
                    AddPrintVariableInDocumentListSQL();
                }
                DocumentCompileList(docTableName);
                
                StandardDocumentPreparationList();
                
                ComipleDocument();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void AddPrintVariableInDocumentListSQL(){
            try{
                string query1="select VARNODE from pcm35 where printvar='"+printVariableDB+"'";
                List<string[]> data1=oraUtility.executeQuery(query1 );
                string varnode = data1[0][0];
                
                string query2="insert into pcm18 (doctablename,doctablecol,printvar,varnode) values ('"+docTableName+"','"+docColNum+"','"+printVariableDB+"','"+varnode+"')";
                oraUtility.executeQuery(query2);
                
                string query3="select * from pcm18 where printvar='"+printVariableDB+"' and doctablename='"+docTableName+"'";
                List<string[]> data2=oraUtility.executeQuery(query3);
                if(data2.Count==1){
                    Report.Info("Print variable added in List ");
                }else{
                    throw new Exception("Unable to add Print variable in the List");
                }
                
                string query4="select * from pcm19 where docname='"+documentName+"' and doctablename='"+docTableName+"'";
                List<string[]> data3=oraUtility.executeQuery(query4);
                if(data3.Count==0){
                    string query5="insert into pcm19 (docname, doctablename) values ('"+documentName+"','"+docTableName+"')";
                    oraUtility.executeQuery(query5);
                    Report.Info("List variable added in document");
                }else{
                    Report.Info("List Already added in Document. Not Adding Again");
                }
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void DocumentCompileList(string docTable){
            try{
                RawText docMaintenanceList = fetchElement("Activate_Document_Production_Maintenance_RAWTXT_Document_Maintenance_List");
                docMaintenanceList.Click();
                
                string docName = docTable;
                RawText listNameRx = fetchElement("Activate_Document_List_Maintenance_RAWTXT_ListName");
                string col = listNameRx.Column.ToString();
                Report.Info("listNameRx Col--"+col);
                string listNameDynamic = fetchElement("Activate_Document_List_Maintenance_TBL_COL_RAWTXT_ListName2");
                string listNameActual = string.Format(listNameDynamic,col);
                Report.Info("listNameActual---"+listNameActual);
                RxPath rxPath = listNameActual;
                Button btnPageDown = fetchElement("Activate_Document_List_Maintenance_BTN_PageDown");
                int sizeDown = btnPageDown.ScreenRectangle.Size.Height;
                int sizeUp = 0;
                RxPath btnPageUpRx = fetchElement("Activate_Document_List_Maintenance_BTN_PageUp");
                Button btnPageUp = null;
                Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                if(Host.Local.TryFindSingle(btnPageUpRx, durationTime,out btnPageUp)){
                    sizeUp = btnPageUp.ScreenRectangle.Size.Height;
                }
                while (!SearchAndSelectActivateTableCell(docName,rxPath,0) && !(sizeUp==sizeDown)) {
                    DocumentListScrollDown();
                    btnPageUp = fetchElement("Activate_Document_List_Maintenance_BTN_PageUp");
                    sizeUp = btnPageUp.ScreenRectangle.Size.Height;
                }
                Button btnCompile = fetchElement("Activate_Document_List_Maintenance_BTN_Compile");
                btnCompile.Click();
                
                Text compilerProgresMessage = fetchElement("Activate_Document_List_Maintenance_TXT_Message");
                string textComplier = compilerProgresMessage.GetAttributeValue<string>("Text");
                int counterWhile = 0;
                while(!textComplier.Equals("Compilation Completed Successfully")){
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                    compilerProgresMessage = fetchElement("Activate_Document_List_Maintenance_TXT_Message");
                    textComplier = compilerProgresMessage.GetAttributeValue<string>("Text");
                    if(textComplier.Contains("ERROR")){
                        throw new Exception("Compilation completed with Errors");
                    }
                    counterWhile++;
                    if(counterWhile==1000){
                        throw new Exception("Compilation has not been completed");
                    }
                }
                
                Button btnClose = fetchElement("Activate_Document_List_Maintenance_BTN_Close");
                btnClose.Click();
                
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void StandardDocumentPreparationList(){
            try{
                RawText stdDocumentPreparation = fetchElement("Activate_Document_Production_Maintenance_RAWTXT_StdDocumentPreparation");
                stdDocumentPreparation.Click();
                
                RxPath docNameTemplateRx = fetchElement("Activate_Document_Template_Preparation_TXT_DocName");
                setTextValue(docNameTemplateRx, documentName);
                
                RxPath btnOkTemplateRx = fetchElement("Activate_Document_Template_Preparation_BTN_OK");
                ClickButtonRx(btnOkTemplateRx);
                
                RxPath btnListRx = fetchElement("Activate_Summit_Activate_Module_BTN_List");
                Button btnList = Host.Local.FindSingle(btnListRx, duration);
                if(btnList.Visible){
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Mouse.Click();
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Keyboard.Press("{Home}");
                    Keyboard.Press("{Enter}");
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Keyboard.Press("{Up}");
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                }else{
                    throw new Exception("Button List not Displayed in Given time");
                }
                btnList.Click();
                RxPath listRx = fetchElement("Activate_Insert_List_TBL_COL_List");
                IList<Element> listTextEle = Host.Local.Find(listRx);
                int count = listTextEle.Count;
                Report.Info("COunt--"+count.ToString());
                int index = 1;
                bool eleFoundNormal = false;
                foreach (var element in listTextEle) {
                    if(element.GetAttributeValueText("Text").Equals(docTableName, StringComparison.OrdinalIgnoreCase)){
                        eleFoundNormal = true;
                        break;
                    }
                    index ++;
                }
                Report.Info("index----"+index.ToString());
                if(eleFoundNormal){
                    string elementStr = fetchElement("Activate_Insert_List_TBL_COL_List")+"["+index+"]";
                    if(index <=12){
                        Text actualElement = elementStr;
                        actualElement.Click();
                    }else{
                        int counter=0;
                        int intIndex = index;
                        while(!(intIndex<=12)){
                            intIndex = intIndex-12;
                            counter++;
                        }
                        BookmarkListScrollDown(counter);
                        Text actualElement = elementStr;
                        actualElement.Click();
                    }
                    
                }else{
                    throw new Exception("Print variable is not Present in List Table to select");
                }
                Button btnOKList = fetchElement("Activate_Insert_List_BTN_Ok");
                btnOKList.Click();
                
                RxPath btnOKRx = fetchElement("Activate_Summit_Activate_Module_BTN_MSG_OK");
                Button btnOK = Host.Local.FindSingle(btnOKRx, duration);
                btnOK.Click();
                
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void BookmarkListScrollDown(int times){
            Button btnLD = fetchElement("Activate_Insert_List_BTN_LineDown");
            for(int i=0;i<times;i++){
                btnLD.Click(System.Windows.Forms.MouseButtons.Right);
                BookMarkNormalContextMenuPageDown();
                Delay.Seconds(1);
            }
            
        }
        
        private void DocumentListScrollDown(){
            Button btnLD = fetchElement("Activate_Document_List_Maintenance_BTN_LineDown");
            btnLD.Click(System.Windows.Forms.MouseButtons.Right);
            BookMarkNormalContextMenuPageDown();
            Delay.Seconds(1);
        }
    }
}
