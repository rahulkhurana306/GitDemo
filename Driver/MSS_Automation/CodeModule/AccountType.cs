/*
 * Created by Ranorex
 * User: pratagarwal
 * Date: 04/01/2023
 * Time: 01:04
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using ng_mss_automation.CodeModule;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of CorporateCustomerCreate.
    /// </summary>
    public partial class Keywords
    {
        public List<string> AccountType(){
            try
            {
                string societyFrom = Main.InputData[0];
                string accountTypeFrom = Main.InputData[1];
                string societyTo = Main.InputData[2];
                String newAccountType = "";
                MenuPromptInternal("ACTC");
                
                Text societyNoFrom = fetchElement("Summit_Account_Type_Clone_TXT_Society");
                Text accountTypeFromApp = fetchElement("Summit_Account_Type_Clone_TXT_AccTypeFrom");
                Text societyNoTo = fetchElement("Summit_Account_Type_Clone_TXT_toSocietyNo");
                Text accountTypeTo = fetchElement("Summit_Account_Type_Clone_TXT_AccTypeTo");
                Text accountTypeLongName = fetchElement("Summit_Account_Type_Clone_TXT_AccTypeLongName");
                Text accountTypeShortName = fetchElement("Summit_Account_Type_Clone_TXT_AccTypeShortName");
                
                InputText(societyNoFrom, societyFrom);
                InputText(accountTypeFromApp, accountTypeFrom);
                InputText(societyNoTo, societyTo);
                newAccountType=utility.GenerateUniqueAccountType();
                InputText(accountTypeTo, newAccountType);
                InputText(accountTypeLongName, "New Account"+""+newAccountType);
                InputText(accountTypeShortName, "New"+""+newAccountType);
                Keyboard.Press("{F3}");
                Delay.Seconds(2);
                Keyboard.Press("{F4}");
                
                MenuPromptInternal("ACTV");
                Text societyValiate = fetchElement("Summit_Account_Type_Validate_TXT_Society");
                Text accountTypeValidate = fetchElement("Summit_Account_Type_Validate_TXT_AccountType");
                
                InputText(societyValiate, societyTo);
                InputText(accountTypeValidate, newAccountType);
                Keyboard.Press("{F11}");
                Button validate = fetchElement("Summit_Account_Type_Validate_BTN_Validate");
                validate.Click();
                Button ok = fetchElement("Summit_Account_Type_Validate_BTN_Ok");
                ok.Click();
                Button createLink = fetchElement("Summit_Account_Type_Validate_BTN_CreateLink");
                createLink.Click();
                
                Text societyNoFromClone = fetchElement("Summit_Global_Transaction_Links_Clone_TXT_Society1");
                Text accountTypeClone = fetchElement("Summit_Global_Transaction_Links_Clone_TXT_AccountType1");
                Text societyNoToClone = fetchElement("Summit_Global_Transaction_Links_Clone_TXT_Society2");
                Text accountType2Clone = fetchElement("Summit_Global_Transaction_Links_Clone_TXT_AccountType2");
                Text dateEffectiveFrom = fetchElement("Summit_Global_Transaction_Links_Clone_TXT_EffectiveFrom");
                                
                InputText(societyNoFromClone, societyFrom);
                InputText(accountTypeClone, accountTypeFrom);
                Keyboard.Press("{TAB}");
                InputText(societyNoToClone, societyTo);
                InputText(accountType2Clone,newAccountType);
                //InputText(dateEffectiveFrom,utility.ProcessWCALDate("WCAL_DATE"));
                Keyboard.Press("{TAB}");
                Keyboard.Press("{F3}");
                Delay.Seconds(3);
                Keyboard.Press("{F4}");
                
                Button createProcessType = fetchElement("Summit_Account_Type_Validate_BTN_CreateProcessType");
                createProcessType.Click();
                
                Text societyProcessType = fetchElement("Summit_Create_Process_Type_TXT_Society1");
                Text accountCodeProcessType = fetchElement("Summit_Create_Process_Type_TXT_AccountCode1");
                Text society2ProcessType = fetchElement("Summit_Create_Process_Type_TXT_Society2");
                Text accountCode2ProcessType = fetchElement("Summit_Create_Process_Type_TXT_AccountCode2");
                Text effectiveDateProcessType = fetchElement("Summit_Create_Process_Type_TXT_EffectiveDate");
                 
                InputText(societyProcessType, societyFrom);
                InputText(accountCodeProcessType, accountTypeFrom);
                InputText(society2ProcessType, societyTo);
                InputText(accountCode2ProcessType, newAccountType);
                InputText(effectiveDateProcessType,utility.ProcessWCALDate("WCAL_DATE"));
                Keyboard.Press("{F3}");
                Delay.Seconds(3);
                Keyboard.Press("{F4}");
                Keyboard.Press("{F4}");
                
                MenuPromptInternal("ACTS");
                
                Text societySchedule = fetchElement("Summit_Account_Type_Schedule_TXT_Society");
                Text accountTypeSchedule = fetchElement("Summit_Account_Type_Schedule_TXT_AccountType");
                Button scheduleofSchedule = fetchElement("Summit_Account_Type_Schedule_BTN_Schedule");
                
                InputText(societySchedule, societyTo);
                InputText(accountTypeSchedule, newAccountType);
                 Keyboard.Press("{F11}");
                scheduleofSchedule.Click();
                Delay.Seconds(1);
                Button scheduleAlertSchedule = fetchElement("Summit_Account_Type_Schedule_BTN_ScheduleAlert");
                scheduleAlertSchedule.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button lastRecordSchedule = fetchElement("Summit_Account_Type_Schedule_BTN_LastRecord");
                lastRecordSchedule.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Keyboard.Press("{F4}");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                MenuPromptInternal("GTLV");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Text societyValidate = fetchElement("Summit_Global_Transaction_Links_Validate_TXT_Society");
                Text accountTypeValidateFirst = fetchElement("Summit_Global_Transaction_Links_Validate_TXT_AccountType ");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                InputText(societyValidate, societyTo);
                InputText(accountTypeValidateFirst, newAccountType);
                Button searchValidate = fetchElement("Summit_Global_Transaction_Links_Validate_BTN_Search");
                searchValidate.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button selectAllValidate = fetchElement("Summit_Global_Transaction_Links_Validate_BTN_SelectAll");
                selectAllValidate.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button validateOfValidate = fetchElement("Summit_Global_Transaction_Links_Validate_BTN_Validate");
                validateOfValidate.Click();
                Delay.Seconds(4);
                Button validationScuccessValidate = fetchElement("Summit_Global_Transaction_Links_Validate_BTN_ValidationSuccess");
                validationScuccessValidate.Click();
                Keyboard.Press("{F4}");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                MenuPromptInternal("GTLS");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Text societySchedulelast = fetchElement("Summit_Global_Transaction_Links_Schedule_TXT_Society");
                Text accountTypeSchedulelast = fetchElement("Summit_Global_Transaction_Links_Schedule_TXT_AccountType");
                
                InputText(societySchedulelast, societyTo);
                InputText(accountTypeSchedulelast, newAccountType);
                Button searchSchedulelast = fetchElement("Summit_Global_Transaction_Links_Schedule_BTN_Search");
                searchSchedulelast.Click();
                Button selectAllSchedulelast = fetchElement("Summit_Global_Transaction_Links_Schedule_BTN_SelectAll");
                selectAllSchedulelast.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button scheduleOfSchedulelast = fetchElement("Summit_Global_Transaction_Links_Schedule_BTN_Schedule");
                scheduleOfSchedulelast.Click();
                Delay.Seconds(4);
                Keyboard.Press("{F4}");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button okSchedulelast = fetchElement("Summit_Global_Transaction_Links_Schedule_BTN_Okay");
                okSchedulelast.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Button alertOkSchedulelast = fetchElement("Summit_Global_Transaction_Links_Schedule_BTN_AlertOk");
                alertOkSchedulelast.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Keyboard.Press("{F4}");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(newAccountType);
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

