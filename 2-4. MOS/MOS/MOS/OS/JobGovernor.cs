using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Resources;

namespace MOS.OS
{
    class JobGovernor : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public JobGovernor(Kernel kernel, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, priority, status, resources, id, pointer, "MainProc") { }

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
            Log.Info("JobGovernor process is running.");

            throw new NotImplementedException();
        }
    }
}
