using System;

namespace MOS.RealMachine
{
    static class Speaker
    {
        public static void Beep(int x)
        {
            Console.Beep(2000, x * 1000);
        }
    }
}
