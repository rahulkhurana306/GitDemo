/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 15/06/2022
 * Time: 17:34
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
    /// Description of PropertyCreateAndLink.
    /// </summary>
    public partial class Keywords
    {
        
        public List<string> PropertyCreateAndLink(){
            try
            {
                string testDataRef = Main.InputData[0];
                string societyInput = Main.InputData[1];
                string accountNumberInput = Main.InputData[2];
                string addressNumberInput = Main.InputData[3];
                
                string sqlQuery = "select * from [Property] where [Account Reference] = '"+testDataRef+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                MenuPromptInternal("ACPDA");
                
                string workingCalDate = null;
                string ownerNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Owner_Name);
                string propertyTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Property_Type);
                string newPropertyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_New_Property);
                string typeClassDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Type_Class);
                string propertyUsageDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Property_Usage);
                string deedsHeldDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Deeds_Held);
                string sharedOwnerDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Shared_Owner);
                string sittingTenantDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Sitting_Tenant);
                string rightToBuyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Right_To_Buy);
                string fullVacantPossessionDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Full_Vacant_Possession);
                string secondHomeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_GeneralDetails_Second_Home);
                string garageDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_Characteristics_Garage);
                string priceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_CostValuation_Cost_Price);
                string latestBSCValuationAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_CostValuation_Latest_BSC_Valuation_Amount);
                string latestBSCValuationDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_CostValuation_Latest_BSC_Valuation_Date);
                string latestBSCValuationTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_CostValuation_Latest_BSC_Valuation_Type);
                
                string propertyUsedAsCollateralDB = dbUtility.GetAccessFieldValue(TestDataConstants.Account_Property_Details_Add_PropertyUsed_as_Collateral);
                string valuationAlloctedToThisAccountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Account_Property_Details_Add_Valuation_Allocted_to_This_Account);
                string dateEffectiveFromDB = dbUtility.GetAccessFieldValue(TestDataConstants.Account_Property_Details_Add_Date_Effective_From);
                
                Text society = fetchElement("Summit_Account_Property_Details_Add_TXT_Society");
                Text accountNo = fetchElement("Summit_Account_Property_Details_Add_TXT_AccountNo");
                setText(society, societyInput);
                setText(accountNo, accountNumberInput+"{TAB}");
                string warning_corresponding_address_create=fetchElement("Summit_Address_Establish_TXT_Warning_AddressCreate");
                string warning_popup_title=fetchElement("Summit_Address_Establish_TXT_Warning_Title");
                if(ElementDisplayed(warning_corresponding_address_create)){
                    Keyboard.Press("{TAB}");
                    Keyboard.Press("{ENTER}");
                }
                if(ElementDisplayed(warning_popup_title)){
                Keyboard.Press("{ENTER}");
                }
                
                Button advanced = fetchElement("Summit_Address_Establish_BTN_Advanced");
                advanced.Click();
                
                Text advanceSearchAddressNo = fetchElement("Summit_Address_Establish_TXT_AdvanceSearch_AddressNo");
                setText(advanceSearchAddressNo, addressNumberInput+"{TAB}");
                
                Button advanceSearchSearch = fetchElement("Summit_Address_Establish_BTN_AdvanceSearch_Search");
                advanceSearchSearch.Click();
                ListItem searchResultSecondRow  = fetchElement("Summit_Address_Establish_LST_SearchResult_SecondRow");
                searchResultSecondRow.Click();
                Utility.Capture_Screenshot();
                
                Button selectButton = fetchElement("Summit_Address_Establish_BTN_Select");
                selectButton.Click();
                Button property = fetchElement("Summit_Account_Property_Details_Add_BTN_Property");
                property.EnsureVisible();
                property.Click();
                if(ElementDisplayed(fetchElement("Summit_Property_Maintain_TXT_Warning_Property_Exist"))){
                    Button yes = fetchElement("Summit_Property_Maintain_BTN_Warning_Yes");
                    yes.Click();
                }
                Text generalDetailsOwnerName = fetchElement("Summit_Property_Maintain_TXT_GeneralDetails_Owner_Name");
                setText(generalDetailsOwnerName, ownerNameDB);
                Text generalDetailsPropertyType = fetchElement("Summit_Property_Maintain_TXT_GeneralDetails_PropertyType");
                setText(generalDetailsPropertyType, propertyTypeDB);
                Text generalDetailsNewProperty = fetchElement("Summit_Property_Maintain_TXT_GeneralDetails_NewProperty");
                setText(generalDetailsNewProperty, newPropertyDB);
                Text generalDetailsTypeClass = fetchElement("Summit_Property_Maintain_TXT_GeneralDetails_TypeClass");
                setText(generalDetailsTypeClass, typeClassDB);
                Text generalDetailsPropertyUsage = fetchElement("Summit_Property_Maintain_TXT_GeneralDetails_PropertyUsage");
                setText(generalDetailsPropertyUsage, propertyUsageDB);
                CheckBox generalDetailsDeedsHeld = fetchElement("Summit_Property_Maintain_CBX_GeneralDetails_Deeds_Held");
                checkboxOperation(deedsHeldDB, generalDetailsDeedsHeld);
                CheckBox generalDetailsSharedOwner = fetchElement("Summit_Property_Maintain_CBX_GeneralDetails_Shared_Owner");
                checkboxOperation(sharedOwnerDB, generalDetailsSharedOwner);
                CheckBox generalDetailsSittingTenant = fetchElement("Summit_Property_Maintain_CBX_GeneralDetails_Sitting_Tenant");
                checkboxOperation(sittingTenantDB, generalDetailsSittingTenant);
                CheckBox generalDetailsRightToBuy = fetchElement("Summit_Property_Maintain_CBX_GeneralDetails_Right_To_Buy");
                checkboxOperation(rightToBuyDB, generalDetailsRightToBuy);
                CheckBox generalDetailsFullVacantPossession = fetchElement("Summit_Property_Maintain_CBX_GeneralDetails_Full_Vacant_Possession");
                checkboxOperation(fullVacantPossessionDB, generalDetailsFullVacantPossession);
                CheckBox generalDetailsSecondHome = fetchElement("Summit_Property_Maintain_CBX_GeneralDetails_Second_Home");
                checkboxOperation(secondHomeDB, generalDetailsSecondHome);
                Utility.Capture_Screenshot();
                
                TabPage characteristicsTab = fetchElement("Summit_Property_Maintain_TABREGN_Characteristics");
                characteristicsTab.Click();
                Text characteristicsGarage = fetchElement("Summit_Property_Maintain_TXT_Characteristics_Garage");
                setText(characteristicsGarage, garageDB);
                Utility.Capture_Screenshot();
                
                TabPage costValuation = fetchElement("Summit_Property_Maintain_TABREGN_CostValuation");
                costValuation.Click();
                Text costCostPrice = fetchElement("Summit_Property_Maintain_TXT_Cost_Cost_Price");
                setText(costCostPrice, priceDB);
                Text costLatestBSCValuationAmnt = fetchElement("Summit_Property_Maintain_TXT_Cost_LatestBSCValuation");
                setText(costLatestBSCValuationAmnt, latestBSCValuationAmountDB);
                Text costDate = fetchElement("Summit_Property_Maintain_TXT_Cost_Date");
                workingCalDate = utility.ProcessWCALDate(latestBSCValuationDateDB,societyInput);
                setText(costDate, workingCalDate);
                Text costType = fetchElement("Summit_Property_Maintain_TXT_Cost_Type");
                setText(costType, latestBSCValuationTypeDB);
                
                string latestHPIValuationAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_CostValuation_Latest_HPI_Valuation_Amount);
                if(!"".Equals(latestHPIValuationAmountDB)){
                    string latestHPIValuationDateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_CostValuation_Latest_HPI_Valuation_Date);
                    string latestHPIValuationTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Property_Maintain_CostValuation_Latest_HPI_Valuation_Type);
                    
                    Text costLatestHPIValuationAmount = fetchElement("Summit_Property_Maintain_TXT_Cost_Latest_HPI_Valuation_Amount");
                    setText(costLatestHPIValuationAmount, latestHPIValuationAmountDB);
                    Text costLatestHPIValuationDate = fetchElement("Summit_Property_Maintain_TXT_Cost_Latest_HPI_Valuation_Date");
                    workingCalDate = utility.ProcessWCALDate(latestHPIValuationDateDB,societyInput);
                    setText(costLatestHPIValuationDate, workingCalDate);
                    Text costLatestHPIValuationType = fetchElement("Summit_Property_Maintain_TXT_Cost_Latest_HPI_Valuation_Type");
                    setText(costLatestHPIValuationType, latestHPIValuationTypeDB);
                }
                
                Utility.Capture_Screenshot();
                Button oK = fetchElement("Summit_Property_Maintain_BTN_OK");
                oK.Click();
                
                CheckBox propertyUsedAsCollateral = fetchElement("Summit_Account_Property_Details_Add_CBX_PropertyUsed_as_Collateral");
                checkboxOperation(propertyUsedAsCollateralDB, propertyUsedAsCollateral);
                Text valuationAlloctedToThisAccount = fetchElement("Summit_Account_Property_Details_Add_TXT_Valuation_Allocted_to_This_Account");
                setText(valuationAlloctedToThisAccount, valuationAlloctedToThisAccountDB);
                Text dateEffectiveFrom = fetchElement("Summit_Account_Property_Details_Add_TXT_Date_Effective_From");
                workingCalDate = utility.ProcessWCALDate(dateEffectiveFromDB,societyInput);
                setText(dateEffectiveFrom, workingCalDate);
                Button AcctDetailsOk = fetchElement("Summit_Account_Property_Details_Add_BTN_Ok");
                Utility.Capture_Screenshot();
                AcctDetailsOk.Click();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }
            catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Report.Error(e.StackTrace);
                return Main.OutputData;
            }
            
        }
        
        private Boolean ElementDisplayed(string elementValue){
            Boolean displayed = false;
            Text txt = null;
            RxPath txtMessage = elementValue;
            Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
            if(Host.Local.TryFindSingle(txtMessage, durationTime,out txt)){
                if(txt.Visible){
                    displayed = true;
                }
            }
            return displayed;
        }
    }
}
