using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    public class Interupt : Process
    {
        public InterruptResourceElement Element { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Interupt(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources)  { }

        public Interupt()
        {
        }

        public void DecrementPriority()
        {

        }

        public void Run()
        {
            Log.Info("Iterrupt process is running.");

            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    Log.Info("Waiting for interrupt resource.");
                    Kernel.dynamicResources.First(res => res.Name == "INTERUPT").AskForResource(this);

                    break;
                case 1:
                    var PI = RealMachine.RealMachine.pi.PI;
                    var SI = RealMachine.RealMachine.si.SI;
                    var TI = RealMachine.RealMachine.ti.TI;
                    RealMachine.RealMachine.pi.PI = 0;
                    RealMachine.RealMachine.si.SI = 0;
                    Pointer = 0;
                    Log.Info("Identifying interrupt.");
                    if (PI > 0|| SI == 3)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "notIO", Element.JobGoverner, null));
                    else if(SI == 1)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "input", Element.JobGoverner, null));
                    else if (SI == 2)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "output", Element.JobGoverner, null));
                    else if (SI == 4)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "byp", Element.JobGoverner, null));
                    else if (TI == 0)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "timer", Element.JobGoverner, null));
                    break;
            }
        }
    }
}
