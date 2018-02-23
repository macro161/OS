using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    class R_Reg
    {
        private int _r;

        public R_Reg()
        {
            _r = 0;
        }

        public int R
        {
            get { return _r; }
            set { _r = value; }
        }

        public void Increment_R()
        {
            _r++;
        }

        public void Decrement_R()
        {
            _r--;
        }
    }
}
