/*
 * Created by Ranorex
 * User: PROJ-MSS-ATEST03.SVC
 * Date: 08/06/2022
 * Time: 16:14
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ranorex;
using System.Diagnostics;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of Utility.
    /// </summary>
    public class Utility
    {
        private static Utility utility = new Utility();
        
        private Utility()
        {
        }
        
        public static Utility getInstance(){
            return utility;
        }
        
        public string GenerateAcctNumber(string globalAccType,string accountNumberFormat){
            OracleUtility oraUtility = OracleUtility.Instance();
            Settings setting=Settings.getInstance();
            string sequenceName=setting.get("ACC_NO_SEQ").Trim();
            if (String.IsNullOrWhiteSpace(sequenceName)) {
                sequenceName=Constants.defaultSequenceName;
            }
            string sqlQuery="select "+sequenceName+".nextval from dual";
            
            List<string[]> oraResult=oraUtility.executeQuery(sqlQuery);
            string sequence="0";
            if(oraResult.Count >0)
            {
                sequence =  oraResult[0][0];
            }

            long seq=Int64.Parse(sequence);
            string accFormat;
            if(String.IsNullOrEmpty(accountNumberFormat))
            {
                accFormat=Settings.getInstance().get("ACC_NO_FORMAT_"+globalAccType);
            }else
            {
                accFormat=accountNumberFormat;
            }
            //, is used as delimeter in CSV file as a separator, so as a workaround used # and replaced here with ,
            accFormat = accFormat.Replace('#',',');
            string accNo;
            try {
                accNo=String.Format(accFormat,seq);
            } catch (Exception e) {
                Report.Error("Account Number generation - formatting issue."+e.Message);
                accNo="EXCEPT"+seq;
            }
            
            return accNo;
        }
        
        public string rndString(int len){
            Random random = new Random();
            var rString = "";
            for (int i = 0; i < len; i++)
            {
                rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
            }
            return rString.ToString();
        }
        
        public static String rnd2(){
            Random rnd = new Random();
            return rnd.Next(111111,999999).ToString();
        }
        
        public string ProcessWCALDate(string WCALType){
            return ProcessWCALDate(WCALType,"1");
        }
        
        public string ProcessWCALDate(string FormattedWCALString,string society){
            FormattedWCALString = FormattedWCALString.Replace(" ","");
            string wcalValue = FormattedWCALString;
            string appFlg = Main.appFlag;
            if(!String.IsNullOrWhiteSpace(FormattedWCALString) && FormattedWCALString.Contains("WCAL")){
                OracleUtility oracUtility=OracleUtility.Instance();
                string query="Select S.Soc_Seqno, TO_CHAR(S.Soc_Wcal_Primary_Date,'dd-MON-yyyy') From Societies s where S.Soc_Seqno='"+society.Trim()+"'";
                List<string[]> result=oracUtility.executeQuery(query);
                if(result.Count>=1){
                    wcalValue=result[0][1]; //Select values the first record.
                    
                    System.DateTime wcalDateTime = System.DateTime.Parse(wcalValue);string inputPattern="WCAL_DATE(([\\+\\-]\\d+)Y)*(([\\+\\-]\\d+)M)*(([\\+\\-]\\d+)D)*";
                    string year=Regex.Replace(FormattedWCALString,inputPattern,"$2");
                    string month=Regex.Replace(FormattedWCALString,inputPattern,"$4");
                    string day=Regex.Replace(FormattedWCALString,inputPattern,"$6");
                    
                    
                    if(!String.IsNullOrWhiteSpace(year))
                        wcalDateTime=wcalDateTime.AddYears(Int32.Parse(year));
                    if(!String.IsNullOrWhiteSpace(month))
                        wcalDateTime=wcalDateTime.AddMonths(Int32.Parse(month));
                    if(!String.IsNullOrWhiteSpace(day))
                        wcalDateTime=wcalDateTime.AddDays(Int32.Parse(day));
                    if(appFlg.Equals(Constants.appSummit)){
                        wcalValue=wcalDateTime.ToString("dd-MMM-yyyy");
                    }else if(appFlg.Equals(Constants.appActivate)){
                        wcalValue=wcalDateTime.ToString("dd/MM/yyyy");
                    }
                    wcalValue=wcalValue.ToUpper();
                }
            }
            return wcalValue;
        }
        
        public string formattedAccount(string accountNumber)
        {
            return formattedSubAccount(accountNumber,"");
        }
        
        public string formattedSubAccount(string accountNumber,string subAcc)
        {
            string formattedAccount= Regex.Replace(accountNumber,"([A-Z]*)(\\d+)([A-Z]*)","$1/$2/$3");
            //Console.WriteLine("::::"+formattedAccount);

            char [] formattedAccountArray=formattedAccount.ToCharArray();

            if(formattedAccountArray[0]=='/')
            {
                formattedAccount=formattedAccount.Substring(1);
            }
            formattedAccountArray=formattedAccount.ToCharArray();
            if(formattedAccountArray[formattedAccount.Length-1]=='/')
            {
                formattedAccount=formattedAccount.Substring(0,formattedAccount.Length-1);
            }
            if(!String.IsNullOrEmpty(subAcc))
                formattedAccount=formattedAccount+"/"+subAcc;
            
            return formattedAccount;
            
            
        }
        
        public string GetOpenBatch(string society){
            string sequence="0";
            string query="";
            while(true){
                string sequenceName=Settings.getInstance().get("BATCH_NO_SEQ");
                if (!String.IsNullOrWhiteSpace(sequenceName)) {
                    query="select "+sequenceName+".nextval as NEXTVAL from dual";
                }
                List<string[]> result=OracleUtility.Instance().executeQuery(query);
                if(result.Count >0)
                {
                    sequence = result[0][0];
                }
                string query2  ="select BATT_BATCH_NO from BATCH_TOTALS where BATT_BATCH_NO='"+sequence+"' and BATT_SOC_SEQNO='"+society+"'";
                List<string[]> result2=OracleUtility.Instance().executeQuery(query2);
                if(result2.Count== 0){
                    break;
                }
            }
            return sequence;
        }
        /**
         *Code to genearate unique Event code and check that code doesn't exists in database
         ***/
        public string GetEventCode(string society){
            int maxRetry=Int32.Parse(Settings.getInstance().get("SUMMIT_OPEN_EVENTS_MAX_RETRY"));
            string eventcode="";
            while(maxRetry-->0)
            {
                eventcode=rndString(6).ToUpper();
                string query="select EVENTS_CODE from EVENTS where EVENTS_CODE='"+eventcode+"' and EVENTS_SOC_SEQNO='"+society+"'";
                List<string[]> result=OracleUtility.Instance().executeQuery(query);
                if(result.Count == 0){
                    return eventcode;
                }
            }
            return eventcode;
        }
        
        public string GenerateUniqueAccountType()
        {
            string GeneratedAccType=String.Empty;
            try
            {
                string sequenceName=Settings.getInstance().get("ACC_NO_SEQ").Trim();
                if (String.IsNullOrWhiteSpace(sequenceName)) {
                    sequenceName=Constants.defaultSequenceName;
                }
                string sqlQuery="select "+sequenceName+".nextval from dual";
                
                List<string[]> oraResult=OracleUtility.Instance().executeQuery(sqlQuery);
                string sequence="0";
                if(oraResult.Count >0)
                {
                    sequence =  oraResult[0][0];
                }
                //Assuming that account-type with pure numbers will never exists in touch-stone base database.
                GeneratedAccType=sequence;
                
                Report.Info("Generated Unique AccountType="+GeneratedAccType);
                return GeneratedAccType;
            } catch (Exception e) {
                Report.Info("Exception while generating Unique AccountType, ErrMSg="+e.Message);
                GeneratedAccType=rndString(6).ToUpper();
                return GeneratedAccType;
            }
        }
        
        public string GenerateUniqueNICode(string niCode)
        {
            //Example : ABCD#D --> ABCD123456D
            string placeHolder="#";
            string GeneratedNiCode;
            int maxRetry=Int32.Parse(Settings.getInstance().get("SUMMIT_NICODE_MAX_RETRY"));
            List<string[]> result;
            string query;
            if(!string.IsNullOrWhiteSpace(niCode) && niCode.IndexOf(placeHolder)>0){
                while(maxRetry-->0)
                {
                    Random rnd = new Random();
                    int num = rnd.Next(0,999999);
                    string x = num.ToString();
                    x=x.PadLeft(6,'0');
                    GeneratedNiCode=niCode.Replace(placeHolder,x);
                    
                    query="select 1 from CUST_PERSONAL_DETAILS where CUSTP_NI_CODE='"+GeneratedNiCode+"'";
                    
                    result=OracleUtility.Instance().executeQuery(query);
                    //Randomely generated batch number doesn't exists - safe to use.
                    if(result.Count == 0){
                        Report.Info("Generated Unique NICode="+GeneratedNiCode);
                        return GeneratedNiCode;
                    }
                }
                
                Report.Error("Failed to generated Unique NICode after max retries.");
            }
            return niCode;
        }
        
        public static void Capture_Screenshot(){
            if(Constants.captureScreenshot){
                Report.Screenshot();
            }
        }
        
        public void Delete_files_server(string folderLocation, string filemask)
        {
            string strHostName = Settings.getInstance().get("SUMMIT_REMOTE_HOST");
            string strHostUserName = Settings.getInstance().get("SUMMIT_REMOTE_USER");
            string strHostPassword = Settings.getInstance().get("SUMMIT_REMOTE_PASSWORD");
            string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
            string serverPathDefault = Settings.getInstance().get("SERVER_PATH_FILECOPY_DEFAULT");
            folderLocation = serverPathDefault +"//"+ strEnvName +"//"+ folderLocation;
            Process p = Process.Start(Constants.Path_TransferBatFile_test,folderLocation+" "+strHostName+" "+strHostUserName+" "+strHostPassword+" "+filemask);
            p.WaitForExit(Constants.FileTransfer_Timeout);
        }
        
        
        public string getDocsContent(string docPath ){
            object path = docPath;
            string totaltext = string.Empty ;

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

            object miss = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            for (int i = 1; i <= docs.Paragraphs.Count; i++)
            {
                totaltext += docs.Paragraphs[i].Range.Text.ToString();
            }
            totaltext=totaltext.Replace(@"\r","\a").Replace(@"\a","\a");
            totaltext=System.Text.RegularExpressions.Regex.Unescape(totaltext).Replace("\r"," ").Replace("\a"," ").Replace("\f"," ");
			
            ((Microsoft.Office.Interop.Word._Document)docs).Close(false);
            ((Microsoft.Office.Interop.Word._Application)word).Quit(false);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(word);
            
            return totaltext;
            
        }
    }
}