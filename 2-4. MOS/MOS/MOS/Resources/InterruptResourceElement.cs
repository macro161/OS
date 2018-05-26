using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class InterruptResourceElement
    {
        public Process Receiver { get; set; }
        public Process Sender { get; set; }
        public string Value { get; set; }
        public Process JobGoverner { get; set; }


        public InterruptResourceElement(Process jobGoverner, string value, Process receiver = null, Process sender = null)
        {
            JobGoverner = jobGoverner;
            Receiver = receiver;
            Sender = sender;
            Value = value;
        }
    }
}
