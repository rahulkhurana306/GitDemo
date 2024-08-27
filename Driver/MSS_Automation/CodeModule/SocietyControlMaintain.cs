/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 27/04/2023
 * Time: 15:52
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
    /// Description of SocietyControlMaintain.
    /// </summary>
    public partial class Keywords
    {
        private string finalSCMAuthFMV = string.Empty;
        private string finalSCMUnderwriting = string.Empty;
        private string finalSCMProcurementFees = string.Empty;
        private string finalSCMFCADirect = string.Empty;
        private string finalSCMCompliance = string.Empty;
        private string fcaDirectAppLevelOfServiceAdvised = "AI";
        private string fcaDirectAppLevelOfServiceExecutionOnly = "EO";
        private string fcaDirectAppLevelOfServiceNonAdvised = "EX";
        private string fcaDirectAppLevelOfServiceNone = "";
        private string fcaDirectIllustrationLevelOfServiceAdvised = "AI";
        private string fcaDirectIllustrationLevelOfServiceNonAdvised = "EX";
        private string fcaDirectIllustrationLevelOfServiceNone = "";
        private string finalSCMPaymentElement=string.Empty;
        private string finalSCMGeneral=string.Empty;
        private string finalSCMFees=string.Empty;
        private enum authmveffdatefromflg
        {
            Processing_Date,
            Completion_Date
        }
        
        private enum authmvfirstdateflg
        {
            Processing_Date,
            Completion_Date,
            Payment_Elements_First_Date,
            Preferred_Payment_Day_in_Completion_Month,
            Payment_Date
        }
        
        private enum authmvfirstregdateflg
        {
            Payment_Elements_First_Regular_Date=1,
            Completion_Date,
            Preferred_Payment_Day_in_Completion_Month,
            Payment_Date
        }
        
        private enum authmvFrequencyflg
        {
            Months='M',
            Days= 'D',
            Days_Working='E',
            Weeks='W',
            None='N'
        }
        
        private enum authmvfirstamountflg
        {
            Sum_of_all_Payment_Element_First_Amounts,
            Sum_of_all_Payment_Regular_Amounts
        }
        
        private enum authmvnextduedateflg
        {
            Authorised_Movements_First_Date,
            Payment_Elements_First_Date
        }
        
        private enum fundlinkfirstamountflg
        {
            Payment_Elements_First_Amount,
            Payment_Elements_Regular_Amount
        }
        
        private enum fundlinkregamountflg
        {
            Payment_Elements_Regular_Amount
        }
        //Adding for payment elements tab
        private enum payeeeffdateflg
        {
            Processing_Date,
            Completion_Date
        }
        
        private enum payelastdateflg
        {
            None,
            Processing_Date
        }
        
        private enum payelastmadedateflg
        {
            None,
            Processing_Date
        }
        
        private enum payelastdebiteddateflg
        {
            None,
            Processing_Date
        }
        
        private enum paymentElementFrequencyflg
        {
            Months='M',
            Days= 'D',
            Days_Working='E',
            Weeks='W',
            None='N'
        }
        
        private enum payefirstdatemortflg
        {
            Completion_Date,
            Payment_Date,
            first_day_of_Completion_Month,
            Preferred_Payment_Day_in_Completion_Month
        }
        
        private enum payefirstregdatemortflg
        {
            Completion_Date,
            Payment_Date,
            first_day_of_Completion_Month,
            Preferred_Payment_Day_in_Completion_Month,
            First_Date
        }
        
        private enum payefirstamountmortflg
        {
            Interim_Interest,
            Regular_Payment_Amount,
            Interim_Interest_Regular_Payment_Amount,
            Interim_Int_Conditional_Regular_Payment_Amount
        }
        
        private enum payeregamountmortflg
        {
            Regular_Monthly_Payment
        }
        
        private enum payefirstdateinsflg
        {
            Completion_Date,
            Payment_Date,
            first_day_of_Completion_Month,
            Preferred_Payment_Day_in_Completion_Month
        }
        
        private enum payefirstregdateinsflg
        {
            Completion_Date,
            Payment_Date,
            first_day_of_Completion_Month,
            Preferred_Payment_Day_in_Completion_Month,
            First_Date
        }
        
        private enum payefirstamountinsflg
        {
            First_Premium,
            First_Premium_Regular_Premium
        }
        
        private enum payeregamountinsflg
        {
            Regular_Premium
        }
        
        private enum payefirstdateinmonthofcompflg
        {
            Interim_Interest,
            Regular_Payment_Amount,
            Interim_Interest_Regular_Payment_Amount
        }
        
        private enum payefirstdatefollowingcompflg
        {
            Interim_Interest,
            Regular_Payment_Amount,
            Interim_Interest_Regular_Payment_Amount
        }
        
        private enum fMEffectiveDateAlignFA
        {
            FM_after_PE_effective_date_BACS_days,
            FM_after_Processing_date_BACS_days,
            FM_And_PE_aligned_after_Processing_date_BACS_days,
            FM_And_PE_aligned_after_BACS_Term_Adjusted
        }
        
        private enum paye_1st_date_in_comp_mth_ind
        {
            Interim_Interest,
            Regular_Payment_Amount,
            Interim_Interest_Regular_Payment_Amount
        }
        
        private enum paye_1st_date_follow_comp_ind
        {
            Interim_Interest,
            Regular_Payment_Amount,
            Interim_Interest_Regular_Payment_Amount
        }
        //Adding for General Tab
        private enum affScheme
        {
            Customer_Affinity_Scheme_Link='C',
            Account_Type_Affinity_Scheme_Link='A'
        }
        
        private enum signOffMandate
        {
            Application_Level='A',
            Exposure_Level='E'
        }
        
        private enum transSource
        {
            Originating_Branch='O',
            Posting_Branch='P'
        }
        private enum whatIfCal
        {
            Enter_interest_rate_only='1',
            Enter_product_code_only='2',
            Enter_product_code_or_rate='3',
        }
        
        
        
        private enum procfeepaydateflg
        {
            Completion_Date='C',
            Fund_Release='F'
        }
        
        private enum procfeesocietybankflg
        {
            Touchstone_Funds_Release=238,
            Touchstone_Collections,
            Touchstone_Payments
        }
        
        private enum procfeepaydateFrequencyflg
        {
            Months='M',
            Days= 'D',
            Days_Working='E',
            Weeks='W',
            None='N'
        }
        
        private enum fcaDirectAppInterview
        {
            FaceToFace='F',
            Telephone= 'T',
            Internet='E',
            Post='P',
            None='N'
        }
        
        private enum fcaDirectIllustrationInterview
        {
            FaceToFace='F',
            Telephone= 'T',
            None='N'
        }
        
        private enum complianceWhoseMortgages
        {
            None='N',
            WholeMarket='W',
            LimitedNumberOfLenders='L',
            SingleLender='S'
        }
        
        private enum complianceOwnSingleLenderMortgage
        {
            Own='O',
            SingleLender='S'
        }
        //Adding for Fees Tab
        private enum prodFeeGeneration
        {
            Primary_Product_Only='Y',
            Unique_fees_for_unique_products='N',
            All_fees_for_Unique_Products='A',
        }
        public List<string> SocietyControlMaintain()
        {
            {
                try{
                    Main.appFlag = Constants.appActivate;
                    string scmReference = Main.InputData[0];
                    string sqlQuery = "select  *  from [ACT_Society_Control_Maintenance] where [Reference] = '"+scmReference+"'";
                    dbUtility.ReadDBResultMS(sqlQuery);
                    string authFundMovementsRef = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_Society_Control_Maintenance_AuthFundMovements);
                    string underwritingRef = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_Society_Control_Maintenance_Underwriting);
                    string procurementFeesRef = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_Society_Control_Maintenance_ProcurementFees);
                    string fcaDirectRef = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_Society_Control_Maintenance_FCADirect);
                    string complianceRef = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_Society_Control_Maintenance_Compliance);
                    string generalRef=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_Society_Control_Maintenance_General);
                    string paymentElementRef=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_Society_Control_Maintenance_PaymentElements);
                    string feesRef=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_Society_Control_Maintenance_Fees);

                    AuthFundMovement(authFundMovementsRef);
                    Underwriting(underwritingRef);
                    ProcurementFees(procurementFeesRef);
                    FCADirect(fcaDirectRef);
                    Compliance(complianceRef);
                    General(generalRef);
                    PaymentElement(paymentElementRef);
                    Fees(feesRef);
                    
                    Main.OutputData.Add(Constants.TS_STATUS_PASS);
                    return Main.OutputData;
                }catch(Exception e){
                    Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                    Main.OutputData.Add(e.Message);
                    return Main.OutputData;
                }
            }
        }
        
        private void AuthFundMovement(string authFunfMvmtDB){
            if(!string.IsNullOrEmpty(authFunfMvmtDB)){
                List<string> authFMVList = new List<string>();
                string sqlQuery = "select  *  from [ACT_SCM_Auth_Fund_Movements] where [Reference] = '"+authFunfMvmtDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_Society);
                string effectiveDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_EffectiveDate);
                string firstPaymentDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_FirstPaymentDate);
                string firstPaymentDateNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_FirstPaymentDateNumber);
                string firstPaymentDatePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_FirstPaymentDatePeriod);
                string firstRegularDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_FirstRegularDate);
                string firstRegularDateNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_FirstRegularDateNumber);
                string firstRegularDatePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_FirstRegularDatePeriod);
                string firstPaymentAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_FirstPaymentAmount);
                string nextPaymentDueDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_NextPaymentDueDate);
                string firstAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_FirstAmount);
                string regularAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Auth_Fund_Movements_RegularAmount);
                
                if(!string.IsNullOrEmpty(societyDB)){
                    if(!societyDB.ToLower().Equals("default")){
                        string societyVal = "SOCSEQNO="+"'"+societyDB+"'";
                        authFMVList.Add(societyVal);
                    }
                }
                if(societyDB.ToLower().Equals("default")){
                    societyDB="1";
                }
                
                if(!string.IsNullOrEmpty(effectiveDateDB)){
                    if(!effectiveDateDB.ToLower().Equals("default")){
                        if(effectiveDateDB.Equals("Processing Date",StringComparison.OrdinalIgnoreCase)){
                            effectiveDateDB = ((int)authmveffdatefromflg.Processing_Date).ToString();
                        }else if(effectiveDateDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            effectiveDateDB = ((int)authmveffdatefromflg.Completion_Date).ToString();
                        }
                        string effectiveDateVal = "AUTHMVEFFDATEFROMFLG="+"'"+effectiveDateDB+"'";
                        authFMVList.Add(effectiveDateVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstPaymentDateDB)){
                    if(!firstPaymentDateDB.ToLower().Equals("default")){
                        if(firstPaymentDateDB.Equals("Processing Date",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDateDB = ((int)authmvfirstdateflg.Processing_Date).ToString();
                        }else if(firstPaymentDateDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDateDB = ((int)authmvfirstdateflg.Completion_Date).ToString();
                        }else if(firstPaymentDateDB.Equals("Payment Elements First Date",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDateDB = ((int)authmvfirstdateflg.Payment_Elements_First_Date).ToString();
                        }else if(firstPaymentDateDB.Equals("Preferred Payment Day in Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDateDB = ((int)authmvfirstdateflg.Preferred_Payment_Day_in_Completion_Month).ToString();
                        }else if(firstPaymentDateDB.Equals("Payment Date",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDateDB = ((int)authmvfirstdateflg.Payment_Date).ToString();
                        }
                        string firstPaymentDateDBVal = "AUTHMVFIRSTDATEFLG="+"'"+firstPaymentDateDB+"'";
                        authFMVList.Add(firstPaymentDateDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(firstPaymentDateNumberDB)){
                    if(!firstPaymentDateNumberDB.ToLower().Equals("default")){
                        string firstPaymentDateNumberDBVal = "AUTHMVFIRSTDATEFREQAMTFLG="+"'"+firstPaymentDateNumberDB+"'";
                        authFMVList.Add(firstPaymentDateNumberDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstPaymentDatePeriodDB)){
                    if(!firstPaymentDatePeriodDB.ToLower().Equals("default")){
                        if(firstPaymentDatePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDatePeriodDB = ((char)authmvFrequencyflg.Days).ToString();
                        }else if(firstPaymentDatePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDatePeriodDB = ((char)authmvFrequencyflg.Months).ToString();
                        }else if(firstPaymentDatePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDatePeriodDB = ((char)authmvFrequencyflg.Days_Working).ToString();
                        }else if(firstPaymentDatePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDatePeriodDB = ((char)authmvFrequencyflg.Weeks).ToString();
                        }else if(firstPaymentDatePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentDatePeriodDB = ((char)authmvFrequencyflg.None).ToString();
                        }
                        string firstPaymentDatePeriodDBVal = "AUTHMVFIRSTDATEFREQFLG="+"'"+firstPaymentDatePeriodDB+"'";
                        authFMVList.Add(firstPaymentDatePeriodDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstRegularDateDB)){
                    if(!firstRegularDateDB.ToLower().Equals("default")){
                        if(firstRegularDateDB.Equals("Payment Elements First Regular Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDateDB = ((int)authmvfirstregdateflg.Payment_Elements_First_Regular_Date).ToString();
                        }else if(firstRegularDateDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDateDB = ((int)authmvfirstregdateflg.Completion_Date).ToString();
                        }else if(firstRegularDateDB.Equals("Preferred Payment Day in Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDateDB = ((int)authmvfirstregdateflg.Preferred_Payment_Day_in_Completion_Month).ToString();
                        }else if(firstRegularDateDB.Equals("Payment Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDateDB = ((int)authmvfirstregdateflg.Payment_Date).ToString();
                        }
                        string firstRegularDateDBVal = "AUTHMVFIRSTREGDATEFLG="+"'"+firstRegularDateDB+"'";
                        authFMVList.Add(firstRegularDateDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(firstRegularDateNumberDB)){
                    if(!firstRegularDateNumberDB.ToLower().Equals("default")){
                        string firstRegularDateNumberDBVal = "AUTHMVFIRSTREGDATEFREQAMTFLG="+"'"+firstRegularDateNumberDB+"'";
                        authFMVList.Add(firstRegularDateNumberDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstRegularDatePeriodDB)){
                    if(!firstRegularDatePeriodDB.ToLower().Equals("default")){
                        if(firstRegularDatePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDatePeriodDB = ((char)authmvFrequencyflg.Days).ToString();
                        }else if(firstRegularDatePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDatePeriodDB = ((char)authmvFrequencyflg.Months).ToString();
                        }else if(firstRegularDatePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDatePeriodDB = ((char)authmvFrequencyflg.Days_Working).ToString();
                        }else if(firstRegularDatePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDatePeriodDB = ((char)authmvFrequencyflg.Weeks).ToString();
                        }else if(firstRegularDatePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            firstRegularDatePeriodDB = ((char)authmvFrequencyflg.None).ToString();
                        }
                        string firstRegularDatePeriodDBVal = "AUTHMVFIRSTREGDATEFREQFLG="+"'"+firstRegularDatePeriodDB+"'";
                        authFMVList.Add(firstRegularDatePeriodDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstPaymentAmountDB)){
                    if(!firstPaymentAmountDB.ToLower().Equals("default")){
                        if(firstPaymentAmountDB.Equals("Sum of all Payment Element First Amounts",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentAmountDB = ((int)authmvfirstamountflg.Sum_of_all_Payment_Element_First_Amounts).ToString();
                        }else if(firstPaymentAmountDB.Equals("Sum of all Payment Regular Amounts",StringComparison.OrdinalIgnoreCase)){
                            firstPaymentAmountDB = ((int)authmvfirstamountflg.Sum_of_all_Payment_Regular_Amounts).ToString();
                        }
                        string firstPaymentAmountDBVal = "AUTHMVFIRSTAMOUNTFLG="+"'"+firstPaymentAmountDB+"'";
                        authFMVList.Add(firstPaymentAmountDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(nextPaymentDueDateDB)){
                    if(!nextPaymentDueDateDB.ToLower().Equals("default")){
                        if(nextPaymentDueDateDB.Equals("Authorised Movements First Date",StringComparison.OrdinalIgnoreCase)){
                            nextPaymentDueDateDB = ((int)authmvnextduedateflg.Authorised_Movements_First_Date).ToString();
                        }else if(nextPaymentDueDateDB.Equals("Payment Elements First Date",StringComparison.OrdinalIgnoreCase)){
                            nextPaymentDueDateDB = ((int)authmvnextduedateflg.Payment_Elements_First_Date).ToString();
                        }
                        string nextPaymentDueDateDBVal = "AUTHMVNEXTDUEDATEFLG="+"'"+nextPaymentDueDateDB+"'";
                        authFMVList.Add(nextPaymentDueDateDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstAmountDB)){
                    if(!firstAmountDB.ToLower().Equals("default")){
                        if(firstAmountDB.Equals("Payment Elements First Amount",StringComparison.OrdinalIgnoreCase)){
                            firstAmountDB = ((int)fundlinkfirstamountflg.Payment_Elements_First_Amount).ToString();
                        }else if(firstAmountDB.Equals("Payment Elements Regular Amount",StringComparison.OrdinalIgnoreCase)){
                            firstAmountDB = ((int)fundlinkfirstamountflg.Payment_Elements_Regular_Amount).ToString();
                        }
                        string firstAmountDBVal = "FUNDLINKFIRSTAMOUNTFLG="+"'"+firstAmountDB+"'";
                        authFMVList.Add(firstAmountDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(regularAmountDB)){
                    if(!regularAmountDB.ToLower().Equals("default")){
                        if(regularAmountDB.Equals("Payment Elements Regular Amount",StringComparison.OrdinalIgnoreCase)){
                            regularAmountDB = ((int)fundlinkregamountflg.Payment_Elements_Regular_Amount).ToString();
                        }
                        string regularAmountDBVal = "FUNDLINKREGAMOUNTFLG="+"'"+regularAmountDB+"'";
                        authFMVList.Add(regularAmountDBVal);
                    }
                }
                
                finalSCMAuthFMV = string.Join(",", authFMVList.ToArray());
                Report.Info("query string inputs--"+finalSCMAuthFMV);
                string updateQueryAFM = "UPDATE SAM05 SET "+finalSCMAuthFMV+" where SOCSEQNO='"+societyDB+"'";
                oraUtility.executeQuery(updateQueryAFM);
            }
        }
        
        private void Underwriting(string underwritingDB){
            if(!string.IsNullOrEmpty(underwritingDB)){
                List<string> underwritingList = new List<string>();
                string sqlQuery = "select  *  from [ACT_SCM_Underwriting] where [Reference] = '"+underwritingDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Underwriting_Society);
                string maximumLtvDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Underwriting_MaximumLTV);
                string maximumValueDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Underwriting_MaximumValue);
                string maximumPerOfLoanDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Underwriting_MaximumPerOfLoan);
                string customerSegmentsNotSetDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Underwriting_CustomerSegmentsNotSet);
                string portfolioLandlordDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Underwriting_PortfolioLandlord);
                string noOfPropsInPortfolioDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Underwriting_NoOfPropsInPortfolio);
                
                if(!string.IsNullOrEmpty(societyDB)){
                    if(!societyDB.ToLower().Equals("default")){
                        string societyVal = "SOCSEQNO="+"'"+societyDB+"'";
                        underwritingList.Add(societyVal);
                    }
                }
                if(societyDB.ToLower().Equals("default")){
                    societyDB="1";
                }
                
                if(!string.IsNullOrEmpty(maximumLtvDB)){
                    if(!maximumLtvDB.ToLower().Equals("default")){
                        string maximumLtvVal = "CAPRAISEMAXLTV="+"'"+maximumLtvDB+"'";
                        underwritingList.Add(maximumLtvVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(maximumValueDB)){
                    if(!maximumValueDB.ToLower().Equals("default")){
                        string maximumValueVal = "CAPRAISEMAXLOANAMT="+"'"+maximumValueDB+"'";
                        underwritingList.Add(maximumValueVal);
                    }
                }
                if(!string.IsNullOrEmpty(maximumPerOfLoanDB)){
                    if(!maximumPerOfLoanDB.ToLower().Equals("default")){
                        string maximumPerOfLoanVal = "CAPRAISEMAXLOANPER="+"'"+maximumPerOfLoanDB+"'";
                        underwritingList.Add(maximumPerOfLoanVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(customerSegmentsNotSetDB)){
                    if(!customerSegmentsNotSetDB.ToLower().Equals("default")){
                        string customerSegmentsNotSetDBVal = "CUSTOMER_SEGMENTS_CHECK="+"'"+customerSegmentsNotSetDB+"'";
                        underwritingList.Add(customerSegmentsNotSetDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(portfolioLandlordDB)){
                    if(!portfolioLandlordDB.ToLower().Equals("default")){
                        string portfolioLandlordDBVal = "CALC_PORTFOLIO_LANDLORD="+"'"+portfolioLandlordDB+"'";
                        underwritingList.Add(portfolioLandlordDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(noOfPropsInPortfolioDB)){
                    if(!noOfPropsInPortfolioDB.ToLower().Equals("default")){
                        string noOfPropsInPortfolioDBVal = "NO_OF_PROPS_IN_PORTFOLIO="+"'"+noOfPropsInPortfolioDB+"'";
                        underwritingList.Add(noOfPropsInPortfolioDBVal);
                    }
                }
                
                finalSCMUnderwriting = string.Join(",", underwritingList.ToArray());
                Report.Info("query string inputs--"+finalSCMUnderwriting);
                string updateQueryUW = "UPDATE SAM05 SET "+finalSCMUnderwriting+" where SOCSEQNO='"+societyDB+"'";
                oraUtility.executeQuery(updateQueryUW);
            }
        }
        
        private void ProcurementFees(string procurementFeeDB){
            if(!string.IsNullOrEmpty(procurementFeeDB)){
                List<string> procurementFeeList = new List<string>();
                string sqlQuery = "select  *  from [ACT_SCM_Procurement_Fees] where [Reference] = '"+procurementFeeDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_Society);
                string procurementFeePayDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_ProcurementFeePayDate);
                string procurementFeePayDateNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_ProcurementFeePayDateNumber);
                string procurementFeePayDatePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_ProcurementFeePayDatePeriod);
                string minimumLoanAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_MinimumLoanAmount);
                string maxProcFeeChequeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_MaxProcFeeCheque);
                string excludeCompanyFeeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_ExcludeCompanyFee);
                string manualEntryOfIndividualBrokerDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_ManualEntryOfIndividualBroker);
                string releaseReasonDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_ReleaseReason);
                string societyBankDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_SocietyBank);
                string sortCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_SortCode);
                string accountNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Procurement_Fees_AccountNumber);
                
                if(!string.IsNullOrEmpty(societyDB)){
                    if(!societyDB.ToLower().Equals("default")){
                        string societyVal = "SOCSEQNO="+"'"+societyDB+"'";
                        procurementFeeList.Add(societyVal);
                    }
                }
                if(societyDB.ToLower().Equals("default")){
                    societyDB="1";
                }
                
                if(!string.IsNullOrEmpty(procurementFeePayDateDB)){
                    if(!procurementFeePayDateDB.ToLower().Equals("default")){
                        if(procurementFeePayDateDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            procurementFeePayDateDB = ((char)procfeepaydateflg.Completion_Date).ToString();
                        }else if(procurementFeePayDateDB.Equals("Fund Release",StringComparison.OrdinalIgnoreCase)){
                            procurementFeePayDateDB = ((char)procfeepaydateflg.Fund_Release).ToString();
                        }
                        string procurementFeePayDateDBVal = "PROCCOMPDATEFREQ="+"'"+procurementFeePayDateDB+"'";
                        procurementFeeList.Add(procurementFeePayDateDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(procurementFeePayDateNumberDB)){
                    if(!procurementFeePayDateNumberDB.ToLower().Equals("default")){
                        string procurementFeePayDateNumberDBVal = "PROCCOMPDATEFREQAMTFLG="+"'"+procurementFeePayDateNumberDB+"'";
                        procurementFeeList.Add(procurementFeePayDateNumberDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(procurementFeePayDatePeriodDB)){
                    if(!procurementFeePayDatePeriodDB.ToLower().Equals("default")){
                        if(procurementFeePayDatePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            procurementFeePayDatePeriodDB = ((char)procfeepaydateFrequencyflg.Days).ToString();
                        }else if(procurementFeePayDatePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            procurementFeePayDatePeriodDB = ((char)procfeepaydateFrequencyflg.Months).ToString();
                        }else if(procurementFeePayDatePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            procurementFeePayDatePeriodDB = ((char)procfeepaydateFrequencyflg.Days_Working).ToString();
                        }else if(procurementFeePayDatePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            procurementFeePayDatePeriodDB = ((char)procfeepaydateFrequencyflg.Weeks).ToString();
                        }else if(procurementFeePayDatePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            procurementFeePayDatePeriodDB = ((char)procfeepaydateFrequencyflg.None).ToString();
                        }
                        string procurementFeePayDatePeriodDBVal = "PROCCOMPDATEFREQFLG="+"'"+procurementFeePayDatePeriodDB+"'";
                        procurementFeeList.Add(procurementFeePayDatePeriodDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(minimumLoanAmountDB)){
                    if(!minimumLoanAmountDB.ToLower().Equals("default")){
                        string minimumLoanAmountDBVal = "PROCMINLOANAMT="+"'"+minimumLoanAmountDB+"'";
                        procurementFeeList.Add(minimumLoanAmountDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(maxProcFeeChequeDB)){
                    if(!maxProcFeeChequeDB.ToLower().Equals("default")){
                        string maxProcFeeChequeDBVal = "PROCFEEMAXCHQ="+"'"+maxProcFeeChequeDB+"'";
                        procurementFeeList.Add(maxProcFeeChequeDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(excludeCompanyFeeDB)){
                    if(!excludeCompanyFeeDB.ToLower().Equals("default")){
                        string excludeCompanyFeeDBVal = "PROCEXCLCOMPFEE="+"'"+excludeCompanyFeeDB+"'";
                        procurementFeeList.Add(excludeCompanyFeeDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(manualEntryOfIndividualBrokerDB)){
                    if(!manualEntryOfIndividualBrokerDB.ToLower().Equals("default")){
                        string manualEntryOfIndividualBrokerDBVal = "PROCMANUALBROKER="+"'"+manualEntryOfIndividualBrokerDB+"'";
                        procurementFeeList.Add(manualEntryOfIndividualBrokerDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(releaseReasonDB)){
                    if(!releaseReasonDB.ToLower().Equals("default")){
                        string releaseReasonDBVal = "PROCFEE_RELEASE_REASON="+"'"+releaseReasonDB+"'";
                        procurementFeeList.Add(releaseReasonDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(societyBankDB)){
                    if(!societyBankDB.ToLower().Equals("default")){
                        string accountNumberVal=null;
                        if(societyBankDB.Equals("TStone Funds Rel",StringComparison.OrdinalIgnoreCase)){
                            societyBankDB = ((int)procfeesocietybankflg.Touchstone_Funds_Release).ToString();
                            accountNumberVal = "PROCFEE_BANK_ACCOUNT_NUMBER=50021365";
                        }else if(societyBankDB.Equals("TStone Collections",StringComparison.OrdinalIgnoreCase)){
                            societyBankDB = ((int)procfeesocietybankflg.Touchstone_Collections).ToString();
                            accountNumberVal = "PROCFEE_BANK_ACCOUNT_NUMBER=50034872";
                        }else if(societyBankDB.Equals("TStone Payments",StringComparison.OrdinalIgnoreCase)){
                            societyBankDB = ((int)procfeesocietybankflg.Touchstone_Payments).ToString();
                            accountNumberVal = "PROCFEE_BANK_ACCOUNT_NUMBER=50047869";
                        }
                        string societyBankDBVal = "PROCFEE_SOCIETY_BANK_SEQNO="+"'"+societyBankDB+"'";
                        procurementFeeList.Add(societyBankDBVal);
                        procurementFeeList.Add(accountNumberVal);
                    }
                }
                
                finalSCMProcurementFees = string.Join(",", procurementFeeList.ToArray());
                Report.Info("query string inputs--"+finalSCMProcurementFees);
                string updateQueryProcFees = "UPDATE SAM05 SET "+finalSCMProcurementFees+" where SOCSEQNO='"+societyDB+"'";
                oraUtility.executeQuery(updateQueryProcFees);
            }
        }
        
        private void FCADirect(string fcaDirectDB){
            if(!string.IsNullOrEmpty(fcaDirectDB)){
                List<string> fcaDirectList = new List<string>();
                string sqlQuery = "select  *  from [ACT_SCM_FCA_Direct] where [Reference] = '"+fcaDirectDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_FCADirect_Society);
                string appLevelOfServiceDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_FCADirect_Application_LevelOfService);
                string appInterviewDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_FCADirect_Application_Interview);
                string illustrationLevelOfServiceDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_FCADirect_Illustration_LevelOfService);
                string illustrationInterviewDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_FCADirect_Illustration_Interview);
                
                if(!string.IsNullOrEmpty(societyDB)){
                    if(!societyDB.ToLower().Equals("default")){
                        string societyVal = "SOCSEQNO="+"'"+societyDB+"'";
                        fcaDirectList.Add(societyVal);
                    }
                }
                if(societyDB.ToLower().Equals("default")){
                    societyDB="1";
                }

                if(!string.IsNullOrEmpty(appLevelOfServiceDB)){
                    if(!appLevelOfServiceDB.ToLower().Equals("default")){
                        if(appLevelOfServiceDB.Equals("Advised",StringComparison.OrdinalIgnoreCase)){
                            appLevelOfServiceDB = fcaDirectAppLevelOfServiceAdvised;
                        }else if(appLevelOfServiceDB.Equals("Execution Only",StringComparison.OrdinalIgnoreCase)){
                            appLevelOfServiceDB = fcaDirectAppLevelOfServiceExecutionOnly;
                        }else if(appLevelOfServiceDB.Equals("Non-Advised (Buy to Let/Non-Regulated)",StringComparison.OrdinalIgnoreCase)){
                            appLevelOfServiceDB = fcaDirectAppLevelOfServiceNonAdvised;
                        }else if(appLevelOfServiceDB.Equals("None",StringComparison.OrdinalIgnoreCase)){
                            appLevelOfServiceDB = fcaDirectAppLevelOfServiceNone;
                        }
                        string appLevelOfServiceDBVal = "CML_DEF_SERVLEVEL="+"'"+appLevelOfServiceDB+"'";
                        fcaDirectList.Add(appLevelOfServiceDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(appInterviewDB)){
                    if(!appInterviewDB.ToLower().Equals("default")){
                        if(appInterviewDB.Equals("Face to Face",StringComparison.OrdinalIgnoreCase)){
                            appInterviewDB = ((char)fcaDirectAppInterview.FaceToFace).ToString();
                        }else if(appInterviewDB.Equals("Telephone",StringComparison.OrdinalIgnoreCase)){
                            appInterviewDB = ((char)fcaDirectAppInterview.Telephone).ToString();
                        }else if(appInterviewDB.Equals("Internet/Email (i.e. Electronic)",StringComparison.OrdinalIgnoreCase)){
                            appInterviewDB = ((char)fcaDirectAppInterview.Internet).ToString();
                        }else if(appInterviewDB.Equals("Post",StringComparison.OrdinalIgnoreCase)){
                            appInterviewDB = ((char)fcaDirectAppInterview.Post).ToString();
                        }else if(appInterviewDB.Equals("None",StringComparison.OrdinalIgnoreCase)){
                            appInterviewDB = ((char)fcaDirectAppInterview.None).ToString();
                        }
                        string appInterviewDBVal = "CML_DEF_INTERVIEW="+"'"+appInterviewDB+"'";
                        fcaDirectList.Add(appInterviewDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(illustrationLevelOfServiceDB)){
                    if(!illustrationLevelOfServiceDB.ToLower().Equals("default")){
                        if(illustrationLevelOfServiceDB.Equals("Advised",StringComparison.OrdinalIgnoreCase)){
                            illustrationLevelOfServiceDB = fcaDirectIllustrationLevelOfServiceAdvised;
                        }else if(illustrationLevelOfServiceDB.Equals("Non-Advised",StringComparison.OrdinalIgnoreCase)){
                            illustrationLevelOfServiceDB = fcaDirectIllustrationLevelOfServiceNonAdvised;
                        }else if(illustrationLevelOfServiceDB.Equals("None",StringComparison.OrdinalIgnoreCase)){
                            illustrationLevelOfServiceDB = fcaDirectIllustrationLevelOfServiceNone;
                        }
                        string illustrationLevelOfServiceDBBVal = "QTNCML_DEF_SERVLEVEL="+"'"+illustrationLevelOfServiceDB+"'";
                        fcaDirectList.Add(illustrationLevelOfServiceDBBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(illustrationInterviewDB)){
                    if(!illustrationInterviewDB.ToLower().Equals("default")){
                        if(illustrationInterviewDB.Equals("Face to Face",StringComparison.OrdinalIgnoreCase)){
                            illustrationInterviewDB = ((char)fcaDirectIllustrationInterview.FaceToFace).ToString();
                        }else if(illustrationInterviewDB.Equals("Telephone",StringComparison.OrdinalIgnoreCase)){
                            illustrationInterviewDB = ((char)fcaDirectIllustrationInterview.Telephone).ToString();
                        }else if(illustrationInterviewDB.Equals("None",StringComparison.OrdinalIgnoreCase)){
                            illustrationInterviewDB = ((char)fcaDirectIllustrationInterview.None).ToString();
                        }
                        string illustrationInterviewDBBVal = "QTNCML_DEF_INTERVIEW="+"'"+illustrationInterviewDB+"'";
                        fcaDirectList.Add(illustrationInterviewDBBVal);
                    }
                }
                
                finalSCMFCADirect = string.Join(",", fcaDirectList.ToArray());
                Report.Info("query string inputs--"+finalSCMFCADirect);
                string updateQueryFCADirect = "UPDATE SAM05 SET "+finalSCMFCADirect+" where SOCSEQNO='"+societyDB+"'";
                oraUtility.executeQuery(updateQueryFCADirect);
            }
        }
        
        private void Compliance(string complianceDB){
            if(!string.IsNullOrEmpty(complianceDB)){
                List<string> complianceList = new List<string>();
                string sqlQuery = "select  *  from [ACT_SCM_Compliance] where [Reference] = '"+complianceDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_Society);
                string mortalityRateThresholdDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_MortalityRateThreshold);
                string mortalityMinTermDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_MortalityMinTerm);
                string whoseMortgagesDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_WhoseMortgages);
                string contractualObligationDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_ContractualObligation);
                string ownOrSingleLenderDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_OwnOrSingleLender);
                string singleLenderNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_SingleLenderName);
                string limitedRangeOfMortgagesDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_LimitedRangeOfMortgages);
                string maintainAuthoritySecurityLevelDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_MaintainAuthoritySecurityLevel);
                string allowMixedFCADB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_AllowMixedFCA);
                string appInitialDisclosurePackageDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_AppInitialDisclosurePackage);
                string regulatedByDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_RegulatedBy);
                string compliantProcessDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_CompliantProcess);
                string fscsTextDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Compliance_FSCSText);
                
                if(!string.IsNullOrEmpty(societyDB)){
                    if(!societyDB.ToLower().Equals("default")){
                        string societyVal = "SOCSEQNO="+"'"+societyDB+"'";
                        complianceList.Add(societyVal);
                    }
                }
                if(societyDB.ToLower().Equals("default")){
                    societyDB="1";
                }
                
                if(!string.IsNullOrEmpty(mortalityRateThresholdDB)){
                    if(!mortalityRateThresholdDB.ToLower().Equals("default")){
                        string mortalityRateThresholdDBVal = "MORTALITY_THRESHOLD="+"'"+mortalityRateThresholdDB+"'";
                        complianceList.Add(mortalityRateThresholdDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(mortalityMinTermDB)){
                    if(!mortalityMinTermDB.ToLower().Equals("default")){
                        string mortalityMinTermDBVal = "MORTALITY_MIN_TERM="+"'"+mortalityMinTermDB+"'";
                        complianceList.Add(mortalityMinTermDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(whoseMortgagesDB)){
                    if(!whoseMortgagesDB.ToLower().Equals("default")){
                        if(whoseMortgagesDB.Equals("None",StringComparison.OrdinalIgnoreCase)){
                            whoseMortgagesDB = ((char)complianceWhoseMortgages.None).ToString();
                        }else if(whoseMortgagesDB.Equals("Whole Market",StringComparison.OrdinalIgnoreCase)){
                            whoseMortgagesDB = ((char)complianceWhoseMortgages.WholeMarket).ToString();
                        }else if(whoseMortgagesDB.Equals("Limited Number of Lenders",StringComparison.OrdinalIgnoreCase)){
                            whoseMortgagesDB = ((char)complianceWhoseMortgages.LimitedNumberOfLenders).ToString();
                        }else if(whoseMortgagesDB.Equals("Single Lender",StringComparison.OrdinalIgnoreCase)){
                            whoseMortgagesDB = ((char)complianceWhoseMortgages.SingleLender).ToString();
                        }
                        string whoseMortgagesDBVal = "INIT_DISCL_WHOSE_MTG_DEFAULT="+"'"+whoseMortgagesDB+"'";
                        complianceList.Add(whoseMortgagesDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(contractualObligationDB)){
                    if(!contractualObligationDB.ToLower().Equals("default")){
                        string contractualObligationDBVal = "INIT_DISCL_CONTRACT_OBLIG="+"'"+contractualObligationDB+"'";
                        complianceList.Add(contractualObligationDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(ownOrSingleLenderDB)){
                    if(!ownOrSingleLenderDB.ToLower().Equals("default")){
                        if(ownOrSingleLenderDB.Equals("Own",StringComparison.OrdinalIgnoreCase)){
                            ownOrSingleLenderDB = ((char)complianceOwnSingleLenderMortgage.Own).ToString();
                        }else if(ownOrSingleLenderDB.Equals("Single Lender",StringComparison.OrdinalIgnoreCase)){
                            ownOrSingleLenderDB = ((char)complianceOwnSingleLenderMortgage.SingleLender).ToString();
                        }
                        string ownOrSingleLenderDBVal = "INIT_DISCL_SINGLE_OWN_MTGS="+"'"+ownOrSingleLenderDB+"'";
                        complianceList.Add(ownOrSingleLenderDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(singleLenderNameDB)){
                    if(!singleLenderNameDB.ToLower().Equals("default")){
                        string singleLenderNameDBVal = "INIT_DISCL_SNGLE_LNDR_NAME="+"'"+singleLenderNameDB+"'";
                        complianceList.Add(singleLenderNameDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(limitedRangeOfMortgagesDB)){
                    if(!limitedRangeOfMortgagesDB.ToLower().Equals("default")){
                        string limitedRangeOfMortgagesDBVal = "INIT_DISCL_LTD_RANGE="+"'"+limitedRangeOfMortgagesDB+"'";
                        complianceList.Add(limitedRangeOfMortgagesDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(maintainAuthoritySecurityLevelDB)){
                    if(!maintainAuthoritySecurityLevelDB.ToLower().Equals("default")){
                        string maintainAuthoritySecurityLevelDBVal = "AUTH_SEC_CAT="+"'"+maintainAuthoritySecurityLevelDB+"'";
                        complianceList.Add(maintainAuthoritySecurityLevelDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(allowMixedFCADB)){
                    if(!allowMixedFCADB.ToLower().Equals("default")){
                        string allowMixedFCADBVal = "MIXED_REGULATION="+"'"+allowMixedFCADB+"'";
                        complianceList.Add(allowMixedFCADBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(appInitialDisclosurePackageDB)){
                    if(!appInitialDisclosurePackageDB.ToLower().Equals("default")){
                        string appInitialDisclosurePackageDBVal = "APP_INIT_DISCL_PKG="+"'"+appInitialDisclosurePackageDB+"'";
                        complianceList.Add(appInitialDisclosurePackageDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(regulatedByDB)){
                    if(!regulatedByDB.ToLower().Equals("default")){
                        string regulatedByDBVal = "INIT_DISCL_REGLD_BY_TXT="+"'"+regulatedByDB+"'";
                        complianceList.Add(regulatedByDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(compliantProcessDB)){
                    if(!compliantProcessDB.ToLower().Equals("default")){
                        string compliantProcessDBVal = "INIT_DISCL_COMPLAINT_TXT="+"'"+compliantProcessDB+"'";
                        complianceList.Add(compliantProcessDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(fscsTextDB)){
                    if(!fscsTextDB.ToLower().Equals("default")){
                        string fscsTextDBVal = "INIT_DISCL_FSA_COMP_SCHEME_TXT="+"'"+fscsTextDB+"'";
                        complianceList.Add(fscsTextDBVal);
                    }
                }
                
                finalSCMCompliance = string.Join(",", complianceList.ToArray());
                Report.Info("query string inputs--"+finalSCMCompliance);
                string updateQueryCompliance = "UPDATE SAM05 SET "+finalSCMCompliance+" where SOCSEQNO='"+societyDB+"'";
                oraUtility.executeQuery(updateQueryCompliance);

            }
        }

        private void PaymentElement(string paymentElementDB){
            if(!string.IsNullOrEmpty(paymentElementDB)){
                List<string> payElementList = new List<string>();
                string sqlQuery = "select  *  from [ACT_SCM_Payment_Elements] where [Reference] = '"+paymentElementDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_Society);
                string effectiveDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_EffectiveDate);
                string effectiveDateNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_EffectiveDateNumber);
                string effectiveDatePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_EffectiveDatePeriod);
                string lastDueDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastDueDate);
                string lastDueDateNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastDueDateNumber);
                string lastDueDatePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastDueDatePeriod);
                string lastPayDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastPayDate);
                string lastPayDateNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastPayDateNumber);
                string lastPayDatePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastPayDatePeriod);
                string lastDebitMadeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastDebitMade);
                string lastDebitMadeNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastDebitMadeNumber);
                string lastDebitMadePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_LastDebitMadePeriod);
                string overPaymentDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_OverPayment);
                string firstDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDate);
                string firstDateNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDateNumber);
                string firstDatePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDatePeriod);
                string firstRegDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstRegDate);
                string firstRegDateNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstRegDateNumber);
                string firstRegDatePeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstRegDatePeriod);
                string firstAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstAmount);
                string regAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_RegAmount);
                string usePEsettingsDDDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_UsePEsettingsDD);
                string firstDateInsDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDateIns);
                string firstDateInsNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDateInsNumber);
                string firstDateInsPeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDateInsPeriod);
                string firstRegDateInsDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstRegDateIns);
                string firstRegDateInsNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstRegDateInsNumber);
                string firstRegDateInsPeriodDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstRegDateInsPeriod);
                string firstAmountInsDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstAmountIns);
                string regAmountInsDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_RegAmountIns);
                string useNextDebitDuePAFADB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_UseNextDebitDuePAFA);
                string firstDateMonthCompletionFADB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDateMonthCompletionFA);
                string firstDateMonthFollowCompletionFADB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDateMonthFollowCompletionFA);
                string fMEffectiveDateAlignFADB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FMEffectiveDateAlignFA);
                string useNextDebitDueOnPAMVDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_UseNextDebitDueOnPAMV);
                string firstDateMonthCompletionMVDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDateMonthCompletionMV);
                string firstDateMonthFollowCompletionMVDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Payment_Elements_FirstDateMonthFollowCompletionMV);

                if(!string.IsNullOrEmpty(societyDB)){
                    if(!societyDB.ToLower().Equals("default")){
                        string societyVal = "SOCSEQNO="+"'"+societyDB+"'";
                        payElementList.Add(societyVal);
                    }
                }
                if(societyDB.ToLower().Equals("default")){
                    societyDB="1";
                }
                if(!string.IsNullOrEmpty(effectiveDateDB)){
                    if(!effectiveDateDB.ToLower().Equals("default")){
                        if(effectiveDateDB.Equals("Processing Date",StringComparison.OrdinalIgnoreCase)){
                            effectiveDateDB = ((int)payeeeffdateflg.Processing_Date).ToString();
                        }else if(effectiveDateDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            effectiveDateDB = ((int)payeeeffdateflg.Completion_Date).ToString();
                        }
                        string effectiveDateDBVal = "PAYEEEFFDATEFLG="+"'"+effectiveDateDB+"'";
                        payElementList.Add(effectiveDateDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(effectiveDateNumberDB)){
                    if(!effectiveDateNumberDB.ToLower().Equals("default")){
                        string effectiveDateNumberDBVal = "PAYEEFFDATEFREQAMTFLG="+"'"+effectiveDateNumberDB+"'";
                        payElementList.Add(effectiveDateNumberDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(effectiveDatePeriodDB)){
                    if(!effectiveDatePeriodDB.ToLower().Equals("default")){
                        if(effectiveDatePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            effectiveDatePeriodDB = ((char)paymentElementFrequencyflg.Days).ToString();
                        }else if(effectiveDatePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            effectiveDatePeriodDB = ((char)paymentElementFrequencyflg.Months).ToString();
                        }else if(effectiveDatePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            effectiveDatePeriodDB = ((char)paymentElementFrequencyflg.Days_Working).ToString();
                        }else if(effectiveDatePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            effectiveDatePeriodDB = ((char)paymentElementFrequencyflg.Weeks).ToString();
                        }else if(effectiveDatePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            effectiveDatePeriodDB = ((char)paymentElementFrequencyflg.None).ToString();
                        }
                        string effectiveDatePeriodDBVal = "PAYEEFFDATEFREQFLG="+"'"+effectiveDatePeriodDB+"'";
                        payElementList.Add(effectiveDatePeriodDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastDueDateDB)){
                    if(!lastDueDateDB.ToLower().Equals("default")){
                        if(lastDueDateDB.Equals("None",StringComparison.OrdinalIgnoreCase)){
                            lastDueDateDB = ((int)payelastdateflg.None).ToString();
                        }else if(lastDueDateDB.Equals("Processing Date",StringComparison.OrdinalIgnoreCase)){
                            lastDueDateDB = ((int)payelastdateflg.Processing_Date).ToString();
                        }
                        string lastDueDateDBVal = "PAYELASTDATEFLG="+"'"+lastDueDateDB+"'";
                        payElementList.Add(lastDueDateDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastDueDateNumberDB)){
                    if(!lastDueDateNumberDB.ToLower().Equals("default")){
                        string lastDueDateNumberDBVal = "PAYELASTDATEFREQAMTFLG="+"'"+lastDueDateNumberDB+"'";
                        payElementList.Add(lastDueDateNumberDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastDueDatePeriodDB)){
                    if(!lastDueDatePeriodDB.ToLower().Equals("default")){
                        if(lastDueDatePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            lastDueDatePeriodDB = ((char)paymentElementFrequencyflg.Days).ToString();
                        }else if(lastDueDatePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            lastDueDatePeriodDB = ((char)paymentElementFrequencyflg.Months).ToString();
                        }else if(lastDueDatePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            lastDueDatePeriodDB = ((char)paymentElementFrequencyflg.Days_Working).ToString();
                        }else if(lastDueDatePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            lastDueDatePeriodDB = ((char)paymentElementFrequencyflg.Weeks).ToString();
                        }else if(lastDueDatePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            lastDueDatePeriodDB = ((char)paymentElementFrequencyflg.None).ToString();
                        }
                        string lastDueDatePeriodDBVal = "PAYELASTDATEFREQFLG="+"'"+lastDueDatePeriodDB+"'";
                        payElementList.Add(lastDueDatePeriodDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastPayDateDB)){
                    if(!lastPayDateDB.ToLower().Equals("default")){
                        if(lastPayDateDB.Equals("None",StringComparison.OrdinalIgnoreCase)){
                            lastPayDateDB = ((int)payelastmadedateflg.None).ToString();
                        }else if(lastPayDateDB.Equals("Processing Date",StringComparison.OrdinalIgnoreCase)){
                            lastPayDateDB = ((int)payelastmadedateflg.Processing_Date).ToString();
                        }
                        string lastPayDateDBVal = "PAYELASTMADEDATEFLG="+"'"+lastPayDateDB+"'";
                        payElementList.Add(lastPayDateDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastPayDateNumberDB)){
                    if(!lastPayDateNumberDB.ToLower().Equals("default")){
                        string lastPayDateNumberDBVal = "PAYELASTMADEDATEFREQAMTFLG="+"'"+lastPayDateNumberDB+"'";
                        payElementList.Add(lastPayDateNumberDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastPayDatePeriodDB)){
                    if(!lastPayDatePeriodDB.ToLower().Equals("default")){
                        if(lastPayDatePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            lastPayDatePeriodDB = ((char)paymentElementFrequencyflg.Days).ToString();
                        }else if(lastPayDatePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            lastPayDatePeriodDB = ((char)paymentElementFrequencyflg.Months).ToString();
                        }else if(lastPayDatePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            lastPayDatePeriodDB = ((char)paymentElementFrequencyflg.Days_Working).ToString();
                        }else if(lastPayDatePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            lastPayDatePeriodDB = ((char)paymentElementFrequencyflg.Weeks).ToString();
                        }else if(lastPayDatePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            lastPayDatePeriodDB = ((char)paymentElementFrequencyflg.None).ToString();
                        }
                        string lastPayDatePeriodDBVal = "PAYELASTMADEDATEFREQFLG="+"'"+lastPayDatePeriodDB+"'";
                        payElementList.Add(lastPayDatePeriodDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastDebitMadeDB)){
                    if(!lastDebitMadeDB.ToLower().Equals("default")){
                        if(lastDebitMadeDB.Equals("None",StringComparison.OrdinalIgnoreCase)){
                            lastDebitMadeDB = ((int)payelastdebiteddateflg.None).ToString();
                        }else if(lastDebitMadeDB.Equals("Processing Date",StringComparison.OrdinalIgnoreCase)){
                            lastDebitMadeDB = ((int)payelastdebiteddateflg.Processing_Date).ToString();
                        }
                        string lastDebitMadeDBVal = "PAYELASTDEBITEDDATEFLG="+"'"+lastDebitMadeDB+"'";
                        payElementList.Add(lastDebitMadeDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastDebitMadeNumberDB)){
                    if(!lastDebitMadeNumberDB.ToLower().Equals("default")){
                        string lastDebitMadeNumberDBVal = "PAYELASTDEBITEDDATEFREQAMTFLG="+"'"+lastDebitMadeNumberDB+"'";
                        payElementList.Add(lastDebitMadeNumberDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(lastDebitMadePeriodDB)){
                    if(!lastDebitMadePeriodDB.ToLower().Equals("default")){
                        if(lastDebitMadePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            lastDebitMadePeriodDB = ((char)paymentElementFrequencyflg.Days).ToString();
                        }else if(lastDebitMadePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            lastDebitMadePeriodDB = ((char)paymentElementFrequencyflg.Months).ToString();
                        }else if(lastDebitMadePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            lastDebitMadePeriodDB = ((char)paymentElementFrequencyflg.Days_Working).ToString();
                        }else if(lastDebitMadePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            lastDebitMadePeriodDB = ((char)paymentElementFrequencyflg.Weeks).ToString();
                        }else if(lastDebitMadePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            lastDebitMadePeriodDB = ((char)paymentElementFrequencyflg.None).ToString();
                        }
                        string lastDebitMadePeriodDBVal = "PAYELASTDEBITEDDATEFREQFLG="+"'"+lastDebitMadePeriodDB+"'";
                        payElementList.Add(lastDebitMadePeriodDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(overPaymentDB)){
                    if(!overPaymentDB.ToLower().Equals("default")){
                        string overPaymentDBVal = "PAYEALLOWOVERPAYMENTFLG="+"'"+overPaymentDB+"'";
                        payElementList.Add(overPaymentDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstDateDB)){
                    if(!firstDateDB.ToLower().Equals("default")){
                        if(firstDateDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            firstDateDB = ((int)payefirstdatemortflg.Completion_Date).ToString();
                        }else if(firstDateDB.Equals("Payment Date",StringComparison.OrdinalIgnoreCase)){
                            firstDateDB = ((int)payefirstdatemortflg.Payment_Date).ToString();
                        }else if(firstDateDB.Equals("1st day of Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstDateDB = ((int)payefirstdatemortflg.first_day_of_Completion_Month).ToString();
                        }else if(firstDateDB.Equals("Preferred Payment Day in Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstDateDB = ((int)payefirstdatemortflg.Preferred_Payment_Day_in_Completion_Month).ToString();
                        }
                        string firstDateDBVal = "PAYEFIRSTDATEMORTFLG="+"'"+firstDateDB+"'";
                        payElementList.Add(firstDateDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(firstDateNumberDB)){
                    if(!firstDateNumberDB.ToLower().Equals("default")){
                        string firstDateNumberDBVal = "PAYEFIRSTDATEMORTFREQAMTFLG="+"'"+firstDateNumberDB+"'";
                        payElementList.Add(firstDateNumberDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(firstDatePeriodDB)){
                    if(!firstDatePeriodDB.ToLower().Equals("default")){
                        if(firstDatePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            firstDatePeriodDB = ((char)paymentElementFrequencyflg.Days).ToString();
                        }else if(firstDatePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            firstDatePeriodDB = ((char)paymentElementFrequencyflg.Months).ToString();
                        }else if(firstDatePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            firstDatePeriodDB = ((char)paymentElementFrequencyflg.Days_Working).ToString();
                        }else if(firstDatePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            firstDatePeriodDB = ((char)paymentElementFrequencyflg.Weeks).ToString();
                        }else if(firstDatePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            firstDatePeriodDB = ((char)paymentElementFrequencyflg.None).ToString();
                        }
                        string firstDatePeriodDBVal = "PAYEFIRSTDATEMORTFREQFLG="+"'"+firstDatePeriodDB+"'";
                        payElementList.Add(firstDatePeriodDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(firstRegDateDB)){
                    if(!firstRegDateDB.ToLower().Equals("default")){
                        if(firstRegDateDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateDB = ((int)payefirstregdatemortflg.Completion_Date).ToString();
                        }else if(firstRegDateDB.Equals("Payment Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateDB = ((int)payefirstregdatemortflg.Payment_Date).ToString();
                        }else if(firstRegDateDB.Equals("1st day of Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateDB = ((int)payefirstregdatemortflg.first_day_of_Completion_Month).ToString();
                        }else if(firstRegDateDB.Equals("Preferred Payment Day in Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateDB = ((int)payefirstregdatemortflg.Preferred_Payment_Day_in_Completion_Month).ToString();
                        }else if(firstRegDateDB.Equals("First Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateDB = ((int)payefirstregdatemortflg.First_Date).ToString();
                        }
                        string firstRegDateDBVal = "PAYEFIRSTREGDATEMORTFLG="+"'"+firstRegDateDB+"'";
                        payElementList.Add(firstRegDateDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(firstRegDateNumberDB)){
                    if(!firstRegDateNumberDB.ToLower().Equals("default")){
                        string firstRegDateNumberDBVal = "PAYEFIRSTREGDATEMORTFREQAMTFLG="+"'"+firstRegDateNumberDB+"'";
                        payElementList.Add(firstRegDateNumberDBVal);
                    }
                }


                if(!string.IsNullOrEmpty(firstRegDatePeriodDB)){
                    if(!firstRegDatePeriodDB.ToLower().Equals("default")){
                        if(firstRegDatePeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            firstRegDatePeriodDB = ((char)paymentElementFrequencyflg.Days).ToString();
                        }else if(firstRegDatePeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            firstRegDatePeriodDB = ((char)paymentElementFrequencyflg.Months).ToString();
                        }else if(firstRegDatePeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            firstRegDatePeriodDB = ((char)paymentElementFrequencyflg.Days_Working).ToString();
                        }else if(firstRegDatePeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            firstRegDatePeriodDB = ((char)paymentElementFrequencyflg.Weeks).ToString();
                        }else if(firstRegDatePeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            firstRegDatePeriodDB = ((char)paymentElementFrequencyflg.None).ToString();
                        }
                        string firstRegDatePeriodDBVal = "PAYEFIRSTREGDATEMORTFREQFLG="+"'"+firstRegDatePeriodDB+"'";
                        payElementList.Add(firstRegDatePeriodDBVal);
                    }
                }


                if(!string.IsNullOrEmpty(firstAmountDB)){
                    if(!firstAmountDB.ToLower().Equals("default")){
                        if(firstAmountDB.Equals("Interim Interest",StringComparison.OrdinalIgnoreCase)){
                            firstAmountDB = ((int)payefirstamountmortflg.Interim_Interest).ToString();
                        }else if(firstAmountDB.Equals("Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstAmountDB = ((int)payefirstamountmortflg.Regular_Payment_Amount).ToString();
                        }else if(firstAmountDB.Equals("Interim Interest + Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstAmountDB = ((int)payefirstamountmortflg.Interim_Interest_Regular_Payment_Amount).ToString();
                        }else if(firstAmountDB.Equals("Interim Int. + Conditional Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstAmountDB = ((int)payefirstamountmortflg.Interim_Int_Conditional_Regular_Payment_Amount).ToString();
                        }
                        string firstAmountDBVal = "PAYEFIRSTAMOUNTMORTFLG="+"'"+firstAmountDB+"'";
                        payElementList.Add(firstAmountDBVal);
                    }
                }
                
                

                if(!string.IsNullOrEmpty(regAmountDB)){
                    if(!regAmountDB.ToLower().Equals("default")){
                        if(regAmountDB.Equals("Regular Monthly Payment",StringComparison.OrdinalIgnoreCase)){
                            regAmountDB = ((int)payeregamountmortflg.Regular_Monthly_Payment).ToString();
                        }
                        string regAmountDBVal = "PAYEREGAMOUNTMORTFLG="+"'"+regAmountDB+"'";
                        payElementList.Add(regAmountDBVal);
                    }
                }

                
                if(!string.IsNullOrEmpty(usePEsettingsDDDB)){
                    if(!usePEsettingsDDDB.ToLower().Equals("default")){
                        string usePEsettingsDDDBVal = "DD_TO_USE_PAYMENT_ELEMENTS="+"'"+usePEsettingsDDDB+"'";
                        payElementList.Add(usePEsettingsDDDBVal);
                    }
                }



                if(!string.IsNullOrEmpty(firstDateInsDB)){
                    if(!firstDateInsDB.ToLower().Equals("default")){
                        if(firstDateInsDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsDB = ((int)payefirstdateinsflg.Completion_Date).ToString();
                        }else if(firstDateInsDB.Equals("Payment Date",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsDB = ((int)payefirstdateinsflg.Payment_Date).ToString();
                        }else if(firstDateInsDB.Equals("1st day of Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsDB = ((int)payefirstdateinsflg.first_day_of_Completion_Month).ToString();
                        }else if(firstDateInsDB.Equals("Preferred Payment Day in Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsDB = ((int)payefirstdateinsflg.Preferred_Payment_Day_in_Completion_Month).ToString();
                        }
                        string firstDateInsDBVal = "PAYEFIRSTDATEINSFLG="+"'"+firstDateInsDB+"'";
                        payElementList.Add(firstDateInsDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(firstDateInsNumberDB)){
                    if(!firstDateInsNumberDB.ToLower().Equals("default")){
                        string firstDateInsNumberDBVal = "PAYEFIRSTDATEINSFREQAMTFLG="+"'"+firstDateInsNumberDB+"'";
                        payElementList.Add(firstDateInsNumberDBVal);
                    }
                }


                if(!string.IsNullOrEmpty(firstDateInsPeriodDB)){
                    if(!firstDateInsPeriodDB.ToLower().Equals("default")){
                        if(firstDateInsPeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsPeriodDB = ((char)paymentElementFrequencyflg.Days).ToString();
                        }else if(firstDateInsPeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsPeriodDB = ((char)paymentElementFrequencyflg.Months).ToString();
                        }else if(firstDateInsPeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsPeriodDB = ((char)paymentElementFrequencyflg.Days_Working).ToString();
                        }else if(firstDateInsPeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsPeriodDB = ((char)paymentElementFrequencyflg.Weeks).ToString();
                        }else if(firstDateInsPeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            firstDateInsPeriodDB = ((char)paymentElementFrequencyflg.None).ToString();
                        }
                        string firstDateInsPeriodDBVal = "PAYEFIRSTDATEINSFREQFLG="+"'"+firstDateInsPeriodDB+"'";
                        payElementList.Add(firstDateInsPeriodDBVal);
                    }
                }
                
                

                if(!string.IsNullOrEmpty(firstRegDateInsDB)){
                    if(!firstRegDateInsDB.ToLower().Equals("default")){
                        if(firstRegDateInsDB.Equals("Completion Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsDB = ((int)payefirstregdateinsflg.Completion_Date).ToString();
                        }else if(firstRegDateInsDB.Equals("Payment Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsDB = ((int)payefirstregdateinsflg.Payment_Date).ToString();
                        }else if(firstRegDateInsDB.Equals("1st day of Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsDB = ((int)payefirstregdateinsflg.first_day_of_Completion_Month).ToString();
                        }else if(firstRegDateInsDB.Equals("Preferred Payment Day in Completion Month",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsDB = ((int)payefirstregdateinsflg.Preferred_Payment_Day_in_Completion_Month).ToString();
                        }else if(firstRegDateInsDB.Equals("First Date",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsDB = ((int)payefirstregdateinsflg.First_Date).ToString();
                        }
                        string firstRegDateInsDBVal = "PAYEFIRSTREGDATEINSFLG="+"'"+firstRegDateInsDB+"'";
                        payElementList.Add(firstRegDateInsDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(firstRegDateInsNumberDB)){
                    if(!firstRegDateInsNumberDB.ToLower().Equals("default")){
                        string firstRegDateInsNumberDBVal = "PAYEFIRSTREGDATEINSFREQAMTFLG="+"'"+firstRegDateInsNumberDB+"'";
                        payElementList.Add(firstRegDateInsNumberDBVal);
                    }
                }


                if(!string.IsNullOrEmpty(firstRegDateInsPeriodDB)){
                    if(!firstDateInsPeriodDB.ToLower().Equals("default")){
                        if(firstRegDateInsPeriodDB.Equals("Days",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsPeriodDB = ((char)paymentElementFrequencyflg.Days).ToString();
                        }else if(firstRegDateInsPeriodDB.Equals("Months",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsPeriodDB = ((char)paymentElementFrequencyflg.Months).ToString();
                        }else if(firstRegDateInsPeriodDB.Equals("Days(Working)",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsPeriodDB = ((char)paymentElementFrequencyflg.Days_Working).ToString();
                        }else if(firstRegDateInsPeriodDB.Equals("Weeks",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsPeriodDB = ((char)paymentElementFrequencyflg.Weeks).ToString();
                        }else if(firstRegDateInsPeriodDB.Equals("(None)",StringComparison.OrdinalIgnoreCase)){
                            firstRegDateInsPeriodDB = ((char)paymentElementFrequencyflg.None).ToString();
                        }
                        string firstRegDateInsPeriodDBVal = "PAYEFIRSTREGDATEINSFREQFLG="+"'"+firstRegDateInsPeriodDB+"'";
                        payElementList.Add(firstRegDateInsPeriodDBVal);
                    }
                }
                

                if(!string.IsNullOrEmpty(firstAmountInsDB)){
                    if(!firstAmountInsDB.ToLower().Equals("default")){
                        if(firstAmountInsDB.Equals("First Premium",StringComparison.OrdinalIgnoreCase)){
                            firstAmountInsDB = ((int)payefirstamountinsflg.First_Premium).ToString();
                        }else if(firstAmountInsDB.Equals("First Premium + Regular Premium",StringComparison.OrdinalIgnoreCase)){
                            firstAmountInsDB = ((int)payefirstamountinsflg.First_Premium_Regular_Premium).ToString();
                        }
                        string firstAmountInsDBVal = "PAYEFIRSTAMOUNTINSFLG="+"'"+firstAmountInsDB+"'";
                        payElementList.Add(firstAmountInsDBVal);
                    }
                }


                if(!string.IsNullOrEmpty(regAmountInsDB)){
                    if(!regAmountInsDB.ToLower().Equals("default")){
                        if(regAmountInsDB.Equals("Regular Premium",StringComparison.OrdinalIgnoreCase)){
                            regAmountInsDB = ((int)payeregamountinsflg.Regular_Premium).ToString();
                        }
                        string regAmountInsDBVal = "PAYEREGAMOUNTINSFLG="+"'"+regAmountInsDB+"'";
                        payElementList.Add(regAmountInsDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(useNextDebitDuePAFADB)){
                    if(!useNextDebitDuePAFADB.ToLower().Equals("default")){
                        string useNextDebitDuePAFADBVal = "PAYEFANEXTDEBITDUEFLG="+"'"+useNextDebitDuePAFADB+"'";
                        payElementList.Add(useNextDebitDuePAFADBVal);
                    }
                }


                if(!string.IsNullOrEmpty(firstDateMonthCompletionFADB)){
                    if(!firstDateMonthCompletionFADB.ToLower().Equals("default")){
                        if(firstDateMonthCompletionFADB.Equals("Interim Interest",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthCompletionFADB = ((int)payefirstdateinmonthofcompflg.Interim_Interest).ToString();
                        }else if(firstDateMonthCompletionFADB.Equals("Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthCompletionFADB = ((int)payefirstdateinmonthofcompflg.Regular_Payment_Amount).ToString();
                        }else if(firstDateMonthCompletionFADB.Equals("Interim Interest + Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthCompletionFADB = ((int)payefirstdateinmonthofcompflg.Interim_Interest_Regular_Payment_Amount).ToString();
                        }
                        string firstDateMonthCompletionFADBVal = "PAYEFIRSTDATEINMONTHOFCOMPFLG="+"'"+firstDateMonthCompletionFADB+"'";
                        payElementList.Add(firstDateMonthCompletionFADBVal);
                    }
                }



                if(!string.IsNullOrEmpty(firstDateMonthFollowCompletionFADB)){
                    if(!firstDateMonthFollowCompletionFADB.ToLower().Equals("default")){
                        if(firstDateMonthFollowCompletionFADB.Equals("Interim Interest",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthFollowCompletionFADB = ((int)payefirstdatefollowingcompflg.Interim_Interest).ToString();
                        }else if(firstDateMonthFollowCompletionFADB.Equals("Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthFollowCompletionFADB = ((int)payefirstdatefollowingcompflg.Regular_Payment_Amount).ToString();
                        }else if(firstDateMonthFollowCompletionFADB.Equals("Interim Interest + Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthFollowCompletionFADB = ((int)payefirstdatefollowingcompflg.Interim_Interest_Regular_Payment_Amount).ToString();
                        }
                        string firstDateMonthFollowCompletionFADBVal = "PAYEFIRSTDATEFOLLOWINGCOMPFLG="+"'"+firstDateMonthFollowCompletionFADB+"'";
                        payElementList.Add(firstDateMonthFollowCompletionFADBVal);
                    }
                }

                if(!string.IsNullOrEmpty(fMEffectiveDateAlignFADB)){
                    if(!fMEffectiveDateAlignFADB.ToLower().Equals("default")){
                        if(fMEffectiveDateAlignFADB.Equals("FM after PE effective date + BACS days",StringComparison.OrdinalIgnoreCase)){
                            fMEffectiveDateAlignFADB = ((int)fMEffectiveDateAlignFA.FM_after_PE_effective_date_BACS_days).ToString();
                        }else if(fMEffectiveDateAlignFADB.Equals("FM after Processing date + BACS days",StringComparison.OrdinalIgnoreCase)){
                            fMEffectiveDateAlignFADB = ((int)fMEffectiveDateAlignFA.FM_after_Processing_date_BACS_days).ToString();
                        }else if(fMEffectiveDateAlignFADB.Equals("FM & PE aligned after Processing date + BACS days",StringComparison.OrdinalIgnoreCase)){
                            fMEffectiveDateAlignFADB = ((int)fMEffectiveDateAlignFA.FM_And_PE_aligned_after_Processing_date_BACS_days).ToString();
                        }else if(fMEffectiveDateAlignFADB.Equals("FM & PE aligned after BACS-Term Adjusted",StringComparison.OrdinalIgnoreCase)){
                            fMEffectiveDateAlignFADB = ((int)fMEffectiveDateAlignFA.FM_And_PE_aligned_after_BACS_Term_Adjusted).ToString();
                        }
                        string fMEffectiveDateAlignFADBVal = "PAYE_FIRST_ALLIGN_FA="+"'"+fMEffectiveDateAlignFADB+"'";
                        payElementList.Add(fMEffectiveDateAlignFADBVal);
                    }
                }

                
                if(!string.IsNullOrEmpty(useNextDebitDueOnPAMVDB)){
                    if(!useNextDebitDueOnPAMVDB.ToLower().Equals("default")){
                        string useNextDebitDueOnPAMVDBVal = "PAYE_MV_NEXT_DEBIT_DUE_IND="+"'"+useNextDebitDueOnPAMVDB+"'";
                        payElementList.Add(useNextDebitDueOnPAMVDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstDateMonthCompletionMVDB)){
                    if(!firstDateMonthCompletionMVDB.ToLower().Equals("default")){
                        if(firstDateMonthCompletionMVDB.Equals("Interim Interest",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthCompletionMVDB = ((int)paye_1st_date_in_comp_mth_ind.Interim_Interest).ToString();
                        }else if(firstDateMonthCompletionMVDB.Equals("Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthCompletionMVDB = ((int)paye_1st_date_in_comp_mth_ind.Regular_Payment_Amount).ToString();
                        }else if(firstDateMonthCompletionMVDB.Equals("Interim Interest + Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthCompletionMVDB = ((int)paye_1st_date_in_comp_mth_ind.Interim_Interest_Regular_Payment_Amount).ToString();
                        }
                        string firstDateMonthCompletionMVDBVal = "PAYE_1ST_DATE_IN_COMP_MTH_IND="+"'"+firstDateMonthCompletionMVDB+"'";
                        payElementList.Add(firstDateMonthCompletionMVDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(firstDateMonthFollowCompletionMVDB)){
                    if(!firstDateMonthFollowCompletionMVDB.ToLower().Equals("default")){
                        if(firstDateMonthFollowCompletionMVDB.Equals("Interim Interest",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthFollowCompletionMVDB = ((int)paye_1st_date_follow_comp_ind.Interim_Interest).ToString();
                        }else if(firstDateMonthFollowCompletionMVDB.Equals("Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthFollowCompletionMVDB = ((int)paye_1st_date_follow_comp_ind.Regular_Payment_Amount).ToString();
                        }else if(firstDateMonthFollowCompletionMVDB.Equals("Interim Interest + Regular Payment Amount",StringComparison.OrdinalIgnoreCase)){
                            firstDateMonthFollowCompletionMVDB = ((int)paye_1st_date_follow_comp_ind.Interim_Interest_Regular_Payment_Amount).ToString();
                        }
                        string firstDateMonthFollowCompletionMVDBVal = "PAYE_1ST_DATE_FOLLOW_COMP_IND="+"'"+firstDateMonthFollowCompletionMVDB+"'";
                        payElementList.Add(firstDateMonthFollowCompletionMVDBVal);
                    }
                }
                finalSCMPaymentElement = string.Join(",", payElementList.ToArray());
                Report.Info("query string inputs--"+finalSCMPaymentElement);
                string updateQueryPAYVM = "UPDATE SAM05 SET "+finalSCMPaymentElement+" where SOCSEQNO='"+societyDB+"'";
                oraUtility.executeQuery(updateQueryPAYVM);
            }
        }

        private void General(string generalDB){
            if(!string.IsNullOrEmpty(generalDB)){
                List<string> generalList = new List<string>();
                string sqlQuery="";
                OracleUtility oracUtility=OracleUtility.Instance();
                sqlQuery = "select  *  from [ACT_SCM_General] where [Reference] = '"+generalDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_Society);
                string defaultCountryDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_DefaultCountry);
                string defaultCountryValue=TestDataConstants.ACT_SCM_General_DefaultCountryValue;
                string affinitySchemeDB =dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_AffinityScheme);
                string useCustTierLoyalFlgDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_UseCustTierLoyalFlg);
                string perfProdUWLoyalChkFlgDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_PerfProdUWLoyalChkFlg);
                string illsCtlEnbAmdFlgDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_IllsCtlEnbAmdFlg);
                string useCredBurInfoFlgDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_UseCredBurInfoFlg);
                string ammortFlgDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_AmmortFlg);
                string effectOfBaseRateUpperRateDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_EffectOfBaseRateUpperRate);
                string effectOfBaseRateLowerRateDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_EffectOfBaseRateLowerRate);
                string fCMPercDecreaseBaseCurrDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_FCMPercDecreaseBaseCurr);
                string whatIfCalcDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_WhatIfCalc);
                string operatorCreationBranchDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_OperatorCreationBranch);
                string operatorCreationBranchDefaultValue=TestDataConstants.ACT_SCM_General_OperatorCreationBranchDefaultValue;
                string operatorCreationRoleDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_OperatorCreationRole);
                string operatorCreationRoleDefaultValue=TestDataConstants.ACT_SCM_General_OperatorCreationRoleDefaultValue;
                string rioMtgTermDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_RioMtgTerm);
                string transactionSourceDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_TransactionSource);
                string signOffMandLevelDB=dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_General_SignOffMandLevel);
                
                if(!string.IsNullOrEmpty(societyDB)){
                    if(!societyDB.ToLower().Equals("default")){
                        string societyVal = "SOCSEQNO="+"'"+societyDB+"'";
                        generalList.Add(societyVal);
                    }
                }
                if(societyDB.ToLower().Equals("default")){
                    societyDB="1";
                }
                if(!string.IsNullOrEmpty(affinitySchemeDB)){
                    if(!affinitySchemeDB.ToLower().Equals("default")){
                        if(affinitySchemeDB.Equals("Customer Affinity Scheme Link",StringComparison.OrdinalIgnoreCase)){
                            affinitySchemeDB = ((char)affScheme.Customer_Affinity_Scheme_Link).ToString();
                        }else if(affinitySchemeDB.Equals("Account Type Affinity Scheme Link",StringComparison.OrdinalIgnoreCase)){
                            affinitySchemeDB = ((char)affScheme.Account_Type_Affinity_Scheme_Link).ToString();
                        }
                        string affinitySchemeDBVal = "AFFSCH_LNK="+"'"+affinitySchemeDB+"'";
                        generalList.Add(affinitySchemeDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(useCustTierLoyalFlgDB)){
                    if(!useCustTierLoyalFlgDB.ToLower().Equals("default")){
                        string useCustTierLoyalFlgDBVal = "TIER="+"'"+useCustTierLoyalFlgDB+"'";
                        generalList.Add(useCustTierLoyalFlgDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(perfProdUWLoyalChkFlgDB)){
                    if(!perfProdUWLoyalChkFlgDB.ToLower().Equals("default")){
                        string perfProdUWLoyalChkFlgDBVal = "PRODUWLTVLOYALTYFLG="+"'"+perfProdUWLoyalChkFlgDB+"'";
                        generalList.Add(perfProdUWLoyalChkFlgDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(illsCtlEnbAmdFlgDB)){
                    if(!illsCtlEnbAmdFlgDB.ToLower().Equals("default")){
                        string illsCtlEnbAmdFlgDBVal = "ILLUSTRATION_AMEND_ENABLED="+"'"+illsCtlEnbAmdFlgDB+"'";
                        generalList.Add(illsCtlEnbAmdFlgDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(useCredBurInfoFlgDB)){
                    if(!useCredBurInfoFlgDB.ToLower().Equals("default")){
                        string useCredBurInfoFlgDBVal = "CREDIT_BUR_INFO="+"'"+useCredBurInfoFlgDB+"'";
                        generalList.Add(useCredBurInfoFlgDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(ammortFlgDB)){
                    if(!ammortFlgDB.ToLower().Equals("default")){
                        string ammortFlgDBVal = "AMORTISATION_CALC="+"'"+ammortFlgDB+"'";
                        generalList.Add(ammortFlgDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(effectOfBaseRateUpperRateDB)){
                    if(!effectOfBaseRateUpperRateDB.ToLower().Equals("default")){
                        string effectOfBaseRateUpperRateDBVal = "UPPER_RATE_CHANGE="+"'"+effectOfBaseRateUpperRateDB+"'";
                        generalList.Add(effectOfBaseRateUpperRateDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(effectOfBaseRateLowerRateDB)){
                    if(!effectOfBaseRateLowerRateDB.ToLower().Equals("default")){
                        string effectOfBaseRateLowerRateDBVal = "LOWER_RATE_CHANGE="+"'"+effectOfBaseRateLowerRateDB+"'";
                        generalList.Add(effectOfBaseRateLowerRateDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(fCMPercDecreaseBaseCurrDB)){
                    if(!fCMPercDecreaseBaseCurrDB.ToLower().Equals("default")){
                        string fCMPercDecreaseBaseCurrDBVal = "NAT_CURR_PERC_DECR="+"'"+fCMPercDecreaseBaseCurrDB+"'";
                        generalList.Add(fCMPercDecreaseBaseCurrDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(signOffMandLevelDB)){
                    if(!signOffMandLevelDB.ToLower().Equals("default")){
                        if(signOffMandLevelDB.Equals("Application Level",StringComparison.OrdinalIgnoreCase)){
                            signOffMandLevelDB = ((char)signOffMandate.Application_Level).ToString();
                        }else if(signOffMandLevelDB.Equals("Exposure Level",StringComparison.OrdinalIgnoreCase)){
                            signOffMandLevelDB = ((char)signOffMandate.Exposure_Level).ToString();
                        }
                        string signOffMandLevelDBVal = "MANDATE_LEVEL="+"'"+signOffMandLevelDB+"'";
                        generalList.Add(signOffMandLevelDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(transactionSourceDB)){
                    if(!transactionSourceDB.ToLower().Equals("default")){
                        if(transactionSourceDB.Equals("Originating Branch",StringComparison.OrdinalIgnoreCase)){
                            transactionSourceDB = ((char)transSource.Originating_Branch).ToString();
                        }else if(transactionSourceDB.Equals("Posting Branch",StringComparison.OrdinalIgnoreCase)){
                            transactionSourceDB = ((char)transSource.Posting_Branch).ToString();
                        }
                        string transactionSourceDBVal = "TRANSOURCEFLAG="+"'"+transactionSourceDB+"'";
                        generalList.Add(transactionSourceDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(whatIfCalcDB)){
                    if(!whatIfCalcDB.ToLower().Equals("default")){
                        if(whatIfCalcDB.Equals("Enter interest rate only",StringComparison.OrdinalIgnoreCase)){
                            whatIfCalcDB = ((char)whatIfCal.Enter_interest_rate_only).ToString();
                        }else if(whatIfCalcDB.Equals("Enter product code only",StringComparison.OrdinalIgnoreCase)){
                            whatIfCalcDB = ((char)whatIfCal.Enter_product_code_only).ToString();
                        }else if(whatIfCalcDB.Equals("Enter product code or rate",StringComparison.OrdinalIgnoreCase)){
                            whatIfCalcDB = ((char)whatIfCal.Enter_product_code_or_rate).ToString();
                        }
                        string whatIfCalcDBVal = "WHAT_IF_BEHAVIOUR="+"'"+whatIfCalcDB+"'";
                        generalList.Add(whatIfCalcDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(rioMtgTermDB)){
                    if(!rioMtgTermDB.ToLower().Equals("default")){
                        string rioMtgTermDBVal = "APP_RIO_LOAN_TERM="+"'"+rioMtgTermDB+"'";
                        generalList.Add(rioMtgTermDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(operatorCreationBranchDB)){
                    if(!operatorCreationBranchDB.ToLower().Equals("default")){
                        if(!operatorCreationBranchDB.ToLower().Equals(operatorCreationBranchDefaultValue)){
                            sqlQuery ="select BRN_CODE from BRANCHES where BRN_NAME='"+operatorCreationBranchDB+"'";
                            List<string[]> result=oracUtility.executeQuery(sqlQuery);
                            operatorCreationBranchDB=result[0][0];
                            string operatorCreationBranchDBVal = "SUMMIT_DEFAULT_BRN_CODE="+"'"+operatorCreationBranchDB+"'";
                            generalList.Add(operatorCreationBranchDBVal);
                        }
                    }
                }
                
                if(!string.IsNullOrEmpty(operatorCreationRoleDB)){
                    if(!operatorCreationRoleDB.ToLower().Equals("default")){
                        if(!operatorCreationRoleDB.ToLower().Equals(operatorCreationRoleDefaultValue)){
                            string[] role=operatorCreationRoleDB.Split('-');
                            sqlQuery ="select ROL_SEQNO from UAS_ROLES where ROL_TITLE like '%"+role[0].Trim()+"%' and ROL_DESCRIPTION like '%"+role[1].Trim()+"%' ";
                            List<string[]> result=oracUtility.executeQuery(sqlQuery);
                            operatorCreationRoleDB=result[0][0];
                            string operatorCreationRoleDBVal = "SUMMIT_DEFAULT_ROL_SEQNO="+"'"+operatorCreationRoleDB+"'";
                            generalList.Add(operatorCreationRoleDBVal);
                        }
                    }
                }
                
                if(!string.IsNullOrEmpty(defaultCountryDB)){
                    if(!defaultCountryDB.ToLower().Equals("default")){
                        if(!defaultCountryDB.ToLower().Equals(defaultCountryValue)){
                            sqlQuery ="select VT_CODE from VALIDATION_TABLE where VT_VTH_SEQNO=2113 and VT_DESC='"+defaultCountryDB+"'";
                            List<string[]> result=oracUtility.executeQuery(sqlQuery);
                            defaultCountryDB=result[0][0];
                            string defaultCountryDBVal = "ADDRESS_DEF_CNTRY_CODE="+"'"+defaultCountryDB+"'";
                            generalList.Add(defaultCountryDBVal);
                        }
                    }
                }
                finalSCMGeneral = string.Join(",", generalList.ToArray());
                Report.Info("query string inputs--"+finalSCMGeneral);
                string updateQueryGeneral = "UPDATE SAM05 SET "+finalSCMGeneral+" where SOCSEQNO='"+societyDB+"'";
                oraUtility.executeQuery(updateQueryGeneral);
            }
        }
        
        
        private void Fees(string feesDB){
            if(!string.IsNullOrEmpty(feesDB)){
                List<string> feesList = new List<string>();
                string sqlQuery="";
                OracleUtility oracUtility=OracleUtility.Instance();
                sqlQuery = "select  *  from [ACT_SCM_Fees] where [Reference] = '"+feesDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_Society);
                string prodFeeGenerationDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_ProdFeeGeneration);
                string hlcFeeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_HlcFee);
                string chargeDuplicateRSFeeCBXDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_ChargeDuplicateRSFeeCBX);
                string rsFeeCatgDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_RSFeeCatg);
                string addInterestDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_AdditionalInterest);
                string legalFeeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_LegalFee);
                string redemFeeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_RedemFee);
                string cashBackDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_CashBack);
                string rsAmndCatgDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_SCM_Fees_RSAmndCatg);
                
                if(!string.IsNullOrEmpty(societyDB)){
                    if(!societyDB.ToLower().Equals("default")){
                        string societyVal = "SOCSEQNO="+"'"+societyDB+"'";
                        feesList.Add(societyVal);
                    }
                }
                if(societyDB.ToLower().Equals("default")){
                    societyDB="1";
                }
                
                if(!string.IsNullOrEmpty(prodFeeGenerationDB)){
                    if(!prodFeeGenerationDB.ToLower().Equals("default")){
                        if(prodFeeGenerationDB.Equals("Primary Product Only",StringComparison.OrdinalIgnoreCase)){
                            prodFeeGenerationDB = ((char)prodFeeGeneration.Primary_Product_Only).ToString();
                        }else if(prodFeeGenerationDB.Equals("Unique fees for unique products",StringComparison.OrdinalIgnoreCase)){
                            prodFeeGenerationDB = ((char)prodFeeGeneration.Unique_fees_for_unique_products).ToString();
                        }else if(prodFeeGenerationDB.Equals("All fees for Unique Products",StringComparison.OrdinalIgnoreCase)){
                            prodFeeGenerationDB = ((char)prodFeeGeneration.All_fees_for_Unique_Products).ToString();
                        }
                        string prodFeeGenerationDBVal = "FEE_PRIMARY_PROD="+"'"+prodFeeGenerationDB+"'";
                        feesList.Add(prodFeeGenerationDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(hlcFeeDB)){
                    if(!hlcFeeDB.ToLower().Equals("default")){
                        string hlcFeeDBVal = "FEECDMIG="+"'"+hlcFeeDB+"'";
                        feesList.Add(hlcFeeDBVal);
                    }
                }
                if(!string.IsNullOrEmpty(chargeDuplicateRSFeeCBXDB)){
                    if(!chargeDuplicateRSFeeCBXDB.ToLower().Equals("default")){
                        string chargeDuplicateRSFeeCBXDBVal = "FEES_DUPLICATE_PROD_FEE="+"'"+chargeDuplicateRSFeeCBXDB+"'";
                        feesList.Add(chargeDuplicateRSFeeCBXDBVal);
                    }
                }

                if(!string.IsNullOrEmpty(rsFeeCatgDB)){
                    if(!rsFeeCatgDB.ToLower().Equals("default")){
                        string rsFeeCatgDBVal = "FEES_RATE_SWITCH_FEE_CAT="+"'"+rsFeeCatgDB+"'";
                        feesList.Add(rsFeeCatgDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(addInterestDB)){
                    if(!addInterestDB.ToLower().Equals("default")){
                        string addInterestDBVal = "FEES_RSF_ADDITIONAL_INT="+"'"+addInterestDB+"'";
                        feesList.Add(addInterestDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(legalFeeDB)){
                    if(!legalFeeDB.ToLower().Equals("default")){
                        string legalFeeDBVal = "FEES_RSF_LEGAL_FEE="+"'"+legalFeeDB+"'";
                        feesList.Add(legalFeeDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(redemFeeDB)){
                    if(!redemFeeDB.ToLower().Equals("default")){
                        string redemFeeDBVal = "FEES_RSF_REDEMP_FEE="+"'"+redemFeeDB+"'";
                        feesList.Add(redemFeeDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(cashBackDB)){
                    if(!cashBackDB.ToLower().Equals("default")){
                        string cashBackDBVal = "FEES_RSF_CASHBACK="+"'"+cashBackDB+"'";
                        feesList.Add(cashBackDBVal);
                    }
                }
                
                if(!string.IsNullOrEmpty(rsAmndCatgDB)){
                    if(!rsAmndCatgDB.ToLower().Equals("default")){
                        string rsAmndCatgDBVal = "FEES_RSF_SECURITY_CAT="+"'"+rsAmndCatgDB+"'";
                        feesList.Add(rsAmndCatgDBVal);
                    }
                }
                
                finalSCMFees = string.Join(",", feesList.ToArray());
                Report.Info("query string inputs--"+finalSCMFees);
                string updateQueryFees = "UPDATE SAM05 SET "+finalSCMFees+" where SOCSEQNO='"+societyDB+"'";
                oraUtility.executeQuery(updateQueryFees);
            }
        }
    }
}
