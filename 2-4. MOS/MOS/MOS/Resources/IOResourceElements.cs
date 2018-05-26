using MOS.GUI;
using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class IOResourceElements
    {
        public Process Receiver { get; set; }
        public Process Sender { get; set; }
        public string MemoryByte { get; set; }
        public int Lenght { get; set; }
        public VMForm ResourceGUI { get; set; }

        public IOResourceElements(string memoryByte = "", int lenght = 0, Process receiver = null, Process sender = null, VMForm resourceGUI = null)
        {
            ResourceGUI = resourceGUI;
            Receiver = receiver;
            Sender = sender;
            MemoryByte = memoryByte;
            Lenght = lenght;
        }
    }
}
