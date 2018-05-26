﻿using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class InterruptResource : Resource
    {
        public new List<InterruptResourceElement> Elements { get; set; }
        public InterruptResource(Kernel kernel, string name, Process creator) : base(kernel, name, creator)
        {
            Elements = new List<InterruptResourceElement>();
        }
    }
}
