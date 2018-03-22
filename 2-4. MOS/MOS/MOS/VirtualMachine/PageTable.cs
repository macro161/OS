using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.VirtualMachine
{
    public class PageTable
    {
        string ptr;

        public PageTable(string ptr)
        {
            this.ptr = ptr;
        }

        public int getPtr()
        {
                return ptr.TwoLastSymbolsToHex();

        }

        public int RealAddress(int x)
        {
            return Int32.Parse(RealMachine.memory.StringAt(getPtr(), x));
        }
    }
}
