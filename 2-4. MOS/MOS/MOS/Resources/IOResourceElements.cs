using MOS.GUI;
using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class IOResourceElements : ResourceElement
    {
        public string MemoryByte { get; set; }
        public int Lenght { get; set; }
        public VMForm ResourceGUI { get; set; }

        public IOResourceElements(string value, string memoryByte = "", int lenght = 0, Process receiver = null, Process sender = null, VMForm resourceGUI = null) : base(value, receiver, sender)
        {
            ResourceGUI = resourceGUI;
            MemoryByte = memoryByte;
            Lenght = lenght;
        }
    }
}