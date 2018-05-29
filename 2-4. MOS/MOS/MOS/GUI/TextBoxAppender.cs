using System.Windows.Forms;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace MOS.RealMachine
{
    public class TextBoxAppender : IAppender
    {
        private TextBox textBox;
        private readonly object _lockObj = new object();
        Form form;
        public string Name { get; set; }

        public TextBoxAppender(Form x, TextBox _textbox)
        {
            var frm = textBox.FindForm();
            if (frm == null)
                return;
            frm.FormClosing += delegate { Close(); };

            this.textBox = _textbox;
            form = x;
            Name = "TextBoxAppender";
        }


        public static void ConfigureTextBoxAppender(Form x, TextBox textBox)
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var appender = new TextBoxAppender(x,textBox);
            hierarchy.Root.AddAppender(appender);
        }

        public void Close()
        {
            lock (_lockObj)
            {
                textBox = null;
            }
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAppender(this);
        }


        public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
        {
            if (textBox == null)
                return;
            string msg = string.Concat(loggingEvent.RenderedMessage, "\r\n");

            lock (_lockObj)
            {
                if (!form.IsHandleCreated)
                {
                   // form.CreateHandle();
                }
                textBox.BeginInvoke((MethodInvoker)delegate
                {
                    textBox.AppendText(msg);
                });
            }
        }
    }
}
