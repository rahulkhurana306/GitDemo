/*
 * Created by Ranorex
 * User: PROJ-MSS-ATEST03.SVC
 * Date: 03/06/2022
 * Time: 11:02
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ng_mss_automation.CodeModule;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Plugin;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of ActionKeywords.
    /// </summary>
    public partial class Keywords
    {
        
        public int PID = 0;
        public Text fieldText = null;
        public Button fieldButton = null;
        public ComboBox fieldCombo = null;
        public string sData = null;
        public string sPageObj = null;
        public string getValuefromUI = null;
        public string val = null;
        
        public List<string> LAUNCH_APPLICATION(){
            try{
                string appName = Main.InputData[0];
                if(appName.ToUpper().Equals("SUMMIT")){
                    PID = Host.Local.RunApplication(Constants.Path_Summit, "-url \""+ Settings.getInstance().get("SUMMIT_ENV_URL") +"\"", "", false);
                    //Delay.Seconds(3);
                    waitForPagetoLoadCompletely(TestDataConstants.logon);
                    Main.appFlag = Constants.appSummit;
                    if(Host.Local.Find(fetchElement("Summit_Welcome_To_Summit_TXT_Username"),Duration.FromMilliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")))).Count!=0){
                        Keyboard.Press("{Tab}");
                        Main.OutputData.Add("Pass");
                    }
                    else{
                        Main.OutputData.Add("Fail");
                    }
                }else if(appName.ToUpper().Equals("ACTIVATE")){
                    PID = Host.Local.RunApplication("sam.exe","",Settings.getInstance().get("ACTIVATE_DIR"), false);
                    //To add the process name to the config
                    RawTextFlavor.Instance.ProcessNames.Add(new Regex("sam"));
                    Delay.Seconds(5);
                    Main.appFlag = Constants.appActivate;
                    Main.OutputData.Add("Pass");
                }

                Report.Info("Application launched with PID="+PID);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> SETVALUE(){
            try{
                sPageObj = Main.sElement;
                sData = Main.InputData[0];
                string appFlg = Main.appFlag;
                if(appFlg.Equals(Constants.appActivate)){
                    if(sPageObj.Contains("rawtext")){
                        enterRawText(sPageObj, sData);
                    }else{
                        enterText(sPageObj, sData);
                    }
                    
                }else{
                    InputTextInternal(sPageObj, sData);
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> SETDATE(){
            try{
                sPageObj = Main.sElement;
                try{
                    sData = utility.ProcessWCALDate(Main.InputData[0],Main.InputData[1]);
                    
                    Main.OutputData.Add(Constants.TS_STATUS_PASS);
                    
                }catch (Exception e){
                    Report.Error("Error while processing keyword SETDATE."+e.Message);
                    sData=Main.InputData[1];
                    Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                }
                string appFlg = Main.appFlag;
                if(appFlg.Equals(Constants.appActivate)){
                    Text ele = sPageObj;
                    if(!string.IsNullOrEmpty(ele.TextValue)){
                        if(!(ele.TextValue.Equals("00/00/0000"))){
                            ele.DoubleClick();
                            ele.PressKeys("{Delete}");
                        }
                    }
                    ele.PressKeys(sData+"{Tab}");
                }else{
                    InputText(sPageObj, sData);
                }
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
            
        }
        
        public List<string> CLICK(){
            //Below logic will work only if XPATH contains form and then specific element only.
            //otherwise split by / and then in the last token look for batching content.
            try{
                sPageObj = Main.sElement;
                
                if (sPageObj.Contains("radiobutton")){
                    RadioButton radButton = sPageObj;
                    radButton.Click();
                }else if(sPageObj.Contains("/button")){
                    Button btn = sPageObj;
                    ClickButton(btn);
                }else if (sPageObj.Contains("rawtext")){
                    try{
                        //suitable for ACTIVATE
                        ClickRawText(sPageObj);
                    }catch(Exception){
                        Delay.Seconds(1);
                        ClickRawText(sPageObj);
                    }
                }else if (sPageObj.Contains("tabpage")){
                    TabPage tabbedRegion = sPageObj;
                    tabbedRegion.Click();
                }
                else if (sPageObj.Contains("listitem")){
                    ListItem listitem = sPageObj;
                    listitem.Click();
                }
                else if (sPageObj.Contains("list")){
                    Ranorex.List list = sPageObj;
                    list.Click();
                }
                else if (sPageObj.Contains("checkbox")){
                    CheckBox checkbox = sPageObj;
                    checkbox.Click();
                }
                else if (sPageObj.Contains("/text")){
                    Text text = sPageObj;
                    text.Click();
                }
                else if (sPageObj.Contains("treeitem")){
                    TreeItem tItem = sPageObj;
                    tItem.Click();
                }
                else if (sPageObj.Contains("menuitem")){
                    MenuItem menuItem = sPageObj;
                    menuItem.Click();
                }
                else if (sPageObj.Contains("/combobox")){
                    ComboBox combx = sPageObj;
                    combx.Click();
                }
                else if (sPageObj.Contains("/a")){
                    WebElement hypLink = sPageObj;
                    hypLink.Click();
                }
                Main.OutputData.Add("Pass");
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
            
        public List<string> SELECT_DROPDOWN_BY_CLICK(){
            try{
                sPageObj = Main.sElement;
                sData = Main.InputData[0];
                string appFlg = Main.appFlag;
                if(appFlg.Equals(Constants.appSummit) && !sPageObj.Contains("list")){
                    Element element = sPageObj;
                    element.Focus();
                    element.SetAttributeValue("SelectedItemText",sData);
                    Keyboard.Press("{ENTER}");
                }else if(appFlg.Equals(Constants.appActivate)){
                    if(sPageObj.Contains("/list")){
                        Ranorex.List lst = sPageObj;
                        ListtemSelectDirectPath(lst, sData);
                    }else if(sPageObj.Contains("/combobox")){
                        ComboBox cmbx = sPageObj;
                        ComboboxItemSelectDirectPath(cmbx, sData);
                    }
                }
                
                Main.OutputData.Add("Pass");
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> SELECT_DROPDOWN_DEFAULT_VALUE(){
            try{
                sPageObj = Main.sElement;
                sData = Main.InputData[0];
                if(sPageObj.Contains("/list")){
                    Ranorex.List lst = sPageObj;
                    ListtemSelectDirectPathDefaultSelected(lst, sData);
                }
                Main.OutputData.Add("Pass");
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> CHECK(){
            try{
                sPageObj = Main.sElement;
                CheckBox cbElement = sPageObj;
                cbElement.Check();
                
                Main.OutputData.Add("Pass");
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> CLOSE_APPLICATION(){
            try{
                sData = Main.InputData[0];
                if(sData.ToLower().Contains("summit")){
                    closeAppSummit();
                }
                else if(sData.ToLower().Contains("activate")){
                    closeAppActivate();
                }
                
                Main.OutputData.Add("Pass");
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> WAITFORFORM(){
            try{
                sData = Main.InputData[0];
                waitForPagetoAppear(sData);
                
                Main.OutputData.Add("Pass");
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        //---------------internally called methods declared here
        
        private void InputText( Text obj, string data){
            obj.EnsureVisible();
            if(Main.InputData.Contains(data)){
                obj.PressKeys(data+"{Tab}");
            }else if (!data.StartsWith(Constants.TestData_DEFAULT)){
                obj.PressKeys(data+"{Tab}");
            }
        }
        
        private void InputTextInternal( Text obj, string data){
            obj.EnsureVisible();
            obj.PressKeys(data+"{Tab}");
        }
        
        private void InputText_Advanced( Text obj, string data){
            obj.EnsureVisible();
            if (!data.StartsWith(Constants.TestData_DEFAULT)){
                Element element = obj;
                element.Focus();
                element.SetAttributeValue("Text",data);
                Keyboard.Press("{TAB}");
            }
        }
        
        private void ClickText( Text obj){
            obj.EnsureVisible();
            obj.Click();
        }
        
        private void ClickRawText(RawText obj){
            obj.EnsureVisible();
            obj.Click();
        }
        
        private void ClickButton( Button obj){
            try{
                obj.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void ClickButtonRx(RxPath fieldRx){
            try{
                string btnStr = fieldRx.ToString();
                Button btn = btnStr;
                btn.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void SelectValue(ComboBox obj, String Value){
            obj.EnsureVisible();
            obj.PressKeys(Value);
            
        }
        
        private void ComboboxItemSelect(Ranorex.List cmbox, string text){
            cmbox.Click();
            cmbox.PressKeys(text);
            RawText value = "/form[@title='']/rawtext[@RawText='"+text+"']";
            value.Click();
        }
        
        private void ComboboxItemSelect(ComboBox cmbox, string text){
            cmbox.Click();
            cmbox.PressKeys(text);
            RawText value = "/form[@title='']/rawtext[@RawText='"+text+"']";
            value.Click();
        }
        
        private void ComboboxItemSelectDirect(Ranorex.List cmbox, string text){
            cmbox.Click();
            RawText value = "/form[@title='']/rawtext[@RawText='"+text+"']";
            value.Click();
        }
        
        private void ComboboxItemSelectDirect(ComboBox cmbox, string text){
            cmbox.Click();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            RawText value = "/form[@title='']/rawtext[@RawText='"+text+"']";
            value.Click();
        }
        
        private void ComboboxItemSelectE(Ranorex.List cmbox, string text){
            cmbox.Click();
            string val = text.Split(' ')[0];
            cmbox.PressKeys(val);
            RawText value = "/form[@title='']/rawtext[@RawText='"+text+"']";
            value.Click();
        }
        
        private bool waitForPagetoAppear(string header){
            bool status = false;
            try{
                for(int i=0;i<Constants.defaultActivatePageTimeOut;i++){
                    int count = Host.Local.Find("/form[@processid='"+PID+"']//titlebar[@accessiblerole='TitleBar' and @accessiblevalue<'"+header+"']").Count;
                    if(count>=1){
                        status= true;
                        break;
                    }
                }
                return status;
            }catch(Exception e){
                Report.Info(e.Message);
                return status;
            }
            
        }
        
        public void closeAppSummit(){
            if( PID !=0 ){
                try {
                    Host.Local.CloseApplication(PID);
                    Button btnOk = null;
                    RxPath btnRx = fetchElement("Summit_Close_Summit_BTN_Close_Summit");
                    Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                    if(Host.Local.TryFindSingle(btnRx, durationTime,out btnOk)){
                        btnOk.Click();
                    }
                    Process.GetProcessById(PID);
                } catch (Exception e) {
                    Report.Info("Exception while closing from functional keyword."+e.Message);
                }
            }
            
            PID = 0;
        }
        
        public void closeAppActivate(){
            try{
                Host.Local.CloseApplication(PID);
            }catch(Exception){
                Host.Local.KillApplication(PID);
            }
            PID = 0;
        }
        
        public List<string> SELECT_FROM_DROPDOWN(){
            try{
                string appFlg = Main.appFlag;
                sPageObj = Main.sElement;
                sData = Main.InputData[0];
                if(appFlg.Equals(Constants.appSummit) && !sPageObj.Contains("list")){
                    SelectValue(sPageObj, sData);
                }else if(appFlg.Equals(Constants.appActivate)){
                    if(sPageObj.Contains("/list")){
                        Ranorex.List lst = sPageObj;
                        ComboboxItemSelectE(lst, sData);
                    }else if(sPageObj.Contains("/combobox")){
                        ComboBox cmbx = sPageObj;
                        ComboboxItemSelect(cmbx, sData);
                    }
                }
                Main.OutputData.Add("Pass");
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void ListItemSelectRawTxt(Ranorex.List cmbox, string text){
            cmbox.Click();
            RxPath rxPath = "/form[@title='']/rawtext[@column='0']";
            ScrollAndSelectValueFromDorpdown(rxPath, text);
        }
        
        private void ScrollAndSelectValueFromDorpdown(RxPath rxPath, string searchValue){
            bool Found = false;
            IList<Element> list = Host.Local.Find(rxPath);
            string cellValue = string.Empty;
            int cnt = list.Count;
            for(int i=1;i<=cnt;i=i+2){
                cellValue = list[i-1].As<RawText>().RawTextValue;
                if(cellValue.Trim().Equals(searchValue, StringComparison.OrdinalIgnoreCase)){
                    Found = true;
                    list[i].As<RawText>().Click();
                    break;
                }
                if(i==cnt-1 && !Found){
                    Button btnDown = fetchElement("Activate_Occupancy Details_BTN_DropDown_Scroll");
                    while(i>0){
                        btnDown.Press();
                        i=i-2;
                    }
                    list = Host.Local.Find(rxPath);
                    cnt = list.Count;
                }
                continue;
            }
        }
        
        public List<string> EXISTS(){
            var sPageObj = Main.sElement;
            
            try
            {
                if(sPageObj.Contains("button") || sPageObj.Contains("text") || sPageObj.Contains("combobox") || sPageObj.Contains("checkbox") ||  sPageObj.Contains("list") || sPageObj.Contains("rawtext"))
                {
                    bool exists = Validate.Exists(sPageObj,"Check Object '{0}'",false);
                    if(exists==true)
                        Main.OutputData.Add("Pass");
                    else
                        Main.OutputData.Add("Fail");
                }
                
            }
            catch (Exception e)
            {
                Main.OutputData.Add("Fail");
                Main.OutputData.Add(e.Message);
            }
            return Main.OutputData;
        }
        
        public List<string> NOT_EXISTS(){
            sPageObj = Main.sElement;
            try
            {
                if (sPageObj.Contains("button") || sPageObj.Contains("text") || sPageObj.Contains("combobox") || sPageObj.Contains("checkbox") || sPageObj.Contains("rawtext"))
                {
                    bool exists = Validate.NotExists(sPageObj,"Check Object '{0}'",false);
                    if(exists==false)
                        Main.OutputData.Add("Pass");
                    else
                        Main.OutputData.Add("Fail");
                }
            }
            catch (Exception e)
            {
                Main.OutputData.Add("Fail");
                Main.OutputData.Add(e.Message);
            }
            return Main.OutputData;
        }
        
        public List<string> GETVALUE(){
            sPageObj = Main.sElement;
            string val = string.Empty;
            try
            {
                if(sPageObj.Contains("rawtext")){
                    RawText elementActivate = sPageObj;
                    val = elementActivate.RawTextValue;
                }
                else if (sPageObj.Contains("combobox")){
                    ComboBox cmbx = sPageObj;
                    val = cmbx.Text;
                }
                else if (sPageObj.Contains("list")){
                    Ranorex.List lst = sPageObj;
                    ListItem list = lst.Children[0].As<ListItem>();
                    val = list.Text;
                }
                else{
                    Text element = sPageObj;
                    val = element.TextValue;
                }
                Main.OutputData.Add("Pass");
                if(!(string.IsNullOrEmpty(val) || string.IsNullOrWhiteSpace(val))){
                    val = val.Trim();
                }
                Main.OutputData.Add(val);
            }
            
            catch (Exception e)
            {
                Main.OutputData.Add("Fail");
                Main.OutputData.Add(e.Message);
            }
            
            return Main.OutputData;
        }
        
        public List<string> GET_RUNTIME_PROP(){
            sPageObj = Main.sElement;
            try{
                string attribute = Main.InputData[0];
                Element element = sPageObj;
                string propValue = element.GetAttributeValue(attribute).ToString();
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(propValue);
            }
            catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
            }
            return Main.OutputData;
        }
        
        public List<string> Get_Record_Count_Table(){
            try{
                sPageObj = Main.sElement;
                RxPath rxPath = sPageObj;
                int count = Host.Local.Find(rxPath, duration).Count;
                
                Main.OutputData.Add("Pass");
                Main.OutputData.Add(count.ToString());
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> CONCATENATE()
        {
            
            List<string> output = new List<string>();
            List<string> concat = new List<string>();
            int inputLen = Main.InputData.Count;
            for(int i=0;i<inputLen;i++){
                concat.Add(Main.InputData[i]);
            }
            val = String.Join("", concat);
            output.Add(Constants.TS_STATUS_PASS);
            output.Add(val);
            return output;
        }

        public List<string> DOUBLE_CLICK(){
            try{
                sPageObj = Main.sElement;
                if(sPageObj.Contains("rawtext")){
                    RawText rawtxtelem = sPageObj;
                    rawtxtelem.LongTouch();
                    rawtxtelem.DoubleClick();
                }
                else{
                    Text txtElement = sPageObj;
                    txtElement.DoubleClick();
                }
                
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> Element_Displayed(){
            try{
                if(Main.InputData.Count==1){
                    sData = Main.InputData[0];
                    string actualObj = string.Format(Main.sElement,sData);
                    sPageObj=actualObj;
                }else{
                    sPageObj = Main.sElement;
                }
                bool eleFound = false;
                RxPath txtElement = sPageObj;
                Element element = null;
                Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                if(Host.Local.TryFindSingle(txtElement, durationTime,out element)){
                    if(element.Visible){
                        eleFound=true;
                    }
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(eleFound.ToString());
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void LoginActivate(){
            try{
                string inputUser=String.Empty;
                string inputPass=String.Empty;
                if(Main.InputData.Count>0)
                {
                    inputUser=Main.InputData[0];
                    inputPass=Main.InputData[1];
                }
                string defaultUser=Settings.getInstance().get("ACTIVATE_ENV_USER");
                string defaultPassword=Settings.getInstance().get("ACTIVATE_ENV_PASSWORD");
                Text usrName = fetchElement("Activate_Activate Login_TXT_Userid");
                Text pswd = fetchElement("Activate_Activate Login_TXT_Password");
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                usrName.EnsureVisible();
                if(String.IsNullOrWhiteSpace(inputUser))
                {
                    usrName.PressKeys(defaultUser);
                    pswd.PressKeys(defaultPassword);
                }else{
                    usrName.PressKeys(inputUser);
                    pswd.PressKeys(inputPass);
                }
                Button Ok = fetchElement("Activate_Activate Login_BTN_OK");
                Ok.Click();
                RxPath invalidLoginOk = fetchElement("Activate_Activate Login_Login_Invalid_BTN_Ok");
                Button btnOkInvalidLogin = null;
                Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                if(Host.Local.TryFindSingle(invalidLoginOk, durationTime,out btnOkInvalidLogin)){
                    btnOkInvalidLogin = Host.Local.FindSingle(invalidLoginOk).As<Button>();
                    btnOkInvalidLogin.Click();
                    throw new Exception("Invalid Login data. Cannot Login To Activate");
                }
                RxPath TitleBarRx = fetchElement("Activate_Activate Login_TITLE_BAR_SummitActivate");
                TitleBar titleBar = null;
                durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                if(Host.Local.TryFindSingle(TitleBarRx, durationTime,out titleBar)){
                    Report.Info("Activate Login Successful!!");
                }else{
                    RxPath loginErrorOk = fetchElement("Activate_Activate Login_BTN_Error_Ok");
                    Button btnloginErrorOk = null;
                    durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                    if(Host.Local.TryFindSingle(loginErrorOk, durationTime,out btnloginErrorOk)){
                        btnloginErrorOk = Host.Local.FindSingle(loginErrorOk).As<Button>();
                        btnloginErrorOk.Click();
                    }
                    
                    TitleBarRx = fetchElement("Activate_Activate Login_TITLE_BAR_SummitActivate");
                    TitleBar activateLogo = Host.Local.FindSingle(TitleBarRx,duration);
                    if(activateLogo.Visible){
                        Report.Info("Activate Login Successful!!");
                    }
                }
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        public List<string> EVENT_CODE_GENERATE(){
            string event_code="";
            sData = Main.InputData[0];
            event_code = utility.GetEventCode(sData).ToUpper();
            if(!String.IsNullOrEmpty(event_code)){
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(event_code);
            }
            else{
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
            }
            return Main.OutputData;
        }
        
        public List<string> OpenMacro()
        {
            try
            {
                string macroName = Main.InputData[0];
                string printFlag = Settings.getInstance().get("PRINT_ACTIVATE_FLAG").ToUpper();
                if("Y".Equals(printFlag)){
                    if(macroName.StartsWith("KF", StringComparison.OrdinalIgnoreCase) && !macroName.EndsWith("PR", StringComparison.OrdinalIgnoreCase)){
                        macroName=macroName+"PR";
                    }
                }
                OpenMacro(macroName);
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> BATCH_CODE_GENERATE(){
            string batch_code="";
            sData = Main.InputData[0];
            batch_code = utility.GetOpenBatch(sData);
            if(!String.IsNullOrEmpty(batch_code)){
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(batch_code);
            }
            else{
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
            }
            return Main.OutputData;
        }
        
        public List<string> GET_NEW_ACCOUNT_TYPE(){
            try{
                String newAccountType=utility.GenerateUniqueAccountType();
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(newAccountType);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> GET_TODAYS_CAL_DATE()
        {
            List<string> output = new List<string>();
            string date="";
            string query="select to_char(SYSDATE, 'DD-MON-yyyy') from dual";
            List<string[]> result=OracleUtility.Instance().executeQuery(query);
            if(result.Count >0)
            {
                date = result[0][0];
            }
            if(!String.IsNullOrEmpty(date)){
                output.Add(Constants.TS_STATUS_PASS);
                output.Add(date);
            }
            return output;
        }
        
        public List<string> SETCHECKBOX(){
            try{
                sPageObj = Main.sElement;
                sData = Main.InputData[0];
                CheckBox cBox = sPageObj;
                checkboxOperation(sData.ToUpper(), sPageObj);
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void ComboboxItemSelectDirectPath(ComboBox cmbox, string text){
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            cmbox.EnsureVisible();
            cmbox.Click();
            IList<ListItem> items = cmbox.FindDescendants<ListItem>();
            foreach(ListItem item in items){
                if(item.Text.StartsWith(text, StringComparison.OrdinalIgnoreCase)){
                    item.Click();
                    break;
                }
            }
        }
        
        private void ListtemSelectDirectPath(Ranorex.List cmbox, string text){
            bool itemFound = false;
            cmbox.Click();
            Delay.Milliseconds(500);
            IList<ListItem> items = cmbox.FindDescendants<ListItem>();
            int totalCount= items.Count;
            RxPath rxpath = fetchElement("Activate_Occupancy Details_BTN_DropDown_Scroll");
            int btnDownCount = Host.Local.Find(rxpath).Count;
            foreach(ListItem item in items){
                string itemText = item.Text;
                if(itemText.StartsWith(text, StringComparison.OrdinalIgnoreCase)){
                    int itemIndex =  item.Index;
                    for(int i=0;i<totalCount;i++){
                        if(i==itemIndex){
                            Report.Info("itemText----->"+itemText+" itemIndex--->"+itemIndex.ToString());
                            itemFound = true;
                            break;
                        }
                        //Keyboard.Press("{Down}");
                        if(btnDownCount==1){
                            Button btnDown = Host.Local.FindSingle(rxpath).As<Button>();
                            btnDown.Click();
                        }
                    }
                    if(itemFound){
                        //Keyboard.Press("{Tab}");
                        Report.Info("Select Raw Text List Item: "+text);
                        SelectRawTextValueListItem(text);
                        break;
                    }
                    
                }
            }
            if(!itemFound){
                throw new Exception("List value is not Valid. Correct the Data and try again");
            }
        }
        
        private void SelectRawTextValueListItem(string textValue){
            RawText val = "/form[@title='']/rawtext[@RawText>'"+textValue+"']";
            val.Click();
        }
        
        private void ListtemSelectDirectPathEquals(Ranorex.List cmbox, string text){
            bool itemFound = false;
            cmbox.Click();
            Delay.Milliseconds(500);
            IList<ListItem> items = cmbox.FindDescendants<ListItem>();
            int totalCount= items.Count;
            RxPath rxpath = fetchElement("Activate_Occupancy Details_BTN_DropDown_Scroll");
            int btnDownCount = Host.Local.Find(rxpath).Count;
            foreach(ListItem item in items){
                string itemText = item.Text;
                if(itemText.Equals(text, StringComparison.OrdinalIgnoreCase)){
                    int itemIndex =  item.Index;
                    for(int i=0;i<totalCount;i++){
                        if(i==itemIndex){
                            Report.Info("itemText----->"+itemText+" itemIndex--->"+itemIndex.ToString());
                            itemFound = true;
                            break;
                        }
                        //Keyboard.Press("{Down}");
                        if(btnDownCount==1){
                            Button btnDown = Host.Local.FindSingle(rxpath).As<Button>();
                            btnDown.Click();
                        }
                    }
                    if(itemFound){
                        //Keyboard.Press("{Tab}");
                        Report.Info("Select Raw Text List Item: "+text);
                        SelectRawTextValueListItemEquals(text);
                        break;
                    }
                    
                }
            }
            if(!itemFound){
                throw new Exception("List value is not Valid. Correct the Data and try again");
            }
        }
        
        private void SelectRawTextValueListItemEquals(string textValue){
            try{
                RawText val = "/form[@title='']/rawtext[@RawText='"+textValue+"']";
                val.Click();
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void ListtemSelectDirectPathDefaultSelected(Ranorex.List cmbox, string text){
            bool itemFound = false;
            cmbox.Click();
            IList<ListItem> items = cmbox.FindDescendants<ListItem>();
            int totalCount= items.Count;
            foreach(ListItem item in items){
                string itemText = item.Text;
                if(itemText.Equals(text, StringComparison.OrdinalIgnoreCase)){
                    int itemIndex =  item.Index;
                    for(int i=0;i<totalCount;i++){
                        if(i==itemIndex){
                            itemFound = true;
                            break;
                        }
                        Keyboard.Press("{Down}");
                    }
                    if(itemFound){
                        Keyboard.Press("{Tab}");
                        break;
                    }
                    
                }
            }
            if(!itemFound){
                throw new Exception("List value is not Valid. Correct the Data and try again");
            }
        }
        
        public List<string> GetRawTextValue(){
            try{
                sPageObj = Main.sElement;
                sData = Main.InputData[0];
                string actualObj = string.Format(sPageObj,sData);
                RawText textObj = actualObj;
                string val = textObj.RawTextValue;
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(val);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> LISA_Automation(){

            string localTargetDir=String.Empty;
            try{
                MenuPromptInternal("CCGVM");
                Keyboard.Press("{F12}");
                Text globalClass = fetchElement("Summit_Maintain_Toolkit_Globals_TXT_GlobalClass");
                Text globalName = fetchElement("Summit_Maintain_Toolkit_Globals_TXT_GlobalName");
                Text lisaURL = fetchElement("Summit_Maintain_Toolkit_Globals_TXT_GlobalValue");
                InputText(globalClass,"HMRC_LISA_API");
                InputText(globalName,"ROOT_CONTEXT");
                Keyboard.Press("{F11}");
                lisaURL.Click();
                lisaURL.Element.InvokeAction("selectAll");
                lisaURL.Element.InvokeAction("clear");
                lisaURL.PressKeys("{Tab}");
                string lisaVAlue = Settings.getInstance().get("LISA_URL").Replace("/login","");
                Delay.Seconds(2);
                InputText(lisaURL,lisaVAlue);
                Delay.Seconds(2);
                Keyboard.Press("{F3}");
                Delay.Seconds(3);
                Keyboard.Press("{ENTER}");
                Keyboard.Press("{F4}");

                string hostValue = Settings.getInstance().get("LISA_URL").Replace("https://","");
                int pos = hostValue.IndexOf(":");

                string finalHostValue = hostValue.Replace(hostValue.Substring(pos),"");

                string dbValueText = Settings.getInstance().get("SUMMIT_DB_HOST_NAME");
                string[] splittedText = dbValueText.Split('.');

                string dbValue = splittedText[0];
                string dbInstance = Settings.getInstance().get("SUMMIT_DB_HOST_SERVICE");
                string finalValue = dbValue+"_"+dbInstance;

                string localdirpath = Settings.getInstance().get("LISA_FILES_PATH");

                localTargetDir = Path.Combine(Constants.projectDir,localdirpath);

                string processP1 = Path.Combine(localTargetDir,"ExecuteQry.bat");
                string processP2 = Path.Combine(localTargetDir,"ExecuteQry2.bat");

                ProcessStartInfo psi1 = new ProcessStartInfo();
                psi1.FileName = processP1;
                psi1.UseShellExecute = true;
                psi1.Arguments = finalValue+" "+localTargetDir;

                ProcessStartInfo psi2 = new ProcessStartInfo();
                psi2.FileName = processP2;
                psi2.UseShellExecute = true;
                psi2.Arguments = finalValue+" "+localTargetDir;

                Process p1 = System.Diagnostics.Process.Start(psi1);
                Process p2 = System.Diagnostics.Process.Start(psi2);
                closeAppActivate();
                Delay.Seconds(5);
                
                Keyboard.Press("{d up}{LWin up}");
                Keyboard.Press("{F5}");
                Keyboard.Press("{F5}");


                Host.Local.RunApplication("C:\\Program Files\\Mozilla Firefox\\firefox.exe", "", "C:\\Program Files\\Mozilla Firefox", true);
                Delay.Seconds(15);
                Keyboard.Press(System.Windows.Forms.Keys.P | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control, 25, Keyboard.DefaultKeyPressTime, 1, true);
                Delay.Seconds(9);
                Keyboard.Press(Settings.getInstance().get("LISA_URL"));
                Delay.Seconds(5);
                Keyboard.Press("{ENTER}");
                Delay.Seconds(10);
                Keyboard.Press("{F11}");

                RxPath goAdvanced = fetchElement("Summit_LISA_Authentication_BTN_Go_Advanced");
                RxPath AdvancedForm = fetchElement("Summit_LISA_Authentication_BTN_FF_Advanced");
                
                Button btnAdv = null;
               Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                
                if(Host.Local.TryFindSingle(goAdvanced, durationTime,out btnAdv)){
                    btnAdv = Host.Local.FindSingle(goAdvanced).As<Button>();
                    btnAdv.Click();
                    Delay.Seconds(10);
                    Button acceptRiskContinue = fetchElement("Summit_LISA_Authentication_BTN_AcceptRisk_Continue");
                    acceptRiskContinue.Click();
                }                
                
                if(Host.Local.TryFindSingle(AdvancedForm, durationTime,out btnAdv)){
                    btnAdv = Host.Local.FindSingle(AdvancedForm).As<Button>();
                    btnAdv.Click();
                    Delay.Seconds(10);
                    Button acceptRiskContinue = fetchElement("Summit_LISA_Authentication_BTN_AcceptRisk_Continue");
                    acceptRiskContinue.Click();
                }
                
                RxPath acceptRiskContinue2 = fetchElement("Summit_LISA_Authentication_BTN_AcceptRisk_Continue");
                Button btnAccept = null;
                
                if(Host.Local.TryFindSingle(acceptRiskContinue2, durationTime,out btnAccept)){
                    btnAccept = Host.Local.FindSingle(acceptRiskContinue2).As<Button>();
                    Delay.Seconds(10);
                    btnAccept.Click();
                    Delay.Seconds(5);
                }
                                
                Utility.Capture_Screenshot();
                Delay.Seconds(18);
                
                Text username = fetchElement("Summit_LISA_Authentication_TXT_UserName");
                Text password = fetchElement("Summit_LISA_Authentication_TXT_Password");
                
                username.PressKeys("{Delete}");
                Delay.Seconds(5);
                string LISA_USERNAME = Settings.getInstance().get("LISA_USERNAME");
                string LISA_PASSWORD = Settings.getInstance().get("LISA_PASSWORD");
                string LISA_CLIENT_ID = Settings.getInstance().get("LISA_CLIENT_ID");
                string LISA_CLIENT_SECRET = Settings.getInstance().get("LISA_CLIENT_SECRET");
                string LISA_VALIDATION_USERNAME = Settings.getInstance().get("LISA_Validation_UserName");
                string LISA_VALIDATION_PASSWORD = Settings.getInstance().get("LISA_Validation_Password");
                string LISA_MANGER_REF_ID = Settings.getInstance().get("LISA_Manager_Reference_Number");

                Report.Info("LISA_USERNAME is " + LISA_USERNAME);
                Report.Info("LISA_PASSWORD is " + LISA_PASSWORD);
                username.PressKeys(LISA_USERNAME);
                password.PressKeys("{Delete}");
                password.PressKeys(LISA_PASSWORD);
                Button login = fetchElement("Summit_LISA_Authentication_BTN_LoginForm");
                
                login.Click();
                Delay.Seconds(10);
                Utility.Capture_Screenshot();

                RxPath resetinitialBtn = fetchElement("Summit_LISA_Authentication_BTN_Reset");
                Button btnReset = null;
                if(Host.Local.TryFindSingle(resetinitialBtn, durationTime,out btnReset)){
                    btnReset = Host.Local.FindSingle(resetinitialBtn).As<Button>();
                    btnReset.Click();
                    Delay.Seconds(2);
                    Button resetBtn = fetchElement("Summit_LISA_Authentication_BTN_Reset_Next");
                    resetBtn.Click();
                }                
                Delay.Seconds(10);

                Text client_ID = fetchElement("Summit_LISA_Authentication_TXT_ClientID");
                client_ID.PressKeys(LISA_CLIENT_ID);
                
                Text client_Secret = fetchElement("Summit_LISA_Authentication_TXT_ClientPassword");
                client_Secret.PressKeys(LISA_CLIENT_SECRET);
                
                Button proceed = fetchElement("Summit_LISA_Authentication_BTN_ProceedForm");
                proceed.Click();

                Delay.Seconds(10);
                Utility.Capture_Screenshot();
                
//                Keyboard.Press(System.Windows.Forms.Keys.R | System.Windows.Forms.Keys.Control, 19, Keyboard.DefaultKeyPressTime, 1, true);
//                Delay.Milliseconds(10);
                Keyboard.Press("{RControlKey down}{End}{RControlKey up}");
                Delay.Milliseconds(5);
//                RxPath continue_Further = fetchElement("Summit_LISA_Authentication_BTN_Continue");
//                ClickButtonRx(continue_Further);
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}");
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}");
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{ENTER}");

                Delay.Seconds(10);
                
                //Button signin_Gateway = fetchElement("Summit_LISA_Authentication_BTN_SignIn_To_GovtGateway");
                //signin_Gateway.Click();
                Keyboard.Press("{RControlKey down}{End}{RControlKey up}");
                Delay.Milliseconds(10);
                
//                RxPath signin_Gateway = fetchElement("Summit_LISA_Authentication_BTN_SignIn_To_GovtGateway");
//                ClickButtonRx(signin_Gateway);
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}");
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}");
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{ENTER}");
                  
                
//                Button gateway = null;
//                RxPath signin_Gateway = fetchElement("Summit_LISA_Authentication_BTN_SignIn_To_GovtGateway");
//
//                gateway = Host.Local.FindSingle(signin_Gateway).As<Button>();
//                Delay.Milliseconds(10);
//                gateway.Click();
                    
                Delay.Seconds(10);
               
                string usrnm = LISA_VALIDATION_USERNAME;
                string passwrd = LISA_VALIDATION_PASSWORD;
                
                Text signInUserID = fetchElement("Summit_LISA_Authentication_TXT_Username_HMRC");
                Text passwordID = fetchElement("Summit_LISA_Authentication_TXT_Password_HMRC");

                Button loginbtnID = fetchElement("Summit_LISA_Authentication_BTN_Submit");

                signInUserID.PressKeys("{Delete}");
                signInUserID.PressKeys(usrnm);
                passwordID.PressKeys("{Delete}");
                passwordID.PressKeys(passwrd);
                loginbtnID.Click();

                Delay.Seconds(10);
                Utility.Capture_Screenshot();
                Keyboard.Press("{F11}");
                
                //RxPath givePermission = fetchElement("Summit_LISA_Authentication_BTN_Give_Permission");
                //Button permission_Give_BTN = Host.Local.FindSingle(givePermission, duration);
                //permission_Give_BTN.Click();
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}");
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}");
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}{LShiftKey down}{Tab}{LShiftKey up}");
                Keyboard.Press("{LShiftKey down}{Tab}{LShiftKey up}{ENTER}");

                Utility.Capture_Screenshot();
                
                Text SuccessText = fetchElement("Summit_LISA_Authentication_TXT_SuccessMsg");
                Delay.Seconds(2);
                if(SuccessText.Visible==false){
                    Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                    Report.Info("HMRC after login doesn't able to establish connection");
                    Utility.Capture_Screenshot();
                }

                Utility.Capture_Screenshot();
                Delay.Seconds(15);
                string processP3 = Path.Combine(localTargetDir,"ExitChrome.bat");
                Process p3 = Process.Start(processP3);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));

                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;

            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
            finally{
               string processP4 = Path.Combine(localTargetDir,"ExitChrome.bat");
                Process p4 = Process.Start(processP4);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("MEDIUM_WAIT")));
            }
        }
        
        public List<string> REST_GET_API_Details()
        {
            try{
                int inputLen = Main.InputData.Count;
                string html;
                string url = Main.InputData[0];
                string headerKey = "";
                string headerValue = "";
                string outputparams = "";
                string splittedstring = "";
                string splitted = "";
                string[] strArray = new string[] {};
                IDictionary<string, string> HeaderList = new Dictionary<string, string>();
                
                for (int i=0; i<inputLen; i++){
                    if(Main.InputData[i].Contains("OutputParams")){
                        splittedstring = Main.InputData[i].Remove(0, 13);
                        strArray = splittedstring.Split('|');
                    }
                    if(Main.InputData[i].Contains("Headers")){
                        splitted = Main.InputData[i].Remove(0,8);
                        HeaderList  = splitted.Split('|')
                            .Select(x => x.Split('='))
                            .Where(x => x.Length > 1 && !String.IsNullOrEmpty(x[0].Trim())
                                   && !String.IsNullOrEmpty(x[1].Trim())).ToDictionary(x => x[0].Trim(), x => x[1].Trim());
                    }
                }
                
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                foreach(KeyValuePair<string, string> kvpheader in HeaderList)
                {
                    headerKey = kvpheader.Key;
                    headerValue = kvpheader.Value;
                    httpWebRequest.Headers.Add(headerKey, headerValue);
                }
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Fail: Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }
                dynamic json = JsonConvert.DeserializeObject(html);
                Report.Info("API Response is " + json);
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(html);
                dynamic DynamicData = JsonConvert.DeserializeObject(html);
                for(int i=0; i<strArray.Length; i++)
                {
                    outputparams = FindValueByKey(html,strArray[i]);
                    Main.OutputData.Add(outputparams);
                }
                Main.OutputData.Add(html);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public List<string> REST_POST_API_Details()
        {
            try{
                int inputLen = Main.InputData.Count;
                List<string> arrLstBody = new List<string>();
                List<string> arrLstHeader = new List<string>();
                string testDataRef = Main.InputData[0];
                string url = Settings.getInstance().get("API_SERVICE_URL") + Main.InputData[1];
                Report.Info("url---"+url);
                string key ="";
                string valuebody = "";
                string headerKey = "";
                string headerValue = "";
                string searchKey = "";
                string bodyfinal = "";
                string json = "";
                string splittedstring = "";
                string outputparams = "";
                string[] strArray = new string[] {};
                string splitted = "";
                IDictionary<string, string> HeaderList = new Dictionary<string, string>();
                IDictionary<string, string> ParamList = new Dictionary<string, string>();
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string sqlQuery = "select  *  from [API_Details] where [Reference] = '"+testDataRef+"'";
                dbUtility.ReadDBResultMS(sqlQuery);
                bodyfinal = dbUtility.GetAccessFieldValue(TestDataConstants.API_Body);
                
                for (int i=0; i<inputLen; i++){
                    if(Main.InputData[i].Contains("OutputParams")){
                        splittedstring = Main.InputData[i].Remove(0, 13);
                        strArray = splittedstring.Split('|');
                    }
                    if(Main.InputData[i].Contains("Headers")){
                        splitted = Main.InputData[i].Remove(0,8);
                        HeaderList  = splitted.Split('|')
                            .Select(x => x.Split('='))
                            .Where(x => x.Length > 1 && !String.IsNullOrEmpty(x[0].Trim())
                                   && !String.IsNullOrEmpty(x[1].Trim())).ToDictionary(x => x[0].Trim(), x => x[1].Trim());
                    }
                    
                    if(Main.InputData[i].Contains("Body")){
                        json =Main.InputData[i].Remove(0,5);
                        dict = json.Split('|').Select(x => x.Split('=')).Where(x => x.Length > 1 && !string.IsNullOrEmpty(x[0].Trim()) && !String.IsNullOrEmpty(x[1].Trim())).ToDictionary(x => x[0].Trim(), x => x[1].Trim());
                        foreach(KeyValuePair<string, string> kvp in dict)
                        {
                            key = kvp.Key;
                            valuebody = kvp.Value;
                            searchKey = "@" + key + "@";

                            bodyfinal=bodyfinal.Replace(searchKey, valuebody);
                        }
                    }
                }
                string html;

                string reqObject2 = @""+bodyfinal+"";
                var reqObject = JObject.Parse(reqObject2);
                string request = JsonConvert.SerializeObject(reqObject);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(request);
                }
                foreach(KeyValuePair<string, string> kvpheader in HeaderList)
                {
                    headerKey = kvpheader.Key;
                    headerValue = kvpheader.Value;
                    httpWebRequest.Headers.Add(headerKey, headerValue);
                }
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    dynamic json2 = JsonConvert.DeserializeObject(html);
                    Report.Info("API Response is " + json2);
                    Main.OutputData.Add(Constants.TS_STATUS_PASS);
                    Main.OutputData.Add(html);
                    for(int i=0; i<strArray.Length; i++)
                    {
                        outputparams = FindValueByKey(html,strArray[i]);
                        Main.OutputData.Add(outputparams);
                    }
                    return Main.OutputData;
                }
                else{
                    var message = String.Format("Fail: Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }
                
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        public string FindValueByKey(string jsonResponse, string keyToFind)
        {
            JObject json = JObject.Parse(jsonResponse);    
            JToken result = TraverseJsonForKey(json, keyToFind); 
            return (result != null) ? result.ToString() : null;
        } 
        
        static JToken TraverseJsonForKey(JToken token, string key)
        {
            if (token == null)
        {
            return null;
        }

        // Check if the current token is the key we're looking for
        if (token.Type == JTokenType.Property && ((JProperty)token).Name == key)
        {
            return ((JProperty)token).Value;
        }

        // Recursively search through the children of the current token
        foreach (JToken child in token.Children())
        {
            JToken result = TraverseJsonForKey(child, key);

            if (result != null)
            {
                return result;
            }
        }

        return null;
        }
    }
}
