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
            switch (Pointer)
            {
                case 0:
                    //Log.Info("JobGovernor process is running.");
                    Log.Info("Waiting for memory.");
                    Pointer = 1;
                    counter++;
                    name += counter.ToString();
                    AskForResource("USERMEMORY");
                    break;
                case 1:
                    Log.Info("Getting memory.");
                    Pointer = 2;
                    _ptr = RealMachine.RealMachine.memory.getMemory();
                    if (_ptr == "0") 
                    {
                        Log.Info("We are out of memory");
                        ReleaseResource("USERMEMORY");
                        AskForResource("USERMEMORY");
                    }
                    ReleaseResource("LOADERPACKET", new MemoryInfoResourceElement(_ptr, TaskInDiskElement.Value, sender : this));
                    ReleaseResource("USERMEMORY");
                    break;
                case 2:
                    Log.Info("Waiting for Loader.");
                    Pointer = 3;
                    AskForResource("FROMLOADER");
                    break;
                case 3:
                    Pointer = 4;
                    Log.Info("Creating VM.");
                    Process vm = new VirtualMachine.VirtualMachine(Kernel, this, 50, (int)ProcessState.Ready, new List<Resource>(), Guid.NewGuid(), 0, TaskInDiskElement.Value);
                    Descriptor = new Descriptor(_ptr);
                    Descriptor.LoadVMState((VirtualMachine.VirtualMachine)vm);
                    vm.CreateProcess();
                    Childrens.Add(vm);
                    _vmForm = MOS.Program.RunVM(this);
                    
                    AskForResource("FROMINTERUPT");
                    break;
                case 4:
                    Log.Info("Dealing with interrupt");
                    Childrens[0].Status = (int)ProcessState.ReadyStopped;
                    var value = Element.Value;
                    if (value == "notIO")
                    {
                        Childrens[0].DeleteProcess();
                        Childrens.RemoveAt(0);
                        ReleaseResource("TASKINDISK", new ResourceElement(value : "0", sender: this));
                    }
                    else if (value == "input")
                    {
                        Pointer = 5;
                        Log.Info("Waiting for input.");
                        AskForResource("LINEFROMUSER");
                    }
                    else if(value == "timer")
                    {
                        Descriptor.TI.TI = 10;
                        Childrens[0].Status = (int)ProcessState.Ready;
                        AskForResource("FROMINTERUPT");

                    }
                    else if(value == "output")
                    {
                        Pointer = 6;
                        Log.Info("Printing.");
                        ReleaseResource("LINEINMEMORY", new IOResourceElements("", memoryByte: Descriptor.R4.R.ToHex(), lenght: Descriptor.R3.R, resourceGUI : _vmForm));

                    }
                    else if (value == "byp")
                    {
                        Pointer = 7;
                        Log.Info("Beeping.");
                        ReleaseResource("BEEPER");
                    }
                    break;
                case 5:
                    Pointer = 4;
                    Log.Info("Got input.");
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
                    AskForResource("FROMINTERUPT");
                    break;
                case 6:
                    Pointer = 4;
                    if (Descriptor.TI.TI <= 0)
                        Descriptor.TI.TI = 10;
                    Childrens[0].Status = (int)ProcessState.Ready;
                    AskForResource("FROMINTERUPT");
                    break;
                case 7:
                    Pointer = 4;
                    if (Descriptor.TI.TI <= 0)
                        Descriptor.TI.TI = 10;
                    Childrens[0].Status = (int)ProcessState.Ready;
                    AskForResource("FROMINTERUPT");
                    break;

            }
        }
    }
}
