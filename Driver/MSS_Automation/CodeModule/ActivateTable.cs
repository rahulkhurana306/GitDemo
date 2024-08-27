/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 11/10/2022
 * Time: 11:54
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
    /// Description of ActivateTable.
    /// </summary>
    public partial class Keywords
    {
        List<string> arrLstIpAct = new List<string>();
        List<string> arrLstValueAct = new List<string>();
        string objAction = string.Empty;
        string indexValAT = string.Empty;
        string operationAT = string.Empty;
        
        public List<string> ActivateTable()
        {
            try{
                Main.appFlag = Constants.appActivate;
                if(!string.IsNullOrEmpty(operationAT)){
                    operationAT = string.Empty;
                }
                EvaluateInputActivateTable();
                if(operationAT.ToUpper().Equals("ADD")){
                       Button btnAdd = fetchElement(objAction);
                       btnAdd.Click();
                       int rcnt = 0;
                       for(int i=0; i<arrLstIpAct.Count; i++){
                           string colEle = fetchElement(arrLstIpAct[i]);
                           string cldData = arrLstValueAct[i];
                           if(i==0){
                               rcnt = countRows(colEle);
                           }
                           colElementCellValueOperation(colEle,rcnt,cldData);
                       }
                }else if(operationAT.ToUpper().Equals("SET_CELL_VALUE")){
                    for(int i=0; i<arrLstIpAct.Count; i++){
                           string colEle = fetchElement(arrLstIpAct[i]);
                           string cldData = arrLstValueAct[i];
                           int actualIndex = Int32.Parse(indexValAT);
                           colElementCellValueOperation(colEle,actualIndex,cldData);
                       }
                    
                }
                  
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void EvaluateInputActivateTable(){
            arrLstIpAct.Clear();
            arrLstValueAct.Clear();
            int inputLen = Main.InputData.Count;
            operationAT = Main.InputData[0]; //Operation-ADD
            if(operationAT.ToUpper().Equals("ADD")){
                objAction = Main.InputData[1]; //Add-object
            }else if(operationAT.ToUpper().Equals("SET_CELL_VALUE")){
                indexValAT =  Main.InputData[1];
            }
            
            int restObjs = inputLen-2;	//rest objects should be in pair key-value
            if(restObjs%2 == 0){
                for(int i=2;i<=restObjs+1;i=i+2){
                    string colName = Main.InputData[i];
                    string cellVal = Main.InputData[i+1];
                    arrLstIpAct.Add(colName);
                    arrLstValueAct.Add(cellVal);
                }
            }else{
                Report.Error("Invalid Data entered in ActivateTable Input");
            }
        }
        
        private int countRows(string ele){
            RxPath primaryObj = ele;
            IList<Element> pEle = Host.Local.Find(primaryObj);
            int rCount = pEle.Count;
            return rCount;
        }
        
        private string CellElementActivate(string colElement, int i){
            return colElement+"["+i+"]";
        }
        
        private void colElementCellValueOperation(string colElement, int rowNum, string data){
            Text txt = null;
            ComboBox cmBox = null;
            CheckBox chBox = null;
            List lst = null;
            Button btn = null;
            
            if(!colElement.Contains("/text")){
                if(colElement.Contains("/combobox")){
                    RxPath cmBoxTxt = CellElementActivate(colElement, rowNum);
                    Element cmBoxElement = Host.Local.FindSingle(cmBoxTxt);
                    cmBox = cmBoxElement.As<ComboBox>();
                    ComboboxItemSelect(cmBox, data);
                    
                }else if(colElement.Contains("/list")){
                    RxPath lstTxt = CellElementActivate(colElement, rowNum);
                    Element lstElement = Host.Local.FindSingle(lstTxt);
                    lst = lstElement.As<List>();
                    //ComboboxItemSelectE(lst, data);
                    ListtemSelectDirectPath(lst, data);
                }
                else if(colElement.Contains("/checkbox")){
                    RxPath chBoxTxt = CellElementActivate(colElement, rowNum);
                    Element chBoxElement = Host.Local.FindSingle(chBoxTxt);
                    chBox = chBoxElement.As<CheckBox>();
                    checkboxOperationClick(data, chBox);
                }else if(colElement.Contains("/button")){
                    RxPath btnRx = CellElementActivate(colElement, rowNum);
                    Element btnElement = Host.Local.FindSingle(btnRx);
                    btn = btnElement.As<Button>();
                    ClickButton(btn);
                }
            }else{
                string txtElement = CellElementActivate(colElement, rowNum);
                Element textElement = Host.Local.FindSingle(txtElement);
                txt = textElement.As<Text>();
                setText(txt,data);
            }
            
        }
    }
}
