using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS;


namespace MOS
{
    public class IC_Reg
    {
        private ushort _ic;

        public IC_Reg()
        {
            _ic = 0;
        }

        public ushort IC
        {
            set { _ic = value; }
            get { return _ic; }
        }

        public void Clean_IC()
        {
            _ic = 0;
        }
    }
}
