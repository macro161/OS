using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;

namespace MOS.OS
{
    class SwapBack : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SwapBack(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "SwapBack") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void DecrementPriority()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            Log.Info("SwapBack process running.");

            switch (Pointer)
            {
                case 0:
                    Status = (int)ProcessState.Blocked;
                    if (Kernel.ready.Contains(this))
                    {
                        Kernel.ready.Remove(this);   
                    }
                    Kernel.blocked.Add(this);

                    Kernel.staticResources.First(res => res.Key.Name == "USERMEMORY").Key.AskForResource(this);
                    Pointer = 1;

                    break;
                case 1:
                    Status = (int)ProcessState.Blocked;
                    if (Kernel.ready.Contains(this))
                    {
                        Kernel.ready.Remove(this);
                    }
                    Kernel.blocked.Add(this);
                    Kernel.staticResources.First(res => res.Key.Name == "CHAN4").Key.AskForResource(this);
                    Pointer = 0;

                    break;
            }
        }
    }
}
