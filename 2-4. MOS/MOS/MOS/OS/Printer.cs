using MOS.GUI;
using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;
using System.Threading;

namespace MOS.OS
{
    public class Printer : Process
    {
        public IOResourceElements Element { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Printer(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "Printer") { }

        public override void DecrementPriority()
        {
        }

        public override void Run()
        {
            Log.Info("Print process is running.");
            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    Kernel.dynamicResources.First(res => res.Name == "LINEINMEMORY").AskForResource(this);
                    
                    break;
                case 1:
                    Pointer = 2;
                    Kernel.staticResources.First(res => res.Key.Name == "CHAN3").Key.AskForResource(this);

                    break;
                case 2:
                    Pointer = 0;
                    string message = "";
                    int byteNumber = Element.MemoryByte.ToHex();

                    for (int i = 0; i < Element.Lenght; i++ )
                    {
                        message = message + RealMachine.RealMachine.memory.StringAt(byteNumber / 16, byteNumber % 16);
                        byteNumber++;
                    }
                    
                    Print(message);
                    Kernel.staticResources.First(res => res.Key.Name == "CHAN3").Key.ReleaseResource();

                    break;
            }
        }

        public void Print(string line)
        {
            List<string> meh = new List<string>();
            meh = Element.ResourceGUI.jg.VMList;
            meh.Add(line);
            Element.ResourceGUI.jg.VMList  = meh;
        }
    }
}
