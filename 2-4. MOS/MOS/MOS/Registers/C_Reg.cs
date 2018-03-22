using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS
{
    public class C_Reg
    {
        public C_Reg()
        {
            _c = false;
        }

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
