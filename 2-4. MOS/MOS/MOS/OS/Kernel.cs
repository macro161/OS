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
        public Dictionary<Resource, bool> staticResources = new Dictionary<Resource, bool>();

        public void SortProcesses()
        {
            ready = ready.OrderByDescending(x => x.Priority).ToList();

            blocked = blocked.OrderByDescending(x => x.Priority).ToList();
        }

        public void Planner() {
            SortProcesses();
            if (ready.Count > 0)
            {
                Process temp = ready[0];
                ready[0] = running;
                running = temp;
            }
            ResourcePlanner();
            foreach (Process block in blocked)
            {
                if (block.CheckIfReady()) {
                    ready.Add(block);
                    blocked.Remove(block);
                }
            }

            running.DecrementPriority();
            running.Run();
        }

        private void ResourcePlanner()
        {
            foreach (Process blockedProcess in blocked)
            {
                /*foreach (Resource dynamicRes in dynamicResources)
                {
                    if (dynamicRes.Awaiters.Contains(blockedProcess) && !(blockedProcess.Resources.Contains(dynamicRes)))
                    {
                        blockedProcess.Resources.Add(dynamicRes);
                        dynamicResources.Remove(dynamicRes);
                    }
                }

                foreach (KeyValuePair<Resource, bool> staticRes in staticResources)
                {
                    if (staticRes.Key.Awaiters.Contains(blockedProcess) && !(blockedProcess.Resources.Contains(staticRes.Key)))
                    {
                        blockedProcess.Resources.Add(staticRes.Key);
                        staticResources[staticRes.Key] = false;
                    }
                }*/
                bool gotAllResources = true;
                foreach (string reqResource in blockedProcess.ResourcesINeed)
                {
                    bool gotResource = false;
                    if (dynamicResources.Any(res => res.Name == reqResource))
                    {
                        Resource temp = dynamicResources.FirstOrDefault(res => res.Name == reqResource);
                        if (temp.Elements.Count > 0)
                        {
                            if (temp.Elements.Any(elem => elem.Receiver == null || elem.Receiver == blockedProcess))
                            {
                                gotResource = true;
                            }
                            gotAllResources = false;
                            break;
                        }
                        else
                        {
                            gotAllResources = false;
                            break; ;
                        }
                    }
                    else if (staticResources.Any(res => res.Key.Name == reqResource && res.Value == true))
                        {
                            gotResource = true;
                        }
                    else
                    {
                        gotAllResources = false;
                        break; ;
                    }
                    }
                }
            }
        }

        public void BlockProcess(Process process)
        {
            if (process == running)
            {
                process.Status = (int)ProcessState.Blocked;
                blocked.Add(process);
            }
            else
            {
                process.Status = (int)ProcessState.Blocked;
                ready.Remove(process);
                blocked.Add(process);
            }
        }

        public void RunProcess(Process process) { }

        public void ReadyProcess(Process process) { }
    }
}
