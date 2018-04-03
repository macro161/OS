namespace MOS.Registers
{
    public class SF_Reg
    {
        public byte SF { get; set; }

        public SF_Reg()
        {
            SF = 0;
        }

        public byte Return_Status_Flag()
        {
            return SF;
        }

        public void Clear_Reg()
        {
            SF = 0;
        }

        public void Set_CF()
        {
            SF = (byte)(SF | 128);
        }

        public void Set_ZF()
        {
            SF = (byte)(SF | 64);
        }

        public void Set_SF()
        {
            SF = (byte)(SF | 32);
        }

        public void Set_OF()
        {
            SF = (byte)(SF | 16);
        }

        public void Set_IF()
        {
            SF = (byte)(SF | 8);
        }

        public bool Get_OF()// CF ZF SF IF OF XXX
        {
            return (SF & (1 << 4)) != 0;
        }

        public bool Get_IF()
        {
            return (SF & (1 << 5)) != 0; //Original mato Savickio kodas, tikrai nevogtas is Stack overflow  https://stackoverflow.com/questions/2431732/checking-if-a-bit-is-set-or-not
        }

        public bool Get_SF()
        {
            return (SF & (1 << 6)) != 0;
        }

        public bool Get_ZF()
        {
            return (SF & (1 << 7)) != 0;
        }

        public bool Get_CF()
        {
            return (SF & (1 << 8)) != 0;
        }
    }
}
