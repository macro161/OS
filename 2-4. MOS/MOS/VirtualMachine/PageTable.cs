using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMachine
{
    class PageTable
    {
        int Ptr;
        public PageTable(int Ptr)
        {
            this.Ptr = Ptr;
        }
    }
}
