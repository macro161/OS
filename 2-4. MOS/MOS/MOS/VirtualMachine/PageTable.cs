using System;

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
            return Int32.Parse(RealMachine.RealMachine.memory.StringAt(getPtr(), x));
        }
    }
}
