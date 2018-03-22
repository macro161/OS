using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS
{
    public class IOI_Reg
    {
        public byte _ioi;

        public IOI_Reg()
        {
            _ioi = 0;
        }

        public ushort IOI
        {
            set { 
                if(value == 1)
                {
                    _ioi = (byte)(_ioi | 1); //uzrasom i pirma bita 1 parodant kad pirmas kanalas atidarytas XXXX XXX1
                }
                if (value == 2) 
                {
                    _ioi = (byte)(_ioi | 2); //uzrasom i antra bita 1 parodant kad antra kanalas atidarytas XXXX XX1X
                }
                if (value == 3)
                {
                    _ioi = (byte)(_ioi | 4); //uzrasom i trecia bita 1 parodant kad trecias kanalas atidarytas XXXX X1XX
                }
            
            
            
            }
            get { return _ioi; }
        }

        public void Clean_IOI()
        {
            _ioi = 0;
        }
    }
}
