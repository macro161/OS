using System;
using System.Windows.Forms;
using log4net.Config;

namespace MOS.RealMachine
{
    public partial class LoggerTextBox : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public LoggerTextBox()
        {
            InitializeComponent();
        }

        private void LoggerTextBox_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();
            TextBoxAppender.ConfigureTextBoxAppender(textBox);

            log.Info("This is just an example of how this appender works...");
        }
        
    }
}
