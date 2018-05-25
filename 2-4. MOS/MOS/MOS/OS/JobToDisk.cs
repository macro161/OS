﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class JobToDisk : Process
    {
        public JobToDisk(Kernel kernel, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, priority, status, resources, id, pointer, "JobToDisk") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override bool CheckIfReady()
        {
            throw new NotImplementedException();
        }

        public override void DecrementPriority()
        {
            
        }

        public override void Run()
        {
            HardDisk.ProgramList = SupervisoryMemory.ProgramList;
        }
    }
}
