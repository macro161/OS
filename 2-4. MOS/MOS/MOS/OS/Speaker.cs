using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Speaker : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Speaker(Kernel kernel, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, priority, status, resources, id, pointer, "Read") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override bool CheckIfReady()
        {
            throw new NotImplementedException();
        }

        public override void DecrementPriority()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            Log.Info("Speaker process is running.");
            Beep(Pointer);
        }

        public static void Beep(int x)
        {
            Console.Beep(2000, x * 1000);
        }
    }
}
