/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 14/06/2022
 * Time: 16:36
 * 
 * Global settings and overloaded user-specific settings for test-scripts/plans.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using ng_mss_automation.CodeModule;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Data;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Creates a Ranorex user code collection. A collection is used to publish user code methods to the user code library.
    /// </summary>
    [UserCodeCollection]
    public sealed  class Settings
    {
        
        private static readonly Settings instance = new Settings();
        
        private Dictionary <string,string> data = new Dictionary<string, string>();
        
        private Settings(){
            CsvUtility globalSettings= new CsvUtility(Path.Combine(ng_mss_automation.CodeModule.Constants.projectDir,"Config","Global.csv"));
            Report.Info("Global config loaded :"+globalSettings.getLineCount());
            string envKey,envValue;
            foreach(string[] csvData in globalSettings.getAllData())
            {
                if(csvData.Length>1)
                {
                    envKey=csvData[0].Trim();
                    envValue=csvData[1].Trim();
                    data[envKey]=AutomationSecurityUtil.Decrypt(envValue);//data[Key]=Value
                }
            }
            
            string userConfigPath=Path.Combine(ng_mss_automation.CodeModule.Constants.projectDir,"Config",Environment.UserName+".csv");
            if(File.Exists(userConfigPath))
            {
                CsvUtility userSettings= new CsvUtility(userConfigPath);
                Report.Info("User config loaded :"+userSettings.getLineCount());
                foreach(string[] userEnv in userSettings.getAllData())
                {
                    if(userEnv.Length>1){
                        envKey=userEnv[0].Trim();
                        envValue=userEnv[1].Trim();
                        data[envKey]=AutomationSecurityUtil.Decrypt(envValue);//data[Key]=Value
                    }
                }
            }
            try {
                Report.Info("Basic Environment Detail :User ["+Environment.UserName+"], Host ["+Environment.MachineName+"], Environment ["+get("SUMMIT_DB_HOST_SERVICE")+"]");
            } catch (Exception) {
            }
            
        }
        
        public static Settings getInstance(){
            return instance;
        }
        
        
        public string get(string key)
        {
            string value = String.Empty;
            try{
                value=data[key];
            }catch(Exception e){
                Report.Warn("Missing key ["+key+"] in config files.");
                throw e;
            }
            return value;
        }
        
        
        public void writeAllSettings()
        {
            string reportName = Path.Combine(Constants.Path_Report,"Config_Runtime.csv");;
            try {
                
                List<string> envEnteries = new List<string>();
                foreach(var item in data)
                {
                    envEnteries.Add(item.Key+","+item.Value);
                }
                
                File.AppendAllLines(reportName,envEnteries);
            } catch (Exception) {
            }
        }
    }
}
