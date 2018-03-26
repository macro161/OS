namespace MOS.Registers
{
    public class IC_Reg
    {
        private ushort _ic;

        public IC_Reg()
        {
            _ic = 0x80;
        }

        public ushort IC
        {
            set => _ic = value;
            get => _ic;
        }

        public int GetX()
        {
            return _ic / 16;
        }

        public int GetY()
        {
            return _ic % 16;
        }

        public void Clear()
        {
            _ic = 0;
        }
        public void Increase()
        {
            IC++;
        }
        public string Hex()
        {
            return _ic.ToString("X");
        }
    }
}
