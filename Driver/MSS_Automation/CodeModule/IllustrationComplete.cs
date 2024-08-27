/*
 * Created by Ranorex
 * User: rajjoshi
 * Date: 22/11/2022
 * Time: 10:28
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of IllustrationComplete.
    /// </summary>
    public partial class Keywords
    {
        private int letterReviewDocCounter = 1;
        private int eleCountDocName = 1;
        public List<string> IllustrationComplete()
        {
            try{
                Main.appFlag = Constants.appActivate;
                OpenMacro(TestDataConstants.Act_IllGenDetails_Macro_Two);
                RxPath btnOkProcFeeDetailsRx = fetchElement("Activate_Procurement_Fee_Details_BTN_OK");
                Button btnOkPFD = null;
                Duration durationTimePS = TimeSpan.FromMilliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                if(Host.Local.TryFindSingle(btnOkProcFeeDetailsRx, durationTimePS,out btnOkPFD)){
                    btnOkPFD.Click();
                }
                SaveIllustrationPopUp();
                MissingDetailsInvalidUWCriteria();
                PrintPackageReviewDocument();
                LetterProductionLetterReview();
                for(int i=0;i<eleCountDocName;i=i+letterReviewDocCounter){
                    PrintHandleOkProceed();
                }
                IllustrationDIPControl();
                
                Main.OutputData.Add(Constants.TS_STATUS_PASS);
                return Main.OutputData;
                
            }catch(Exception e){
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                Main.OutputData.Add(e.Message);
                return Main.OutputData;
            }
        }
        
        private void SaveIllustrationPopUp(){
            try{
            Button btnOk = fetchElement("Activate_Illustration_and_DIP_Control_BTN_OK");
            btnOk.Click();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            }catch(Exception e){
                throw new Exception(e.Message);
            }
            
        }
        
        private void MissingDetailsInvalidUWCriteria(){
            try{
            Button btnClose = fetchElement("Activate_Missing Details_BTN_Close");
            btnClose.Click();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void PrintPackageReviewDocument(){
            try{
            Button btnOk = fetchElement("Activate_Print Package Review Document_BTN_OK");
            btnOk.Click();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
        private void LetterProductionLetterReview(){
            try{
                RxPath rxPath = fetchElement("Activate_Letter Production Letter Review_TBL_RAWTXT_COL_DocumentName");
                IList<Element> list = Host.Local.Find(rxPath);
                eleCountDocName = list.Count;
                Report.Info("eleCountDocName--"+eleCountDocName);
                if(eleCountDocName>=2){
                    letterReviewDocCounter=2;
                }
                Button btnOk = fetchElement("Activate_Letter Production Letter Review_BTN_OK");
                btnOk.Click();
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        
         private void IllustrationDIPControl(){
            try{
            Button btnClose = fetchElement("Activate_Illustration_and_DIP_Control_BTN_Close");
            btnClose.Click();
            Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }

        private void PrintHandleOkProceed()
        {
            
            try{
                RxPath btnOkRx = fetchElement("Activate_Summit_Activate_Module_BTN_OK");
                Button btnOk = null;
                if(Host.Local.TryFindSingle(btnOkRx, duration,out btnOk)){
                    btnOk.Click();
                    Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("VERY_SHORT_WAIT")));
                }
            }
            catch(Exception e){
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
