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
        public StartStop(Kernel kernel, int priority, string id, int status, int pointer, Resource[] resources) : base(kernel, priority,status,resources,id,pointer){}

        public void run() {
            InitSystemProceses();
            InitStaticResources();
        }

        public void InitSystemProceses() //nezinojau ka prie to poiterio rasyt tai 0 parasiau
        {
            Read read = new Read(Kernel, 100, "READ", (int)ProcessState.Blocked, 0, null);
            JCL jcl = new JCL(Kernel, 100,"JCL",(int)ProcessState.Blocked,0,null);
            JobToDisk jobToDisk = new JobToDisk(Kernel, 100,"JOBTODISK", (int)ProcessState.Blocked, 0,null);
            Loader loader = new Loader(Kernel, 100,"LOADER", (int)ProcessState.Blocked, 0, null);
            MainProc mainProc = new MainProc(Kernel, 100,"MAINPROC", (int)ProcessState.Blocked, 0, null);
            SwapBack swapBack = new SwapBack(Kernel, 100, "SWAPBACK", (int)ProcessState.Blocked, 0, null);
            Interupt interupt = new Interupt(Kernel, 100, "INTERUPT", (int)ProcessState.Blocked, 0, null);
            ChanDevice chanDevice = new ChanDevice(Kernel, 100, "CHANDEVICE", (int)ProcessState.Blocked, 0, null);
            Speaker speaker = new Speaker(Kernel, 100,"SPEAKER", (int)ProcessState.Blocked, 0, null);
            Printer printer = new Printer(Kernel, 100, "PRINTER", (int)ProcessState.Blocked, 0, null);

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

        public void InitStaticResources()
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

            Kernel.staticResources.Add(mosEnd);
            Kernel.staticResources.Add(outputStream);
            Kernel.staticResources.Add(supervisoryMemory);
            Kernel.staticResources.Add(externalMemory);
            Kernel.staticResources.Add(chanOne);
            Kernel.staticResources.Add(chanTwo);
            Kernel.staticResources.Add(chanThree);
            Kernel.staticResources.Add(chanFour);
            Kernel.staticResources.Add(userMemory);

        }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
