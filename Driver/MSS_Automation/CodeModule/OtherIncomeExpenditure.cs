/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 05/12/2022
 * Time: 05:10
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
    /// Description of OtherIncomeExpenditure.
    /// </summary>
    public partial class Keywords
    {
        
        string otherIncomeDB = string.Empty;
        string proposedRentalIncomeDB = string.Empty;
        string expenditure = string.Empty;
        
        public List<string> OtherIncomeExpenditure()
        {
            try{
                
                Main.appFlag = Constants.appActivate;
                OpenMacro(TestDataConstants.Act_OthIncomeExp_Macro);
                
                string sqlQuery = "select  *  from [ACT_Other_Income_Expenditure] where [Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string clientDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Client);                
                otherIncomeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_OtherIncome);
                proposedRentalIncomeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_ProposedRentalIncome);
                expenditure = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure);
                if(!string.IsNullOrEmpty(clientDB)){
                    if(!clientDB.ToLower().Equals("default")){
                        ComboBox client = fetchElement("Activate_Other_Income_Expenditure_COMBX_Client");
                        if(!ComboBoxValueCompare(client,clientDB)){
                            ComboboxItemSelectDirectPath(client, clientDB);
                        }
                    }
                }
                
                if(!string.IsNullOrEmpty(otherIncomeDB)){
                    addIncome();
                }
                
                if(!string.IsNullOrEmpty(proposedRentalIncomeDB)){
                    addRentalIncome();
                }
                
                if(!string.IsNullOrEmpty(expenditure)){
                    addExpenditure();
                }
                
                Button btnOkIncomeExp = fetchElement("Activate_Other Income and Expenditure_BTN_OK");
                btnOkIncomeExp.Click();
                
                RxPath btnOkMsgRx = fetchElement("Activate_Other_Income_Expenditure_BTN_MSG_OK");
                Button btnOkMsg = null;
                if(Host.Local.TryFindSingle(btnOkMsgRx, out btnOkMsg)){
                    btnOkMsg = Host.Local.FindSingle(btnOkMsgRx).As<Button>();
                    btnOkMsg.Click();
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void addIncome(){
            string[] incomes = otherIncomeDB.Split(',');
            int incomeCount = incomes.Length;
            for(int i=1; i<=incomeCount; i++){
                Button btnAddIncome = fetchElement("Activate_Other_Income_Expenditure_BTN_OtherIncome_ADD");
                btnAddIncome.Click();
                string sqlQueryIncome = "select  *  from [ACT_Oth_Inc_Exp_Other_Income] where [Reference] = '"+incomes[i-1].Trim()+"'";
                dbUtility.ReadDBResultMS(sqlQueryIncome);
                string incomeTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_OtherIncome_IncomeType);
                string grossAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_OtherIncome_GrossAmount);
                string frequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_OtherIncome_Frequency);
                string netAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_OtherIncome_NetAmount);
                string netFrequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_OtherIncome_NetFrequency);
                string undPercentDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_OtherIncome_UndPercent); //Not-Editable in application

                string incomeType = fetchElement("Activate_Other_Income_Expenditure_LST_IncomeType");
                string grossAmount = fetchElement("Activate_Other_Income_Expenditure_TXT_GrossAmt");
                string frequency = fetchElement("Activate_Other_Income_Expenditure_CMBOX_Frequency");
                string netAmount = fetchElement("Activate_Other_Income_Expenditure_TXT_NetAmount");
                string netFrequency = fetchElement("Activate_Other_Income_Expenditure_CMBOX_NetFreqency");
                string undPrecent = fetchElement("Activate_Other_Income_Expenditure_TXT_UndPercent_OI");
                
                RxPath incomeTypeRx = CellElement(incomeType, i);
                RxPath grossAmountRx = CellElement(grossAmount, i);
                RxPath frequencyRx = CellElement(frequency, i);
                RxPath netAmountRx = CellElement(netAmount, i);
                RxPath netFrequencyRx = CellElement(netFrequency, i);
                RxPath undPrecentRx = CellElement(undPrecent, i);
                
                selectValueListDropDown(incomeTypeRx, incomeTypeDB);
                setTextValue(grossAmountRx, grossAmountDB);
                selectValueCombobox(frequencyRx, frequencyDB);
                setTextValue(netAmountRx, netAmountDB);
                selectValueCombobox(netFrequencyRx, netFrequencyDB);
            }
        }
        
        
        private void addRentalIncome(){
            string[] incomes = proposedRentalIncomeDB.Split(',');
            int incomeCount = incomes.Length;
            for(int i=1; i<=incomeCount; i++){
                Button btnAddIncome = fetchElement("Activate_Other_Income_Expenditure_BTN_RentIncome_ADD");
                btnAddIncome.Click();
                string sqlQueryIncome = "select  *  from [ACT_Oth_Inc_Exp_Proposed_Rental_Income] where [Reference] = '"+incomes[i-1].Trim()+"'";
                dbUtility.ReadDBResultMS(sqlQueryIncome);
                string rentalIncomeTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_ProposedRentalIncome_RentalIncomeType);
                string proposedGrossAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_ProposedRentalIncome_ProposedGrossAmount);
                string proposedGrossFrequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_ProposedRentalIncome_ProposedGrossFrequency);
                string proposedNetAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_ProposedRentalIncome_ProposedNetAmount);
                string proposedNetFrequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_ProposedRentalIncome_ProposedNetFrequency);
                string actualNetAmountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_ProposedRentalIncome_ActualNetAmount);
                string actualNetFrequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_ProposedRentalIncome_ActualNetFrequency);

                string rentalIncomeType = fetchElement("Activate_Other_Income_Expenditure_LST_Rental_Income_Type");
                string proposedGrossAmount = fetchElement("Activate_Other_Income_Expenditure_TXT_Rental_GrossAmt");
                string proposedGrossFrequency = fetchElement("Activate_Other_Income_Expenditure_CMBOX_Rental_GrossFrequency");
                string proposedNetAmount = fetchElement("Activate_Other_Income_Expenditure_TXT_Rental_NetAmt");
                string proposedNetFrequency = fetchElement("Activate_Other_Income_Expenditure_CMBOX_Rental_NetFrequency");
                string actualNetAmount = fetchElement("Activate_Other_Income_Expenditure_TXT_Rental_ACT_NetAmt");
                string actualNetFrequency = fetchElement("Activate_Other_Income_Expenditure_CMBOX_Rental_ACT_NetFrequency");
                
                RxPath rentalIncomeTypeRx = CellElement(rentalIncomeType, i);
                RxPath proposedGrossAmountRx = CellElement(proposedGrossAmount, i);
                RxPath proposedGrossFrequencyRx = CellElement(proposedGrossFrequency, i);
                RxPath proposedNetAmountRx = CellElement(proposedNetAmount, i);
                RxPath proposedNetFrequencyRx = CellElement(proposedNetFrequency, i);
                RxPath actualNetAmountRx = CellElement(actualNetAmount, i);
                RxPath actualNetFrequencyRx = CellElement(actualNetFrequency, i);
                
                selectValueListDropDown(rentalIncomeTypeRx, rentalIncomeTypeDB);
                setTextValue(proposedGrossAmountRx, proposedGrossAmountDB);
                selectValueCombobox(proposedGrossFrequencyRx, proposedGrossFrequencyDB);
                setTextValue(proposedNetAmountRx, proposedNetAmountDB);
                selectValueCombobox(proposedNetFrequencyRx, proposedNetFrequencyDB);
                setTextValue(actualNetAmountRx, actualNetAmountDB);
                selectValueCombobox(actualNetFrequencyRx, " "+actualNetFrequencyDB.Trim());
            }
        }

        private void addExpenditure(){
            string[] incomes = expenditure.Split(',');
            int incomeCount = incomes.Length;
            for(int i=1; i<=incomeCount; i++){
                Button btnAddIncome = fetchElement("Activate_Other_Income_Expenditure_BTN_Add_E");
                btnAddIncome.Click();
                string sqlQueryIncome = "select  *  from [ACT_Oth_Inc_Exp_Expenditure] where [Reference] = '"+incomes[i-1].Trim()+"'";
                dbUtility.ReadDBResultMS(sqlQueryIncome);
                string expenditureTypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_ExpenditureType);
                string amountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_Amount);
                string frequencyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_Frequency);
                string totalOSDebt_ExpenditureDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_TotalOSDebt_Expenditure);
                string consolidatedDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_Consolidated);
                string statisticalDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_Statistical);

                string expenditureType = fetchElement("Activate_Other_Income_Expenditure_LST_ExpenditureType_E");
                string amount = fetchElement("Activate_Other_Income_Expenditure_TXT_Amount_E");
                string frequency = fetchElement("Activate_Other_Income_Expenditure_CMBX_Frequency_E");
                string totalOSDebt_Expenditure = fetchElement("Activate_Other_Income_Expenditure_TXT_TotalOSDebt_Expenditure");
                string consolidated = fetchElement("Activate_Other_Income_Expenditure_TXT_Consolidated_E");
                string statistical = fetchElement("Activate_Other_Income_Expenditure_CBX_Statistical_E");
                
                RxPath expenditureTypeRx = CellElement(expenditureType, i);
                selectValueListDropDown(expenditureTypeRx, expenditureTypeDB);
                if(expenditureTypeDB.Equals("Credit Card", StringComparison.OrdinalIgnoreCase) ||
                   expenditureTypeDB.Equals("Court Order Payments", StringComparison.OrdinalIgnoreCase) ||
                   expenditureTypeDB.Equals("Loans", StringComparison.OrdinalIgnoreCase) ||
                   expenditureTypeDB.Equals("Other Commitments", StringComparison.OrdinalIgnoreCase) ||
                   expenditureTypeDB.Equals("Rent", StringComparison.OrdinalIgnoreCase) ||
                   expenditureTypeDB.Equals("Secured Loan", StringComparison.OrdinalIgnoreCase)
                  ){
                    string expenditureDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_Expenditure);
                    string recipientDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_Recipient);
                    string addressLine1DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_AddressLine1);
                    string addressLine2DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_AddressLine2);
                    string addressLine3DB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_AddressLine3);
                    string townDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_Town);
                    string countyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_County);
                    string postcodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_Postcode);
                    string refAccNoDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_RefAccNo);
                    string monthsLeftRunDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_MonthsLeftRun);
                    string balanceClearedMonthlyDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_BalanceClearedMonthly);
                    string totaOSDebt_ExpRecipientDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_TotaOSDebt_ExpRecipient);
                    string consolidateAmtDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_OthIncomeExp_Expenditure_ConsolidateAmt);
                    
                    RxPath expenditureRx = fetchElement("Activate_Other_Income_Expenditure_LST_Expenditure_E");
                    RxPath recipient = fetchElement("Activate_Other_Income_Expenditure_TXT_Recipient_E");
                    RxPath addressLine1 = fetchElement("Activate_Other_Income_Expenditure_TXT_AddressLine1_E");
                    RxPath addressLine2 = fetchElement("Activate_Other_Income_Expenditure_TXT_AddressLine2_E");
                    RxPath addressLine3 = fetchElement("Activate_Other_Income_Expenditure_TXT_AddressLine3_E");
                    RxPath town = fetchElement("Activate_Other_Income_Expenditure_TXT_Town_E");
                    RxPath county = fetchElement("Activate_Other_Income_Expenditure_TXT_County_E");
                    RxPath postcode = fetchElement("Activate_Other_Income_Expenditure_TXT_Postcode_E");
                    RxPath refAccNo = fetchElement("Activate_Other_Income_Expenditure_TXT_RefAccNo_E");
                    RxPath monthsLeftRun = fetchElement("Activate_Other_Income_Expenditure_TXT_MonthsLeftRun_E");
                    RxPath balanceClearedMonthly = fetchElement("Activate_Other_Income_Expenditure_CBX_BalanceClearedMonthly_E");
                    RxPath totaOSDebt_ExpRecipient = fetchElement("Activate_Other_Income_Expenditure_TXT_TotaOSDebt_ExpRecipient_E");
                    RxPath consolidateAmt = fetchElement("Activate_Other_Income_Expenditure_TXT_ConsolidateAmt_E");
                    
                    selectValueListDropDown(expenditureRx, expenditureDB);
                    setTextValue(recipient, recipientDB);
                    setTextValue(addressLine1, addressLine1DB);
                    setTextValue(addressLine2, addressLine2DB);
                    setTextValue(addressLine3, addressLine3DB);
                    setTextValue(town, townDB);
                    setTextValue(county, countyDB);
                    setTextValue(postcode, postcodeDB);
                    setTextValue(refAccNo, refAccNoDB);
                    setTextValue(monthsLeftRun, monthsLeftRunDB);
                    setCheckbox(balanceClearedMonthly, balanceClearedMonthlyDB);
                    setTextValue(totaOSDebt_ExpRecipient, totaOSDebt_ExpRecipientDB);
                    setTextValue(consolidateAmt, consolidateAmtDB);
                    
                    Button btnOkExp = fetchElement("Activate_Other_Income_Expenditure_BTN_Ok_E");
                    btnOkExp.Click();
                }
                RxPath amountRx = CellElement(amount, i);
                RxPath frequencyRx = CellElement(frequency, i);
                RxPath totalOSDebt_ExpenditureRx = CellElement(totalOSDebt_Expenditure, i);
                RxPath consolidatedRx = CellElement(consolidated, i);
                RxPath statisticalRx = CellElement(statistical, i);
                setTextValue(amountRx, amountDB);
                selectValueCombobox(frequencyRx, frequencyDB);
                setTextValue(totalOSDebt_ExpenditureRx, totalOSDebt_ExpenditureDB);
                setTextValue(consolidatedRx, consolidatedDB);
                setCheckbox(statisticalRx, statisticalDB);
            }
        }
        
        private void setTextValue(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    Text txt = Host.Local.FindSingle(path).As<Text>();
                    setText(txt, val);
                }
            }
        }
        
        private void selectValueListDropDown(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    List lst = Host.Local.FindSingle(path).As<List>();
                    ListtemSelectDirectPath(lst, val);
                }
            }
        }
        
        private void selectValueCombobox(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    ComboBox cmbox = Host.Local.FindSingle(path).As<ComboBox>();
                    ComboboxItemSelectDirect(cmbox, val);
                }
            }
        }
        
        private void setCheckbox(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    CheckBox cbx = Host.Local.FindSingle(path).As<CheckBox>();
                    checkboxOperation(val,cbx);
                }
            }
        }
    }
}
