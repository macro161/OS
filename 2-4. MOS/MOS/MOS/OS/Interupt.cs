using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Interupt : Process
    {
        public InterruptResourceElement Element { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Interupt(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "Interupt") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void DecrementPriority()
        {

        }

        public override void Run()
        {
            Log.Info("Iterrupt process is running.");

            switch (Pointer)
            {
                case 0:
                    Log.Info("Waiting for interrupt resource.");
                    Kernel.dynamicResources.First(res => res.Name == "INTERRUPT").AskForResource(this);
                    Pointer = 1;
                    break;
                case 1:
                    Log.Info("Identifying interrupt.");
                    if(RealMachine.RealMachine.pi.PI >0|| RealMachine.RealMachine.ti.TI == 0 || RealMachine.RealMachine.si.SI == 3)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERRUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "notIO", Resources.First().Elements.First().Sender, null));
                    else if(RealMachine.RealMachine.si.SI == 1)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERRUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "input", Resources.First().Elements.First().Sender, null));
                    else if (RealMachine.RealMachine.si.SI == 2)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERRUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "output", Resources.First().Elements.First().Sender, null));
                    else if (RealMachine.RealMachine.si.SI == 4)
                        Kernel.dynamicResources.First(res => res.Name == "FROMINTERRUPT")
                            .ReleaseResource(new InterruptResourceElement(null, "pyp", Resources.First().Elements.First().Sender, null));

                    break;
            }
        }
    }
}
