﻿/*
     * Created by Ranorex
     * User: pratagarwal
     * Date: 28/06/2022
     * Time: 11:05
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
    /// Description of Trasaction Posting.
    /// </summary>
    public partial class Keywords
    {
        public List<string> TransactionPosting(){
            try{
                
                if(Main.InputData.Count!=4){
                    Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                    Main.OutputData.Add("Transaction Posting Failed. Parameters expected=4,Actual"+Main.InputData.Count);
                }else{
                    string testDataReference=Main.InputData[0];
                    string society=Main.InputData[1];
                    
                    string sqlQuery = "select * from [Transaction_Posting] where [Account Reference] = '"+testDataReference+"'";
                    dbUtility.ReadDBResultMS(sqlQuery);
                    
                    MenuPromptInternal("TRDP");
                    
                    Text TXT_BatchNo=fetchElement("Summit_Transactions_Direct_Post_TXT_BatchNo");
                    Text TXT_Society=fetchElement("Summit_Transactions_Direct_Post_TXT_Society");
                    Text TXT_Source=fetchElement("Summit_Transactions_Direct_Post_TXT_Source");
                    CheckBox CBX_EnterChequeDetails=fetchElement("Summit_Transactions_Direct_Post_CBX_EnterChequeDetails");
                    Text TXT_Date=fetchElement("Summit_Transactions_Direct_Post_TXT_Date");
                    string Transaction_Posting_Batch_Enter_Cheque_Details = dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Batch_Enter_Cheque_Details);
                    string batchSource=dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Batch_Source);
                    
                    InputText(TXT_BatchNo, utility.GetOpenBatch(society));
                    InputText(TXT_Society,society);
                    setText(TXT_Source,batchSource);
                    
                    string Batch_Date = dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Batch_Date);
                    if(! Batch_Date.Equals(Constants.TestData_DEFAULT))
                    {
                        InputText(TXT_Date,utility.ProcessWCALDate(Batch_Date));
                    }
                    
                    if(dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Batch_Enter_Cheque_Details).Equals("Y")){
                        CBX_EnterChequeDetails.Check();
                    }
                    
                    Utility.Capture_Screenshot();
                    
                    Transaction_Page();
                    
                    Main.OutputData.Add(Constants.TS_STATUS_PASS);
                    
                }
                
                return Main.OutputData;
                
            } catch (Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add("Transaction Posting fail."+e.Message);
                return Main.OutputData;
            }
        }
        
        private void Transaction_Page()
        {
            
            string accountNumber=Main.InputData[2];
            string subAccountNumber=Main.InputData[3];
            
            Button enterTransaction=fetchElement("Summit_Transactions_Direct_Post_BTN_EnterTransaction");
            enterTransaction.Click();
            
            Text TXT_TransacationDetails_Date=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_Date");
            Text TXT_TransacationDetails_AccountNo=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_AccountNo");
            Text TXT_TransacationDetails_SubAccNo=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_SubAccNo");
            Text TXT_TransacationDetails_GlobalTransType=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_GlobalTransType");
            Text TXT_TransacationDetails_PaymentMethod=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_PaymentMethod");
            Text TXT_TransacationDetails_TransType=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_TransType");
            Text TXT_TransacationDetails_EffIntDate=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_EffIntDate");
            Text TXT_TransacationDetails_Currency=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_Currency");
            Text TXT_TransacationDetails_Amount=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_Amount");
            Text TXT_TransacationDetails_Reference=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_Reference");
            Text TXT_TransacationDetails_Payer=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_Payer");
            Text TXT_TransacationDetails_Payee=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_Payee");
            Text TXT_TransacationDetails_CustReference=fetchElement("Summit_Transactions_Direct_Post_TXT_TransacationDetails_CustReference");
            Button BTN_Posttransaction=fetchElement("Summit_Transactions_Direct_Post_BTN_Posttransaction");
            Button BTN_Clear=fetchElement("Summit_Transactions_Direct_Post_BTN_Clear");
            Button BTN_Reversal=fetchElement("Summit_Transactions_Direct_Post_BTN_Reversal");
            Button BTN_Override=fetchElement("Summit_Transactions_Direct_Post_BTN_Override");
            

            string Posting_Date = dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Date);
            if(! Posting_Date.Equals(Constants.TestData_DEFAULT))
            {
                InputText(TXT_TransacationDetails_Date,utility.ProcessWCALDate(Posting_Date));
                
                System.DateTime currentDate = System.DateTime.Parse(utility.ProcessWCALDate("WCAL_DATE"));
                System.DateTime TransactionDate = System.DateTime.Parse(utility.ProcessWCALDate(Posting_Date));
                
                int dayDifference  = (currentDate - TransactionDate).Days;
                
                if(dayDifference >30) {
                	Button dtAlert = fetchElement("Summit_Transactions_Direct_Post_BTN_Possible_Date_Error_OK");
                	dtAlert.Click();
                	BTN_Override.Click();
                	InputText(TXT_TransacationDetails_Date,utility.ProcessWCALDate(Posting_Date));
                }
                
            }
            
            InputText(TXT_TransacationDetails_AccountNo,accountNumber);
            InputText(TXT_TransacationDetails_SubAccNo,subAccountNumber);
           
            InputText(TXT_TransacationDetails_GlobalTransType,dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Gbl_Trans_Type));
            InputText(TXT_TransacationDetails_PaymentMethod,dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Payment_Meth));
            
            string Posting_Eff_Int_Date = dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Eff_Int_Date);
            if(! Posting_Eff_Int_Date.Equals(Constants.TestData_DEFAULT))
            {
                InputText(TXT_TransacationDetails_EffIntDate,utility.ProcessWCALDate(Posting_Eff_Int_Date));
                
            }
            
            setText(TXT_TransacationDetails_Amount,dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Amount));
            setText(TXT_TransacationDetails_Reference,dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Reference));
            setText(TXT_TransacationDetails_Payer,dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Payer));
            setText(TXT_TransacationDetails_Payee,dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Payee));
            setText(TXT_TransacationDetails_CustReference,dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Customer_Ref));
            
            string sourceOfFundsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Source_of_Funds);
            Delay.Milliseconds(10);
            if(!String.IsNullOrEmpty(sourceOfFundsDB)){
                string sourceOfFundsStr = fetchElement("Summit_Transactions_Direct_Post_LST_TransacationDetails_SourceOfFunds");
                ComboBox sourceOfFunds = sourceOfFundsStr;
                selectValue(sourceOfFunds, sourceOfFundsDB);
                string sourceOfFundsDescDB = dbUtility.GetAccessFieldValue(TestDataConstants.Transaction_Posting_Description);
                if(!String.IsNullOrEmpty(sourceOfFundsDescDB)){
                    Text desc = fetchElement("Summit_Transactions_Direct_Post_LST_TransacationDetails_Description");
                    setText(desc, sourceOfFundsDescDB );
                }
            }
            Utility.Capture_Screenshot();
            BTN_Posttransaction.Click();
            
            string LISAAlert = fetchElement("Summit_Transactions_Direct_Post_TXT_Lisa_Alert");
            if(ElementDisplayed(LISAAlert)){
                Button btn = fetchElement("Summit_Transactions_Direct_Post_BTN_Warning_Alert");
                btn.Click();
            }
            Button BTN_Close_Transaction_Post=fetchElement("Summit_Transactions_Direct_Post_BTN_Close_Transaction_Post");
            BTN_Close_Transaction_Post.Click();
        }
    }
}
