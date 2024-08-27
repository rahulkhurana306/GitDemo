/*
 * Created by Ranorex
 * User: gchandel
 * Date: 21/06/2022
 * Time: 14:03
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
    /// Description of CustomerLink.
    /// </summary>
    public partial class Keywords
    {
        public List<string> CustomerLink()
        {
            try{
                string testDataRefInput = Main.InputData[0];
                string customerNumberInput = Main.InputData[1];
                string societyInput = Main.InputData[2];
                string accountNumberInput = Main.InputData[3];
                string subAccountNumberInput = Main.InputData[4];
                
                //Print input parameters in log
                Report.Info(testDataRefInput);//TESTDATAREFERENCE
                Report.Info(customerNumberInput);//CUSTOMER_NUMBER
                Report.Info(societyInput);//SOCIETY
                Report.Info(accountNumberInput);//ACCOUNT_NUMBER
                Report.Info(subAccountNumberInput);//SUB_ACCOUNT_NUMBER
                
                //Read data from MS Acess
                string sqlQuery = "select  *  from [Customer_Link] where [Account Reference] = '"+testDataRefInput+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                
                //Fetch value from MS Access
                string testNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Test_Name);
                string subAccountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Sub_Account);
                string usageDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Usage);
                string effectiveDateFromDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Effective_Date_From);
                string taxGroupDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Tax_Group);
                string holdingMultDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Holding_Mult);
                string interestPaymentCodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Interest_Payment_Code);
                string holdingDivDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Holding_Div);
                string instFromDB = dbUtility.GetAccessFieldValue(TestDataConstants.Customer_Link_Inst_From);
                
                string oDate;
                MenuPromptInternal("ACLM");
                //Customer Search Screen - Code will only execute when search is done directly (not from CUVL/BCUVL)
                try {
                    Text customerNumber = fetchElement("Summit_Customer_Search_TXT_Customer_Number");
                    setText(customerNumber,customerNumberInput);
                    Button search = fetchElement("Summit_Customer_Search_BTN_Search");
                    search.Click();
                    Utility.Capture_Screenshot();
                    Button go = fetchElement("Summit_Customer_Search_BTN_Go");
                    go.Click();
                }
                catch {
                }
                //Account Links Maintain Screen
                Button accountLinksWizard = fetchElement("Summit_Account_Links_Maintain_BTN_AccountLinksWizard");
                accountLinksWizard.Click();
                
                //Customer Account Links Wizard Screen - options
                RadioButton linkCustomerToAnotherAccount = fetchElement("Summit_Customer_Account_Links_Wizard_RAD_LinkCustomerToAnotherAccount");
                linkCustomerToAnotherAccount.Click();
                Button custAccLinkWizardnext = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Next");
                custAccLinkWizardnext.Click();
                
                //Customer Account Links Wizard Screen - Add new account
                Button newAccSearch = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Search");
                newAccSearch.Click();
                //--Find Account Details popup
                Text society = fetchElement("Summit_Find_Account_Details_TXT_Society");
                Text accountNumber = fetchElement("Summit_Find_Account_Details_TXT_Account_Number");
                setText(accountNumber,accountNumberInput);
                setText(society,societyInput);
                Utility.Capture_Screenshot();
                Button accountDetailsGo = fetchElement("Summit_Find_Account_Details_BTN_Go");
                accountDetailsGo.Click();
                //Customer Account Links Wizard Screen - Select sub account (using loop)
                String subValue;
                String subElement;
                Text sub;
                
                String statusValue;
                String statusElement;
                Text status;
                
                String linkElement;
                CheckBox link;
                int i = 1;
                int max = 9;
                //loop for Customer Account Links Wizard Screen - Select sub account (here Table functionality is not used considering max subaccount used will be 9)
                while (i <= max) {
                    subElement = "Summit_Customer_Account_Links_Wizard_TXT_Sub"+i;
                    sub = fetchElement(subElement);
                    subValue = sub.TextValue;
                    if (String.IsNullOrEmpty(subValue)){
                        break;
                    }
                    statusElement = "Summit_Customer_Account_Links_Wizard_TXT_Status"+i;
                    status = fetchElement(statusElement);
                    statusValue = status.TextValue;
                    
                    linkElement = "Summit_Customer_Account_Links_Wizard_CBX_Link"+i;
                    link = fetchElement(linkElement);
                    
                    if (statusValue != "Closed"){
                        if ((String.IsNullOrEmpty(subAccountNumberInput)) || (subAccountNumberInput == subValue)){
                            checkboxOperationClick("Y",link);
                        }
                        else {
                            checkboxOperationClick("N",link);
                        }
                    }
                    Utility.Capture_Screenshot();
                    i++;
                }
                
                Button custAclmWizardnext1 = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Next");
                custAclmWizardnext1.Click();
                
                //Customer Account Links Wizard Screen - Linking details
                ComboBox subAccount = fetchElement("Summit_Customer_Account_Links_Wizard_LST_Sub_Account");
                if(subAccount.Enabled){
                    selectValue(subAccount,subAccountDB);
                }
                
                Text accountLinkedTo = fetchElement("Summit_Customer_Account_Links_Wizard_TXT_AccountLinkedToCustomer");
                String accountLinkedToCustomer = accountLinkedTo.TextValue;
                
                //Loop to  search customer name and set usage code
                String customerValue;
                String customerElement;
                Text customer;
                String usageCodeElement;
                ComboBox usageCode;
                i = 1;
                max = 4;
                while (i <= max) {
                    customerElement = "Summit_Customer_Account_Links_Wizard_TXT_CustFullName"+i;
                    customer = fetchElement(customerElement);
                    customerValue = customer.TextValue;
                    if (customerValue == accountLinkedToCustomer){
                        usageCodeElement = "Summit_Customer_Account_Links_Wizard_LST_CustUsage"+i;
                        usageCode = fetchElement(usageCodeElement);
                        selectValue(usageCode,usageDB);
                        break;
                    }
                    i++;
                }
                
                String alertwindowYesGaurantor = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Guarantor_Warning_Alert_Yes");
                if(isButtonDisplayed(alertwindowYesGaurantor)){
                    Button alertYes = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Guarantor_Warning_Alert_Yes");
                    alertYes.Click();
                }
                
                String alertwindowYes = fetchElement("Summit_Change_Title_And_Salutation_Alert_BTN_Alert_Yes");
                if(isButtonDisplayed(alertwindowYes)){
                    Button alertYes = fetchElement("Summit_Change_Title_And_Salutation_Alert_BTN_Alert_Yes");
                    alertYes.Click();
                }
                
                //Applicable for Savings account
                Button customerAclmWizardNext = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Next");
                if(customerAclmWizardNext.Enabled && !String.IsNullOrEmpty(effectiveDateFromDB)){
                    // Button custAclmWizardnext2 = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Next");
                    customerAclmWizardNext.Click();
                    
                    String accHoldTaxCustFullNameValue;
                    String accHoldTaxCustFullNameElement;
                    Text accHoldTaxCustFullName;
                    
                    String effectiveFromElement;
                    Text effectiveFrom;
                    
                    String holdingMultElement;
                    Text holdingMult;
                    
                    String holdingDivElement;
                    Text holdingDiv;
                    
                    String taxGroupElement;
                    Text taxGroup;
                    
                    String taxInstFromElement;
                    Text taxInstFrom;
                    
                    i = 1;
                    max = 6;
                    while (i <= max) {
                        accHoldTaxCustFullNameElement = "Summit_Customer_Account_Links_Wizard_TXT_AccHoldTaxCustFullName"+i;
                        accHoldTaxCustFullName = fetchElement(accHoldTaxCustFullNameElement);
                        accHoldTaxCustFullNameValue = accHoldTaxCustFullName.TextValue;
                        
                        if (accHoldTaxCustFullNameValue.Equals(accountLinkedToCustomer)){
                            //Check/calculate effective date
                            if (effectiveDateFromDB != "DEFAULT"){
                                oDate = effectiveDateFromDB;
                                if (effectiveDateFromDB.Contains("WCAL_DATE") == true){
                                    oDate = utility.ProcessWCALDate(effectiveDateFromDB,societyInput);
                                }
                                effectiveFromElement = "Summit_Customer_Account_Links_Wizard_TXT_Effective_From"+i;
                                effectiveFrom = fetchElement(effectiveFromElement);
                                setText(effectiveFrom,oDate);
                            }
                            //Check Hold Mult
                            if (holdingMultDB != "DEFAULT"){
                                holdingMultElement = "Summit_Customer_Account_Links_Wizard_TXT_Holding_Mult"+i;
                                holdingMult = fetchElement(holdingMultElement);
                                setText(holdingMult,holdingMultDB);
                            }
                            //Check Hold Div
                            if (holdingDivDB != "DEFAULT"){
                                holdingDivElement = "Summit_Customer_Account_Links_Wizard_TXT_Holding_Div"+i;
                                holdingDiv = fetchElement(holdingDivElement);
                                setText(holdingDiv,holdingDivDB);
                            }
                            //Check Tax Group
                            if (taxGroupDB != "DEFAULT"){
                                taxGroupElement = "Summit_Customer_Account_Links_Wizard_TXT_Tax_Group"+i;
                                taxGroup = fetchElement(taxGroupElement);
                                setText(taxGroup,taxGroupDB);
                            }
                            //Check Tax Group
                            if (instFromDB != "DEFAULT"){
                                taxInstFromElement = "Summit_Customer_Account_Links_Wizard_TXT_Tax_Inst_From"+i;
                                taxInstFrom = fetchElement(taxInstFromElement);
                                setText(taxInstFrom,instFromDB);
                            }
                            Utility.Capture_Screenshot();
                            break;
                        }
                        i++;
                    }
                    if(!String.IsNullOrEmpty(interestPaymentCodeDB)){
                        Button interest = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Interest");
                        interest.Click();
                        ComboBox intPayCode = fetchElement("Summit_Customer_Account_Links_Wizard_LST_Int_Pay_Code");
                        selectValue(intPayCode,interestPaymentCodeDB);
                        Utility.Capture_Screenshot();
                        Button interestOk = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Interest_Ok");
                        interestOk.Click();
                    }
                }
                Utility.Capture_Screenshot();
                Button finish = fetchElement("Summit_Customer_Account_Links_Wizard_BTN_Finish");
                finish.Click();
                string warning_popup_eID_Check=fetchElement("Summit_Customer_Account_Links_Wizard_TXT_Warning_EID_Check");
                if(ElementDisplayed(warning_popup_eID_Check)){
                    Utility.Capture_Screenshot();
                    Keyboard.Press("{ENTER}");
                }
                Button cancel = fetchElement("Summit_Account_Links_Maintain_BTN_Cancel");
                cancel.Click();
                
                //Generic Code
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
                
            }
            catch (Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Report.Error(e.StackTrace);
                return Main.OutputData;
            }
        }
        private Boolean isButtonDisplayed(string elementValue){
            Boolean displayed = false;
            try{
                Button element = elementValue;
                if(element.Visible){
                    displayed = true;
                }
                return displayed;
            }catch(Exception){
                return displayed;
            }
        }
    }
}