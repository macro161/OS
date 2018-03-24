﻿namespace MOS.Registers
{
    public class SF_Reg
    {
        private byte _sf; // CF ZF SF IF OF XXX

        public SF_Reg()
        {
            _sf = 0;
        }

        public void Clear_Reg()
        {
            _sf = 0;
        }

        public void Set_CF()
        {
            _sf = (byte)(_sf | 128);
        }

        public void Set_ZF()
        {
            _sf = (byte)(_sf | 64);
        }

        public void Set_SF()
        {
            _sf = (byte)(_sf | 32);
        }

        public void Set_IF()
        {
            _sf = (byte)(_sf | 16);
        }

        public void Set_OF()
        {
            _sf = (byte)(_sf | 8);
        }


        public byte Get_SF()
        {
            return _sf;
        }
    }
}
