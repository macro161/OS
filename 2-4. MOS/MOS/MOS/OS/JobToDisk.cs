using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class JobToDisk : Process
    {
        public JobToDisk(int priority, string id, string status, int pointer, Resource[] resources) : base(priority, status, resources, id, pointer) { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override string GetId()
        {
            throw new NotImplementedException();
        }

        public override int GetPriority()
        {
            throw new NotImplementedException();
        }

        public override string GetStatus()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override void SetPriority(int priority)
        {
            throw new NotImplementedException();
        }

        public override void SetStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}
