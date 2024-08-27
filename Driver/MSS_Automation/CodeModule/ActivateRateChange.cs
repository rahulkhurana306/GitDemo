/*
 * Created by Ranorex
 * User: hkohli
 * Date: 01/05/2023
 * Time: 10:27
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of ActivateRateChange.
    /// </summary>
    /// 
    public partial class Keywords
    {
        public List<string> ActivateRateChange()
        {
            try{
                string rowIndex=null;
                Main.appFlag = Constants.appActivate;
                string societyValue =Main.InputData[0];
                string rateCode=Main.InputData[1];
                string accNum=Main.InputData[2];
                
                string deleteLocks = "DELETE FROM S30 WHERE FILEKEY IN ('MASTERRUN','MASTER')";
                try {
                	oraUtility.executeQuery(deleteLocks);
                	Report.Info("Deleted the records from S30 tables which were locked");
                }
                catch (Exception e){
                Report.Error("Error while deleting the records from S30 table."+e.Message);                	
                }
                
                Main.InputData.Clear();
                Main.InputData.Add("Top Menu");
                Main.InputData.Add("Batch Processes");
                Main.InputData.Add("Activate_Batch_Processes_RAWTXT_Interest_Rate_Change");
                NavigateToActivateScreen();
                
                Button unSelectAll=fetchElement("Activate_Request_Rate_Change_Master_BTN_UnSelectAll");
                unSelectAll.Click();
                
                RxPath society = fetchElement("Activate_Request_Rate_Change_Master_LST_Society");
                if(societyValue.Equals("1")){
                    selectValueListDropDown(society,"Touchstone Financial");
                }else{
                    selectValueListDropDown(society,"Touchstone Financial (Ireland)");
                }
                
                Main.InputData.Clear();
                Main.InputData.Add("SEARCH");
                Main.InputData.Add("1");
                Main.InputData.Add("Activate_Request_Rate_Change_Master_RAWTXT_Code");
                Main.InputData.Add(rateCode);
                rowIndex = TABLE()[2];
                Report.Info("rowIndex---"+rowIndex);
                int rIndex = Int32.Parse(rowIndex);
                if(rIndex==8){
                    ScrollBar tableScroller = scrollObjActivate;
                    IList<Button> scrollBtns = tableScroller.FindDescendants<Button>();
                    int btnCount = scrollBtns.Count;
                    Report.Info("Scroll buttons count--"+btnCount);
                    Button scrollDwonBtn = scrollBtns[3];
                    scrollDwonBtn.Click();
                    rIndex = rIndex-1;
                    rowIndex = rIndex.ToString();
                }
                
                string codeCbx = fetchElement("Activate_Request_Rate_Change_Master_CBX_Code_Selected");
                CheckBox cbxSelect=String.Format(codeCbx,rowIndex);
                cbxSelect.Click();
                
                Button btnOk=fetchElement("Activate_Request_Rate_Change_Master_BTN_OK");
                btnOk.Click();
                
                Button btnYes=fetchElement("Activate_Application_Question_Rate_Change_BTN_Yes");
                btnYes.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                
                Main.InputData.Clear();
                Main.InputData.Add("Top Menu");
                Main.InputData.Add("Batch Processes");
                Main.InputData.Add("Activate_Batch_Processes_RAWTXT_Request_Mailshot");
                NavigateToActivateScreen();
                Main.InputData.Clear();
                
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                RawText lisIDLabel=fetchElement("Activate_Request_Mailshot_RAWTXT_LBL_ListID");
                ClickRawText(lisIDLabel);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
                ClickRawText(lisIDLabel);
                
                
                Button btndetails=fetchElement("Activate_Request_Mailshot_BTN_Details");
                btndetails.Click();
                
                Text keyValueText=fetchElement("Activate_Mailshot_Details_TXT_Key_Value");
                string mortgageNumber=keyValueText.Element.GetAttributeValueText("Text");
                
                if(mortgageNumber.StartsWith(accNum, StringComparison.OrdinalIgnoreCase)){
                    Main.OutputData.Add(Constants.TS_STATUS_PASS);
                }else{
                    Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                    Report.Info("Mortgage Number is "+mortgageNumber);
                }
                Button btnClose=fetchElement("Activate_Mailshot_Details_BTN_Close");
                btnClose.Click();
                
                Button btnOK=fetchElement("Activate_Request_Mailshot_BTN_OK");
                btnOK.Click();
                
                return Main.OutputData;
                
            }catch(Exception e){
                Report.Info(e.Message);
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                return Main.OutputData;
            }
        }
        
        public List<string> SearchBaseRate(){
            try{
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                RawText listIdRx = fetchElement("Activate_Request_Mailshot_RAWTXT_LBL_ListID");
                listIdRx.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                listIdRx.Click();
                string baseRateQuery = "select max(l1.listid) from lpm13 l1 where l1.listname = 'BASERATE'";
                List<string[]> data=oraUtility.executeQuery(baseRateQuery );
                string baseRateListId = data[0][0];
                Report.Info("baseRateListIdDB--"+baseRateListId);
                if(!string.IsNullOrEmpty(baseRateListId)){
                    int col = listIdRx.Column;
                    int row = listIdRx.Row;
                    Report.Info("col--"+col.ToString()+" row--"+row.ToString());
                    string eleStr = fetchElement("Activate_Request_Mailshot_RAWTXT_TBL_COL");
                    string ListIdValueStr = string.Format(eleStr,col.ToString(),row.ToString());
                    Report.Info("ListIdValueStr---"+ListIdValueStr);
                    string firstListIdValueStr = ListIdValueStr+"[1]";
                    RawText firstListIdValueRx = firstListIdValueStr;
                    string firstListIdValue = firstListIdValueRx.RawTextValue;
                    Report.Info("firstListIdValue--"+firstListIdValue);
                    if(!baseRateListId.Equals(firstListIdValue)){
                        int len = Host.Local.Find(ListIdValueStr).Count;
                        bool baseRateFound = false;
                        Report.Info("ele count--"+len);
                        for(int i=3;i<=len-3;i=i+2){
                            RawText nthtListIdValueRx = firstListIdValueStr+"["+i+"]";
                            string nthtListIdValue = nthtListIdValueRx.RawTextValue;
                            Report.Info("---"+i+"---nthtListIdValue"+nthtListIdValue);
                            if(nthtListIdValue.Equals(baseRateListId)){
                                RawText StatusRx = fetchElement("Activate_Request_Mailshot_RAWTXT_LBL_Status");
                                int colStatus = StatusRx.Column;
                                int rowStatus = StatusRx.Row;
                                string eleStatus = fetchElement("Activate_Request_Mailshot_RAWTXT_TBL_COL");
                                string statusValueStr = string.Format(eleStatus,colStatus.ToString(),rowStatus.ToString());
                                string nthStatusValueStr = statusValueStr+"["+(i-2)+"]";
                                Report.Info("nthStatusValueStr---"+nthStatusValueStr);
                                RawText nthStatusValueRx = Host.Local.FindSingle(nthStatusValueStr).As<RawText>();
                                nthStatusValueRx.Click();
                                baseRateFound=true;
                                break;
                            }
                        }
                        if(!baseRateFound){
                            throw new Exception("Base Rate is not found in UI--"+baseRateListId);
                        }
                    }
                    
                }else{
                    throw new Exception("Base Rate List ID is not found in the DB");
                }
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
