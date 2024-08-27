/*
 * Created by Ranorex
 * User: mhuriya
 * Date: 6/17/2022
 * Time: 12:28 PM
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
    /// Description of AddressCreate.
    /// </summary>
    public partial class Keywords
    {
        public List<string> AddressCreate()
        {
            try
            {
                string Proj_Stage = Settings.getInstance().get("PROJ_STAGE");
                string sqlQuery = "select  *  from [Address] where [Account Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string addressLine1Db = dbUtility.GetAccessFieldValue(TestDataConstants.Address_AddressLine1);
                
                if (Constants.PROJ_STAGE_DEV == Proj_Stage) {
                    addressLine1Db = addressLine1Db+ utility.rndString(7);
                }
                
                string addressLine2Db = dbUtility.GetAccessFieldValue(TestDataConstants.Address_AddressLine2);
                string addressLine3Db = dbUtility.GetAccessFieldValue(TestDataConstants.Address_AddressLine3);
                string addressLine4Db = dbUtility.GetAccessFieldValue(TestDataConstants.Address_AddressLine4);
                string addressLine5Db = dbUtility.GetAccessFieldValue(TestDataConstants.Address_AddressLine5);
                string postCodeDb = dbUtility.GetAccessFieldValue(TestDataConstants.Address_PostCode);
                string countryCodeDb = dbUtility.GetAccessFieldValue(TestDataConstants.Address_CountryCode);
                string foreignDb = dbUtility.GetAccessFieldValue(TestDataConstants.Address_Foreign);
                
                string addressNumberGenerated;
                MenuPromptInternal("ADS");
                
                Text searchPostCode = fetchElement("Summit_Address_Establish_TXT_Summit_Address_Search_PostCode");
                InputText(searchPostCode, "A9A 9AA");
                Button search = fetchElement("Summit_Address_Establish_BTN_Search");
                search.Click();
                Text addressLine1 = fetchElement("Summit_Address_Establish_TXT_Address_Established_AddressLine1");
                InputText(addressLine1, addressLine1Db);
                //Fetch value of addressline from Screen
                string addressLine1_search = addressLine1.TextValue;
                
                Text addressLine2 = fetchElement("Summit_Address_Establish_TXT_Address_Established_AddressLine2");
                InputText(addressLine2, addressLine2Db);
                Text addressLine3 = fetchElement("Summit_Address_Establish_TXT_Address_Established_AddressLine3");
                InputText(addressLine3, addressLine3Db);
                Text addressLine4 = fetchElement("Summit_Address_Establish_TXT_Address_Established_AddressLine4");
                InputText(addressLine4, addressLine4Db);
                Text addressLine5 = fetchElement("Summit_Address_Establish_TXT_Address_Established_AddressLine5");
                InputText(addressLine5, addressLine5Db);
                Text postCode = fetchElement("Summit_Address_Establish_TXT_Address_Established_PostCode");
                InputText(postCode, postCodeDb);
                Text countryCode = fetchElement("Summit_Address_Establish_TXT_Address_Established_CountryCode");
                InputText(countryCode, countryCodeDb);
                ComboBox foreign = fetchElement("Summit_Address_Establish_LST_Foreign");
                selectValue(foreign, foreignDb);
                Utility.Capture_Screenshot();
                Button create = fetchElement("Summit_Address_Establish_BTN_Create");
                create.Click();
                string warning_popup_title=fetchElement("Summit_Address_Establish_TXT_Warning_Alert");
                if(ElementDisplayed(warning_popup_title)){
                    Keyboard.Press("{ENTER}");
                }
                Button addressAlertOk = fetchElement("Summit_Notification_Warning_BTN_OK");
                addressAlertOk.Click();
                
                //Code Added for fetching Address no from DB using addressLine1_search
                string addNoQuery = "SELECT addr_address_no FROM addresses WHERE addr_address_1='"+addressLine1_search+"' ORDER BY addr_address_no DESC";
                List<string[]> data=oraUtility.executeQuery(addNoQuery);
                string Status=String.Empty;
                if(data.Count>0){
                  addressNumberGenerated=data[0][0];
                  Main.OutputData.Add(Constants.TS_STATUS_PASS);
                  Main.OutputData.Add(addressNumberGenerated);
                  Report.Info("Address Number: "+addressNumberGenerated);
                }
                else{
                   Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                   Main.OutputData.Add("Error-Address no. not generated");
                   Report.Error("Error-Address no. not generated");
                }
                return Main.OutputData;
            }
            catch (Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
    }
}
