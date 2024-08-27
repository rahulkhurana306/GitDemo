/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 22/09/2022
 * Time: 11:00
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
    /// Description of Completions.
    /// </summary>
    public partial class Keywords
    {
        public List<string> Completions()
        {
            try{
                
                Main.appFlag = Constants.appActivate;
                if(MVFlag || TOEFlag){
                    CompletionsMV();
                }else{
                    OpenMacro(TestDataConstants.Act_Completions_Macro);
                    
                    AwaitCompletionConfirmation();
                    AutoAccountValidation();
                    MaintainMortgageTransactionDetailsEnquiry();
                    NewStageConfirmation();
                    
                    //waitForPagetoAppear(TestDataConstants.Page_TopMenu);
                }
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void AwaitCompletionConfirmation(){
            string pageTitle1 = "Await Completion Confirmation";
            string message1 = fetchElement("Activate_Await Completion Confirmation_TXT_Message");
            string btn1 = fetchElement("Activate_Await Completion Confirmation_BTN_Yes");
            HandlePromptFundReleaseKeyword(pageTitle1, message1, btn1);
        }
        
        private void AutoAccountValidation(){
            string pageTitle2 = "Auto Account Validation";
            string message2 = fetchElement("Activate_Auto Account Validation_TXT_Message");
            string btn2 = fetchElement("Activate_Auto Account Validation_BTN_OK");
            HandlePromptFundReleaseKeyword(pageTitle2, message2, btn2);
            Button btnOk = null;
            Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
            RxPath btnOkRx = btn2 ;
            if(Host.Local.TryFindSingle(btnOkRx, durationTime,out btnOk)){
                btnOk.Click();
            }
        }
        
        private void MaintainMortgageTransactionDetailsEnquiry(){
            waitForPagetoAppear("- Maintain Mortgage Transaction Details - Enquiry");
            Button btnClose = fetchElement("Activate_Maintain Mortgage Transaction Details_BTN_Close");
            btnClose.Click();
        }
        
        private void NewStageConfirmation(){
            waitForPagetoAppear("- New Stage Confirmation");
            Button btnOk = fetchElement("Activate_New Stage Confirmation_BTN_OK");
            btnOk.Click();
        }
        
        private void CompletionsMV(){
            OpenMacro(TestDataConstants.Act_CompletionsMV_Macro);
            AwaitCompletionConfirmation();
            MaintainCompletionDetails();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
            HandleAccountTypeWarningMessage();
            AutoAccountValidation();
            MaintainMortgageTransactionDetailsEnquiry();
            NewStageConfirmation();
        }
        
    }
}
