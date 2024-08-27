/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 04/11/2022
 * Time: 12:41
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of AffordabilityControlIncommeTypes.
    /// </summary>
    public partial class Keywords
    {
    
        string affordControlIncomeType = string.Empty;
        string affordControl = string.Empty;
        List<String> incomeTypesOutput = new List<string>();
    	public List<string> AffordabilityControlIncommeTypes()
    	{
    	    try{
    	        Main.appFlag = Constants.appActivate;
    	        affordControlIncomeType = fetchElement("Activate_Affordability_Control_Element_IncomeType_Parent");
                affordControl = fetchElement("Activate_Affordability_Control_Form_AffordabilityControl");
    	        
    	        string listValue = Main.InputData[0];
    	        int count = Main.InputData.Count;
    	        int eleIndex = 0;
    	        int visibleSize = 7;
    	        
    	        AffordabilityControalTableScrollRightClick(affordControlIncomeType);
    	        AffordabilityControlContextMenuTop();
    	        RawText colIncomeType = affordControl+"//rawtext[@rawtext='Income Type'][1]";
    	        int colNum = colIncomeType.Column;
    	        int colCells = colNum+1;
    	        
    	        //get the count of the List elements
    	        string incomeTypesList = affordControlIncomeType+"/list";
    	        RxPath rxpathList = incomeTypesList;
    	        IList<Element> incomeTypes = Host.Local.Find(rxpathList);
    	        int countList = incomeTypes.Count;
    	        int counterVal = 1;
    	        for(int i=1;i<=countList;i++){
    	            //pick the elements text by Rawtext
    	            RxPath rxPathElement = affordControlIncomeType+"/rawtext[@column='"+colCells+"' and @row!='0']";
    	            
    	            if(SearchAndSelectActivateTableCellAffordabilityControl(listValue,rxPathElement,counterVal)){
    	                eleIndex = i;
    	                Report.Info("Counter--->"+i+"----eleIndex--->"+eleIndex);
    	                break;
    	            }
    	            int elementLeft = 0;
    	            if(i%visibleSize==0){
    	                AffordabilityControalTableScrollRightClick(affordControlIncomeType);
    	                AffordabilityControlContextMenuPageDown();
    	                RawText colIncomeType2 = affordControl+"//rawtext[@rawtext='Income Type'][1]";
    	                colNum = colIncomeType2.Column;
    	                colCells = colNum+1;
    	                counterVal = 0;
    	                elementLeft = countList-i;
    	            }
    	            if(elementLeft<visibleSize && elementLeft!=0){
    	                counterVal= (visibleSize-elementLeft)+1;
    	            }else{
    	                counterVal++;
    	            }
    	        }
    	        
    	        if(eleIndex!=0 && count>1){
    	            for(int i=1;i<count;i++){
    	                string[] values = Main.InputData[i].Split(':');
    	                string columnLabel = values[0];
    	                string fieldValue = string.Empty;
    	                string fieldRxPath = string.Empty;
    	                if(columnLabel.Equals("RES",StringComparison.OrdinalIgnoreCase)){
    	                    fieldValue = values[1];
    	                    fieldRxPath = affordControlIncomeType+"/list["+eleIndex+"]/following-sibling::text[@accessibleName='ait_perc'][1]";
    	                }else if(columnLabel.Equals("BTL",StringComparison.OrdinalIgnoreCase)){
    	                    fieldValue = values[1];
    	                    fieldRxPath = affordControlIncomeType+"/list["+eleIndex+"]/following-sibling::text[@accessibleName='ait_btl_perc'][1]";
    	                    
    	                }else if(columnLabel.Equals("ICR",StringComparison.OrdinalIgnoreCase)){
    	                    fieldValue = values[1];
    	                    fieldRxPath = affordControlIncomeType+"/list["+eleIndex+"]/following-sibling::text[@accessibleName='ait_icr_perc'][1]";
    	                    
    	                }
    	                RxPath rxpath = fieldRxPath;
    	                if(fieldValue.ToUpper().Equals("GET_VALUE")){
    	                    string val = getTextIcomeType(rxpath);
    	                    incomeTypesOutput.Add(val);
    	                }else{
    	                    setTextIcomeType(rxpath, fieldValue);
    	                }
    	            }
    	        }
    	        
    	        Main.OutputData.Add(Constants.TS_STATUS_PASS);
    	        int lenOp = incomeTypesOutput.Count;
    	        for(int i=0;i<lenOp;i++){
    	            Main.OutputData.Add(incomeTypesOutput[i]);
    	        }
    	        incomeTypesOutput.Clear();
    	        return Main.OutputData;
    	    }catch(Exception e){
    	        Main.OutputData.Add(Constants.TS_STATUS_FAIL);
    	        Main.OutputData.Add(e.Message);
    	        return Main.OutputData;
    	    }
    		
    	}
    	
    	private void setTextIcomeType(RxPath pathValue, string fieldValue){
    		Text text = Host.Local.FindSingle(pathValue, duration).As<Text>();
    		setText(text, fieldValue);
    	}

    	private string getTextIcomeType(RxPath pathValue){
    	    Text field = Host.Local.FindSingle(pathValue, duration).As<Text>();
    	    string val= field.GetAttributeValue<string>("Text");
    	    return val;
    	}
    	
    	private bool SearchAndSelectActivateTableCellAffordabilityControl(string searchValue, RxPath rxPath, int i){
    		IList<Element> list = Host.Local.Find(rxPath);
    		string cellValue = string.Empty;
    		bool elementFound = false;

    		int j = i-1;
    		cellValue = list[i+j].As<RawText>().RawTextValue;
    		if(cellValue.Trim().Equals(searchValue, StringComparison.OrdinalIgnoreCase)){
    			list[i+j].As<RawText>().Click();
    			SelectRawTextValueAC(searchValue);
    			elementFound = true;
    		}
    		return elementFound;
    	}
    	
    	private void SelectRawTextValueAC(string textValue){
    		RawText val = "/form[@title='']/rawtext[@RawText='"+textValue+"']";
    		val.Click();
    	}
    	
    	
    	private void AffordabilityControalTableScrollRightClick(string scrollerParentType){
    	    string indicatorIncomeType = scrollerParentType+"/scrollbar[@accessiblename='Vertical']/indicator[@accessiblename='Position']";
    	    RxPath indicator = indicatorIncomeType;
    	    Indicator incomeTypeIndicator = Host.Local.FindSingle(indicator, duration).As<Indicator>();
    	    incomeTypeIndicator.EnsureVisible();
    	    incomeTypeIndicator.Click(MouseButtons.Right);
    	}    	
    	
    	private void AffordabilityControlContextMenuTop(){
    	    string contextMenuTopString = "/contextmenu[@ProcessId='"+PID+"']//menuitem[@accessiblename='Top']";
    	    RxPath contextMenuTop = contextMenuTopString;
    	    Ranorex.MenuItem top = Host.Local.FindSingle(contextMenuTop, duration).As<Ranorex.MenuItem>();
    	    top.Click();
    	}
    	
    	private void AffordabilityControlContextMenuPageDown(){
    	    string contextMenuPageDownString = "/contextmenu[@ProcessId='"+PID+"']//menuitem[@accessiblename='Page Down']";
    	    RxPath contextMenuPageDown = contextMenuPageDownString;
    	    Ranorex.MenuItem pageDown = Host.Local.FindSingle(contextMenuPageDown, duration).As<Ranorex.MenuItem>();
    	    pageDown.Click();
    	}
    	
    }
}
