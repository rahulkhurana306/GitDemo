/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 15/12/2022
 * Time: 13:46
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
    /// Description of ProductAddMaintain.
    /// </summary>
    public partial class Keywords
    {
        public List<string> ProductAddMaintain()
        {
            try{
                Main.appFlag = Constants.appActivate;
                int inputLen = Main.InputData.Count;
                string accountTypeInput = String.Empty;
                string accountTypeInputCI = String.Empty;
                string accountTypeInputIntCntrl = String.Empty;
                if(inputLen==2){
                    accountTypeInput = Main.InputData[1];
                }
                if(inputLen==3){
                    accountTypeInput = Main.InputData[1];
                    accountTypeInputCI = Main.InputData[2];
                }
                if(inputLen==4){
                    accountTypeInput = Main.InputData[1];
                    accountTypeInputCI = Main.InputData[2];
                    accountTypeInputIntCntrl= Main.InputData[3];
                }
                
                string sqlQuery = "select  *  from [ACT_ProductMaint] where [TC_reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string prodcodeRndm = generateUniqueProdCode(); //some random-number code
                string prod_DescDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Prod_Desc);
                string prod_Alt_DescDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Prod_Alt_Desc);
                string prod_IO_Acc_TypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Prod_IO_Acc_Type);
                string prod_CI_Acc_TypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Prod_CI_Acc_Type);
                string int_Control_CodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Int_Control_Code);
                string redemp_Control_CodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Redemp_Control_Code);
                string purchaseDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Purchase);
                string remortgageDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Remortgage);
                string customer_RetentionDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Customer_Retention);
                string prod_TypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Prod_Type);
                string payment_MethodDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Payment_Method);
                string def_UW_CodeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Def_UW_Code);
                string paymentDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Payment);
                string product_CategoryDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Product_Category);
                string further_AdvDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Further_Adv);
                string product_SwitchDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Product_Switch);
                string change_TermDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Change_Term);
                string change_Repay_TypeDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Change_Repay_Type);
                string balance_TransferDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_Balance_Transfer);
                string availableIntroducerDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_AvailableIntroducer);
                string societySeqDB=dbUtility.GetAccessFieldValue(TestDataConstants.Act_ProdMaintainAdd_SocietySeq);
                
                //Navigate to product Maintain screen
                navigateToProdMaintainenceAdd(societySeqDB);
                RxPath prodcode = fetchElement("Activate_Maintain_Product_Add_TXT_Prod_Code");
                RxPath prod_Desc = fetchElement("Activate_Maintain_Product_Add_TXT_Prod_Desc");
                RxPath prod_Alt_Desc = fetchElement("Activate_Maintain_Product_Add_TXT_Prod_Alt_Desc");
                RxPath prod_IO_Acc_Type = fetchElement("Activate_Maintain_Product_Add_LST_Prod_IO_Acc_Type");
                RxPath prod_CI_Acc_Type = fetchElement("Activate_Maintain_Product_Add_LST_Prod_CI_Acc_Type");
                RxPath int_Control_Code = fetchElement("Activate_Maintain_Product_Add_LST_Int_Control_Code");
                RxPath redemp_Control_Code = fetchElement("Activate_Maintain_Product_Add_LST_Redemp_Control_Code");
                RxPath purchase = fetchElement("Activate_Maintain_Product_Add_CBX_Purchase");
                RxPath remortgage = fetchElement("Activate_Maintain_Product_Add_CBX_Remortgage");
                RxPath customer_Retention = fetchElement("Activate_Maintain_Product_Add_CBX_Customer_Retention");
                RxPath prod_Type = fetchElement("Activate_Maintain_Product_Add_LST_Prod_Type");
                RxPath payment_Method = fetchElement("Activate_Maintain_Product_Add_LST_Payment_Method");
                RxPath def_UW_Code = fetchElement("Activate_Maintain_Product_Add_LST_Def_UW_Code");
                RxPath payment = fetchElement("Activate_Maintain_Product_Add_LST_Payment");
                RxPath product_Category = fetchElement("Activate_Maintain_Product_Add_LST_Product_Category");
                RxPath further_Adv = fetchElement("Activate_Maintain_Product_Add_RAWTXT_CBX_Further_Adv");
                RxPath product_Switch = fetchElement("Activate_Maintain_Product_Add_RAWTXT_CBX_Product_Switch");
                RxPath change_Term = fetchElement("Activate_Maintain_Product_Add_RAWTXT_CBX_Change_Term");
                RxPath change_Repay_Type = fetchElement("Activate_Maintain_Product_Add_RAWTXT_CBX_Change_Repay_Type");
                RxPath balance_Transfer = fetchElement("Activate_Maintain_Product_Add_RAWTXT_CBX_Balance_Transfer");
                RxPath availabletoIntroducer = fetchElement("Activate_Maintain_Product_Add_CBX_AvailabletoIntroducer");
                
                setTextValue(prodcode, prodcodeRndm);
                setTextValue(prod_Desc, prod_DescDB);
                setTextValue(prod_Alt_Desc, prod_Alt_DescDB);
                if(inputLen==2 && "NEW".Equals(prod_IO_Acc_TypeDB.ToUpper())){
                    prod_IO_Acc_TypeDB=accountTypeInput;
                }else if(inputLen==2 && "NEW".Equals(prod_CI_Acc_TypeDB.ToUpper())){
                    prod_CI_Acc_TypeDB=accountTypeInput;
                }else if(inputLen==2 && "NEW".Equals(int_Control_CodeDB.ToUpper())){
                    int_Control_CodeDB=accountTypeInput;
                }else if(inputLen==3 && "NEW".Equals(prod_IO_Acc_TypeDB.ToUpper()) && "NEW".Equals(prod_CI_Acc_TypeDB.ToUpper())){
                    prod_IO_Acc_TypeDB=accountTypeInput;
                    prod_CI_Acc_TypeDB=accountTypeInputCI;
                }else if(inputLen==3 && "NEW".Equals(prod_IO_Acc_TypeDB.ToUpper()) && "NEW".Equals(int_Control_CodeDB.ToUpper())){
                    prod_IO_Acc_TypeDB=accountTypeInput;
                    int_Control_CodeDB=accountTypeInputCI;
                }else if(inputLen==3 && "NEW".Equals(prod_CI_Acc_TypeDB.ToUpper()) && "NEW".Equals(int_Control_CodeDB.ToUpper())){
                    prod_CI_Acc_TypeDB=accountTypeInput;
                    int_Control_CodeDB=accountTypeInputCI;
                }else if(inputLen==4 && "NEW".Equals(prod_IO_Acc_TypeDB.ToUpper()) && "NEW".Equals(prod_CI_Acc_TypeDB.ToUpper()) && "NEW".Equals(int_Control_CodeDB.ToUpper())){
                    prod_IO_Acc_TypeDB=accountTypeInput;
                    prod_CI_Acc_TypeDB=accountTypeInputCI;
                    int_Control_CodeDB=accountTypeInputIntCntrl;
                }

                selectValueCIIOList(prod_IO_Acc_Type, prod_IO_Acc_TypeDB);
                selectValueCIIOList(prod_CI_Acc_Type, prod_CI_Acc_TypeDB);
                selectValueCIIOList(int_Control_Code, int_Control_CodeDB);
                selectValueListDropDownNoItemText(redemp_Control_Code, redemp_Control_CodeDB);
                setCheckbox(purchase, purchaseDB);
                setCheckbox(remortgage, remortgageDB);
                setCheckbox(customer_Retention, customer_RetentionDB);
                setRawTextCheckbox(further_Adv, further_AdvDB);
                setRawTextCheckbox(product_Switch, product_SwitchDB);
                setRawTextCheckbox(change_Term, change_TermDB);
                setRawTextCheckbox(change_Repay_Type, change_Repay_TypeDB);
                setRawTextCheckbox(balance_Transfer, balance_TransferDB);
                selectValueListDropDownNoItemText(prod_Type, prod_TypeDB);
                List pmtMethodLst = Host.Local.FindSingle(payment_Method).As<List>();
                ComboboxItemSelect(pmtMethodLst, payment_MethodDB);
                selectValueListDropDownNoItemText(def_UW_Code, def_UW_CodeDB);
                selectComboboxValue(payment, paymentDB);
                selectValueListDropDownNoItemText(product_Category, product_CategoryDB);
                
                setCheckbox(availabletoIntroducer, availableIntroducerDB);
                
                Button btnOk = fetchElement("Activate_Maintain_Product_Add_BTN_OK");
                btnOk.Click();
                
                searchProdAddAvailability(prodcodeRndm);
                
                Button btnClose = fetchElement("Activate_Product_Maintenance_BTN_Close");
                btnClose.Click();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(prodcodeRndm);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void selectValueListDropDownNoItemText(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    List lst = Host.Local.FindSingle(path).As<List>();
                    ListItemSelectRawTxt(lst, val);
                }
            }
        }

        private void selectValueCIIOList(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    List lst = Host.Local.FindSingle(path).As<List>();
                    ComboboxItemSelect(lst,val);
                }
            }
        }
        
        private string generateUniqueProdCode(){
            int maxRetry=Int32.Parse(Settings.getInstance().get("ACTIVATE_PRODCODE_MAX_RETRY"));
            List<string[]> result = new List<string[]>();
            string generatedProdCode = string.Empty;
            string productCodePrefix = "AUT#";
            string query;
            while(maxRetry-->0)
            {
                Random rnd = new Random();
                int num = rnd.Next(100000,999999);
                string x = num.ToString();
                x=x.PadLeft(6,'0');
                generatedProdCode=productCodePrefix.Replace("#",x);
                
                //MSD-73110-MSS5.7-13-feb-2023: corrected query statement.
                query="select 1 from acm02 where prodcode='"+generatedProdCode+"'";
                
                result=OracleUtility.Instance().executeQuery(query);
                //Randomely generated Product number doesn't exists - safe to use.
                if(result.Count == 0){
                    Report.Info("Generated Unique ProductCode="+generatedProdCode);
                    return generatedProdCode;
                }
            }
            if(result.Count == 0 && maxRetry == 0){
                Report.Error("Failed to generated Unique ProductCode after max retries.");
            }
            return generatedProdCode;
        }
        
        private void setRawTextCheckbox(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    RawText cbx = Host.Local.FindSingle(path).As<RawText>();
                    if(val.ToUpper().Equals("Y")){
                        cbx.Click();
                    }
                }
            }
        }
        
        private void selectComboboxValue(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    ComboBox cmbx = Host.Local.FindSingle(path).As<ComboBox>();
                    ComboboxItemSelect(cmbx, val);
                }
            }
        }
        
        private void navigateToProdMaintainenceAdd(String societySeq){
            MenuItem fieldMenu = fetchElement("Activate_MenuItems_MENUITEM_Menu");
            fieldMenu.EnsureVisible();
            if(fieldMenu.Visible){
                fieldMenu.Click();
            }else{
                Report.Error("MenuItem Field is not Clickable");
            }
            
            TreeItem topMenu = fetchElement("Activate_Tree_TreeItem_Top_Menu");
            topMenu.Click();
            
            arrLstMenuTree.Clear();
            arrLstMenuTree.Add("Top Menu");
            arrLstMenuTree.Add("Static Data/ System Utilities");
            arrLstMenuTree.Add("Product and Underwriting");
            
            TreeItemActivate();
            RawText prodMaintenance = fetchElement("Activate_Product_Underwriting_RAWTXT_Product_Maintenance");
            prodMaintenance.Click();
            
            if(!string.IsNullOrEmpty(societySeq)){
                if(!societySeq.ToUpper().Equals("DEFAULT")){
                    RxPath societyRx=fetchElement("Activate_Product_Maintenance_LST_Society");
                    if(societySeq.Equals("1")){
                        selectValueListDropDown(societyRx,"Touchstone Financial");
                    }else if (societySeq.Equals("2")){
                        selectValueListDropDown(societyRx,"Touchstone Financial (Ireland)");
                    }else{
                        throw new Exception("Society data is not valid--"+societySeq);
                    }
                }
            }
            
            Button btnAdd = fetchElement("Activate_Product_Maintenance_BTN_Add");
            btnAdd.Click();
            
        }
        
        private void searchProdAddAvailability(string valProdCode){
            Text txtProduct = fetchElement("Activate_Product_Maintenance_TXT_Product");
            setText(txtProduct, valProdCode);
            Button btnSearch = fetchElement("Activate_Product_Maintenance_BTN_Search");
            btnSearch.Click();
            
            RxPath txtProdCodeTbl1stValueRx = fetchElement("Activate_Product_Maintenance_Tbl_Col_TXT_Product_1st_Value");
            Text txtProdCodeTbl1stValue = Host.Local.FindSingle(txtProdCodeTbl1stValueRx, duration).As<Text>();
            if(txtProdCodeTbl1stValue.TextValue.ToLower().Equals(valProdCode.ToLower())){
                Report.Info("Prodcut code added successfully");
                Button btnAvailability = fetchElement("Activate_Product_Maintenance_BTN_Availability");
                btnAvailability.Click();
                Button btnAdd = fetchElement("Activate_Maintain_Product_Availability_Type_BTN_Add");
                btnAdd.Click();
                RxPath rxPath = fetchElement("Activate_Maintain_Product_Availability_Type_LST_Availability_Type");
                List lst = Host.Local.FindSingle(rxPath).As<List>();
                ListtemSelectDirectPath(lst, "Available To All");
                Button btnOk = fetchElement("Activate_Maintain_Product_Availability_Type_BTN_OK");
                btnOk.Click();
            }else{
                throw new Exception("Product code value does not exist/match");
            }
            
        }
    }
}
