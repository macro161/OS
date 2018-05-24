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
        private static string READY = "READY";
        private static string RUNNING = "RUNNING";
        private static string BLOCKED = "BLOCKED";
        private int priorioty;
        private string status = READY;
        private string [] resources;
        private string id;
        private int pointer;

        public Process(int priority, string id, string status, int pointer, string[] res)
        {
            this.priorioty = priorioty;
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
            this.priorioty = priorioty;
        }

        public int GetPriority()
        {
            return priorioty;
        }

        public void AddResource(string resource)
        {
            pointer++;
            resources[pointer] = resource;
        }

        abstract public void run();

    }
}