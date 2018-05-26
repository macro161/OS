﻿using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Resources
{
    class MemoryInfoResourceElement
    {
        public Process Receiver { get; set; }
        public Process Sender { get; set; }
        public string Value { get; set; }
        public int Ptr  { get; set; }

        public MemoryInfoResourceElement(int ptr,  string value, Process receiver = null, Process sender = null)
        {
            Ptr = ptr;
            Receiver = receiver;
            Sender = sender;
            Value = value;
        }
    }
}
