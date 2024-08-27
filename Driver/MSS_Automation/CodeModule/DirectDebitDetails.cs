/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 06/09/2022
 * Time: 15:26
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
    /// Description of DirectDebitDetails.
    /// </summary>
    public partial class Keywords
    {
        public List<string> DirectDebitDetails()
        {
            try{
                Main.appFlag = Constants.appActivate;
                if(!FAFlag){
                    if("Y".Equals(Settings.getInstance().get("PRINT_ACTIVATE_FLAG").ToUpper())){
                        DirectDebitDetailsPrintFlow();
                    }else{
                        OpenMacro(TestDataConstants.Act_DirectDebitDetails_Macro);
                        string sqlQuery = "select  *  from [ACT_Direct_Debit_Details] where [Reference] = '"+Main.InputData[0]+"'";
                        dbUtility.ReadDBResultMS(sqlQuery);
                        string paymentFromAboveAccountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_DirectDebitDetails_DDpaymentFromAboveAcc);
                        if(TOEFlag){
                            RxPath btnRx = fetchElement("Activate_Direct_Debit_Details_BTN_Yes");
                            Button btnYes = null;
                            Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                            if(Host.Local.TryFindSingle(btnRx, durationTime,out btnYes)){
                                btnYes.Click();
                            }
                        }
                        if(!string.IsNullOrEmpty(paymentFromAboveAccountDB)){
                            CheckBox paymentFromAboveAccount = fetchElement("Activate_Direct Debit Details_CBX_Direct_Debit_Payment");
                            checkboxOperation(paymentFromAboveAccountDB, paymentFromAboveAccount);
                        }
                        
                        Text acctNo = fetchElement("Activate_Direct Debit Details_TXT_AccountNo");
                        if(!string.IsNullOrEmpty(acctNo.TextValue)){
                            Button okDirectDebitDetails = fetchElement("Activate_Direct Debit Details_BTN_OK");
                            okDirectDebitDetails.Click();
                        }else{
                            Report.Error("Account Number is not populated. Please correct the Data on DirectDebitDetails Screen");
                        }
                    }
                }else{
                    OpenMacro(TestDataConstants.Act_DirectDebitDetails_Macro);
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Button okDirectDebitDetails = fetchElement("Activate_Direct Debit Details_BTN_OK");
                    okDirectDebitDetails.Click();
                }
                
                //waitForPagetoAppear(TestDataConstants.Page_TopMenu);
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void DirectDebitDetailsPrintFlow(){
            OpenMacro(TestDataConstants.Act_DirectDebitDetails_MacroPrint);
            string sqlQuery = "select  *  from [ACT_Direct_Debit_Details] where [Reference] = '"+Main.InputData[0]+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            string paymentFromAboveAccountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_DirectDebitDetails_DDpaymentFromAboveAcc);
            if(TOEFlag){
                RxPath btnRx = fetchElement("Activate_Direct_Debit_Details_BTN_Yes");
                ClickButtonRx(btnRx);
            }
            if(!string.IsNullOrEmpty(paymentFromAboveAccountDB)){
                CheckBox paymentFromAboveAccount = fetchElement("Activate_Direct Debit Details_CBX_Direct_Debit_Payment");
                checkboxOperation(paymentFromAboveAccountDB, paymentFromAboveAccount);
            }
            
            Text acctNo = fetchElement("Activate_Direct Debit Details_TXT_AccountNo");
            if(!string.IsNullOrEmpty(acctNo.TextValue)){
                Button okDirectDebitDetails = fetchElement("Activate_Direct Debit Details_BTN_OK");
                okDirectDebitDetails.Click();
            }else{
                Report.Error("Account Number is not populated. Please correct the Data on DirectDebitDetails Screen");
            }
            
            PrintPackageReviewDocumentOk();
            LetterProductionLetterReviewOk();
            PrintHandleOk();
            PrintHandleOk();
            PrintHandleOk();
            PrintHandleOk();
            PrintPackageReviewDocumentOk();
            LetterProductionLetterReviewOk();
            LetterProductionParagraphSelectionOk();
            PrintHandleOk();
            
        }
    }
}
