using MOS.Enums;
using MOS.Resources;
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
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

        public void Planner()
        {
            Log.Info("Planner process is running.");
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
                if (block.CheckIfReady())
                {
                    ready.Add(block);
                    blocked.Remove(block);
                }
            }
            running.DecrementPriority();
            running.Run();
        }

        public void ResourcePlanner()
        {
            foreach (var res in staticResources) // paskiria statinį resursą.
            {
                if (res.Key.Awaiters.Count > 0 && res.Value == true)
                {
                    blocked.Remove(res.Key.Awaiters[0]);
                    ready.Add(res.Key.Awaiters[0]);
                    res.Key.Awaiters[0].Status = (int)ProcessState.Ready;
                    res.Key.Awaiters[0].Resources.Add(res.Key);
                    res.Key.Awaiters.RemoveAt(0);
                }
            }
            foreach (var res in dynamicResources)
            {
                if (res.Awaiters.Count > 0 && res.Elements.Count > 0)
                {
                    Process proc = null;
                    switch (res.Name)
                    {
                        case "FILEINPUT":
                            res.Awaiters[0].Resources.Add(res);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            ((Read)res.Awaiters[0]).Element = res.Elements[0];
                            res.Elements.RemoveAt(0);
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "TASKINSUPERVISORY":
                            res.Awaiters[0].Resources.Add(res);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            res.Elements.RemoveAt(0);
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "TASKNAMEINSUPERVISORY":
                            res.Awaiters[0].AddResource(res);
                            ((JobToDisk)res.Awaiters[0]).PropElement = ((ProgramInfoResource)res).Elements[0];
                            res.Elements.RemoveAt(0);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "TASKDATAINSUPERVISORY":
                            res.Awaiters[0].AddResource(res);
                            ((JobToDisk)res.Awaiters[0]).DataElement = ((ProgramInfoResource)res).Elements[0];
                            res.Elements.RemoveAt(0);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "TASKCODEINSUPERVISORY":
                            res.Awaiters[0].AddResource(res);
                            ((JobToDisk)res.Awaiters[0]).CodeElement = ((ProgramInfoResource)res).Elements[0];
                            res.Elements.RemoveAt(0);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "TASKINDISK":
                            res.Awaiters[0].AddResource(res);
                            ((MainProc)res.Awaiters[0]).Element = (res).Elements[0];
                            res.Elements.RemoveAt(0);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "LOADERPACKET":
                            var temp = (MemoryInfoResource)res;
                            res.Awaiters[0].Resources.Add(res);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            ((Loader)res.Awaiters[0]).Element = temp.Elements[0];
                            res.Elements.RemoveAt(0);
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "FROMLOADER":
                            foreach (var elem in res.Elements)
                            {
                                if (res.Awaiters.Any(pro => pro == elem.Receiver))
                                {
                                    proc = res.Awaiters.First(pro => pro == elem.Receiver);
                                    break;
                                }
                            }
                            if (proc != null)
                            {
                                proc.Resources.Add(res);
                                proc.Status = (int)ProcessState.Ready;
                                blocked.Remove(proc);
                                ready.Add(proc);
                                res.Awaiters.Remove(proc);
                                res.Elements.Remove(res.Elements.First(elem => elem.Receiver == proc));
                            }
                            break;
                        case "FROMINTERRUPT":
                            foreach (var elem in res.Elements)
                            {
                                if (res.Awaiters.Any(pro => pro == elem.Receiver))
                                {
                                    proc = res.Awaiters.First(pro => pro == elem.Receiver);
                                    break;
                                }
                            }
                            if (proc != null)
                            {
                                proc.Resources.Add(res);
                                proc.Status = (int)ProcessState.Ready;
                                blocked.Remove(proc);
                                ready.Add(proc);
                                res.Awaiters.Remove(proc);
                                ResourceElement element = res.Elements.First(elem => elem.Receiver == proc);
                                ((JobGovernor)proc).Element = element;
                                res.Elements.Remove(element);
                            }
                            break;
                        case "INTERUPT":
                            res.Awaiters[0].AddResource(res);
                            ((Interupt)res.Awaiters[0]).Element = ((InterruptResource)res).Elements[0];
                            res.Elements.RemoveAt(0);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "LINEINMEMORY":
                            res.Awaiters[0].AddResource(res);
                            ((Printer)res.Awaiters[0]).Element = ((IOResource)res).Elements[0];
                            res.Elements.RemoveAt(0);
                            res.Awaiters[0].Status = (int)ProcessState.Ready;
                            blocked.Remove(res.Awaiters[0]);
                            ready.Add(res.Awaiters[0]);
                            res.Awaiters.RemoveAt(0);
                            break;
                        case "LINEFROMUSER":
                            foreach (var elem in res.Elements)
                            {
                                if (res.Awaiters.Any(pro => pro == elem.Receiver))
                                {
                                    proc = res.Awaiters.First(pro => pro == elem.Receiver);
                                    break;
                                }
                            }
                            if (proc != null)
                            {
                                proc.Resources.Add(res);
                                proc.Status = (int)ProcessState.Ready;
                                blocked.Remove(proc);
                                ready.Add(proc);
                                res.Awaiters.Remove(proc);
                                IOResourceElements element = ((IOResource)res).Elements.First(elem => elem.Receiver == proc);
                                //proc.Element = element šitą reiks prie job governer
                                ((IOResource)res).Elements.Remove(element);
                            }
                            break;


                    }
                }
            }
            /*foreach (Process blockedProcess in blocked)
            {
                foreach (Resource dynamicRes in dynamicResources)
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
                }
                bool gotAllResources = true;
                List<Resource> tempResources = new List<Resource>();
                foreach (string reqResource in blockedProcess.ResourcesINeed)
                {
                    if (dynamicResources.Any(res => res.Name == reqResource))
                    {
                        Resource temp = dynamicResources.FirstOrDefault(res => res.Name == reqResource);
                        if (temp.Elements.Count > 0)
                        {
                            if (temp.Elements.Any(elem => elem.Receiver == null || elem.Receiver == blockedProcess))
                            {
                                tempResources.Add(temp);
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
                        if (reqResource == "USERMEMORY")
                        {
                            if(((MemoryResource)(staticResources.First(res => res.Key.Name == "USERMEMORY").Key)).FreeElements < 17)
                            {
                                gotAllResources = false;
                                break;
                            }
                        }
                        tempResources.Add(staticResources.FirstOrDefault(res => res.Key.Name == reqResource).Key);
                    }
                    else
                    {
                        gotAllResources = false;
                        break;
                    }
                }
                if (gotAllResources)
                {
                    foreach (Resource res in tempResources)
                    {
                        if (staticResources.ContainsKey(res))
                        {
                            int numb = res.Awaiters.IndexOf(blockedProcess);
                            res.WaitingCount.RemoveAt(numb);
                            res.WaitingProcPoint.RemoveAt(numb);
                            res.Awaiters.RemoveAt(numb);
                            blockedProcess.ResourcesINeed.Remove(res.Name);
                            blockedProcess.Resources.Add(res);
                            staticResources[res] = false;
                        }
                        else
                        {
                            if (res.Elements.Any(elem => elem.Receiver == blockedProcess))
                            {
                                string value = res.Elements.First(elem => elem.Receiver == blockedProcess).Value;
                                int numb = res.Awaiters.IndexOf(blockedProcess);
                                res.WaitingProcPoint[numb].Clear().Append(numb);
                                res.WaitingCount.RemoveAt(numb);
                                res.WaitingProcPoint.RemoveAt(numb);
                                res.Awaiters.RemoveAt(numb);
                            }
                        }
                    }
            }*/
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
