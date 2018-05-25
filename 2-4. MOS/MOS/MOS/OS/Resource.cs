using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Resource
    {
        public string Name { get; private set; }
        public int Elements { get; private set; }
        public string Data { get; private set; }
        public Process Creator { get; private set; }
        List<Process> Awaiters { get; set; }
        public Kernel Kernel { get; private set; }

        public Resource(Kernel kernel, string name, Process creator, int elements, string data)
        {
            Kernel = kernel;
            Name = name;
            Creator = creator;
            Elements = elements;
            Data = data;
            Awaiters = new List<Process>();
        }
    }
}
