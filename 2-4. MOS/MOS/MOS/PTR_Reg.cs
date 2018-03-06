using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS
{
    public class PTR_Reg
    {
        private int _ptr;

        public PTR_Reg(int adress)
        {
            _ptr = adress;
        }

        public int PTR
        {
            get { return _ptr; }
        }

       

    }
}
