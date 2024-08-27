/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 01/08/2022
 * Time: 08:43
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of ReportRowData.
    /// </summary>
    public class ReportRowData
    {
        public int rowIndex;
        public Boolean testStepStatus;
        public DateTime testStepStartTime;
        public string durationtotal;
        public string inputData;
        public string outputData;
        
        
        public ReportRowData(int r,Boolean status,DateTime startTime,string duration,string input,string output)
        {
            rowIndex = r;
            testStepStatus = status;
            testStepStartTime = startTime;
            durationtotal=duration;
            inputData = input;
            outputData = output;
        }
        
    }
}
