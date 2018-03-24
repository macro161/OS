namespace MOS.Registers
// jei TI = 0, issaukiamas pertraukimas
// TI galima nustatyti/pakeisti supervisor rezime
{
    public class TI_Reg
    {
        public ushort _ti;

        public TI_Reg()
        {
            _ti = 10;
        }

        public ushort TI
        {
            set => _ti = value;
            get => _ti;
        }

        public void DecrementTI()
        {
            _ti--;
        }

        public void Clear()
        {
            _ti = 0;
        }

        public string Hex()
        {
            return _ti.ToString("X");
        }

    }
}