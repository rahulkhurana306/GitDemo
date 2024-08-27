/*
 * Created by Ranorex
 * User: PROJ-MSS-ATEST03.SVC
 * Date: 03/06/2022
 * Time: 11:12
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using System.Data.OleDb;
using ng_mss_automation.CodeModule;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of FunctionalKeywords.
    /// </summary>
    public partial class Keywords
    {

        public DBUtility dbUtility = null;
        public Utility utility = null;
        public OracleUtility oraUtility;
        
        public Keywords()
        {
            dbUtility = new DBUtility();
            utility = Utility.getInstance();
            oraUtility = OracleUtility.Instance();
            
            if(Main.appFlag.Equals(Constants.appActivate)){
                Mouse.DefaultMoveTime = 100;
                Keyboard.DefaultKeyPressTime = 50;
                Delay.SpeedFactor = 1;
            }
        }
        
        
        public List<string> LoginApplication(){
            try{
            if(Main.appFlag.Equals(Constants.appSummit)){
                string inputUser=String.Empty;
                string inputPass=String.Empty;
                if(Main.InputData.Count>0)
                {
                    inputUser=Main.InputData[0];
                    inputPass=Main.InputData[1];
                }
                string defaultUser=Settings.getInstance().get("SUMMIT_ENV_USER");
                string defaultPassword=Settings.getInstance().get("SUMMIT_ENV_PASSWORD");
                Text U_Name;
                Text Passwrd;
                if(Host.Local.Find(fetchElement("Summit_Welcome_To_Summit_TXT_Username"),Duration.FromMilliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")))).Count!=0)
                {
                    U_Name = fetchElement("Summit_Welcome_To_Summit_TXT_Username");
                    Passwrd = fetchElement("Summit_Welcome_To_Summit_TXT_Password");
                    //waitForPagetoLoadCompletely(TestDataConstants.logon);
                    //Delay.Seconds(3);
                    if(String.IsNullOrWhiteSpace(inputUser))
                    {
                        inputUser=defaultUser;
                        inputPass=defaultPassword;
                    }
                    //InputText(U_Name,inputUser);
                    InputText_Advanced(U_Name,inputUser);

                    InputText(Passwrd,inputPass);
                    if(Host.Local.Find<Text>(fetchElement("Summit_Account_Create_TXT_MenuPrompt"),Duration.FromMilliseconds(2 * Int32.Parse(Settings.getInstance().get("LONG_WAIT")))).Count!=0){
                        Main.OutputData.Add("Pass");
                    }
                    else{
                        Main.OutputData.Add("Fail");
                    }
                }else{
                    Main.OutputData.Add("Fail");
                }
            }
            else if(Main.appFlag.Equals(Constants.appActivate)){
                try{
                    RxPath titleBarActivateRx = fetchElement("Activate_Activate Login_TITLE_BAR_SummitActivate");
                    TitleBar titleBarActivate = null;
                    if((Host.Local.TryFindSingle(titleBarActivateRx, duration,out titleBarActivate))){
                        titleBarActivate.Focus();
                        titleBarActivate.Click();
                    }else{
                        throw new Exception("Login Activate not Visible");
                    }
                    LoginActivate();
                    FAFlag = false;
                    RMFlag = false;
                    MVFlag = false;
                    LMFlag = false;
                    TOEFlag = false;
                    notDirectApp = true;
                    Main.OutputData.Add("Pass");
                }catch (Exception ex){
                    Main.OutputData.Add("Fail");
                    Report.Error("Login failed for Activate"+ex.Message);
                }
                
            }
            return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add("Fail");
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> AccountHeader(){
            int inputLen = Main.InputData.Count;
            string addressNumberInput = String.Empty;
            string accountTypeInput = String.Empty;
            string accountNumberFormat = String.Empty;
            if(inputLen>3){
                addressNumberInput = Main.InputData[1];
                accountTypeInput = Main.InputData[2];
                accountNumberFormat = Main.InputData[3];
            }else if(inputLen>2){
                addressNumberInput = Main.InputData[1];
                accountTypeInput = Main.InputData[2];
            }else if(inputLen>1){
                addressNumberInput = Main.InputData[1];
                
            }
            try{
                string sqlQuery = "select  *  from [Account_Header] where [Account Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                MenuPromptInternal("ACHC");
                string accountTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Account_Type).Trim();
                
                
                string globalAccountType = dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Global_Account_Type);
                string ACCNT_NUMBER = utility.GenerateAcctNumber(globalAccountType,accountNumberFormat);
                string ACCOUNT_NUMBER_FORMATTED = null;
                string SOCIETY = null;
                
                Text TXT_Society = fetchElement("Summit_Account_Header_Create_TXT_Society");
                Text TXT_Acc_Type = fetchElement("Summit_Account_Header_Create_TXT_Acc_Type");
                Text TXT_Account_No = fetchElement("Summit_Account_Header_Create_TXT_Account_No");
                Text TXT_Branch = fetchElement("Summit_Account_Header_Create_TXT_Branch");
                Text TXT_Account_Title = fetchElement("Summit_Account_Header_Create_TXT_Account_Title");
                Text TXT_Salutation = fetchElement("Summit_Account_Header_Create_TXT_Salutation");
                Text TXT_Account_NickName = fetchElement("Summit_Account_Header_Create_TXT_Account_Nickname");
                Text TXT_Securitisation = fetchElement("Summit_Account_Header_Create_TXT_Securitisation");
                Text TXT_Account_Holder_Type = fetchElement("Summit_Account_Header_Create_TXT_Account_Holder_Type");
                CheckBox CBX_Inc_In_Sect17 = fetchElement("Summit_Account_Header_Create_CBX_Inc_In_Sect17");
                
                InputText(TXT_Society, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Society));
                
                
                if("NEW".Equals(accountTypeDB)){
                    if(String.IsNullOrEmpty(accountTypeInput))
                    {
                        throw new Exception("DB column says NEW AccountType & However AccountType Input Param is missing");
                    }
                    setText(TXT_Acc_Type,accountTypeInput);
                }else{
                    if(!String.IsNullOrEmpty(accountTypeInput))
                    {
                        throw new Exception("DB column doesn't have NEW & still AccountType Input Param is present.");
                    }
                    setText(TXT_Acc_Type,accountTypeDB);
                }
                
                InputText(TXT_Account_No, ACCNT_NUMBER);
                InputText(TXT_Branch, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Branch));
                InputText(TXT_Account_Title, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Account_Title));
                InputText(TXT_Salutation, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Salutation));
                InputText(TXT_Account_NickName, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Account_NickName));
                InputText(TXT_Securitisation, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Securitisation));
                InputText(TXT_Account_Holder_Type, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_AC_Holder_Type));
                checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Sect_17),CBX_Inc_In_Sect17);
                Text TXT_Rate_Change_Notice_Indicator_0 = fetchElement("Summit_Account_Header_Create_TXT_Rate_Change_Notice_Indicator_0");
                Text TXT_Rate_Change_Notice_Period_0 = fetchElement("Summit_Account_Header_Create_TXT_Rate_Change_Notice_Period_0");
                // Text TXT_Arrears_Code = fetchElement("Summit_Account_Header_Create_TXT_Arrears_Code");
                Text TXT_Credit_Score = fetchElement("Summit_Account_Header_Create_TXT_Credit_Score");
                Text TXT_Account_Rating = fetchElement("Summit_Account_Header_Create_TXT_Account_Rating");
                ComboBox LST_Home_Mover = fetchElement("Summit_Account_Header_Create_LST_Home_Mover");
                
                InputText(TXT_Rate_Change_Notice_Indicator_0, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Rate_Chg_Notice));
                InputText(TXT_Rate_Change_Notice_Period_0, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Rate_Chg_Notice_Period));
                // setText(TXT_Arrears_Code, GetAccessFieldValue("Arrears Code"));
                InputText(TXT_Credit_Score, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Credit_Score));
                InputText(TXT_Account_Rating, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Account_Rating));
                if(dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Home_Mover) != ""){
                    selectValue(LST_Home_Mover, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Home_Mover));
                }
                ComboBox LST_Existing_Borrower = fetchElement("Summit_Account_Header_Create_LST_Existing_Borrower");
                if(dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Existing_Borrower) != ""){
                    selectValue(LST_Existing_Borrower, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Existing_Borrower));
                }
                
                Text TXT_Loan_Purpose = fetchElement("Summit_Account_Header_Create_TXT_Loan_Purpose");
                Text TXT_Holders_Resident = fetchElement("Summit_Account_Header_Create_TXT_Holders_Resident");
                CheckBox CBX_First_Time_Buyer = fetchElement("Summit_Account_Header_Create_CBX_First_Time_Buyer");
                InputText(TXT_Loan_Purpose, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Loan_Purpose));
                InputText(TXT_Holders_Resident, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Holders_Resident));
                checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_First_Time_Buyer),CBX_First_Time_Buyer);
                
                Text TXT_Status_Check = fetchElement("Summit_Account_Header_Create_TXT_Status_Check");
                Text TXT_Known_Debt = fetchElement("Summit_Account_Header_Create_TXT_Known_Debt");
                Text TXT_Part_Purchase = fetchElement("Summit_Account_Header_Create_TXT_Part_Purchase");
                Text TXT_Loan_Income_Ratio = fetchElement("Summit_Account_Header_Create_TXT_Loan_Income_Ratio");
                ComboBox LST_Income_Type = fetchElement("Summit_Account_Header_Create_LST_Income_Type");
                InputText(TXT_Status_Check, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Status_Check));
                InputText(TXT_Known_Debt, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Known_Debt));
                InputText(TXT_Part_Purchase, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Part_Purch));
                InputText(TXT_Loan_Income_Ratio, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Loan_Income_Ratio));
                if(dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Income_Type) != ""){
                    selectValue(LST_Income_Type, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Income_Type));
                }
                
                CheckBox CBX_Guaranter = fetchElement("Summit_Account_Header_Create_CBX_Guaranter");
                checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Guarantor),CBX_Guaranter);
                
                
                Text TXT_Ported_Account_No = fetchElement("Summit_Account_Header_Create_TXT_Ported_Account_No");
                Text TXT_Facility_Limit = fetchElement("Summit_Account_Header_Create_TXT_Facility_Limit");
                Text TXT_Regulation_Regime = fetchElement("Summit_Account_Header_Create_TXT_Regulation_Regime");
                CheckBox CBX_Online_Account = fetchElement("Summit_Account_Header_Create_CBX_Online_Account");
                InputText(TXT_Ported_Account_No, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Ported_Account));
                InputText(TXT_Facility_Limit, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Facility_Limit));
                InputText(TXT_Regulation_Regime, dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Regulation_Regime));
                checkboxOperation(dbUtility.GetAccessFieldValue(TestDataConstants.Account_Header_Online_Account),CBX_Online_Account);
                Utility.Capture_Screenshot();
                if( !String.IsNullOrWhiteSpace(addressNumberInput) )
                {
                    Button corpAddress = fetchElement("Summit_Account_Header_Create_BTN_Correspondence_Addr");
                    corpAddress.Click();
                    AddressToCustomerAdd(addressNumberInput);
                }
                
                ACCOUNT_NUMBER_FORMATTED = TXT_Account_No.Element.GetAttributeValueText("Text");
                SOCIETY = TXT_Society.Element.GetAttributeValueText("Text");
                Button BTN_OK = fetchElement("Summit_Account_Header_Create_BTN_OK");
                BTN_OK.Click();

                if(String.IsNullOrWhiteSpace(addressNumberInput))
                {
                    Button BTN_Yes = fetchElement("Summit_Account_Header_Create_BTN_Yes");
                    BTN_Yes.Click();
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(SOCIETY);
                Main.OutputData.Add(ACCNT_NUMBER);
                Main.OutputData.Add(ACCOUNT_NUMBER_FORMATTED);
                return Main.OutputData;
                
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                Report.Info(e.StackTrace);
                return Main.OutputData;
            }
        }
        
        private void AddressToCustomerAdd(string addressNumberInput){
            if(!String.IsNullOrEmpty(addressNumberInput)){
                Button advancedAddressEstablish = fetchElement("Summit_Address_Establish_BTN_Advanced");
                advancedAddressEstablish.Click();
                Text addressNumberAdvSearchCriteria = fetchElement("Summit_Advanced_Search_Criteria_TXT_Address_Number");
                setText(addressNumberAdvSearchCriteria, addressNumberInput+"{Tab}");
                Button searchAdvSearchCriteria = fetchElement("Summit_Advanced_Search_Criteria_BTN_Search");
                searchAdvSearchCriteria.Click();
                ListItem resultSecondRowAddressEstablish = fetchElement("Summit_Address_Establish_LST_SearchResult_SecondRow");
                resultSecondRowAddressEstablish.Click();
                Utility.Capture_Screenshot();
                Button selectAddressEstablish = fetchElement("Summit_Address_Establish_BTN_Select");
                selectAddressEstablish.Click();
            }
        }
        public List<string> IndividualCustomerCreate(){
            string Proj_Stage = Settings.getInstance().get("PROJ_STAGE");
            string addressNumberInput = String.Empty;
            if(Main.InputData.Count==2)
            {
                addressNumberInput = Main.InputData[1];
            }
            try
            {
                string sqlQuery = "select  *  from [Customer_Individual] where [Account Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string deliveryPrefDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Doc_Delivery_Pref);
                
                string CUSTOMER_NUMBER;
                MenuPromptInternal("CUNSC");
                
                Text TXT_Identity_SocietyNo = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_SocietyNo");
                ComboBox LST_Identity_CustomerType = fetchElement("Summit_Create_Customer_No_Search_LST_Identity_CustomerType");
                Text TXT_Identity_Customer_Title = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_Customer_Title");
                Text TXT_Identity_FirstForename = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_FirstForename");
                Text TXT_Identity_Initials = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_Initials");
                Text TXT_Identity_Surname = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_Surname");
                Text TXT_Identity_Fullname = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_Fullname");
                //MSS 6.0 Sex fied changed from COmbobox to LOV hence Text field
                Text TXT_Identity_Sex = fetchElement("Summit_Customer_Create_TXT_Identity_Sex");
                Text TXT_Identity_DOB = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_DOB");
                ComboBox LST_Identity_Status = fetchElement("Summit_Create_Customer_No_Search_LST_Identity_Status");
                CheckBox CBX_Identity_Prevent_Auto_Delete = fetchElement("Summit_Create_Customer_No_Search_CBX_Identity_Prevent_Auto_Delete");
                CheckBox CBX_Identity_Resides_In_Reportable_Country = fetchElement("Summit_Create_Customer_No_Search_CBX_Identity_Resides_In_Reportable_Country");
                Text TXT_Identity_Country_Of_Residence = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_Country_Of_Residence");
                CheckBox CBX_Identity_High_Net_Worth = fetchElement("Summit_Create_Customer_No_Search_CBX_Identity_High_Net_Worth");
                
                setText(TXT_Identity_SocietyNo, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Society));
                selectValue(LST_Identity_CustomerType, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Customer_Type));
                setText(TXT_Identity_Customer_Title, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Title));
                setText(TXT_Identity_FirstForename, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_First_Forenames));
                if (Constants.PROJ_STAGE_DEV == Proj_Stage) {
                    setText(TXT_Identity_Surname, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Surname)+utility.rndString(7));
                }
                else{
                    setText(TXT_Identity_Surname, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Surname));
                }
                string sexVal = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Sex);
                if(!string.IsNullOrEmpty(sexVal)){
                	if(!sexVal.ToUpper().Equals("DEFAULT")){
                		TXT_Identity_Sex.PressKeys(sexVal[0].ToString());
                	}
                }
                setText(TXT_Identity_DOB, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Date_of_Birth));
                selectValue(LST_Identity_Status, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Status));
                
                if(!string.IsNullOrEmpty(deliveryPrefDB)){
                    Text docDelivery = fetchElement("Summit_Customer_Create_TXT_DocDelivery");
                    setText(docDelivery, deliveryPrefDB);
                }
                
                string preventAutoDelete = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Prevent_Auto_Delete);
                checkboxOperation(preventAutoDelete, CBX_Identity_Prevent_Auto_Delete);
                
                string resideInReportableCountry = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Resides_In_Reportable_Country);
                checkboxOperation(resideInReportableCountry, CBX_Identity_Resides_In_Reportable_Country);
                
                string countryOfResidence = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Country_Of_Residence);
                if(!countryOfResidence.Equals(""))
                {
                    setText(TXT_Identity_Country_Of_Residence, countryOfResidence);
                }else{
                    setDefaultValueToBlank(TXT_Identity_Country_Of_Residence);
                }
                
                string highNetWorth = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_High_Net_Worth);
                checkboxOperation(highNetWorth, CBX_Identity_High_Net_Worth);
                Utility.Capture_Screenshot();
                Button Apply = fetchElement("Summit_Create_Customer_No_Search_BTN_Apply");
                Apply.Click();
                
                //Retry case - if existing customer identified then click cancel and recreate a new customer.
                if(ElementDisplayed(fetchElement("Summit_Check_Customers_TXT_CheckCustomer"))){
                    Utility.Capture_Screenshot();
                    Report.Info("Similar Customer identified - Check above screenshot , Performing cancel to continue.");
                    Button btnCancel=fetchElement("Summit_Check_Customers_BTN_Cancel");
                    btnCancel.Click();
                }
                
                if(String.IsNullOrWhiteSpace(addressNumberInput))
                {
                    Button BTN_No = fetchElement("Summit_Notification_Note_BTN_No");
                    BTN_No.Click();
                }else{
                    Button yes = fetchElement("Summit_Notification_Note_BTN_Yes");
                    yes.Click();
                    Button searchCULAE = fetchElement("Summit_Customer_Living_Address_Establish_BTN_Search");
                    searchCULAE.Click();
                    AddressToCustomerAdd(addressNumberInput);
                    Button okCULAE = fetchElement("Summit_Customer_Living_Address_Establish_BTN_Ok");
                    okCULAE.Click();
                }
                Text TXT_CustomerNumber = fetchElement("Summit_Create_Customer_No_Search_TXT_CustomerNumber");
                CUSTOMER_NUMBER = TXT_CustomerNumber.Element.GetAttributeValueText("Text");
                
                TabPage TABREGN_Personal = fetchElement("Summit_Create_Customer_No_Search_TABREGN_Personal");
                TABREGN_Personal.Click();
                
                Text TXT_Personal_NINumber = fetchElement("Summit_Create_Customer_No_Search_TXT_Personal_NINumber");
                string niCode=dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_NI_Code);
                setText(TXT_Personal_NINumber, utility.GenerateUniqueNICode(niCode));
                Apply.Click();
                
                // Added tp handle Employment Type if coming from DB
                if(dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Employment_Type) != ""){
                    TabPage TABREGN_Employment = fetchElement("Summit_Create_Customer_No_Search_TABREGN_Employment");
                    TABREGN_Employment.Click();
                    Text TXT_Employment_Type = fetchElement("Summit_Create_Customer_No_Search_TXT_Employment_Type");
                    InputText(TXT_Employment_Type, dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Individual_Employment_Type));
                }
                
                Button BTN_OK = fetchElement("Summit_Create_Customer_No_Search_BTN_OK");
                BTN_OK.Click();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(CUSTOMER_NUMBER);
                return Main.OutputData;
            }
            catch (Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Report.Info(e.StackTrace);
                return Main.OutputData;
            }
        }
        
        public List<string> AccountValidate(){
            try{
                MenuPromptInternal("ACV");
                string SOCIETY = Main.InputData[0];
                string ACCNT_NUMBER = Main.InputData[1];
                
                
                Text SOCIETY_Value = fetchElement("Summit_Account_Validate_TXT_Society");
                Text TXT_Account_No = fetchElement("Summit_Account_Validate_TXT_AccountNo");
                
                setText(SOCIETY_Value, SOCIETY);
                setText(TXT_Account_No, ACCNT_NUMBER);
                
                Button Validate = fetchElement("Summit_Account_Validate_BTN_Validate");
                Validate.Click();
                Utility.Capture_Screenshot();
                string alert = fetchElement("Summit_Account_Validate_TXT_Alert_Warning_Msg"); //TODO: Check title was not matching with 'Warning Alert'
                string alert2 = fetchElement("Summit_Account_Validate_TXT_Alert_Validation_Success_Msg");
                string alert3 = fetchElement("Summit_Account_Validate_TXT_Validation_Alert_Warning");
                
                if(ElementDisplayed(alert) || ElementDisplayed(alert2) || ElementDisplayed(alert3)){
                    Button btn = fetchElement("Summit_Account_Validate_BTN_OK_Alert");
                    btn.Click();
                }
                
                string errorAlert = fetchElement("Summit_Account_Validate_TXT_Error_Alert");
                if(ElementDisplayed(errorAlert)){
                    throw new Exception("Account is not validated because of Error");
                }
                
                string ACCHStatusQuery = "Select Acch_Status From Account_Headers Where Acch_Soc_Seqno ='"+SOCIETY+"' And Acch_Account_No = '"+ACCNT_NUMBER+"'";
                Report.Info("ACCHStatusQuery--"+ACCHStatusQuery);
                List<string[]> data=oraUtility.executeQuery(ACCHStatusQuery );
                string statusAcct = data[0][0];
                if(!statusAcct.Equals("A")){
                    throw new Exception("Account status is not Valid: "+statusAcct);
                }
                Button Close = fetchElement("Summit_Account_Validate_BTN_Close");
                Close.Click();
                
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
                
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add("Account Validation failed. Reason="+e.Message);
                return Main.OutputData;
            }
        }
        

        private void MenuPromptInternal(string strMenu){
            Text field = fetchElement("Summit_Account_Create_TXT_MenuPrompt");
            field.PressKeys(strMenu);
            Button Go = fetchElement("Summit_Account_Create_BTN_Go");
            if(Go.Enabled){
                Go.Click();
            }else{
                Utility.Capture_Screenshot();
                throw new Exception("Menu Prompt is Invalid: "+strMenu);
            }
        }
        
        private void setText(Text field, string data){
            string appFlg = Main.appFlag;
            try
            {
                if(field.Enabled && !data.StartsWith(Constants.TestData_DEFAULT))
                {
                    if(appFlg.Equals(Constants.appActivate)){
                        enterText(field, data);
                    }else{
                        field.PressKeys(data);
                    }
                }
            }
            catch (Exception e)
            {
                Report.Info(e.StackTrace);
            }
        }
        
        private void selectValue(ComboBox field, string data){
            try
            {
                if(field.Enabled)
                {
                    field.PressKeys(data);
                }
            }
            catch(Exception e){
                Report.Info(e.StackTrace);
            }
        }
        
        public string fetchElement(string ObjName){
            try
            {
                string elementXPath=(string)Main.elementcoll[ObjName];
                string appFlg = Main.appFlag;
                if(!appFlg.Equals(Constants.appActivate)){
                    String env = Settings.getInstance().get("SUMMIT_TITLE");
                    elementXPath = String.Format(elementXPath,env);
                }
                if(Settings.getInstance().get("XPATH_REPO_LOGS").Equals("Y"))
                {
                    Report.Debug("Element XPATH="+elementXPath);
                }
                return elementXPath;
            }
            catch (Exception e)
            {
                Report.Error("Element ("+ObjName+")not fetched from the Repository."+e.Message);
                throw new Exception("Element ("+ObjName+") not fetched from the Repository."+e.Message);
            }
        }
        
        private void checkboxOperation(string flag, CheckBox checkBox){
            try
            {
                
                if(flag.Equals("Y") && checkBox.Enabled && !checkBox.Checked){
                    checkBox.Click();
                }else if(flag.Equals("N") && checkBox.Enabled && checkBox.Checked){
                    checkBox.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
        
        private void setDefaultValueToBlank(Text text){
            try
            {
                text.Click();
                text.Element.InvokeAction("selectAll");
                text.Element.InvokeAction("clear");
                text.PressKeys("{Tab}");
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
        
        private void enterText(Text ele, string val){
            if(!string.IsNullOrEmpty(ele.TextValue)){
                if(!(ele.TextValue.Equals(".00") || ele.TextValue.Equals("0.00") || ele.TextValue.Equals("0"))){
                    ele.DoubleClick();
                    ele.PressKeys("{Delete}");
                }
                
            }
            ele.Click();
            ele.PressKeys(val+"{Tab}");
        }
        
        private void enterRawText(RawText ele, string val){
            ele.Click();
            ele.PressKeys(val+"{Tab}");
        }
        
        public void waitForPagetoLoadCompletely(string header){
            String logintimeout=Settings.getInstance().get("LOGIN_TIMEOUT");
            int timeout=0;
            int maxWait=Int32.Parse(Settings.getInstance().get("LAUNCH_LOGIN_TIMEOUT"));
            Delay.Seconds(maxWait);
            if(!String.IsNullOrWhiteSpace(logintimeout))
            {
                timeout=Int32.Parse(logintimeout);
            }
            for(int i=0;i<timeout;i++){
                int count = Host.Local.Find("/form[@title='"+Settings.getInstance().get("SUMMIT_TITLE")+"']//text[@AccessibleName='"+header+"']").Count;
                if(count>=1){
                    break;
                }
            }
        }
        
        public List<string> WaitForActivateScreen(){
            try
            {
                string screenTitle = Main.InputData[0];
                if(waitForPagetoAppear(screenTitle)){
                    Main.OutputData.Add(Constants.TS_STATUS_PASS);
                }else{
                    Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                }
                return Main.OutputData;
            }
            catch(Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> SearchTask()
        {
            try
            {
                string taskName = Main.InputData[0];
                SearchTaskInternal(taskName);
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }
            catch(Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void SearchTaskInternal(string taskName){
            try{
                RawText task = fetchElement("Activate_Progress Review_RAWTXT_COL_TASK");
                int col = task.Column;
                int actualColum = col + 1;
                string cell = fetchElement("Activate_Progress Review_TBL_RAWTXT_COL_TASK");
                RxPath actualObj = string.Format(cell,actualColum);
                SearchAndSelectActivateTableCell(taskName, actualObj, 0);
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
    }
}
