using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    public class MemoryInfoResource : Resource
    {
        public MemoryInfoResource(Kernel kernel, string name, Process creator) : base(kernel, name, creator)
        {
            Elements = new List<MemoryInfoResourceElement>();
        }
        public new List<MemoryInfoResourceElement> Elements { get; set; }
    }
}
