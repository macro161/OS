namespace MOS.Registers
{
    public class C_Reg
    {
        public C_Reg()
        {
            _c = false;
        }

        private bool _c;

        public bool C
        {
            get => _c;
            set => _c = value;
        }

        public void Clear()
        {
            _c = false;
        }

    }
}
