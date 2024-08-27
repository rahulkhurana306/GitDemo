/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 07/09/2022
 * Time: 17:15
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
    /// Description of NavigateToActivateScreen.
    /// </summary>
    public partial class Keywords
    {
    	private List<string> arrLstMenuTree = new List<string>();
    	private int inputLen = 0;
    	
    	public List<string> NavigateToActivateScreen()
    	{
    		try
    		{
    			Main.appFlag = Constants.appActivate;
    			EvaluateMenuInput();
    			TreeItemActivate();    			
    			string sPageObj = fetchElement(Main.InputData[inputLen-1]);
    			RawText iconLabel = sPageObj;
    			iconLabel.Click();
    			RxPath titleBarRx = fetchElement("Activate_Tree_Title_Bar");
    			TitleBar titleBar = null;
    			if(Host.Local.TryFindSingle(titleBarRx, out titleBar)){
    			    Report.Info("Navigation to the given Activate Screen is Successful");
    			}
    			Main.OutputData.Add(Constants.TS_STATUS_PASS);
    			return Main.OutputData;
    		}catch(Exception e)
    		{
    			Main.OutputData.Add(Constants.TS_STATUS_FAIL);
    			Report.Error(e.Message);
    			return Main.OutputData;
    		}
    	}
    	
    	private void EvaluateMenuInput(){
    		arrLstMenuTree.Clear();
    		inputLen = Main.InputData.Count;
    		for(int i=0;i<inputLen-1;i++){
    			arrLstMenuTree.Add(Main.InputData[i]);
    		}
    	}
    	
    	private void TreeItemActivate(){
    		string startingNode = arrLstMenuTree[0];
    		string rootItem = fetchElement("Activate_Tree_TreeItem_Top_Menu");
    		TreeItem firstItem = rootItem;
    		bool expanded = firstItem.Expanded;
    		if(!expanded){
    			firstItem.Expand();
    		}
            int listCount = arrLstMenuTree.Count;
    		for(int i=1;i<listCount;i++){
    			TreeItem item2 = ActivateTreeExpand(firstItem, arrLstMenuTree[i]);
    			firstItem = item2;
    			if(i==listCount-1){
    				firstItem.Select();
    			}
    			
    		}
    	}
    	
    	private TreeItem ActivateTreeExpand(TreeItem firstItem, string val){
    		string itemPath = firstItem.GetPath().ToString();
    		TreeItem item = itemPath+"/treeitem[@text='"+val+"']";
    		item.Expand();
    		return item;
    	}
    }
}
