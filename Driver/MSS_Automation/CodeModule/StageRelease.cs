/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 06/02/2023
 * Time: 12:35
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
    /// Description of StageRelease.
    /// </summary>
    public partial class Keywords
    {
        public List<string> StageRelease()
        {
            try{
                string buildStage = Main.InputData[0];
                string stageAmount = Main.InputData[1];
                string finalStageFlag = Main.InputData[2];
                EndTaskReviewPage("Chase O/S Stage Release");
                LogInformationReceived("STGREL");
                EndTaskReviewPage("Assess Stage Release");
                
                RxPath ASRbtnNoRx = fetchElement("Activate_Assess_Stage_Release_BTN_NO");
                RxPath LRbtnOkRx = fetchElement("Activate_Loan Requirements_BTN_OK");
                RxPath LRbtnCLose = fetchElement("Activate_Loan_Requirements_BTN_Close");
                RxPath MDbtnCloseRx = fetchElement("Activate_Mortgage Details_BTN_Close");
                ClickButtonRx(ASRbtnNoRx);
                Button btnOk = null;
                Duration durationTimeLR = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("ELEMENT_SEARCH_TIMEOUT")));
                if(Host.Local.TryFindSingle(LRbtnOkRx, durationTimeLR,out btnOk)){
                    btnOk.Click();
                }else if(Host.Local.TryFindSingle(LRbtnCLose, durationTimeLR,out btnOk)){
                    btnOk.Click();
                }
                ClickButtonRx(MDbtnCloseRx);
                ClickButtonRx(ASRbtnNoRx);
                
                EndTaskReviewPage("Release Stage Release");
                
                RxPath warningFirstPaymentBtnOkRx = fetchElement("Activate_WARNING_FIRST_PAYMENT_BTN_OK");
                if(Host.Local.TryFindSingle(warningFirstPaymentBtnOkRx, durationTimeLR,out btnOk)){
                    btnOk.Click();
                }
                
                RxPath SRRtxtBuildingStageRx = fetchElement("Activate_Stage_Release_Request_TXT_BuildingStage");
                RxPath SRRtxtReleaseAmountRx = fetchElement("Activate_Stage_Release_Request_TXT_ReleaseAmount");
                setTextValue(SRRtxtBuildingStageRx, buildStage);
                setTextValue(SRRtxtReleaseAmountRx, stageAmount);
                RxPath finalStageCBX = fetchElement("Activate_Stage_Release_Request_CHKBOX_FinalStage");
                setCheckbox(finalStageCBX, finalStageFlag);
                RxPath SRRbtnOk = fetchElement("Activate_Stage_Release_Request_BTN_OK");
                ClickButtonRx(SRRbtnOk);
                
                RxPath ETCbtnOk = fetchElement("Activate_End Task Confirmation_BTN_OK");
                ClickButtonRx(ETCbtnOk);
                RxPath FRDbtnOk = fetchElement("Activate_Funds Release Details_BTN_OK");
                ClickButtonRx(FRDbtnOk);
                RxPath FRbtnOk = fetchElement("Activate_Funds Release_BTN_OK2");
                ClickButtonRx(FRbtnOk);
                
                //generate CHAPS
                GenerateCHAPS();
                //produce CHAPS
                ProduceCHAPS();
                
                PCDTaskClosure();
                
                POSTStageRetentionTaskClosure();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> RetentionRelease()
        {
            try{
                string reasonForRelease = Main.InputData[0];
                string releaseAmount = Main.InputData[1];
                EndTaskReviewPage("Chase O/S Retention Release");
                LogInformationReceived("RETREL");
                EndTaskReviewPage("Assess Retention Release");
                RxPath ARRbtnNoRx = fetchElement("Activate_Assess_Retention_Release_BTN_NO");
                ClickButtonRx(ARRbtnNoRx);
                int waitTime = Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT"));
                Delay.Milliseconds(waitTime);
                ClickButtonRx(ARRbtnNoRx);
                
                EndTaskReviewPage("Release Retention Release");
                Duration durationTimeRR = TimeSpan.FromMilliseconds(waitTime);
                RxPath warningFirstPaymentBtnOkRx = fetchElement("Activate_WARNING_FIRST_PAYMENT_BTN_OK");
                Button btnOk = null;
                if(Host.Local.TryFindSingle(warningFirstPaymentBtnOkRx, durationTimeRR,out btnOk)){
                    btnOk.Click();
                }
                
                RxPath RRtxtBuildingStageRx = fetchElement("Activate_Retention_Release_Request_TXT_ReasonForRelease");
                RxPath RRtxtReleaseAmountRx = fetchElement("Activate_Retention_Release_Request_TXT_ReleaseAmount");
                setTextValue(RRtxtBuildingStageRx, reasonForRelease);
                setTextValue(RRtxtReleaseAmountRx, releaseAmount);
                RxPath RRbtnOk = fetchElement("Activate_Retention_Release_Request_BTN_OK");
                ClickButtonRx(RRbtnOk);
                
                RxPath ETCbtnOk = fetchElement("Activate_End Task Confirmation_BTN_OK");
                ClickButtonRx(ETCbtnOk);
                RxPath FRDbtnOk = fetchElement("Activate_Funds Release Details_BTN_OK");
                ClickButtonRx(FRDbtnOk);
                RxPath FRbtnOk = fetchElement("Activate_Funds Release_BTN_OK2");
                ClickButtonRx(FRbtnOk);
                
                //generate CHAPS
                GenerateCHAPS();
                //produce CHAPS
                ProduceCHAPS();
                
                PCDTaskClosure();
                
                POSTStageRetentionTaskClosure();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
                
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void EndTaskReviewPage(string taskname){
            try{
                RxPath menuItemReviewRx = fetchElement("Activate_MenuItems_MENUITEM_Review");
                MenuItem menuItemReview = Host.Local.FindSingle(menuItemReviewRx, duration);
                menuItemReview.Click();
                SearchTaskInternal(taskname);
                RxPath btnEndTaskRx = fetchElement("Activate_Progress Review_BTN_End_Task");
                ClickButtonRx(btnEndTaskRx);
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void LogInformationReceived(string val){
            try{
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                ListItem logInfoReceived = fetchElement("Activate_Await Missing Reference Information_LST_ITEM_LogInfoReceived");
                logInfoReceived.Click();
                
                RxPath amriBtnOkRx = fetchElement("Activate_Await Missing Reference Information_BTN_OK");
                ClickButtonRx(amriBtnOkRx);
                
                RxPath acrBtnAddRx = fetchElement("Activate_Correspondence Receipt_BTN_Add");
                ClickButtonRx(acrBtnAddRx);
                
                RxPath lstCodeRx = fetchElement("Activate_Correspondence Receipt_LST_CODE");
                selectValueListDropDown(lstCodeRx, val);
                
                RxPath acrBtnOkRx = fetchElement("Activate_Correspondence Receipt_BTN_OK");
                ClickButtonRx(acrBtnOkRx);
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void GenerateCHAPS(){
            try{
                CHAPS99Fix();
                Main.InputData.Clear();
                Main.InputData.Add("Top Menu");
                Main.InputData.Add("Batch Processes");
                Main.InputData.Add("Activate_Batch_Processes_RAWTXT_GenerateChaps");
                NavigateToActivateScreen();
                RxPath EPBGbtnOkRx = fetchElement("Activate_Electronic Payments Batch Generation_BTN_OK");
                ClickButtonRx(EPBGbtnOkRx);
                RxPath GEPbtnOkRx = fetchElement("Activate_Generate Electronic Payments_BTN_OK");
                ClickButtonRx(GEPbtnOkRx);
                RxPath RFbtnYesRx = fetchElement("Activate_Release Funds_BTN_Yes");
                ClickButtonRx(RFbtnYesRx);
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void ProduceCHAPS(){
            try{
                CHAPS99Fix();
                RxPath treeItemTopMenuRx = fetchElement("Activate_Tree_TreeItem_Top_Menu");
                TreeItem treeItemTopMenu = Host.Local.FindSingle(treeItemTopMenuRx, duration);
                treeItemTopMenu.Click();
                Main.InputData.Clear();
                Main.InputData.Add("Top Menu");
                Main.InputData.Add("Batch Processes");
                Main.InputData.Add("Activate_Batch_Processes_RAWTXT_ProduceChaps");
                NavigateToActivateScreen();
                
                RxPath EPBPbtnOk = fetchElement("Activate_Electronic Payments Batch Production_BTN_OK");
                ClickButtonRx(EPBPbtnOk);
                RxPath EPBFPbtnOk = fetchElement("Activate_Electronic_Payments_Batch_File_Production_BTN_OK");
                ClickButtonRx(EPBFPbtnOk);
                RxPath PEPRbtnClose = fetchElement("Activate_Printing Electronic Payments Report_BTN_Close");
                ClickButtonRx(PEPRbtnClose);
                
                treeItemTopMenu = Host.Local.FindSingle(treeItemTopMenuRx, duration);
                treeItemTopMenu.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void PCDTaskClosure(){
            try{
                EndTaskReviewPage("Print Completion Documents");
                RxPath SAMbtnOk = fetchElement("Activate_Summit_Activate_Module_BTN_OK");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("LONG_WAIT")));
                ClickButtonRx(SAMbtnOk);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("LONG_WAIT")));
                ClickButtonRx(SAMbtnOk);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("LONG_WAIT")));
                ClickButtonRx(SAMbtnOk);
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void POSTStageRetentionTaskClosure(){
            try{
                EndTaskReviewPage("Post Stage/Retention Release");
                RxPath PREbtnClose = fetchElement("Activate_Progress_Review_Enquiry_BTN_CLOSE");
                ClickButtonRx(PREbtnClose);
                RxPath SDbtnOk = fetchElement("Activate_Solicitor Details_BTN_OK");
                ClickButtonRx(SDbtnOk);
                RxPath SBDbtnOk = fetchElement("Activate_Solicitor Bank Details_BTN_OK");
                ClickButtonRx(SBDbtnOk);
                RxPath MECDbtnOk = fetchElement("Activate_Maintain Expected Completion Date_BTN_OK");
                ClickButtonRx(MECDbtnOk);
                RxPath FCbtnOk = fetchElement("Activate_Fee Charges_BTN_OK");
                ClickButtonRx(FCbtnOk);
                RxPath NAbtnOk = fetchElement("Activate_Notice Address_BTN_OK");
                ClickButtonRx(NAbtnOk);
                RxPath ETCbtnOk2 = fetchElement("Activate_End Task Confirmation_BTN_OK");
                ClickButtonRx(ETCbtnOk2);
                RxPath MMTDbtnClose = fetchElement("Activate_Maintain Mortgage Transaction Details_BTN_Close");
                ClickButtonRx(MMTDbtnClose);
                RxPath NSCbtnOk = fetchElement("Activate_New Stage Confirmation_BTN_OK");
                ClickButtonRx(NSCbtnOk);
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void CHAPS99Fix(){
            try{
                string dbQuery = "select CHP_BATCHNO from SAM05 where CHP_BATCHNO like '%99'";
                List<string[]> data=oraUtility.executeQuery(dbQuery );
                if(data.Count!=0){
                    string batchNum = data[0][0];
                    if(!string.IsNullOrEmpty(batchNum)){
                        string result = batchNum.Substring(8);
                        string preFix = batchNum.Substring(0,8);
                        string postFix= string.Empty;
                        string finalVal= string.Empty;
                        int resultInt = Int32.Parse(result);
                        if(resultInt==99){
                            postFix="01";
                            finalVal=preFix+postFix;
                            dbQuery="UPDATE SAM05 SET CHP_BATCHNO='"+finalVal+"' where CHP_BATCHNO='"+batchNum+"'";
                            oraUtility.executeQuery(dbQuery);
                        }
                    }
                }
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
    }
}
