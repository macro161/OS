using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class ChanDevice : Process
    {
        public ChanDevice(Kernel kernel, int priority, int status, Guid id, int pointer, Resource[] resources) : base(kernel, priority, status, resources, id, pointer, "ChanDevice") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void DecrementPriority()
        {
           
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
