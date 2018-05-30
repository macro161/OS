using MOS.RealMachine;
using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    public class JobToDisk : Process
    {
        public ProgramInfoResourceElement PropElement { get; set; }
        public ProgramInfoResourceElement CodeElement { get; set; }
        public ProgramInfoResourceElement DataElement { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public JobToDisk(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "JobToDisk") { }
        

        public override void DecrementPriority()
        {

        }

        public override void Run()
        {
            Log.Info("JobToDisk process is running.");
            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    Log.Info("Waiting for task in supervisory.");
                    AskForResource("TASKNAMEINSUPERVISORY");
                    break;
                case 1:
                    Pointer = 2;
                    AskForResource("TASKCODEINSUPERVISORY");
                    break;
                case 2:
                    Pointer = 3;
                    AskForResource("TASKDATAINSUPERVISORY");
                    break;
                case 3:
                    Pointer = 4;
                    Log.Info("Waiting for task in Hard Disk.");
                    AskForResource("EXTERNALMEMORY");
                    break;
                case 4:
                    Pointer = 5;
                    Log.Info("Waiting for Channel 4.");
                    AskForResource("CHAN4");
                    break;
                case 5:
                    Log.Info("Loading program into Hard Disk.");
                    Pointer = 6;
                    if (!HardDisk.ProgramList.Any(prog => prog.name == PropElement.Lines[0]))
                    {
                        ChannelsDevice cd = new ChannelsDevice
                        {
                            ST = 2,
                            DT = 3
                        };
                        cd.XCHG(new Program(PropElement.Lines[0], DataElement.Lines, CodeElement.Lines));
                    }
                    Kernel.ProgramList = HardDisk.GetNames();
                    ReleaseResource("CHAN4");
                    break;
                case 6:
                    Pointer = 0;
                    Log.Info("Task in Hard Disk.");
                    ReleaseResource("TASKINDISK", new ResourceElement(value : PropElement.Lines[0]));
                    break;
            }
        }
    }
}
