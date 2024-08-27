/*
 * Created by Ranorex
 * User: PROJ-MSS-ATEST03.SVC
 * Date: 08/06/2022
 * Time: 15:58
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Data.OleDb;
using Ranorex;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of DBUtility.
    /// </summary>
    public class DBUtility
    {
        public OleDbConnection connectionMSAccess = null;
        public OleDbDataReader SQLReader = null;
        private string ProjStage = String.Empty;
        public DBUtility()
        {
            connectionMSAccess = ConnectToMSACCESS();
            ProjStage = Settings.getInstance().get("PROJ_STAGE");
        }
        
        private OleDbConnection ConnectToMSACCESS(){
            OleDbConnection connection = null;
            try{
                string MdbPath = Constants.Path_MSAccess;
                string ConString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+MdbPath+"";
                connection = new OleDbConnection(ConString);
                connection.Open();
            }catch(Exception e){
                throw new Exception("Error locating MS-Access test-data repository"+e.Message);
            }
            return connection;
        }
        
        public Boolean ReadDBResultMS(String sqlQuery){
            return ReadDBResultMS(connectionMSAccess, sqlQuery);
        }
        
        private  Boolean ReadDBResultMS(OleDbConnection connection, String sqlQuery){
            Boolean recordFound = false;
            try{
                OleDbCommand command = new OleDbCommand(sqlQuery, connection);
                SQLReader = command.ExecuteReader();
                recordFound=SQLReader.Read();
                if(!recordFound)
                {
                    throw new Exception("Record not found in MS-Access : "+sqlQuery);
                }
                
            }catch(Exception e){
                Report.Error("Exception while executing MS-Access query : "+e.Message);
                throw e;
            }
            
            return recordFound;
        }
        
        public string GetAccessFieldValue(string FieldName){
            try{
                int fieldId = SQLReader.GetOrdinal(FieldName);
                String val = SQLReader.GetValue(fieldId).ToString();
                if (Constants.PROJ_STAGE_DEV == ProjStage) 
                {
                    Report.Info("MS-Access-field-info FieldName="+FieldName+", Value="+val);
                }
                return val;
            }catch(Exception e){
                Report.Error("Exception while getting field: "+e.Message);
                throw e;
            }
        }
    }
}
