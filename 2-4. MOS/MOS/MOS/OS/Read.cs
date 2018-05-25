using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Read : Process
    {
        public Read(Kernel kernel, int priority, string id, int status, int pointer, Resource[] resources) : base(kernel, priority, status, resources, id, pointer) { }

        public string[] flashData;

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();

        }

        public override void Run()
        {
            string flashLocation = Resources[Pointer].Data;
            int counter = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(@"" + flashLocation);

            while ((line = file.ReadLine()) != null) {
                flashData[counter] = line;
                counter++;
            }

            Resource taskInSupervisoryMemory = new Resource(Kernel, "TASKINSUPERVISORYMEMORY", this ,1,"");

            Kernel.dynamicResources.Add(taskInSupervisoryMemory);

        }
    }
}
