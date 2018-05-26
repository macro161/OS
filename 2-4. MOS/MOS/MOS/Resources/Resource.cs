using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;

namespace MOS.Resources
{
    class Resource
    {
        public string Name { get; private set; }
        public Process Creator { get; private set; }
        public List<Process> Awaiters { get; set; }
        public Kernel Kernel { get; private set; }
        public Guid Id { get; private set; }
        public List<ResourceElement> Elements { get; set; }
        public Resource(Kernel kernel, string name, Process creator)
        {
            Kernel = kernel;
            Name = name;
            Creator = creator;
            Awaiters = new List<Process>();
            Id = Guid.NewGuid();
            Elements = new List<ResourceElement>();
        }

        public void AskForResource(Process process)
        {
            process.Status = (int)ProcessState.Blocked;
            Kernel.blocked.Add(process);
            Kernel.ready.Remove(process);
            Awaiters.Add(process);
            Kernel.ResourcePlanner();
        }

        public void ReleaseResource(ResourceElement resElement)
        {
            Elements.Add(resElement);
            Kernel.ResourcePlanner();
        }
    }
}
