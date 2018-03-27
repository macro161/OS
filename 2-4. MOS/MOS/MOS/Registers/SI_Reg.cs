namespace MOS.Registers
{
    public class SI_Reg
    {
        private short _si;

        public short SI { get => _si; set => _si = value; }

        public SI_Reg()
        {
            _si = 0;
        }

        public void Int_1() //Add more in future
        {
            _si = 1;
        }

        public void Clear()
        {
            _si = 0;
        }

        public string Hex()
        {
            return _si.ToString("X");
        }
    }
}
