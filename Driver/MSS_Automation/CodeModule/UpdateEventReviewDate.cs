/*
 * Created by Ranorex
 * User: gaursingh
 * Date: 25/08/2022
 * Time: 1:24 PM
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using Ranorex;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of UpdateEventReviewDate.
    /// </summary>
    public partial class Keywords
    {
        public List<string> UPDATE_EVENT_REVIEW_DATE()
        {
            List<string> output=new List<string>();
            string society = Main.InputData[0];
            string eventCode = Main.InputData[1];
            string startDate;
            string reviewDate;
            try {
                OracleUtility oracleUtility = OracleUtility.Instance();
                startDate = utility.ProcessWCALDate(Main.InputData[2], society);
                reviewDate = utility.ProcessWCALDate(Main.InputData[3], society);
                
                Report.Info("Update EVENT ["+eventCode+"] with Start date ["+startDate+"] and Review date ["+reviewDate+"]");
                string updateReviewDateQuery = "UPDATE DIARIES set " +
                    "dia_start_date=TO_DATE('"+startDate+"', 'DD-MON-RRRR'), dia_review_date=TO_DATE('"+reviewDate+"', 'DD-MON-RRRR') where dia_events_code='"+eventCode+"'";
                
                oracleUtility.executeQuery(updateReviewDateQuery);
                oracleUtility.executeQuery("commit");
                output.Add(Constants.TS_STATUS_PASS);
            }
            catch (Exception e){
                Report.Error("Error while processing keyword UPDATE_EVENT_REVIEW_DATE."+e.Message);
                output.Add(Constants.TS_STATUS_FAIL);
            }
            return output;
        }
    }
}
