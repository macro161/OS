using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    class CS_Reg
    {
        private int _ds;

        public CS_Reg(int adress)
        {
            _ds = adress;
        }

        public int DS
        {
            get { return _ds; }
        }
    }
}
