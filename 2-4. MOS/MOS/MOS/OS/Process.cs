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
        private static string BLOCKED = "BLOCKED";
        private int priority;
        private string status = READY;
        private Resource [] resources;
        private string id;
        private int pointer;

        public Process(int priority, string id, string status, int pointer, Resource[] res)
        {
            this.priority = priority;
            this.id = id;
            this.status = status;
            this.pointer = pointer;
            this.resources = res;
            this.pointer = pointer;
        }

        public void SetStatus(string status)
        {
            this.status = status;
        }

        public string GetStatus()
        {
            return status;
        }

        public void SetPriority(int priority)
        {
            this.priority = priority;
        }

        public int GetPriority()
        {
            return priority;
        }

        public void AddResource(Resource resource)
        {
            pointer++;
            resources[pointer] = resource;
        }

       

    }
}