namespace MOS.Registers
{
    public class PTR_Reg
    {
        private int _ptr;

        public PTR_Reg(int adress)
        {
            _ptr = adress;
        }

        public int PTR => _ptr;

        public string Hex()
        {
            return _ptr.ToString("X");
        }

        public void Clear()
        {
            _ptr = 0;
        }

    }
}
