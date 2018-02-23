using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    class SI_Reg
    {
        private short _si;

        public SI_Reg()
        {
            _si = 0;
        }

        public void Int_1() //Add more in future
        {
            _si = 1;
        }
    }
}
