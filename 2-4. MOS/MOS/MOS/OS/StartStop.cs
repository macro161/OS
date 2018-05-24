using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class StartStop
    {

        public static string READY = "READY";
        public static string RUNNING = "RUNNING";
        public static string BLOCKED = "BLOCKED";
        public int priority;
        public string status = READY;
        public Resource[] resources;
        public string id;
        public int pointer;

        public StartStop(int priority, string id, string status, int pointer, Resource[] res)
        {
            this.priority = priority;
            this.id = id;
            this.status = status;
            this.pointer = pointer;
            this.resources = res;
            this.pointer = pointer;
        }

        public void run() {
            InitSystemProceses();
            InitStaticResources();
        }

        public void InitSystemProceses() //nezinojau ka prie to poiterio rasyt tai 0 parasiau
        {
            Read read = new Read(100,"READ","BLOCKED",0,null);
            JCL jcl = new JCL(100,"JCL","BLOCKED",0,null);
            JobToDisk jobToDisk = new JobToDisk(100,"JOBTODISK", "BLOCKED", 0,null);
            Loader loader = new Loader(100,"LOADER", "BLOCKED", 0, null);
            MainProc mainProc = new MainProc(100,"MAINPROC", "BLOCKED", 0, null);
            SwapBack swapBack = new SwapBack(100, "SWAPBACK", "BLOCKED", 0, null);
            Interupt interupt = new Interupt(100, "INTERUPT", "BLOCKED", 0, null);
            ChanDevice chanDevice = new ChanDevice(100, "CHANDEVICE", "BLOCKED", 0, null);
            Speaker speaker = new Speaker(100,"SPEAKER", "BLOCKED", 0, null);
            Printer printer = new Printer(100, "PRINTER", "BLOCKED", 0, null);

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
            Resource mosEnd = new Resource("MOSEND",id,1);
            Resource outputStream = new Resource("OUTPUTSTREAM",id,1);
            Resource supervisoryMemory = new Resource("SUPERVISORYMEMORY", id, 1);
            Resource externalMemory = new Resource("EXTERNALMEMORY", id, 1);
            Resource chanOne = new Resource("CHAN1", id, 1);
            Resource chanTwo = new Resource("CHAN2", id, 1);
            Resource chanThree = new Resource("CHAN3", id, 1);
            Resource chanFour = new Resource("CHAN4", id, 1);
            Resource userMemory = new Resource("USERMEMORY", id, 1);

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
    }
}
