using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class MainProc : Process
    {
        public MainProc(Kernel kernel, int priority, string id, int status, int pointer, Resource[] resources) : base(kernel, priority, status, resources, id, pointer) { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
