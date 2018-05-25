using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Read : Process
    {
        public Read(int priority, string id, string status, int pointer, Resource[] resources) : base(priority, status, resources, id, pointer) { }

        public string[] flashData;

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override string GetId()
        {
            throw new NotImplementedException();
        }

        public override int GetPriority()
        {
            throw new NotImplementedException();
        }

        public override string GetStatus()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            string flashLocation = base.resources[base.pointer].data;
            int counter = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(@"" + flashLocation);

            while ((line = file.ReadLine()) != null) {
                flashData[counter] = line;
                counter++;
            }

            Resource taskInSupervisoryMemory = new Resource("TASKINSUPERVISORYMEMORY",base.id,1,"");

            Kernel.dynamicResources.Add(taskInSupervisoryMemory);

        }

        public override void SetPriority(int priority)
        {
            throw new NotImplementedException();
        }

        public override void SetStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}
