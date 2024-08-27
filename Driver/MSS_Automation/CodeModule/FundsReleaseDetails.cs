/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 20/09/2022
 * Time: 08:11
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
    /// Description of FundsReleaseDetails.
    /// </summary>
    public partial class Keywords
    {
        string societyDropdown= string.Empty;
        string funderSelectFlg = string.Empty;
        string paymentTypeDB = string.Empty;
        public List<string> FundsReleaseDetails()
        {
            try{
                Main.appFlag = Constants.appActivate;
                string dbQuery = "select CHP_BATCHNO from SAM05 where CHP_BATCHNO like '%99'";
                List<string[]> data=oraUtility.executeQuery(dbQuery );
                if(data.Count!=0){
                    string batchNum = data[0][0];
                    if(!string.IsNullOrEmpty(batchNum)){
                        string result = batchNum.Substring(8);
                        string preFix = batchNum.Substring(0,8);
                        string postFix= string.Empty;
                        string finalVal= string.Empty;
                        int resultInt = Int32.Parse(result);
                        if(resultInt==99){
                            postFix="01";
                            finalVal=preFix+postFix;
                            dbQuery="UPDATE SAM05 SET CHP_BATCHNO='"+finalVal+"' where CHP_BATCHNO='"+batchNum+"'";
                            oraUtility.executeQuery(dbQuery);
                        }
                    }
                }
                if("Y".Equals(Settings.getInstance().get("PRINT_ACTIVATE_FLAG").ToUpper())){
                    OpenMacro(TestDataConstants.Act_FundsReleaseDetails_MacroPrint);
                }else{
                    OpenMacro(TestDataConstants.Act_FundsReleaseDetails_Macro);
                }
                if(!FAFlag){
                    ValuationReport();
                }else{
                    ValuationEnquiry();
                    ValuationHistory();
                }
                if("Y".Equals(Settings.getInstance().get("PRINT_ACTIVATE_FLAG").ToUpper())){
                    LetterProductionLetterReviewOk();
                    LetterProductionParagraphSelectionOk();
                    PrintHandleOk();
                    LetterProductionParagraphSelectionOk();
                    PrintHandleOk();
                }
                MaintainCompletionDetails();
                FundReleaseDetails();
                ElectronicPaymentsBatchGeneration();
                GenerateElectronicPayments();
                ElectronicPaymentsBatchProduction();
                PrintingElectronicPaymentsReport();
                if("Y".Equals(Settings.getInstance().get("PRINT_ACTIVATE_FLAG").ToUpper())){
                    LetterProductionLetterReviewOk();
                    LetterProductionParagraphSelectionOk();
                    PrintHandleOk();
                    LetterProductionParagraphSelectionOk();
                    PrintHandleOk();
                    PrintHandleOk();
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
        
        private void HandlePromptFundReleaseKeyword(string pageTitle, string txtMsg, string btn){
            try{
                waitForPagetoAppear(pageTitle);
                Button btnOk = btn;
                btnOk.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void ValuationReport(){
            string sqlQuery = "select  *  from [ACT_Valuation_Report] where [Reference] = '"+Main.InputData[0]+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            
            string valuerFirmDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_ValuerFirm);
            string buildStageDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_BuildStage);
            string propertyTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_PropertyType);
            string wallConstructionTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_WallConstructionType);
            string roofConstructionTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_RoofConstructionType);
            string tenureDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_Tenure);
            string valtypePerformedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_ValtypePerformed);
            string propertysuitableDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_Propertysuitable);
            string valuationAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_ValuationAmount);
            string valuationDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_ValuationDate);
            string rentalDemandDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_RentalDemand);
            string recommendedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_Recommended);
            string actualDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_Actual);
            string enhancedValuationDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ValuationReport_EnhancedValuation);
            waitForPagetoAppear("- Valuation Report");
            
            if(!string.IsNullOrEmpty(valuerFirmDB)){
                if(!valuerFirmDB.ToLower().Equals("default")){
                    Text valuerFirm = fetchElement("Activate_Valuation Report_TXT_ValuerFirm");
                    Button vFPushButton = fetchElement("Activate_Valuation Report_BTN_ValuerFirm_PushButton");
                    vFPushButton.Click();
                    waitForPagetoAppear("Valuer Selection");
                    Text valuerName = fetchElement("Activate_Valuer Selection_TXT_Valuer Name");
                    string[] valuerNameFirst = valuerFirmDB.Split(' ');
                    string valuerNameTxt = valuerNameFirst[0];
                    setText(valuerName, valuerNameTxt);
                    Button btnSearch = fetchElement("Activate_Valuer Selection_BTN_Search");
                    btnSearch.Click();
                    RxPath rxPath = fetchElement("Activate_Valuer Selection_TBL_Col_ValuerBranch");
                    DoubleClickActivateTableCell(valuerFirmDB, rxPath);
                    if(!string.IsNullOrEmpty(valuerFirm.TextValue)){
                        Report.Success("Valuer Selected successfully");
                    }else{
                        Report.Error("Valuer Not Selected.");
                    }
                }
            }
            
            if(!string.IsNullOrEmpty(buildStageDB)){
                if(!buildStageDB.ToLower().Equals("default")){
                    List buildStage = fetchElement("Activate_Valuation Report_LST_Build_Stage");
                    ComboboxItemSelectDirect(buildStage, buildStageDB);
                }
            }
            if(!string.IsNullOrEmpty(propertyTypeDB)){
                if(!propertyTypeDB.ToLower().Equals("default")){
                    List propertyType = fetchElement("Activate_Valuation Report_LST_Property_Type");
                    ComboboxItemSelectDirect(propertyType, propertyTypeDB);
                    if(propertyTypeDB.ToUpper().Equals("APARTMENT")){
                        waitForPagetoAppear("Valuation Receipt - Flats");
                        Button btnOkVRF = fetchElement("Activate_Valuation Report_BTN_ValuationReceipt_Flats_Ok");
                        btnOkVRF.Click();
                    }
                }
            }
            if(!string.IsNullOrEmpty(wallConstructionTypeDB)){
                if(!wallConstructionTypeDB.ToLower().Equals("default")){
                    List wallConstructionType = fetchElement("Activate_Valuation Report_LST_Wall_Construction_Type");
                    ComboboxItemSelectDirect(wallConstructionType, wallConstructionTypeDB);
                }
            }
            if(!string.IsNullOrEmpty(roofConstructionTypeDB)){
                if(!roofConstructionTypeDB.ToLower().Equals("default")){
                    List roofConstructionType = fetchElement("Activate_Valuation Report_LST_Roof_Construction_Type");
                    ComboboxItemSelectDirect(roofConstructionType, roofConstructionTypeDB);
                }
            }
            if(!string.IsNullOrEmpty(tenureDB)){
                if(!tenureDB.ToLower().Equals("default")){
                    List tenure = fetchElement("Activate_Valuation Report_LST_Tenure");
                    ComboboxItemSelectDirect(tenure, tenureDB);
                }
            }
            if(!string.IsNullOrEmpty(valtypePerformedDB)){
                if(!valtypePerformedDB.ToLower().Equals("default")){
                    List valtypePerformed = fetchElement("Activate_Valuation Report_LST_Val_Type_Performed");
                    ListtemSelectDirectPathEquals(valtypePerformed, valtypePerformedDB);
                }
            }
            if(!string.IsNullOrEmpty(propertysuitableDB)){
                if(!propertysuitableDB.ToLower().Equals("default")){
                    ComboBox propertysuitable = fetchElement("Activate_Valuation Report_LST_Property_Suitable");
                    ComboboxItemSelectDirect(propertysuitable, propertysuitableDB);
                }
            }
            if(!string.IsNullOrEmpty(valuationAmountDB)){
                if(!valuationAmountDB.ToLower().Equals("default")){
                    Text valuationAmount = fetchElement("Activate_Valuation Report_TXT_Valuation_Amount");
                    setText(valuationAmount, valuationAmountDB);
                }
            }
            if(!string.IsNullOrEmpty(valuationDateDB)){
                if(!valuationDateDB.ToLower().Equals("default")){
                    Text valuationDate = fetchElement("Activate_Valuation Report_TXT_Valuation_Date");
                    setText(valuationDate, valuationDateDB);
                }
            }
            if(!string.IsNullOrEmpty(rentalDemandDB)){
                if(!rentalDemandDB.ToLower().Equals("default")){
                    List rentalDemand = fetchElement("Activate_Valuation Report_LST_RentalDemand");
                    ComboboxItemSelectDirect(rentalDemand, rentalDemandDB);
                }
            }
            RxPath recommendedRx = fetchElement("Activate_Valuation Report_TXT_Recommended");
            RxPath actualRx = fetchElement("Activate_Valuation Report_TXT_Actual");
            RxPath enhancedValuationRx = fetchElement("Activate_Valuation Report_TXT_EnhancedValuation");
            setTextValue(recommendedRx, recommendedDB);
            setTextValue(actualRx, actualDB);
            setTextValue(enhancedValuationRx, enhancedValuationDB);
            Button btnOk = fetchElement("Activate_Valuation Report_BTN_OK");
            btnOk.Click();            
        }
        
        private void MaintainCompletionDetails(){
            string sqlQuery = "select  *  from [ACT_Maintain_Completion_Details] where [Reference] = '"+Main.InputData[0]+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            waitForPagetoAppear("- Maintain Completion Details");
            string expectedDateOfCompletionDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MaintainCompletionDetails_ExpectedDateOfCompletion);
            if(!expectedDateOfCompletionDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                Text expectedDateOfCompletion = fetchElement("Activate_Maintain Completion Details_TXT_Expected_Date_of_Completion");
                setText(expectedDateOfCompletion, expectedDateOfCompletionDB);
            }
            paymentTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MaintainCompletionDetails_PaymentType);
            if(!paymentTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                List paymentType = fetchElement("Activate_Maintain Completion Details_LST_Payment_Type");
                ComboboxItemSelectDirect(paymentType, paymentTypeDB);
            }
            string payeeTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MaintainCompletionDetails_PayeeType);
            ComboBox payeeType = fetchElement("Activate_Maintain Completion Details_LST_Payee_Type");
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            ComboboxItemSelectDirect(payeeType, payeeTypeDB);
            
            Button btnOk2 = fetchElement("Activate_Maintain Completion Details_BTN_OK");
            btnOk2.Click();
        }
        
        private void FundReleaseDetails(){
        	if(!FAFlag){
        		string sqlQuery = "select  *  from [ACT_Funds_Release_Details] where [Reference] = '"+Main.InputData[0]+"'";
        		dbUtility.ReadDBResultMS(sqlQuery);
        		waitForPagetoAppear("- Funds Release Details");
        		string releaseReasonDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_FundsReleaseDetails_ReleaseReason);
        		string releaseAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_FundsReleaseDetails_ReleaseAmount);
        		string paymentTypeDB2 = dbUtility.GetAccessFieldValue(TestDataConstants.Act_FundsReleaseDetails_PaymentType);
        		string paymentAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_FundsReleaseDetails_PaymentAmount);
        		string adviceTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_FundsReleaseDetails_AdviceType);
        		if(!string.IsNullOrEmpty(releaseReasonDB)){
        			if(!releaseReasonDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
        				Report.Failure("This is read Only field for the Keyword. Please check the data and try again");
        			}
        		}
        		if(!string.IsNullOrEmpty(releaseAmountDB)){
        			if(!releaseAmountDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
        				Report.Failure("This is read Only field for the Keyword. Please check the data and try again");
        			}
        		}
        		if(!string.IsNullOrEmpty(paymentTypeDB2)){
        			if(!paymentTypeDB2.ToUpper().Equals(Constants.TestData_DEFAULT)){
        				Report.Failure("This is read Only field for the Keyword. Please check the data and try again");
        			}
        		}
        		if(!string.IsNullOrEmpty(paymentAmountDB)){
        			if(!paymentAmountDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
        				Report.Failure("This is read Only field for the Keyword. Please check the data and try again");
        			}
        		}
        		if(!string.IsNullOrEmpty(adviceTypeDB)){
        			if(!adviceTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
        				Report.Failure("This is read Only field for the Keyword. Please check the data and try again");
        			}
        		}
        	}
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            Button btnOk3 = fetchElement("Activate_Funds Release Details_BTN_OK");
            btnOk3.Click();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            
            string pageTitle1 = "Fund Release";
            string message1 = fetchElement("Activate_Funds Release_TXT_Message");
            string btn1 = fetchElement("Activate_Funds Release_BTN_Ok");
            HandlePromptFundReleaseKeyword(pageTitle1, message1, btn1);
        }
        
        private void ElectronicPaymentsBatchGeneration(){
            waitForPagetoAppear("Electronic Payments Batch Generation");
            string sqlQuery = "select  *  from [ACT_Funds_Release_Details] where [Reference] = '"+Main.InputData[0]+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            societyDropdown = dbUtility.GetAccessFieldValue(TestDataConstants.Act_FundsReleaseDetails_Society);
            if(!string.IsNullOrEmpty(societyDropdown)){
                List societySelect = fetchElement("Activate_Electronic Payments Batch Generation_LST_Society");
                ComboboxItemSelectDirect(societySelect, societyDropdown);
           }
            Button btnOKEPBG = fetchElement("Activate_Electronic Payments Batch Generation_BTN_OK");
            btnOKEPBG.Click();
        }
        
        private void GenerateElectronicPayments(){
            waitForPagetoAppear("Generate Electronic Payments");
            string societyNum = "1";
            if(societyDropdown.Equals("Touchstone Financial (Ireland)", StringComparison.OrdinalIgnoreCase)){
                societyNum="2";
            }
            List<string[]> funderSelect=oraUtility.executeQuery("select funder_select from sam05 where socseqno = '"+societyNum+"'");
            funderSelectFlg=funderSelect[0][0];
            string sqlQuery = "select  *  from [ACT_Funds_Release_Details] where [Reference] = '"+Main.InputData[0]+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            string generateElectronicPmtRefDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_FundsReleaseDetails_GenerateElectronicPmtRef);
            if(!string.IsNullOrEmpty(generateElectronicPmtRefDB) && funderSelectFlg.ToUpper().Equals("Y")){
                string sqlQuery2 = "select  *  from [ACT_GenerateElectronicPayments] where [Reference] = '"+generateElectronicPmtRefDB+"'";
                dbUtility.ReadDBResultMS(sqlQuery2);
                string funderRefDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_GenerateElectronicPayments_Funder);
                if(Main.InputData.Count==2 && "NEW".Equals(funderRefDB.ToUpper())){
                    funderRefDB=Main.InputData[1];
                    RxPath funderRx = fetchElement("Activate_Generate_Electronic_Payments_LST_Funder");
                    selectValueListDropDown(funderRx,funderRefDB);
                }
            }
            Button btnOKGEP = fetchElement("Activate_Generate Electronic Payments_BTN_OK");
            btnOKGEP.Click();
            string pageTitle2 = "Release Funds";
            string message2 = fetchElement("Activate_Release Funds_TXT_Message");
            string btn2 = fetchElement("Activate_Release Funds_BTN_Yes");
            HandlePromptFundReleaseKeyword(pageTitle2, message2, btn2);
        }
        
        private void ElectronicPaymentsBatchProduction(){
            waitForPagetoAppear("Electronic Payments Batch Production");
            string sqlQuery = "select  *  from [ACT_Funds_Release_Details] where [Reference] = '"+Main.InputData[0]+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            societyDropdown = dbUtility.GetAccessFieldValue(TestDataConstants.Act_FundsReleaseDetails_Society);
            if(!string.IsNullOrEmpty(societyDropdown)){
                List societySelect = fetchElement("Activate_Electronic Payments Batch Production_LST_Society");
                ComboboxItemSelectDirect(societySelect, societyDropdown);
            }
            Button btnOKEPBP = fetchElement("Activate_Electronic Payments Batch Production_BTN_OK");
            btnOKEPBP.Click();
            if(funderSelectFlg.Equals("Y")){
                CHAPBatchProduction();
            }else{
                string pageTitle3 = string.Empty;
                if(paymentTypeDB.StartsWith("Electronic Transfer", StringComparison.OrdinalIgnoreCase)){
                    pageTitle3 = "Electronic Payment Records Production";
                }else{
                    pageTitle3 = "Electronic Payments Batch File Production";
                }
                string message3 = fetchElement("Activate_Electronic Payments Batch Production_TXT_Prompt_Message");
                string btn3 = fetchElement("Activate_Electronic Payments Batch Production_BTN_Prompt_Ok");
                HandlePromptFundReleaseKeyword(pageTitle3, message3, btn3);
            }
        }
        
        private void CHAPBatchProduction(){
            waitForPagetoAppear("CHAPS Batch File Production");
            RxPath valRx = fetchElement("Activate_CHAPS_Batch_File_Production_BTN_OK");
            Button btnOk = Host.Local.FindSingle(valRx).As<Button>();
            btnOk.Click();
        }
        
        private void PrintingElectronicPaymentsReport(){
            waitForPagetoAppear("Printing Electronic Payments Report");
            Button btnClose = fetchElement("Activate_Printing Electronic Payments Report_BTN_Close");
            btnClose.Click();
        }
        
        private void ValuationEnquiry(){
            waitForPagetoAppear("- Valuation Report - Enquiry");
            Button btnClose = fetchElement("Activate_Valuation Report Enquiry_BTN_Close");
            btnClose.Click();
        }
        
        private void ValuationHistory(){
            waitForPagetoAppear("- Valuation History");
            Button btnOk = fetchElement("Activate_Valuation History_BTN_Ok");
            btnOk.Click();
        }
    }
}