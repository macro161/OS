using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    class PI_Reg
    {
        private short _pi;

        public PI_Reg()
        {
            _pi = 0;
        }

        public void Int_1()
        {
            _pi = 1;
        }
    }
}
