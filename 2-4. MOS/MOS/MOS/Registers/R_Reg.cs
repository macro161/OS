namespace MOS.Registers
{
    public class R_Reg
    {
        private int _r;

        public R_Reg()
        {
            _r = 0;
        }

        public int R
        {
            get => _r;
            set => _r = value;
        }

        public string Hex()
        {
            return _r.ToString("X");
        }

    }
}
