/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 08/07/2022
 * Time: 16:00
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using ng_mss_automation.CodeModule;
using System.Drawing;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of Table.
    /// </summary>
    public partial class Keywords
    {
        string scrollObj = "/form[@title='"+Settings.getInstance().get("SUMMIT_TITLE")+"']//scrollbar[@type='LWScrollbar' and @enabled='True' and @visible='True']";
        string scrollObjActivate = "/form[@processName='sam']//scrollbar[@accessiblename='Vertical']";
        string operation = null;
        string scrollObjActual = null;
        List<string> arrLstIp = new List<string>();
        List<string> arrLstValue = new List<string>();
        List<string> arrLstOp = new List<string>();
        List<string> arrLstGetCellData = new List<string>();
        int getCellDataCount = 0;
        List<string> outputData = new List<string>();
        int btnClicks = 0;
        int rowNum = -1;
        string logicalColName = null;
        int rowIndex = -1;
        string textValue = null;
        string chBoxFlag = null;
        string sortFlag = "";
        private int counterAct = 0;
        private int rowNumAct = -1;
        private int rowMultipleAct = 1;
        private int recordsCountSummit = 0;
        string appFlg = string.Empty;
        Duration duration = TimeSpan.FromSeconds(Constants.Timeout);
        
        public List<string> TABLE()
        {
            try
            {
                appFlg = Main.appFlag;
                EvaluateTableInput();
                ComboBox cmBox = null;
                CheckBox chBox = null;
                Button btn = null;
                Text text = null;
                RxPath logicalColNameRx = null;

                switch(operation)
                {
                    case "SEARCH":
                        if(appFlg.Equals(Constants.appSummit)){
                            if(rowNum != -1){
                                rowNum = -1;
                            }
                            int row = Table();
                        }else if(appFlg.Equals(Constants.appActivate)){
                            int row = TableActivate();
                        }
                        break;
                    case "COUNT":
                        recordsCountSummit = GetCountOfRecordsInTable();
                        break;
                    case "SELECT_CHKBOX":
                        Report.Info("logicalColName-------"+logicalColName);
                        chBoxFlag = "Y";
                        chBox = CheckBoxElementIndexed(logicalColName);
                        checkboxOperationClick(chBoxFlag, chBox);
                        break;
                    case "UNSELECT_CHKBOX":
                        Report.Info("logicalColName-------"+logicalColName);
                        chBoxFlag = "N";
                        chBox = CheckBoxElementIndexed(logicalColName);
                        checkboxOperationClick(chBoxFlag, chBox);
                        break;
                    case "SELECT_ALL":
                        chBoxFlag = "Y";
                        chBox = CheckBoxElement(logicalColName);
                        checkboxOperationClick(chBoxFlag, chBox);
                        break;
                    case "UNSELECT_ALL":
                        chBoxFlag = "N";
                        chBox = CheckBoxElement(logicalColName);
                        checkboxOperationClick(chBoxFlag, chBox);
                        break;
                    case "SELECT_LST":
                        Report.Info("logicalColName-------"+logicalColName);
                        cmBox = ComboboxElementIndexed(logicalColName);
                        selectValue(cmBox, textValue);
                        break;
                    case "CLICK":
                        btn = logicalColName;
                        btn.Click();
                        break;
                    case "DBL_CLICK":
                        text = logicalColName;
                        text.DoubleClick();
                        break;
                    case "SET_VALUE":
                        logicalColNameRx = logicalColName;
                        text = Host.Local.FindSingle(logicalColNameRx).As<Text>();
                        setText(text, textValue+"{Tab}");
                        break;
                    case "GET_CELL_DATA":
                        for(int opCount =0; opCount<getCellDataCount; opCount++){
                            string colElement = fetchElement(arrLstGetCellData[opCount]);
                            string cellValue = colElementCellValueOutput(colElement, rowIndex);
                            outputData.Add(cellValue);
                        }
                        break;
                    case "VALIDATE_SORTING":
                        text = logicalColName;
                        Report.Info("SORTING Implementation Is Coming Soon..");
                        break;
                    default:
                        Report.Info("Unknown Operation on Table " + operation);
                        break;
                        
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                
                if(operation.Equals("COUNT")){
                    if(appFlg.Equals(Constants.appSummit)){
                        Main.OutputData.Add(recordsCountSummit.ToString());
                    }
                }
                
                if(operation.Equals("SEARCH")){
                    if(appFlg.Equals(Constants.appSummit)){
                        Main.OutputData.Add(rowNum.ToString());
                    }else if(appFlg.Equals(Constants.appActivate)){
                        Main.OutputData.Add(rowNumAct.ToString());
                    }
                }
                
                if(operation.Equals("SEARCH") || operation.Equals("GET_CELL_DATA")){
                    int outputCellLen = outputData.Count;
                    for(int i=0; i<outputCellLen; i++){
                        Main.OutputData.Add(outputData[i]);
                    }
                    outputData.Clear();
                }
                return Main.OutputData;
            }
            catch (Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private CheckBox CheckBoxElement(string objectName){
            RxPath objectNameRx = objectName;
            return Host.Local.FindSingle(objectNameRx).As<CheckBox>();
        }
        
        private CheckBox CheckBoxElementIndexed(string objectName){
            int indexVal = objectName.LastIndexOf('[');
            int indexVal2 = objectName.LastIndexOf(']');
            int countLen = (indexVal2 - indexVal+1);
            string origString = objectName.Remove(indexVal,countLen);
            Report.Info("CBX Original String-------"+origString);
            RxPath origStringRx = origString;
            CheckBox chBox = Host.Local.Find(origStringRx)[rowIndex-1];
            return chBox;
        }
        
        private ComboBox ComboboxElementIndexed(string objectName){
            int indexVal = objectName.LastIndexOf('[');
            int indexVal2 = objectName.LastIndexOf(']');
            int countLen = (indexVal2 - indexVal+1);
            string origString = objectName.Remove(indexVal,countLen);
            Report.Info("COMBX Original String-------"+origString);
            RxPath origStringRx = origString;
            ComboBox cmBox = Host.Local.Find(origStringRx)[rowIndex-1];
            return cmBox;
        }
        
        private Text TextElementIndexed(string objectName){
            int indexVal = objectName.LastIndexOf('[');
            int indexVal2 = objectName.LastIndexOf(']');
            int countLen = (indexVal2 - indexVal+1);
            string origString = objectName.Remove(indexVal,countLen);
            Report.Info("Text Original String-------"+origString);
            RxPath origStringRx = origString;
            Text text = Host.Local.Find(origStringRx)[rowNum-1];
            return text;
        }
        
        private void EvaluateTableInput(){
            int inputLen = Main.InputData.Count;
            operation = Main.InputData[0];
            if(operation.Equals("SEARCH")){
                arrLstIp.Clear();
                arrLstOp.Clear();
                arrLstValue.Clear();
                string scrollIndex = Main.InputData[1];
                if(appFlg.Equals(Constants.appSummit)){
                    scrollObjActual = scrollObj +"["+scrollIndex+"]";
                }else if(appFlg.Equals(Constants.appActivate)){
                    scrollObjActual = scrollObjActivate +"["+scrollIndex+"]";
                }                
                string lastElement = Main.InputData[inputLen-1];
                int headerLength = 0;
                if(lastElement.ToUpper().Contains("OUTPUT")){
                    string[] strArr = lastElement.Split('|');
                    int len = strArr.Length;
                    for(int i=1;i<len;i++){
                        arrLstOp.Add(strArr[i].Trim());
                    }
                    headerLength = inputLen-3;
                }else{
                    headerLength = inputLen-2;
                }
                if(headerLength%2 == 0){
                    for(int i=2; i<=headerLength+1;i=i+2){
                        string colName = Main.InputData[i];
                        string cellVal = Main.InputData[i+1];
                        arrLstIp.Add(colName);
                        arrLstValue.Add(cellVal);
                    }
                }else{
                    Report.Error("Invalid Data entered in search Table Input");
                }
            }
            else if(operation.Equals("GET_CELL_DATA")){
                string[] strArr = Main.InputData[1].Split('|');
                getCellDataCount = strArr.Length;
                rowIndex = Int32.Parse(Main.InputData[2]);
                arrLstGetCellData.Clear();
                for(int i=0;i<getCellDataCount;i++){
                    arrLstGetCellData.Add(strArr[i].Trim());
                }
            }
            else if(operation.Equals("SELECT_ALL") || operation.Equals("UNSELECT_ALL")){
                logicalColName = fetchElement(Main.InputData[1]);
            }
            else if(operation.Equals("VALIDATE_SORTING")){
                string scrollIndex = Main.InputData[1];
                scrollObjActual = scrollObj +"["+scrollIndex+"]";
                logicalColName = fetchElement(Main.InputData[2]);
                sortFlag = Main.InputData[3];
            }
            else if(operation.Equals("COUNT")){
                arrLstIp.Clear();
                arrLstOp.Clear();
                arrLstValue.Clear();
                recordsCountSummit = 0;
                string scrollIndex = Main.InputData[1];
                if(appFlg.Equals(Constants.appSummit)){
                    scrollObjActual = scrollObj +"["+scrollIndex+"]";
                    logicalColName = fetchElement(Main.InputData[2]);
                }
            }
            else{
                logicalColName = fetchElement(Main.InputData[1]);
                if(inputLen<=4 || inputLen>=3){
                    rowIndex = Int32.Parse(Main.InputData[2]);
                    logicalColName = CellElement(logicalColName, rowIndex);
                }
                if(inputLen==4){
                    textValue = Main.InputData[3];
                }
            }
            
        }
        
        public int Table(){
            //to get the # of I/Ps being passed
            int inputCount = arrLstIp.Count;
            int outputCount = arrLstOp.Count;
            
            //to get first element column in table
            string colElementStatic = fetchElement(arrLstIp[0]);
            string colElement = colElementStatic;
            //to get first element value to be searched in table
            string valueSearched = arrLstValue[0];
            //to get the # of rows visible in table
            //int count = VisibleRowCount(colElement);
            string cellValue = null;
            Report.Info("Table Scroll Object Actual---"+scrollObjActual);
            RxPath scrollObjRx = scrollObj;
            IList<Element> scrollBarList = Host.Local.Find(scrollObjRx);
            int scrollerCount = scrollBarList.Count;
            Report.Info("Table Scroller Count on Page--"+scrollerCount.ToString());
            ScrollBar tableScrollerActual = null;
            int scrollIndexInput = Int32.Parse(Main.InputData[1]);
            if(scrollIndexInput<=scrollerCount){
                for(int i=1;i<=scrollerCount;i++){
                    if(i==scrollIndexInput){
                        tableScrollerActual = scrollBarList[i-1].As<ScrollBar>();
                        Report.Info("MAXVALUE--"+tableScrollerActual.MaxValue.ToString());
                    }
                }
            }else{
                throw new Exception ("invalid Index parameter for Table Scroller"+scrollIndexInput);
            }
            ScrollBar tableScroller = tableScrollerActual;
            int visibleAmount = Int32.Parse(tableScroller.Element.GetAttributeValueText("VisibleAmount"));
            Report.Info("visibleAmount--"+visibleAmount.ToString());
            IList<Button> scrollBtns = tableScroller.FindDescendants<Button>();
            int btnCount = scrollBtns.Count;
            Report.Info("Scroll buttons count--"+btnCount);
            Button scrollDwonBtn = scrollBtns[0];
            Button scrollUpBtn = scrollBtns[1];
            scrollDwonBtn.Click();
            scrollUpBtn.Click();            
            int maxValue = Int32.Parse(tableScroller.Element.GetAttributeValueText("MaxValue"));
            Report.Info("maxValue--"+maxValue.ToString());
            int counter = visibleAmount;
            IList<Element> list = Host.Local.Find(colElement);
            for(int i=1;i<=list.Count;i++){
                cellValue = list[i-1].GetAttributeValueText("Text");
                if(!String.IsNullOrEmpty(cellValue)){
                    if(cellValue.Contains(valueSearched)){
                        rowNum = i;
                        if(inputCount > 1){
                            rowNum = MultipleFieldsSearch(inputCount, rowNum);
                        }
                        
                    }
                }
                if (rowNum != -1){
                    colElementSelectRow(CellElement(colElementStatic,rowNum));
                    break;
                }
                if(i == visibleAmount && maxValue >= counter ){
                    if(maxValue == counter){
                        int newMaxValue = Int32.Parse(tableScroller.Element.GetAttributeValueText("MaxValue"));
                        Report.Info("newMaxValue---"+newMaxValue);
                        if(newMaxValue>maxValue){
                            maxValue = newMaxValue;
                        }
                    }
                    int k=0;
                    btnClicks = maxValue-counter;
                    if(btnClicks<visibleAmount){
                        while(k<btnClicks){
                            scrollDwonBtn.Click();
                            k++;
                        }
                        i=visibleAmount-btnClicks;
                        counter = counter+btnClicks;
                        Report.Info("counter--"+counter);
                    }else if(btnClicks == visibleAmount ){
                        while(k<visibleAmount){
                            scrollDwonBtn.Click();
                            k++;
                        }
                        i=0;
                        counter = visibleAmount+counter;
                        Report.Info("counter--"+counter);
                    }
                    else{
                        while(k<visibleAmount){
                            scrollDwonBtn.Click();
                            k++;
                        }
                        i=0;
                        counter = visibleAmount+counter;
                        Report.Info("counter--"+counter);
                    }
                    
                }
                
                
            }
            
            if(rowNum!=-1 && outputCount != 0){
                for(int opCount =0; opCount<outputCount; opCount++){
                    colElement = fetchElement(arrLstOp[opCount]);
                    cellValue = colElementCellValueOutput(colElement, rowNum);
                    outputData.Add(cellValue);
                }
            }
            
            return rowNum;
            
        }
        
        private int readTable(int inputCount, string colElement, string cellValue, string valueSearched, int i, int rowNum ){
            if(!String.IsNullOrEmpty(cellValue)){
                if(cellValue.Contains(valueSearched)){
                    rowNum = i;
                    if(inputCount > 1){
                        for(int j=1; j<inputCount;j++){
                            colElement = fetchElement(arrLstIp[j]);
                            cellValue = colElementCellValueOutput(colElement, i);
                            bool searchBlank = String.IsNullOrEmpty(arrLstValue[j]);
                            bool cellBlank = String.IsNullOrEmpty(cellValue);
                            if(searchBlank && cellBlank){
                                break;
                            }
                            if(!(searchBlank && !cellBlank)){
                                if(!cellValue.Contains(arrLstValue[j])){
                                    rowNum = -1;
                                    break;
                                }
                                
                            }else{
                                rowNum = -1;
                                break;
                            }
                            
                        }
                        
                        
                    }
                    
                }
                
            }
            
            return rowNum;
        }
        
        private int VisibleRowCount(string colElement){
            IList<Text> list = Host.Local.Find<Text>(colElement);
            int count = list.Count;
            return count;
        }
        
        private string CellElement(string colElement, int i){
            return colElement+"["+i+"]";
        }
        
        private string CellElementValue(Text text){
            return text.Element.GetAttributeValueText("Text");
        }
        
        
        private string colElementCellValueOutput(string colElement, int rowNum){
            string cellValue = null;
            ComboBox cmBox = null;
            CheckBox chBox = null;
            Button btn = null;
            RxPath colElementRx = null;
            
            if(!colElement.Contains("//text")){
                if(colElement.Contains("//combobox")){
                    //cmBox = CellElement(colElement, rowNum);
                    colElementRx = colElement;
                    cmBox = Host.Local.Find(colElementRx)[rowNum-1];
                    cellValue = cmBox.Element.GetAttributeValueText("Text");
                }else if(colElement.Contains("//checkbox")){
                    //chBox = CellElement(colElement, rowNum);
                    colElementRx = colElement;
                    chBox = Host.Local.Find(colElementRx)[rowNum-1];
                    cellValue = chBox.Element.GetAttributeValueText("Checked");
                    if(cellValue.Equals("True")){
                        cellValue = "Y";
                    }else{
                        cellValue = "N";
                    }
                }else if(colElement.Contains("//button")){
                    btn = CellElement(colElement, rowNum);
                    cellValue = btn.Element.GetAttributeValueText("Enabled");
                }
                return cellValue;
            }else{
                //string txtElement = CellElement(colElement, rowNum);
                //Element textElement = Host.Local.FindSingle(txtElement);
                colElementRx = colElement;
                Element textElement = Host.Local.Find(colElementRx)[rowNum-1];
                return textElement.GetAttributeValueText("Text");
            }
            
        }
        
        
        private void colElementSelectRow(string colElement){
            ComboBox cmBox = null;
            CheckBox chBox = null;
            Text text = null;
            if(!colElement.Contains("//text")){
                if(colElement.Contains("//combobox")){
                    cmBox = colElement;
                    cmBox.DoubleClick();
                }else if(colElement.Contains("//checkbox")){
                    chBox = colElement;
                    chBox.Focus();
                    chBox.PressKeys("{Tab}");
                }
            }
            else{
                text = TextElementIndexed(colElement);
                text.Click();
            }
        }
        
        private void checkboxOperationClick(string flag, CheckBox checkBox){
            try
            {
                if(flag.Equals("Y") && checkBox.Enabled && !checkBox.Checked){
                    checkBox.Click();
                }else if(flag.Equals("N") && checkBox.Enabled && checkBox.Checked){
                    checkBox.Click();
                }
            }
            catch (Exception e)
            {
                Report.Info(e.StackTrace);
            }
        }
        
        private int MultipleFieldsSearch(int inputCount, int rowNum){
            for(int j=1; j<inputCount;j++){
                string colElement = fetchElement(arrLstIp[j]);
                string cellValue = colElementCellValueOutput(colElement, rowNum);
                bool searchBlank = String.IsNullOrEmpty(arrLstValue[j]);
                bool cellBlank = String.IsNullOrEmpty(cellValue);
                if(searchBlank && cellBlank){
                    break;
                }
                if(!(searchBlank && !cellBlank)){
                    if(!cellValue.Contains(arrLstValue[j])){
                        rowNum = -1;
                        break;
                    }
                    
                }else{
                    rowNum = -1;
                    break;
                }
                
            }
            
            return rowNum;
        }
        
        public int TableActivate(){
            if(rowNumAct != -1){
                rowNumAct = -1;
            }
            //to get the # of I/Ps being passed
            int inputCount = arrLstIp.Count;
            int outputCount = arrLstOp.Count;
            
            //to get first element column in table
            string colElementStatic = fetchElement(arrLstIp[0]);
            RxPath rxPathElement  = colElementStatic;
            //to get first element value to be searched in table
            string valueSearched = arrLstValue[0];
            if(!SearchAndSelectActivateTableCell(valueSearched,rxPathElement,inputCount)){
                //Duration duration = TimeSpan.FromSeconds(Constants.Timeout);                
                RxPath rxPathScroller = scrollObjActual;
                IList<Element> tableScroller = Host.Local.Find(rxPathScroller);
                Report.Info("Table Scroller Count-------"+tableScroller.Count.ToString());
                if(tableScroller.Count>0){
                    IList<Button> buttons = tableScroller[0].As<ScrollBar>().FindDescendants<Button>();
                    int buttonsCount = buttons.Count;
                    Report.Info("Buttons count---"+buttonsCount.ToString());
                    Button button1 = buttons[0];
                    Button button2 = buttons[1];
                    Button button3 = buttons[2];                    
                    Size sizeUp = new Size();
                    int heightUp = 0;
                    if(buttonsCount == 4){
                        Button btnUp = buttons[1];
                        button2 = buttons[2];
                        button3 = buttons[3];
                        sizeUp = btnUp.Element.Size;
                        heightUp = sizeUp.Height;
                    }
                    Size size = button2.Element.Size;
                    Report.Info("pgDownSize--"+size.ToString());
                    int height = size.Height;
                    Report.Info("height--"+height.ToString());
                    int countCLick = 1;
                    Report.Info("counterAct---"+counterAct.ToString());
                    if(height>counterAct){
                        while(countCLick<=counterAct){
                            button3.Click();
                            countCLick++;
                        }
                        if(!SearchAndSelectActivateTableCell(valueSearched,rxPathElement,inputCount)){
                            if(buttonsCount == 3){
                                buttons = tableScroller[0].As<ScrollBar>().FindDescendants<Button>();
                                Button buttonUp = buttons[1];
                                sizeUp = buttonUp.Element.Size;
                                Report.Info("pgUpSize--"+sizeUp.ToString());
                                heightUp = sizeUp.Height;
                                Report.Info("heightUp--"+heightUp.ToString());
                            }
                            countCLick = 1;
                            while(countCLick<=counterAct){
                                button3.Click();
                                countCLick++;
                            }
                            countCLick = 1;
                            if(buttonsCount == 4){
                                height = height+heightUp;
                            }
                            while(heightUp<height && !SearchAndSelectActivateTableCell(valueSearched,rxPathElement,inputCount)){
                                while(countCLick<=counterAct){
                                    button3.Click();
                                    countCLick++;
                                }
                                Button buttonUp = buttons[1];
                                sizeUp = buttonUp.Element.Size;
                                heightUp = sizeUp.Height;
                                countCLick = 1;
                            }
                            if(heightUp==height && rowNumAct==-1){
                                return -1;
                            }
                        }

                    }
                }
                
            }
            
            if(rowNumAct!=-1 && outputCount != 0){
                for(int opCount =0; opCount<outputCount; opCount++){
                    string colElement = fetchElement(arrLstOp[opCount]);
                    string cellValue = colElementCellValueOutputActivate(colElement, rowNumAct);
                    outputData.Add(cellValue);
                }
            }
            return rowNumAct;
        }
        
        private bool SearchAndSelectActivateTableCell(string searchValue, RxPath rxPath, int inputCount){
            rowMultipleAct=1;
            IList<Element> list = Host.Local.Find(rxPath);
            int eleCount = list.Count;
            Report.Info("rxPath--"+rxPath);
            Report.Info("elementsCount--"+eleCount);
            if(eleCount==0){
                return false;
            }
            string cellValue = string.Empty;
            int counter = 0;
            bool elementFound = false;
            if(eleCount>1){
                if(list[0].As<RawText>().RawTextValue == list[1].As<RawText>().RawTextValue){
                    rowMultipleAct=2;
                }
            }
            for(int i=0;i<eleCount;i=i+rowMultipleAct){
                cellValue = list[i].As<RawText>().RawTextValue;
                Report.Info(counter.ToString()+"----------"+cellValue);
                counter++;
                if(cellValue.Trim().Contains(searchValue)){
                    rowNumAct = counter;
                    if(inputCount > 1){
                        rowNumAct = MultipleFieldsSearchActivate(inputCount, rowNumAct, eleCount);
                        if(rowNumAct!=-1){
                            list[i].As<RawText>().Click();
                            elementFound = true;
                            break;
                        }
                    }else{
                        list[i].As<RawText>().Click();
                        elementFound = true;
                        break;
                    }
                    
                }
            }
            counterAct = eleCount/rowMultipleAct;
            return elementFound;
        }
        
        private int MultipleFieldsSearchActivate(int inputCount, int rowNumActivate, int eleCount){
            for(int j=1; j<inputCount;j++){
                string colElement = fetchElement(arrLstIp[j]);
                RxPath rxPath = colElement;
                IList<Element> list = Host.Local.Find(rxPath);
                int eleCountActual = list.Count;
                string cellValue = string.Empty;
                if(eleCountActual>1 && eleCount==eleCountActual && (list[0].As<RawText>().RawTextValue == list[1].As<RawText>().RawTextValue)){
                    cellValue= colElementCellValueOutputActivate(colElement, rowNumActivate);
                    if(!cellValue.Contains(arrLstValue[j])){
                        rowNumActivate = -1;
                        break;
                    }
                }else{
                    cellValue = colElementCellValueOutputActivateNoMultiples(colElement, rowNumActivate);
                    if(!cellValue.Contains(arrLstValue[j])){
                        rowNumActivate = -1;
                        break;
                    }
                }
            }
            return rowNumActivate;
        }
        
        private string colElementCellValueOutputActivate(string colElement, int rowNum){
            int index = rowNum*rowMultipleAct;
            RawText textField = colElement+"["+index+"]";
            return textField.RawTextValue;
        }
        
        private string colElementCellValueOutputActivateNoMultiples(string colElement, int rowNum){
            int index = rowNum;
            RawText textField = colElement+"["+index+"]";
            return textField.RawTextValue;
        }
        
        private int GetCountOfRecordsInTable(){
            int recordCount=0;
            Report.Info("Table Scroll Object Actual---"+scrollObjActual);
            RxPath scrollObjRx = scrollObj;
            IList<Element> scrollBarList = Host.Local.Find(scrollObjRx);
            int scrollerCount = scrollBarList.Count;
            Report.Info("Table Scroller Count on Page--"+scrollerCount.ToString());
            ScrollBar tableScrollerActual = null;
            int scrollIndexInput = Int32.Parse(Main.InputData[1]);
            if(scrollIndexInput<=scrollerCount){
                for(int i=1;i<=scrollerCount;i++){
                    if(i==scrollIndexInput){
                        tableScrollerActual = scrollBarList[i-1].As<ScrollBar>();
                        Report.Info("MAXVALUE--"+tableScrollerActual.MaxValue.ToString());
                    }
                }
            }else{
                throw new Exception ("invalid Index parameter for Table Scroller"+scrollIndexInput);
            }
            ScrollBar tableScroller = tableScrollerActual;
            int visibleAmount = Int32.Parse(tableScroller.Element.GetAttributeValueText("VisibleAmount"));
            Report.Info("visibleAmount--"+visibleAmount.ToString());
            IList<Button> scrollBtns = tableScroller.FindDescendants<Button>();
            int btnCount = scrollBtns.Count;
            Report.Info("Scroll buttons count--"+btnCount);
            Button scrollDwonBtn = scrollBtns[0];
            Button scrollUpBtn = scrollBtns[1];
            scrollDwonBtn.Click();
            scrollUpBtn.Click();
            int maxValue = Int32.Parse(tableScroller.Element.GetAttributeValueText("MaxValue"));
            Report.Info("maxValue--"+maxValue.ToString());
            RxPath colElement = logicalColName;
             IList<Element> list = Host.Local.Find(colElement);
             int elemCount = list.Count;
             string cellValue = string.Empty;
             int countElems = 0;
            for(int i=1;i<=elemCount;i++){
                cellValue = list[i-1].GetAttributeValueText("Text");
                countElems++;
                if(String.IsNullOrEmpty(cellValue)){
                    countElems--;
                    break;
                }
                
             }
             if(countElems<visibleAmount){
                 recordCount=countElems;
             }else if(countElems==visibleAmount && maxValue==visibleAmount){
                 recordCount=visibleAmount;
             }
             else{
                 recordCount = maxValue;
             }
            return recordCount;
        }
    }
    
}
