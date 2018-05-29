using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MOS.Enums;
using MOS.Resources;

namespace MOS.OS
{
    public class JobGovernor : Process
    {
        public ResourceElement Element { get; set; }
        public string name = "Virtual Machine ";
        public static int counter = 0;
        public ResourceElement TaskInDiskElement { get; set; }
        public Descriptor Descriptor { get; set; }
        public List<string> VMList
        {
            get => list; set
            {
                list = value;
                RaisePropertyChangedEvent("VMList");
            }
        }

        private string _ptr;
        private GUI.VMForm _vmForm;
        private List<String> list = new List<string>();
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public JobGovernor(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "JobGovernor " ) { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null) handler(this, new PropertyChangedEventArgs(propertyName));
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
                    counter++;
                    name += counter.ToString();
                    Kernel.staticResources.First(res => res.Key.Name == "USERMEMORY").Key.AskForResource(this);
                    break;
                case 1:
                    Pointer = 2;
                    _ptr = RealMachine.RealMachine.memory.getMemory();
                    if (_ptr == "0") // edge case scenario/ needs fix
                    {
                        Log.Info("We are out of memory");
                        Resource resource = Kernel.staticResources.First(res => res.Key.Name == "USERMEMORY").Key;
                        Kernel.staticResources[resource] = true;
                        Kernel.staticResources.First(res => res.Key.Name == "USERMEMORY").Key.AskForResource(this);
                    }
                    Kernel.dynamicResources.First(res => res.Name == "LOADERPACKET").ReleaseResource(new MemoryInfoResourceElement(_ptr, TaskInDiskElement.Value, sender : this));
                    break;
                case 2:
                    Pointer = 3;
                    Kernel.dynamicResources.First(res => res.Name == "FROMLOADER").AskForResource(this);
                    break;
                case 3:
                    Pointer = 4;
                    Process vm = new VirtualMachine.VirtualMachine(Kernel, this, 50, (int)ProcessState.Ready, new List<Resource>(), Guid.NewGuid(), 0, TaskInDiskElement.Value);
                    Descriptor = new Descriptor(_ptr);
                    Descriptor.LoadVMState((VirtualMachine.VirtualMachine)vm);
                    Kernel.ready.Add(vm);
                    Childrens.Add(vm);
                    _vmForm = MOS.Program.RunVM(this);
                    
                    Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT").AskForResource(this);
                    break;
                case 4:
                    Childrens[0].Status = (int)ProcessState.ReadyStopped;
                    var value = Element.Value;
                    if (value == "notIO")
                    {
                        Childrens[0].DeleteProcess();
                        Childrens.RemoveAt(0);
                        Kernel.dynamicResources.First(res => res.Name == "TASKINDISK").ReleaseResource(new ResourceElement(value : "0", sender: this));
                    }
                    else if (value == "input")
                    {
                        Pointer = 5;
                        Kernel.dynamicResources.First(res => res.Name == "LINEFROMUSER").AskForResource(this);
                    }
                    else if(value == "timer")
                    {
                        Descriptor.TI.TI = 10;
                        Childrens[0].Status = (int)ProcessState.Ready;
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT").AskForResource(this);

                    }
                    else if(value == "output")
                    {
                        Pointer = 6;
                        Kernel.dynamicResources.First(res => res.Name == "LINEINMEMORY").ReleaseResource(new IOResourceElements("", memoryByte: Descriptor.R4.R.ToHex(), lenght: Descriptor.R3.R, resourceGUI : _vmForm));

                    }
                    else if (value == "byp")
                    {
                        Pointer = 7;
                        Kernel.dynamicResources.First(res => res.Name == "BEEPER").ReleaseResource();
                    }
                    break;
                case 5:
                    Pointer = 4;
                    if (Descriptor.TI.TI <= 0)
                        Descriptor.TI.TI = 10;
                    Childrens[0].Status = (int)ProcessState.Ready;
                    string line = Element.Value;
                    int byteToWrite = Descriptor.R4.R;
                    while (line.Length < 16)
                    {
                        line += " ";
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        RealMachine.RealMachine.memory.WriteAt(byteToWrite / 16, byteToWrite % 16, line.Substring(4 * i, 4));
                        byteToWrite++;
                    }
                    Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT").AskForResource(this);
                    break;
                case 6:
                    Pointer = 4;
                    if (Descriptor.TI.TI <= 0)
                        Descriptor.TI.TI = 10;
                    Childrens[0].Status = (int)ProcessState.Ready;
                    Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT").AskForResource(this);
                    break;
                case 7:
                    Pointer = 4;
                    if (Descriptor.TI.TI <= 0)
                        Descriptor.TI.TI = 10;
                    Childrens[0].Status = (int)ProcessState.Ready;
                    Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT").AskForResource(this);
                    break;

            }
        }
    }
}
