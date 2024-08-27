/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 29/06/2022
 * Time: 17:09
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Data.OleDb;
using System.Collections.Generic;
using Ranorex;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Description of OracleUtility.
    /// </summary>
    public class OracleUtility
    {
        
        private OleDbConnection connection;
        
        private static OracleUtility oracleUtility = new OracleUtility();
        
        public static OracleUtility Instance()
        {
            return oracleUtility;
        }
        
        
        
        private OracleUtility()
        {
            Settings setting = Settings.getInstance();
            string oradb = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)" +
                "(HOST = "+setting.get("SUMMIT_DB_HOST_NAME")+")(PORT = "+setting.get("SUMMIT_DB_HOST_PORT")+"))" +
                "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = "+setting.get("SUMMIT_DB_HOST_SERVICE")+")));" +
                "User Id= "+setting.get("SUMMIT_DB_HOST_USER")+";Password="+setting.get("SUMMIT_DB_HOST_PASS")+";";
            
            try
            {
                connection = new OleDbConnection(oradb);
                connection.Open();
            }
            catch (Exception e)
            {
                Report.Error("Unable to open connection with Summit-Oracle database:"+e.Message);
                throw e;
            }
            
        }
        
        public List<string[]> executeQuery(string sqlQuery)
        {
            OleDbCommand command = new OleDbCommand(sqlQuery, connection);
            OleDbDataReader reader=command.ExecuteReader();
            int fieldCount=reader.FieldCount;
            string[] row ;
            List<string[]> result = new List<string[]>();
            while(reader.Read())
            {
                row = new string[fieldCount];
                for(int i=0;i<fieldCount;i++)
                {
                    row[i]=""+reader.GetValue(i);
                }
                result.Add(row);
            }
            
            Report.Info("Summit-Oracle-Query["+sqlQuery+"] Result-Row-Count="+result.Count);
            return result;
            
        }
        
        public void CloseConnection(){
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                Report.Error("Unable to close connection with Summit-Oracle database:"+e.Message);
            }
            
            
        }
    }
}
