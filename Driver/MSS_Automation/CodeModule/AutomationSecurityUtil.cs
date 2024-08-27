/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 19/06/2023
 * Time: 14:50
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    public static class AutomationSecurityUtil
    {
        private static byte[] key = new byte[8] {1, 3, 5, 7, 2, 4, 6, 8};
        private static byte[] iv = new byte[8]  {5, 6, 7, 8, 1, 2, 3, 4};
        
        public static string Crypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            string encryptedText = Convert.ToBase64String(outputBuffer);
            Report.Info("EnCrypted String for ["+text+"] is ["+encryptedText+"]");
            return encryptedText;
        }

        public static string Decrypt(string text)
        {
            Regex re = new Regex("ENC\\{(.*)\\}");
            Match m = re.Match(text);
            if(m.Success){
                text = m.Groups[1].Value;
                SymmetricAlgorithm algorithm = DES.Create();
                ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
                byte[] inputbuffer = Convert.FromBase64String(text);
                byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
                return Encoding.Unicode.GetString(outputBuffer);
            }else{                
                return text;
            }
        }
    }
}

