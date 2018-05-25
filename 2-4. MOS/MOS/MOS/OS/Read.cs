﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Read : Process
    {
        public Read(Kernel kernel, int priority, int status, Guid id, int pointer, Resource[] resources) : base(kernel, priority, status, resources, id, pointer, "Read") { }

        public List<String> flashData = new List<String>();

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();

        }

        public override void Run()
        {
            string flashLocation = Resources[Pointer].Data;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(@"" + flashLocation);

            while ((line = file.ReadLine()) != null) {
                flashData.Add(line);
            }

            SupervisoryMemory.Memory = flashData;

            Resource taskInSupervisoryMemory = new Resource(Kernel, "TASKINSUPERVISORYMEMORY", this ,1,"");

            Kernel.dynamicResources.Add(taskInSupervisoryMemory);

        }
    }
}
