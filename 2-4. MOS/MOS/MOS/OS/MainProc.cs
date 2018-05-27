using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;

namespace MOS.OS
{
    class MainProc : Process
    {
        public ResourceElement Element { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainProc(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "MainProc") { }

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
            Log.Info("Main process is running.");
            switch (Pointer)
            {
                case 0:
                    Kernel.dynamicResources.First(res => res.Name == "TASKINDISK").AskForResource(this);
                    Pointer = 1;
                    break;
                case 1:
                    if (Element.Value == "1")
                    {
                        Pointer = 0;
                        JobGovernor jg = new JobGovernor(Kernel, this, 80, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);
                        Childrens.Add(jg);
                    }
                    else
                    {
                        Childrens.First(x => x.Id == Element.Sender.Id).DeleteProcess();
                    }
                    break;
            }
        }
    }
}
