﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Read : Process
    {
        public Read(int priority, string id, string status, int pointer, Resource[] res) : base(priority, id, status, pointer, res) { }
    }
}
