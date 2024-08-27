/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 10/06/2022
 * Time: 19:54
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ng_mss_automation.CodeModule
{
    /// <summary>
    /// Creates a Ranorex user code collection. A collection is used to publish user code methods to the user code library.
    /// </summary>
    [UserCodeCollection]
    public class CsvUtility
    {
        private string[] lines;
        private string[][] cellData;
        
        private string[] linesSkipped;
        private string[][] cellDataSkipped;
        /**
         * Csv Utility with default separator.
         * */
        
        public CsvUtility(String csvPath):this(csvPath,',')
        {
            
        }
        
        public CsvUtility(String csvPath,char sep)
        {
            
            
            string []allLines = new string[1];
            try
            {
                allLines = System.IO.File.ReadAllLines(csvPath);
            }
            catch(IOException e)
            {
                Report.Error("IOException source: "+e.Source+" and error message: "+e.Message);
                throw e;
            }
            
            ArrayList validLines = new ArrayList();
            ArrayList skippedLines = new ArrayList();
            
            foreach(string line in allLines)
            {
                if(!line.StartsWith("#") && !String.IsNullOrEmpty(line))
                {
                    validLines.Add(line);
                }
                else{
                    skippedLines.Add(line);
                }
            }
            
            cellData=new string[validLines.Count][];
            lines = new string[validLines.Count];
            int i=0;
            foreach(string line in validLines.ToArray())
            {
                //                Report.Info("Line->"+line);
                lines[i]=line;
                string[] columns = line.Split(sep);
                cellData[i] = columns ;
                i++;
                
                
            }
            cellDataSkipped=new string[skippedLines.Count][];
            linesSkipped = new string[skippedLines.Count];
            int j=0;
            foreach(string line in skippedLines.ToArray())
            {
                linesSkipped[j]=line;
                string[] columns = line.Split(sep);
                cellDataSkipped[j] = columns ;
                j++;
            }
        }
        
        public string[][] getAllData(){
            return this.cellData;
        }
        
        public string[] getAllLines(){
            return this.lines;
        }
        
        public int getLineCount(){
            return lines.Length;
        }
        
         public string[][] getSkippedAllData(){
            return this.cellDataSkipped;
        }
        
        public string[] getSkippedAllLines(){
            return this.linesSkipped;
        }
        
        public int getSkippedLineCount(){
            return linesSkipped.Length;
        }
    }
    
    
}
