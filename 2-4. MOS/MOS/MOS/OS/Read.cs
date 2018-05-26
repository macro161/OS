using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Read : Process
    {
        public ResourceElement Element { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Read(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "Read") { }

        public List<String> flashData = new List<String>();

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();

        }

        public override void Run()
        {
            Log.Info("Read process is running.");
            switch (Pointer)
            {
                case 0:
                    Log.Info("Reading programs from file.");
                    string flashLocation = Element.Value;
                    string line;

                    System.IO.StreamReader file = new System.IO.StreamReader(@"" + flashLocation);

                    while ((line = file.ReadLine()) != null)
                    {
                        flashData.Add(line);
                    }
                    break;

                case 1:
                    Log.Info("Loading programs into Supervisory memory");
                    SupervisoryMemory.Memory = flashData;
                    Resource taskInSupervisoryMemory = new Resource(Kernel, "TASKINSUPERVISORYMEMORY", this);
                    Kernel.dynamicResources.Add(taskInSupervisoryMemory);
                    break;
            }
        }

        public override void DecrementPriority()
        {
            throw new NotImplementedException();
        }
    }
}
