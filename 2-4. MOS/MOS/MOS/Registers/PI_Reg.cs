namespace MOS.Registers
{
    public class PI_Reg
    {
        public short PI { get; set; }

        public PI_Reg()
        {
            PI = 0;
        }

        public void Int_1()
        {
            PI = 1;
        }

        public void Clear()
        {
            PI = 0;
        }

        public string Hex()
        {
            return PI.ToString("X");
        }
    }
}
