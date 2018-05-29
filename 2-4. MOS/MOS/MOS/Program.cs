﻿using log4net;
using log4net.Appender;
using MOS.Enums;
using MOS.OS;
using MOS.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace MOS
{
    static class Program
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Info("****************************************************************************");
            Kernel kernel = new Kernel();
            RealMachine.RealMachine realMachine = new RealMachine.RealMachine(kernel);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Thread(() =>
            {
                realMachine.Kernel.ready.Add(new StartStop(kernel, null, 100, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>()));
                kernel.Planner();
            }).Start();
            new Thread(() =>
            {
                Application.Run(new GUI.SystemForm(kernel));
            }).Start();

            //new Thread(() =>
            //{
            //    Application.Run(new RealMachine.LoggerTextBox());
            //}).Start();
        }

        public static GUI.VMForm RunVM(JobGovernor jg)
        {
            GUI.VMForm VmForm = new GUI.VMForm(jg);
            new Thread(() =>
            {
                Application.Run(new GUI.VMForm(jg));
            }).Start();
            return VmForm;
        }
    }
}
