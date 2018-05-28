using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.GUI;
using MOS.OS;
using MOS.Resources;

namespace MOS.Resources
{
     public  class IOResource : Resource
    {
        public new List<IOResourceElements> Elements { get; set; }
        public IOResource(Kernel kernel, string name, Process creator) : base(kernel, name, creator)
        {
            Elements = new List<IOResourceElements>();
        }

        public void ReleaseResource(IOResourceElements resElement)
        {
            Elements.Add(resElement);
            Kernel.ResourcePlanner();
        }

    }
}
