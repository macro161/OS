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
        public ResElement Element { get; set; }

        public Read(Kernel kernel, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, priority, status, resources, id, pointer, "Read") { }

        public List<String> flashData = new List<String>();

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();

        }

        public override void Run()
        {
            switch (Pointer)
            {
                case 0:
                    string flashLocation = Element.Value;
                    string line;

                    System.IO.StreamReader file = new System.IO.StreamReader(@"" + flashLocation);

                    while ((line = file.ReadLine()) != null)
                    {
                        flashData.Add(line);
                    }
                    break;

                case 1:
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

        public override bool CheckIfReady()
        {
            throw new NotImplementedException();
        }
    }
}
