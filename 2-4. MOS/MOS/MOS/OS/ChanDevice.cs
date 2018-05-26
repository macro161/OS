using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class ChanDevice : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ChanDevice(Kernel kernel, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, priority, status, resources, id, pointer, "ChanDevice") { }

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
           
        }

        public override void Run()
        {
            Log.Info("Channel device is running.");
        }
    }
}
