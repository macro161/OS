using MOS.Enums;
using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class StartStop : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public StartStop(Kernel kernel, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, priority, status, resources, id, pointer, "StartStop") {

            ResourcesINeed[0] = "MOSEND";
        }

        public void InitSystemProceses() //nezinojau ka prie to poiterio rasyt tai 0 parasiau
        {
            Read read = new Read(Kernel, 100, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            JCL jcl = new JCL(Kernel, 99, (int)ProcessState.Ready, Guid.NewGuid(), 0,null);
            JobToDisk jobToDisk = new JobToDisk(Kernel, 98, (int)ProcessState.Ready, Guid.NewGuid(), 0,null);
            Loader loader = new Loader(Kernel, 97, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            MainProc mainProc = new MainProc(Kernel, 96, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            SwapBack swapBack = new SwapBack(Kernel, 95, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            Interupt interupt = new Interupt(Kernel, 90, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            ChanDevice chanDevice = new ChanDevice(Kernel, 90, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            Speaker speaker = new Speaker(Kernel, 90, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            Printer printer = new Printer(Kernel, 90, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);

            Kernel.ready.Add(read);
            Kernel.ready.Add(jcl);
            Kernel.ready.Add(jobToDisk);
            Kernel.ready.Add(loader);
            Kernel.ready.Add(mainProc);
            Kernel.ready.Add(swapBack);
            Kernel.ready.Add(interupt);
            Kernel.ready.Add(chanDevice);
            Kernel.ready.Add(speaker);
            Kernel.ready.Add(printer);

        }

        public void InitSystemResources()
        {
            Resource mosEnd = new Resource(Kernel, "MOSEND", this);
            Resource outputStream = new Resource(Kernel, "OUTPUTSTREAM", this);
            Resource supervisoryMemory = new Resource(Kernel, "SUPERVISORYMEMORY", this);
            Resource externalMemory = new Resource(Kernel, "EXTERNALMEMORY", this);
            Resource chanOne = new Resource(Kernel, "CHAN1", this);
            Resource chanTwo = new Resource(Kernel, "CHAN2", this);
            Resource chanThree = new Resource(Kernel, "CHAN3", this);
            Resource chanFour = new Resource(Kernel, "CHAN4", this);
            Resource userMemory = new MemoryResource(Kernel, "USERMEMORY", this);

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
            Log.Info("StartStop process is running.");
            switch (Pointer)
            {
                case 0:
                    Log.Info("Initializing system resources and processes.");
                    InitSystemProceses();
                    InitSystemResources();
                    Pointer++;
                    Status = (int)ProcessState.Blocked;
                    Kernel.blocked.Add(this);
                    Kernel.Planner();
                    break;
                case 1:
                    Log.Info("");
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
