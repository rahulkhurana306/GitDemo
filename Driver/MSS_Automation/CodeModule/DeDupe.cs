/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 08/02/2023
 * Time: 10:54
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
    /// Description of DeDupe.
    /// </summary>
    public partial class Keywords
    {
        List<string> arrLstIpDeDupe = new List<string>();
        List<string> arrLstOpDeDupe = new List<string>();
        List<string> outputDataDeDupe = new List<string>();
        string scrollObjHorizontal = "/form[@title='"+Settings.getInstance().get("SUMMIT_TITLE")+"']//scrollbar[@type='LWScrollbar' and @Style='Horizontal' and @enabled='True' and @visible='True']";
        private int horizontalCounter = 0;
        private int colNumHorizontal =-1;
        private IList<Element> list;
        private int loopCounter = 0;
        private int maxValueHorizDedupe = 0;
        private int iVal=1;
        public List<string> DeDupe()
        {
            try{
                appFlg = Constants.appSummit;
                ReInitializeDeDupeVariables();
                EvaluateDeDupeInput();
                for(int i=2;i<arrLstIpDeDupe.Count;i=i+3){
                    findCustomerOrAddressInVisibleRange(arrLstIpDeDupe[i],i);
                }
                int outputCount = arrLstOpDeDupe.Count;
                if(outputCount != 0){
                    for(int opCount =0; opCount<outputCount; opCount++){
                        RxPath fieldRx = fetchElement(arrLstOpDeDupe[opCount]);
                        Text fieldTxt = Host.Local.FindSingle(fieldRx,duration);
                        fieldTxt.EnsureVisible();
                        string cellVal = fieldTxt.GetAttributeValue<string>("Text");
                        outputDataDeDupe.Add(cellVal);
                    }
                }
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                for(int i=0; i<outputCount; i++){
                    Main.OutputData.Add(outputDataDeDupe[i]);
                }
                outputDataDeDupe.Clear();
                //TODO: the below steps are commented for now as the error messags comes up dynamically which needs to be handled at low level
                //hence merge button  click and subsequent steps to be performed at low-level. Once team starts using this keyword we will get more clear idea.
//                Button btnMerge = fetchElement("Summit_Deduplication_Group_Review_BTN_Merge");
//                btnMerge.Click();
//                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
//                
//                RxPath txtMessage = fetchElement("Summit_Notification_Alert_BTN_YES");
//                Button btnYes = null;
//                Duration durationTime = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("SHORT_WAIT")));
//                if(Host.Local.TryFindSingle(txtMessage, durationTime,out btnYes)){
//                    btnYes.Click();
//                }
//                
//                for(int i=0;i<Constants.Timeout;i++){
//                    RxPath txtMergeGrp = fetchElement("Summit_Deduplication_Matches_Review_BTN_MergeGroup");
//                    Button btnMergeGrp = null;
//                    if(Host.Local.TryFindSingle(txtMergeGrp, durationTime,out btnMergeGrp)){
//                        break;
//                    }
//                    
//                }
                
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void ReInitializeDeDupeVariables(){
            arrLstIpDeDupe.Clear();
            arrLstOpDeDupe.Clear();
            outputDataDeDupe.Clear();
            horizontalCounter = 0;
            colNumHorizontal =-1;
            loopCounter = 0;
            maxValueHorizDedupe = 0;
            iVal=1;
        }
        
        private void EvaluateDeDupeInput(){
            int inputLen = Main.InputData.Count;
            arrLstIpDeDupe.Clear();
            string lastElement = Main.InputData[inputLen-1];
            if(lastElement.ToUpper().Contains("OUTPUT")){
                string[] strArr = lastElement.Split('|');
                int len = strArr.Length;
                for(int i=1;i<len;i++){
                    arrLstOpDeDupe.Add(strArr[i].Trim());
                }
                inputLen=inputLen-1;
            }
            for(int i=0;i<inputLen;i++){
                string val = Main.InputData[i];
                arrLstIpDeDupe.Add(val);
            }
            
        }
        
        private void findCustomerOrAddressInVisibleRange(string item, int val){
            Dictionary<string, string> keyVal = new Dictionary<string,string>();
            string[] arr = item.Split('|');
            //string[] firstItem = arr[0].Split(':');
            for(int i=0;i<arr.Length;i++){
                string[] field = arr[i].Split(':');
                string keyField = field[0];
                string keyValue = field[1];
                keyVal.Add(keyField,keyValue);
            }
            RxPath seqNoRx = fetchElement(arrLstIpDeDupe[val-2]);
            list = Host.Local.Find(seqNoRx);
            string valueSearched = arrLstIpDeDupe[val-1];
            int eleCount = list.Count;
            for(int i=iVal;i<=2;i++){
                loopCounter++;
                maxValueHorizDedupe = GetMaxRecordsHorizontalScroll();
                int visibAmtHorizDedupe = GetHorizonatlScrollerPropertyValue("VisibleAmount");
                int valHorizDedupe = GetHorizonatlScrollerPropertyValue("Value");
                if(loopCounter>2 && loopCounter%2!=0 && valHorizDedupe+visibAmtHorizDedupe<maxValueHorizDedupe && colNumHorizontal==-1){
                    HorizontalScrollClicks(visibAmtHorizDedupe,"RIGHT");
                    i=1;
                }
                list[i-1].EnsureVisible();
                //eleCounter++;
                string cellValue = list[i-1].GetAttributeValueText("Text");
                if(!String.IsNullOrEmpty(cellValue)){
                    if(cellValue.Contains(valueSearched)){
                        colNumHorizontal = i;
                        if(i==1){
                            iVal=i+1;
                        }else if(i==2){
                            iVal=i-1;
                        }
                        break;
                    }
                }
                if(i==visibAmtHorizDedupe && colNumHorizontal==-1){
                    iVal=1;
                    i=iVal;
                    continue;
                }
            }
            if(colNumHorizontal!=-1){
                foreach(KeyValuePair<string, string> entry in keyVal){
                    string fieldIndex = fetchElement(entry.Key)+"["+colNumHorizontal+"]";
                    RxPath fieldRx = fieldIndex;
                    string fieldValue = entry.Value;
                    setCheckboxEnsureVisible(fieldRx, fieldValue);
                    if(colNumHorizontal==1 && loopCounter>2 && loopCounter%2!=0 && loopCounter!=maxValueHorizDedupe){
                        string rightButtonStr = scrollObjHorizontal+"/button[1]";
                        RxPath btnRightRx = rightButtonStr;
                        Button btnRight = Host.Local.FindSingle(btnRightRx, duration);
                        btnRight.Click();
                    }
                }
                keyVal.Clear();
                colNumHorizontal=-1;
            }
            else if(colNumHorizontal==-1 && loopCounter==maxValueHorizDedupe){
                throw new Exception("Seq Noumber not found.");
            }
            
                
        }
        
        private void HorizontalScroll(){
            RxPath scrollObjRx = scrollObjHorizontal;
            ScrollBar tableScroller = Host.Local.FindSingle(scrollObjRx, duration);
            int visibleAmount = Int32.Parse(tableScroller.Element.GetAttributeValueText("VisibleAmount"));
            Report.Info("visibleAmountHz--"+visibleAmount.ToString());
            IList<Button> scrollBtns = tableScroller.FindDescendants<Button>();
            int btnCount = scrollBtns.Count;
            Report.Info("Scroll buttons count--"+btnCount);
            Button scrollRtBtn = scrollBtns[0];
            Button scrollLeftBtn = scrollBtns[1];
            scrollRtBtn.Click();
            scrollLeftBtn.Click();
            maxValueHorizDedupe = Int32.Parse(tableScroller.Element.GetAttributeValueText("MaxValue"));
            if(horizontalCounter==0){
                horizontalCounter = maxValueHorizDedupe-visibleAmount;
            }
            if(horizontalCounter==visibleAmount){
                //click right button as visible amount Number
                for(int i=1;i<=visibleAmount;i++){
                    scrollRtBtn.Click();
                }
            }else if(horizontalCounter<visibleAmount){
                //click right button as counter #
                for(int i=1;i<=horizontalCounter;i++){
                    scrollRtBtn.Click();
                }
            }else{
                //click as visible amount
                for(int i=1;i<=visibleAmount;i++){
                    scrollRtBtn.Click();
                }
                //make counter as counter-VisibleAmount
                horizontalCounter = horizontalCounter-visibleAmount;
            }
        }
        
        private void setCheckboxEnsureVisible(RxPath path, string val){
            if(!string.IsNullOrEmpty(val)){
                if(!val.ToLower().Equals("default")){
                    CheckBox cbx = Host.Local.FindSingle(path).As<CheckBox>();
                    cbx.EnsureVisible();
                    checkboxOperation(val,cbx);
                }
            }
        }
        
        private int GetMaxRecordsHorizontalScroll(){
            RxPath scrollObjRx = scrollObjHorizontal;
            ScrollBar tableScroller = Host.Local.FindSingle(scrollObjRx, duration);
            maxValueHorizDedupe = Int32.Parse(tableScroller.Element.GetAttributeValueText("MaxValue"));
            return maxValueHorizDedupe;
        }
        
        public List<string> DeDupeAddress(){
            try{
                appFlg = Constants.appSummit;
                EvaluateDeDupeInput();
                FindAddress();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
            
        }
        
        public void FindAddress(){
            int maxValAddr = GetHorizonatlScrollerPropertyValue("MaxValue");
            int visibileAmtAddr = GetHorizonatlScrollerPropertyValue("VisibleAmount");
            int valueAddr = GetHorizonatlScrollerPropertyValue("Value");
            int counterAddr=1;
            Boolean foundFlg = false;
            for(int i=2;i<=arrLstIpDeDupe.Count-1;i=i+3){
            SEARCHAGAIN:
                foundFlg = false;
                string addrObj = arrLstIpDeDupe[i-2];
                string addrObjValue = arrLstIpDeDupe[i-1];
                if(counterAddr>visibileAmtAddr){
                    HorizontalScrollClicks(visibileAmtAddr,"RIGHT");
                    counterAddr=1;
                }
                for(int j=counterAddr;j<=visibileAmtAddr;j++){
                    RxPath addrObjRx = fetchElement(addrObj)+"["+j+"]";
                    Text textAddrObj = Host.Local.FindSingle(addrObjRx,duration);
                    string valFromApp = textAddrObj.GetAttributeValue<string>("Text");
                    Report.Info("valFromApp----"+valFromApp);
                    if(valFromApp.Equals(addrObjValue)){
                        string incExcCbxStr = fetchElement(arrLstIpDeDupe[i])+"["+j+"]";
                        RxPath incExcCbxRx = incExcCbxStr;
                        int valueOrig = GetHorizonatlScrollerPropertyValue("Value");
                        setCheckbox(incExcCbxRx,"Y");
                        //to reset the scroller position as it gets unset
                        int valuePresent = GetHorizonatlScrollerPropertyValue("Value");
                        if(valueOrig!=valuePresent){
                            if(valuePresent>valueOrig){
                            HorizontalScrollClicks(valuePresent-valueOrig,"LEFT");
                            }
                        }
                        foundFlg = true;
                        break;
                    }else{
                        counterAddr=j;
                    }
                }
                valueAddr = GetHorizonatlScrollerPropertyValue("Value");
                if(valueAddr+visibileAmtAddr==maxValAddr && !foundFlg){
                    throw new Exception("Searched Address Number is Not Found, Please check the Data");
                }
                counterAddr++;
                if(counterAddr>visibileAmtAddr && !foundFlg){
                    goto SEARCHAGAIN;
                }
                
            }
            
        }
        
        private void HorizontalScrollClicks(int num, string val){
            RxPath scrollObjRx = scrollObjHorizontal;
            ScrollBar tableScroller = Host.Local.FindSingle(scrollObjRx, duration);
            int visibleAmount = Int32.Parse(tableScroller.Element.GetAttributeValueText("VisibleAmount"));
            Report.Info("visibleAmountHz--"+visibleAmount.ToString());
            IList<Button> scrollBtns = tableScroller.FindDescendants<Button>();
            int btnCount = scrollBtns.Count;
            Report.Info("Scroll buttons count--"+btnCount);
            Button scrollBtn = null;
            if(val.Equals("RIGHT")){
                scrollBtn = scrollBtns[0];
            }else if(val.Equals("LEFT")){
                scrollBtn = scrollBtns[1];
            }
            for(int i=0;i<num;i++){
                scrollBtn.Click();
            }
        }
        
        private int GetHorizonatlScrollerPropertyValue(string property){
            ScrollBar tableScrollerDeDupe = Host.Local.FindSingle(scrollObjDeDupe, duration);
            int val = Int32.Parse(tableScrollerDeDupe.Element.GetAttributeValueText(property));
            return val;
        }
    }
}