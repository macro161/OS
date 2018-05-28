using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    public class InterruptResource : Resource
    {
        public new List<InterruptResourceElement> Elements { get; set; }
        public InterruptResource(Kernel kernel, string name, Process creator) : base(kernel, name, creator)
        {
            Elements = new List<InterruptResourceElement>();
        }
        public void ReleaseResource(InterruptResourceElement resElement)
        {
            Elements.Add(resElement);
            Kernel.ResourcePlanner();
        }
    }
}
