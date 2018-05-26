using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class ResElement
    {
        public Process Receiver { get; set; }
        public Process Sender { get; set; }
        public string Value { get; set; }
        public ResElement(string value = "",  Process receiver = null, Process sender = null)
        {
            Receiver = receiver;
            Sender = sender;
            Value = value;
        }
    }
}
