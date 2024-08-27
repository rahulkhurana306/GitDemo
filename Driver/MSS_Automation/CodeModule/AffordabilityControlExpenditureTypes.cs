/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 29/11/2022
 * Time: 14:40
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
    /// Description of AffordabilityControlExpenditureTypes.
    /// </summary>
    public partial class Keywords
    {
        string affordControlExpType = string.Empty;
        
        public List<string> AffordabilityControlExpenditureTypes()
        {
            try{
                Main.appFlag = Constants.appActivate;
                affordControlExpType = fetchElement("Activate_Affordability_Control_Element_ExpenditureType_Parent");
                affordControl = fetchElement("Activate_Affordability_Control_Form_AffordabilityControl");
                
                string listValue = Main.InputData[0];
                int count = Main.InputData.Count;
                int eleIndex = 0;
                int visibleSize = 8;
                
                AffordabilityControalTableScrollRightClick(affordControlExpType);
                AffordabilityControlContextMenuTop();
                RawText colExpType = affordControl+"//rawtext[@rawtext='Expenditure Type'][1]";
                int colNum = colExpType.Column;
                int colCells = colNum+1;
                //get the count of the List elements
                string expTypesList = affordControlExpType+"/list";
                RxPath rxpathList = expTypesList;
                IList<Element> expTypes = Host.Local.Find(rxpathList);
                int countList = expTypes.Count;
                int counterVal = 1;
                for(int i=1;i<=countList;i++){
                    //pick the elements text by Rawtext
                    RxPath rxPathElement = affordControlExpType+"/rawtext[@column='"+colCells+"' and @row!='0']";
                    
                    if(SearchAndSelectActivateTableCellAffordabilityControl(listValue,rxPathElement,counterVal)){
                        eleIndex = i;
                        Report.Info("Counter--->"+i+"----eleIndex--->"+eleIndex);
                        break;
                    }
                    int elementLeft = 0;
                    if(i%visibleSize==0){
                        AffordabilityControalTableScrollRightClick(affordControlExpType);
                        AffordabilityControlContextMenuPageDown();
                        RawText colExpType2 = affordControl+"//rawtext[@rawtext='Expenditure Type'][1]";
                        colNum = colExpType2.Column;
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
                            fieldRxPath = affordControlExpType+"/list["+eleIndex+"]/following-sibling::text[@accessibleName='aet_perc'][1]";
                        }else if(columnLabel.Equals("BTL",StringComparison.OrdinalIgnoreCase)){
                            fieldValue = values[1];
                            fieldRxPath = affordControlExpType+"/list["+eleIndex+"]/following-sibling::text[@accessibleName='aet_btl_perc'][1]";
                        }
                        RxPath rxpath = fieldRxPath;
                        setTextIcomeType(rxpath, fieldValue);
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
    }
}
