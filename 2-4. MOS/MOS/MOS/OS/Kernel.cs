using MOS.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Kernel
    {
        public List<Process> ready = new List<Process>();
        public List<Process> blocked = new List<Process>();
        public Process running;

        public List<Resource> dynamicResources = new List<Resource>();
        public List<Resource> staticResources = new List<Resource>();

        public void SortProcesses()
        {
            ready = ready.OrderByDescending(x => x.Priority).ToList();

            blocked = blocked.OrderByDescending(x => x.Priority).ToList();
        }

        public void Planner() {
            SortProcesses();
            Process temp = ready[0];
            ready[0] = running;
            running = temp;
            ResourcePlanner();
            
           
        }

        private void ResourcePlanner()
        {
            foreach (Process blockedProcess in blocked)
            {
                foreach (Resource dynamicRes in dynamicResources)
                {
                    if (dynamicRes.Awaiters.Contains(blockedProcess) && !(blockedProcess.Resources.Contains(dynamicRes)))
                    {
                        //blockedProcess.Resources;
                    }
                }
            }
        }

        public void BlockProcess(Process process)
        {
            if (process == running)
            {
                process.Status = (int)ProcessState.Blocked;
            }
        }

        public void RunProcess(Process process) { }

        public void ReadyProcess(Process process) { }
    }
}
