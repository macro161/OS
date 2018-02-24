
namespace Registers
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
            set
            {
                //if(Mode_Reg.Mode == 'S')
                  //  _ti = value;
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