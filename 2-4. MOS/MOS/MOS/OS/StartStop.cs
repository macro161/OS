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

        public StartStop(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "StartStop") {

            ResourcesINeed[0] = "MOSEND";
        }

        public void InitSystemProcesesAndResources() //nezinojau ka prie to poiterio rasyt tai 0 parasiau
        {
            Read read = new Read(Kernel, this, 100, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            JCL jcl = new JCL(Kernel, this, 99, (int)ProcessState.Ready, Guid.NewGuid(), 0,null);
            JobToDisk jobToDisk = new JobToDisk(Kernel, this, 98, (int)ProcessState.Ready, Guid.NewGuid(), 0,null);
            Loader loader = new Loader(Kernel, this, 97, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            MainProc mainProc = new MainProc(Kernel, this, 96, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            SwapBack swapBack = new SwapBack(Kernel, this, 95, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            Interupt interupt = new Interupt(Kernel, this, 90, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            ChanDevice chanDevice = new ChanDevice(Kernel, this, 90, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            Speaker speaker = new Speaker(Kernel, this, 90, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);
            Printer printer = new Printer(Kernel, this, 90, (int)ProcessState.Ready, Guid.NewGuid(), 0, null);

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

            //Statinių resursų sukūrimas
            Resource mosEnd = new Resource(Kernel, "MOSEND", this);
            Resource outputStream = new Resource(Kernel, "OUTPUTSTREAM", this);
            Resource supervisoryMemory = new Resource(Kernel, "SUPERVISORYMEMORY", this);
            Resource externalMemory = new Resource(Kernel, "EXTERNALMEMORY", this);
            Resource chanOne = new Resource(Kernel, "CHAN1", this);
            Resource chanTwo = new Resource(Kernel, "CHAN2", this);
            Resource chanThree = new Resource(Kernel, "CHAN3", this);
            Resource chanFour = new Resource(Kernel, "CHAN4", this);
            Resource userMemory = new MemoryResource(Kernel, "USERMEMORY", this);

            //Statinių resursų pridėjimas į sąrašą
            Kernel.staticResources.Add(mosEnd, false);
            Kernel.staticResources.Add(outputStream, true);
            Kernel.staticResources.Add(supervisoryMemory, true);
            Kernel.staticResources.Add(externalMemory, true);
            Kernel.staticResources.Add(chanOne, true);
            Kernel.staticResources.Add(chanTwo, true);
            Kernel.staticResources.Add(chanThree, true);
            Kernel.staticResources.Add(chanFour, true);
            Kernel.staticResources.Add(userMemory, true);

            //Dinaminių resursų sukūrimas
            Resource filePath = new Resource(Kernel, "FILEINPUT", this);
            Resource taskInSupervisory = new Resource(Kernel, "TASKINSUPERVISORY", read);
            Resource taskNameInSupervisory = new Resource(Kernel, "TASKNAMEINSUPERVISORY", jcl);
            Resource taskDataInSupervisory = new Resource(Kernel, "TASKDATAINSUPERVISORY", jcl);
            Resource taskCodeInSupervisory = new Resource(Kernel, "TASKCODEINSUPERVISORY", jcl);
            Resource taskInDisk = new Resource(Kernel, "TASKINDISK", jobToDisk);
            Resource loaderPacket = new MemoryInfoResource(Kernel, "LOADERPACKET", this);
            Resource fromLoader = new Resource(Kernel, "FROMLOADER", loader);
            Resource fromInterrupt = new Resource(Kernel, "FROMINTERRUPT", interupt);
            Resource interrupt = new InterruptResource(Kernel, "INTERUPT", this);
            Resource lineInMemory = new IOResource(Kernel, "LINEINMEMORY", this);
            Resource lineFromUser = new Resource(Kernel, "LINEFROMUSER", this);

            //Dinaminių resursų pridėjimas prie sąrašo.
            Kernel.dynamicResources.Add(filePath);
            Kernel.dynamicResources.Add(taskInSupervisory);
            Kernel.dynamicResources.Add(taskNameInSupervisory);
            Kernel.dynamicResources.Add(taskDataInSupervisory);
            Kernel.dynamicResources.Add(taskCodeInSupervisory);
            Kernel.dynamicResources.Add(taskInDisk);
            Kernel.dynamicResources.Add(loaderPacket);
            Kernel.dynamicResources.Add(fromLoader);
            Kernel.dynamicResources.Add(fromInterrupt);
            Kernel.dynamicResources.Add(interrupt);
            Kernel.dynamicResources.Add(lineInMemory);
            Kernel.dynamicResources.Add(lineFromUser);

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
                    InitSystemProcesesAndResources();
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
    }
}
