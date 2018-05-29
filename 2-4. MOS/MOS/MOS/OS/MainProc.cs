﻿using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;

namespace MOS.OS
{
     public class MainProc : Process
    {
        public ResourceElement Element { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainProc(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "MainProc") { }

        public override void DecrementPriority()
        {
        }

        public override void Run()
        {
            Log.Info("Main process is running.");
            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    Kernel.dynamicResources.First(res => res.Name == "TASKINDISK").AskForResource(this);
                    break;
                case 1:
                    if (!(Element.Value == "0"))
                    {
                        JobGovernor jg = new JobGovernor(Kernel, this, 80, (int)ProcessState.Ready, Guid.NewGuid(), 0, new List<Resource>(Resources.Where(res => res.Name == "TASKINDISK")));
                        Kernel.ready.Add(jg);
                        Childrens.Add(jg);
                        jg.TaskInDiskElement = Element;
                        Log.Info("Creating Job Governor.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Element.Sender.DeleteProcess();
                    }
                    Kernel.dynamicResources.First(res => res.Name == "TASKINDISK").AskForResource(this);
                    break;
            }
        }
    }
}
