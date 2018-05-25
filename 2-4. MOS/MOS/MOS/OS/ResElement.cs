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
        public bool ReturnList { get; set; }
        public ResElement(bool returnList = false,  Process receiver = null, Process sender = null)
        {
            Receiver = receiver;
            Sender = sender;
            ReturnList = returnList;
        }
    }
}
