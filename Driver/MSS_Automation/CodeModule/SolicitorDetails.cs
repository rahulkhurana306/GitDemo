/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 02/09/2022
 * Time: 12:54
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
    /// Description of SolicitorDetails.
    /// </summary>
    public partial class Keywords
    {
        public List<string> SolicitorDetails()
        {
            try{
                
                Main.appFlag = Constants.appActivate;
                OpenMacro(TestDataConstants.Act_SolicitorDetails_Macro);
                string sqlQuery = "select  *  from [ACT_Solicitor_Details] where [Reference] = '"+Main.InputData[0]+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                string applicantNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_SolicitorDetails_ApplicantName);
                string lenderNameDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_SolicitorDetails_LenderName);
                string paymentToAboveAccountDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_SolicitorBankDetails_PaymentToAboveAccount);                
                
                Button searchApplicant = fetchElement("Activate_Solicitor Details_BTN_Name_Solicitor_Search");
                searchApplicant.Click();
                Text solicitorName = fetchElement("Activate_Solicitor Search_TXT_Name");
                setText(solicitorName, applicantNameDB);
                Button searchSolicitor = fetchElement("Activate_Solicitor Search_BTN_Search");
                searchSolicitor.Click();
                Ranorex.Core.RxPath col_0 = fetchElement("Activate_Solicitor Search_TBL_RawText_Col_SolicitorName");
                DoubleClickActivateTableCell(applicantNameDB, col_0);
                
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Text applicantName = fetchElement("Activate_Solicitor Details_TXT_ApplicantName");
                Text lenderName = fetchElement("Activate_Solicitor Details_TXT_LenderName");
                if(!string.IsNullOrEmpty(applicantName.TextValue) && !string.IsNullOrEmpty(lenderName.TextValue)){
                    Button okSolicitorDetails = fetchElement("Activate_Solicitor Details_BTN_OK");
                    okSolicitorDetails.Click();
                }else{
                    Report.Error("Application/Lender name is not populated.");
                }                
                
                if(!string.IsNullOrEmpty(paymentToAboveAccountDB)){
                    CheckBox paymentToAboveAccount = fetchElement("Activate_Solicitor Bank Details_CBX_Payment_to_Above_Acc");
                    checkboxOperation(paymentToAboveAccountDB, paymentToAboveAccount);
                }
                
                Button okSolicitorBankDetails = fetchElement("Activate_Solicitor Bank Details_BTN_OK");
                okSolicitorBankDetails.Click();
                
                //waitForPagetoAppear(TestDataConstants.Page_TopMenu);
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void DoubleClickActivateTableCell(string searchValue, RxPath rxPath){
            IList<Element> list = Host.Local.Find(rxPath);
            string cellValue = string.Empty;
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            for(int i=1;i<=list.Count;i++){
                cellValue = list[i].As<RawText>().RawTextValue;
                if(cellValue.Trim().Equals(searchValue, StringComparison.OrdinalIgnoreCase )){
                    list[i].As<RawText>().DoubleClick();
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                    break;
                }
            }
        }
    }
}
