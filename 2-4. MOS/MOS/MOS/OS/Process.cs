using MOS.Enums;
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

        private int priority;
        private string id;
        private int pointer;
        private int status = (int)ProcessState.Ready;

        private string[] resource;

        public int GetPointer() {
            return pointer;
        }

        public void SetPointer(int pointer) {
            this.pointer = pointer;
        }

        public int GetStatus() {
            return status;
        }

        public void SetStatus(int status) {
            this.status = status;
        }

        public int GetPriority() {
            return priority;
        }

        public void SetPriority(int priority)
        {
            this.priority = priority;
        }

        public string GetId() {
            return id;
        }

        public void SetId(string id) {

        }

        public Process(int priority, string id, int status, int pointer, string[] list) {
            this.priority = priority;
            this.id = id;
            this.status = status;
            this.pointer = pointer;
            this.resource = list;
        }

        public string GetAResource() {
            return this.resource[this.priority];
        }

        abstract public void Run(int pointer);
        
        

    }
}
