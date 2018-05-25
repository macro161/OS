using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class MemoryResource : Resource
    {
        public MemoryResource(Kernel kernel, string name, Process creator, int count, string data) : base(kernel, name, creator, count, data)
        {
        }

        List<MemoryResourceElement> resourceElements = new List<MemoryResourceElement>();

        public int ElementAmount { get; set; }

        public int FreeElements { get; set; }
    }

    class MemoryResourceElement {
        public int Takelis { get; set; }
    }
}
