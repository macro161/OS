using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.GUI;

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
        public List<int> WaitingCount { get; set; }
        public List<StringBuilder> WaitingProcPoint { get; set; }
        public VMForm resourceGUI;


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
            WaitingProcPoint = new List<StringBuilder>();
            WaitingCount = new List<int>();
        }

        public void AskForResource(Process process, int count, ref StringBuilder destiny)
        {
            Awaiters.Add(process);
            WaitingCount.Add(count);
            WaitingProcPoint.Add(destiny);
        }
    }
}
