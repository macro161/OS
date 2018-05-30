using MOS.Enums;
using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    public class StartStop : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public StartStop(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "StartStop")
        {

        }

        public void InitSystemProcesesAndResources() //nezinojau ka prie to poiterio rasyt tai 0 parasiau
        {
            Read read = new Read(Kernel, this, 99, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            JCL jcl = new JCL(Kernel, this, 98, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            JobToDisk jobToDisk = new JobToDisk(Kernel, this, 97, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            Loader loader = new Loader(Kernel, this, 96, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            MainProc mainProc = new MainProc(Kernel, this, 95, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            Interupt interupt = new Interupt(Kernel, this, 94, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            Speaker speaker = new Speaker(Kernel, this, 93, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            Printer printer = new Printer(Kernel, this, 92, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());

            Kernel.ready.Add(read);
            Kernel.ready.Add(jcl);
            Kernel.ready.Add(jobToDisk);
            Kernel.ready.Add(loader);
            Kernel.ready.Add(mainProc);
            Kernel.ready.Add(interupt);
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
            mosEnd.CreateStaticResource();
            outputStream.CreateStaticResource();
            supervisoryMemory.CreateStaticResource();
            externalMemory.CreateStaticResource();
            chanOne.CreateStaticResource();
            chanTwo.CreateStaticResource();
            chanThree.CreateStaticResource();
            chanFour.CreateStaticResource();
            userMemory.CreateStaticResource();

            //Dinaminių resursų sukūrimas
            Resource filePath = new Resource(Kernel, "FILEINPUT", this);
            Resource taskInSupervisory = new ProgramInfoResource(Kernel, "TASKINSUPERVISORY", read);
            Resource taskNameInSupervisory = new ProgramInfoResource(Kernel, "TASKNAMEINSUPERVISORY", jcl);
            Resource taskDataInSupervisory = new ProgramInfoResource(Kernel, "TASKDATAINSUPERVISORY", jcl);
            Resource taskCodeInSupervisory = new ProgramInfoResource(Kernel, "TASKCODEINSUPERVISORY", jcl);
            Resource taskInDisk = new Resource(Kernel, "TASKINDISK", jobToDisk);
            Resource loaderPacket = new MemoryInfoResource(Kernel, "LOADERPACKET", this);
            Resource fromLoader = new Resource(Kernel, "FROMLOADER", loader);
            Resource fromInterrupt = new Resource(Kernel, "FROMINTERUPT", interupt);
            Resource interrupt = new InterruptResource(Kernel, "INTERUPT", this);
            Resource lineInMemory = new IOResource(Kernel, "LINEINMEMORY", this);
            Resource lineFromUser = new Resource(Kernel, "LINEFROMUSER", this);
            Resource beep = new Resource(Kernel,"BEEPER", this);

            //Dinaminių resursų pridėjimas prie sąrašo.
            filePath.CreateDynamicResource();
            taskInSupervisory.CreateDynamicResource();
            taskNameInSupervisory.CreateDynamicResource();
            taskDataInSupervisory.CreateDynamicResource();
            taskCodeInSupervisory.CreateDynamicResource();
            taskInDisk.CreateDynamicResource();
            loaderPacket.CreateDynamicResource();
            fromLoader.CreateDynamicResource();
            fromInterrupt.CreateDynamicResource();
            interrupt.CreateDynamicResource();
            lineInMemory.CreateDynamicResource();
            lineFromUser.CreateDynamicResource();
            beep.CreateDynamicResource();

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
                    AskForResource("MOSEND");
                    break;
                case 1:
                    Log.Info("Exiting");
                    this.DeleteProcess();
                    Environment.Exit(1);
                    break;
            }
        }

        public override void DecrementPriority()
        {
        }
    }
}
