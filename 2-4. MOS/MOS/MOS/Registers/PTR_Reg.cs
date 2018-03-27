namespace MOS.Registers
{
    public class PTR_Reg
    {
        private string _ptr;

        public string PTR { get => _ptr; set => _ptr = value; }

        public void Clear()
        {
            _ptr = "";
        }

    }
}
