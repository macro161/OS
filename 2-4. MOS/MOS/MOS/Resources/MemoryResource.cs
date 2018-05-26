using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class MemoryResource : Resource
    {
        public MemoryResource(Kernel kernel, string name, Process creator) : base(kernel, name, creator)
        {
            FreeElements = 0x254;
        }

        List<MemoryResourceElement> resourceElements = new List<MemoryResourceElement>();

        public int ElementAmount { get; set; }

        public int FreeElements { get; set; }
    }

    class MemoryResourceElement
    {
        public int Takelis { get; set; }
    }
}
