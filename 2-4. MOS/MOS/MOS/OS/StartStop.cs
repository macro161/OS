using MOS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class StartStop : Process
    {
        public StartStop(Kernel kernel, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, priority, status, resources, id, pointer, "StartStop") {

            ResourcesINeed[0] = "MOSEND";
        }

        public void InitSystemProceses() //nezinojau ka prie to poiterio rasyt tai 0 parasiau
        {
            Read read = new Read(Kernel, 100, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);
            JCL jcl = new JCL(Kernel, 99, (int)ProcessState.Blocked, Guid.NewGuid(), 0,null);
            JobToDisk jobToDisk = new JobToDisk(Kernel, 98, (int)ProcessState.Blocked, Guid.NewGuid(), 0,null);
            Loader loader = new Loader(Kernel, 97, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);
            MainProc mainProc = new MainProc(Kernel, 96, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);
            SwapBack swapBack = new SwapBack(Kernel, 95, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);
            Interupt interupt = new Interupt(Kernel, 90, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);
            ChanDevice chanDevice = new ChanDevice(Kernel, 90, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);
            Speaker speaker = new Speaker(Kernel, 90, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);
            Printer printer = new Printer(Kernel, 90, (int)ProcessState.Blocked, Guid.NewGuid(), 0, null);

            Kernel.blocked.Add(read);
            Kernel.blocked.Add(jcl);
            Kernel.blocked.Add(jobToDisk);
            Kernel.blocked.Add(loader);
            Kernel.blocked.Add(mainProc);
            Kernel.blocked.Add(swapBack);
            Kernel.blocked.Add(interupt);
            Kernel.blocked.Add(chanDevice);
            Kernel.blocked.Add(speaker);
            Kernel.blocked.Add(printer);

        }

        public void InitSystemResources()
        {
            Resource mosEnd = new Resource(Kernel, "MOSEND", this,1, "");
            Resource outputStream = new Resource(Kernel, "OUTPUTSTREAM", this, 1, "");
            Resource supervisoryMemory = new Resource(Kernel, "SUPERVISORYMEMORY", this, 1, "");
            Resource externalMemory = new Resource(Kernel, "EXTERNALMEMORY", this, 1, "");
            Resource chanOne = new Resource(Kernel, "CHAN1", this, 1, "");
            Resource chanTwo = new Resource(Kernel, "CHAN2", this, 1, "");
            Resource chanThree = new Resource(Kernel, "CHAN3", this, 1, "");
            Resource chanFour = new Resource(Kernel, "CHAN4", this, 1, "");
            Resource userMemory = new Resource(Kernel, "USERMEMORY", this, 1, "");

            Kernel.staticResources.Add(mosEnd, false); 
            Kernel.staticResources.Add(outputStream, true);
            Kernel.staticResources.Add(supervisoryMemory, true);
            Kernel.staticResources.Add(externalMemory, true);
            Kernel.staticResources.Add(chanOne, true);
            Kernel.staticResources.Add(chanTwo, true);
            Kernel.staticResources.Add(chanThree, true);
            Kernel.staticResources.Add(chanFour, true);
            Kernel.staticResources.Add(userMemory, true);

        }



        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            switch (Pointer)
            {
                case 0:
                    InitSystemProceses();
                    InitSystemResources();
                    Pointer++;
                    Status = (int)ProcessState.Blocked;
                    Kernel.blocked.Add(this);
                    //Kernel.staticResources.Find(x => x.Name == "MOSEND").AskForResource(this);
                    Kernel.Planner();
                    break;
                case 1:
                    System.Environment.Exit(1);
                    break;

            }
        }

        public override void DecrementPriority()
        {
            throw new NotImplementedException();
        }

        public override bool CheckIfReady()
        {
            return true;
        }
    }
}
