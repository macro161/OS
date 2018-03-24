using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.RealMachine
{
    static class Speaker
    {
        public static void Beep()
        {
            Console.Beep(5000,1000);
        }
    }
}
