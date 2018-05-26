using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;
using MOS.RealMachine;
using MOS.Resources;

namespace MOS.OS
{
    abstract class Process
    {
        public int Priority { get; set; }
        public int Status { get; set; }
        public List<Resource> Resources = new List<Resource>();
        public Process Father { get; set; }
        public Guid Id { get; private set; }
        public int Pointer { get; set; }
        public Kernel Kernel { get; private set; }
        public List<Process> Childrens { get; set; }
        public string Name { get; private set; }
        public List<string> ResourcesINeed = new List<string>();


        public Process(Kernel kernel, Process father, int priority, int status, List<Resource> resources, Guid id, int pointer, string name)
        {
            Kernel = kernel;
            Priority = priority;
            Status = status;
            Resources = resources;
            Id = id;
            Pointer = pointer;
            Childrens = new List<Process>();
            Name = name;
            Father = father;
        }    

        public abstract void AddResource(Resource resource);

        public abstract void Run();

        public abstract void DecrementPriority();

        public bool CheckIfReady()
        {
            if (Status == (int)ProcessState.Ready)
                return true;
            return false;
        }    
    }
}