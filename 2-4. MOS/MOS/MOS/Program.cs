using log4net;
using log4net.Appender;
using System;
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
            RealMachine.RealMachine realMachine = new RealMachine.RealMachine();
            //realMachine.PowerOn();
            //Thread t = new Thread(realMachine.PowerOn);
            //t.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //    var file = new RealMachine.ChannelsDevice();
            //    file.ReadFromFlash();
            new Thread(() => {
            Application.Run(new GUI.RMform(realMachine));
        }).Start();
            

            Log.Info("So far only logs to bin/Debug/mylog");
            //var logging = new LoggerTextBox();
            //Application.Run(logging);
            //Thread BackgroundThread = new Thread(()=> Application.Run(logging));
            //BackgroundThread.Start();
            //log.Info("_log info");
            //logging.Update();
            //Application.Run(new Form1());
        }
    }
}
