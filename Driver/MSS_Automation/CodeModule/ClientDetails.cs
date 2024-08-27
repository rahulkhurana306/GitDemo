/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 12/08/2022
 * Time: 15:37
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
    /// Description of ClientDetails.
    /// </summary>
    public partial class Keywords
    {
        private string occupancyRef = string.Empty;
        private string bankDetailRef = string.Empty;
        private string pidRef = string.Empty;
        private string empRef = string.Empty;
        private string creditRef = string.Empty;
        string occupancyDB = string.Empty;
        string selfEmployedDB = string.Empty;
        string selfEmployedIncomeDB = string.Empty;
        string companyNameDB = string.Empty;
        string employedRefDB = string.Empty;
        string incomeDetailsRefDB = string.Empty;
        string bankruptcyDateDB = string.Empty;
        string dateBankruptcyDischargedDB = string.Empty;
        string noYearsBankruptcyDischargedDB = string.Empty;
        string bankruptcyAmountDB = string.Empty;
        string FMP_date_arrears_repaidDB = string.Empty;
        string FMP_tot_mort_arrearsDB = string.Empty;
        string FMP_no_mths_arrearsDB = string.Empty;
        string FMP_date_highest_arrearsDB = string.Empty;
        string FMP_worsening_arrearsDB = string.Empty;
        string FMP_arrears_last_6_mthsDB = string.Empty;
        string FMP_arrears_last_6_mths_valDB = string.Empty;
        string FMP_arrears_last_12_mthsDB = string.Empty;
        string FMP_arrears_last_12_mths_valDB = string.Empty;
        string FMP_arrears_last_24_mthsDB = string.Empty;
        string FMP_arrears_last_24_mths_valDB = string.Empty;
        string FMP_arrears_over_24_mthsDB = string.Empty;
        string FMP_PropertyRepossessedDB = string.Empty;
        string FMP_property_repo_dateDB = string.Empty;
        string FMP_cli_loan_arrears_repaid_dDB = string.Empty;
        string FMP_cli_current_loan_arrearsDB = string.Empty;
        string FMP_cli_no_months_in_arrearsDB = string.Empty;
        string FMP_cli_date_of_hgst_arrearsDB = string.Empty;
        string FMP_cli_arrs_worsened_last_6DB = string.Empty;
        string FMP_cli_hgst_mths_arrs_prev_2DB = string.Empty;
        string FMP_cli_hgst_val_arrs_prev_2DB = string.Empty;
        string numberofCCJsDB = string.Empty;
        string dateofLastCCJDB = string.Empty;
        string amountPayableForLastCCJDB = string.Empty;
        string dateRepaidCCJDB = string.Empty;
        string CCJssatisfiedDB = string.Empty;
        string MAWC_DateArrangementsMadeDB = string.Empty;
        string MAWC_failedToMeetTermsArrangementDB = string.Empty;
        string DOCA_no_credit_accDB = string.Empty;
        string DOCA_no_credit_defaultsDB = string.Empty;
        string DOCA_credit_arrears_6_mthsDB = string.Empty;
        string DOCA_credit_arrears_12_mthsDB = string.Empty;
        string DOCA_credit_arrears_24_mthsDB = string.Empty;
        string DOCA_tot_val_creditDB = string.Empty;
        string DOCA_mth_pay_credit_accDB = string.Empty;
        string DOCA_ivasDB = string.Empty;
        string DOCA_ivas_clearedDB = string.Empty;
        string DOCA_cli_latest_ivas_start_datDB = string.Empty;
        string DOCA_cli_has_debt_relief_orderDB = string.Empty;
        string DOCA_cli_debt_relief_orders_clDB = string.Empty;
        string DOCA_cli_debt_relief_ordrs_staDB = string.Empty;
        string expectedRetirementDateDB = string.Empty;
        
        public List<string> ClientDetails()
        {
            try{
                Main.appFlag = Constants.appActivate;
                OpenMacro(TestDataConstants.Act_ClientDetails_MacroName);
                ClientDetailsFunctionality();
                OccupancyDetails();
                ClientBankAccounts();
                ProofOfID();
                ClientEmploymentDetails();
                CreditInformation();
                //waitForPagetoAppear(TestDataConstants.Page_TopMenu);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                string clientNumber = string.Empty;
                if(!FAFlag){
                    clientNumber = fetchClientNumFromDB();
                }
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                if(!FAFlag){
                    Main.OutputData.Add(clientNumber);
                }
                return Main.OutputData;
            }catch(Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void OpenMacro(string strMenu){
            string fieldRx = fetchElement("Activate_Macro_Editor_Menubar_Menu_Field");
            MenuItem field = fieldRx;
            if(field.Visible){
                field.Click();
            }else{
                Report.Error("MenuItem Field is not Clickable");
            }
            Text txtMacro = fetchElement("Activate_Macro_Editor_TXT_Macro");
            setText(txtMacro, strMenu);
            Button btnOk = fetchElement("Activate_Macro_Editor_BTN_OK");
            btnOk.Click();
        }
        
        private void ClientDetailsFunctionality(){
            if(!FAFlag){
                string sqlQuery = "select  *  from [ACT_Client_Details] where [Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string custTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_CustType);
                string applicantTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_ApplicantType);
                string titleDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Title);
                string forenameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Forename);
                string surnameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Surname);
                string fullNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_FullName);
                string alphaDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Alpha);
                string dateOfBirthDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_DateOfBirth);
                string niCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_NINumber);
                string reasonForNoNIDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_ReasonForNoNI);
                string maritalStatusDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_MaritalStatus);
                string sexDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Sex);
                string deliveryPrefDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_DeliveryPref);
                string clientStatusDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_ClientStatus);
                string nationalityDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Nationality);
                string residentInUKDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_ResidentInUK);
                string countryOfResidenceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_CountryOfResidence);
                string countryOfRegistrationDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_CountryOfRegistration);
                string firstTimeLandlordDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_FirstTimeLandlord);
                string mobileDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Mobile);
                string homeTelDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_HomeTel);
                string faxNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_FaxNo);
                string emailDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Email);
                string mailshotDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_Mailshot);
                occupancyRef = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_occupancy_ref);
                bankDetailRef = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_bank_detail_ref);
                pidRef = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_pid_ref);
                empRef = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_emp_ref);
                creditRef = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientDetails_credit_ref);
                
                //waitForPagetoAppear("- Applicant Details");
                if(TOEFlag){
                    //get the client number for existing client
                    string clientNumExisting = fetchClientNumFromDBTOE();
                    //for the given client number update the client_status and current_statua field
                    string updateQueryAFM = "UPDATE acf02 SET CLISTATUS='PO',TOFESTATUS='R' where CLINUM='"+clientNumExisting+"'";
                    oraUtility.executeQuery(updateQueryAFM);
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                }
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                Button btnAdd = fetchElement("Activate_Applicant Details_BTN_Add");
                btnAdd.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button btnNewClient = fetchElement("Activate_Client Search_BTN_New_Client");
                btnNewClient.Click();
                //waitForPagetoAppear("Client Details");
                ComboBox cmbxCustType = fetchElement("Activate_Client_Details_LST_Cust_Type");
                ComboboxItemSelectDirect(cmbxCustType, custTypeDB);
                ComboBox applicantType = fetchElement("Activate_Client_Details_LST_Applicant_Type");
                ComboboxItemSelectDirect(applicantType, applicantTypeDB);
                if(!string.IsNullOrEmpty(titleDB)){
                    if(!titleDB.ToLower().Equals("default")){
                        List title = fetchElement("Activate_Client_Details_LST_Title");
                        ComboboxItemSelect(title, titleDB);
                        Keyboard.Press("{Tab}");
                    }
                }
                if(!string.IsNullOrEmpty(forenameDB)){
                    if(!forenameDB.ToLower().Equals("default")){
                        Text forename = fetchElement("Activate_Client_Details_TXT_Forenames");
                        setText(forename, forenameDB);
                    }
                }
                if(!string.IsNullOrEmpty(surnameDB)){
                    if(!surnameDB.ToLower().Equals("default")){
                        Text surname = fetchElement("Activate_Client_Details_TXT_Surname");
                        setText(surname, surnameDB);
                    }
                }
                if(!string.IsNullOrEmpty(fullNameDB)){
                    if(!fullNameDB.ToLower().Equals("default")){
                        Text fullname = fetchElement("Activate_Client_Details_TXT_Full_Name");
                        setText(fullname, fullNameDB);
                    }
                }
                
                if(!string.IsNullOrEmpty(alphaDB)){
                    if(!alphaDB.ToLower().Equals("default")){
                        Text alpha = fetchElement("Activate_Client_Details_TXT_Alpha");
                        setText(alpha, alphaDB);
                    }
                }
                
                if(!string.IsNullOrEmpty(dateOfBirthDB)){
                    if(!dateOfBirthDB.ToLower().Equals("default")){
                        Text datOfBirth = fetchElement("Activate_Client_Details_TXT_Date_Of_Birth");
                        setText(datOfBirth, dateOfBirthDB);
                    }
                }
                
				//MSS 6.0 sex field is a List Drop-down
				RxPath sexRx = fetchElement("Activate_Client_Details_LST_Sex");
				selectValueListDropDown(sexRx, sexDB);
                
                RxPath niNumberRx = fetchElement("Activate_Client_Details_TXT_NI_Number");
                setTextValue(niNumberRx, utility.GenerateUniqueNICode(niCodeDB));
                
                RxPath reasonForNoNIRx = fetchElement("Activate_Client_Details_LST_Reason_For_No_NI");
                selectValueListDropDown(reasonForNoNIRx,reasonForNoNIDB);
                
                if(!string.IsNullOrEmpty(maritalStatusDB)){
                    if(!maritalStatusDB.ToLower().Equals("default")){
                        List maritalStatus = fetchElement("Activate_Client_Details_LST_Marital_Status");
                        ComboboxItemSelect(maritalStatus, maritalStatusDB);
                    }
                }
                
                if(!string.IsNullOrEmpty(deliveryPrefDB)){
                    if(!deliveryPrefDB.ToLower().Equals("default")){
                        List deliveryPref = fetchElement("Activate_Client_Details_LST_Delivery_Pref");
                        ComboboxItemSelectDirect(deliveryPref, deliveryPrefDB);
                    }
                }
                
                if(!string.IsNullOrEmpty(clientStatusDB)){
                    if(!clientStatusDB.ToLower().Equals("default")){
                        List clientStatus = fetchElement("Activate_Client_Details_LST_Client_Status");
                        ComboboxItemSelectDirect(clientStatus, clientStatusDB);
                    }
                }
                
                if(!string.IsNullOrEmpty(nationalityDB)){
                    if(!nationalityDB.ToLower().Equals("default")){
                        List nationality = fetchElement("Activate_Client_Details_LST_Nationality");
                        ComboboxItemSelectE(nationality, nationalityDB);
                    }
                }
                
                if(!string.IsNullOrEmpty(residentInUKDB)){
                    if(!residentInUKDB.ToLower().Equals("default")){
                        Text residentInUK = fetchElement("Activate_Client_Details_TXT_Resident_in_UK");
                        setText(residentInUK, residentInUKDB);
                    }
                }
                
                if(!string.IsNullOrEmpty(countryOfResidenceDB)){
                    if(!countryOfResidenceDB.ToLower().Equals("default")){
                        List countryOfResidence = fetchElement("Activate_Client_Details_LST_Country_of_Residence");
                        ComboboxItemSelectE(countryOfResidence, countryOfResidenceDB);
                    }
                }
                
                if(!string.IsNullOrEmpty(countryOfRegistrationDB)){
                    if(!countryOfRegistrationDB.ToLower().Equals("default")){
                        List countryOfResidence = fetchElement("Activate_Client_Details_LST_Country_of_Residence");
                        ComboboxItemSelectE(countryOfResidence, countryOfRegistrationDB);
                    }
                }
                
                CheckBox firstTimeLandlord = fetchElement("Activate_Client_Details_CBX_First_Time_Landlord");
                checkboxOperation(firstTimeLandlordDB, firstTimeLandlord);
                
                RxPath mobileRx = fetchElement("Activate_Client_Details_TXT_Mobile");
                RxPath homeTelRx = fetchElement("Activate_Client_Details_TXT_Home_Tel");
                RxPath faxNoRx = fetchElement("Activate_Client_Details_TXT_Fax_No");
                RxPath emailRx = fetchElement("Activate_Client_Details_TXT_E_Mail");
                RxPath mailShotRx = fetchElement("Activate_Client_Details_CBX_Mailshot");
                setTextValue(mobileRx, mobileDB);
                setTextValue(homeTelRx, homeTelDB);
                setTextValue(faxNoRx, faxNoDB);
                setTextValue(emailRx, emailDB);
                setCheckbox(mailShotRx, mailshotDB);
                
                Button Ok = fetchElement("Activate_Client_Details_BTN_OK");
                Ok.Click();
                
                //waitForPagetoAppear("- Applicant Details");
            }
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            Button btnOk = fetchElement("Activate_Applicant Details_BTN_OK");
            btnOk.Click();
            
            if(TOEFlag || MVFlag){
                RxPath warningRx = fetchElement("Activate_Application_Warning_User_Confirmation_BTN_Yes");
                ClickButtonRx(warningRx);
            }
            
            if(LMFlag==true){
                RxPath txtMessage = fetchElement("Activate_Mortality_Table_BTN_OK");
                Button btnOK = null;
                Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                if(Host.Local.TryFindSingle(txtMessage, durationTime,out btnOK)){
                    btnOK = Host.Local.FindSingle(txtMessage).As<Button>();
                    btnOK.Click();
                }
            }
            //waitForPagetoAppear("- Occupancy Details");
        }
        
        private void OccupancyDetails(){
            if(!FAFlag){
                if(!string.IsNullOrEmpty(occupancyRef)){
                    string sqlQuery = "select  *  from [ACT_Occupancy_Details] where [Reference] = '"+occupancyRef+"'";
                    dbUtility.ReadDBResultMS(sqlQuery);
                    string clientDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OccupancyDetails_Client);
                    occupancyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OccupancyDetails_Occupancy);
                    
                    ComboBox client = fetchElement("Activate_Occupancy Details_LST_Client");
                    if(!ComboBoxValueCompare(client,clientDB)){
                        ComboboxItemSelectDirectPath(client, clientDB);
                    }
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    Button btnAdd = fetchElement("Activate_Occupancy Details_BTN_Add");
                    btnAdd.Click();
                    
                    
                    List occupancy = fetchElement("Activate_Occupancy Details_LST_Occupancy Type");
                    ListtemSelectDirectPath(occupancy, occupancyDB);
                    ClientsAddressAdd();
                }
            }
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));
                Button btnClose = fetchElement("Activate_Occupancy Details_BTN_Close");
                btnClose.Click();
                //waitForPagetoAppear("Client Bank Accounts");
        }
        
        private void ClientsAddressAdd(){
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            string addressLine1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientAddressAdd_AddressLine1);
            string addressLine2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientAddressAdd_AddressLine2);
            string addressLine3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientAddressAdd_AddressLine3);
            string addressLine4DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientAddressAdd_AddressLine4);
            string addressLine5DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientAddressAdd_AddressLine5);
            string postCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientAddressAdd_PostCode);
            string countryCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientAddressAdd_CountryCode);
            string lengthOfResidenceYrsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientAddressAdd_LengthOfResidenceYrs);
            string ROICountyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OccupancyDetails_ROICounty);
            RxPath addressLine1Rx = fetchElement("Activate_Clients_Address_Add_TXT_Line_1");
            Text addressLine1 = Host.Local.FindSingle(addressLine1Rx, duration).As<Text>();
            Text addressLine2 = fetchElement("Activate_Clients_Address_Add_TXT_Line_2");
            Text addressLine3 = fetchElement("Activate_Clients_Address_Add_TXT_Line_3");
            Text addressLine4 = fetchElement("Activate_Clients_Address_Add_TXT_Line_4");
            Text addressLine5 = fetchElement("Activate_Clients_Address_Add_TXT_Line_5");
            Text postCode = fetchElement("Activate_Clients_Address_Add_TXT_Postcode");
            Text lengthOfResidenceYrs = fetchElement("Activate_Clients_Address_Add_TXT_Years");
            
            setText(addressLine1, addressLine1DB);
            setText(addressLine2, addressLine2DB);
            setText(addressLine3, addressLine3DB);
            setText(addressLine4, addressLine4DB);
            setText(addressLine5, addressLine5DB);
            setText(postCode, postCodeDB);
            if(!countryCodeDB.ToUpper().Equals(TestDataConstants.CountryType_UK)){
                List countryCode = fetchElement("Activate_Clients_Address_Add_LST_Country_Code");
                ComboboxItemSelectE(countryCode, countryCodeDB);
            }
            setText(lengthOfResidenceYrs, lengthOfResidenceYrsDB);
            RxPath ROICountyRx = fetchElement("Activate_Clients_Address_Add_LST_ROI_County");
            selectValueListDropDown(ROICountyRx, ROICountyDB);
            Button btnOk = fetchElement("Activate_Clients_Address_Add_BTN_OK");
            btnOk.Click();
            if(occupancyDB.Equals("Existing Borrower", StringComparison.OrdinalIgnoreCase) || occupancyDB.Equals("Owner Occupier - With Mortgage", StringComparison.OrdinalIgnoreCase)){
                Button btnCancelLender = fetchElement("Activate_Lender Details_BTN_Cancel");
                btnCancelLender.Click();
            }else if (occupancyDB.Equals("Single Tenant", StringComparison.OrdinalIgnoreCase)){
                Button btnCancelLandlord = fetchElement("Activate_Landlord_Address_BTN_Cancel");
                btnCancelLandlord.Click();
            }
            
            //waitForPagetoAppear("- Occupancy Details");
        }
        
        private void ClientBankAccounts(){
            if(!FAFlag){
                string sqlQuery = "select  *  from [ACT_Client_Bank_Accounts] where [Reference] = '"+bankDetailRef+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string clientDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientBankAccounts_Client);
                
                ComboBox client = fetchElement("Activate_Client Bank Accounts_LST_Client");
                if(!ComboBoxValueCompare(client,clientDB)){
                    ComboboxItemSelectDirectPath(client, clientDB);
                }
                Button btnAdd = fetchElement("Activate_Client Bank Accounts_BTN_Add");
                btnAdd.Click();
                
                //waitForPagetoAppear("Customer Bank Details");
                CustomerBankDetails();
                Text bankBranch = fetchElement("Activate_Client Bank Accounts_TXT_BankBranch");
                if(!string.IsNullOrEmpty(bankBranch.Element.GetAttributeValueText("AccessibleValue"))){
                    Report.Success("BankBranch is Active and Displayed.");
                }else{
                    Report.Failure("Bank Branch is not Active or Displayed..");
                }
            }
            RxPath btnClose = fetchElement("Activate_Client Bank Accounts_BTN_Close");
            ClickButtonRx(btnClose);
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            //waitForPagetoAppear("- Proof Of ID");
        }
        
        private void CustomerBankDetails(){
            string sqlQuery = "select  *  from [ACT_Customer_Bank_Details] where [Reference] = '"+bankDetailRef+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            string accountNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CustomerBankDetails_AccountName);
            string accountNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CustomerBankDetails_AccountNo);
            string sortCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CustomerBankDetails_SortCode);
            string acDurationYearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CustomerBankDetails_AcDurationYears);
            string acDurationMonthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CustomerBankDetails_AcDurationMonths);
            string chequeGTeeCardDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CustomerBankDetails_ChequeGTeeCard);
            string cardNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CustomerBankDetails_CardNumber);
            
            Text accountName = fetchElement("Activate_Customer Bank Details_TXT_Account_Name");
            Text accountNo = fetchElement("Activate_Customer Bank Details_TXT_Account_Number");
            Text sortCode = fetchElement("Activate_Customer Bank Details_TXT_Sort_Code");
            Text acDurationYears = fetchElement("Activate_Customer Bank Details_TXT_Years");
            Text acDurationMonths = fetchElement("Activate_Customer Bank Details_TXT_Months");
            
            setText(sortCode, sortCodeDB);
            setText(accountName, accountNameDB);
            setText(accountNo, accountNoDB);
            setText(acDurationYears, acDurationYearsDB);
            setText(acDurationMonths, acDurationMonthsDB);
            if(!string.IsNullOrEmpty(chequeGTeeCardDB)){
                CheckBox chequeGTeeCard = fetchElement("Activate_Customer Bank Details_CBX_Cheque_Gtee_Card");
                checkboxOperation(chequeGTeeCardDB,chequeGTeeCard);
                Text cardNumber = fetchElement("Activate_Customer Bank Details_TXT_Card_Number");
                setText(cardNumber, cardNumberDB);
            }
            Button btnOk = fetchElement("Activate_Customer Bank Details_BTN_OK");
            btnOk.Click();
            
            //waitForPagetoAppear("Client Bank Accounts");
        }
        
        private void ProofOfID(){
            if(!FAFlag){
                string sqlQuery = "select  *  from [ACT_Proof_Of_Id] where [Reference] = '"+pidRef+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string clientDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProofOfID_Client);
                string docClass1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProofOfID_DocClass1);
                string docType1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProofOfID_DocType1);
                string docReference1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProofOfID_DocReference1);
                string docClass2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProofOfID_DocClass2);
                string docType2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProofOfID_DocType2);
                string docReference2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProofOfID_DocReference2);
                List<string> docClassLst = new List<string>();
                docClassLst.Add(docClass1DB);
                docClassLst.Add(docClass2DB);
                List<string> docTypeLst = new List<string>();
                docTypeLst.Add(docType1DB);
                docTypeLst.Add(docType2DB);
                List<string> docReferenceLst = new List<string>();
                docReferenceLst.Add(docReference1DB);
                docReferenceLst.Add(docReference2DB);
                
                ComboBox client = fetchElement("Activate_Proof of ID_LST_Client");
                if(!ComboBoxValueCompare(client,clientDB)){
                    ComboboxItemSelectDirectPath(client, clientDB);
                }
                
                string docClass = fetchElement("Activate_Proof of ID_LST_Document_Class");
                string docType = fetchElement("Activate_Proof of ID_LST_Document_Type");
                string docReference = fetchElement("Activate_Proof of ID_TXT_Document_Reference");
                

                for(int i=1;i<=2;i++){
                	if(!string.IsNullOrEmpty(docClassLst[i-1]) && !string.IsNullOrEmpty(docTypeLst[i-1])){
                		Button btnAdd = fetchElement("Activate_Proof of ID_BTN_Add");
                		btnAdd.Click();
                		
                		List docClassEle = docClass;
                		ComboboxItemSelectDirect(docClassEle, docClassLst[i-1]);
                		
                		List docTypeEle = docType;
                		ComboboxItemSelectE(docTypeEle, docTypeLst[i-1]);
                		
                		Text docReferenceEle = docReference;
                		setText(docReferenceEle, docReferenceLst[i-1]);
                		
                		Button btnAddOk = fetchElement("Activate_Proof_Of_ID_BTN_Add_Ok");
                		btnAddOk.Click();
                	}
                }
        	}
            Button btnOk = fetchElement("Activate_Proof of ID_BTN_OK");
            btnOk.Click();
            
            //waitForPagetoAppear("- Client Employment Details");
        }
        
        private void ClientEmploymentDetails(){
            if(!FAFlag){
                string sqlQuery = "select  *  from [ACT_Client_Employment_Details] where [Reference] = '"+empRef+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string clientDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Client);
                string retiredDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Retired);
                string notEmployedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_NotEmployed);
                selfEmployedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_SelfEmpRef);
                selfEmployedIncomeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_SelfEmpIncomeRef);
                employedRefDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_EmpRef);
                incomeDetailsRefDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_EmpIncomeRef);
                expectedRetirementDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_ExpectedRetirementDate);
                ComboBox client = fetchElement("Activate_Client Employment Details_LST_Client");
                if(!ComboBoxValueCompare(client,clientDB)){
                    ComboboxItemSelectDirectPath(client, clientDB);
                }
                
                if(!string.IsNullOrEmpty(retiredDB)){
                    CheckBox retired = fetchElement("Activate_Client Employment Details_CBX_Retired");
                    checkboxOperation(retiredDB,retired);
                }
                
                if(!string.IsNullOrEmpty(notEmployedDB)){
                    CheckBox notEmployed = fetchElement("Activate_Client Employment Details_CBX_Not Employed");
                    checkboxOperation(notEmployedDB,notEmployed);
                }
                
                if("N".Equals(notEmployedDB.ToUpper())){
                    if(!string.IsNullOrEmpty(employedRefDB)){
                        Button btnAddEmployed = fetchElement("Activate_Client Employment Details_BTN_Add Employed Current");
                        if(btnAddEmployed.Enabled){
                            btnAddEmployed.Click();
                            //waitForPagetoAppear("Employment Details : Employed");
                            EmploymentDetailsEmployed();
                        }
                    }
                    
                    if(!string.IsNullOrEmpty(incomeDetailsRefDB)){
                        Button btnIncomeDetails = fetchElement("Activate_Client Employment Details_BTN_Income Details Current");
                        if(btnIncomeDetails.Enabled){
                            btnIncomeDetails.Click();
                            //waitForPagetoAppear("Income Details");
                            IncomeDetails();
                            RawText yearlyGross = fetchElement("Activate_Client Employment Details_Raw_TXT_YearlyGross");
                            if(!string.IsNullOrEmpty(yearlyGross.RawTextValue)){
                                Report.Success("Employment and Income Details Added Successfully..");
                            }else{
                                Report.Failure("Employment/Income Details NOT added proprerly");
                            }
                        }
                    }
                    
                    if(!string.IsNullOrEmpty(selfEmployedDB)){
                        Button btnSelfEmp = fetchElement("Activate_Client Employment Details_BTN_Add Self Employed Current");
                        btnSelfEmp.Click();
                        EmploymentDetailsSelfEmployed();
                        RawText colExpType = fetchElement("Activate_Client Employment Details_RawTxt_Tbl_Col_Employer_BusinessName");
                        int colNum = colExpType.Column;
                        int colCells = colNum+1;
                        RxPath rxPathElement = fetchElement("Activate_Client Employment Details_Element_Parent_Tbl_Cell")+"rawtext[@column='"+colCells+"' and @row!='0']";
                        SearchAndSelectActivateTableCell(companyNameDB, rxPathElement, 0);
                        if(!string.IsNullOrEmpty(selfEmployedIncomeDB)){
                            Button btnIncomeDetails2 = fetchElement("Activate_Client Employment Details_BTN_Income Details Current");
                            btnIncomeDetails2.Click();
                            IncomeDetailsSelfEmployed();
                        }
                    }
                }
            }
        	
            
            if(!string.IsNullOrEmpty(expectedRetirementDateDB)){
                if(expectedRetirementDateDB.StartsWith("WCAL")){
                    string valDate = utility.ProcessWCALDate(expectedRetirementDateDB,"1");
                    expectedRetirementDateDB=valDate;
                }
                Text txtDate = fetchElement("Activate_Client Employment Details_TXT_Retirement Date");
                txtDate.PressKeys(expectedRetirementDateDB+"{Tab}");
            }
            
            RxPath btnClose = fetchElement("Activate_Client Employment Details_BTN_Close");
            ClickButtonRx(btnClose);
            
            RxPath txtMessage = fetchElement("Activate_Client Employment Details_BTN_Yes");
            Button btnYes = null;
            Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
            if(Host.Local.TryFindSingle(txtMessage, durationTime,out btnYes)){
                btnYes = Host.Local.FindSingle(txtMessage).As<Button>();
                btnYes.Click();
            }
        }
            
        private void EmploymentDetailsEmployed(){
            string sqlQuery = "select  *  from [ACT_Employment_Details_Employed] where [Reference] = '"+employedRefDB+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            string empNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_EmpName);
            string addressLine1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_AddressLine1);
            string addressLine2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_AddressLine2);
            string postCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_PostCode);
            string countryDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_Country);
            string natureOfBusinessDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_NatureOfBusiness);
            string parentPositionDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_ParentPosition);
            string empTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_EmpType);
            string empStatusDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_EmpStatus);
            string occupationDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_Occupation);
            string lengthOfServiceYrsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_LengthOfServiceYrs);
            string deliveryPreferenceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_DeliveryPreference);
            string statusDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_EmploymentDetailsEmployed_Status);
            
            Text empName = fetchElement("Activate_Employment Details Employed_TXT_Employee_Name");
            setText(empName, empNameDB);
            Text addressLine1 = fetchElement("Activate_Employment Details Employed_TXT_Address_Line_1");
            setText(addressLine1, addressLine1DB);
            Text addressLine2 = fetchElement("Activate_Employment Details Employed_TXT_Address_Line_2");
            setText(addressLine2, addressLine2DB);
            Text postCode = fetchElement("Activate_Employment Details Employed_TXT_Postcode");
            setText(postCode, postCodeDB);
            if(!countryDB.ToUpper().Equals(TestDataConstants.CountryType_UK)){
                List country = fetchElement("Activate_Employment Details Employed_LST_Country_Code");
                ComboboxItemSelectE(country,countryDB);
            }
            Text natureOfBusiness = fetchElement("Activate_Employment Details Employed_TXT_Nature_of_Business");
            setText(natureOfBusiness, natureOfBusinessDB);
            Text parentPosition = fetchElement("Activate_Employment Details Employed_TXT_Present_Position");
            setText(parentPosition, parentPositionDB);
            List empType = fetchElement("Activate_Employment Details Employed_LST_Employment_Type");
            ComboboxItemSelect(empType,empTypeDB);
            List empStatus = fetchElement("Activate_Employment Details Employed_LST_Employment_Status");
            ComboboxItemSelect(empStatus,empStatusDB);
            List occupation = fetchElement("Activate_Employment Details Employed_LST_Occupation");
            ComboboxItemSelect(occupation,occupationDB);
            Text lengthOfServiceYrs = fetchElement("Activate_Employment Details Employed_TXT_Years");
            setText(lengthOfServiceYrs, lengthOfServiceYrsDB);
            List deliveryPreference = fetchElement("Activate_Employment Details Employed_LST_Delivery_Preference");
            ComboboxItemSelectDirect(deliveryPreference,deliveryPreferenceDB);
            
            if(statusDB.ToUpper().Equals("CONTRACT")){
                RadioButton status = fetchElement("Activate_Employment Details Employed_RAD_Contract");
                radioButtonSelect(status);
            }else if(statusDB.ToUpper().Equals("PERMANENT")){
                RadioButton status = fetchElement("Activate_Employment Details Employed_RAD_Permanent");
                radioButtonSelect(status);
            }else if(statusDB.ToUpper().Equals("PROBATIONARY")){
                RadioButton status = fetchElement("Activate_Employment Details Employed_RAD_Probationary");
                radioButtonSelect(status);
            }else if(statusDB.ToUpper().Equals("TEMPORARY")){
                RadioButton status = fetchElement("Activate_Employment Details Employed_RAD_Temporary");
                radioButtonSelect(status);
            }else if(statusDB.ToUpper().Equals("NOT KNOWN")){
                RadioButton status = fetchElement("Activate_Employment Details Employed_RAD_Not_Known");
                radioButtonSelect(status);
            }
            
            Button btnOk = fetchElement("Activate_Employment Details Employed_BTN_OK");
            btnOk.Click();
            
        }
        
        private void IncomeDetails(){
            string sqlQuery = "select  *  from [ACT_Income_Details] where [Reference] = '"+incomeDetailsRefDB+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            string incomeTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_IncomeType);
            string incomeAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_IncomeAmount);
            string incomeFrequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_IncomeFrequency);
            string netIncomeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_NetIncome);
            string netIncomeFrequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_NetIncomeFrequency);
            string monthlyNetIncomeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_MonthlyNetIncome);
            
            Button btnAdd = fetchElement("Activate_Income Details_BTN_Add");
            btnAdd.Click();
            
            List incomeType = fetchElement("Activate_Income Details_LST_Income_Type");
            ComboboxItemSelectE(incomeType, incomeTypeDB);
            Text incomeAmount = fetchElement("Activate_Income Details_TXT_Income_Amount");
            setText(incomeAmount, incomeAmountDB);
            ComboBox incomeFrequency = fetchElement("Activate_Income Details_LST_Income_Frequency");
            ComboboxItemSelectDirect(incomeFrequency, incomeFrequencyDB);
            Text netIncome = fetchElement("Activate_Income Details_TXT_Net_Income");
            setText(netIncome, netIncomeDB);
            ComboBox netIncomeFrequency = fetchElement("Activate_Income Details_LST_Net_Income_Frequency");
            ComboboxItemSelectDirect(netIncomeFrequency, netIncomeFrequencyDB);
            RawText monthlyNetIncome = fetchElement("Activate_Income Details_Raw_TXT_MonthlyNetIncome");
            enterRawText(monthlyNetIncome, monthlyNetIncomeDB);
            
            Button btnOk = fetchElement("Activate_Income Details_BTN_OK");
            btnOk.Click();
        }
        
        private void CreditInformation(){
            if(!FAFlag){
                string sqlQuery = "select  *  from [ACT_Credit_Information] where [Reference] = '"+creditRef+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string clientDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_Client);
                string q1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_Q1);
                string q2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_Q2);
                string q3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_Q3);
                string q4DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_Q4);
                string q5DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_Q5);
                string q6DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_Q6);
                
                ComboBox client = fetchElement("Activate_Credit Information_LST_Client");
                if(!ComboBoxValueCompare(client,clientDB)){
                    ComboboxItemSelectDirectPath(client, clientDB);
                }
                
                ComboBox q1 = fetchElement("Activate_Credit Information_LST_1");
                if("YES".Equals(q1DB.ToUpper())){
                    ComboboxItemSelectDirect(q1,q1DB);
                    bankruptcyDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_bankruptcyDate);
                    dateBankruptcyDischargedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_dateBankruptcyDischarged);
                    noYearsBankruptcyDischargedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_noYearsBankruptcyDischarged);
                    bankruptcyAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_bankruptcyAmount);
                    CreditInfoBankruptcy();
                }else{
                    ComboboxItemSelectDirect(q1,q1DB);
                }
                
                ComboBox q2 = fetchElement("Activate_Credit Information_LST_2");
                if("YES".Equals(q2DB.ToUpper())){
                    ComboboxItemSelectDirect(q2,q2DB);
                    FMP_date_arrears_repaidDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_date_arrears_repaid);
                    FMP_tot_mort_arrearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_tot_mort_arrears);
                    FMP_no_mths_arrearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_no_mths_arrears);
                    FMP_date_highest_arrearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_date_highest_arrears);
                    FMP_worsening_arrearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_worsening_arrears);
                    FMP_arrears_last_6_mthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_arrears_last_6_mths);
                    FMP_arrears_last_6_mths_valDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_arrears_last_6_mths_val);
                    FMP_arrears_last_12_mthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_arrears_last_12_mths);
                    FMP_arrears_last_12_mths_valDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_arrears_last_12_mths_val);
                    FMP_arrears_last_24_mthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_arrears_last_24_mths);
                    FMP_arrears_last_24_mths_valDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_arrears_last_24_mths_val);
                    FMP_arrears_over_24_mthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_arrears_over_24_mths);
                    FMP_PropertyRepossessedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_PropertyRepossessed);
                    FMP_property_repo_dateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_property_repo_date);
                    FMP_cli_loan_arrears_repaid_dDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_cli_loan_arrears_repaid_d);
                    FMP_cli_current_loan_arrearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_cli_current_loan_arrears);
                    FMP_cli_no_months_in_arrearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_cli_no_months_in_arrears);
                    FMP_cli_date_of_hgst_arrearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_cli_date_of_hgst_arrears);
                    FMP_cli_arrs_worsened_last_6DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_cli_arrs_worsened_last_6);
                    FMP_cli_hgst_mths_arrs_prev_2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_cli_hgst_mths_arrs_prev_2);
                    FMP_cli_hgst_val_arrs_prev_2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_FMP_cli_hgst_val_arrs_prev_2);
                    CreditInfoFMP();
                }else{
                    ComboboxItemSelectDirect(q2,q2DB);
                }
                
                ComboBox q3 = fetchElement("Activate_Credit Information_LST_3");
                if("YES".Equals(q3DB.ToUpper())){
                    ComboboxItemSelectDirect(q3,q3DB);
                    numberofCCJsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_numberofCCJs);
                    dateofLastCCJDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_dateofLastCCJ);
                    amountPayableForLastCCJDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_amountPayableForLastCCJ);
                    dateRepaidCCJDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_dateRepaidCCJ);
                    CCJssatisfiedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_CCJssatisfied);
                    CreditInfoCCJ();
                }else{
                    ComboboxItemSelectDirect(q3,q3DB);
                }
                
                ComboBox q4 = fetchElement("Activate_Credit Information_LST_4");
                if("YES".Equals(q4DB.ToUpper())){
                    ComboboxItemSelectDirect(q4,q4DB);
                    MAWC_DateArrangementsMadeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_MAWC_DateArrangementsMade);
                    MAWC_failedToMeetTermsArrangementDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_MAWC_failedToMeetTermsArrangement);
                    CreditInfoMAWC();
                }else{
                    ComboboxItemSelectDirect(q4,q4DB);
                }
                
                ComboBox q5 = fetchElement("Activate_Credit Information_LST_5");
                if("YES".Equals(q5DB.ToUpper())){
                    ComboboxItemSelectDirect(q5,q5DB);
                    string creditDeclarationDetailsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_creditDeclarationDetails);
                    CreditInfoCreditDeclarationDetails(creditDeclarationDetailsDB);
                }else{
                    ComboboxItemSelectDirect(q5,q5DB);
                }
                
                ComboBox q6 = fetchElement("Activate_Credit Information_LST_6");
                if("YES".Equals(q6DB.ToUpper())){
                    ComboboxItemSelectDirect(q6,q6DB);
                    DOCA_no_credit_accDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_no_credit_acc);
                    DOCA_no_credit_defaultsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_no_credit_defaults);
                    DOCA_credit_arrears_6_mthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_credit_arrears_6_mths);
                    DOCA_credit_arrears_12_mthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_credit_arrears_12_mths);
                    DOCA_credit_arrears_24_mthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_credit_arrears_24_mths);
                    DOCA_tot_val_creditDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_tot_val_credit);
                    DOCA_mth_pay_credit_accDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_mth_pay_credit_acc);
                    DOCA_ivasDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_ivas);
                    DOCA_ivas_clearedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_ivas_cleared);
                    DOCA_cli_latest_ivas_start_datDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_cli_latest_ivas_start_dat);
                    DOCA_cli_has_debt_relief_orderDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_cli_has_debt_relief_order);
                    DOCA_cli_debt_relief_orders_clDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_cli_debt_relief_orders_cl);
                    DOCA_cli_debt_relief_ordrs_staDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_CreditInformation_DOCA_cli_debt_relief_ordrs_sta);
                    CreditInfoDOCA();
                }else{
                    ComboboxItemSelectDirect(q6,q6DB);
                }
            }
            Button btnOK = fetchElement("Activate_Credit Information_BTN_OK");
            btnOK.Click();
            
        }
        
        private bool ComboBoxValueCompare(ComboBox cmbox, string val){
            return cmbox.Text.StartsWith(val, StringComparison.OrdinalIgnoreCase);
        }
        
        private string fetchClientNumFromDB(){
            string mortNum = Main.InputData[1];
            int len = Main.InputData.Count;
            int seqNo = 1;
            if(len-2!=0){
                seqNo = Int32.Parse(Main.InputData[2]);
            }
            string clientNumQuery = "select CLINUM from acf02 WHERE MORTGAGE = '"+mortNum+"' AND F02SEQNUM = '00"+seqNo+"'";
            List<string[]> data=oraUtility.executeQuery(clientNumQuery );
            string cliNum = data[0][0];
            return cliNum;
        }
        
        private string fetchClientNumFromDBTOE(){
            string mortNum = Main.InputData[1];
            int seqNo = 1;
            string clientNumQuery = "select CLINUM from acf02 WHERE MORTGAGE = '"+mortNum+"' AND F02SEQNUM = '00"+seqNo+"'";
            List<string[]> data=oraUtility.executeQuery(clientNumQuery );
            string cliNum = data[0][0];
            return cliNum;
        }
        
        private void EmploymentDetailsSelfEmployed(){
            string sqlQuery = "select  *  from [ACT_Employment_Details_Self_Employed] where [Reference] = '"+selfEmployedDB+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            companyNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_CompanyName);
            string addressLine1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_AddressLine1);
            string addressLine2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_AddressLine2);
            string addressLine3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_AddressLine3);
            string addressLine4DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_AddressLine4);
            string addressLine5DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_AddressLine5);
            string postCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Postcode);
            string countryDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Country);
            string companyRegNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_CompanyRegNo);
            string vATRegNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_VATRegNo);
            string telephoneNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_TelephoneNo);
            string deliveryPreferenceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_DeliveryPreference);
            string faxNumberDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_FaxNumber);
            string emailAddressDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_EmailAddress);
            string percentageSharesDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_PercentageShares);
            string yearsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Years);
            string monthsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Months);
            string financialYearEndDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_FinancialYearEndDate);
            string noYearsAccountsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_NoYearsAccounts);
            string natureOfBusinessDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_NatureBusiness);
            string empTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_EmploymentType);
            string empStatusDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_EmployerStatus);
            string occupationDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Occupation);
            string statusDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Status);
            string year1Ending1_PDDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Profilt_Year1Ending1_PD);
            string year1Ending2_PDDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Profilt_Year1Ending2_PD);
            string year1Ending3_PDDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Profilt_Year1Ending3_PD);
            string profit1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Profilt_Profit1);
            string profit2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Profilt_Profit2);
            string profit3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Profilt_Profit3);
            string year1Ending1_DDDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Dividend_Year1Ending1_DD);
            string year1Ending2_DDDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Dividend_Year1Ending2_DD);
            string year1Ending3_DDDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Dividend_Year1Ending3_DD);
            string dividend1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Dividend_Dividend1);
            string dividend2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Dividend_Dividend2);
            string dividend3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ClientEmploymentDetails_Self_Dividend_Dividend3);
            

            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_CompanyName", companyNameDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_Address1", addressLine1DB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_Address2", addressLine2DB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_Address3", addressLine3DB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_Address4", addressLine4DB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_Address5", addressLine5DB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_PostCode", postCodeDB);
            selectValueListDropDownBlankDefault("Activate_Employment_Details_Self_Employed_LST_CountryCode", countryDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_CompnyRegstNum", companyRegNoDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_VATRegstNum", vATRegNoDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_TeleNum", telephoneNoDB);
            selectValueListDropDownBlankDefault("Activate_Employment_Details_Self_Employed_LST_DeliveryPref", deliveryPreferenceDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_FaxNum", faxNumberDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_EmailAddr", emailAddressDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_PercentageShares", percentageSharesDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_Yrs", yearsDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_Mnths", monthsDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_FinYrEndDate", financialYearEndDateDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_NoYrsAccount", noYearsAccountsDB);
            setTextBlankDefault("Activate_Employment_Details_Self_Employed_TXT_NatureOfBusiness", natureOfBusinessDB);
            selectValueListDropDownBlankDefault("Activate_Employment_Details_Self_Employed_LST_EmpType", empTypeDB);
            selectValueListDropDownBlankDefault("Activate_Employment_Details_Self_Employed_LST_EmpStatus", empStatusDB);
            selectValueListDropDownBlankDefault("Activate_Employment_Details_Self_Employed_LST_Occupation", occupationDB);
            
            if(statusDB.ToUpper().Equals("DIRECTOR")){
                RadioButton status = fetchElement("Activate_Employment_Details_Self_Employed_RAD_Director");
                radioButtonSelect(status);
            }else if(statusDB.ToUpper().Equals("PARTNER")){
                RadioButton status = fetchElement("Activate_Employment_Details_Self_Employed_RAD_Partner");
                radioButtonSelect(status);
            }else if(statusDB.ToUpper().Equals("SOLE TRADER")){
                RadioButton status = fetchElement("Activate_Employment_Details_Self_Employed_RAD_SoleTrader");
                radioButtonSelect(status);
            }else if(statusDB.ToUpper().Equals("NOT KNOWN")){
                RadioButton status = fetchElement("Activate_Employment_Details_Self_Employed_RAD_NotKnown");
                radioButtonSelect(status);
            }
            
            if(!string.IsNullOrEmpty(year1Ending1_PDDB) || !string.IsNullOrEmpty(year1Ending2_PDDB) || !string.IsNullOrEmpty(year1Ending3_PDDB)){
                Button btnProfit = fetchElement("Activate_Employment_Details_Self_Employed_BTN_PROFITS");
                btnProfit.Click();
                setTextBlankDefault("Activate_SelfEmployed_ProfitDetails_TXT_Year1", year1Ending1_PDDB);
                setTextBlankDefault("Activate_SelfEmployed_ProfitDetails_TXT_Year2", year1Ending2_PDDB);
                setTextBlankDefault("Activate_SelfEmployed_ProfitDetails_TXT_Year3", year1Ending3_PDDB);
                setTextBlankDefault("Activate_SelfEmployed_ProfitDetails_TXT_Year1Profit", profit1DB);
                setTextBlankDefault("Activate_SelfEmployed_ProfitDetails_TXT_Year2Profit", profit2DB);
                setTextBlankDefault("Activate_SelfEmployed_ProfitDetails_TXT_Year3Profit", profit3DB);
                Button btnOkProfit = fetchElement("Activate_SelfEmployed_ProfitDetails_BTN_Ok");
                btnOkProfit.Click();
            }
            
            if(!string.IsNullOrEmpty(year1Ending1_DDDB) || !string.IsNullOrEmpty(year1Ending2_DDDB) || !string.IsNullOrEmpty(year1Ending3_DDDB)){
                Button btnDividend = fetchElement("Activate_Employment_Details_Self_Employed_BTN_DIVIDENDS");
                btnDividend.Click();
                setTextBlankDefault("Activate_SelfEmployed_DividendDetails_TXT_Year1", year1Ending1_DDDB);
                setTextBlankDefault("Activate_SelfEmployed_DividendDetails_TXT_Year2", year1Ending2_DDDB);
                setTextBlankDefault("Activate_SelfEmployed_DividendDetails_TXT_Year3", year1Ending3_DDDB);
                setTextBlankDefault("Activate_SelfEmployed_DividendDetails_TXT_Year1Profit", dividend1DB);
                setTextBlankDefault("Activate_SelfEmployed_DividendDetails_TXT_Year2Profit", dividend2DB);
                setTextBlankDefault("Activate_SelfEmployed_DividendDetails_TXT_Year3Profit", dividend3DB);
                Button btnOkDividend = fetchElement("Activate_SelfEmployed_DividendDetails_BTN_Ok");
                btnOkDividend.Click();
            }
            
            Button btnOk = fetchElement("Activate_Employment_Details_Self_Employed_BTN_OK");
            btnOk.Click();
            
        }
        
        private void IncomeDetailsSelfEmployed(){
            string sqlQuery = "select  *  from [ACT_Income_Details] where [Reference] = '"+selfEmployedIncomeDB+"'";
            dbUtility.ReadDBResultMS(sqlQuery);
            string incomeTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_IncomeType);
            string incomeAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_IncomeAmount);
            string incomeFrequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_IncomeFrequency);
            string netIncomeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_NetIncome);
            string netIncomeFrequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_NetIncomeFrequency);
            string monthlyNetIncomeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_IncomeDetails_MonthlyNetIncome);
            
            Button btnAdd = fetchElement("Activate_Income Details_BTN_Add");
            btnAdd.Click();
            
            List incomeType = fetchElement("Activate_Income Details_LST_Income_Type");
            ComboboxItemSelectE(incomeType, incomeTypeDB);
            Text incomeAmount = fetchElement("Activate_Income Details_TXT_Income_Amount");
            setText(incomeAmount, incomeAmountDB);
            ComboBox incomeFrequency = fetchElement("Activate_Income Details_LST_Income_Frequency");
            ComboboxItemSelectDirect(incomeFrequency, incomeFrequencyDB);
            Text netIncome = fetchElement("Activate_Income Details_TXT_Net_Income");
            setText(netIncome, netIncomeDB);
            ComboBox netIncomeFrequency = fetchElement("Activate_Income Details_LST_Net_Income_Frequency");
            ComboboxItemSelectDirect(netIncomeFrequency, netIncomeFrequencyDB);
            RawText monthlyNetIncome = fetchElement("Activate_Income Details_Raw_TXT_MonthlyNetIncome");
            enterRawText(monthlyNetIncome, monthlyNetIncomeDB);
            
            Button btnOk = fetchElement("Activate_Income Details_BTN_OK");
            btnOk.Click();
        }
        
        private void setTextBlankDefault(string key, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    Text txt = fetchElement(key);
                    setText(txt, val);
                }
            }
        }
        
        private void selectValueListDropDownBlankDefault(string key, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    List lst = fetchElement(key);
                    ListtemSelectDirectPath(lst, val);
                }
            }
        }
        
        private void CreditInfoBankruptcy(){
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            if(!string.IsNullOrEmpty(bankruptcyDateDB)){
                Keyboard.Press(bankruptcyDateDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            if(!string.IsNullOrEmpty(dateBankruptcyDischargedDB)){
                Keyboard.Press(dateBankruptcyDischargedDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            if(!string.IsNullOrEmpty(noYearsBankruptcyDischargedDB)){
                Keyboard.Press(noYearsBankruptcyDischargedDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            if(!string.IsNullOrEmpty(bankruptcyAmountDB)){
                Keyboard.Press(bankruptcyAmountDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            Button btnOK = fetchElement("Activate_Credit Information_BTN_Bankruptcy_OK");
            btnOK.Click();
        }
        
        private void CreditInfoFMP(){
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            setDateValue("Activate_Credit Information_TXT_FMP_date_arrears_repaid", FMP_date_arrears_repaidDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_tot_mort_arrears", FMP_tot_mort_arrearsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_no_mths_arrears", FMP_no_mths_arrearsDB);
            setDateValue("Activate_Credit Information_TXT_FMP_date_highest_arrears", FMP_date_highest_arrearsDB);
            RxPath rxPath1 = fetchElement("Activate_Credit Information_COMBX_FMP_worsening_arrears");
            selectComboboxValue(rxPath1, FMP_worsening_arrearsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_arrears_last_6_mths", FMP_arrears_last_6_mthsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_arrears_last_6_mths_val", FMP_arrears_last_6_mths_valDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_arrears_last_12_mths", FMP_arrears_last_12_mthsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_arrears_last_12_mths_val", FMP_arrears_last_12_mths_valDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_arrears_last_24_mths", FMP_arrears_last_24_mthsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_arrears_last_24_mths_val", FMP_arrears_last_24_mths_valDB);
            RxPath rxPath2 = fetchElement("Activate_Credit Information_COMBX_FMP_arrears_over_24_mths");
            selectComboboxValue(rxPath2, FMP_arrears_over_24_mthsDB);
            RxPath rxPath3 = fetchElement("Activate_Credit Information_CHKBOX_FMP_PropertyRepossessed");
            setCheckbox(rxPath3, FMP_PropertyRepossessedDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_property_repo_date", FMP_property_repo_dateDB);
            setDateValue("Activate_Credit Information_TXT_FMP_cli_loan_arrears_repaid_d", FMP_cli_loan_arrears_repaid_dDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_cli_current_loan_arrears", FMP_cli_current_loan_arrearsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_cli_no_months_in_arrears", FMP_cli_no_months_in_arrearsDB);
            setDateValue("Activate_Credit Information_TXT_FMP_cli_date_of_hgst_arrears", FMP_cli_date_of_hgst_arrearsDB);
            RxPath rxPath4 = fetchElement("Activate_Credit Information_COMBX_FMP_cli_arrs_worsened_last_6");
            selectComboboxValue(rxPath4, FMP_cli_arrs_worsened_last_6DB);
            
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_cli_hgst_mths_arrs_prev_2", FMP_cli_hgst_mths_arrs_prev_2DB);
            setTextBlankDefault("Activate_Credit Information_TXT_FMP_cli_hgst_val_arrs_prev_2", FMP_cli_hgst_val_arrs_prev_2DB);
            
            Button btnOK = fetchElement("Activate_Credit Information_BTN_FMP_OK");
            btnOK.Click();
        }
        
        private void CreditInfoCCJ(){
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            if(!string.IsNullOrEmpty(numberofCCJsDB)){
                Keyboard.Press(numberofCCJsDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            if(!string.IsNullOrEmpty(dateofLastCCJDB)){
                Keyboard.Press(dateofLastCCJDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            if(!string.IsNullOrEmpty(amountPayableForLastCCJDB)){
                Keyboard.Press(amountPayableForLastCCJDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            if(!string.IsNullOrEmpty(dateRepaidCCJDB)){
                Keyboard.Press(dateRepaidCCJDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            if(!string.IsNullOrEmpty(CCJssatisfiedDB)){
                Keyboard.Press(CCJssatisfiedDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            Button btnOK = fetchElement("Activate_Credit Information_BTN_CCJ_OK");
            btnOK.Click();
        }
        
        private void CreditInfoMAWC(){
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            if(!string.IsNullOrEmpty(MAWC_DateArrangementsMadeDB)){
                Keyboard.Press(MAWC_DateArrangementsMadeDB);
                Keyboard.Press("{Tab}");
            }else{
                Keyboard.Press("{Tab}");
            }
            
            if(!string.IsNullOrEmpty(MAWC_failedToMeetTermsArrangementDB)){
                if("YES".Equals(MAWC_failedToMeetTermsArrangementDB.ToUpper())){
                    Keyboard.Press("{Down}");
                }else if("NO".Equals(MAWC_failedToMeetTermsArrangementDB.ToUpper())){
                    Keyboard.Press("{Down}");
                    Keyboard.Press("{Down}");
                }
            }
            
            Button btnOK = fetchElement("Activate_Credit Information_BTN_MAWC_OK");
            btnOK.Click();
        }
        
        private void CreditInfoCreditDeclarationDetails(string credtiDeclarationText){
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            setTextBlankDefault("Activate_Credit Information_TXT_CreditDeclarationDetails", credtiDeclarationText);
            Button btnOK = fetchElement("Activate_Credit Information_BTN_CreditDeclarationDetails_Ok");
            btnOK.Click();
        }
        
        private void CreditInfoDOCA(){
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_no_credit_acc", DOCA_no_credit_accDB);
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_no_credit_defaults", DOCA_no_credit_defaultsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_credit_arrears_6_mths", DOCA_credit_arrears_6_mthsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_credit_arrears_12_mths", DOCA_credit_arrears_12_mthsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_credit_arrears_24_mths", DOCA_credit_arrears_24_mthsDB);
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_tot_val_credit", DOCA_tot_val_creditDB);
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_mth_pay_credit_acc", DOCA_mth_pay_credit_accDB);
            
            RxPath rxPath1 = fetchElement("Activate_Credit Information_COMBX_DOCA_ivas");
            selectComboboxValue(rxPath1, DOCA_ivasDB);
            
            RxPath rxPath2 = fetchElement("Activate_Credit Information_COMBX_DOCA_ivas_cleared");
            selectComboboxValue(rxPath2, DOCA_ivas_clearedDB);
            
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_cli_latest_ivas_start_dat", DOCA_cli_latest_ivas_start_datDB);
            
            RxPath rxPath3 = fetchElement("Activate_Credit Information_COMBX_DOCA_cli_has_debt_relief_order");
            selectComboboxValue(rxPath3, DOCA_cli_has_debt_relief_orderDB);
            RxPath rxPath4 = fetchElement("Activate_Credit Information_COMBX_DOCA_cli_debt_relief_orders_cl");
            selectComboboxValue(rxPath4, DOCA_cli_debt_relief_orders_clDB);
            
            setTextBlankDefault("Activate_Credit Information_TXT_DOCA_cli_debt_relief_ordrs_sta", DOCA_cli_debt_relief_ordrs_staDB);

            Button btnOK = fetchElement("Activate_Credit Information_BTN_DOCA_OK");
            btnOK.Click();
        }
        
        private void setDateValue(string key, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    Text txt = fetchElement(key);
                    txt.PressKeys(val);
                    txt.PressKeys("{Tab}");
                }
            }
        }
    }
}
