﻿
namespace Registers
{
    class Mode_Reg
    {
        private static byte _mode;

        public Mode_Reg()
        {
            _mode = 1; // default - supervisor
        }

        public static byte Mode {
            set
            {
                switch (value)
                {
                    case 1:       
                        _mode = 1;
                        break;
                    case 0:
                        _mode = 0; // user
                        break;
                    default:
                        break;
                }
            }
            get
            {
                return _mode;
            }
        }

    }   
}
