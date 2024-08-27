/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 19/07/2022
 * Time: 11:03
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
	/// Description of ListOfValues.
	/// </summary>
	public partial class Keywords
	{
		private string operationLOV = null;
		private List<string> arrLstIpLOV = new List<string>();
		private string listOfValuesStr = "/form[@title='"+Settings.getInstance().get("SUMMIT_TITLE")+"']//element[@type='ListView']";
		private int headerCount = 0;
		private int entries = 0;
		private string title = "";
		private bool flagHeader;
		private bool flagCell;
		
		
		public List<string> ListOfValues()
		{
			try
			{
				EvaluateLOVInput();
				switch(operationLOV)
				{
					case "VALIDATE_HEADER":
						flagHeader = HeaderValidationLOV();
						break;
					case "HEADER_AND_ROW_COUNT":
						headerCount = ColumnCountLOV();
						entries = RowCountLOV();
						break;
					case "VALIDATE_CELL":
						flagCell = ValidateCellLOV(arrLstIpLOV[0], arrLstIpLOV[1]);						
						break;
					case "SELECT_ROW":
						SelectRowLOV(arrLstIpLOV[0], arrLstIpLOV[1]);
						break;
					case "GET_TITLE":
						title = GetTitleLOV();
						break;
					case "CLOSE_LOV":
						ClickCancelLOV();
						break;
					default:
						Report.Info("Unknown Operation on ListOfValues " + operationLOV);
						break;
				}
				
				Main.OutputData.Add(Constants.TS_STATUS_PASS);
				if(operationLOV.Equals("HEADER_AND_ROW_COUNT")){
					Main.OutputData.Add(headerCount.ToString());
					Main.OutputData.Add(entries.ToString());
				}else if(operationLOV.Equals("GET_TITLE")){
					Main.OutputData.Add(title);
				}else if(operationLOV.Equals("VALIDATE_HEADER")){
					Main.OutputData.Add(flagHeader.ToString());
				}else if(operationLOV.Equals("VALIDATE_CELL")){
					Main.OutputData.Add(flagCell.ToString());
				}
				return Main.OutputData;
			}catch(Exception e)
			{
				Main.OutputData.Add(Constants.TS_STATUS_FAIL);
				Report.Error(e.Message);
				return Main.OutputData;
			}
		}
		
		
		
		
		
		
		private int ColumnCountLOV(){
			Element listOfValues = listOfValuesStr;
			return Int32.Parse(listOfValues.GetAttributeValueText("ColumnCount"));
		}
		
		private int RowCountLOV(){
			Element listOfValues = listOfValuesStr;
			return Int32.Parse(listOfValues.GetAttributeValueText("RowCount"));
		}
		
		private void ClickOkLOV(){
			try{
		        Button oK = fetchElement("Summit_LOV_BTN_Ok");
				oK.Click();
			}catch(Exception e){
				Report.Error(e.Message);
			}
		}
		
		private void ClickCancelLOV(){
		    Button cancel = fetchElement("Summit_LOV_BTN_Cancel");
			cancel.Click();
		}
		
		private int FindColumnLOV(string colHeader){
			Element listOfValues = listOfValuesStr;
			int count = ColumnCountLOV();
			int colIndex = -1;
			for(int i=0; i<count; i++){
				string colName = listOfValues.As<JavaElement>().InvokeMethod("getHeaderData",new Object[]{i}).ToString();
				if(colName.Equals(colHeader)){
					colIndex = i;
					break;
				}
			}
			return colIndex;
		}
		
		private bool HeaderValidationLOV(){
			bool flag = false;
			int inputLen = arrLstIpLOV.Count;
			int index = -1;
			for(int i=0;i<inputLen;i++){
				index =	FindColumnLOV(arrLstIpLOV[i]);
				if(index != -1){
					flag = true;
					Report.Info("Column Header :"+arrLstIpLOV[i]+" is Present in LOV");
				}else{
					flag = false;
					Report.Info("Column Header :"+arrLstIpLOV[i]+" is NOT Present in LOV");
				}
			}
			
			if(flag){
				return true;
			}else{
				return false;
			}
			
		}
		
		private bool ValidateCellLOV(string colHeader, string cellValue){
			int colIndexV = FindColumnLOV(colHeader);
			int rowIndexV = -1;
			if(colIndexV != -1){
				int rowV = SetValueInFindAndSearchLOV(cellValue);
				if(rowV>0){
					rowIndexV = FindRowLOV(colIndexV, cellValue);
					if(rowIndexV != -1){
						return true;
					}else{
						return false;
					}
					
				}else{
					Report.Info("Cell Value "+cellValue+" NOT is present in LOV");
					return false;
				}
			}else{
				Report.Info("Invalid Column header: "+colHeader);
				return false;
			}
		}
		
		private void SelectRowLOV(string colHeader, string cellValue){
			Element listOfValues = listOfValuesStr;
			int rowIndex = SetValueInFindAndSearchLOV(cellValue);
			if(rowIndex > 1){
				int colIndex = FindColumnLOV(colHeader);
				rowIndex = FindRowLOV(colIndex, cellValue);
				if(rowIndex!=0){
					listOfValues.SetAttributeValue("SelectedRow",rowIndex);
				}
			}
			if(rowIndex == 0){
				ClickCancelLOV();
				throw new Exception("Data not found in LOV: "+cellValue);
			}else{
				ClickOkLOV();
				Keyboard.Press("{Tab}");
			}
			
		}
		
		private void SelectRowLOV(int rowIndex){
			Element listOfValues = listOfValuesStr;
			if(rowIndex != -1){
				listOfValues.SetAttributeValue("SelectedRow",rowIndex);
			}
			
		}
		
		private int FindRowLOV(int colIndex, string cellValue){
			Element listOfValues = listOfValuesStr;
			int rowNum = RowCountLOV();
			int rowIndex = -1;
			for(int i=0; i<rowNum; i++){
				string cell = listOfValues.As<JavaElement>().InvokeMethod("getCellData",new Object[]{colIndex,i}).ToString();
				if(cell.Equals(cellValue) || cell.StartsWith(cellValue)){
					rowIndex = i;
					break;
				}
			}
			return rowIndex;
		}
		
		private string GetTitleLOV(){
		    Form form = fetchElement("Summit_LOV_Element_Form");
			return form.Element.GetAttributeValueText("Title");
		}
		
		private int SetValueInFindAndSearchLOV(string txtValue){
		    Text txtFind = fetchElement("Summit_LOV_TXT_Find");
			txtFind.As<JavaElement>().InvokeMethod("selectAll");
			txtFind.As<JavaElement>().InvokeMethod("Clear");
			txtFind.PressKeys(txtValue+"{Enter}");
			Delay.Seconds(1);
			int rowNumber = RowCountLOV();
			return rowNumber;
		}
		
		private void EvaluateLOVInput(){
			arrLstIpLOV.Clear();
			int inputLen = Main.InputData.Count;
			operationLOV = Main.InputData[0];
			if(operationLOV.Equals("VALIDATE_HEADER")){
				for(int i=1;i<inputLen;i++){
					arrLstIpLOV.Add(Main.InputData[i]);
				}
			}else if(operationLOV.Equals("VALIDATE_CELL") || operationLOV.Equals("SELECT_ROW")){
				arrLstIpLOV.Add(Main.InputData[1]);
				arrLstIpLOV.Add(Main.InputData[2]);
			}
		}
	}
}
