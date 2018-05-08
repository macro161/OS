using System;

namespace MOS.Registers
{
    public class SF_Reg // CF ZF SF IF OF XXX
    {
        public byte SF { get; set; }

        public SF_Reg()
        {
            SF = 0;
        }

        public void Flip_CF()
        {
            SF ^= 128;
        }

        public void Flip_ZF()
        {
            SF ^= 64;
        }

        public void Flip_SF()
        {
            SF ^= 32;
        }

        public void Flip_IF()
        {
            SF ^= 16;
        }

        public void Flip_OF()
        {
            SF ^= 16;
        }

        public bool Get_CF()
        {
            return (SF & (1 << 7)) != 0;
        }

        public bool Get_ZF()
        {
            return (SF & (1 << 6)) != 0;
        }

        public bool Get_SF()
        {
            return (SF & (1 << 5)) != 0;
        }

        public bool Get_IF()
        {
            return (SF & (1 << 4)) != 0;
        }

        public bool Get_OF()
        {
            return (SF & (1 << 3)) != 0;
        }


    }
}
