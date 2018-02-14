using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    class SF_Reg
    {
        private byte _sf; // CF OF XX XXX ZF

        public SF_Reg()
        {
            _sf = 0;
        }

        public void Clear_Reg()
        {
            _sf = 0;
        }

        public void Set_CF()
        {
            _sf = (byte)(_sf | 128);
        }

        public void Set_OF()
        {
            _sf = (byte)(_sf | 64);
        }

        public void Set_ZF()
        {
            _sf = (byte)(_sf | 1);
        }

        public byte Get_SF()
        {
            return _sf;
        }
        


    }
}
