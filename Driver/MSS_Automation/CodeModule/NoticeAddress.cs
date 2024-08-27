/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 01/09/2022
 * Time: 15:47
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
    /// Description of NoticeAddress.
    /// </summary>
    public partial class Keywords
    {
        public List<string> NoticeAddress()
        {
            try{
                
                Main.appFlag = Constants.appActivate;
                OpenMacro(TestDataConstants.Act_NoticeAddress_Macro);
                if(!(FAFlag || MVFlag || TOEFlag)){
                    string sqlQuery = "select  *  from [ACT_Notice_Address] where [Reference] = '"+Main.InputData[0]+"'";
                    dbUtility.ReadDBResultMS(sqlQuery);
                    string noticeAddressDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_NoticeAddress_NoticeAddress);
                    string summitAddressDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_NoticeAddress_SummitAddress);
                    string letterSalutationDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_NoticeAddress_LetterSalutation);
                    string clientAddressToUseDB = dbUtility.GetAccessFieldValue(TestDataConstants.Act_NoticeAddress_ClientAddressToUse);
                    
                    SelectNoticeAddressRadioButton(noticeAddressDB);
                    
                    CheckBox summitAddress = fetchElement("Activate_Notice Address_CBX_Summitaddress");
                    if(!string.IsNullOrEmpty(summitAddressDB)){
                        checkboxOperation(summitAddressDB, summitAddress);
                    }
                    
                    if(!string.IsNullOrEmpty(letterSalutationDB)){
                        if(!letterSalutationDB.ToLower().Equals("default")){
                            Text letterSalutation = fetchElement("Activate_Notice Address_TXT_LetterSalutation");
                            setText(letterSalutation, letterSalutationDB);
                        }
                    }
                    
                    string clientAddressToUsePre = fetchElement("Activate_Notice Address_RAD_ClientAddressToUse_PreFix");
                    string clientAddressToUsePost = fetchElement("Activate_Notice Address_RAD_ClientAddressToUse_PostFix");
                    
                    RadioButton clientAddressToUse = clientAddressToUsePre+clientAddressToUseDB+clientAddressToUsePost;
                    radioButtonSelect(clientAddressToUse);
                }
                Button btnOk = fetchElement("Activate_Notice Address_BTN_OK");
                btnOk.Click();
                Delay.Seconds(3);
                
                //waitForPagetoAppear(TestDataConstants.Page_TopMenu);
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
            
        }
        
        private void SelectNoticeAddressRadioButton(string option){
            if(option.ToUpper().Equals("OCCUPANCY")){
                RadioButton optionRd = fetchElement("Activate_Notice Address_RAD_Occupancy");
                radioButtonSelect(optionRd);
            }else if(option.ToUpper().Equals("CORRESPONDENCE")){
                RadioButton optionRd = fetchElement("Activate_Notice Address_RAD_Correspondence");
                radioButtonSelect(optionRd);
            }else if(option.ToUpper().Equals("PROPERTY")){
                RadioButton optionRd = fetchElement("Activate_Notice Address_RAD_Property");
                radioButtonSelect(optionRd);
            }else if(option.ToUpper().Equals("OTHERS")){
                RadioButton optionRd = fetchElement("Activate_Notice Address_RAD_Others");
                radioButtonSelect(optionRd);
            }
        }
    }
}
