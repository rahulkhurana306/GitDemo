/*
 * Created by Ranorex
 * User: pratagarwal
 * Date: 08/07/2022
 * Time: 11:24
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
        public List<string> SubAccountInsuranceCreate(){
            try{
                string testDataReference=Main.InputData[0];
                string society=Main.InputData[1];
                string accountNumber=Main.InputData[2];
                string accountTypeInput=String.Empty;
                
                if(Main.InputData.Count==4)
                {
                    accountTypeInput=Main.InputData[3];
                }
                string sqlQuery = "select  *  from [Sub_Account_Insurance] where [Account Reference] = '"+testDataReference+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                MenuPromptInternal("SAIC");

                Text TXT_society=fetchElement("Summit_SubAccount_Insurance_Create_TXT_Society");
                Text TXT_accountNumber=fetchElement("Summit_SubAccount_Insurance_Create_TXT_AccountNo");
                Text TXT_accountType=fetchElement("Summit_SubAccount_Insurance_Create_TXT_AccountType");
                Text TXT_Title=fetchElement("Summit_SubAccount_Insurance_Create_TXT_Title");
                Text TXT_ControlStatus=fetchElement("Summit_SubAccount_Insurance_Create_TXT_ControlStatus");
                Text TXT_ControlLevel=fetchElement("Summit_SubAccount_Insurance_Create_TXT_ControlLevel");
                CheckBox CBX_SocietyControlled=fetchElement("Summit_SubAccount_Insurance_Create_CBX_SocietyControlled");
                Text TXT_InterestControl=fetchElement("Summit_SubAccount_Insurance_Create_TXT_InterestControl");
                Text TXT_DefaulttaxGroup=fetchElement("Summit_SubAccount_Insurance_Create_TXT_DefaulttaxGroup");
                Text TXT_OldAccountNo=fetchElement("Summit_SubAccount_Insurance_Create_TXT_OldAccountNo");
                Text TXT_ArrearsCode=fetchElement("Summit_SubAccount_Insurance_Create_TXT_ArrearsCode");
                Text TXT_RedemptionControl=fetchElement("Summit_SubAccount_Insurance_Create_TXT_RedemptionControl");
                Text TXT_PenaltyTermStartDate=fetchElement("Summit_SubAccount_Insurance_Create_TXT_PenaltyTermStartDate");
                Text TXT_subAccountNumber=fetchElement("Summit_SubAccount_Insurance_Create_TXT_SubAccountNo");
                
                Text TXT_SectorCode=fetchElement("Summit_SubAccount_Insurance_Create_TXT_SectorCode");
                Text TXT_SectorSubCode=fetchElement("Summit_SubAccount_Insurance_Create_TXT_SectorSubCode");
                Text TXT_Classification=fetchElement("Summit_SubAccount_Insurance_Create_TXT_Classification");
                Text TXT_StatementGroup=fetchElement("Summit_SubAccount_Insurance_Create_TXT_StatementGroup");
                ComboBox LST_PrincipalSubAccount=fetchElement("Summit_SubAccount_Insurance_Create_LST_PrincipalSubAccount");
                Text TXT_NextStatementDate=fetchElement("Summit_SubAccount_Insurance_Create_TXT_NextStatementDate");
                ComboBox LST_FiscalPrincipalSubAccount=fetchElement("Summit_SubAccount_Insurance_Create_LST_FiscalPrincipalSubAccount");
                Text TXT_NextFiscalStatementDate=fetchElement("Summit_SubAccount_Insurance_Create_TXT_NextFiscalStatementDate");
                
                InputText(TXT_society,society);
                InputText(TXT_accountNumber,accountNumber);
                
                string accountTypeFromDB=dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_AC_Type).Trim();
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
                
                setText(TXT_Title, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Title));
                checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Society_Controlled),CBX_SocietyControlled);
                setText(TXT_ControlStatus, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Control_Status));
                setText(TXT_ControlLevel, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Control_Level));
                setText(TXT_InterestControl, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Interest_Control));
                setText(TXT_DefaulttaxGroup, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Default_Tax_Group));
                setText(TXT_OldAccountNo, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Old_Account_No));
                setText(TXT_RedemptionControl, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Redemption_Control));
                setText(TXT_SectorCode, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Sector_Code));
                setText(TXT_SectorSubCode, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Sector_Subcode));
                setText(TXT_Classification, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Classification));
                setText(TXT_StatementGroup, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Statement_Group));
                SelectValue(LST_PrincipalSubAccount,dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Principal_Sub_Ac));
                setText(TXT_NextStatementDate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Next_Statement_Date));
                SelectValue(LST_FiscalPrincipalSubAccount,dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Fiscal_Principal_Sub_AC));
                setText(TXT_NextFiscalStatementDate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Next_Fiscal_Statement_Date));
                Utility.Capture_Screenshot();
                PolicyDetails();
                RiskMaintain();
                
                Button ok_Insurance = fetchElement("Summit_SubAccount_Insurance_Create_BTN_Ok");
                ok_Insurance.Click();
                
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
        private void PolicyDetails()
        {
            Button Policy_Details=fetchElement("Summit_SubAccount_Insurance_Policy_Details_BTN_PolicyDetail");
            Policy_Details.Click();

            Text TXT_InsuranceCompany=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_InsuranceCompany");
            Text TXT_InsuranceBranch=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_InsuranceBranch");
            Text TXT_InsuranceType=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_InsuranceType");
            Text TXT_InsurancePolicyNo=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_InsurancePolicyNo");
            Text TXT_CoverFromDate=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_CoverFromDate");
            Text TXT_CoverToDate=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_CoverToDate");
            Text TXT_PremiumDebitDueDate=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_PremiumDebitDueDate");
            Text TXT_RewardInd=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_RewardInd");
            Text TXT_RenewalDate=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_RenewalDate");
            Text TXT_Associate=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_Associate");
            Text TXT_NewRenewal=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_NewRenewal");
            Text TXT_PaymentFreq=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_PaymentFreq");
            Text TXT_NoofPayments=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_NoofPayments");
            Text TXT_DebitFreq=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_DebitFreq");
            CheckBox CBX_DebitFirstPrem=fetchElement("Summit_SubAccount_Insurance_Policy_Details_CBX_DebitFirstPrem");
            CheckBox CBX_CombinedPolicyInd=fetchElement("Summit_SubAccount_Insurance_Policy_Details_CBX_CombinedPolicyInd");
            ComboBox LST_PreviousPolicyIndexLinking=fetchElement("Summit_SubAccount_Insurance_Policy_Details_LST_PreviousPolicyIndexLinking");
            CheckBox CBX_Scheduled=fetchElement("Summit_SubAccount_Insurance_Policy_Details_CBX_Scheduled");
            Text TXT_CancellationDate=fetchElement("Summit_SubAccount_Insurance_Policy_Details_TXT_CancellationDate");
            
            
            setText(TXT_InsuranceCompany, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Insurance_Company));
            setText(TXT_InsuranceBranch, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Insurance_Branch));
            InputText(TXT_InsuranceType, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Insurance_Type));
            string popup_LOV=fetchElement("Summit_SubAccount_Insurance_Policy_Details_BTN_Find");
            if(isButtonDisplayed(popup_LOV)){
                Keyboard.Press("{ENTER}");
            }
            setText(TXT_InsurancePolicyNo, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Ins_Policy_No));
            setText(TXT_CoverFromDate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Cover_From_Date));
            setText(TXT_CoverToDate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Cover_To_Date));
            setText(TXT_PremiumDebitDueDate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Prem_Debit_Due_Date));
            setText(TXT_RewardInd, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Renewal_Ind));
            setText(TXT_RenewalDate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Renewal_Date));
            setText(TXT_Associate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Associate));
            setText(TXT_NewRenewal, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_New_Renewal));
            setText(TXT_PaymentFreq, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Payment_Freq));
            setText(TXT_NoofPayments, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_No_of_Payments));
            setText(TXT_DebitFreq, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Debit_Freq));
            
            
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Debit_First_Prem),CBX_DebitFirstPrem);
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Combined_Policy_Ind),CBX_CombinedPolicyInd);
            SelectValue(LST_PreviousPolicyIndexLinking,dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Previous_Policy_Linking));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Scheduled),CBX_Scheduled);
            setText(TXT_CancellationDate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Cancellation_Date));
            Utility.Capture_Screenshot();
        }
        private void RiskMaintain()
        {
            Button riskMaintain=fetchElement("Summit_SubAccount_Insurance_Risk_Items_BTN_RiskItems");
            riskMaintain.Click();
            string warning_popup_title=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_Warning_Title");
            string warning_default_change=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_Warning_Default_Change");
            
            Text TXT_RiskType=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_RiskType");
            Text TXT_SumInsured=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_SumInsured");
            Text TXT_AddExpenses=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_AddExpenses");
            Text TXT_PremiumCalcMethod=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_PremiumCalcMethod");
            Text TXT_AreaCode=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_AreaCode");
            Text TXT_OverrideCommCode=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_OverrideCommCode");
            Text TXT_OverrideCommRate=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_OverrideCommRate");
            CheckBox CBX_ManualCommRate=fetchElement("Summit_SubAccount_Insurance_Risk_Items_CBX_ManualCommRate");
            Text TXT_DeathBenefit=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_DeathBenefit");
            Text TXT_LastEvalDate=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_LastEvalDate");
            ComboBox LST_Endorsement=fetchElement("Summit_SubAccount_Insurance_Risk_Items_LST_Endorsement");
            CheckBox CBX_AddSumInsuToPolicy=fetchElement("Summit_SubAccount_Insurance_Risk_Items_CBX_AddSumInsuToPolicy");
            ComboBox LST_PreviousPolicyIL=fetchElement("Summit_SubAccount_Insurance_Risk_Items_LST_PreviousPolicyIL");
            CheckBox CBX_Excess1=fetchElement("Summit_SubAccount_Insurance_Risk_Items_CBX_Excess1");
            CheckBox CBX_Excess2=fetchElement("Summit_SubAccount_Insurance_Risk_Items_CBX_Excess2");
            CheckBox CBX_Excess3=fetchElement("Summit_SubAccount_Insurance_Risk_Items_CBX_Excess3");
            CheckBox CBX_Excess4=fetchElement("Summit_SubAccount_Insurance_Risk_Items_CBX_Excess4");
            CheckBox CBX_Excess5=fetchElement("Summit_SubAccount_Insurance_Risk_Items_CBX_Excess5");
            Text TXT_RegularPremium=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_RegularPremium");
            Text TXT_RegularCommission=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_RegularCommission");
            Text TXT_RegularAssocCommission=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_RegularAssocCommission");
            Text TXT_FirstPremium=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_FirstPremium");
            Text TXT_FirstCommission=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_FirstCommission");
            Text TXT_FirstAssocCommission=fetchElement("Summit_SubAccount_Insurance_Risk_Items_TXT_FirstAssocCommission");
            
            
            InputText(TXT_RiskType, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Risk_Type));
            InputText(TXT_SumInsured, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Sum_Insured));
            setText(TXT_AddExpenses, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Add_Expenses));
            InputText(TXT_PremiumCalcMethod, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Prem_CalcMethod));
            if(ElementDisplayed(warning_default_change)){
                Keyboard.Press("{ENTER}");
            }
            InputText(TXT_AreaCode, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Area_Code));
            setText(TXT_OverrideCommCode, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Override_Comm_Code));
            setText(TXT_OverrideCommRate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Override_Comm_Rate));
            
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Manual_Comm_Ind),CBX_ManualCommRate);
            
            setText(TXT_DeathBenefit, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Death_Benefit));
            setText(TXT_LastEvalDate, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Last_Val_Date));
            
            SelectValue(LST_Endorsement,dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Endorsement));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Add_Sum_Ins_to_Policy),CBX_AddSumInsuToPolicy);
            SelectValue(LST_PreviousPolicyIL,dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Prev_Policy));
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Excess_1),CBX_Excess1);
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Excess_2),CBX_Excess2);
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Excess_3),CBX_Excess3);
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Excess_4),CBX_Excess4);
            checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Excess_5),CBX_Excess5);
            
            setText(TXT_RegularPremium, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Regular_Premium));
            setText(TXT_RegularCommission, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Regular_Commission));
            setText(TXT_RegularAssocCommission, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_Regular_Assoc_Comm));
            setText(TXT_FirstPremium, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_First_Premium));
            setText(TXT_FirstCommission, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_First_Commission));
            setText(TXT_FirstAssocCommission, dbUtility.GetAccessFieldValue(TestDataConstants.SubAccount_InsuranceCreate_First_Assoc_Comm));
            Utility.Capture_Screenshot();
            Button ok = fetchElement("Summit_SubAccount_Insurance_Risk_Items_BTN_OK");
            ok.Click();
            
            if(ElementDisplayed(warning_popup_title)){
                Keyboard.Press("{ENTER}");
            }

        }
    }
}
