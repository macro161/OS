namespace MOS.Registers
{
    public class PI_Reg
    {
        public short _pi;

        public PI_Reg()
        {
            _pi = 0;
        }

        public void Int_1()
        {
            _pi = 1;
        }

        public void Clear()
        {
            _pi = 0;
        }

        public string Hex()
        {
            return _pi.ToString("X");
        }
    }
}
