/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 17/06/2022
 * Time: 10:24
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
    /// Creates a Ranorex user code collection. A collection is used to publish user code methods to the user code library.
    /// </summary>
    public partial class Keywords
    {
        public List<string> SubAccountSavingCreate(){
            try{
                string testDataReference=Main.InputData[0];
                string society=Main.InputData[1];
                string accountNumber=Main.InputData[2];
                string accountTypeInput=String.Empty;
                
                if(Main.InputData.Count==4)
                {
                    accountTypeInput=Main.InputData[3];
                }
                
                Report.Info(testDataReference);//TestDATAReference
                Report.Info(society);//SOCIETY
                Report.Info(accountNumber);//ACCOUNT_NUMBER
                
                
                string sqlQuery = "select  *  from [Sub_Account_Investment] where [Account Reference] = '"+testDataReference+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                MenuPromptInternal("SASC");

                //Common Part
                Text TXT_society=fetchElement("Summit_SubAccount_Savings_Create_TXT_Society");
                Text TXT_accountNumber=fetchElement("Summit_SubAccount_Savings_Create_TXT_AccountNo");
                Text TXT_subAccountNumber=fetchElement("Summit_SubAccount_Savings_Create_TXT_SubAccNo");
                Text TXT_accountType=fetchElement("Summit_SubAccount_Savings_Create_TXT_AccountType");
                
                setText(TXT_society,society);
                setText(TXT_accountNumber,accountNumber+"{Tab}");
                string accountTypeFromDB=dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_AccType).Trim();
                if("NEW".Equals(accountTypeFromDB)){
                    if(String.IsNullOrEmpty(accountTypeInput))
                    {
                        throw new Exception("DB column says NEW AccountType & However AccountType Input Param is missing");
                    }
                    setText(TXT_accountType,accountTypeInput);    
                }else{
                    if(!String.IsNullOrEmpty(accountTypeInput))
                    {
                        throw new Exception("DB column doesn't have NEW & still AccountType Input Param is present.");
                    }
                    setText(TXT_accountType,accountTypeFromDB);
                }
                
                string subAccountNumber=TXT_subAccountNumber.TextValue;
                
                SubAccountSavingCreatePage1();
                SubAccountSavingCreatePage2();
                SubAccountSavingCreatePage3();
                SubAccountSavingCreatePage4();
                SubAccountSavingCreatePage5();

                //Common Control
                Button BTN_Ok=fetchElement("Summit_SubAccount_Savings_Create_BTN_OK");
                BTN_Ok.Click();
                
                Button BTN_notificationOk=fetchElement("Summit_Notification_Warning_BTN_OK");
                BTN_notificationOk.Click();


                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(subAccountNumber);
                Main.OutputData.Add(utility.formattedSubAccount(accountNumber,subAccountNumber));
                return Main.OutputData;
                
            } catch (Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                Report.Info(e.StackTrace);
                return Main.OutputData;
            }
        }
        
        private void SubAccountSavingCreatePage1()
        {
            //Page 1
            TabPage page1 = fetchElement("Summit_SubAccount_Savings_Create_TABREGN_Page1");
            page1.Click();
            
            CheckBox CBX_Page1_PersonalisedAccount=fetchElement("Summit_SubAccount_Savings_Create_CBX_Page1_PersonalisedAccount");
            CheckBox CBX_Page1_Passbook=fetchElement("Summit_SubAccount_Savings_Create_CBX_Page1_Passbook");
            Text TXT_Page1_InterestControlCode=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page1_InterestControlCode");
            Text TXT_Page1_OldAccountNo = fetchElement("Summit_SubAccount_Savings_Create_TXT_Page1_OldAccountNo");
            Text TXT_Page1_DefaultTaxGroup=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page1_DefaultTaxGroup");
            CheckBox CBX_Page1_SocietyControl=fetchElement("Summit_SubAccount_Savings_Create_CBX_Page1_SocietyControl");
            Text TXT_Page1_ControlStatus=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page1_ControlStatus");
            Text TXT_Page1_ControlLevel=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page1_ControlLevel");
            CheckBox CBX_Page1_PenaltyApplied=fetchElement("Summit_SubAccount_Savings_Create_CBX_Page1_PenaltyApplied");
            Text TXT_Page1_OriginalTerm=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page1_OriginalTerm");
            
            
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_PersonalisedAccount),CBX_Page1_PersonalisedAccount);
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Passbook),CBX_Page1_Passbook);
            setText(TXT_Page1_InterestControlCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_InterestControlCode));
            setText(TXT_Page1_OldAccountNo,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Old_Acc_Num));
            setText(TXT_Page1_DefaultTaxGroup,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Default_Tax_Group));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Society_Ctrl),CBX_Page1_SocietyControl);
            setText(TXT_Page1_ControlStatus,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Control_Status));
            setText(TXT_Page1_ControlLevel,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Control_Level));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Penalty_Applied),CBX_Page1_PenaltyApplied);
            setText(TXT_Page1_OriginalTerm,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Original_Term));
            Utility.Capture_Screenshot();
        }
        
        private void SubAccountSavingCreatePage2()
        {
            //  Page 2
            TabPage page2 = fetchElement("Summit_SubAccount_Savings_Create_TABREGN_Page2");
            page2.Click();
            
            Text TXT_Page2_Title=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page2_Title");
            Text TXT_Page2_ArrearsCode=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page2_ArrearsCode");
            Text TXT_Page2_CapAdequanciesGroup=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page2_CapAdequanciesGroup");
            Text TXT_Page2_ReviewDate=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page2_ReviewDate");
            
            Text TXT_Page2_CreditType=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page2_CreditType");
            Text TXT_Page2_CreditLimit=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page2_CreditLimit");
            Text TXT_Page2_CreditExpiryDate=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page2_CreditExpiryDate");

            ComboBox LST_Page2_OverrideEventCharges=fetchElement("Summit_SubAccount_Savings_Create_LST_Page2_OverrideEventCharges");
            ComboBox LST_Page2_OverrideTransCharges=fetchElement("Summit_SubAccount_Savings_Create_LST_Page2_OverrideTransCharges");
            ComboBox LST_Page2_AllowDDIs=fetchElement("Summit_SubAccount_Savings_Create_LST_Page2_AllowDDIs");
            ComboBox LST_Page2_LoyaltyFlag=fetchElement("Summit_SubAccount_Savings_Create_LST_Page2_LoyaltyFlag");


            setText(TXT_Page2_Title,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Title));
            setText(TXT_Page2_ArrearsCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_ArrearCode));
            setText(TXT_Page2_CapAdequanciesGroup,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_CapAdeq_Group));
            setText(TXT_Page2_ReviewDate,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Review_Date));
            
            setText(TXT_Page2_CreditType,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Credit_Type));
            setText(TXT_Page2_CreditLimit,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Credit_Limit));
            setText(TXT_Page2_CreditExpiryDate,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Credit_Expiry_Date));
            
            SelectValue(LST_Page2_OverrideEventCharges,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_OverrideEventCharges));
            SelectValue(LST_Page2_OverrideTransCharges,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_OverrideTransCharges));
            SelectValue(LST_Page2_AllowDDIs,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_AllowDDI));
            SelectValue(LST_Page2_LoyaltyFlag,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_LoyaltyFlag));
            Utility.Capture_Screenshot();
        }
        
        private void SubAccountSavingCreatePage3()
        {
            TabPage page3 = fetchElement("Summit_SubAccount_Savings_Create_TABREGN_Page3");
            page3.Click();
            
            Text TXT_Page3_AccountNo=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_AccountNo");
            Text TXT_Page3_PayCode=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_PayCode");
            Text TXT_Page3_SortCode=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_SortCode");
            Text TXT_Page3_Reference=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_Reference");
            Text TXT_Page3_Customer=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_Customer");
            Text TXT_Page3_SubAccountNo=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_SubAccountNo");
            Text TXT_Page3_BankAccountNo=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_BankAccountNo");
            Text TXT_Page3_RtlByAccType=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_RtlByAccType");
            Text TXT_Page3_RtlNonRtlGrouping=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_RtlNonRtlGrouping");
            Text TXT_Page3_SectorCode=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_SectorCode");
            Text TXT_Page3_SectorSubCode=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_SectorSubCode");
            Text TXT_Page3_HolderCategory=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_HolderCategory");
            Text TXT_Page3_ATMTransationAcc=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_ATMTransationAcc");
            Text TXT_Page3_EarlyIntPay=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_EarlyIntPay");
            Text TXT_Page3_NoticePeriodInd=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_NoticePeriodInd");
            Text TXT_Page3_NoticePeriod=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page3_NoticePeriod");


            
            setText(TXT_Page3_PayCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_PayCode));
            setText(TXT_Page3_Customer,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Customer));
            setText(TXT_Page3_SortCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_SortCode));
            setText(TXT_Page3_BankAccountNo,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_BankAccountNo));
            setText(TXT_Page3_AccountNo,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_AccountNo));
            setText(TXT_Page3_SubAccountNo,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_SubAccountNo));
            setText(TXT_Page3_Reference,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Reference));
            setText(TXT_Page3_RtlByAccType,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_RtlByAcc_Mat));
            setText(TXT_Page3_RtlNonRtlGrouping,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Rtl_Non_Grouping));
            setText(TXT_Page3_SectorCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_SectorCode));
            setText(TXT_Page3_SectorSubCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_SectorSubcode));
            setText(TXT_Page3_HolderCategory,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_HolderCategory));
            setText(TXT_Page3_ATMTransationAcc,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_ATM_Trans_Acc));
            setText(TXT_Page3_EarlyIntPay,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Early_Int_Pay));
            setText(TXT_Page3_NoticePeriodInd,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Notice_Period_Ind));
            setText(TXT_Page3_NoticePeriod,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Notice_Period));
            Utility.Capture_Screenshot();
        }
        
        private void SubAccountSavingCreatePage4()
        {
            TabPage page4 = fetchElement("Summit_SubAccount_Savings_Create_TABREGN_Page4");
            page4.Click();
            
            Text TXT_Page4_Group=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_Group");
            ComboBox LST_Page4_PrincipalSubAcc=fetchElement("Summit_SubAccount_Savings_Create_LST_Page4_PrincipalSubAcc");
            Text TXT_Page4_StatementType=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_StatementType");
            Text TXT_Page4_NumOfTransactions=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_NumOfTransactions");
            Text TXT_Page4_Interval=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_Interval");
            Text TXT_Page4_DayBaseMonth=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_DayBaseMonth");
            Text TXT_Page4_DayBaseMonth_Sub=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_DayBaseMonth_Sub");
            Text TXT_Page4_NextStatementBased=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_NextStatementBasedOn");
            Text TXT_Page4_NextDate=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_NextDate");
            ComboBox LST_Page4_FiscalPrincipalSub=fetchElement("Summit_SubAccount_Savings_Create_LST_Page4_FiscalPrincipalSubAcc");
            Text TXT_Page4_NextFiscalDate=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page4_NextFiscalDate");

            setText(TXT_Page4_Group,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Group));
            SelectValue(LST_Page4_PrincipalSubAcc,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_PrincipalSubAcc));
            setText(TXT_Page4_StatementType,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_StatementType));
            setText(TXT_Page4_NumOfTransactions,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Number_Transaction));
            setText(TXT_Page4_Interval,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Interval));
            setText(TXT_Page4_DayBaseMonth,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Day_Base_Month));
            setText(TXT_Page4_DayBaseMonth_Sub,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Day_Base_Month));
            setText(TXT_Page4_NextStatementBased,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Next_Statement));
            string society=Main.InputData[1];
            setText(TXT_Page4_NextDate,Utility.getInstance().ProcessWCALDate(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Next_Date),society));
            SelectValue(LST_Page4_FiscalPrincipalSub,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Fiscal_Princ_Sub_Acc));
            setText(TXT_Page4_NextFiscalDate,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Next_Fiscal_Date));
            Utility.Capture_Screenshot();
        }
        
        private void SubAccountSavingCreatePage5()
        {
            //  Page 5
            TabPage page5 = fetchElement("Summit_SubAccount_Savings_Create_TABREGN_Page5");
            page5.Click();
            
            Text TXT_Page5_DepositControlCode=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_DepositControlCode");
            Text TXT_Page5_FirstYearLimit=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_FirstYearLimit");
            Text TXT_Page5_CurrentTermInterval=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_CurrentTermInterval");
            Text TXT_Page5_BonusControlCode=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_BonusControlCode");
            Text TXT_Page5_BonusNextDueDate=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_BonusNextDueDate");
            Text TXT_Page5_MatureTESSADeps=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_MatureTESSADeps");
            Text TXT_Page5_TESSAMatured=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_TESSAMatured");
            ComboBox LST_Page5_MaturityCertificateReq=fetchElement("Summit_SubAccount_Savings_Create_LST_Page5_MaturityCertificateReq");
            Text TXT_Page5_MaturityCertificateRecevied=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_MaturityCertificateRecevied");
            Text TXT_Page5_AffinitySchemeBenefit=fetchElement("Summit_SubAccount_Savings_Create_TXT_Page5_AffinitySchemeBenefit");

            setText(TXT_Page5_DepositControlCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Deposit_Ctrl_Code));
            setText(TXT_Page5_FirstYearLimit,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_First_Year_Limit));
            setText(TXT_Page5_CurrentTermInterval,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Current_Term_Interval));
            setText(TXT_Page5_BonusControlCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Bonus_Ctrl_Code));
            setText(TXT_Page5_BonusNextDueDate,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Bonus_Next_Due_Date));
            setText(TXT_Page5_MatureTESSADeps,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Mature_Tessa_Deps));
            setText(TXT_Page5_TESSAMatured,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Tessa_Matured));
            SelectValue(LST_Page5_MaturityCertificateReq,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Maturity_Cert_Req));
            setText(TXT_Page5_MaturityCertificateRecevied,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Maturity_Cert_Rec));
            setText(TXT_Page5_AffinitySchemeBenefit,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Investment_Affinity_Scheme_Benefit));
            Utility.Capture_Screenshot();
        }
    }
}
