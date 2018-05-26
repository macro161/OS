using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.OS;

namespace MOS.Resources
{
    class ResourceElement
    {
        public Process Receiver { get; set; }
        public Process Sender { get; set; }
        public string Value { get; set; }

        public ResourceElement(string value = "", Process receiver = null, Process sender = null)
        {
            Receiver = receiver;
            Sender = sender;
            Value = value;
        }
    }
}
