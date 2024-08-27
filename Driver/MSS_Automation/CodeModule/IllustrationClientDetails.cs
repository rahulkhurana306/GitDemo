/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 21/11/2022
 * Time: 18:19
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
    /// Description of IllustrationClientDetails.
    /// </summary>
    public partial class Keywords
    {
        public List<string> IllustrationClientDetails()
        {
            try{
                Main.appFlag = Constants.appActivate;
                OpenMacro(TestDataConstants.Act_IllClientDetails_Macro);
                ClientDetailsFunctionality();
                CreditInformation();
                OccupancyDetails();
                ClientEmploymentDetails();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                string clientNumber = string.Empty;
                if(!FAFlag){
                    clientNumber = fetchClientNumFromDB();
                }
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                if(!FAFlag){
                    Main.OutputData.Add(clientNumber);
                }
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
    }
}
