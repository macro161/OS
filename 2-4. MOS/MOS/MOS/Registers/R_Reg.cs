namespace MOS.Registers
{
    public class R_Reg
    {
        public R_Reg()
        {
            R = 0;
        }

        public int R { get; set; }

        public string Hex()
        {
            return R.ToString("X");
        }
        public void Clear()
        {
            R = 0;
        }
    }
}
