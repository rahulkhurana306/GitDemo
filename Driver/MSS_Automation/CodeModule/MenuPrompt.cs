/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 20/06/2022
 * Time: 12:28
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
    /// Description of MenuPrompt.
    /// </summary>
    public partial class Keywords
    {
        public List<string> MenuPrompt()
        {
            try
            {
                string screenPrompt = Main.InputData[0];
                MenuPromptInternal(screenPrompt);
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }
            catch(Exception e)
            {
                Report.Error(e.StackTrace);
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
    }
}
