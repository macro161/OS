using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class JobToDisk : Process
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public JobToDisk(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources) : base(kernel, father, priority, status, resources, id, pointer, "JobToDisk") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }


        public override void DecrementPriority()
        {
            
        }

        public override void Run()
        {
            Log.Info("JobToDisk process is running.");
            Log.Info("Loading programs into Hard Disk.");
            HardDisk.ProgramList = SupervisoryMemory.ProgramList;
        }
    }
}
