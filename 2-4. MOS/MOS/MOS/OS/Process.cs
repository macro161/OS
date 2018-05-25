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
        public static string READY = "READY";
        public static string RUNNING = "RUNNING";
        public static string BLOCKED = "BLOCKED";
        public int priority;
        public string status = READY;
        public Resource [] resources;
        public string id;
        public int pointer;


        public Process(int priority, string status, Resource[] resources, string id, int pointer)
        {
            this.priority = priority;
            this.status = status;
            this.resources = resources;
            this.id = id;
            this.pointer = pointer;
        }

        public abstract void SetStatus(string status);

        public abstract string GetStatus();

        public abstract void SetPriority(int priority);

        public abstract int GetPriority();

        public abstract string GetId();

      

        public abstract void AddResource(Resource resource);

        public abstract void Run();
    }
}