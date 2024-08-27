using System;
using System.IO;

namespace ng_mss_automation.CodeModule
{
    
    public class Constants
    {
        public static readonly string assemblyDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string tempPath = Path.GetDirectoryName(Path.GetDirectoryName(assemblyDir));
        public static readonly string projectDir = Path.GetFullPath(Path.Combine(tempPath, @"..\..\"));
        public static readonly string Path_ORData = Path.Combine(projectDir, "Repository","Keywords_And_ObjectRepository.xlsx");
        public static readonly string Path_Report= Path.Combine(projectDir, "Results");
        public static readonly string Path_MSAccess = Path.Combine(projectDir, "Repository","TestData.mdb");
        public static readonly string Path_Summit = Path.Combine(projectDir, "ThirdParty\\fsal","frmsal.jar");
        public static readonly string Path_TransferBatFile = Path.Combine(projectDir, "ThirdParty\\batch", "transfer_files.bat");
        public static readonly string Path_TransferBatFile_LocalToRemote = Path.Combine(projectDir, "ThirdParty\\batch", "transfer_files_LocalToRemote.bat");
        public static readonly string Path_TransferBatFile_test = Path.Combine(projectDir, "ThirdParty\\batch", "delete_files_from_Remote.bat");

        public static readonly string Sheet_TestCases = "TestCaseSteps";
        public static readonly string Sheet_RevertTestCaseSystemSettings = "RevertTestCaseSystemSettings";
        public static readonly string Sheet_PreReqSteps = "PreReqSteps";
        public static readonly string Sheet_ORPropname = "RANOREX MAIN OR";
        public static readonly string RUN_MODE_PREQ="1";
        public static readonly string RUN_MODE_TEST="2";
        public static readonly string RUN_MODE_DEV="3";
        public static readonly string TC_GLOBAL_STATUS="TC_GLOBAL_STATUS";
        public static readonly string SUMMIT_REPORT_LEVEL="MSS-Report";
        
        public static readonly string FORCE_CLOSE_FLAG="FORCE_CLOSE_FLAG";
        
        public static readonly int Col_TestCaseID = 0;
        public static readonly int Col_TestStepID = 1;
        public static readonly int Col_TestStepDesc = 2;
        public static readonly int Col_TestScreenName = 3;
        public static readonly int Col_Logical_Object_Name = 4;
        public static readonly int Col_ActionKeyword = 5;
        public static readonly int Col_InputDataSet = 6;
        public static readonly int Col_OutputDataSet = 7;
        public static readonly int Col_Blocking = 8;
        public static readonly int Col_Remarks = 9;
        public static readonly int Col_StepStatus = 11;
        public static readonly int Col_StartTime =12;
        public static readonly int Col_Duration =13;
        public static readonly int Col_ReportInputData = 14;
        public static readonly int Col_ReportOutputData = 15;
        public static readonly int Col_ID = 0;
        
        public static readonly double Timeout = 15;
        public static readonly double NavigationTimeout = 200;
        
        public static readonly string TS_STATUS_PASS = "Pass";
        public static readonly string TS_STATUS_FAIL = "Fail";
        public static readonly string TS_STATUS_ABORT = "Abort";
        public static readonly string TS_STATUS_SKIP = "Skip";
        public static readonly string TestData_DEFAULT = "DEFAULT";
        public static readonly int FileTransfer_Timeout = 300000;
        public static readonly string PROJ_STAGE_DEV = "Development";

        
        public static readonly int defaultJobTimeOut=120;//120 seconds
        public static readonly int defaultCheckJobStatusRetryTime=30;//30 seconds
        public static readonly string defaultSequenceName="AUT_MSS_SEQ";
        
        public static readonly string appSummit="SUMMIT";
        public static readonly string appActivate="ACTIVATE";
        public static readonly string appAbort = "ABORT";
        public static readonly int defaultActivateTimeOut=80;
        public static readonly int defaultActivatePageTimeOut=30;
        
        public static readonly string Default_TestPlan="Default.csv";
        public static readonly string Skip_TestPlan="Skip_Plan_TC.csv";
        public static readonly string BEFORE_KEYWORD="before";
        public static readonly string AFTER_KEYWORD="after";
        public static readonly Boolean captureScreenshot=String.Equals(Settings.getInstance().get("CAPTURE_SCREENSHOTS").Trim(),"Y")?true:false;
    }
}
