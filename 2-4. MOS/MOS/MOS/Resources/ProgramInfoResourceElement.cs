using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class ProgramInfoResourceElement : ResourceElement
    {
        public List<string> Lines{ get; set; }

        public ProgramInfoResourceElement(List<string> lines, string value = "", Process receiver = null, Process sender = null) : base(value, receiver, sender)
        {
            Lines = lines;
        }
    }
}
