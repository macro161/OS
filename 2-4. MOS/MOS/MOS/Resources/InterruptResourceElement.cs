using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class InterruptResourceElement : ResourceElement
    {
        public Process JobGoverner { get; set; }

        public InterruptResourceElement(Process jobGoverner, string value, Process receiver = null, Process sender = null) : base(value, receiver, sender)
        {
            JobGoverner = jobGoverner;
        }
    }
}
