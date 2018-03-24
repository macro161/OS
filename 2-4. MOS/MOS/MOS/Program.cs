﻿using System;
using System.Threading;
using System.Windows.Forms;
using MOS.RealMachine;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace MOS
{
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            log.Info("Bla");
            var logging = new LoggerTextBox();
            //Application.Run(logging);
            Thread BackgroundThread = new Thread(()=> Application.Run(logging));
            BackgroundThread.Start();
            log.Info("_log info");
            //logging.Update();
            //Application.Run(new Form1());
        }
    }
}
