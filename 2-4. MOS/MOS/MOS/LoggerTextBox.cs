using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOS
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
