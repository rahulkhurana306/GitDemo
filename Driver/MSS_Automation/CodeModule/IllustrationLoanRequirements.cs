/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 22/11/2022
 * Time: 06:51
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
    /// Description of IllustrationLoanRequirements.
    /// </summary>
    public partial class Keywords
    {
        public List<string> IllustrationLoanRequirements()
        {
            try{
                Main.appFlag = Constants.appActivate;
                OpenMacro(TestDataConstants.Act_IllLoanDets_Macro);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                
                LoanRequirementsFill();
                MortgageDetailsFill();
                CashbackDetails();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
                
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
            
        }
        
        private void CashbackDetails(){
            RxPath btnOkRx = fetchElement("Activate_Cashback Details_BTN_OK");
            Button btnOk = null;
            Duration durationTimePS = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_LONG_WAIT")));
            if(Host.Local.TryFindSingle(btnOkRx, durationTimePS,out btnOk)){
                btnOk.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            }
        }
    }
}
