/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 21/11/2022
 * Time: 12:00
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
    /// Description of IllustrationGeneralDetails.
    /// </summary>
    public partial class Keywords
    {
        public List<string> IllustrationGeneralDetails()
        {
            try{
                Main.appFlag = Constants.appActivate;
                OpenMacro(TestDataConstants.Act_IllGenDetails_Macro);
                
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
    }
}
