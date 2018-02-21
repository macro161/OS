﻿using System;

namespace Registers
{
    // 0 - laisvas, 1 - uzimtas
    class Ch_regs
    {
        private static bool _ch1;
        private static bool _ch2;
        private static bool _ch3;

        public Ch_regs()
        {
            _ch1 = 0;
            _ch2 = 0;
            _ch3 = 0;
        }

        public static Boolean CH1
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
        public static Boolean CH2
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
        public static Boolean CH3
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
