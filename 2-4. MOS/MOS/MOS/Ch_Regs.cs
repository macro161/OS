using System;

namespace MOS
{
    // 0 - laisvas, 1 - uzimtas
    public class Ch_Regs
    {
        private static bool _ch1;
        private static bool _ch2;
        private static bool _ch3;

        public Ch_Regs()
        {
            _ch1 = false;
            _ch2 = false;
            _ch3 = false;
        }

        public Boolean CH1
        {
            get
            {
                return _ch1;
            }
            set
            {
                _ch1 = value;
            }
        }
        public Boolean CH2
        {
            get
            {
                return _ch2;
            }
            set
            {
                _ch2 = value;
            }
        }
        public Boolean CH3
        {
            get
            {
                return _ch3;
            }
            set
            {
                _ch3 = value;
            }
        }
    }
}
