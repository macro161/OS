namespace MOS.Registers
{
    public class C_Reg
    {
        public C_Reg()
        {
            C = false;
        }

        public bool C { get; set; }

        public void Clear()
        {
            C = false;
        }

    }
}
