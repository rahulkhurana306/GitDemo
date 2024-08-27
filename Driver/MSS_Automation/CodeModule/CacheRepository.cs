/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 16/06/2022
 * Time: 16:32
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Cache repository helps to load ObjectRepository quick and save time.
    /// </summary>
    [UserCodeCollection]
    public class CacheRepository
    {
        const int VERSION = 1;
        private static string fileName=Path.Combine(Constants.projectDir,"Repository","CacheRepository.bin");//Specified cache file path from Settings.
        
        public static void Save(Dictionary<Object, Object> repository) {
            Stream stream = null;
            try {
                Report.Info("Cache Repository : Elements to Save = "+repository.Count);
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, VERSION);
                formatter.Serialize(stream, repository);
            } catch {
                // do nothing, just ignore any possible errors
            } finally {
                if (null != stream)
                    stream.Close();
            }
        }
        
        public static Dictionary<Object, Object> Load() {
            Stream stream = null;
            Dictionary<Object, Object> repository = null;
            try {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                int version = (int)formatter.Deserialize(stream);
                Debug.Assert(version == VERSION);
                repository = (Dictionary<Object, Object>)formatter.Deserialize(stream);
                Report.Info("CacheRepository: Elements Loaded - "+repository.Count);
            } catch {
                // do nothing, just ignore any possible errors
            } finally {
                if (null != stream)
                    stream.Close();
            }
            return repository;
        }
    }
}
