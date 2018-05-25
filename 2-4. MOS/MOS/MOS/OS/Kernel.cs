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
        public static List<Process> ready = new List<Process>();
        public static List<Process> blocked = new List<Process>();
        public static Process running;

        public static List<Resource> dynamicResources = new List<Resource>();
        public static List<Resource> staticResources = new List<Resource>();

        public static void SortProcesses()
        {
            ready = ready.OrderByDescending(x => x.Priority).ToList();

            blocked = blocked.OrderByDescending(x => x.Priority).ToList();
        }

        public static void Planner() {
            SortProcesses();
            Process temp = ready[0];
            ready[0] = running;
            running = temp;
            ResourcePlanner();
            
           
        }

        private static void ResourcePlanner()
        {
            foreach (Process blockedProcess in blocked)
            {

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
