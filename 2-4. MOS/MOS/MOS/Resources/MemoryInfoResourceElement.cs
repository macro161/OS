using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class MemoryInfoResourceElement : ResourceElement
    {
        public string Ptr { get; set; }

        public MemoryInfoResourceElement(string ptr, string value, Process receiver = null, Process sender = null) : base(value, receiver, sender)
        {
            Ptr = ptr;
        }
    }
}
