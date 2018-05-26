using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class Message : Resource
    {
        public Message(Kernel kernel, string name, Process creator) : base(kernel, name, creator)
        {
        }

        public string Value { get; set; }
    }
}
