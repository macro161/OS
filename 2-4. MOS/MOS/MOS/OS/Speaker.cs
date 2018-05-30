using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;

namespace MOS.OS
{
    public class Speaker : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Speaker(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "Read") { }


        public override void DecrementPriority()
        {
        }

        public override void Run()
        {
            Log.Info("Speaker process is running.");
            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    AskForResource("BEEPER");

                    break;
                case 1:
                    Pointer = 2;
                    AskForResource("CHAN1");

                    break;
                case 2:

                    Beep(Pointer);
                    Pointer = 0;
                    ReleaseResource("CHAN1");
                    break;
            }

            
        }
        public static void Beep(int x)
        {
            Console.Beep(2000, x * 1000);
        }
    }
}