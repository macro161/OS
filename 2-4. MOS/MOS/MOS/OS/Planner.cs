using MOS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Planner
    {
        public static List<Process> ready = new List<Process>();
        public static Process running;
        public static List<Process> blocked = new List<Process>();

        /**
         * Procesai surikiuojami pagal prioritetą
         */
        public static void LineUp()
        {
            for (int i = 0; i < blocked.Count && blocked.Any(); i++)
            {
                if (CanBeSetToReady(blocked[i]))
                {
                    ReadyProcess(blocked[i]);
                }
            }
            ready = ready.OrderBy(x => x.GetPriority()).ToList();
            if (ready.Any())
            {
                RunProcess(ready[0]);
                running.Run(running.GetPointer());
            }
        }

        private static bool CanBeSetToReady(Process process)
        {
            String resource = process.GetAResource();
            if (Resources.check(resource))
            {
                Resources.takeAResource(resource);
                return true;
            }
            return false;
        }

        public static void BlockProcess(Process process)
        {

            if (process.Equals(running))
            {
                process.SetStatus((int)ProcessState.Blocked);
                blocked.Add(process);
            }
        }
        public static void RunProcess(Process process)
        {
            ready.Remove(process);
            process.SetStatus((int)ProcessState.Running);
            running = process;
        }

        public static void ReadyProcess(Process process)
        {
            blocked.Remove(process);
            process.SetStatus((int)ProcessState.Ready);
            ready.Add(process);
        }
    }
}
