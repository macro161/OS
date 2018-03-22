using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS
{
    public class R_Reg
    {
        private string _r;

        public R_Reg()
        {
            _r = "0000";
        }

        public string R
        {
            get { return _r; }
            set { _r = value; }
        }

    }
}
