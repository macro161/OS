using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MOS.Enums;
using MOS.Resources;

namespace MOS.OS
{
    class JobGovernor : Process
    {
        public ResourceElement Element { get; set; }
        public ResourceElement TaskInDiskElement { get; set; }

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public JobGovernor(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "MainProc") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void DecrementPriority()
        {
        }

        public override void Run()
        {
            Log.Info("JobGovernor process is running.");
            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    Kernel.staticResources.First(res => res.Key.Name == "USERMEMORY").Key.AskForResource(this);
                    break;
                case 1:
                    Pointer = 2;
                    string ptr = RealMachine.RealMachine.memory.getMemory();
                    if (ptr == "0") // edge case scenario/ needs fix
                    {
                        Log.Info("We are out of memory");
                        Resource resource = Kernel.staticResources.First(res => res.Key.Name == "USERMEMORY").Key;
                        Kernel.staticResources[resource] = true;
                        Kernel.staticResources.First(res => res.Key.Name == "USERMEMORY").Key.AskForResource(this);
                    }
                    Kernel.dynamicResources.First(res => res.Name == "LOADERPACKET").ReleaseResource(new MemoryInfoResourceElement(ptr, TaskInDiskElement.Value));
                    break;
                case 2:
                    Pointer = 3;
                    Kernel.dynamicResources.First(res => res.Name == "FROMLOADER").AskForResource(this);
                    break;
                case 3:
                    Pointer = 4;
                    Process vm = new VirtualMachine.VirtualMachine(Kernel, this, 50, (int)ProcessState.Ready, new List<Resource>(), Guid.NewGuid(), 0, TaskInDiskElement.Value);
                    Kernel.ready.Add(vm);
                    MOS.Program.RunVM(this);
                    Kernel.dynamicResources.First(res => res.Name == "FROMINTERRUPT").AskForResource(this);
                    break;
                case 4:
                    var value = Element.Value;
                    if (value == "notIO")
                    {
                        DeleteProcess();
                    }
                    else if (value == "input")
                    {
                        Pointer = 5;
                        Kernel.staticResources.First(res => res.Key.Name == "LINEFROMUSER").Key.AskForResource(this);
                    }
                    else if (value == "output")
                    {
                        Pointer = 6;
                        Kernel.dynamicResources.First(res => res.Name == "LINEINMEMORY").ReleaseResource(new IOResourceElements(""));

                    }
                    else if (value == "byp")
                    {
                        Pointer = 7;
                        Kernel.dynamicResources.First(res => res.Name == "BEEPER").ReleaseResource();
                    }
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;

            }
        }
    }
}
