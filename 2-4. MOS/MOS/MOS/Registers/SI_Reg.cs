namespace MOS.Registers
{
    public class SI_Reg
    {
        public short SI { get; set; }

        public SI_Reg()
        {
            SI = 0;
        }

        public void Int_1() //Add more in future
        {
            SI = 1;
        }

        public void Clear()
        {
            SI = 0;
        }

        public string Hex()
        {
            return SI.ToString("X");
        }
    }
}
