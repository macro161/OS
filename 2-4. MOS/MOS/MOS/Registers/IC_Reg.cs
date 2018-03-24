namespace MOS.Registers
{
    public class IC_Reg
    {
        private ushort _ic;

        public IC_Reg()
        {
            _ic = 0;
        }

        public ushort IC
        {
            set => _ic = value;
            get => _ic;
        }

        public void Clear()
        {
            _ic = 0;
        }

        public string Hex()
        {
            return _ic.ToString("X");
        }
    }
}
