using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS // pagrindine klase is kurios inheritina visi kiti procesai
{
    abstract class Process
    {

        private static string READY = "READY";
        private static string RUNNING = "RUNNING";
        private static string BLOCKED = "BLOCKED";

        private int priority;
        private string id;
        private int pointer;
        private string status = READY;

        private string[] resource;

        public int GetPointer() {
            return pointer;
        }

        public void SetPointer() {
            this.pointer = pointer;
        }

        public string GetStatus() {
            return status;
        }

        public void SetStatus(string status) {
            this.status = status;
        }

        public int GetPriority() {
            return priority;
        }

        public void SetPriority(int priority)
        {
            this.priority = priority;
        }

        public string getId() {
            return id;
        }

        public void setId(string id) {

        }

        public Process(int priority, string id, string status, int pointer, string[] list) {
            this.priority = priority;
            this.id = id;
            this.status = status;
            this.pointer = pointer;
            this.resource = list;
        }

        public string getResouce() {
            return this.resource[this.priority];
        }

        abstract public void run(int pointer);
        
        

    }
}
