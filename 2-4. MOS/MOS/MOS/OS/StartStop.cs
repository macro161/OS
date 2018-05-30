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

        public StartStop(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) 
        {

        }

        public void InitSystemProcesesAndResources() //nezinojau ka prie to poiterio rasyt tai 0 parasiau
        {
            Read read = new Read() ;// = new Read(Kernel, this, 99, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            JCL jcl = new JCL(); ;// = new JCL(Kernel, this, 98, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            JobToDisk jobToDisk = new JobToDisk();// = new JobToDisk(Kernel, this, 97, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            Loader loader = new Loader();// = new Loader(Kernel, this, 96, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            MainProc mainProc = new MainProc();// = new MainProc(Kernel, this, 95, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            Interupt interupt = new Interupt();// = new Interupt(Kernel, this, 94, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            Speaker speaker = new Speaker();// = new Speaker(Kernel, this, 93, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());
            Printer printer = new Printer();// = new Printer(Kernel, this, 92, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>());

            read.CreateProcess(Kernel,this,99, (int)ProcessState.Ready, new List<Resource>(), 0);
            jcl.CreateProcess(Kernel, this, 98, (int)ProcessState.Ready, new List<Resource>(), 0);
            jobToDisk.CreateProcess(Kernel, this, 97, (int)ProcessState.Ready, new List<Resource>(), 0);
            loader.CreateProcess(Kernel, this, 96, (int)ProcessState.Ready, new List<Resource>(), 0);
            mainProc.CreateProcess(Kernel, this, 95, (int)ProcessState.Ready, new List<Resource>(), 0);
            interupt.CreateProcess(Kernel, this, 94, (int)ProcessState.Ready, new List<Resource>(), 0);
            speaker.CreateProcess(Kernel, this, 93, (int)ProcessState.Ready, new List<Resource>(), 0);
            printer.CreateProcess(Kernel, this, 92, (int)ProcessState.Ready, new List<Resource>(), 0);

            //Statinių resursų sukūrimas
            Resource mosEnd = new Resource();// = new Resource(Kernel, "MOSEND", this);
            Resource outputStream = new Resource();// = new Resource(Kernel, "OUTPUTSTREAM", this);
            Resource supervisoryMemory = new Resource();// = new Resource(Kernel, "SUPERVISORYMEMORY", this);
            Resource externalMemory = new Resource();// = new Resource(Kernel, "EXTERNALMEMORY", this);
            Resource chanOne = new Resource();// = new Resource(Kernel, "CHAN1", this);
            Resource chanTwo = new Resource();// = new Resource(Kernel, "CHAN2", this);
            Resource chanThree = new Resource();// = new Resource(Kernel, "CHAN3", this);
            Resource chanFour = new Resource();// = new Resource(Kernel, "CHAN4", this);
            Resource userMemory = new Resource();// = new MemoryResource(Kernel, "USERMEMORY", this);

            //Statinių resursų pridėjimas į sąrašą
            /*Kernel.staticResources.Add(mosEnd, false);
            Kernel.staticResources.Add(outputStream, true);
            Kernel.staticResources.Add(supervisoryMemory, true);
            Kernel.staticResources.Add(externalMemory, true);
            Kernel.staticResources.Add(chanOne, true);
            Kernel.staticResources.Add(chanTwo, true);
            Kernel.staticResources.Add(chanThree, true);
            Kernel.staticResources.Add(chanFour, true);
            Kernel.staticResources.Add(userMemory, true);*/

            mosEnd.CreateResource(Kernel,"MOSEND", this, true);
            outputStream.CreateResource(Kernel, "OUTPUTSTREAM", this, true);
            supervisoryMemory.CreateResource(Kernel, "SUPERVISORYMEMORY", this, true);
            externalMemory.CreateResource(Kernel, "EXTERNALMEMORY", this, true);
            chanOne.CreateResource(Kernel, "CHAN1", this, true);
            chanTwo.CreateResource(Kernel, "CHAN2", this, true);
            chanThree.CreateResource(Kernel, "CHAN3", this, true);
            chanFour.CreateResource(Kernel, "CHAN4", this, true);
            userMemory.CreateResource(Kernel, "USERMEMORY", this, true);

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
            Kernel.dynamicResources.Add(beep);

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
                    Kernel.staticResources.First(res => res.Key.Name == "MOSEND").Key.AskForResource(this);
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
