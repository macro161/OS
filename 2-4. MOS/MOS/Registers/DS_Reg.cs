using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registers
{
    public class DS_Reg // Rodo i duometu segmento pradzia
    {
        public int _ds;

        public DS_Reg(int register)
        {
            _ds = register;
        }

        public int DS
        {
            get { return _ds; }
        }


    }
}
