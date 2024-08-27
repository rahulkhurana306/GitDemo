/*
 * Created by Ranorex
 * User: gaursingh
 * Date: 23/06/2022
 * Time: 14:27
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
    /// Description of CallCenterGlobalVariable.
    /// </summary>
    /// 
    public partial class Keywords{
        
        public List<string> CallCenterGlobalVariable(){
            try
            {
                string globalClass = Main.InputData[0];
                string globalName = Main.InputData[1];
                string globalValue = Main.InputData[2];
                string OLD_VALUE;
                MenuPromptInternal("CCGVM");
                Text GlobalClass = fetchElement("Summit_Maintain_Toolkit_Globals_TXT_GlobalClass");
                Text GlobalName = fetchElement("Summit_Maintain_Toolkit_Globals_TXT_GlobalName");
                Text GlobalValue = fetchElement("Summit_Maintain_Toolkit_Globals_TXT_GlobalValue");
                Button BTN_OK = fetchElement("Summit_Maintain_Toolkit_Globals_BTN_OK");
                
                GlobalClass.PressKeys("{F12}");
                InputText(GlobalClass, globalClass);
//                setText(GlobalName, globalName);                  //replaced this code with Keyboard.Press, as setText stopped working for GlobalName field
                Keyboard.Press(globalName);
                GlobalName.PressKeys("{F11}");
                Utility.Capture_Screenshot();
                OLD_VALUE = GlobalValue.Element.GetAttributeValueText("Text");
                
                setText(GlobalValue, globalValue);
                Utility.Capture_Screenshot();
                BTN_OK.Click();
                
                BTN_OK = fetchElement("Summit_Notification_Information_BTN_OK");
                BTN_OK.Click();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(OLD_VALUE);
                return Main.OutputData;
            }
            catch (Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Report.Error(e.StackTrace);
                return Main.OutputData;
            }
        }

    }
}