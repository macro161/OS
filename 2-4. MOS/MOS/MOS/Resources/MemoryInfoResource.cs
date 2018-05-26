using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class MemoryInfoResource : Resource
    {
        public MemoryInfoResource(Kernel kernel, string name, Process creator) : base(kernel, name, creator)
        {
        }

        List<MemoryInfoResourceElement> resourceElements = new List<MemoryInfoResourceElement>();
    }
}
