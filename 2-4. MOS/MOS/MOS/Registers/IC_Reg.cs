namespace MOS.Registers
{
    public class IC_Reg
    {
        public IC_Reg()
        {
            IC = 0x80;
        }

        public ushort IC { set; get; }

        public int GetX()
        {
            return IC / 16;
        }

        public int GetY()
        {
            return IC % 16;
        }

        public void Clear()
        {
            IC = 0x80;
        }
        public void Increase()
        {
            IC++;
        }
        public string Hex()
        {
            return IC.ToString("X");
        }
    }
}
