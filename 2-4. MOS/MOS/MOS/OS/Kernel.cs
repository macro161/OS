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
        private static string READY = "READY";
        private static string RUNNING = "RUNNING";
        private static string BLOCKED = "BLOCKED";

        public static List<Process> ready = new List<Process>();
        public static List<Process> blocked = new List<Process>();
        public static Process running;

        public static List<Resource> dynamicResources = new List<Resource>();
        public static List<Resource> staticResources = new List<Resource>();

        public static void sortProcesses()
        {
            ready.Sort(delegate (Process x, Process y)
            {
                return y.priority.CompareTo(x.priority);
            });

            blocked.Sort(delegate (Process x, Process y)
            {
                return y.priority.CompareTo(x.priority);
            });

            
        }

        

        public static void BlockProcess(Process process)
        {
            if (process == running)
            {
                process.status = "BLOCKED";
            }

        }

        public static void RunProcess(Process process) { }

        public static void ReadyProcess(Process process) { }
    }
}
