using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    public class CS_Reg
    {
        public int _cs;

        public CS_Reg(int adress)
        {
            _cs = adress;
        }

        public int DS
        {
            get { return _cs; }
        }
    }
}
