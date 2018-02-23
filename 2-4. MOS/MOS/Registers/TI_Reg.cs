
namespace Registers
// jei TI = 0, issaukiamas pertraukimas
// TI galima nustatyti/pakeisti supervisor rezime
{
    class TI_Reg
    {
        private static ushort _ti;

        public TI_Reg()
        {
            _ti = 10;
        }

        public static ushort TI
        {
            set
            {
                if(Mode_Reg.Mode == 'S')
                    _ti = value;
            }
            get
            {
                return _ti;
            }
        }

        public void DecrementTI()
        {
            _ti--;
        }
    }
}