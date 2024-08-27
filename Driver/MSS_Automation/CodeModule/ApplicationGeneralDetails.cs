/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 17/08/2022
 * Time: 13:48
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
    /// Description of ApplicationGeneralDetails.
    /// </summary>
    public partial class Keywords
    {
        string iDD_Question2DB = string.Empty;
        string iDD_Question3DB = string.Empty;
        string iDD_Question4DB = string.Empty;
        string mortNumApp = String.Empty;
        string advanceTypeDB = string.Empty;
        string directApplicationDB = string.Empty;
        string firstTimeBuyerDB = string.Empty;
        string societyDB = string.Empty;
        string branchDB = string.Empty;
        string channelDB = string.Empty;
        string marketingSourceDB = string.Empty;
        string indirectSource_CompanyNameDB = string.Empty;
        string indirectSource_CompanyNameDB2 = string.Empty;
        string packagedDB = string.Empty;
        string applicationSubTypeDB = string.Empty;
        string useDirectConveyancingServiceDB = string.Empty;
        string willANewContractBeIssuedDB = string.Empty;
        string registeredIntermediaryDB = string.Empty;
        string levelOfServiceDB = string.Empty;
        string interviewDB = string.Empty;
        string newInput = String.Empty;
        public bool FAFlag = false;
        public bool RMFlag = false;
        public bool MVFlag = false;
        public bool LMFlag=false;
        public bool TOEFlag=false;
        public bool notDirectApp = true;
        
        public List<string> ApplicationGeneralDetails()
        {
            try{
                Main.appFlag = Constants.appActivate;

                OpenMacro(TestDataConstants.Act_AppGenDetails_Macro);
                
                ApplicationGenDetails();
                InitialDisclosureDetailsFill();
                LevelOfService();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                if(!FAFlag){
                    Main.OutputData.Add(mortNumApp);
                }
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void radioButtonSelect(RadioButton rbtn){
            rbtn.Click();
        }
        
        private void InitialDisclosureDetails(string header){
            try{
                waitForPagetoAppear(header);
                string PreFix_InitialDisclosureDetails = fetchElement("Activate_Initial_Disclosure_Details_PreFix_InitialDisclosureDetails");
                if(!(string.IsNullOrEmpty(iDD_Question2DB))){
                    if(!iDD_Question2DB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        string flag = "Y";
                        if(iDD_Question2DB.ToUpper().Equals("OPTION1")){
                            string posElement = fetchElement("Activate_Initial_Disclosure_Details_CBX_checkbox3");
                            CheckBox cbOption = PreFix_InitialDisclosureDetails+header+posElement;
                            checkboxOperation(flag, cbOption);
                        }else if(iDD_Question2DB.ToUpper().Equals("OPTION2")){
                            string posElement = fetchElement("Activate_Initial_Disclosure_Details_CBX_checkbox4");
                            CheckBox cbOption = PreFix_InitialDisclosureDetails+header+posElement;
                            checkboxOperation(flag, cbOption);
                        }else if(iDD_Question2DB.ToUpper().Equals("OPTION3")){
                            string posElement = fetchElement("Activate_Initial_Disclosure_Details_CBX_checkbox5");
                            CheckBox cbOption = PreFix_InitialDisclosureDetails+header+posElement;
                            checkboxOperation(flag, cbOption);
                        }
                    }
                }
                if(!(string.IsNullOrEmpty(iDD_Question3DB))){
                    if(!iDD_Question3DB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        string flag = "Y";
                        if(iDD_Question3DB.ToUpper().Equals("OPTION1")){
                            string posElement = fetchElement("Activate_Initial_Disclosure_Details_CBX_checkbox1");
                            CheckBox cbOption = PreFix_InitialDisclosureDetails+header+posElement;
                            checkboxOperation(flag, cbOption);
                        }else if(iDD_Question3DB.ToUpper().Equals("OPTION2")){
                            string posElement = fetchElement("Activate_Initial_Disclosure_Details_CBX_checkbox2");
                            CheckBox cbOption = PreFix_InitialDisclosureDetails+header+posElement;
                            checkboxOperation(flag, cbOption);
                        }
                    }
                }
                if(!(string.IsNullOrEmpty(iDD_Question4DB))){
                    if(!iDD_Question4DB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        string flag = "Y";
                        if(iDD_Question4DB.ToUpper().Equals("OPTION1")){
                            string posElement = fetchElement("Activate_Initial_Disclosure_Details_CBX_checkbox6");
                            CheckBox  cbOption = PreFix_InitialDisclosureDetails+header+posElement;
                            checkboxOperation(flag, cbOption);
                        }else if(iDD_Question4DB.ToUpper().Equals("OPTION2")){
                            string posElement = fetchElement("Activate_Initial_Disclosure_Details_CBX_checkbox7");
                            CheckBox  cbOption = PreFix_InitialDisclosureDetails+header+posElement;
                            checkboxOperation(flag, cbOption);
                        }
                    }
                }
                string posFixBtn = fetchElement("Activate_Initial_Disclosure_Details_PostFix_Btn_OK");
                string btnOk = PreFix_InitialDisclosureDetails+header+posFixBtn;
                Report.Info("btnOkRx--"+btnOk);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                Button btnFAOk = btnOk;
                btnFAOk.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void handlePrint(){
            Button PrintOK = null;
            try{
                PrintOK = fetchElement("Activate_Initial_Disclosure_Details_BTN_Print_Reject");
                PrintOK.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
            
        }
        
        private void IDDPopupElementHandle(){
            string iDDStr = fetchElement("Activate_Initial_Disclosure_Details_Form_HousePurchase_IDD");
            try{
                Form IDD = iDDStr;
                if(IDD.EnsureVisible()){
                    Button btnOk3 = fetchElement("Activate_Initial_Disclosure_Details_Form_HousePurchase_IDD_BTN_OK");
                    btnOk3.Click();
                }
            }catch(Exception){
                Form IDD = iDDStr;
                if(IDD.EnsureVisible()){
                    Button btnOk3 = fetchElement("Activate_Initial_Disclosure_Details_Form_HousePurchase_IDD_BTN_OK");
                    btnOk3.Click();
                }
            }
        }
        
        private void ApplicationGenDetails(){
            try{
                int inputLen = Main.InputData.Count;
                if(inputLen>1){
                    newInput = Main.InputData[1];
                }
                string sqlQuery = "select  *  from [ACT_Application_General_Details] where [Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                advanceTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_AdvanceType);
                directApplicationDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_DirectApplication);
                firstTimeBuyerDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_FirstTimeBuyer);
                societyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_Society);
                branchDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_Branch);
                channelDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_Channel);
                marketingSourceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_MarketingSource);
                indirectSource_CompanyNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_IndirectSource_CompanyName);
                indirectSource_CompanyNameDB2 = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_IndirectSource2_CompanyName);
                packagedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_Packaged);
                applicationSubTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_ApplicationSubType);
                useDirectConveyancingServiceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_UseDirectConveyancingService);
                willANewContractBeIssuedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_WillANewContractBeIssued);
                iDD_Question2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_IDD_Question2);
                iDD_Question3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_IDD_Question3);
                iDD_Question4DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_IDD_Question4);
                registeredIntermediaryDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_RegisteredIntermediary);
                levelOfServiceDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_LevelOfService);
                interviewDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_AppGenDetails_Interview);
                
                //waitForPagetoAppear(TestDataConstants.Page_AppGeneralDetails);
                RxPath listRx = fetchElement("Activate_Application_General_Details_LST_Advance_Type");
                if(advanceTypeDB.ToUpper().Equals(TestDataConstants.AdvanceType_FA)){
                    selectValueListDropDown(listRx, advanceTypeDB);
                    FAFlag = true;
                    if(inputLen>2){
                        newInput = Main.InputData[2];
                    }
                    //waitForPagetoAppear(TestDataConstants.Page_MortDetails);
                    string mortNumberInput = Main.InputData[1];
                    Text mortNumber = fetchElement("Activate_Application_General_Details_TXT_FA_mortNmber");
                    if(string.IsNullOrEmpty(mortNumber.TextValue)){
                        setText(mortNumber, mortNumberInput);
                    }else{
                        mortNumber.DoubleClick();
                        Keyboard.Press("{Delete}");
                        setText(mortNumber, mortNumberInput);
                    }
                    Button mortOk = fetchElement("Activate_Application_General_Details_Btn_FA_mortOk");
                    mortOk.Click();
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));
                    
                }else if(advanceTypeDB.ToUpper().Equals("MORTGAGE VARIATION") || advanceTypeDB.ToUpper().Equals("PRODUCT SWITCH") || advanceTypeDB.ToUpper().Equals("TRANSFER OF EQUITY")){
                    if(advanceTypeDB.ToUpper().Equals("TRANSFER OF EQUITY")){
                        selectValueListDropDown(listRx, advanceTypeDB);
                        TOEFlag = true;
                    }else{
                        selectValueListDropDown(listRx, advanceTypeDB);
                        MVFlag = true;
                    }
                    if(inputLen>2){
                        newInput = Main.InputData[2];
                    }
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    string mortNumberInput = Main.InputData[1];
                    Text mortNumber = fetchElement("Activate_Application_General_Details_TXT_FA_mortNmber");
                    if(string.IsNullOrEmpty(mortNumber.TextValue)){
                        setText(mortNumber, mortNumberInput);
                    }else{
                        mortNumber.DoubleClick();
                        Keyboard.Press("{Delete}");
                        setText(mortNumber, mortNumberInput);
                    }
                    Button mortOk = fetchElement("Activate_Application_General_Details_Btn_FA_mortOk");
                    mortOk.Click();
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));
                    if(advanceTypeDB.ToUpper().Equals("PRODUCT SWITCH")){
                        RxPath btnCloseRx = fetchElement("Activate_Product_Switch_Validations_BTN_Close");
                        Button btnClose = null;
                        Duration durationTimePS = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("ELEMENT_SEARCH_TIMEOUT")));
                        if(Host.Local.TryFindSingle(btnCloseRx, durationTimePS,out btnClose)){
                            btnClose.Click();
                        }
                    }
                }
                else if(advanceTypeDB.ToUpper().Equals("REMORTGAGE")){
                    selectValueListDropDown(listRx, advanceTypeDB);
                    RMFlag = true;
                }else if(advanceTypeDB.ToUpper().Equals("LIFETIME MORTGAGE")){
                    selectValueListDropDown(listRx, advanceTypeDB);
                    LMFlag = true;
                }else{
                    selectValueListDropDown(listRx, advanceTypeDB);
                    RxPath cbxRx = fetchElement("Activate_Application_General_Details_CBX_UseDirectConveyancingService");
                    setCheckbox(cbxRx, useDirectConveyancingServiceDB);
                }
                CheckBox directApplication = fetchElement("Activate_Application_General_Details_CBX_Direct_Application");
                if(!string.IsNullOrWhiteSpace(directApplicationDB)){
                    if(!directApplicationDB.ToLower().Equals("default")){
                        checkboxOperation(directApplicationDB, directApplication);
                    }
                }
                notDirectApp = directApplication.Checked;
                if(FAFlag || MVFlag ){
                    RxPath cbxRx = fetchElement("Activate_Application_General_Details_CBX_WillANewContractBeIssued");
                    setCheckbox(cbxRx, willANewContractBeIssuedDB);
                }
                if(!string.IsNullOrWhiteSpace(firstTimeBuyerDB)){
                    if(!firstTimeBuyerDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        CheckBox firstTimeBuyer = fetchElement("Activate_Application_General_Details_CBX_First_Time_Buyer");
                        checkboxOperation(firstTimeBuyerDB, firstTimeBuyer);
                    }
                }
                if(!(MVFlag || FAFlag || TOEFlag)){
                    List society = fetchElement("Activate_Application_General_Details_LST_Society");
                    ComboboxItemSelectDirect(society, societyDB);
                }
                List branch = fetchElement("Activate_Application_General_Details_LST_Branch");
                ComboboxItemSelectDirect(branch, branchDB);
                
                if(!string.IsNullOrEmpty(applicationSubTypeDB)){
                    List applicationSubType = fetchElement("Activate_Application_General_Details_LST_Application_Sub_Type");
                    ComboboxItemSelect(applicationSubType, applicationSubTypeDB);
                }

                List channel = fetchElement("Activate_Application_General_Details_LST_Channel");
                ComboboxItemSelectDirect(channel, channelDB);
                List marketingSource = fetchElement("Activate_Application_General_Details_LST_Marketing_Source");
                ComboboxItemSelectDirect(marketingSource, marketingSourceDB);
                
                if(!string.IsNullOrEmpty(indirectSource_CompanyNameDB)){
                    Button source1Button = fetchElement("Activate_Application_General_Details_IndirectSource_BTN_source1Button");
                    source1Button.Click();
                    if("NEW".Equals(indirectSource_CompanyNameDB.ToUpper())){
                        if(String.IsNullOrEmpty(newInput))
                        {
                            throw new Exception("DB column says NEW IntroducertType-1 & However IntroducertType-1 Input Param is missing");
                        }
                        IndirectSourceFill(newInput);
                    }else{
                        IndirectSourceFill(indirectSource_CompanyNameDB);
                    }
                    Text source_1  = fetchElement("Activate_Application_General_Details_TXT_Source_1");
                    string sc_1 = source_1.TextValue;
                    if(String.IsNullOrEmpty(sc_1)){
                        Delay.Seconds(3);
                    }
                    sc_1 = source_1.TextValue;
                    Report.Info("source_1: "+sc_1);
                }
                
                if(!string.IsNullOrEmpty(indirectSource_CompanyNameDB2)){
                    Button source2Button = fetchElement("Activate_Application_General_Details_IndirectSource_BTN_source2Button");
                    source2Button.Click();
                    if("NEW".Equals(indirectSource_CompanyNameDB2.ToUpper())){
                        if(String.IsNullOrEmpty(newInput))
                        {
                            throw new Exception("DB column says NEW IntroducertType-2 & However IntroducertType-2 Input Param is missing");
                        }
                        IndirectSourceFill(newInput);
                    }else{
                        IndirectSourceFill(indirectSource_CompanyNameDB2);
                    }
                    Text source_2  = fetchElement("Activate_Application_General_Details_TXT_Source_2");
                    string sc_2 = source_2.TextValue;
                    if(String.IsNullOrEmpty(sc_2)){
                        Delay.Seconds(3);
                    }
                    sc_2 = source_2.TextValue;
                    Report.Info("source_2: "+sc_2);
                }
                
                if(!string.IsNullOrWhiteSpace(packagedDB)){
                    if(!packagedDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        CheckBox packaged = fetchElement("Activate_Application_General_Details_CBX_Packaged");
                        checkboxOperation(packagedDB, packaged);
                    }
                }
                
                Button btnOk2 = fetchElement("Activate_Application_General_Details_BTN_OK");
                btnOk2.Click();
                
                RxPath btnOk3 = fetchElement("Activate_Select_Product_BTN_OK");
                Button btnOkSelectProduct = null;
                Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                if(Host.Local.TryFindSingle(btnOk3, durationTime,out btnOkSelectProduct)){
                    btnOkSelectProduct.Click();
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                }
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void InitialDisclosureDetailsFill(){
            try{
                if(advanceTypeDB.ToUpper().Equals(TestDataConstants.AdvanceType_FA)){
                    if(!notDirectApp){
                        IDDPopupElementHandle();
                    }else{
                        InitialDisclosureDetails(TestDataConstants.Page_InitialDisclosure_FA);
                        handlePrint();
                    }
                    //waitForPagetoAppear(TestDataConstants.Page_LevelOfService_FA);
                }else if(advanceTypeDB.ToUpper().Equals(TestDataConstants.AdvanceType_HP) || RMFlag || MVFlag || LMFlag){
                    if(!notDirectApp){
                        IDDPopupElementHandle();
                    }else{
                        InitialDisclosureDetails(TestDataConstants.Page_InitialDisclosure_HP);
                        handlePrint();
                    }
                    //waitForPagetoAppear(TestDataConstants.Page_LevelOfService_HP);
                }else{
                    InitialDisclosureDetails(TestDataConstants.Page_InitialDisclosure_HP);
                    handlePrint();
                    //waitForPagetoAppear(TestDataConstants.Page_LevelOfService_HP);
                }
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void LevelOfService(){
            try{
                if(!string.IsNullOrEmpty(registeredIntermediaryDB)){
                    if(!registeredIntermediaryDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        CheckBox registeredIntermediary = fetchElement("Activate_Level_of_Service_CBX_Registered_Intermediary");
                        checkboxOperation(registeredIntermediaryDB, registeredIntermediary);
                    }
                }
                
                if(!string.IsNullOrEmpty(levelOfServiceDB)){
                    if(!levelOfServiceDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        if(levelOfServiceDB.ToUpper().Equals("ADVISED")){
                            RadioButton  radBtn = fetchElement("Activate_Level_of_Service_RAD_Advised");
                            radioButtonSelect(radBtn);
                        }else if(levelOfServiceDB.ToUpper().Equals("EXECUTION ONLY")){
                            RadioButton  radBtn = fetchElement("Activate_Level_of_Service_RAD_Execution_Only");
                            radioButtonSelect(radBtn);
                        }else if(levelOfServiceDB.ToUpper().Equals("ADVICE REJECTED")){
                            RadioButton  radBtn = fetchElement("Activate_Level_of_Service_RAD_Advice_Rejected");
                            radioButtonSelect(radBtn);
                        }else if(levelOfServiceDB.ToUpper().Equals("NON ADVISED (BUY TO LET/NON-REGULATED)")){
                            RadioButton  radBtn = fetchElement("Activate_Level_of_Service_RAD_Non_Advised");
                            radioButtonSelect(radBtn);
                        }
                    }
                }
                
                if(!string.IsNullOrEmpty(interviewDB)){
                    if(!interviewDB.ToUpper().Equals(Constants.TestData_DEFAULT)){
                        if(interviewDB.ToUpper().Equals("FACE TO FACE")){
                            RadioButton radBtn = fetchElement("Activate_Level_of_Service_RAD_Face_to_Face");
                            radioButtonSelect(radBtn);
                        }else if(interviewDB.ToUpper().Equals("TELEPHONE")){
                            RadioButton radBtn = fetchElement("Activate_Level_of_Service_RAD_Telephone");
                            radioButtonSelect(radBtn);
                        }else if(interviewDB.ToUpper().Equals("INTERNET/EMAIL (I.E. ELECTRONIC)")){
                            RadioButton radBtn = fetchElement("Activate_Level_of_Service_RAD_Internet_Email");
                            radioButtonSelect(radBtn);
                        }else if(interviewDB.ToUpper().Equals("POST")){
                            RadioButton radBtn = fetchElement("Activate_Level_of_Service_RAD_Post");
                            radioButtonSelect(radBtn);
                        }else if(interviewDB.ToUpper().Equals("NONE")){
                            RadioButton radBtn = fetchElement("Activate_Level_of_Service_RAD_None");
                            radioButtonSelect(radBtn);
                        }
                    }
                }
                
                Text mortNumAppPath = fetchElement("Activate_Level_of_Service_TXT_mortNum");
                mortNumApp = mortNumAppPath.TextValue;
                
                Button btnOk4 = fetchElement("Activate_Level_of_Service_BTN_OK");
                btnOk4.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void IndirectSourceFill(string dbValue){
            try{
                RxPath companyNameRx  = fetchElement("Activate_Application_General_Details_IndirectSource_TXT_companyName");
                Text companyName = Host.Local.FindSingle(companyNameRx, duration);
                setText(companyName, dbValue);
                Button btnSearch = fetchElement("Activate_Application_General_Details_IndirectSource_BTN_Search");
                btnSearch.Click();
                RawText tableRecord2Col_Source  = fetchElement("Activate_Application_General_Details_IndirectSource_tableRecord2Col_Source");
                tableRecord2Col_Source.EnsureVisible();
                string val = tableRecord2Col_Source.Element.GetAttributeValueText("RawText");
                Report.Info("tableRecord2Col_Source: "+val);
                tableRecord2Col_Source.Click();
                
                Button btnOk = fetchElement("Activate_Application_General_Details_IndirectSource_btnOk");
                btnOk.Click();
                Delay.Seconds(1);
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
    }
}
