using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Planner
    {
        private static string READY = "READY";
        private static string RUNNING = "RUNNING";
        private static string BLOCKED = "BLOCKED";

        public static List<Process> ready = new List<Process>();
        public static List<Process> blocked = new List<Process>();
        public static Process running;
    }
}
