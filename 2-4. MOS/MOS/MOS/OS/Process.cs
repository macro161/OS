using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.RealMachine;

namespace MOS.OS
{
    class Process
    {
        public static string READY = "READY";
        public static string RUNNING = "RUNNING";
        public static string BLOCKED = "BLOCKED";
        public int priority;
        public string status = READY;
        public Resource [] resources;
        public string id;
        public int pointer;

        public Process(int priority, string id, string status, int pointer, Resource[] res)
        {
            this.priority = priority;
            this.id = id;
            this.status = status;
            this.pointer = pointer;
            this.resources = res;
            this.pointer = pointer;
        }

        public void AddResource(Resource resource)
        {
            pointer++;
            resources[pointer] = resource;
        }
    }
}