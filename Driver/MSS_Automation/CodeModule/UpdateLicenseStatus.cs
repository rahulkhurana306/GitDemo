/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 30/06/2022
 * Time: 10:54
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of UpdateLicenseStatus.
    /// </summary>
    public partial class Keywords
    {
        public List<string> UPDATE_LICENSE_STATUS()
        {
            List<string> output=new List<string>();
            string licenseName = Main.InputData[0];
            string licStatus = Main.InputData[1];
            Report.Info("Update License["+licenseName+"] with New Status["+licStatus+"]");
            
            string licStatusQuery = "SELECT LICA_REFERENCE,LICA_STATUS from licensed_applications where LICA_REFERENCE='"+licenseName.Trim()+"'";
            
            string updateLicenseStatusQuery = "UPDATE licensed_applications set " +
                "LICA_STATUS='"+licStatus.Trim()+"'"+
                " where LICA_REFERENCE='"+licenseName.Trim()+"'";
            string defaultUser=Settings.getInstance().get("SUMMIT_ENV_USER");
            
            string insertLicenseQuery="Insert into LICENSED_APPLICATIONS (LICA_LAST_CHANGE_BY,LICA_LAST_CHANGE_DATE,LICA_QUADRA_NO,LICA_REFERENCE,LICA_STATUS) " +
                " values ('"+defaultUser+"',sysdate,'T005','"+licenseName.Trim()+"','"+licStatus.Trim()+"')";
            
            List<string[]> data=oraUtility.executeQuery(licStatusQuery);
            string oldStatus=String.Empty;
            if(data.Count>0)
            {
                oldStatus=data[0][1];
                oraUtility.executeQuery(updateLicenseStatusQuery);
            }
            else
            {
                oraUtility.executeQuery(insertLicenseQuery);
                oldStatus="I";
            }
            
            
            oraUtility.executeQuery("commit");
            
            output.Add(Constants.TS_STATUS_PASS);
            output.Add(oldStatus);
            return output;
            
        }
        
    }
}
