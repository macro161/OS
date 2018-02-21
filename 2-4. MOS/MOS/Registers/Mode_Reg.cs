
namespace Registers
{
    class Mode_Reg
    {
        private static char _mode;

        public Mode_Reg()
        {
            _mode = 'S'; // default - supervisor
        }

        public static char Mode {
            set
            {
                switch (value)
                {
                    case 'S':
                        _mode = 'S';
                        break;
                    case 'U':
                        _mode = 'U'; // user
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
