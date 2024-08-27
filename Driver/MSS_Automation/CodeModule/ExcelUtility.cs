using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    public class ExcelUtility
    {
        public static Excel.Application ExcelApp;
        public static Excel.Workbook ExcelWBook;
        private static Excel.Worksheet ExcelWSheet;
        
        public ExcelUtility()
        {
        }
        
        public static void SetExcelFile(String path)
        {
            try
            {
                ExcelApp = new Excel.Application();
                ExcelApp.Visible = false;
                ExcelWBook = ExcelApp.Workbooks.Open(path,Missing.Value, true, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                                     Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            
            catch (Exception e)
            {
                Report.Error("ExcelUtils-SetExcelFile | Exception: " + e.Message);
                throw e;
            }
        }
        
        public static void CloseExcelFile()
        {
            try
            {
                if(ExcelWBook !=null ){
                    ExcelWBook.Close(false);
                    ExcelApp.Quit();
                }
            }
            catch (Exception e)
            {
                Report.Error("ExcelUtils-SetExcelFile | Exception: " + e.Message);
            }
        }
        
        
        public static string GetCellData(int rowNum, int colNum, String sheetName)
        {
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                string cellValue = (ExcelWSheet.Cells[rowNum + 1, colNum + 1] as Excel.Range).Text as string;
                return cellValue;
            }
            catch (Exception e)
            {
                
                Report.Info("ExcelUtils-GetCellData | Exception: " + e.Message);
                return "";
            }
        }
        
        
        public static int GetRowCount(String sheetName)
        {
            int number = 0;
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                number = ExcelWSheet.UsedRange.Rows.Count+1;
            }
            catch (Exception e)
            {
                Report.Info("ExcelUtils-GetRowCount | Exception: " + e.Message);
            }
            return number;
        }
        
        
        
        public static int GetRowContains(String testCaseName, int colNum, String sheetName)
        {
            int rowNum = 0;
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                int rowCount = GetRowCount(sheetName);

                for (; rowNum < rowCount; rowNum++)
                {
                    if (GetCellData(rowNum, colNum, sheetName).Equals(testCaseName))
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Report.Info("ExcelUtils-GetRowContains | Exception: " + e.Message);
            }
            return rowNum;
        }
        
        
        
        public static int GetTestStepsCount(String sheetName, String testCaseID, int testCaseStart)
        {
            try
            {
                for (int i = testCaseStart; i <= GetRowCount(sheetName); i++)
                {
                    if (!testCaseID.Equals(GetCellData(i, Constants.Col_TestCaseID, sheetName)))
                    {
                        int number = i;
                        return number;
                    }
                }
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                int number1 = ExcelWSheet.UsedRange.Rows.Count + 1;
                return number1;
            }
            catch (Exception e)
            {
                Report.Info("ExcelUtils-GetRowContains | Exception: " + e.Message);
                return 0;
            }
        }
        
        
        
        public static void SetCellData(String Result, int rowNum, int colNum, String sheetName)
        {
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                (ExcelWSheet.Cells[rowNum + 1, colNum + 1] as Excel.Range).Value = Result;
            }
            catch (Exception e)
            {
                Report.Info("ExcelUtils-SetCellData | Exception: " + e.Message);
            }

        }
        
         public static bool SheetExists(String sheetName)
        {
            try
            {
                foreach (Excel.Worksheet sheet in ExcelWBook.Sheets) {
                    if(sheet.Name==sheetName){
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                
                Report.Info("ExcelUtils-SheetExists | Exception: " + e.Message);
                return false;
            }
        }
        
    }
}

