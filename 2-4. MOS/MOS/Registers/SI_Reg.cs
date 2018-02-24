using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    public class SI_Reg
    {
        public short _si;

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
