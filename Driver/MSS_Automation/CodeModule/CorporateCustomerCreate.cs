/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 22/06/2022
 * Time: 16:04
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using ng_mss_automation.CodeModule;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of CorporateCustomerCreate.
    /// </summary>
    public partial class Keywords
    {
        public List<string> CorporateCustomerCreate(){
            try
            {
                int inputLen = Main.InputData.Count;
                string testDataRef = Main.InputData[0];
                string addressNumberInput = null;
                if(inputLen>1){
                    if(!String.IsNullOrEmpty(Main.InputData[1])){
                        addressNumberInput = Main.InputData[1];
                    }
                }
                
                string sqlQuery = "select  *  from [Customer_Corporate] where [Account Reference] = '"+testDataRef+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                string CUSTOMER_NUMBER;
                MenuPromptInternal("CUNSC");
                
                Text societyNo = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_SocietyNo");
                ComboBox customerType = fetchElement("Summit_Create_Customer_No_Search_LST_Identity_CustomerType");
                Text fullName = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_Fullname");
                ComboBox status = fetchElement("Summit_Create_Customer_No_Search_LST_Identity_Status");
                CheckBox preventAutoDelete = fetchElement("Summit_Create_Customer_No_Search_CBX_Identity_Prevent_Auto_Delete");
                CheckBox residesInReportableCountry = fetchElement("Summit_Create_Customer_No_Search_CBX_Identity_Resides_In_Reportable_Country");
                Text countryOfResidence = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_Country_Of_Residence");
                CheckBox highNetWorth = fetchElement("Summit_Create_Customer_No_Search_CBX_Identity_High_Net_Worth");
                
                string societyInput = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_Society);
                string customerTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_Customer_Type);
                string fullnameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_Fullname);
                string alphaKeyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_Alpha_Key);
                string statusDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_Status);
                string preventAutoDeleteDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_Prevent_Auto_Delete);
                string residesInReportableCountryDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_ResidesIn_Reportable_Country);
                string countryOfResidenceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_Country_Of_Residence);
                string highNetWorthDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Corporate_High_Net_Worth);
                
                setText(societyNo, societyInput);
                selectValue(customerType, customerTypeDB);
                string Proj_Stage = Settings.getInstance().get("PROJ_STAGE");
                if (Constants.PROJ_STAGE_DEV == Proj_Stage) {
                    setText(fullName, fullnameDB+utility.rndString(7));
                }
                else{
                    setText(fullName, fullnameDB);
                }
                
                if(!alphaKeyDB.Equals(Constants.TestData_DEFAULT)){
                    Text alphaKey = fetchElement("Summit_Create_Customer_No_Search_TXT_Identity_AlphaKey");
                    setText(alphaKey, alphaKeyDB);
                }
                selectValue(status, statusDB);
                checkboxOperation(preventAutoDeleteDB, preventAutoDelete);
                checkboxOperation(residesInReportableCountryDB, residesInReportableCountry);
                setText(countryOfResidence, countryOfResidenceDB);
                checkboxOperation(highNetWorthDB, highNetWorth);
                Utility.Capture_Screenshot();
                Button apply = fetchElement("Summit_Create_Customer_No_Search_BTN_Apply");
                apply.Click();
                
                //Retry case - if existing customer identified then click cancel and recreate a new customer.
                if(ElementDisplayed(fetchElement("Summit_Check_Customers_TXT_CheckCustomer"))){
                    Utility.Capture_Screenshot();
                    Report.Info("Similar Customer identified - Check above screenshot , Performing cancel to continue.");
                    Button btnCancel=fetchElement("Summit_Check_Customers_BTN_Cancel");
                    btnCancel.Click();
                }
                
                AddAddressToCustomer(addressNumberInput);
                
                Text customerNumber = fetchElement("Summit_Create_Customer_No_Search_TXT_CustomerNumber");
                CUSTOMER_NUMBER = customerNumber.Element.GetAttributeValueText("Text");
                
                Button ok = fetchElement("Summit_Create_Customer_No_Search_BTN_OK");
                ok.Click();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(CUSTOMER_NUMBER);
                return Main.OutputData;
            }
            catch (Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Report.Error(e.StackTrace);
                return Main.OutputData;
            }
        }
        
        private void AddAddressToCustomer(string addressNumberInput){
            if(!String.IsNullOrEmpty(addressNumberInput)){
                Button yes = fetchElement("Summit_Notification_Note_BTN_Yes");
                yes.Click();
                Button searchCULAE = fetchElement("Summit_Customer_Living_Address_Establish_BTN_Search");
                searchCULAE.Click();
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
                Button okCULAE = fetchElement("Summit_Customer_Living_Address_Establish_BTN_Ok");
                okCULAE.Click();
                
            }else{
                Button no = fetchElement("Summit_Notification_Note_BTN_No");
                no.Click();
            }
        }
        
    }
}

