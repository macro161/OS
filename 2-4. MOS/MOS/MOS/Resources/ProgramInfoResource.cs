using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    public class ProgramInfoResource : Resource
    {
        public new List<ProgramInfoResourceElement> Elements { get; set; }
        public ProgramInfoResource(Kernel kernel, string name, Process creator) : base(kernel, name, creator)
        {
            Elements = new List<ProgramInfoResourceElement>();
        }
    }
}
