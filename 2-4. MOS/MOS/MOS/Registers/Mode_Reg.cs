
namespace MOS.Registers
{
    public class Mode_Reg
    {
        private byte _mode;

        public Mode_Reg()
        {
            _mode = 1; // default - supervisor
        }

        public string Hex()
        {
            return _mode.ToString("X");
        }

        public byte Mode {
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
                }
            }
            get => _mode;
        }

    }   
}
