using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;

namespace MOS.OS
{
    class Speaker : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Speaker(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "Read") { }

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
            Log.Info("Speaker process is running.");
            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    Status = (int)ProcessState.Blocked;
                    if (Kernel.ready.Contains(this))
                    {
                        Kernel.ready.Remove(this);
                    }
                    Kernel.blocked.Add(this);
                    Kernel.dynamicResources.First(res => res.Name == "TASKINSUPERVISORY").AskForResource(this);

                    break;
                case 1:
                    Pointer = 2;
                    Status = (int)ProcessState.Blocked;
                    if (Kernel.ready.Contains(this))
                    {
                        Kernel.ready.Remove(this);
                    }
                    Kernel.blocked.Add(this);
                    Kernel.staticResources.First(res => res.Key.Name == "CHAN3").Key.AskForResource(this);

                    break;
                case 2:
                    Status = (int)ProcessState.Blocked;
                    if (Kernel.ready.Contains(this))
                    {
                        Kernel.ready.Remove(this);
                    }
                    Kernel.blocked.Add(this);
                    
                    Beep(Pointer);
                    Pointer = 0;
                    break;
            }

            
        }
        public static void Beep(int x)
        {
            Console.Beep(2000, x * 1000);
        }
    }
}