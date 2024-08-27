/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 19/10/2022
 * Time: 10:40
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
    /// Description of SetApplicationContext.
    /// </summary>
    public partial class Keywords
    {
        public List<string> SetApplicationContext()
        {
            try{
                Main.appFlag = Constants.appActivate;
                string accountNum = Main.InputData[0];
                
                MenuItem search = fetchElement("Activate_MenuItems_MENUITEM_Search");
                search.Click();
                MenuItem application = fetchElement("Activate_MenuItems_MENUITEM_Application");
                application.Click();
                waitForPagetoAppear(TestDataConstants.Page_MortDetails);
                string mortNumberInput = accountNum;
                Text mortNumber = fetchElement("Activate_Application_General_Details_TXT_FA_mortNmber");
                if(string.IsNullOrEmpty(mortNumber.TextValue)){
                    setText(mortNumber, mortNumberInput);
                }else{
                    mortNumber.DoubleClick();
                    Keyboard.Press("{Delete}");
                    setText(mortNumber, mortNumberInput);
                }
                Button mortOk = fetchElement("Activate_Application_General_Details_Btn_FA_mortOk");
                mortOk.Click();
                waitForPagetoAppear(TestDataConstants.Page_TopMenu);
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
                
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> AdvanceType_Flag()
        {
            try{
                Main.appFlag = Constants.appActivate;
                string operation = Main.InputData[0];
                string flag = Main.InputData[1];
                AdvTypeFalgToSet(operation, flag);
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void AdvTypeFalgToSet(string opts, string flag){
            if(opts.ToUpper().Equals("SET")){
                if(flag.ToUpper().Equals("FA")){
                    FAFlag = true;
                }else if(flag.ToUpper().Equals("RM")){
                    RMFlag = true;
                }else if(flag.ToUpper().Equals("MV") || flag.ToUpper().Equals("PS")){
                    MVFlag = true;
                }else if(flag.ToUpper().Equals("LM")){
                    LMFlag = true;
                }else if(flag.ToUpper().Equals("TOE")){
                    TOEFlag = true;
                }
            }else if(opts.ToUpper().Equals("UNSET")){
                if(flag.ToUpper().Equals("FA")){
                    FAFlag = false;
                }else if(flag.ToUpper().Equals("RM")){
                    RMFlag = false;
                }else if(flag.ToUpper().Equals("MV") || flag.ToUpper().Equals("PS")){
                    MVFlag = false;
                }else if(flag.ToUpper().Equals("LM")){
                    LMFlag = false;
                }else if(flag.ToUpper().Equals("TOE")){
                    TOEFlag = false;
                }
            }
        }
    }
}
