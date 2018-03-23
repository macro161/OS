﻿namespace MOS.RealMachine
{
    public class RealMachine
    {
        public C_Reg c = new C_Reg();
        public Ch_Regs ch = new Ch_Regs();
        public CS_Reg cs = new CS_Reg(1234);
        public DS_Reg ds = new DS_Reg(1234);
        public IC_Reg ic = new IC_Reg();
        public IOI_Reg ioi = new IOI_Reg();
        public Mode_Reg mode = new Mode_Reg();
        public PI_Reg pi = new PI_Reg();
        public R_Reg r1 = new R_Reg();
        public R_Reg r2 = new R_Reg();
        public R_Reg r3 = new R_Reg();
        public R_Reg r4 = new R_Reg();
        public SF_Reg sf = new SF_Reg();
        public SI_Reg si = new SI_Reg();
        public TI_Reg ti = new TI_Reg();
        string ptr;
        public static UserMemory memory = new UserMemory();
        VirtualMachine.VirtualMachine vm;

        public RealMachine()
        {
            this.ptr = memory.getMemory();
            vm = new VirtualMachine.VirtualMachine(ptr);
        }

        //public InputChannel channleOne = new InputChannel();

        public void Start()
        {
            
        }

        public void InsertFlash()
        {
           // channleOne.GetData(memory);
        }

        public void LoadProgram()
        {
            
        }


        

        







    }
