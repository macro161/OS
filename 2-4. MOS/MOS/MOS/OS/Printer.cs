﻿using MOS.GUI;
using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;

namespace MOS.OS
{
    class Printer : Process
    {
        public IOResourceElements Element { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Printer(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "Printer") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void DecrementPriority()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            Log.Info("Print process is running.");
            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    Status = (int)ProcessState.Blocked;
                    if (Kernel.ready.Contains(this))
                    {
                        Kernel.ready.Remove(this);
                    }
                    Kernel.blocked.Add(this);
                    Kernel.dynamicResources.First(res => res.Name == "TASKINSUPERVISORY").AskForResource(this);
                    
                    break;
                case 1:
                    Pointer = 2;
                    Status = (int)ProcessState.Blocked;
                    if (Kernel.ready.Contains(this))
                    {
                        Kernel.ready.Remove(this);
                    }
                    Kernel.blocked.Add(this);
                    Kernel.staticResources.First(res => res.Key.Name == "CHAN3").Key.AskForResource(this);

                    break;
                case 2:
                    string message = "";
                    int x = Element.MemoryByte.Substring(0,2).ToHex();
                    int y = Element.MemoryByte.Substring(2, 2).ToHex();

                    for (int i = 0; i < Element.Lenght; i++ )
                    {
                        message = message + RealMachine.RealMachine.memory.StringAt(x, y).ToHex();
                        y++;
                        if (y > 15)
                        {
                            y = 0;
                            x++;
                        }
                    }

                    Print(message);
                    break;
            }
        }

        public void Print(string line)
        {
            Element.ResourceGUI.Print(line);
        }
    }
}
