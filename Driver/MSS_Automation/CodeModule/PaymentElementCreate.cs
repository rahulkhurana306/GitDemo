/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 21/06/2022
 * Time: 13:27
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using ng_mss_automation.CodeModule;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of PaymentElementCreate.
    /// </summary>
    public partial class Keywords
    {
        public List<string>  PaymentElementCreate()
        {
            try
            {
                string testDataRef = Main.InputData[0];
                string societyInput = Main.InputData[1];
                string accountNumberInput = Main.InputData[2];
                string subAccountNumberInput = Main.InputData[3];
                
                string sqlQuery = "select * from [Payment_Element] where [Account Reference] = '"+testDataRef+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                MenuPromptInternal("PAYVM");
                
                string workingCalDate = null;
                string payTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Pay_Type);
                string effDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Eff_Date);
                string debitArrsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_DebitArrs);
                string allowOverPaymentDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_AllowOverPayments);
                string paymentPriorityDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Payment_Priority);
                string stopDebitDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Stop_Debit);
                string scheduledDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Scheduled);
                string paymentStyleDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Payment_Style);
                string frequencyNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Frequency_No);
                string frenquencyDurationDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Frenquency_Duration);
                string firstDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_First_Date);
                string firstAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_First_Amount);
                string regularDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Regular_Date);
                string regularAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Regular_Amount);
                string lastDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Payments_Create_Last_Date);
                
                Text society = fetchElement("Summit_Find_Account_Details_TXT_Society");
                Text accountNumber = fetchElement("Summit_Find_Account_Details_TXT_Account_Number");
                setText(society, societyInput);
                setText(accountNumber, accountNumberInput+"{Tab}");
                Button buttonOk = null;
                if(ElementDisplayed(fetchElement("Summit_Payments_View_Manage_TXT_Warning"))){
                    buttonOk = fetchElement("Summit_Notification_Warning_BTN_OK");
                    buttonOk.Click();
                    //string subAccNoFormatted = utility.formattedSubAccount(accountNumberInput, subAccountNumberInput);
                    Text subAcct = "/form[@title='"+Settings.getInstance().get("SUMMIT_TITLE")+"']//text[@caption~'/"+subAccountNumberInput+"$']";
                    subAcct.Click();
                }else{
                    Text subAccount = fetchElement("Summit_Find_Account_Details_TXT_SubAccount");
                    setText(subAccount, subAccountNumberInput+"{Tab}");
                }
                Utility.Capture_Screenshot();
                
                if(ElementDisplayed(fetchElement("Summit_Payments_View_Manage_TXT_Warning"))){
                    buttonOk = fetchElement("Summit_Notification_Warning_BTN_OK");
                    buttonOk.EnsureVisible();
                    buttonOk.Click();
                }
                
                Button createPayment = fetchElement("Summit_Payments_View_Manage_BTN_CreatePayment");
                createPayment.Click();
                
                ComboBox createPaymentType = fetchElement("Summit_Payments_Create_LST_CreatePaymentType");
                Text paymentEffectiveDate = fetchElement("Summit_Payments_Create_TXT_PaymentEffectiveDate");
                Button buttonNext = fetchElement("Summit_Payments_Create_BTN_Next");
                selectValue(createPaymentType, payTypeDB);
                workingCalDate = utility.ProcessWCALDate(effDateDB,societyInput);
                setText(paymentEffectiveDate, workingCalDate);
                buttonNext.Click();
                
                CheckBox debitArrs = fetchElement("Summit_Payments_Create_CBX_DebitArrs");
                CheckBox allowOverPayment = fetchElement("Summit_Payments_Create_CBX_AllowOverPayment");
                Text paymentPriority = fetchElement("Summit_Payments_Create_TXT_PaymentPriority");
                CheckBox stopDebit = fetchElement("Summit_Payments_Create_CBX_StopDebit");
                CheckBox scheduled = fetchElement("Summit_Payments_Create_CBX_Scheduled");
                
                checkboxOperation(debitArrsDB, debitArrs);
                checkboxOperation(allowOverPaymentDB, allowOverPayment);
                setText(paymentPriority, paymentPriorityDB);
                checkboxOperation(stopDebitDB, stopDebit);
                checkboxOperation(scheduledDB, scheduled);
                Utility.Capture_Screenshot();

                buttonNext.Click();
                
                RadioButton paymentStyleRegular = fetchElement("Summit_Payments_Create_RAD_PaymentStyle_Regular");
                RadioButton paymentStyleSingle = fetchElement("Summit_Payments_Create_RAD_PaymentStyle_Single");
                if(paymentStyleDB.Equals("R")){
                    paymentStyleRegular.Click();
                }else if(paymentStyleDB.Equals("S")){
                    paymentStyleSingle.Click();
                }
                
                Text frequencyNo = fetchElement("Summit_Payments_Create_TXT_FrequencyNo");
                Text frenquencyDuration = fetchElement("Summit_Payments_Create_TXT_FrenquencyDuration");
                Text firstDate = fetchElement("Summit_Payments_Create_TXT_FirstDate");
                Text firstAmount = fetchElement("Summit_Payments_Create_TXT_FirstAmount");
                Text regularDate = fetchElement("Summit_Payments_Create_TXT_RegularDate");
                Text regularAmount = fetchElement("Summit_Payments_Create_TXT_RegularAmount");
                Text lastDate = fetchElement("Summit_Payments_Create_TXT_Last_Date");
                setText(frequencyNo, frequencyNoDB);
                setText(frenquencyDuration, frenquencyDurationDB);
                workingCalDate = utility.ProcessWCALDate(firstDateDB,societyInput);
                InputText(firstDate, workingCalDate);
                InputText(firstAmount, firstAmountDB);
                workingCalDate = utility.ProcessWCALDate(regularDateDB,societyInput);
                InputText(regularDate, workingCalDate);
                InputText(regularAmount, regularAmountDB);
                workingCalDate = utility.ProcessWCALDate(lastDateDB,societyInput);
                InputText(lastDate, workingCalDate);
                Utility.Capture_Screenshot();
                
                Button finish = fetchElement("Summit_Payments_Create_BTN_Finish");
                finish.Click();
                buttonOk = fetchElement("Summit_Notification_Warning_BTN_OK");
                buttonOk.Click();
                Button close = fetchElement("Summit_Payments_View_Manage_BTN_Close");
                close.Click();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Report.Error(e.StackTrace);
                return Main.OutputData;
            }
        }
        
        
    }
}
