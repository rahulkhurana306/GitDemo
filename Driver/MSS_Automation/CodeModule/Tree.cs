/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 12/07/2022
 * Time: 19:01
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
    /// Description of Tree.
    /// </summary>
    public partial class Keywords
    {
        private List<string> arrLstIpTree = new List<string>();        
        private string parent = string.Empty;
        private int allSubItemsCount = 0;
        private int immediateChildItemsCount = 0;
        
        public List<string> TREE(){
            try
            {
                EvaluateTreeInput();
                EvaluateTreeParent();
                string appFlg = Main.appFlag;
                if(appFlg.Equals(Constants.appSummit)){
                TreeItemSummit();
                }
                if(appFlg.Equals(Constants.appActivate)){
                TreeItem();
                }
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                Main.OutputData.Add(allSubItemsCount.ToString());
                Main.OutputData.Add(immediateChildItemsCount.ToString());
                return Main.OutputData;
            }catch(Exception e)
            {
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Report.Error(e.Message);
                return Main.OutputData;
            }
        }
        
        
        private void TreeItem(){
            string startingNode = arrLstIpTree[0];
            string rootItem = parent+"/treeitem[@text>'"+startingNode+"']";
            if(TreeElementIsDisplayed(rootItem)){
                TreeItem firstItem = rootItem;
                bool expanded = firstItem.Expanded;
                bool isSelected = firstItem.Selected;
                if(!isSelected){
                    firstItem.Select();
                }
                if(!expanded){
                    firstItem.Expand();
                }

                IList<TreeItem> treeItemsChild = null;
                int counter = 0;
                for(int i=1;i<arrLstIpTree.Count;i++){
                    counter++;
                    treeItemsChild = firstItem.Items;
                    bool childItemFound = false;
                    foreach (TreeItem item in treeItemsChild) {
                        if(item.Text.Contains(arrLstIpTree[i])){
                            item.Select();
                            firstItem = item;
                            if(!item.Expanded){
                                item.Expand();
                            }
                            childItemFound = true;
                            break;
                        }
                    }
                    
                    if(i==counter && !childItemFound){
                        throw new Exception("Child Treeitem: "+arrLstIpTree[i]+" is Not Found in the searched Tree");
                    }
                    
                }
                
                TreeItem lastItem = firstItem;
                allSubItemsCount = lastItem.DescendantItems.Count;
                immediateChildItemsCount = lastItem.Items.Count;
                
            }else{
                throw new Exception("Treeitem Root Node: "+startingNode+" is NOT Found");
            }
            
            
        }
        
        
        private void EvaluateTreeInput(){
            arrLstIpTree.Clear();
            int inputLen = Main.InputData.Count;
            for(int i=0;i<inputLen;i++){
                arrLstIpTree.Add(Main.InputData[i]);
            }
        }
        
        private Boolean TreeElementIsDisplayed(string elementValue){
            Boolean displayed = false;
            try{
                TreeItem element = elementValue;
                if(element.Visible){
                    displayed = true;
                }
                return displayed;
            }catch(Exception){
                return displayed;
            }
        }
        
        private void EvaluateTreeParent(){
            string appFlg = Main.appFlag;
            if(appFlg.Equals(Constants.appActivate)){
                parent = fetchElement("Activate_Tree_TreeItem_Parent");
            }
            if(appFlg.Equals(Constants.appSummit)){
                parent = fetchElement("Summit_Tree_TreeItem_Parent");
            }
        }
        
        private void TreeItemSummit(){
            string startingNode = arrLstIpTree[0];
            string rootItem = parent+"/treeitem[@text>'"+startingNode+"']";
            if(TreeElementIsDisplayed(rootItem)){
                TreeItem firstItem = rootItem;
                bool expanded = firstItem.Expanded;
                bool isSelected = firstItem.Selected;
                if(!isSelected){
                    firstItem.Select();
                }
                if(!expanded){
                    firstItem.Expand();
                }

                IList<TreeItem> treeItemsChild = null;
                int counter = 0;
                for(int i=1;i<arrLstIpTree.Count;i++){
                    counter++;
                    treeItemsChild = firstItem.Items;
                    bool childItemFound = false;
                    foreach (TreeItem item in treeItemsChild) {
                        if(i==arrLstIpTree.Count-1 && arrLstIpTree[i].Split('|').Length>1){
                            string[] multipleItems = arrLstIpTree[i].Split('|');
                            TreeItem lastItemParent = firstItem;
                            IList<TreeItem> itemTree = lastItemParent.Items;
                            for(int j=0;j<multipleItems.Length;j++){
                                Keyboard.Press("{LControlKey down}");
                                foreach(TreeItem siblingItem in itemTree){
                                    String strVal = multipleItems[j];
                                    string actualItem=string.Empty;
                                    try{
                                        actualItem = Main.GlobalData[strVal];
                                    }catch(Exception){
                                        actualItem= strVal;
                                    }
                                    if(siblingItem.Text.Contains(actualItem) && !siblingItem.Selected){
                                        siblingItem.Click();
                                        childItemFound = true;
                                        break;
                                    }
                                    
                                }
                                Keyboard.Press("{LControlKey up}");
                            }
                            break;
                            
                        }else{
                            if(item.Text.Contains(arrLstIpTree[i])){
                                item.Select();
                                firstItem = item;
                                if(!item.Expanded){
                                    item.Expand();
                                }
                                childItemFound = true;
                                break;
                            }
                        }
                        
                    }
                    
                    if(i==counter && !childItemFound){
                        throw new Exception("Child Treeitem: "+arrLstIpTree[i]+" is Not Found in the searched Tree");
                    }
                    
                }
                
                TreeItem lastItem = firstItem;
                allSubItemsCount = lastItem.DescendantItems.Count;
                immediateChildItemsCount = lastItem.Items.Count;
                
            }else{
                throw new Exception("Treeitem Root Node: "+startingNode+" is NOT Found");
            }
            
            
        }
        
    }
}
