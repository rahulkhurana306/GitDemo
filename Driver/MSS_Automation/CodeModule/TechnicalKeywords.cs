/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 01/07/2022
 * Time: 18:04
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Ranorex;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Ranorex.Plugin;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of TechnicalKeywords.
    /// </summary>
    public partial class Keywords
    {
        public List<string> SUBSTRING(){
            List<string> output = new List<string>();
            
            string srcString = Main.InputData[0];
            string startIndex = Main.InputData[1];
            string length = Main.InputData[2];
            
            int sIndex=Int32.Parse(startIndex);
            int len=Int32.Parse(length);
            
            
            string result=srcString.Substring(sIndex,len);
            
            output.Add(Constants.TS_STATUS_PASS);
            output.Add(result);
            return output;
            
        }
        public List<string> EQUALS()
        {
            List<string> output = new List<string>();
            string input1 = Main.InputData[0];
            string input2 = Main.InputData[1];
            if(string.IsNullOrEmpty(input1)){
                input1 = string.Empty;
            }
            if(string.IsNullOrEmpty(input2)){
                input2 = string.Empty;
            }
            input1=input1.Trim();
            input2=input2.Trim();
            Boolean result=input1.Equals(input2);
            
            if(result)
            {
                output.Add(Constants.TS_STATUS_PASS);
            }else{
                output.Add(Constants.TS_STATUS_FAIL);
            }
            output.Add(result.ToString());
            return output;
        }
        public List<string> NOT_EQUALS()
        {
            List<string> output = new List<string>();
            string input1 = Main.InputData[0];
            string input2 = Main.InputData[1];
            input1=input1.Trim();
            input2=input2.Trim();
            Boolean result=!(input1.Equals(input2));
            
            if(result)
            {
                output.Add(Constants.TS_STATUS_PASS);
            }else{
                output.Add(Constants.TS_STATUS_FAIL);
            }
            output.Add(result.ToString());
            return output;
        }

        public List<string> REPLACE()
        {
            List<string> output = new List<string>();
            string srcString = Main.InputData[0];
            string oldString = Main.InputData[1];
            string newString = Main.InputData[2];
            
            string result=srcString.Replace(oldString,newString);
            
            output.Add(Constants.TS_STATUS_PASS);
            output.Add(result);
            return output;
        }
        public List<string> SENDKEYS()
        {
            List<string> output = new List<string>();
            string sendKeyText = Main.InputData[0];
            
            try
            {
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SENDKEY_WAIT_PRE")));
                Keyboard.Press(sendKeyText);
                Delay.Milliseconds(Int32.Parse(Settings.getInstance().get("SENDKEY_WAIT_POST")));
                output.Add(Constants.TS_STATUS_PASS);
            } catch (Exception e)
            {
                Report.Error("SendKey keyword failed."+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
                
            }
            return output;
        }
        public List<string> TRIM()
        {
            List<string> output = new List<string>();
            string input = Main.InputData[0];
            string result = input.Trim();
            
            output.Add(Constants.TS_STATUS_PASS);
            output.Add(result);
            return output;
        }
        public List<string> WAIT()
        {
            List<string> output = new List<string>();
            try {
                int delayBy = Int32.Parse(Main.InputData[0]);
                double oldSpeedFactor=Delay.SpeedFactor;
                Delay.SpeedFactor=1.0;
                Delay.Milliseconds(delayBy);
                Delay.SpeedFactor=oldSpeedFactor;
                output.Add(Constants.TS_STATUS_PASS);
            } catch (Exception e) {
                Report.Error("WAIT keyword failed."+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            return output;
        }
        public List<string> FORMAT_DATE()
        {
            List<string> output = new List<string>();
            try {
                string sourceDate = Main.InputData[0];
                string sourceDateFormat = Main.InputData[1];
                string targetDateFormat = Main.InputData[2];
                System.DateTime oDate = System.DateTime.ParseExact(sourceDate,sourceDateFormat,CultureInfo.InvariantCulture);
                string oDateString = oDate.ToString(targetDateFormat);
                output.Add(Constants.TS_STATUS_PASS);
                output.Add(oDateString.ToUpper());
            } catch (Exception e) {
                Report.Error("FORMAT_DATE keyword failed."+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            return output;
        }
        
        public List<string> SEARCH_SUBSTRING(){
            List<string> output = new List<string>();
            
            string parentString = Main.InputData[0];
            string childSring = Main.InputData[1];
            
            try{
                int stringPosition = parentString.IndexOf(childSring);
                output.Add(Constants.TS_STATUS_PASS);
                output.Add(stringPosition.ToString());
                
            } catch (Exception e) {
                Report.Error("SEARCHSUBSTRING Keyword Failed."+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            
            return output;
            
        }
        
        public List<string> COPY_FILES_TO_LOCAL()
        {
            List<string> output = new List<string>();
            string remoteRelSourceDirMode="";
            string fileName="";
            string strFullPath="";
            string localSrcDir="";
            string TargetPath="";
            int inputLen = Main.InputData.Count;
            string strmode = Main.InputData[0];
            string strFolderName = Main.InputData[1];
            string localRelTargetDir = Main.InputData[2];
            string strFileMask = Main.InputData[3];
            if(inputLen>4){
                if(!String.IsNullOrEmpty(Main.InputData[4])){
                    remoteRelSourceDirMode= Main.InputData[4];
                }
            }
            string strDestinationFolder = System.IO.Path.GetFullPath(Constants.projectDir);
            string localRelSrcDir = Settings.getInstance().get("LOCAL_REL_SRC_DIR");
            string localRelativeTargetDir = Settings.getInstance().get("LOCAL_REL_TARGET_DIR");
            string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
            
            if(strmode.Trim().Equals("LOCAL",StringComparison.OrdinalIgnoreCase)){
                if(strFolderName.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                    if(localRelSrcDir.Contains("ng_mss_automation\\")){
                        localRelSrcDir=localRelSrcDir.Replace("ng_mss_automation\\","");
                    }
                    localSrcDir = strDestinationFolder+"\\"+localRelSrcDir;
                }
                else{
                    localSrcDir=Main.InputData[1];
                }
                if(localRelTargetDir.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                    TargetPath = strDestinationFolder+"\\"+localRelativeTargetDir;
                }
                else{
                    TargetPath=Main.InputData[2];
                }
                if(!Directory.Exists(TargetPath))
                {
                    Directory.CreateDirectory(TargetPath);
                }
                string[] filePaths = Directory.GetFiles(TargetPath);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
                try{
                    foreach (string newPath in Directory.GetFiles(localSrcDir,strFileMask+"*",SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(localSrcDir, TargetPath), true);
                    }
                    DirectoryInfo di = new DirectoryInfo(TargetPath);
                    FileInfo[] fi = di.GetFiles("*.*");
                    foreach (FileInfo file in fi)
                    {
                        fileName = file.Name;
                    }
                }
                catch(Exception e){
                    Report.Error("Not able to copy the files from Local directory to other Local directory"+e.Message);
                    output.Add(Constants.TS_STATUS_FAIL);
                }
            }
            if(strmode.Trim().Equals("REMOTE",StringComparison.OrdinalIgnoreCase)){
                string serverPathDefault = Settings.getInstance().get("SERVER_PATH_FILECOPY_DEFAULT");
                string serverPathExport = Settings.getInstance().get("SERVER_PATH_FILECOPY_EXPORT");
                string serverPathPdfExport = Settings.getInstance().get("SERVER_PATH_FILECOPY_PDF");
                string strHostName = Settings.getInstance().get("SUMMIT_REMOTE_HOST");
                string strHostUserName = Settings.getInstance().get("SUMMIT_REMOTE_USER");
                string strHostPassword = Settings.getInstance().get("SUMMIT_REMOTE_PASSWORD");

                string remoteRelSourceDir="";
                
                if(localRelTargetDir.Contains("ng_mss_automation\\")){
                    localRelTargetDir=localRelTargetDir.Replace("ng_mss_automation\\","");
                }
                strFullPath= strDestinationFolder+"\\"+localRelTargetDir+"\\"+strEnvName;
                
                if(!Directory.Exists(strFullPath))
                {
                    Directory.CreateDirectory(strFullPath);
                } else {
                    DirectoryInfo di = new DirectoryInfo(strFullPath);
                    FileInfo[] fi = di.GetFiles("*.*");
                    foreach (FileInfo file in fi)
                    {
                        fileName = file.Name;
                    }
                    string[] filePaths = Directory.GetFiles(strFullPath);
                    foreach (string filePath in filePaths)
                        File.Delete(filePath);
                }
                
                if(remoteRelSourceDirMode.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                    remoteRelSourceDir = serverPathDefault;
                }
                if (remoteRelSourceDirMode.Trim().Equals("export",StringComparison.OrdinalIgnoreCase)){
                    remoteRelSourceDir = serverPathExport;
                }
                if (remoteRelSourceDirMode.Trim().Equals("pdf",StringComparison.OrdinalIgnoreCase)){
                    remoteRelSourceDir = serverPathPdfExport;
                    strEnvName = "/";
                    string PDF_REPORT_PATH = Settings.getInstance().get("PDF_REPORT_PATH");
                    strHostName = PDF_REPORT_PATH;
                }
                try {
                    Process p = Process.Start(Constants.Path_TransferBatFile , strFolderName+" "+strFullPath+" "+strFileMask+" "+strHostName+" "+strEnvName+" "+strHostUserName+" "+strHostPassword+" "+remoteRelSourceDir);
                    p.WaitForExit(Constants.FileTransfer_Timeout);
                    
                    DirectoryInfo di = new DirectoryInfo(strFullPath);
                    FileInfo[] fi = di.GetFiles("*.*");
                    System.DateTime lastWrite = System.DateTime.MinValue;
                    foreach (FileInfo file in fi)
                    {
                        fileName = file.Name;
                    }
                }
                catch (Exception e) {
                    Report.Error("No files copied."+e.Message);
                    output.Add(Constants.TS_STATUS_FAIL);
                }
            }
            if(fileName!= "")
            {
                output.Add(Constants.TS_STATUS_PASS);
                if(strmode.Trim().Equals("LOCAL",StringComparison.OrdinalIgnoreCase)){
                    output.Add(TargetPath);
                }
                if(strmode.Trim().Equals("REMOTE",StringComparison.OrdinalIgnoreCase)){
                    output.Add(strFullPath);
                }
            } else {
                output.Add(Constants.TS_STATUS_FAIL);
                output.Add("No file found with matching file mask format");
            }
            return output;
        }
        
        public List<string> GET_LATEST_FILENAME()
        {
            List<string> output = new List<string>();
            string fileMask="";
            string TargetDirPath="";
            string strLatestFile = "";
            string fileName="";
            int inputLen = Main.InputData.Count;
            string localTargetDirAbsPath = Main.InputData[0];
            string strDestinationFolder = System.IO.Path.GetFullPath(Constants.projectDir);
            string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
            if(localTargetDirAbsPath.StartsWith("C")|| localTargetDirAbsPath.StartsWith("D")|| localTargetDirAbsPath.StartsWith("E")){
                TargetDirPath = localTargetDirAbsPath;
            }
            else{
                if(localTargetDirAbsPath.Contains("ng_mss_automation\\")){
                    localTargetDirAbsPath=localTargetDirAbsPath.Replace("ng_mss_automation\\","");
                }
                TargetDirPath = strDestinationFolder+"\\"+localTargetDirAbsPath +"\\"+strEnvName;
            }
            
            if(inputLen>1){
                if(!String.IsNullOrEmpty(Main.InputData[1])){
                    fileMask= Main.InputData[1];
                }
            }
            try{
                DirectoryInfo di = new DirectoryInfo(TargetDirPath);
                FileInfo[] fi = di.GetFiles("*.*");
                System.DateTime lastWrite = System.DateTime.MinValue;
                foreach (FileInfo file in fi)
                {
                    fileName = file.Name;
                    if(fileMask!=""){
                        int stringPos = fileName.IndexOf(fileMask);
                        if(stringPos != -1 && file.LastWriteTime> lastWrite){
                            strLatestFile = fileName;
                            lastWrite = file.LastWriteTime;
                        }
                    }
                    else{
                        if (file.LastWriteTime > lastWrite)
                        {
                            strLatestFile = fileName;
                            lastWrite = file.LastWriteTime;
                        }
                    }
                }
                output.Add(Constants.TS_STATUS_PASS);
                output.Add(strLatestFile);
            }
            catch (Exception e) {
                Report.Error("GET_LATEST_FILENAME keyword failed."+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            return output;
        }

        public List<string> SEARCH_TEXT_IN_FILES()
        {
            string localDirPath="";
            string fileMask="";
            string firstOccurrence="";
            string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
            string strFolder = System.IO.Path.GetFullPath(Constants.projectDir);
            int inputLen = Main.InputData.Count;
            string searchValue = Main.InputData[0];
            string searchDirPath = Main.InputData[1];
            if(inputLen>2){
                if(!String.IsNullOrEmpty(Main.InputData[2])){
                    fileMask= Main.InputData[2];
                }
            }
            else{
                fileMask="";
            }
            if(searchDirPath.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                string localdirpath = Settings.getInstance().get("LOCAL_PATH_DIR");
                localDirPath = strFolder+"\\"+localdirpath +"\\"+strEnvName;
            }
            else{
              if(searchDirPath.ToUpper().StartsWith("C")|| searchDirPath.ToUpper().StartsWith("D")|| searchDirPath.ToUpper().StartsWith("E")){
                    localDirPath = searchDirPath;
                }
                else{
                    searchDirPath=searchDirPath.Replace("ng_mss_automation\\","");
                    localDirPath = strFolder+"\\"+searchDirPath;
                }
            }
            try{
                string[] allFiles = Directory.GetFiles(localDirPath, fileMask+"*").OrderByDescending(File.GetLastWriteTime).ToArray();
                foreach (string file in allFiles)
                {
                    string[] lines = File.ReadAllLines(file);
                    firstOccurrence = lines.FirstOrDefault(l => l.Contains(searchValue));
                    if (firstOccurrence != null)
                    {
                        Report.Info("Searched text found in file -> " + file);
                        Main.OutputData.Add(Constants.TS_STATUS_PASS);
                        Main.OutputData.Add(file);
                        break;
                    }
                }
                if(firstOccurrence==null){
                    Report.Error("Search text does not exists in files");
                    Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                }
                
            }
            catch(Exception e){
                Report.Error("SEARCH_TEXT_FILE keyword failed"+e.Message);
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
            }
            return Main.OutputData;
        }
        
        public List<string> REPLACE_TEXT_IN_FILE()
        {
            string localDirPath="";
            string firstOccurrence="";
            string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
            string strFolder = System.IO.Path.GetFullPath(Constants.projectDir);
            string localSearchDirPath = Main.InputData[0];
            string fileMask = Main.InputData[1];
            string searchValue = Main.InputData[2];
            string replaceValue = Main.InputData[3];

            if(localSearchDirPath.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                string localdirpath = Settings.getInstance().get("LOCAL_PATH_DIR");
                localDirPath = strFolder+"\\"+localdirpath +"\\"+strEnvName;
            }
            else{
                if(localSearchDirPath.StartsWith("C")|| localSearchDirPath.StartsWith("D")|| localSearchDirPath.StartsWith("E")){
                    localDirPath = localSearchDirPath;
                }
                else{
                    localSearchDirPath=localSearchDirPath.Replace("ng_mss_automation\\","");
                    localDirPath = strFolder+"\\"+localSearchDirPath;
                }
            }
            try{
                string[] allFiles = Directory.GetFiles(localDirPath, fileMask+"*");
                foreach (string file in allFiles)
                {
                    string[] lines = File.ReadAllLines(file);
                    firstOccurrence = lines.FirstOrDefault(l => l.Contains(searchValue));
                    if (firstOccurrence != null)
                    {
                        StreamReader reader = new StreamReader(file);
                        string content = reader.ReadToEnd();
                        reader.Close();

                        content = Regex.Replace(content,searchValue,replaceValue);
                        StreamWriter writer = new StreamWriter(file);
                        writer.Write(content);
                        writer.Close();
                        Report.Info("Replaced text found in file -> " + file);
                        Main.OutputData.Add(Constants.TS_STATUS_PASS);
                        Main.OutputData.Add(localDirPath);
                        break;
                    }
                }

                if(firstOccurrence==null){
                    Report.Error("Search text does not exists in files, hence cannot replace the specified searched text");
                    Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                }
                
            }
            catch(Exception e){
                Report.Error("REPLACE_TEXT_IN_FILE keyword failed"+e.Message);
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
            }
            return Main.OutputData;
        }

        public List<string> COPY_FILES_TO_REMOTE()
        {
            List<string> output = new List<string>();
            string strFullPath="";
            string remoteRelSourceDir="";
            string localSrcDir = Main.InputData[0];
            string strFileMask = Main.InputData[1];
            string remoteRelTargetDir = Main.InputData[2];
            string remoteRelSourceDirMode= Main.InputData[3];

            string strDestinationFolder = System.IO.Path.GetFullPath(Constants.projectDir);
            string serverPathDefault = Settings.getInstance().get("SERVER_PATH_FILECOPY_DEFAULT");
            string serverPathExport = Settings.getInstance().get("SERVER_PATH_FILECOPY_EXPORT");
            string strHostName = Settings.getInstance().get("SUMMIT_REMOTE_HOST");
            string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
            string strHostUserName = Settings.getInstance().get("SUMMIT_REMOTE_USER");
            string strHostPassword = Settings.getInstance().get("SUMMIT_REMOTE_PASSWORD");

            if(localSrcDir.StartsWith("C")|| localSrcDir.StartsWith("D")|| localSrcDir.StartsWith("E")){
                strFullPath = localSrcDir;
            }
            else{
                if(localSrcDir.Contains("ng_mss_automation\\")){
                    localSrcDir=localSrcDir.Replace("ng_mss_automation\\","");
                }
                // Removed this on 27th Mar 23 as per requirement from Sunny (as env name was not needed
                strFullPath = strDestinationFolder+"\\"+localSrcDir;
            }

            if(remoteRelSourceDirMode.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                remoteRelSourceDir = serverPathDefault;
            }
            if (remoteRelSourceDirMode.Trim().Equals("export",StringComparison.OrdinalIgnoreCase)){
                remoteRelSourceDir = serverPathExport;
            }

            if(!Directory.Exists(strFullPath))
            {
                Directory.CreateDirectory(strFullPath);
            }
            string[] allFiles = Directory.GetFiles(strFullPath, strFileMask+"*");
            if(allFiles.Length==0){
                Report.Error("No matching files with filemask found");
                output.Add(Constants.TS_STATUS_FAIL);
            }
            else{
                try {
                    
                    Process p = Process.Start(Constants.Path_TransferBatFile_LocalToRemote , strFullPath+" "+remoteRelTargetDir+" "+strFileMask+" "+strHostName+" "+strEnvName+" "+strHostUserName+" "+strHostPassword+" "+remoteRelSourceDir);
                    p.WaitForExit(Constants.FileTransfer_Timeout);
                    output.Add(Constants.TS_STATUS_PASS);
                }
                catch (Exception e) {
                    Report.Error("Exception in file copying"+e.Message);
                    output.Add(Constants.TS_STATUS_FAIL);
                }
            }
            return output;
        }
        
        public List<string> DELETE_FILES_REMOTE(){
            int inputLen = Main.InputData.Count;
            List<string> output = new List<string>();
            sData = Main.InputData[0];
            try{
                string fileMask = null;
                if(inputLen>1){
                    if(!String.IsNullOrEmpty(Main.InputData[1])){
                        fileMask = Main.InputData[1];
                        utility.Delete_files_server(sData,fileMask);
                        output.Add(Constants.TS_STATUS_PASS);
                    }
                }else{
                    utility.Delete_files_server(sData,"");
                    output.Add(Constants.TS_STATUS_PASS);
                }
            }
            catch (Exception e) {
                Report.Error("Exception in DELETE_FILES_REMOTE keyword"+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            return output;
        }
        
        public List<string> MULTILEVEL_SEARCH()
        {
            List<string> output = new List<string>();
            List<string> searchparams = new List<string>();
            int inputLen = Main.InputData.Count;
            string[] listofElem = new String[inputLen-2];
            int totalSearchParams = inputLen-2;
            bool found = false;
            int count;
            string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
            string strFolder = System.IO.Path.GetFullPath(Constants.projectDir);
            string localSearchDirPath = Main.InputData[0];
            string localDirPath = "";
            int len = listofElem.Length;
            string fileName = Main.InputData[1];
            StringBuilder strBuilder = new StringBuilder();
            if(localSearchDirPath.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                string localdirpath = Settings.getInstance().get("LOCAL_PATH_DIR");
                localDirPath = strFolder+"\\"+localdirpath +"\\"+strEnvName;
            }
            else{
                if(localSearchDirPath.StartsWith("C")|| localSearchDirPath.StartsWith("D")|| localSearchDirPath.StartsWith("E")){
                    localDirPath = localSearchDirPath;
                }
                else{
                    localDirPath = strFolder+"\\"+localSearchDirPath;
                }
            }
            string[] allFiles = Directory.GetFiles(localDirPath, fileName+"*").OrderByDescending(File.GetLastWriteTime).ToArray();
            for(int i=2;i<inputLen;i++){
                searchparams.Add(Main.InputData[i]);
            }
            foreach (string file in allFiles)
            {
                var inputFile = File.ReadAllLines(@""+file);
                int j=0;
                count=0;
                foreach (var line in inputFile){
                    if(j <totalSearchParams && line.Contains(searchparams[j])){
                        strBuilder.AppendLine(line);
                        found = true;
                        count++;
                        j++;
                    }
                }
                if(count==totalSearchParams){
                    break;
                }
                else{
                    strBuilder.Clear();
                }
            }
            if(found){
                Report.Success("all elements are matched");
                output.Add(Constants.TS_STATUS_PASS);
                output.Add(fileName);
                string[] resultLines = strBuilder.ToString().Split(new char[] {'\n', '\r'});
                List<string> y = resultLines.ToList<string>();
                y.RemoveAll(p => string.IsNullOrEmpty(p));
                resultLines = y.ToArray();
                for (int i = 0; i < resultLines.Length; i++)
                {
                    output.Add(resultLines[i]);
                }
            }
            else{
                output.Add(Constants.TS_STATUS_FAIL);
                Report.Failure("One or More elements are not Matched");
            }
            return output;
        }
        
        public List<string> SEARCH_TEXT_NOT_EXIST_IN_FILES()
        {
            string localDirPath="";
            string fileMask="";
            string firstOccurrence="";
            string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
            string strFolder = System.IO.Path.GetFullPath(Constants.projectDir);
            int inputLen = Main.InputData.Count;
            string searchValue = Main.InputData[0];
            string searchDirPath = Main.InputData[1];
            if(inputLen>2){
                if(!String.IsNullOrEmpty(Main.InputData[2])){
                    fileMask= Main.InputData[2];
                }
            }
            else{
                fileMask="";
            }
            if(searchDirPath.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                string localdirpath = Settings.getInstance().get("LOCAL_PATH_DIR");
                localDirPath = strFolder+"\\"+localdirpath +"\\"+strEnvName;
            }
            else{
                if(searchDirPath.StartsWith("C")|| searchDirPath.StartsWith("D")|| searchDirPath.StartsWith("E")){
                    localDirPath = searchDirPath;
                }
                else{
                    searchDirPath=searchDirPath.Replace("ng_mss_automation\\","");
                    localDirPath = strFolder+"\\"+searchDirPath;
                }
            }
            try{
                string[] allFiles = Directory.GetFiles(localDirPath, fileMask+"*");
                foreach (string file in allFiles)
                {
                    string[] lines = File.ReadAllLines(file);
                    firstOccurrence = lines.FirstOrDefault(l => l.Contains(searchValue));
                    if (firstOccurrence == null)
                    {
                        Report.Info("Search text does not exists in files");
                        Main.OutputData.Add(Constants.TS_STATUS_PASS);
                        break;
                    }
                    else{
                        Report.Error("Search text exists in files");
                        Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                    }
                }
            }
            catch(Exception e){
                Report.Error("SEARCH_TEXT_FILE keyword failed"+e.Message);
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
            }
            return Main.OutputData;
        }
        
        public List<string> COMPARE()
        {
            List<string> output = new List<string>();
            string input1 = Main.InputData[0];
            string input2 = Main.InputData[1];
            string result;
            try {
                if(input1.Contains(".") && input2.Contains(".")){
                    decimal num1 = Convert.ToDecimal(input1);
                    decimal num2 = Convert.ToDecimal(input2);
                    Decimal value = Decimal.Compare(num1,num2);
                    if(value==1){
                        output.Add(Constants.TS_STATUS_PASS);
                        result = "1";
                        output.Add(result);
                    }
                    if(value==-1){
                        output.Add(Constants.TS_STATUS_PASS);
                        result = "-1";
                        output.Add(result);
                    }
                    if(value==0){
                        output.Add(Constants.TS_STATUS_PASS);
                        result = "0";
                        output.Add(result);
                    }
                }
                else{
                    int num1 = Int32.Parse(input1);
                    int num2 = Int32.Parse(input2);
                    if (num1 < num2) {
                        output.Add(Constants.TS_STATUS_PASS);
                        result = "-1";
                        output.Add(result);
                    }
                    else {
                        if (num1 > num2) {
                            output.Add(Constants.TS_STATUS_PASS);
                            result = "1";
                            output.Add(result);
                        }
                        else {
                            output.Add(Constants.TS_STATUS_PASS);
                            result = "0";
                            output.Add(result);
                        }
                    }
                }
            }
            catch {
                try {
                    System.DateTime date1 = System.DateTime.Parse(input1);
                    System.DateTime date2 = System.DateTime.Parse(input2);
                    result = System.DateTime.Compare(date1, date2).ToString();
                    output.Add(Constants.TS_STATUS_PASS);
                    output.Add(result);
                }
                catch (FormatException e) {
                    output.Add(Constants.TS_STATUS_FAIL);
                    output.Add("Unable to compare the inputs. "+e.Message);
                }
            }
            return output;
        }
        
        public List<string> ADD_DATE()
        {
            List<string> output = new List<string>();
            string input1 = Main.InputData[0];
            string input2 = Main.InputData[1];
            string[] toAdd = input2.Split('+');
            
            try {
                System.DateTime date = System.DateTime.Parse(input1);
                foreach (string item in toAdd)
                {
                    if (item.Contains("D"))
                    {
                        string[] NumberOfDays = item.Split('D');
                        date = date.AddDays(Int32.Parse(NumberOfDays[0]));
                    }
                    else if (item.Contains("M"))
                    {
                        string[] NumberOfMonths = item.Split('M');
                        date = date.AddMonths(Int32.Parse(NumberOfMonths[0]));
                    }
                    else if (item.Contains("Y"))
                    {
                        string[] NumberOfYears = item.Split('Y');
                        date = date.AddYears(Int32.Parse(NumberOfYears[0]));
                    }
                    else
                    {
                        Report.Error("Invalid input");
                        output.Add(Constants.TS_STATUS_FAIL);
                        return output;
                    }
                }
                string long_date = date.ToLongDateString();
                System.DateTime newdate = System.DateTime.Parse(long_date,CultureInfo.InvariantCulture);
                string result = newdate.ToString("dd/MMM/yyyy").ToUpper();
                output.Add(Constants.TS_STATUS_PASS);
                output.Add(result);
            }

            catch (FormatException e) {
                Report.Error("Unable to add the inputs. "+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            return output;
        }
        
        public List<string> SUBSTRING_BY_DELIMITER(){
            List<string> output = new List<string>();
            
            string parentString = Main.InputData[0];
            //string[] delimiter = new string[] {Main.InputData[1]};
            string index = Main.InputData[2];
            
            try{
                //string[] splittedString = parentString.Split(delimiter, StringSplitOptions.None);
                string[] splittedString = Regex.Split(parentString, Main.InputData[1]);
                if (splittedString.Length >= Int32.Parse(index)) {
                    output.Add(Constants.TS_STATUS_PASS);
                    output.Add(splittedString[Int32.Parse(index)]);
                }
                else {
                    Report.Error("Index out of range");
                    output.Add(Constants.TS_STATUS_FAIL);
                    return output;
                }
            } catch (Exception e) {
                Report.Error("SUBSTRING_BY_DELIMITER Keyword Failed."+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            
            return output;
        }
        
        public List<string> ADD()
        {
            List<string> output = new List<string>();
            string input1 = Main.InputData[0];
            string input2 = Main.InputData[1];
            try {
                if (input1.Contains(".") || input2.Contains(".")) {
                    decimal num1 = Convert.ToDecimal(input1.Replace(",",""));
                    decimal num2 = Convert.ToDecimal(input2.Replace(",",""));
                    decimal result = Convert.ToDecimal(num1+num2);
                    output.Add(Constants.TS_STATUS_PASS);
                    output.Add(result.ToString());
                }
                else{
                    int num1 = Int32.Parse(input1);
                    int num2 = Int32.Parse(input2);
                    int result = num1+num2;
                    output.Add(Constants.TS_STATUS_PASS);
                    output.Add(result.ToString());
                }
            }
            catch (FormatException e) {
                Report.Error("Unable to add the inputs. "+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            return output;
        }
        
        public List<string> SELECT_QUERY(){
            List<string> output = new List<string>();
            string sql = Main.InputData[0];
            string tableName = "";
            if(sql.StartsWith("Select",StringComparison.OrdinalIgnoreCase)){
                var groups = Regex.Match(sql,".*(from|FROM|From)\\s+(\\w+)").Groups;
                tableName = groups[2].Value;
                string tableNameInConfig=Settings.getInstance().get("ALLOWED_TABLES_TO_BE_USED_IN_SELECT_KEYWORD");
                string[] allTables=tableNameInConfig.Split('#');
                if(allTables.Contains(tableName,StringComparer.OrdinalIgnoreCase)){
                    List<string[]> data=oraUtility.executeQuery(sql);
                    output.Add(Constants.TS_STATUS_PASS);
                    int x;
                    for(x=0;x<data[0].Length;x++){
                        output.Add(data[0][x]);
                    }
                }else{
                    output.Add(Constants.TS_STATUS_FAIL);
                    output.Add("Table name > "+tableName+" entered doesnot contains in global config file ");
                    Report.Error("Table name > "+tableName+" entered doesnot contains in global config file");
                }
            }else{
                output.Add(Constants.TS_STATUS_FAIL);
                Report.Error("Only Select query can be executed. Any other DML operations (Alter, Update, Delete) cannot be done using this keyword");
                output.Add("Only Select query can be executed. Any other DML operations (Alter, Update, Delete) cannot be done using this keyword");
            }
            return output;
        }
        
        public List<string> ENVIRONMENT(){
            List<string> output = new List<string>();
            string key = Main.InputData[0];
            string value=string.Empty;
            if(! string.IsNullOrEmpty(key))
            {
                try {
                    value=Environment.GetEnvironmentVariable(key);
                    output.Add(Constants.TS_STATUS_PASS);
                    output.Add(value);
                } catch (Exception e) {
                    output.Add(Constants.TS_STATUS_FAIL);
                    Report.Error(e.Message);
                }
            }
            return output;
        }
        
        
        public List<string> CONFIGURATION(){
            List<string> output = new List<string>();
            string key = Main.InputData[0];
            string value=string.Empty;
            if(! string.IsNullOrEmpty(key))
            {
                try {
                    value=Settings.getInstance().get(key);
                    output.Add(Constants.TS_STATUS_PASS);
                    output.Add(value);
                } catch (Exception e) {
                    output.Add(Constants.TS_STATUS_FAIL);
                    Report.Error(e.Message);
                }
            }
            return output;
        }
        
        public List<string> SEARCH_DOC_BY_STR(){
            List<string> output = new List<string>();
            string docPath = Main.InputData[0];
            string key = Main.InputData[1];
            string value=string.Empty;
            
            
            string  basePath = docPath.Substring(0,docPath.LastIndexOf('\\'));
            string  filemask = docPath.Substring(docPath.LastIndexOf('\\')+1,docPath.Length-basePath.Length-1);
            
            if( !string.IsNullOrEmpty(key) && Directory.Exists(basePath))
            {
                string textContent = string.Empty;
                try {
                    string [] filenames;
                    filenames = Directory.GetFiles(@basePath, filemask, SearchOption.AllDirectories).OrderByDescending(File.GetLastWriteTime).ToArray();
                    Report.Info("File matched="+String.Join("#",filenames));
                    textContent=utility.getDocsContent(filenames[0]);
                    output.Add(Constants.TS_STATUS_PASS);
                    string []splitContent=textContent.Split(new string[]{key},StringSplitOptions.None);
                    int count = splitContent.Length-1;
                    output.Add(filenames[0]);
                    output.Add(""+count);
                } catch (Exception e) {
                    output.Add(Constants.TS_STATUS_FAIL);
                    output.Add(e.Message);
                    Report.Error(e.Message);
                }
            }else
            {
                output.Add(Constants.TS_STATUS_FAIL);
                output.Add("Somthing wrong-Key:"+key+",basePath="+basePath+",filemask="+filemask);
                Report.Error("Somthing wrong-Key:"+key+",basePath="+basePath+",filemask="+filemask);
            }
            
            return output;
            
        }
        
        public List<string> SEARCH_XML_BY_XPATH(){
            List<string> output = new List<string>();
            string docPath = Main.InputData[0];
            string key = Main.InputData[1];
            string value=string.Empty;
            
            string xmlNamespaces=string.Empty;
            
            if(Main.InputData.Count>=3){
                xmlNamespaces=Main.InputData[2];
            }
            
            string  basePath = docPath.Substring(0,docPath.LastIndexOf('\\'));
            string  filemask = docPath.Substring(docPath.LastIndexOf('\\')+1,docPath.Length-basePath.Length-1);
            
            if( !string.IsNullOrEmpty(key) && Directory.Exists(basePath))
            {
                string textContent = string.Empty;
                try {
                    string [] filenames;
                    filenames = Directory.GetFiles(@basePath, filemask, SearchOption.AllDirectories).OrderByDescending(File.GetLastWriteTime).ToArray();
                    Report.Info("File matched="+String.Join("#",filenames));
                    
                    XmlDocument xmlDoc = new XmlDocument();
                    
                    xmlDoc.Load(filenames[0]);
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                    if(!string.IsNullOrWhiteSpace(xmlNamespaces))
                    {
                        String []namespaces=xmlNamespaces.Split('|');
                        string []xmlns;
                        foreach(string ns in namespaces)
                        {
                            xmlns=ns.Split('=');
                            if(xmlns.Length != 2)
                            {
                               Report.Warn("Namespace supplied has no valid pair."+ns);
                            }
                                
                            nsmgr.AddNamespace(xmlns[0],xmlns[1]);
                        }
                    }
                    XmlNode titleNode = xmlDoc.SelectSingleNode(key,nsmgr);
                    if( titleNode!=null )
                    {
                        textContent=titleNode.InnerXml;
                        output.Add(Constants.TS_STATUS_PASS);
                        output.Add(filenames[0]);
                        output.Add(textContent);
                    }else{
                        output.Add(Constants.TS_STATUS_FAIL);
                        output.Add("NOT_FOUND");
                    }
                } catch (Exception e) {
                    output.Add(Constants.TS_STATUS_FAIL);
                    output.Add(e.Message);
                    Report.Error(e.Message);
                }
            }else
            {
                output.Add(Constants.TS_STATUS_FAIL);
                output.Add("Something wrong-Key:"+key+",basePath="+basePath+",filemask="+filemask);
                Report.Error("Something wrong-Key:"+key+",basePath="+basePath+",filemask="+filemask);
            }
            
            return output;
            
        }
        
        public List<string> INSERT_UPDATE_QUERY(){
            List<string> output = new List<string>();
            string sql = Main.InputData[0];
            string tableName = "";
            if(sql.StartsWith("Insert",StringComparison.OrdinalIgnoreCase) || sql.StartsWith("Update",StringComparison.OrdinalIgnoreCase)){
                var groups = Regex.Match(sql,".*(update|UPDATE|Update|into|INTO|Into)\\s+(\\w+)").Groups;
                tableName = groups[2].Value;
                string tableNameInConfig=Settings.getInstance().get("ALLOWED_TABLES_TO_BE_USED_IN_INSERT_UPDATE_KEYWORD");
                string[] allTables=tableNameInConfig.Split('#');
                if(allTables.Contains(tableName,StringComparer.OrdinalIgnoreCase)){
                    //Check in case of update - it is impacting only 1 single record. Perform select with the same where clause.
                    if(sql.StartsWith("Update",StringComparison.OrdinalIgnoreCase))
                    {
                        var whereGroups = Regex.Match(sql,".*(WHERE|where|Where)\\s+(.*)").Groups;
                        var tableGroups = Regex.Match(sql,".*(Update|UPDATE|update)\\s+(\\w+)").Groups;
                        string selectQuery =  "Select count(*) from "+tableGroups[2] +" "+whereGroups[1]+" "+ whereGroups[2];
                        List<string[]> recordCount=oraUtility.executeQuery(selectQuery);
                        string count=recordCount[0][0];
                        Report.Info(selectQuery+" has "+ count+" records. Expected Record-Count=1");
                        if(! "1".Equals(count))
                        {
                            
                            output.Add(Constants.TS_STATUS_FAIL);
                            output.Add(selectQuery+" has "+ count+" records. Expected Record-Count=1");
                            return output;
                        }
                    }
                    oraUtility.executeQuery(sql);
                    output.Add(Constants.TS_STATUS_PASS);
                    
                }else{
                    output.Add(Constants.TS_STATUS_FAIL);
                    output.Add("Table name ["+tableName+"] entered does not contains in global config file.");
                    return output;
                }
            }else{
                output.Add(Constants.TS_STATUS_FAIL);
                output.Add("Only Update/Insert can be done using this keyword.");
                return output;
            }
            return output;
        }
        
        public List<string> SEARCH_PDF_BY_STR(){
            Boolean flag = false;
            try{
                List<string> output = new List<string>();
                string strEnvName = Settings.getInstance().get("SUMMIT_REMOTE_SID");
                string strFolder = System.IO.Path.GetFullPath(Constants.projectDir);
                string localSearchDirPath = Main.InputData[0];
                string localDirPath = "";
                string fileName = Main.InputData[1];
                string stringToCheck = Main.InputData[2];
                StringBuilder text = new StringBuilder();
                if(localSearchDirPath.Trim().Equals("default",StringComparison.OrdinalIgnoreCase)){
                    string localdirpath = Settings.getInstance().get("LOCAL_PATH_DIR");
                    localDirPath = strFolder+"\\"+localdirpath +"\\"+strEnvName;
                }
                if(localSearchDirPath.Trim().Equals("relative",StringComparison.OrdinalIgnoreCase)){
                    localDirPath = strFolder+"\\"+localSearchDirPath;
                }
                else{
                    if(localSearchDirPath.StartsWith("C")|| localSearchDirPath.StartsWith("D")|| localSearchDirPath.StartsWith("E")){
                        localDirPath = localSearchDirPath;
                    }
                    else{
                        localDirPath = localSearchDirPath;
                        using (PdfReader reader = new PdfReader(localDirPath))
                        {
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                            }
                        }
                        //Report.Info("Entire text is " + text);
                        if(text.ToString().Contains(stringToCheck)){
                            flag = true;
                            Main.OutputData.Add(Constants.TS_STATUS_PASS);
                            Report.Info("Text matched in PDF");
                        }
                        else{
                            Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                            Report.Info("Text didnot matched in PDF");
                        }
                    }
                }
                if(!localDirPath.Contains("http") || localDirPath.Contains("https")){
                    
                    string[] allFiles = Directory.GetFiles(localDirPath, fileName+"*");
                    foreach (string file in allFiles)
                    {
                        using (PdfReader reader = new PdfReader(file))
                        {
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                            }
                        }
                        //Report.Info("Entire text is " + text);
                        if(text.ToString().Contains(stringToCheck)){
                            Report.Info("Searched text found in file -> " + file);
                            Main.OutputData.Add(Constants.TS_STATUS_PASS);
                            Main.OutputData.Add(file);
                            Main.OutputData.Add(file.Split('\\').Last());
                            flag = true;
                            break;
                        }
                        else{
                            continue;
                        }
                    }
                    if(flag == true){
                        Report.Info("Text matched in PDF");
                    }
                    else{
                        Main.OutputData.Add(Constants.TS_STATUS_FAIL);
                        Report.Info("Text didnot matched in PDF");
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e.StackTrace);
                Main.OutputData.Add(Constants.TS_STATUS_FAIL);
            }
            return Main.OutputData;
        }
        
        
        public List<string> COPY_FILES_FROM_REMOTE()
        {
            List<string> output = new List<string>();
            string profile = Main.InputData[0];
            string remoteDirectory = Main.InputData[1];
            string fileMask = Main.InputData[2];
            
            
            string userName=String.Empty;
            string pass=String.Empty;
            string hostname=String.Empty;
            string remoteBaseDirectory=String.Empty;
            string envName=String.Empty;
            string localTargetDir=String.Empty;
            try {
                if(!String.IsNullOrEmpty(profile))
                {
                    profile=profile.Trim();
                    string key_prefix="SERVER_"+profile+"_";
                    userName=Settings.getInstance().get(key_prefix+"USERNAME");
                    pass=Settings.getInstance().get(key_prefix+"PASS");
                    hostname=Settings.getInstance().get(key_prefix+"HOSTNAME");
                    remoteBaseDirectory=Settings.getInstance().get(key_prefix+"REMOTE_BASE_DIR");
                    
                    
                    
                    try {
                        envName=Settings.getInstance().get(key_prefix+"ENV_NAME");
                    } catch (Exception) {
                        throw;
                    }
                    string localdirpath = Settings.getInstance().get("LOCAL_PATH_DIR");
                    localTargetDir = Constants.projectDir+"\\"+localdirpath;
                    if(!Directory.Exists(localTargetDir))
                    {
                        Directory.CreateDirectory(localTargetDir);
                    }
                    
                }
                //Actual directory will be BASE_DIR_config+ENV_config+remoteDir_input
                Process p = Process.Start(Constants.Path_TransferBatFile , remoteDirectory+" "+localTargetDir+" "+fileMask+" "+hostname+" "+envName+" "+userName+" "+pass+" "+remoteBaseDirectory);
                
                p.WaitForExit(Constants.FileTransfer_Timeout);
                
                output.Add(Constants.TS_STATUS_PASS);
                output.Add(localTargetDir);
            }
            catch (Exception e) {
                Report.Error("Exception in file copying. "+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
                output.Add(e.Message);
            }
            
            return output;
        }
        
        public List<string> SET_APPLICATION(){
            List<string> output = new List<string>();
            
            string appFlagValue = Main.InputData[0];
            
            try{
                if(appFlagValue.ToUpper().Equals(Constants.appSummit)){
                    Main.appFlag = Constants.appSummit;
                }else if(appFlagValue.ToUpper().Equals(Constants.appActivate)){
                    Main.appFlag = Constants.appActivate;
                    RawTextFlavor.Instance.ProcessNames.Add(new Regex("sam"));
                }
                output.Add(Constants.TS_STATUS_PASS);
                
            } catch (Exception e) {
                Report.Error("SET_APPLICATION Keyword Failed."+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            
            return output;
            
        }
    }
}
