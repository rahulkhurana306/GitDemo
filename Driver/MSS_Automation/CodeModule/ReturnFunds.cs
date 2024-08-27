/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 06/06/2023
 * Time: 13:05
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
    /// Description of ReturnFunds.
    /// </summary>
    public partial class Keywords
    {
        public List<string> ReturnFundsAfterCompletion()
        {
            try{
                Main.appFlag = Constants.appActivate;
                OpenMacro("RETURNAFTER");
                string sqlQuery = "select  *  from [ACT_Return_Funds] where [Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string returnFundsDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_ReturnedFunds_ReturnedFunds);
                string returnDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_ReturnedFunds_ReturnDate);
                string returnFundTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_ReturnedFunds_ReturnFundType);
                string amtReturnedDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_ReturnedFunds_AmountReturned);
                
                RxPath NSCBtnOkRx = fetchElement("Activate_New_Stage_Confirmation_BTN_OK");
                ClickButtonRx(NSCBtnOkRx);
                EndTaskReviewPage("Returned Funds");
                RxPath EOFRABtnReturnRx = fetchElement("Activate_Enquiry_on_Funds_Release_Audit_BTN_Return");
                ClickButtonRx(EOFRABtnReturnRx);
                
                if(!string.IsNullOrEmpty(returnDateDB)){
                    if(returnDateDB.StartsWith("WCAL", StringComparison.OrdinalIgnoreCase)){
                        string valDate = utility.ProcessWCALDate(returnDateDB,"1");
                        returnDateDB=valDate;
                    }
                    Text txtDate = fetchElement("Activate_Returned_Funds_Processing_TXT_ReturnedFundDate");
                    txtDate.PressKeys(returnDateDB+"{Tab}");
                }
                RxPath ReturnFundTypeRx = fetchElement("Activate_Returned_Funds_Processing_CBX_ReturnFundType");
                selectValueCombobox(ReturnFundTypeRx, returnFundTypeDB);
                RxPath ReturnedFundAmtRX = fetchElement("Activate_Returned_Funds_Processing_TXT_ReturnedFundAmt");
                setTextValue(ReturnedFundAmtRX, amtReturnedDB);
                RxPath RFPbtnOk = fetchElement("Activate_Returned_Funds_Processing_BTN_OK");
                ClickButtonRx(RFPbtnOk);
                
                RxPath EOFRABtnCloseRx = fetchElement("Activate_Enquiry_on_Funds_Release_Audit_BTN_Close");
                ClickButtonRx(EOFRABtnCloseRx);
                
                if(returnFundsDB.ToUpper().Equals("REVISED OFFER REQUIRED")){
                    Report.Info("This FLow should be Done Manually.");
                }else if(returnFundsDB.ToUpper().Equals("REVISED COMPLETION DATE REQUIRED")){
                    RevisedCompletionDateRequiredFlow();
                }else if(returnFundsDB.ToUpper().Equals("DECLINE APPLICATION")){
                    Report.Info("THis Flow Should be done Manually");
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void RevisedCompletionDateRequiredFlow(){
            try{
                ListItem rcdrItem = fetchElement("Activate_Returned_Funds_LST_ITEM_RevisedCompletionDateRequired");
                rcdrItem.Click();
                RxPath RFbtnOk = fetchElement("Activate_Returned_Funds_BTN_OK");
                ClickButtonRx(RFbtnOk);
                
                RxPath NSCBtnOkRx = fetchElement("Activate_New_Stage_Confirmation_BTN_OK");
                ClickButtonRx(NSCBtnOkRx);
                EndTaskReviewPage("Change Completion Date");
                RxPath MCDBtnOkRx = fetchElement("Activate_Maintain Completion Details_BTN_OK");
                ClickButtonRx(MCDBtnOkRx);
                RxPath SDBtnOkRx = fetchElement("Activate_Solicitor Details_BTN_OK");
                ClickButtonRx(SDBtnOkRx);
                RxPath SBDBtnOkRx = fetchElement("Activate_Solicitor Bank Details_BTN_OK");
                ClickButtonRx(SBDBtnOkRx);
                RxPath NABtnOkRx = fetchElement("Activate_Notice Address_BTN_OK");
                ClickButtonRx(NABtnOkRx);
                RxPath FCBtnOkRx = fetchElement("Activate_Fee Charges_BTN_OK");
                ClickButtonRx(FCBtnOkRx);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button btnOk3 = fetchElement("Activate_Funds Release Details_BTN_OK");
                btnOk3.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                RxPath txtMessage = fetchElement("Activate_Funds Release_BTN_OK2");
                Button btnOk = null;
                Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                if(Host.Local.TryFindSingle(txtMessage, durationTime,out btnOk)){
                    btnOk.Click();
                }
                
                PrintPackageReviewDocumentOk();
                string warning_popup_title=fetchElement("Activate_Print_window_popup_BTN_OK");
                LetterProductionLetterReviewOk();
                PrintHandleOk();
                PrintHandleOk();
                if(ElementDisplayed(warning_popup_title)){
                    Keyboard.Press("{ENTER}");
                }
                //FundReleaseAndCompletions();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        public List<string> ReCompletions(){
            try{
                string refRecompletions = string.Empty;
                if(Main.InputData.Count>=1){
                    refRecompletions = Main.InputData[0];
                }
                FundReleaseAndCompletions(refRecompletions);
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
             
        private void FundReleaseAndCompletions(string refReCompletion){
            try{
                OpenMacro(TestDataConstants.Act_FundsReleaseDetails_Macro);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button btnOk = fetchElement("Activate_Valuation Report_BTN_OK");
                btnOk.Click();
                if(!string.IsNullOrEmpty(refReCompletion)){
                    MaintainCompletionDetails(refReCompletion);
                }else{
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Button btnOk2 = fetchElement("Activate_Maintain Completion Details_BTN_OK");
                    btnOk2.Click();
                }
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button btnOk3 = fetchElement("Activate_Funds Release Details_BTN_OK");
                btnOk3.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                string pageTitle1 = "Fund Release";
                string message1 = fetchElement("Activate_Funds Release_TXT_Message");
                string btn1 = fetchElement("Activate_Funds Release_BTN_Ok");
                HandlePromptFundReleaseKeyword(pageTitle1, message1, btn1);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button btnOKEPBG = fetchElement("Activate_Electronic Payments Batch Generation_BTN_OK");
                btnOKEPBG.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button btnOKGEP = fetchElement("Activate_Generate Electronic Payments_BTN_OK");
                btnOKGEP.Click();
                string pageTitle2 = "Release Funds";
                string message2 = fetchElement("Activate_Release Funds_TXT_Message");
                string btn2 = fetchElement("Activate_Release Funds_BTN_Yes");
                HandlePromptFundReleaseKeyword(pageTitle2, message2, btn2);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button btnOKEPBP = fetchElement("Activate_Electronic Payments Batch Production_BTN_OK");
                btnOKEPBP.Click();
                string pageTitle3 = "Electronic Payments Batch File Production";
                string message3 = fetchElement("Activate_Electronic Payments Batch Production_TXT_Prompt_Message");
                string btn3 = fetchElement("Activate_Electronic Payments Batch Production_BTN_Prompt_Ok");
                HandlePromptFundReleaseKeyword(pageTitle3, message3, btn3);
                PrintingElectronicPaymentsReport();
                
                OpenMacro(TestDataConstants.Act_Completions_Macro);
                AwaitCompletionConfirmation();
                AutoAccountValidation();
                MaintainMortgageTransactionDetailsEnquiry();
                NewStageConfirmation();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
            
        }
        
        private void MaintainCompletionDetails(string refMCD){
            try{
                string sqlQuery = "select  *  from [ACT_Maintain_Completion_Details] where [Reference] = '"+refMCD+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                waitForPagetoAppear("- Maintain Completion Details");
                string expectedDateOfCompletionDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MaintainCompletionDetails_ExpectedDateOfCompletion);
                if(!string.IsNullOrEmpty(expectedDateOfCompletionDB)){
                    if(!expectedDateOfCompletionDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        if(expectedDateOfCompletionDB.StartsWith("WCAL_DATE", StringComparison.OrdinalIgnoreCase)){
                            string dateValue = utility.ProcessWCALDate(expectedDateOfCompletionDB,"1");
                            Text expectedDateOfCompletion = fetchElement("Activate_Maintain Completion Details_TXT_Expected_Date_of_Completion");
                            setText(expectedDateOfCompletion, dateValue);
                        }
                    }
                }
                string paymentTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MaintainCompletionDetails_PaymentType);
                if(!string.IsNullOrEmpty(paymentTypeDB)){
                    if(!paymentTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        List paymentType = fetchElement("Activate_Maintain Completion Details_LST_Payment_Type");
                        ComboboxItemSelectDirect(paymentType, paymentTypeDB);
                    }
                }
                string payeeTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MaintainCompletionDetails_PayeeType);
                if(!string.IsNullOrEmpty(payeeTypeDB)){
                    if(!payeeTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        ComboBox payeeType = fetchElement("Activate_Maintain Completion Details_LST_Payee_Type");
                        ComboboxItemSelectDirect(payeeType, payeeTypeDB);
                    }
                }
                Button btnOk2 = fetchElement("Activate_Maintain Completion Details_BTN_OK");
                btnOk2.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
         public List<string> ReturnFundsBeforeCompletion()
        {
            try{
                Main.appFlag = Constants.appActivate;
                OpenMacro("RETURNFUNDS");
                string sqlQuery = "select  *  from [ACT_Return_Funds] where [Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string returnFundsDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_ReturnedFunds_ReturnedFunds);
                string returnDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_ReturnedFunds_ReturnDate);
                string returnFundTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_ReturnedFunds_ReturnFundType);
                string amtReturnedDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_ReturnedFunds_AmountReturned);
                
                RxPath EOFRABtnReturnRx = fetchElement("Activate_Enquiry_on_Funds_Release_Audit_BTN_Return");
                ClickButtonRx(EOFRABtnReturnRx);
                
                if(!string.IsNullOrEmpty(returnDateDB)){
                    if(returnDateDB.StartsWith("WCAL", StringComparison.OrdinalIgnoreCase)){
                        string valDate = utility.ProcessWCALDate(returnDateDB,"1");
                        returnDateDB=valDate;
                    }
                    Text txtDate = fetchElement("Activate_Returned_Funds_Processing_TXT_ReturnedFundDate");
                    txtDate.PressKeys(returnDateDB+"{Tab}");
                }
                RxPath ReturnFundTypeRx = fetchElement("Activate_Returned_Funds_Processing_CBX_ReturnFundType");
                selectValueCombobox(ReturnFundTypeRx, returnFundTypeDB);
                RxPath ReturnedFundAmtRX = fetchElement("Activate_Returned_Funds_Processing_TXT_ReturnedFundAmt");
                setTextValue(ReturnedFundAmtRX, amtReturnedDB);
                RxPath RFPbtnOk = fetchElement("Activate_Returned_Funds_Processing_BTN_OK");
                ClickButtonRx(RFPbtnOk);
                
                RxPath EOFRABtnCloseRx = fetchElement("Activate_Enquiry_on_Funds_Release_Audit_BTN_Close");
                ClickButtonRx(EOFRABtnCloseRx);
                
                if(returnFundsDB.ToUpper().Equals("REVISED OFFER REQUIRED")){
                    Report.Info("This FLow should be Done Manually.");
                }else if(returnFundsDB.ToUpper().Equals("REVISED COMPLETION DATE REQUIRED")){
                    RevisedCompletionDateRequiredFlowBefore();
                }else if(returnFundsDB.ToUpper().Equals("DECLINE APPLICATION")){
                    Report.Info("THis Flow Should be done Manually");
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
             }catch(Exception e){
                 Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                 Main.OutputData.Add(e.Message);
                 return Main.OutputData;
             }
        }
         
         private void RevisedCompletionDateRequiredFlowBefore(){
             try{
                 ListItem rcdrItem = fetchElement("Activate_Returned_Funds_LST_ITEM_RevisedCompletionDateRequired");
                 rcdrItem.Click();
                 RxPath RFbtnOk = fetchElement("Activate_Returned_Funds_BTN_OK");
                 ClickButtonRx(RFbtnOk);
                 
                 RxPath NSCBtnOkRx = fetchElement("Activate_New_Stage_Confirmation_BTN_OK");
                 ClickButtonRx(NSCBtnOkRx);
                 EndTaskReviewPage("Change Completion Date");
                 RxPath MCDBtnOkRx = fetchElement("Activate_Maintain Completion Details_BTN_OK");
                 ClickButtonRx(MCDBtnOkRx);
                 RxPath SDBtnOkRx = fetchElement("Activate_Solicitor Details_BTN_OK");
                 ClickButtonRx(SDBtnOkRx);
                 RxPath SBDBtnOkRx = fetchElement("Activate_Solicitor Bank Details_BTN_OK");
                 ClickButtonRx(SBDBtnOkRx);
                 RxPath NABtnOkRx = fetchElement("Activate_Notice Address_BTN_OK");
                 ClickButtonRx(NABtnOkRx);
                 RxPath FCBtnOkRx = fetchElement("Activate_Fee Charges_BTN_OK");
                 ClickButtonRx(FCBtnOkRx);
                 Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                 Button btnOk3 = fetchElement("Activate_Funds Release Details_BTN_OK");
                 btnOk3.Click();
                 Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                 RxPath txtMessage = fetchElement("Activate_Funds Release_BTN_OK2");
                 Button btnOk = null;
                 Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                 if(Host.Local.TryFindSingle(txtMessage, durationTime,out btnOk)){
                     btnOk.Click();
                 }
                 PrintPackageReviewDocumentOk();
                 LetterProductionLetterReviewOk();
                 PrintHandleOk();
                 PrintHandleOk();
                 Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));
                 //generate CHAPS
                 GenerateCHAPS();
                 //produce CHAPS
                 ProduceCHAPS();
             }catch(Exception e){
                 throw new Exception(e.Message);
             }
         }
    }
}
