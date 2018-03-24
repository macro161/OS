using System.Windows.Forms;
using System.Windows.Forms;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace MOS.RealMachine
{
    public class TextBoxAppender : IAppender
    {
        private TextBox _textBox;
        private readonly object _lockObj = new object();
        public string Name { get; set; }

        public TextBoxAppender(TextBox textBox)
        {
            var frm = textBox.FindForm();
            if (frm == null)
                return;
            frm.FormClosing += delegate { Close(); };

            _textBox = textBox;
            Name = "TextBoxAppender";
        }

        internal static void ConfigureTextBoxAppender(object textBox)
        {
          //  throw new NotImplementedException();
        }

        public static void ConfigureTextBoxAppender(TextBox textBox)
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var appender = new TextBoxAppender(textBox);
            hierarchy.Root.AddAppender(appender);
        }

        public void Close()
        {
            lock (_lockObj)
            {
                _textBox = null;
            }
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAppender(this);
        }

        public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
        {
            //Debug.WriteLine("***********");
            if (_textBox == null)
                return;

            var msg = string.Concat(loggingEvent.RenderedMessage, "\r\n");

            lock (_lockObj)
            {
                if (_textBox == null)
                    return;
                //Debug.WriteLine("++++++++++++++");
              //  var del = new Action<string>(s => _textBox.AppendText(s));
               // _textBox.BeginInvoke(del, msg);
            }
        }
    }
}
