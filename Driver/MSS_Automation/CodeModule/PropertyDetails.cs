/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 22/08/2022
 * Time: 13:51
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
	/// Description of PropertyDetails.
	/// </summary>
	public partial class Keywords
	{
	    string propertyCollateralDB = string.Empty;
	    
	    public List<string> PropertyDetails()
	    {
	        try{
	            
	            Main.appFlag = Constants.appActivate;	            
	            OpenMacro(TestDataConstants.Act_PropertyDetails_Macro);
	            if(!FAFlag){
	                string sqlQuery = "select  *  from [ACT_Property_Details] where [Reference] = '"+Main.InputData[0]+"'";
	                dbUtility.ReadDBResultMS(sqlQuery);
	                
	                string addressLine1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_AddressLine1);
	                string addressLine2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_AddressLine2);
	                string addressLine3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_AddressLine3);
	                string postCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PostCode);
	                string countryDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_Country);
	                string propertyTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropertyType);
	                string receptionRoomsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_ReceptionRooms);
	                string bedRoomsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_BedRooms);
	                string kitchensDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_Kitchens);
	                string bathroomsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_Bathrooms);
	                string usageDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_Usage);
	                string classDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_Class);
	                string tenureDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_Tenure);
	                string unexpiredLeaseTermDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_UnexpiredLeaseTerm);
	                string wallTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_WallType);
	                string roofTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_RoofType);
	                string newTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_NewType);
	                string warrantyTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_WarrantyType);
	                string yearBuiltDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_YearBuilt);
	                string selfBuildDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_SelfBuild);
	                string rolCountyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_RolCounty);
	                propertyCollateralDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropertyCollateral);
	                
	                Text addressLine1 = fetchElement("Activate_Property Details_TXT_Address_Line_1");
	                setText(addressLine1, addressLine1DB);
	                Text addressLine2 = fetchElement("Activate_Property Details_TXT_Address_Line_2");
	                setText(addressLine2, addressLine2DB);
	                Text addressLine3 = fetchElement("Activate_Property Details_TXT_Address_Line_3");
	                setText(addressLine3, addressLine3DB);
	                Text postCode = fetchElement("Activate_Property Details_TXT_Postcode");
	                setText(postCode, postCodeDB);
	                if(!string.IsNullOrWhiteSpace(countryDB)){
	                    if(!countryDB.ToUpper().Equals(TestDataConstants.CountryType_UK)){
	                        List country = fetchElement("Activate_Property Details_LST_Country_Code");
	                        ComboboxItemSelectE(country, countryDB);
	                    }
	                }
	                List wallType = fetchElement("Activate_Property Details_LST_Wall_Type");
	                ComboboxItemSelect(wallType, wallTypeDB);
	                List roofType = fetchElement("Activate_Property Details_LST_Roof_Type");
	                ComboboxItemSelectDirect(roofType, roofTypeDB);
	                List newType = fetchElement("Activate_Property Details_LST_New_Type");
	                ComboboxItemSelectDirect(newType, newTypeDB);
	                List warrantyType = fetchElement("Activate_Property Details_LST_Warranty_Type");
	                ComboboxItemSelectDirect(warrantyType, warrantyTypeDB);
	                Text yearBuilt = fetchElement("Activate_Property Details_TXT_Year_Built");
	                setText(yearBuilt, yearBuiltDB);
	                
	                List propertyType = fetchElement("Activate_Property Details_LST_PropertyType");
	                if(!string.IsNullOrWhiteSpace(propertyTypeDB)){
	                    if(!propertyTypeDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                        ListtemSelectDirectPath(propertyType, propertyTypeDB);
	                    }
	                }
	                
	                Text receptionRooms = fetchElement("Activate_Property Details_TXT_Reception_Rooms");
	                setText(receptionRooms, receptionRoomsDB);
	                Text bedRooms = fetchElement("Activate_Property Details_TXT_Bedroom");
	                setText(bedRooms, bedRoomsDB);
	                Text kitchens = fetchElement("Activate_Property Details_TXT_Kitchens");
	                setText(kitchens, kitchensDB);
	                Text bathrooms = fetchElement("Activate_Property Details_TXT_Bathrooms");
	                setText(bathrooms, bathroomsDB);
	                if(!string.IsNullOrWhiteSpace(usageDB)){
	                    if(!usageDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                        List usage = fetchElement("Activate_Property Details_LST_Usage");
	                        ComboboxItemSelectE(usage, usageDB);
	                    }
	                }
	                if(!string.IsNullOrWhiteSpace(classDB)){
	                    if(!classDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
	                        List classField = fetchElement("Activate_Property Details_LST_PropertyClass");
	                        ListtemSelectDirectPathPropertyDetails(classField, classDB);
	                    }
	                }
	                List tenure = fetchElement("Activate_Property Details_LST_Tenure");
	                ComboboxItemSelect(tenure, tenureDB);
	                
	                Text unexpiredLeaseTerm = fetchElement("Activate_Property Details_TXT_Unexpired_Lease_Term");
	                setText(unexpiredLeaseTerm, unexpiredLeaseTermDB);
	                
	                if(!string.IsNullOrEmpty(selfBuildDB)){
	                    RxPath rxPath = fetchElement("Activate_Property_Details_CBX_SelfBuild");
	                    setCheckbox(rxPath, selfBuildDB);
	                    string estimatedValueDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_EstimatedValue);
	                    RxPath rxPathTxt = fetchElement("Activate_Property_Details_TXT_EstimatedValue");
	                    setTextValue(rxPathTxt, estimatedValueDB);
	                }
	                
	                string flatDetails_ClkBtnDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_FlatDetails_ClkBtn);
	                if(flatDetails_ClkBtnDB.ToUpper().Equals("Y")){
	                    Button btnFlatDetails = fetchElement("Activate_Property Details_BTN_Flat_Details_Search");
	                    btnFlatDetails.Click();
	                    string noOfStoriesDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_NoOfStories);
	                    string floorNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_FloorNo);
	                    string noOfFlatsDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_NoOfFlats);
	                    string serviceByLiftDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_ServiceByLift);
	                    
	                    Text noOfStories = fetchElement("Activate_Property Details_TXT_FlatDetails_NoOfStories");
	                    setText(noOfStories, noOfStoriesDB);
	                    Text floorNo = fetchElement("Activate_Property Details_TXT_FlatDetails_FloorNo");
	                    setText(floorNo, floorNoDB);
	                    Text noOfFlats = fetchElement("Activate_Property Details_TXT_FlatDetails_NoOfFlats");
	                    setText(noOfFlats, noOfFlatsDB);
	                    
	                    CheckBox serviceByLift = fetchElement("Activate_Property Details_CBX_FlatDetails_ServiceByLift");
	                    if(!string.IsNullOrEmpty(serviceByLiftDB)){
	                        checkboxOperation(serviceByLiftDB, serviceByLift);
	                    }
	                    Button btnOk = fetchElement("Activate_Property Details_BTN_FlatDeatils_OK");
	                    btnOk.Click();
	                    
	                }
	                
	                if(!string.IsNullOrEmpty(propertyCollateralDB)){
	                    PropertyCollateralFill();
	                }
	                
	                RxPath rolCountyRx = fetchElement("Activate_Property_Details_LST_RolCounty");
	                selectValueListDropDown(rolCountyRx, rolCountyDB);
	            }
	            Button btnOKPropDetails = fetchElement("Activate_Property Details_BTN_OK");
	            btnOKPropDetails.Click();
	            
	            //waitForPagetoAppear(TestDataConstants.Page_TopMenu);
	            
	            Main.OutputData.Add(Constants.TS_STATUS_PASS);
	            return Main.OutputData;
	        }catch(Exception e){
	            Main.OutputData.Add(Constants.TS_STATUS_FAIL);
	            Main.OutputData.Add(e.Message);
	            return Main.OutputData;
	        }
	    }

		private void ListtemSelectDirectPathPropertyDetails(List cmbox, string text){
	    	bool itemFound = false;
	    	cmbox.Click();
	    	IList<ListItem> items = cmbox.FindDescendants<ListItem>();
	    	foreach(ListItem item in items){
	    		if(item.Text.Equals(text, StringComparison.OrdinalIgnoreCase)){
	    			int count =  item.Index;
	    			for(int i=1;i<=count-1;i++){
	    				Keyboard.Press("{Down}");
	    			}
	    			Keyboard.Press("{Tab}");
	    			itemFound = true;
	    			break;
	    		}
	    	}
	    	if(!itemFound){
	    		throw new Exception("List value is not Valid. Correct the Data and try again");
	    	}
	    }
	    
	    private void PropertyCollateralFill(){
	        Button btnPropColl = fetchElement("Activate_Property_Details_BTN_PropertyCollateral");
	        btnPropColl.Click();
	        string[] properties = propertyCollateralDB.Split(',');
	        int propertyCount = properties.Length;
	        for(int i=0; i<propertyCount; i++){
	            Button btnAdd = fetchElement("Activate_Property_Collateral_List_BTN_ADD");
	            btnAdd.Click();
	            string sqlQueryProperties = "select  *  from [ACT_Property_Collateral_Details] where [Reference] = '"+properties[i]+"'";
	            dbUtility.ReadDBResultMS(sqlQueryProperties);
	            string tierDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_Tier);
	            string loyaltyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_Loyalty);
	            string AWEDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_AWE);
	            string addrLine1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_AddrLine1);
	            string addrLine2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_AddrLine2);
	            string addrLine3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_AddrLine3);
	            string addrLine4DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_AddrLine4);
	            string addrLine5DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_AddrLine5);
	            string postCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_PostCode);
	            string countryCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_CountryCode);
	            string summitAddrFlagDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_SummitAddrFlag);
	            string summitAddrDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_SummitAddr);
	            string amtDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_PropVal_Amt);
	            string dateDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_PropVal_Date);
	            string typeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_PropVal_Type);
	            string newTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_PropDtls_NewType);
	            string propDtls_TypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_PropDtls_Type);
	            string usageDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_PropDtls_Usage);
	            string classDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_PropDtls_Class);
	            string deedsHeldDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_DeedsHeld);
	            string sharedOwnerDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_SharedOwner);
	            string crossChargeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_CrossCharge);
	            string chargeNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_ChargeNo);
	            string collateralStatusDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_PropertyDetails_PropColl_CollateralStatus);
	            
	            RxPath tierRx = fetchElement("Activate_Property_Collateral_Details_TXT_Tier");
	            RxPath loyaltyRx = fetchElement("Activate_Property_Collateral_Details_CBX_Loyalty");
	            RxPath AWERx = fetchElement("Activate_Property_Collateral_Details_CBX_AWE");
	            RxPath addrLine1Rx = fetchElement("Activate_Property_Collateral_Details_TXT_AddrLine1");
	            RxPath addrLine2Rx = fetchElement("Activate_Property_Collateral_Details_TXT_AddrLine2");
	            RxPath addrLine3Rx = fetchElement("Activate_Property_Collateral_Details_TXT_AddrLine3");
	            RxPath addrLine4Rx = fetchElement("Activate_Property_Collateral_Details_TXT_AddrLine4");
	            RxPath addrLine5Rx = fetchElement("Activate_Property_Collateral_Details_TXT_AddrLine5");
	            RxPath postCodeRx = fetchElement("Activate_Property_Collateral_Details_TXT_PostCode");
	            RxPath countryCodeRx = fetchElement("Activate_Property_Collateral_Details_LST_CountryCode");
	            RxPath summitAddrFlagRx = fetchElement("Activate_Property_Collateral_Details_CBX_SummitAddrFlag");
	            RxPath summitAddrRx = fetchElement("Activate_Property_Collateral_Details_TXT_SummitAddr");
	            RxPath amtRx = fetchElement("Activate_Property_Collateral_Details_TXT_PropVal_Amt");	            
	            RxPath typeRx = fetchElement("Activate_Property_Collateral_Details_LST_PropVal_Type");
	            RxPath newTypeRx = fetchElement("Activate_Property_Collateral_Details_LST_PropDtls_NewType");
	            RxPath propDtls_TypeRx = fetchElement("Activate_Property_Collateral_Details_LST_PropDtls_Type");
	            RxPath usageRx = fetchElement("Activate_Property_Collateral_Details_LST_PropDtls_Usage");
	            RxPath classRx = fetchElement("Activate_Property_Collateral_Details_LST_PropDtls_Class");
	            RxPath deedsHeldRx = fetchElement("Activate_Property_Collateral_Details_CBX_DeedsHeld");
	            RxPath sharedOwnerRx = fetchElement("Activate_Property_Collateral_Details_CBX_SharedOwner");
	            RxPath crossChargeRx = fetchElement("Activate_Property_Collateral_Details_CBX_CrossCharge");
	            RxPath chargeNoRx = fetchElement("Activate_Property_Collateral_Details_TXT_ChargeNo");
	            RxPath collateralStatusRx = fetchElement("Activate_Property_Collateral_Details_LST_CollateralStatus");
	            
	            setTextValue(tierRx, tierDB);
	            setCheckbox(loyaltyRx, loyaltyDB);
	            setCheckbox(AWERx, AWEDB);
	            
	            setTextValue(addrLine1Rx, addrLine1DB);
	            setTextValue(addrLine2Rx, addrLine2DB);
	            setTextValue(addrLine3Rx, addrLine3DB);
	            setTextValue(addrLine4Rx, addrLine4DB);
	            setTextValue(addrLine5Rx, addrLine5DB);
	            setTextValue(postCodeRx, postCodeDB);
	            selectValueListDropDown(countryCodeRx, countryCodeDB);
	            setCheckbox(summitAddrFlagRx, summitAddrFlagDB);
	            setTextValue(summitAddrRx, summitAddrDB);
	            
	            setTextValue(amtRx, amtDB);
	            setDateValue("Activate_Property_Collateral_Details_TXT_PropVal_Date", dateDB);
	            selectValueListDropDown(typeRx, typeDB);
	            
	            selectValueListDropDown(newTypeRx, newTypeDB);
	            selectValueListDropDown(propDtls_TypeRx, propDtls_TypeDB);
	            selectValueListDropDown(usageRx, usageDB);
	            selectValueListDropDown(classRx, classDB);
	            setCheckbox(deedsHeldRx, deedsHeldDB);
	            setCheckbox(sharedOwnerRx, sharedOwnerDB);
	            
	            setCheckbox(crossChargeRx, crossChargeDB);
	            setTextValue(chargeNoRx, chargeNoDB);
	            selectValueListDropDown(collateralStatusRx, collateralStatusDB);
	            
	            Button btnOk = fetchElement("Activate_Property_Collateral_Details_BTN_OK");
	            btnOk.Click();
	        }
	        Button btnOkPropList = fetchElement("Activate_Property_Collateral_List_BTN_OK");
	        btnOkPropList.Click();
	    }
	}
}
