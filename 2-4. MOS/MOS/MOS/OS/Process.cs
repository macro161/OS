﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;
using MOS.RealMachine;
using MOS.Resources;

namespace MOS.OS
{
    public abstract class Process
    {
        Process process;

        public int Priority { get; set; }
        public int Status { get; set; }
        public List<Resource> Resources = new List<Resource>();
        public Process Father { get; set; }
        public Guid Id { get; private set; }
        public int Pointer { get; set; }
        public Kernel Kernel { get; private set; }
        public List<Process> Childrens { get; set; }
        public string Name { get; private set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Process(Kernel kernel, Process father, int priority, int status, List<Resource> resources, Guid id, int pointer, string name)
        {
            Kernel = kernel;
            Priority = priority;
            Status = status;
            Resources = resources;
            Id = id;
            Pointer = pointer;
            Childrens = new List<Process>();
            Name = name;
            Father = father;
        }

        public abstract void Run();

        public abstract void DecrementPriority();

        public bool CheckIfReady()
        {
            if (Status == (int)ProcessState.Ready)
                return true;
            return false;
        }

        public void DeleteProcess()
        {
            Log.Info("Deleting " + Name + " Process");
            foreach (Resource resource in Resources)
            {
                if (Kernel.staticResources.ContainsKey(resource))
                {
                    Kernel.staticResources[resource] = true;
                }
            }

            if (Status == (int)ProcessState.Ready)
            {
                Kernel.ready.Remove(this);
            }
            else if (Status == (int)ProcessState.Blocked)
            {
                Kernel.blocked.Remove(this);
            }


            foreach (Process childProcess in Childrens)
            {
                if (childProcess.Status == (int)ProcessState.Blocked)
                {
                    Kernel.blocked.Remove(childProcess);
                }
                else if(Status == (int)ProcessState.Blocked)
                {
                    Kernel.ready.Remove(childProcess);
                }
                childProcess.DeleteProcess();
            }
        }

        public void CreateProcess(Kernel kernel, Process father, int priority, int status, List<Resource> resources, int pointer, string name)
        {
            process.Kernel = kernel;
            process.Priority = priority;
            process.Status = status;
            process.Resources = resources;
            process.Id = Guid.NewGuid();
            process.Pointer = pointer;
            process.Childrens = new List<Process>();
            process.Name = name;
            process.Father = father;

            kernel.ready.Add(process);
            father.Childrens.Add(process);
            foreach (Process child in Childrens)
            {
                child.Father = process;
            }
        }

        public void StopProcess() {
            if (Status == (int)ProcessState.Blocked)
            {
                Status = (int)ProcessState.BlockedStopped;
            }

            if (Status == (int)ProcessState.Ready)
            {
                Status = (int)ProcessState.ReadyStopped;
            }


        }

        public void ActivateProcess()
        {
            if (Status == (int)ProcessState.BlockedStopped)
            {
                Status = (int)ProcessState.Blocked;
            }

            if (Status == (int)ProcessState.ReadyStopped)
            {
                Status = (int)ProcessState.Ready;
            }
        }
    }
}