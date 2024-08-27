/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 10/02/2023
 * Time: 12:22
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using ng_mss_automation.CodeModule;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of SearchAndSelectDeDupeRecords.
    /// </summary>
    public partial class Keywords
    {
        int dictCount=0;
        int visibleAmountDeDupe = 0;
        int rowIndexDeDupe = 0;
        Button scrollDwonBtnDeDupe = null;
        string scrollObjDeDupe = "/form[@title='"+Settings.getInstance().get("SUMMIT_TITLE")+"']//scrollbar[@type='LWScrollbar' and @enabled='True' and @visible='True'][1]";
        string GID = string.Empty;
        bool custFlag = true;
        string groupIDObj = string.Empty;
        public List<string> SearchAndSelectDeDupeRecords()
        {
            try{
                int inputLen = Main.InputData.Count;
                string key1 = Main.InputData[0];
                string field1 = Main.InputData[1];
                string key2 = string.Empty;
                string field2 = string.Empty;
                List<int> strinList = new List<int>();
                int iVal=2;
                Dictionary<int,string> dictIntString = new Dictionary<int, string>();
                if(inputLen>3){
                    string str = Main.InputData[2];
                    if(str.ToUpper().StartsWith("SUMMIT")){
                        key2 = Main.InputData[2];
                        field2 = Main.InputData[3];
                        iVal=4;
                    }
                }
                if(!(inputLen-iVal==0)){
                    for(int i=iVal;i<inputLen;i++){
                        string[] indexAndFlag = Main.InputData[i].Split(':');
                        string indexValue = indexAndFlag[0];
                        string flagVal = indexAndFlag[1];
                        strinList.Add(Int32.Parse(indexValue));
                        dictIntString.Add(Int32.Parse(indexValue),flagVal);
                    }
                }
                Main.InputData.Clear();
                Main.InputData.Add("SEARCH");
                Main.InputData.Add("1");
                Main.InputData.Add(key1);
                Main.InputData.Add(field1);
                if(iVal==4){
                    Main.InputData.Add(key2);
                    Main.InputData.Add(field2);
                }
                rowIndexDeDupe = Int32.Parse(TABLE()[1]);
                Report.Info("Row_Index----"+rowIndexDeDupe.ToString());
                if(rowIndexDeDupe==-1){
                    throw new Exception("Given custmer is not found.Please check the test input data");
                }
                string runNoVal = string.Empty;
                try{
                    Text runNo = fetchElement("Summit_Deduplication_Matches_Review_TXT_RunNo");
                    runNoVal = runNo.GetAttributeValue<string>("Text");
                    groupIDObj = fetchElement("Summit_Deduplication_Matches_Review_TBL_TXT_GroupID");
                }catch(Exception e){
                    string strMsg = e.Message;
                    if(strMsg.StartsWith("No element found for path")){
                        Text runNo = fetchElement("Summit_Deduplication_Address_Matches_Review_TXT_RunNo");
                        runNoVal = runNo.GetAttributeValue<string>("Text");
                        groupIDObj = fetchElement("Summit_Deduplication_Address_Matches_Review_TBL_TXT_GroupID");
                        custFlag=false;
                    }
                }
                string fieldGID = groupIDObj+"["+rowIndexDeDupe+"]";
                Text txtFieldGID = Host.Local.FindSingle(fieldGID);
                GID = txtFieldGID.GetAttributeValue<string>("Text");
                
                ScrollBar tableScrollerDeDupe = Host.Local.FindSingle(scrollObjDeDupe, duration);
                visibleAmountDeDupe = Int32.Parse(tableScrollerDeDupe.Element.GetAttributeValueText("VisibleAmount"));
                dictCount = dictIntString.Count;
                Main.InputData.Clear();
                Main.OutputData.Clear();
                int sameEleCount = 0;
                int diffCountVisible = visibleAmountDeDupe-rowIndexDeDupe+1;
                List<int> listSorted = strinList;
                if(strinList.Count!=0){
                    listSorted.Sort();
                    int lastIndexItem = listSorted[listSorted.Count-1];
                    if(lastIndexItem>dictCount){
                        dictCount=lastIndexItem;
                    }
                }
                if(dictCount>diffCountVisible){
                    for(int i=diffCountVisible;i<dictCount;i++){
                        scrollDown();
                        rowIndexDeDupe=rowIndexDeDupe-1;
                    }
                }
                string gidCount = string.Empty;
                if(custFlag){
                    string GIDQuery = "Select COUNT(*) From Dedup_Cust_Matches Where Dcm_Drd_Seqno = '"+runNoVal+"' AND dcm_match_group_id = '"+GID+"'";
                    List<string[]> data=oraUtility.executeQuery(GIDQuery);
                    gidCount = data[0][0];
                }else{
                    string GIDQuery = "Select COUNT(*) From DEDUP_ADDRESS_MATCHES Where DAM_DAR_SEQNO = '"+runNoVal+"' AND DAM_MATCH_GROUP_ID = '"+GID+"'";
                    List<string[]> data=oraUtility.executeQuery(GIDQuery);
                    gidCount = data[0][0];
                }
                sameEleCount=Int32.Parse(gidCount);
                Dictionary<int, string> dictNew = new Dictionary<int, string>();
                for(int i=0;i<sameEleCount;i++){
                    int val = -1;
                    string fieldChkboxPath = string.Empty;
                    string flagValue = string.Empty;
                    if(dictIntString.TryGetValue(i+1, out flagValue)){
                        if(flagValue.ToUpper().Equals("MERGE")){
                            val = rowIndexDeDupe+i;
                            fieldChkboxPath = fetchElement("Summit_Deduplication_Matches_Review_TBL_CBX_Merge")+"["+val+"]";
                        }else if(flagValue.ToUpper().Equals("EXCLUDE")){
                            val = rowIndexDeDupe+i;
                            string excludeObj = string.Empty;
                            if(custFlag){
                                excludeObj = fetchElement("Summit_Deduplication_Matches_Review_TBL_CBX_Exclude");
                            }else{
                                excludeObj = fetchElement("Summit_Deduplication_Address_Matches_Review_TBL_CBX_Exclude");
                            }
                            fieldChkboxPath = excludeObj+"["+val+"]";
                        }
                    }
                    
                    
                    
                    dictNew.Add(i+1,fieldChkboxPath);
                }
                
                foreach(KeyValuePair<int,string> entry in dictNew){
                    Report.Info(entry.Key.ToString()+"-----------"+entry.Value);
                }
                
                for(int i=0;i<strinList.Count;i++){
                    RxPath fieldCBxRx = dictNew[strinList[i]];
                    setCheckbox(fieldCBxRx,"Y");
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                
                Main.OutputData.Add(gidCount);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }

        private void scrollDown(){
            ScrollBar tableScrollerDeDupe = Host.Local.FindSingle(scrollObjDeDupe, duration);
            IList<Button> scrollBtnsDeDupe = tableScrollerDeDupe.FindDescendants<Button>();
            scrollDwonBtnDeDupe = scrollBtnsDeDupe[0];
            scrollDwonBtnDeDupe.Click();
        }
        
    }
}