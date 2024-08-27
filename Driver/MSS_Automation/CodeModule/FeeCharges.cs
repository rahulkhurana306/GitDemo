/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 06/09/2022
 * Time: 09:09
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
    /// Description of FeeCharges.
    /// </summary>
    public partial class Keywords
    {
        public List<string> FeeCharges()
        {
            try{
                
                Main.appFlag = Constants.appActivate;
                waitForPagetoAppear(TestDataConstants.Page_TopMenu);
                OpenMacro(TestDataConstants.Act_FeeCharges_Macro);
                Button btnOkFeeChrgs = fetchElement("Activate_Fee Charges_BTN_OK");
                btnOkFeeChrgs.Click();
                //waitForPagetoAppear(TestDataConstants.Page_TopMenu);
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
    }
}
