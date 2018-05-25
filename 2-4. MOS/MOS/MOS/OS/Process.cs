using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.RealMachine;

namespace MOS.OS
{
    abstract class Process
    {
        public int Priority { get; set; }
        public int Status { get; set; }
        public Resource [] Resources { get; set; }
        public List<string> resourcesNeeded = new List<string>() ;
        public Guid Id { get; private set; }
        public int Pointer { get; set; }
        public Kernel Kernel { get; private set; }
        public List<Process> Childrens { get; set; }
        public string Name { get; private set; }


        public Process(Kernel kernel, int priority, int status, Resource[] resources, Guid id, int pointer, string name)
        {
            Kernel = kernel;
            Priority = priority;
            Status = status;
            Resources = resources;
            Id = id;
            Pointer = pointer;
            Childrens = new List<Process>();
            Name = name;
        }    

        public abstract void AddResource(Resource resource);

        public abstract void Run();
    }
}