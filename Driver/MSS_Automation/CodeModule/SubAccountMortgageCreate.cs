/*
 * Created by Ranorex
 * User: pratagarwal
 * Date: 22/06/2022
 * Time: 10:40
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
        public List<string> SubAccountMortgageCreate(){
            List<string> output = new List<string>();
            try{
                string testDataReference=Main.InputData[0];
                string society=Main.InputData[1];
                string accountNumber=Main.InputData[2];
                string accountTypeInput=String.Empty;
                
                if(Main.InputData.Count==4)
                {
                    accountTypeInput=Main.InputData[3];
                }
                string sqlQuery = "select  *  from [Sub_Account_Mortgage] where [Account Reference] = '"+testDataReference+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                MenuPromptInternal("SALC");

                Text TXT_society=fetchElement("Summit_SubAccount_Loan_Create_TXT_Society");
                Text TXT_accountNumber=fetchElement("Summit_SubAccount_Loan_Create_TXT_AccountNo");
                Text TXT_accountType=fetchElement("Summit_SubAccount_Loan_Create_TXT_AccountType");
                
                InputText(TXT_society,society);
                InputText(TXT_accountNumber,accountNumber);
                
                string accountTypeFromDB=dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Account_Type).Trim();
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
                
                Text TXT_subAccountNumber=fetchElement("Summit_SubAccount_Loan_Create_TXT_SubAccountNo");
                string subAccountNum=TXT_subAccountNumber.TextValue;
                string subAccountFormatted = utility.formattedSubAccount(accountNumber,subAccountNum);
                Sub_Account_Loan_Page1();
                Sub_Account_Loan_Page2();
                
                Button BTN_Ok=fetchElement("Summit_Notification_Warning_BTN_OK");
                BTN_Ok.Click();

                output.Add(Constants.TS_STATUS_PASS);
                output.Add(subAccountNum);
                output.Add(subAccountFormatted);
                
            } catch (Exception e){
                output.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                Report.Info(e.StackTrace);
            }
            return output;
        }
        
        private void Sub_Account_Loan_Page1()
        {
            CheckBox CBX_Page1_PersonalisedAccount=fetchElement("Summit_SubAccount_Loan_Create_CBX_PersonalisedAccount");
            Text TXT_Page1_InterestControlCode=fetchElement("Summit_SubAccount_Loan_Create_TXT_InterestControlCode");
            Text TXT_Page1_TermBandStartDate=fetchElement("Summit_SubAccount_Loan_Create_TXT_TermBandStartDate");
            Text TXT_Page1_OldAccountNo=fetchElement("Summit_SubAccount_Loan_Create_TXT_OldAccountNo");
            Text TXT_Page1_DefaulttaxtGroup=fetchElement("Summit_SubAccount_Loan_Create_TXT_DefaulttaxtGroup");
            CheckBox CBX_Page1_SocietyControl=fetchElement("Summit_SubAccount_Loan_Create_CBX_SocietyControl");
            Text TXT_Page1_ControlStatus=fetchElement("Summit_SubAccount_Loan_Create_TXT_ControlStatus");
            Text TXT_Page1_ControlLevel=fetchElement("Summit_SubAccount_Loan_Create_TXT_ControlLevel");
            CheckBox CBX_Page1_MIRAS5Required=fetchElement("Summit_SubAccount_Loan_Create_CBX_MIRAS5Required");
            Text TXT_Page1_AgreedAdvance=fetchElement("Summit_SubAccount_Loan_Create_TXT_AgreedAdvance");
            Text TXT_Page1_OriginalTerm=fetchElement("Summit_SubAccount_Loan_Create_TXT_OriginalTerm");
            Text TXT_Page1_TermDate=fetchElement("Summit_SubAccount_Loan_Create_TXT_TermDate");
            CheckBox CBX_Page1_RegulatedIndicator=fetchElement("Summit_SubAccount_Loan_Create_CBX_RegulatedIndicator");
            Text TXT_Page1_RegulationCode=fetchElement("Summit_SubAccount_Loan_Create_TXT_RegulationCode");
            
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Personalised_Account),CBX_Page1_PersonalisedAccount);
            setText(TXT_Page1_InterestControlCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Interest_Control_Code));
            setText(TXT_Page1_TermBandStartDate,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Term_Band_Start_Date));
            setText(TXT_Page1_OldAccountNo,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Old_Account_No));
            setText(TXT_Page1_DefaulttaxtGroup,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Default_Tax_Group));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Society_Control),CBX_Page1_SocietyControl);
            setText(TXT_Page1_ControlStatus,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Control_Status));
            setText(TXT_Page1_ControlLevel,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Control_Level));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_MIRAS_5_Required),CBX_Page1_MIRAS5Required);
            setText(TXT_Page1_AgreedAdvance,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Agreed_Advance));
            setText(TXT_Page1_OriginalTerm,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Original_Term));
            //setText(TXT_Page1_TermDate,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Term_Date));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Regulated_Ind),CBX_Page1_RegulatedIndicator);
            setText(TXT_Page1_RegulationCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Regulation_Code));
            Utility.Capture_Screenshot();
        }
        
        
        private void Sub_Account_Loan_Page2()
        {
            
            TabPage page2 = fetchElement("Summit_SubAccount_Loan_Create_TABREGN_Page2");
            page2.Click();
            
            Text TXT_Page2_Title=fetchElement("Summit_SubAccount_Loan_Create_TXT_Title");
            Text TXT_Page2_ArrearsCode=fetchElement("Summit_SubAccount_Loan_Create_TXT_ArrearsCode");
            Text TXT_Page2_CapAdeqGroup=fetchElement("Summit_SubAccount_Loan_Create_TXT_CapAdeqGroup");
            ComboBox LST_Page2_LoyaltyFlag=fetchElement("Summit_SubAccount_Loan_Create_LST_LoyaltyFlag");
            CheckBox CBX_Page2_Passbook=fetchElement("Summit_SubAccount_Loan_Create_CBX_Passbook");
            CheckBox CBX_Page2_PenaltyApplied=fetchElement("Summit_SubAccount_Loan_Create_CBX_PenaltyApplied");
            Text TXT_Page2_ReviewDate=fetchElement("Summit_SubAccount_Loan_Create_TXT_ReviewDate");
            Text TXT_Page2_RedemptionControl=fetchElement("Summit_SubAccount_Loan_Create_TXT_RedemptionControl");
            Text TXT_Page2_PenaltyTermStartDate=fetchElement("Summit_SubAccount_Loan_Create_TXT_PenaltyTermStartDate");
            ComboBox LST_Page2_ICBAuthorised=fetchElement("Summit_SubAccount_Loan_Create_LST_ICBAuthorised");
            Text TXT_Page2_ICBLoanType=fetchElement("Summit_SubAccount_Loan_Create_TXT_ICBLoanType");
            Text TXT_Page2_SectorCode=fetchElement("Summit_SubAccount_Loan_Create_TXT_SectorCode");
            Text TXT_Page2_SectorSubCode=fetchElement("Summit_SubAccount_Loan_Create_TXT_SectorSubCode");
            Text TXT_Page2_Classification=fetchElement("Summit_SubAccount_Loan_Create_TXT_Classification");
            Text TXT_Page2_OverridingPropertyUsage=fetchElement("Summit_SubAccount_Loan_Create_TXT_OverridingPropertyUsage");
            Text TXT_Page2_LoanPurpose=fetchElement("Summit_SubAccount_Loan_Create_TXT_LoanPurpose");
            Text TXT_Page2_StatementGroup=fetchElement("Summit_SubAccount_Loan_Create_TXT_StatementGroup");
            ComboBox LST_Page2_PrincipalSubAccount=fetchElement("Summit_SubAccount_Loan_Create_LST_PrincipalSubAccount");
            Text TXT_Page2__NextStatementDate=fetchElement("Summit_SubAccount_Loan_Create_TXT_NextStatementDate");
            ComboBox LST_Page2_FiscalPrincipalSubAccount=fetchElement("Summit_SubAccount_Loan_Create_LST_FiscalPrincipalSubAccount");
            Text TXT_Page2_NextFiscalPrincipalSubAccount=fetchElement("Summit_SubAccount_Loan_Create_LST_NextFiscalPrincipalSubAccount");
            
            setText(TXT_Page2_Title,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Title));
            setText(TXT_Page2_ArrearsCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Arrears_Code));
            setText(TXT_Page2_CapAdeqGroup,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Cap_Adeqs_Group));
            SelectValue(LST_Page2_LoyaltyFlag,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Loyalty_Flag));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Passbook),CBX_Page2_Passbook);
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Passbook),CBX_Page2_PenaltyApplied);
            setText(TXT_Page2_ReviewDate,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Review_Date));
            setText(TXT_Page2_RedemptionControl,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Redemption_Control));
            setText(TXT_Page2_PenaltyTermStartDate,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Penalty_Term_Start_Date));
            SelectValue(LST_Page2_ICBAuthorised,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_ICB_Authorised));
            setText(TXT_Page2_ICBLoanType,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_ICB_Loan_Type));
            setText(TXT_Page2_SectorCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Sector_Code));
            setText(TXT_Page2_SectorSubCode,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Sector_Subcode));
            setText(TXT_Page2_Classification,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Classification));
            setText(TXT_Page2_OverridingPropertyUsage,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Overrding_Property_Usage));
            setText(TXT_Page2_LoanPurpose,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Loan_Purpose));
            setText(TXT_Page2_StatementGroup,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Statement_Group));
            SelectValue(LST_Page2_PrincipalSubAccount,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Principal_Sub_AC));
            string nextStmtDate=dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Next_Statement_Date);
            if(!"DEFAULT".Equals(nextStmtDate))
            {
                setText(TXT_Page2__NextStatementDate,nextStmtDate);
            }
            SelectValue(LST_Page2_FiscalPrincipalSubAccount,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Fiscal_Principal_Sub_AC));
            InputText(TXT_Page2_NextFiscalPrincipalSubAccount,dbUtility.GetAccessFieldValue(TestDataConstants.Sub_Account_Mortgage_Next_Fiscal_Statement_Date));
            Utility.Capture_Screenshot();
        }
    }
}