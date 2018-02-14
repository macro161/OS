using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    class C_Reg
    {
        private bool _c;

        public bool C
        {
            get { return _c; }
            set { _c = value; }
        }

        public void Clear()
        {
            _c = false;
        }
           
    }
}
