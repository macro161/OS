using Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMachine
{
    public class VirtualMachinem
    {
        public R_Reg ax = new R_Reg();
        public R_Reg bx = new R_Reg();
        public R_Reg dx = new R_Reg();
        public R_Reg cx = new R_Reg();
        byte[] ptr;
        int mode = 0;
        PageTable pt;
        public VirtualMachinem(byte[] ptr, int mode)

        {
            this.mode = mode;
            this.ptr = ptr;
            pt = new PageTable(ptr);
        }
    }
}
