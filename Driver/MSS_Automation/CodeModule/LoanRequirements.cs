/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 29/08/2022
 * Time: 17:07
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
	/// Description of LoanRequirements.
	/// </summary>
	public partial class Keywords
	{
	    string newBorrowingsDB = string.Empty;
	    string purchasePriceDB = string.Empty;
	    string borrowingLimitDB = string.Empty;
	    string statusCheckedDB = string.Empty;
	    string incomeVerifiedDB = string.Empty;
	    string verifiedByDB = string.Empty;
	    string overrideReasonDB = string.Empty;
	    string customerTypeDB = string.Empty;
	    string repaymentBasisDB = string.Empty;
	    string affordabilityTypeDB = string.Empty;
	    string estimatedValueDB = string.Empty;
	    string stageReleaseLRDB = string.Empty;
	    string productsToAddDB = string.Empty;
	    string singleInput = String.Empty;
	    int inputLenLR = 0;
	    List<string> prodTypeInputList = null;
	    
	    public List<string> LoanRequirements()
	    {
	        try{
	            Main.appFlag = Constants.appActivate;
	            OpenMacro(TestDataConstants.Act_LoanRequirements_Macro);
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
	            LoanRequirementsFill();
	            MortgageDetailsFill();
	            AccessibleIncome();
	            AffordabilityAssessmentRequired();
	            
	           
	            //waitForPagetoAppear(TestDataConstants.Page_TopMenu);
	            
	            Main.OutputData.Add(Constants.TS_STATUS_PASS);
	            return Main.OutputData;
	            
	        }catch(Exception e){
	            Main.OutputData.Add(Constants.TS_STATUS_FAIL);
	            Main.OutputData.Add(e.Message);
	            return Main.OutputData;
	        }
	    }
	    
	    private void SelectRawTextValue(string textValue){
	        Keyboard.Press(textValue.Split(' ')[0]);
	        RawText val = "/form[@title='']/rawtext[@RawText='"+textValue+"']";
	        val.Click();
	    }
	    
	    private IList<Element> findListElements(RxPath rxPath){
	        IList<Element> listElements = Host.Local.Find(rxPath);
	        return listElements;
	    }
	    
	    
	    private void ExistingProductUpdate(){
	        string productsToAddDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirementBorrowings);
	        string[] products = productsToAddDB.Split(',');
	        int prodCount = products.Length;
	        string[] borrowingIODB = new String[prodCount];
	        string[] borrowingCIDB = new String[prodCount];
	        string[] productCodeDBAr = new String[prodCount];
	        for(int i=0; i<prodCount; i++){
	            string sqlQueryProd = "select  *  from [ACT_Loan_Requirements_Borrowing] where [Reference] = '"+products[i]+"'";
	            dbUtility.ReadDBResultMS(sqlQueryProd);
	            productCodeDBAr[i] = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_ProductCode);
	            string productCodeDB = productCodeDBAr[i];
	            string loanAmtDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_LoanAmt);
	            string termYrsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_TermYrs);
	            string termMonthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_TermMonths);
	            string loanPurposeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_LoanPurpose);
	            borrowingIODB[i] = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_IO);
	            borrowingCIDB[i] = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_CI);
	            string regAuthDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_RegAuth);
	            if(!string.IsNullOrEmpty(productCodeDB)){
	                if(!productCodeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    RxPath rxpath1 = fetchElement("Activate_Loan Requirements_TXT_LoanReq_ProductCode");
	                    IList<Element> productCodes = findListElements(rxpath1);
	                    Text productCode = productCodes[i].As<Text>();
	                    string txtValue = productCode.TextValue;
	                    if(!string.IsNullOrEmpty(txtValue)){
	                        productCode.Click();
	                        productCode.PressKeys("{Delete}");
	                    }
	                    if(inputLenLR==2 && "NEW".Equals(productCodeDB.ToUpper())){
	                        productCodeDB=singleInput;
	                    }else if(inputLenLR>2){
	                        if("NEW".Equals(productCodeDB.ToUpper()) && !string.IsNullOrEmpty(prodTypeInputList[i])){
	                            productCodeDB=prodTypeInputList[i];
	                        }
	                    }
	                    enterText(productCode,productCodeDB);
	                    //check for Conditional Flexible Pop-Up Message and handle it
	                    RxPath btnRx = fetchElement("Activate_Application_Warning_Flexible_to_Non_Flexible_Mortgage_BTN_Ok");
	                    Button btnOk = null;
	                    Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	                    if(Host.Local.TryFindSingle(btnRx, durationTime,out btnOk)){
	                        btnOk.Click();
	                    }
	                }
	            }
	            if(!string.IsNullOrEmpty(termYrsDB)){
	                if(!termYrsDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    RxPath rxpath1 = fetchElement("Activate_Loan Requirements_TXT_LoanReq_Yrs");
	                    IList<Element> years = findListElements(rxpath1);
	                    Text txt = years[i].As<Text>();
	                    enterText(txt, termYrsDB);
	                }
	            }
	            
	            if(!string.IsNullOrEmpty(termMonthsDB)){
	                if(!termMonthsDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    RxPath rxpath1 = fetchElement("Activate_Loan Requirements_TXT_LoanReq_Months");
	                    IList<Element> months = findListElements(rxpath1);
	                    Text txt2 = months[i].As<Text>();
	                    enterText(txt2, termMonthsDB);
	                }
	            }
	            
	            if(!MVFlag){
	                if(!string.IsNullOrEmpty(loanPurposeDB)){
	                    if(!loanPurposeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                        //TODO: need to handle if need to change existing Loanpurpose for Advance-Type:other than MV as for MV its not Editable.
	                        Report.Failure("Code need to be handeld for Non MV Advance-Types");
	                    }
	                }
	            }

	            if(!string.IsNullOrEmpty(borrowingIODB[i])){
	                if(!borrowingIODB[i].ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    RxPath rxpath1 = fetchElement("Activate_Loan Requirements_CBX_LoanReq_IO");
	                    IList<Element> IO = findListElements(rxpath1);
	                    CheckBox cbx1 = IO[i].As<CheckBox>();
	                    checkboxOperationClick(borrowingIODB[i],cbx1);
	                }
	            }
	            if(!string.IsNullOrEmpty(borrowingCIDB[i])){
	                if(!borrowingCIDB[i].ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    RxPath rxpath1 = fetchElement("Activate_Loan Requirements_CBX_LoanReq_CI");
	                    IList<Element> CI = findListElements(rxpath1);
	                    CheckBox cbx2 = CI[i].As<CheckBox>();
	                    checkboxOperationClick(borrowingCIDB[i],cbx2);
	                }
	            }
	            
	            if(!string.IsNullOrEmpty(regAuthDB)){
	                if(!regAuthDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    RxPath rxpath1 = fetchElement("Activate_Loan Requirements_COMBX_LoanReq_RegAuth");
	                    IList<Element> regAuths = findListElements(rxpath1);
	                    ComboBox regAuth = regAuths[i].As<ComboBox>();
	                    ComboboxItemSelectDirect(regAuth, regAuthDB);
	                }
	            }
	        }
	        
	        Button btnOkLoanReq = fetchElement("Activate_Loan Requirements_BTN_OK");
	        btnOkLoanReq.Click();
	        
	        for(int i=0; i<prodCount; i++){
	            if(borrowingIODB[i].ToUpper().Equals("Y") || borrowingCIDB[i].ToUpper().Equals("Y") || !productCodeDBAr[i].ToUpper().Equals("DEFAULT")){
	                   RxPath txtMessage = fetchElement("Activate_Application_Warning_Executing_External_API_Function_BTN_OK");
	                   Button btnOk = null;
	                   Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	                   if(Host.Local.TryFindSingle(txtMessage, durationTime,out btnOk)){
	                       btnOk.Click();
	                   }
	               }
	            }
	    }
	    
	    private void LoanRequirementsFill(){
	        inputLenLR = Main.InputData.Count;
	        if(inputLenLR==2){
	            singleInput = Main.InputData[1];
	        }
	        if(inputLenLR>2){
	            prodTypeInputList = new List<string>();
	            for(int i=0;i<inputLenLR-1;i++){
	                prodTypeInputList.Add(Main.InputData[i+1]);
	            }
	            
	        }
	        string sqlQuery = "select  *  from [ACT_Loan_Requirements] where [Reference] = '"+Main.InputData[0]+"'";
	        dbUtility.ReadDBResultMS(sqlQuery);
	        newBorrowingsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_NewBorrowings);
	        purchasePriceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_PurchasePrice);
	        borrowingLimitDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_BorrowingLimit);
	        string rioTermSuppliedByDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_RIOTermSuppliedBy);
	        statusCheckedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_AssessableIncome_StatusChecked);
	        incomeVerifiedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_AssessableIncome_IncomeVerified);
	        verifiedByDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_AssessableIncome_VerifiedBy);
	        overrideReasonDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_AssessableIncome_OverrideReason);
	        customerTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_AffordabilityCalculation_CustomerType);
	        repaymentBasisDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_AffordabilityCalculation_RepaymentBasis);
	        affordabilityTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_AffordabilityCalculation_AffordabilityType);
	        estimatedValueDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_EstimatedValue);
	        productsToAddDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirementBorrowings);
	        stageReleaseLRDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_StagePayments);
	        //waitForPagetoAppear("- Loan Requirements");
	        if(FAFlag){
	            Button btnBorrowingDets = fetchElement("Activate_Loan Requirements_BTN_BorrowingDetails");
	            btnBorrowingDets.Click();
	            //waitForPagetoAppear("Additional Borrowings Details");
	            Button btnAdd = fetchElement("Activate_Loan Requirements_BTN_ABD_Add");
	            btnAdd.Click();
	            List purpose = fetchElement("Activate_Loan Requirements_LST_ABD_Purpose");
	            ComboboxItemSelectDirect(purpose, "FA");
	            Text amountNewBorrowing = fetchElement("Activate_Loan Requirements_TXT_ABD_AmountNewBorrowing");
	            setText(amountNewBorrowing, newBorrowingsDB);
	            Button btnOk = fetchElement("Activate_Loan Requirements_BTN_ABD_OK");
	            btnOk.Click();
	            Text estimatedValue = fetchElement("Activate_Loan Requirements_TXT_EstimatedValue");
	            setText(estimatedValue, estimatedValueDB);
	        }else if(RMFlag){
	            string existingMtgTransferDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_ExistingMtgTransfer);
	            string amountOrigMortDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_AmountOrigMort);
	            string origPurchPriceDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_OrigPurchPrice);
	            string currentBalanceDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_CurrentBalance);
	            string additionalBorrowingPurposeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_AdditionalBorrowingPurpose);
	            string additionalBorrowingAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_AdditionalBorrowingAmount);
	            string originalMortgageNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_OriginalMortgageNo);
	            if(inputLenLR==2 && "NEW".Equals(originalMortgageNoDB.ToUpper())){
	                originalMortgageNoDB=singleInput;
	            }
	            Button btnBorrowingDets = fetchElement("Activate_Loan Requirements_BTN_BorrowingDetails");
	            btnBorrowingDets.Click();
	            //waitForPagetoAppear("Additional Borrowings Details");
	            Button btnAdd = fetchElement("Activate_Loan Requirements_BTN_ABD_Add");
	            btnAdd.Click();
	            List purpose = fetchElement("Activate_Loan Requirements_LST_ABD_Purpose");
	            ComboboxItemSelectE(purpose, additionalBorrowingPurposeDB);
	            Text amountNewBorrowing = fetchElement("Activate_Loan Requirements_TXT_ABD_AmountNewBorrowing");
	            setText(amountNewBorrowing, additionalBorrowingAmountDB);
	            Button btnOk = fetchElement("Activate_Loan Requirements_BTN_ABD_OK");
	            btnOk.Click();
	            
	            Text estimatedValue = fetchElement("Activate_Loan Requirements_TXT_EstimatedValue");
	            setText(estimatedValue, estimatedValueDB);
	            Text existingMtgTransfer = fetchElement("Activate_Loan Requirements_TXT_ExistingMtgTransfer");
	            setText(existingMtgTransfer, existingMtgTransferDB);
	            Text amountOrigMort = fetchElement("Activate_Loan Requirements_TXT_AmountOrigMort");
	            setText(amountOrigMort, amountOrigMortDB);
	            Text origPurchPrice = fetchElement("Activate_Loan Requirements_TXT_OrigPurchPrice");
	            setText(origPurchPrice, origPurchPriceDB);
	            Text currentBalance = fetchElement("Activate_Loan Requirements_TXT_CurrentBalance");
	            setText(currentBalance, currentBalanceDB);
	            RxPath originalMortgageNoRx = fetchElement("Activate_Loan_Requirements_TXT_OriginalMortgageNo");
	            setTextValue(originalMortgageNoRx,originalMortgageNoDB);
	        }else if(MVFlag || TOEFlag){
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            Text estimatedValue = fetchElement("Activate_Loan Requirements_TXT_EstimatedValue");
	            setText(estimatedValue, estimatedValueDB);
	        }
	        else{
	            Text newBorrowings = fetchElement("Activate_Loan Requirements_TXT_New_Borrowings");
	            setText(newBorrowings,newBorrowingsDB);
	            Text purchasePrice = fetchElement("Activate_Loan Requirements_TXT_Purchase_Price");
	            setText(purchasePrice,purchasePriceDB);
	            //30/01/2023: Changed Combobox to List, as XPath was changed after 5.7 release.
	            if(!string.IsNullOrEmpty(rioTermSuppliedByDB)){
	                if(!rioTermSuppliedByDB.ToLower().Equals("default")){
	                    List RioTermSuppliedBy = fetchElement("Activate_Loan_Requirements_COMBX_RIOTermSuppliedBy");
	                    ComboboxItemSelectDirect(RioTermSuppliedBy, rioTermSuppliedByDB);
	                }
	            }
	        }
	        RxPath borrowingLimitRx = fetchElement("Activate_Loan_Requirements_TXT_Borrowing_Limit");
	        setTextValue(borrowingLimitRx, borrowingLimitDB);
	        
	        if(!string.IsNullOrEmpty(stageReleaseLRDB)){
	            Button btnStagePayments = fetchElement("Activate_Loan_Requirements_BTN_StagePayments");
	            btnStagePayments.Click();
	            string[] stages = stageReleaseLRDB.Split(',');
	            int stageCount = stages.Length;
	            for(int i=0; i<stageCount; i++){
	                Button btnAdd = fetchElement("Activate_Stage_Releases_Proposed_Schedule_BTN_ADD");
	                btnAdd.Click();
	                string sqlQueryStages = "select  *  from [ACT_Stage_Releases_Proposed_Schedule] where [Reference] = '"+stages[i]+"'";
	                dbUtility.ReadDBResultMS(sqlQueryStages);
	                string buildingStageDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_StagePayments_BuildingStage);
	                string advancedSplitDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_LoanRequirement_StagePayments_AdvancedSplit);
	                if(!string.IsNullOrEmpty(buildingStageDB)){
	                    Keyboard.Press(buildingStageDB);
	                    Keyboard.Press("{Tab}");
	                }else{
	                    Keyboard.Press("{Tab}");
	                }	                
	                Keyboard.Press(advancedSplitDB);
	                Keyboard.Press("{Tab}");
	            }
	            Button btnOk = fetchElement("Activate_Stage_Releases_Proposed_Schedule_BTN_OK");
	            btnOk.Click();
	        }
	        
	        if(MVFlag || TOEFlag){
	            ExistingProductUpdate();
	        }else{
	        	string[] products = productsToAddDB.Split(',');
	        	int prodCount = products.Length;
	            for(int i=0; i<prodCount; i++){
	                Button btnAddProduct = fetchElement("Activate_Loan Requirements_BTN_Add");
	                btnAddProduct.Click();
	                string sqlQueryProd = "select  *  from [ACT_Loan_Requirements_Borrowing] where [Reference] = '"+products[i]+"'";
	                dbUtility.ReadDBResultMS(sqlQueryProd);
	                string productCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_ProductCode);
	                string loanAmtDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_LoanAmt);
	                string termYrsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_TermYrs);
	                string termMonthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_TermMonths);
	                string loanPurposeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_LoanPurpose);
	                string borrowingIODB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_IO);
	                string borrowingCIDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_CI);
	                string secondChargeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_2ndCharge);
	                string regAuthDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_LoanDetails_Borrowing_RegAuth);
	                if(inputLenLR==2 && "NEW".Equals(productCodeDB.ToUpper())){
	                    productCodeDB=singleInput;
	                }else if(inputLenLR>2){
	                    if("NEW".Equals(productCodeDB.ToUpper()) && !string.IsNullOrEmpty(prodTypeInputList[i])){
	                        productCodeDB=prodTypeInputList[i];
	                    }
	                }
	                RxPath productCodeRx = fetchElement("Activate_Loan Requirements_TXT_Product_Code");
	                Text productCode = null;
	                Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));
	                if(Host.Local.TryFindSingle(productCodeRx, durationTime,out productCode)){
	                    setText(productCode,productCodeDB);
	                }else{
	                    RawImage rImg = fetchElement("Activate_Loan_Requirements_RAWIMG_CurrentRow");
	                    rImg.Click();
	                    productCode = Host.Local.FindSingle(productCodeRx,duration);
	                    setText(productCode,productCodeDB);
	                }
	                Keyboard.Press(loanAmtDB);
	                Keyboard.Press("{Tab}");
	                if(!string.IsNullOrEmpty(termYrsDB)){
	                    if(!termYrsDB.ToUpper().Equals("DEFAULT")){
	                        Keyboard.Press("{ControlKey down}{AKey}{Delete}{ControlKey up}");
	                        Keyboard.Press(termYrsDB);
	                        Keyboard.Press("{Tab}");
	                    }
	                }else{
	                    Keyboard.Press("{Tab}");
	                }
	                if(!string.IsNullOrEmpty(termMonthsDB)){
	                    if(!termMonthsDB.ToUpper().Equals("DEFAULT")){
	                        Keyboard.Press("{ControlKey down}{AKey}{Delete}{ControlKey up}");
	                        Keyboard.Press(termMonthsDB);
	                        Keyboard.Press("{Tab}");
	                    }
	                }else{
	                    Keyboard.Press("{Tab}");
	                }
	                if(!string.IsNullOrEmpty(loanPurposeDB)){
	                    Keyboard.Press("H");
	                    RxPath rxpath1 = fetchElement("Activate_Loan Requirements_Tbl_Col_RawTxt_Loan_Purpose_HP");
	                    IList<Element> loanPurposeCol = findListElements(rxpath1);
	                    int count = loanPurposeCol.Count;
	                    Report.Info("Adding Loan purpose in row"+ count);
	                    loanPurposeCol[count-2].As<RawText>().Click();
	                    SelectRawTextValue(loanPurposeDB);
	                    Keyboard.Press("{Tab}");
	                }

	                if(borrowingIODB.ToUpper().Equals("Y")){
	                    Keyboard.Press("{Space}");
	                }else if(borrowingCIDB.ToUpper().Equals("Y")){
	                    Keyboard.Press("{Tab}");
	                    Keyboard.Press("{Space}");
	                }
	                
	                RxPath secondChargeRx = fetchElement("Activate_Loan Requirements_RAWTXT_CBX_2ndCharge")+"["+(i+1)+"]";
	                setRawTextCheckbox(secondChargeRx, secondChargeDB);
	                
	                if(!string.IsNullOrEmpty(regAuthDB)){
	                    if(!regAuthDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                        RxPath regAuthRx1 = fetchElement("Activate_Loan Requirements_RawTxt_RegAuthLabel");
	                        IList<RawText> regAuthRawtxt = Host.Local.Find<RawText>(regAuthRx1);
	                        int countActual = regAuthRawtxt.Count;
	                        int index = 0;
	                        if(countActual>i+1){
	                            index = countActual;
	                        }else{
	                            index = i+1;
	                        }
	                        RxPath regAuthRx = fetchElement("Activate_Loan Requirements_RawTxt_RegAuthLabel")+"["+index+"]";
	                        RawText RegAuthLabel = Host.Local.FindSingle(regAuthRx).As<RawText>();
	                        int rowVal = RegAuthLabel.Row;
	                        RxPath rxpath2 = string.Format(fetchElement("Activate_Loan Requirements_RawTxt_RegAuthListField"),index.ToString(),rowVal.ToString());
	                        RawText regAuthField = Host.Local.FindSingle(rxpath2).As<RawText>();
	                        Report.Info("Adding regAuth in row"+ index.ToString());
	                        regAuthField.Click();
	                        SelectRawTextValue(regAuthDB);
	                    }
	                }
	            }
	            
	            Button btnOkLoanReq = fetchElement("Activate_Loan Requirements_BTN_OK");
	            btnOkLoanReq.Click();
	        }
	    }
	    
	    private void MortgageDetailsFill(){
	        try{
	    		Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	    		string sqlQuery2 = "select  *  from [ACT_Mortgage_Details] where [Reference] = '"+Main.InputData[0]+"'";
	    		dbUtility.ReadDBResultMS(sqlQuery2);
	            string DOEPurposeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_DOEPurpose);
	    		if(!FAFlag){
	            string paymentMethodDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_PaymentMethod);
	            string paymentDueDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_PaymentDueDate);
	            string underwritingCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_UnderwritingCode);
	            string holderTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_HolderType);
	            string hoderResidentDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_HoderResident);
	            string mIRASTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_MIRASType);
	            string variationReasonDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_VariationReason);
	            string variationDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_VariationDate);
	            string govtInitiativeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_GovtInitiative);
	            string IORepayVehicleDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_IORepayVehicle);
	            string affinitySchemeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_AffinityScheme);
	            //waitForPagetoAppear(TestDataConstants.Page_MortgageDetails_HP);
	            if(!string.IsNullOrEmpty(paymentMethodDB)){
	                if(!paymentMethodDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    List paymentMethod = fetchElement("Activate_Mortgage Details_LST_PaymentMethod");
	                    ComboboxItemSelect(paymentMethod, paymentMethodDB);
	                }
	            }
	            if(!string.IsNullOrEmpty(paymentDueDateDB)){
	                if(!paymentDueDateDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    ComboBox paymentDueDate = fetchElement("Activate_Mortgage Details_LST_PaymentDueDate");
	                    ComboboxItemSelect(paymentDueDate, paymentDueDateDB);
	                }
	            }
	            if(!string.IsNullOrEmpty(IORepayVehicleDB)){
	                if(!IORepayVehicleDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	                    List IORepayVehicle = fetchElement("Activate_Mortgage_Details_LST_IORepayVehicle");
	                    ComboboxItemSelectE(IORepayVehicle, IORepayVehicleDB);
	                    Keyboard.Press("{Tab}");
	                }
	            }
	            if(!string.IsNullOrEmpty(underwritingCodeDB)){
	                if(!underwritingCodeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    List underwritingCode = fetchElement("Activate_Mortgage Details_LST_UnderwritingCode");
	                    ComboboxItemSelectDirect(underwritingCode, underwritingCodeDB);
	                }
	            }
	            if(!string.IsNullOrEmpty(holderTypeDB)){
	                if(!holderTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    List holderType = fetchElement("Activate_Mortgage Details_LST_HolderType");
	                    ComboboxItemSelect(holderType, holderTypeDB);
	                }
	            }
	            if(!string.IsNullOrEmpty(hoderResidentDB)){
	                if(!hoderResidentDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    List hoderResident = fetchElement("Activate_Mortgage Details_LST_HoderResident");
	                    ComboboxItemSelectDirect(hoderResident, hoderResidentDB);
	                }
	            }
	            if(!string.IsNullOrEmpty(mIRASTypeDB)){
	                if(!mIRASTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    List mIRASType = fetchElement("Activate_Mortgage Details_LST_MIRASType");
	                    ComboboxItemSelectDirect(mIRASType, mIRASTypeDB);
	                }
	            }
	            
	            RxPath govtInitiativeRx = fetchElement("Activate_Mortgage_Details_CBX_GovtInitiative");
	            setCheckbox(govtInitiativeRx, govtInitiativeDB);
	            
	            if(!string.IsNullOrEmpty(variationReasonDB)){
	                if(!variationReasonDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    List variationReason = fetchElement("Activate_Mortgage Details_LST_VariationReason");
	                    ComboboxItemSelectDirect(variationReason, variationReasonDB);
	                    if(!string.IsNullOrEmpty(variationDateDB)){
	                        if(!variationDateDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                            if(variationDateDB.StartsWith("WCAL_DATE", StringComparison.OrdinalIgnoreCase) == true){
	                                string dateValue = utility.ProcessWCALDate(variationDateDB,"1");
	                                Keyboard.Press("{Tab}");
	                                Keyboard.Press(dateValue);
	                            }else{
	                                Report.Error("Date is not in correct format");
	                            }
	                        }
	                    }
	                }
	            }
	            
	            if(!string.IsNullOrEmpty(affinitySchemeDB)){
	                if(!affinitySchemeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                    List affinityScheme = fetchElement("Activate_Mortgage_Details_LST_Affinity_Scheme");
	                    ComboboxItemSelectE(affinityScheme, affinitySchemeDB);
	                }
	            }
	            
	        }
            if(!string.IsNullOrEmpty(DOEPurposeDB)){
            	if(!DOEPurposeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
            		List DOEPurpose = fetchElement("Activate_Mortgage_Details_LST_DOEPurpose");
            		ComboboxItemSelectE(DOEPurpose, DOEPurposeDB);
            	}
            }
	        Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
	        RxPath btnCloseMDRx = fetchElement("Activate_Mortgage Details_BTN_Close");
	        Button btnCloseMD = Host.Local.FindSingle(btnCloseMDRx, duration);
	        btnCloseMD.Click();
	        Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	        }catch(Exception e){
	            throw new Exception(e.Message);
	        }
	    }
	    
	    private void AccessibleIncome(){
	        Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("LONG_WAIT")));
	        waitForPagetoAppear(TestDataConstants.Page_AssessableIncome_HP);
	        if(!string.IsNullOrEmpty(statusCheckedDB)){
	            if(!statusCheckedDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	                CheckBox statusChecked = fetchElement("Activate_Assessable Income_CBX_StatusChecked");
	                checkboxOperation(statusCheckedDB,statusChecked);
	            }
	        }
	        if(!string.IsNullOrEmpty(incomeVerifiedDB)){
	            if(!incomeVerifiedDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	                CheckBox incomeVerified = fetchElement("Activate_Assessable Income_CBX_IncomeVerified");
	                checkboxOperation(incomeVerifiedDB,incomeVerified);
	            }
	        }
	        if(!string.IsNullOrEmpty(verifiedByDB)){
	            if(!verifiedByDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	                ComboBox verifiedBy = fetchElement("Activate_Assessable Income_LST_VerifiedBy");
	                ComboboxItemSelectDirect(verifiedBy, verifiedByDB);
	            }
	        }
	        Button btnOkAI = fetchElement("Activate_Assessable Income_BTN_OK");
	        btnOkAI.Click();
	        Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	    }
	    
	    private void AffordabilityAssessmentRequired(){
	        //waitForPagetoAppear(TestDataConstants.Page_TypeofAffordabilityAssessmentRequired_HP);
	        Report.Info("Affordability Type selection");
	        if(!string.IsNullOrEmpty(affordabilityTypeDB)){
	            if(!affordabilityTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                if(affordabilityTypeDB.StartsWith("Standard",StringComparison.OrdinalIgnoreCase)){
	                    RadioButton rbutton = fetchElement("Activate_Type_of_Affordability_Assessment_Required_RAD_Standard");
	                    rbutton.Click();
	                }else if(affordabilityTypeDB.StartsWith("Unregulated Buy to Let",StringComparison.OrdinalIgnoreCase)){
	                    RadioButton rbutton = fetchElement("Activate_Type_of_Affordability_Assessment_Required_RAD_Unregulated_Buy_To_Let");
	                    rbutton.Click();
	                }
	            }
	        }
	        Report.Info("Customer Type selection");
	        if(!string.IsNullOrEmpty(customerTypeDB)){
	            if(!customerTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                if(customerTypeDB.StartsWith("Borrower/s",StringComparison.OrdinalIgnoreCase)){
	                    RadioButton rbutton = fetchElement("Activate_Type_of_Affordability_Assessment_Required_RAD_Borrowers");
	                    rbutton.Click();
	                }else if(customerTypeDB.StartsWith("Guarantor",StringComparison.OrdinalIgnoreCase)){
	                    RadioButton rbutton = fetchElement("Activate_Type_of_Affordability_Assessment_Required_RAD_Guarantor");
	                    rbutton.Click();
	                }else if(customerTypeDB.StartsWith("Lowest Income Borrower Only",StringComparison.OrdinalIgnoreCase)){
	                    RadioButton rbutton = fetchElement("Activate_Type_of_Affordability_Assessment_Required_RAD_Lowest_Income_Borrower_Only");
	                    rbutton.Click();
	                }
	            }
	        }
	        Report.Info("Repayment basis selection");
	        if(!string.IsNullOrEmpty(repaymentBasisDB)){
	            if(!repaymentBasisDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                if(repaymentBasisDB.StartsWith("Capital and Interest",StringComparison.OrdinalIgnoreCase)){
	                    RadioButton rbutton = fetchElement("Activate_Type_of_Affordability_Assessment_Required_RAD_Capital_And_Investment");
	                    rbutton.Click();
	                }else if(repaymentBasisDB.StartsWith("Actual",StringComparison.OrdinalIgnoreCase)){
	                    RadioButton rbutton = fetchElement("Activate_Type_of_Affordability_Assessment_Required_RAD_Acutal");
	                    rbutton.Click();
	                }else if(repaymentBasisDB.StartsWith("Transitional",StringComparison.OrdinalIgnoreCase)){
	                    RadioButton rbutton = fetchElement("Activate_Type_of_Affordability_Assessment_Required_RAD_Transitional");
	                    rbutton.Click();
	                }
	            }
	            
	        }
	        
	        if(!string.IsNullOrEmpty(overrideReasonDB)){
	            if(!overrideReasonDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                Report.Info("Override Reason selection");
	                List lst = fetchElement("Activate_Type_of_Affordability_Assessment_Required_LST_Override_Reason");
	                ComboboxItemSelectDirect(lst, overrideReasonDB);
	            }
	        }
	        Utility.Capture_Screenshot();
	        Button btnOkTOAAR = fetchElement("Activate_Type of Affordability Assessment Required_BTN_OK");
	        btnOkTOAAR.Click();
	        
	        //waitForPagetoAppear(TestDataConstants.Page_AffordabilityCalculation_HP);
	        Utility.Capture_Screenshot();
	        string affordBtlRequiredCalc = "Select afford_btl_required_calc from sam05 where socseqno = 1";
	        List<string[]> data=oraUtility.executeQuery(affordBtlRequiredCalc);
	        string affordBtl = data[0][0];
	        if(affordBtl.Equals("NONE", StringComparison.OrdinalIgnoreCase)){
	            try{
	                Report.Info("btnOkACMsg--");
	                Button btnOkACMsg = fetchElement("Activate_Affordability_Calculation_BTN_MSG_OK");
	                btnOkACMsg.Click();
	            }catch(Exception e){
	                Report.Info("btnOkAC--"+e.Message);
	                Button btnOkAC = fetchElement("Activate_Affordability Calculation_BTN_OK");
	                btnOkAC.Click();
	            }
	        }else{
	            Button btnOkAC = fetchElement("Activate_Affordability Calculation_BTN_OK");
	            btnOkAC.Click();
	        }
	        Utility.Capture_Screenshot();
	    }
	
	    public List<string> BalanceTransferToExistingSubAccount(){
	        try{
	            Main.appFlag = Constants.appActivate;
	            OpenMacro("NAVLOAN1");
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
	            Button btnBalTransfer = fetchElement("Activate_Loan_Requirements_BTN_BalanceTransfer");
	            btnBalTransfer.Click();
	            string subAcccountToSelect = Main.InputData[0];
	            string AmountToTransfer = Main.InputData[1];
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            string sqlQuery2 = "select  *  from [ACT_Mortgage_Details] where [Reference] = '"+Main.InputData[2]+"'";
	            dbUtility.ReadDBResultMS(sqlQuery2);
	            string variationReasonDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_VariationReason);
	            string variationDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_MortgageDetails_VariationDate);
	            RxPath txtMessage = fetchElement("Activate_Application_Warning_Executing_External_API_Function_BTN_OK");
	            Button btnOk = null;
	            Duration durationTimeExisting = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            if(Host.Local.TryFindSingle(txtMessage, durationTimeExisting,out btnOk)){
	                btnOk.Click();
	            }
	            RxPath prodCodeSelectRx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_CBX_TransferFundsFrom_ProdCodeSelect")+"["+subAcccountToSelect+"]";
	            CheckBox prodCodeSelect = Host.Local.FindSingle(prodCodeSelectRx, duration);
	            prodCodeSelect.Click();
	            
	            //if a row of prodcut already existis
	            RxPath trnsferAmtRx2 = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_TXT_TransferFundsTo_TransferAmount2")+"[-1]";
	            Text trnsferAmt2 = null;
	            Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            if(Host.Local.TryFindSingle(trnsferAmtRx2, durationTime,out trnsferAmt2)){
	                trnsferAmt2 = Host.Local.FindSingle(trnsferAmtRx2).As<Text>();
	                enterText(trnsferAmt2, AmountToTransfer);
	            }else{
	                RxPath trnsferAmtRx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_TXT_TransferFundsTo_TransferAmount");
	                Text trnsferAmt = Host.Local.FindSingle(trnsferAmtRx, duration);
	                enterText(trnsferAmt, AmountToTransfer);
	            }
	            
	            Button btnOK = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_BTN_OK");
	            btnOK.Click();
	            
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            Button btnOkLoanReq = fetchElement("Activate_Loan Requirements_BTN_OK");
	            btnOkLoanReq.Click();
	            HandleAccountTypeWarningMessage();
	            if(!string.IsNullOrEmpty(variationReasonDB)){
	            	if(!variationReasonDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	            		List variationReason = fetchElement("Activate_Mortgage Details_LST_VariationReason");
	            		ComboboxItemSelectDirect(variationReason, variationReasonDB);
	            		if(!string.IsNullOrEmpty(variationDateDB)){
	            			if(!variationDateDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	            				if(variationDateDB.StartsWith("WCAL_DATE", StringComparison.OrdinalIgnoreCase) == true){
	            					string dateValue = utility.ProcessWCALDate(variationDateDB,"1");
	            					Keyboard.Press("{Tab}");
	            					Keyboard.Press(dateValue);
	            				}else{
	            					Report.Error("Date is not in correct format");
	            				}
	            			}
	            		}
	            	}
	            }
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
	            RxPath btnCloseMDRx = fetchElement("Activate_Mortgage Details_BTN_Close");
	            Button btnCloseMD = Host.Local.FindSingle(btnCloseMDRx, duration);
	            btnCloseMD.Click();
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            
	            Main.OutputData.Add(Constants.TS_STATUS_PASS);
	            return Main.OutputData;
	        }catch(Exception e){
	            throw new Exception(e.Message);
	        }
	    }
	    
	    public List<string> BalanceTransferToNewSubAccount(){
	        try{
	            Main.appFlag = Constants.appActivate;
	            int inputCount = Main.InputData.Count;
	            OpenMacro("NAVLOAN1");
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
	            Button btnBalTransfer = fetchElement("Activate_Loan_Requirements_BTN_BalanceTransfer");
	            btnBalTransfer.Click();
	            string sqlQueryMVNewSubAcct = "select  *  from [ACT_BalanceTransfer_NewSubAccount] where [Reference] = '"+Main.InputData[1]+"'";
	            dbUtility.ReadDBResultMS(sqlQueryMVNewSubAcct);
	            string MVBTProdCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_MortgageVariationBalanceTransfer_TrnsferToNew_ProdCode);
	            string MVBTtransferAmount2DB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_MortgageVariationBalanceTransfer_TrnsferToNew_TransferAmount2);
	            string MVBTtermYrsDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_MortgageVariationBalanceTransfer_TrnsferToNew_TermYrs);
	            string MVBTtermMnthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_MortgageVariationBalanceTransfer_TrnsferToNew_TermMnths);
	            string MVBTloanPurposeDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_MortgageVariationBalanceTransfer_TrnsferToNew_LoanPurpose);
	            string MVBTIODB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_MortgageVariationBalanceTransfer_TrnsferToNew_IO);
	            string MVBTCIDB = dbUtility.GetAccessFieldValue(TestDataConstants.ACT_MortgageVariationBalanceTransfer_TrnsferToNew_CI);
	            
	            string subAcccountToSelect = Main.InputData[0];
	            RxPath txtMessage = fetchElement("Activate_Application_Warning_Executing_External_API_Function_BTN_OK");
	            Button btnOk = null;
	            Duration durationTimeNew = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            if(Host.Local.TryFindSingle(txtMessage, durationTimeNew,out btnOk)){
	                btnOk.Click();
	            }
	            RxPath prodCodeSelectRx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_CBX_TransferFundsFrom_ProdCodeSelect")+"["+subAcccountToSelect+"]";
	            CheckBox prodCodeSelect = Host.Local.FindSingle(prodCodeSelectRx, duration);
	            prodCodeSelect.Click();
	            
	            Button btnAdd = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_BTN_ADD");
	            btnAdd.Click();
	            
	            RxPath MVBTprodCodeRx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_TXT_TransferFundsTo_ProdCode")+"[-1]";
	            RxPath MVBTtransferAmount2Rx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_TXT_TransferFundsTo_TransferAmount2")+"[-1]";
	            RxPath MVBTtermYrsRx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_TXT_TransferFundsTo_TermYrs")+"[-1]";
	            RxPath MVBTtermMnthsRx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_TXT_TransferFundsTo_TermMnths")+"[-1]";
	            RxPath MVBTloanPurposeRx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_LST_TransferFundsTo_LoanPurpose")+"[-1]";
	            RxPath MVBTiORx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_CBX_TransferFundsTo_IO")+"[-1]";
	            RxPath MVBTcIRx = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_CBX_TransferFundsTo_CI")+"[-1]";
	            
	            Button btnProducts = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_BTN_Products");
	            btnProducts.Click();
	            if((inputCount==3 || inputCount==5) && "NEW".Equals(MVBTProdCodeDB.ToUpper())){
	                MVBTProdCodeDB= Main.InputData[2];
	            }
	            ProductSearchAndSelect(MVBTProdCodeDB);
	            setTextValue(MVBTtransferAmount2Rx, MVBTtransferAmount2DB);
	            setTextValue(MVBTtermYrsRx, MVBTtermYrsDB);
	            setTextValue(MVBTtermMnthsRx, MVBTtermMnthsDB);
	            selectValueListDropDownNoItemText(MVBTloanPurposeRx,MVBTloanPurposeDB);
	            setCheckbox(MVBTiORx,MVBTIODB);
	            setCheckbox(MVBTcIRx,MVBTCIDB);
	            
	            Button btnOK = fetchElement("Activate_Mortgage_Variation_Balance_Transfers_BTN_OK");
	            btnOK.Click();
	            
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            Button btnOkLoanReq = fetchElement("Activate_Loan Requirements_BTN_OK");
	            btnOkLoanReq.Click();
	            HandleAccountTypeWarningMessage();
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
	            if(inputCount==4 || inputCount==5){
	                string reasonVariation = Main.InputData[inputCount-2];
	                string effectivedate = Main.InputData[inputCount-1];
	                List variationReason = fetchElement("Activate_Mortgage Details_LST_VariationReason");
	                ComboboxItemSelectDirect(variationReason, reasonVariation);
	                if(effectivedate.StartsWith("WCAL", StringComparison.OrdinalIgnoreCase)){
	                    string dateValue = utility.ProcessWCALDate(effectivedate,"1");
	                    Keyboard.Press("{Tab}");
	                    Keyboard.Press(dateValue);
	                }else{
	                    Report.Error("Date is not in correct format");
	                }
	            }
	            RxPath btnCloseMDRx = fetchElement("Activate_Mortgage Details_BTN_Close");
	            Button btnCloseMD = Host.Local.FindSingle(btnCloseMDRx, duration);
	            btnCloseMD.Click();
	            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
	            
	            Main.OutputData.Add(Constants.TS_STATUS_PASS);
	            return Main.OutputData;
	        }catch(Exception e){
	            throw new Exception(e.Message);
	        }
	    }
	    
	    private void ProductSearchAndSelect(string prodCode){
	        Text txtProdCode = fetchElement("Activate_Product_Search_TXT_ProductCode");
	        enterText(txtProdCode, prodCode);
	        Button btnSearch = fetchElement("Activate_Product_Search_BTN_Search");
	        btnSearch.Click();
	        RxPath prodCodeSearched = fetchElement("Activate_Product_Search_TBL_COL_TXT_ProdCode_Row1");
	        Text txtProdeCodeVal = Host.Local.FindSingle(prodCodeSearched, duration);
	        txtProdeCodeVal.Click();
	        Button btnOk = fetchElement("Activate_Product_Search_BTN_OK");
	        btnOk.Click();
	    }
	    
	    private void HandleAccountTypeWarningMessage(){
	        RxPath txtMessage = fetchElement("Activate_Application_Warning_Executing_External_API_Function_BTN_OK");
	        Button btnOk = null;
	        Duration durationTimeNew = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
	        if(Host.Local.TryFindSingle(txtMessage, durationTimeNew,out btnOk)){
	            btnOk.Click();
	        }
	    }
	    
	}
}
