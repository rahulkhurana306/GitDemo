/*
 * Created by Ranorex
 * User: pratagarwal
 * Date: 24/06/2022
 * Time: 10:05
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of FundsMovementCreate.
    /// </summary>
    public partial class Keywords
    {
        public List<string> FundsMovementCreate(){
            
            try{
                int inputLen = Main.InputData.Count;
                string testDataReference=Main.InputData[0];
                string society=Main.InputData[1];
                string accountNumber=Main.InputData[2];
                
                string sqlQuery = "select * from [Funds_Movement] where [Script Reference] = '"+testDataReference+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                MenuPromptInternal("FMVM");
                
                Text TXT_society=fetchElement("Summit_Find_Account_Details_TXT_Society");
                Text TXT_accountNumber=fetchElement("Summit_Find_Account_Details_TXT_Account_Number");
                ComboBox LST_Mandate_Type=fetchElement("Summit_Funds_Movement_View_Manage_LST_MandateType");
                
                setText(TXT_society, society);
                setText(TXT_accountNumber,accountNumber+"{Tab}");
                string warning_popup_title=fetchElement("Summit_Funds_Movement_View_Manage_TXT_Warning_Msg");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                if(ElementDisplayed(warning_popup_title)){
                    Utility.Capture_Screenshot();
                    Keyboard.Press("{ENTER}");
                }
                //TODO: Add for Direct Debits
                SelectValue(LST_Mandate_Type,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Mandate_Type));
                Funds_Movement_Create_Page();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
                
            } catch (Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Report.Info(e.StackTrace);
                return Main.OutputData;
            }
        }
        
        private void Funds_Movement_Create_Page()
        {
            
            Button create=fetchElement("Summit_Funds_Movement_View_Manage_BTN_Create");
            create.Click();
            
            string paymentStyleDB = dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Payment_Style);
            string createLinkType = dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Payment_Link);
            string allowAutoRecalc = dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Allow_Auto_Recalc);
            
            RadioButton paymentStyleRegular = fetchElement("Summit_Funds_Movement_View_Create_RAD_Regular_Payment_Style");
            RadioButton paymentStyleSingle = fetchElement("Summit_Funds_Movement_View_Create_RAD_Single_Payment_Style");
            string warning_popup_title=fetchElement("Summit_Funds_Movement_View_Manage_TXT_Warning_Msg");
            
            if(paymentStyleDB.Equals("R")){
                paymentStyleRegular.Click();
            }else if(paymentStyleDB.Equals("S")){
                paymentStyleSingle.Click();
            }
            
            Text TXT_To_Sub_Account=fetchElement("Summit_Funds_Movement_View_Create_TXT_To_Sub_Account");
            Text TXT_From_Sort_Code=fetchElement("Summit_Funds_Movement_View_Create_TXT_From_Sort_Code");
            Text TXT_From_Account_Number=fetchElement("Summit_Funds_Movement_View_Create_TXT_From_Account_Number");
            Text TXT_Statement_Reference=fetchElement("Summit_Funds_Movement_View_Create_TXT_Statement_Reference");
            Text TXT_Reference=fetchElement("Summit_Funds_Movement_View_Create_TXT_Reference");
            Text TXT_Payer=fetchElement("Summit_Funds_Movement_View_Create_TXT_Payer");
            Text TXT_Payee=fetchElement("Summit_Funds_Movement_View_Create_TXT_Payee");
            Text TXT_Customer_Refrence=fetchElement("Summit_Funds_Movement_View_Create_TXT_Customer_Refrence");
            Text TXT_Society_Bank_Account=fetchElement("Summit_Funds_Movement_View_Create_TXT_Society_Bank_Account");
            Text TXT_Effective_Date=fetchElement("Summit_Funds_Movement_View_Create_TXT_Effective_Date");
            RadioButton	RAD_Manual_Payment_Create_Link=fetchElement("Summit_Funds_Movement_View_Create_RAD_Manual_Payment_Create_Link");
            RadioButton RAD_Link_To_Payment_Due=fetchElement("Summit_Funds_Movement_View_Create_RAD_Link_To_Payment_Due");
            Button BTN_Create_Link=fetchElement("Summit_Funds_Movement_View_Create_BTN_Create_Link");
            Button BTN_OK_Form=fetchElement("Summit_Funds_Movement_View_Create_BTN_OK_Form");
            Button BTN_Close_Form=fetchElement("Summit_Funds_Movement_View_Create_BTN_Close_Form");
            
            int inputLen = Main.InputData.Count;
            string subaccountNumber=Main.InputData[3];
            string shortcode = null;
            string bankaccountnum = null;
            
            if(inputLen>4){
                if(!String.IsNullOrEmpty(Main.InputData[4])){
                    shortcode = Main.InputData[4];
                }
                if(!String.IsNullOrEmpty(Main.InputData[5])){
                    bankaccountnum = Main.InputData[5];
                }
            }
            
            setText(TXT_To_Sub_Account,subaccountNumber);
            
            if(!String.IsNullOrWhiteSpace(shortcode))
            {
                InputText(TXT_From_Sort_Code,shortcode);
            }
            else{
                InputText(TXT_From_Sort_Code,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_From_Sort_Code));
            }
            
            if(!String.IsNullOrWhiteSpace(bankaccountnum))
            {
                InputText(TXT_From_Account_Number,bankaccountnum);
            }
            else{
                InputText(TXT_From_Account_Number,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_From_Account_No));
            }
            
            setText(TXT_Statement_Reference,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Statement_Reference));
            setText(TXT_Reference,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Reference));
            setText(TXT_Payer,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Payer));
            setText(TXT_Payee,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Payee));
            InputText(TXT_Society_Bank_Account,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Society_Bank_Account));           
            InputText(TXT_Effective_Date,utility.ProcessWCALDate(dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Effective_Date)));
            Utility.Capture_Screenshot();
            //Only applicable when Payment style is Regular - so additional check must be added.
            if(paymentStyleDB.Equals("R") && createLinkType.Equals("M")){
                Text TXT_Regular_Frequency_Interval=fetchElement("Summit_Funds_Movement_View_Create_TXT_Regular_Frequency_Interval");
                Text TXT_Regular_Frequency_Interval_Months=fetchElement("Summit_Funds_Movement_View_Create_TXT_Regular_Frequency_Interval_Months");
                setText(TXT_Regular_Frequency_Interval_Months,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Regular_Frequency_Interval_Payment));
                setText(TXT_Regular_Frequency_Interval,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Regular_Frequency_Interval_No));
                Utility.Capture_Screenshot();
                RAD_Manual_Payment_Create_Link.Click();
                
                Text TXT_First_Date=fetchElement("Summit_Funds_Movement_View_Create_TXT_First_Date");
                Text TXT_First_Amount=fetchElement("Summit_Funds_Movement_View_Create_TXT_First_Amount");
                Text TXT_Regular_Date=fetchElement("Summit_Funds_Movement_View_Create_TXT_Regular_Date");
                Text TXT_Regular_Amount=fetchElement("Summit_Funds_Movement_View_Create_TXT_Regular_Amount");
                Text TXT_Final_Date=fetchElement("Summit_Funds_Movement_View_Create_TXT_Final_Date");
                Text TXT_Final_Amount=fetchElement("Summit_Funds_Movement_View_Create_TXT_Final_Amount");
                
                string firstDate = dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_First_Date);
                if(! firstDate.Equals(Constants.TestData_DEFAULT))
                {
                    InputText(TXT_First_Date,utility.ProcessWCALDate(firstDate));
                }
                
                InputText(TXT_First_Amount,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_First_Amount));
                string regularDate = dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Regular_Date);
                if(! regularDate.Equals(Constants.TestData_DEFAULT))
                {
                    InputText(TXT_Regular_Date,utility.ProcessWCALDate(regularDate));
                }
                InputText(TXT_Regular_Amount,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Regular_Amount));
                string finalDate = dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Final_Date);
                if(! finalDate.Equals(Constants.TestData_DEFAULT))
                {
                    InputText(TXT_Final_Date,utility.ProcessWCALDate(finalDate));
                }
                InputText(TXT_Final_Amount,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Final_Amount));
                Utility.Capture_Screenshot();
            }

            if(paymentStyleDB.Equals("S") && createLinkType.Equals("M")){
                RAD_Manual_Payment_Create_Link.Click();
                
                Text TXT_Single_Date=fetchElement("Summit_Funds_Movement_View_Create_TXT_First_Date");
                Text TXT_Single_Amounts=fetchElement("Summit_Funds_Movement_View_Create_TXT_SingleAmount_Single");
                setText(TXT_Single_Amounts,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Single_Amount));
                
                if(! TXT_Single_Date.Equals(Constants.TestData_DEFAULT))
                {
                    setText(TXT_Single_Date,utility.ProcessWCALDate(dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Single_Date)));
                }
                Utility.Capture_Screenshot();
            }
            if(paymentStyleDB.Equals("R") && createLinkType.Equals("L")){
                
                RAD_Link_To_Payment_Due.Click();
                Text TXT_Regular_Frequency_Interval=fetchElement("Summit_Funds_Movement_View_Create_TXT_Regular_Frequency_Interval");
                Text TXT_Regular_Frequency_Interval_Months=fetchElement("Summit_Funds_Movement_View_Create_TXT_Regular_Frequency_Interval_Months");
                InputText(TXT_Regular_Frequency_Interval_Months,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Regular_Frequency_Interval_Payment));
                InputText(TXT_Regular_Frequency_Interval,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Regular_Frequency_Interval_No));
                Utility.Capture_Screenshot();
                BTN_Create_Link.Click();
                
                if(ElementDisplayed(warning_popup_title)){
                    Keyboard.Press("{ENTER}");
                }
                Button BTN_Accept_Paymnet_Form=fetchElement("Summit_Funds_Movement_View_Create_BTN_Accept_Paymnet_Form");
                BTN_Accept_Paymnet_Form.Click();
                if(ElementDisplayed(warning_popup_title)){
                    Keyboard.Press("{ENTER}");
                }
                //TODO for First and Regular in future (if required)
                Text TXT_Final_Date=fetchElement("Summit_Funds_Movement_View_Create_TXT_Final_Date");
                Text TXT_Final_Amount=fetchElement("Summit_Funds_Movement_View_Create_TXT_Final_Amount");
                string finalDate = dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Final_Date);
                InputText(TXT_Final_Date,utility.ProcessWCALDate(finalDate));
                InputText(TXT_Final_Amount,dbUtility.GetAccessFieldValue(TestDataConstants.Fund_Movement_Create_Final_Amount));
                Utility.Capture_Screenshot();
            } if(paymentStyleDB.Equals("S") && createLinkType.Equals("L")){
                
                //                ComboBox CBX_Auto_Allow_Recalc=fetchElement("Summit_Funds_Movement_View_Create_CBX_Auto_Allow_Recalc");
                RAD_Link_To_Payment_Due.Click();
                BTN_Create_Link.Click();
                if(ElementDisplayed(warning_popup_title)){
                    Keyboard.Press("{ENTER}");
                }
                Button BTN_Accept_Paymnet_Form=fetchElement("Summit_Funds_Movement_View_Create_BTN_Accept_Paymnet_Form");
                BTN_Accept_Paymnet_Form.Click();
                if(ElementDisplayed(warning_popup_title)){
                    Keyboard.Press("{ENTER}");
                }
            }
            
            BTN_OK_Form.Click();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            if(ElementDisplayed(warning_popup_title)){
                Keyboard.Press("{ENTER}");
            }
            if(ElementDisplayed(warning_popup_title)){
                Keyboard.Press("{ENTER}");
            }
            Utility.Capture_Screenshot();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            BTN_Close_Form.Click();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
        }
        
    }
}