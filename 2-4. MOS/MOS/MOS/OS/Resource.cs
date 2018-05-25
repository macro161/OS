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
        public int Count { get; private set; }
        public string Data { get; private set; }
        public Process Creator { get; private set; }
        public List<Process> Awaiters { get; set; }
        public Kernel Kernel { get; private set; }
        public Guid Id { get; private set; }
        public List<ResElement> Elements { get; set; }

        public Resource(Kernel kernel, string name, Process creator, int count, string data)
        {
            Kernel = kernel;
            Name = name;
            Creator = creator;
            Count = count;
            Data = data;
            Awaiters = new List<Process>();
            Id = Guid.NewGuid();
            Elements = new List<ResElement>();
        }

        public void AskForResource(Process process)
        {
            Awaiters.Add(process);
        }

        


    }
}
