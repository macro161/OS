using System;
using System.Threading;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;

namespace MOS.RealMachine
{
    public partial class LoggerTextBox : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        System.IO.StreamReader file;
        private readonly object _lockObj = new object();
        public BindingSource lg = new BindingSource();
        public LoggerTextBox()
        {
            InitializeComponent();
            TextBoxAppender.ConfigureTextBoxAppender(this,textBox);
            //lg.DataSource = log;
            //+= (sender, args) => { ) { HandleChanged(); } };
            //System.IO.StreamReader file = new System.IO.StreamReader("log.txt");
        }

        private void LoggerTextBox_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();
        }
        
        void Updating()
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
            }
            textBox.BeginInvoke((MethodInvoker)delegate
            {
                //file.s
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    textBox.AppendText(line);
                }
            });
            Thread.Sleep(1000);
            Updating();
        }
        
    }
}
