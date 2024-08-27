/*
 * Created by Ranorex
 * User: magnihotri
 * Date: 19/06/2023
 * Time: 15:42
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
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
    /// Description of Test.
    /// </summary>
    [TestModule("D7D38C94-29C9-450A-B9B4-712FEE10AE9E", ModuleType.UserCode, 1)]
    public class Test : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Test()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            AutomationSecurityUtil.Crypt("$8awep?5Hpd$");
            AutomationSecurityUtil.Decrypt("$8awep?5Hpd$");
            Report.Info(AutomationSecurityUtil.Decrypt("ENC{SPMPQ/pKZfcxL+Zgw6c5iBsxdONOMHdHhjQLHuuwlF0=}"));
        }
    }
}
