using System;
using System.Drawing.Imaging;
using MOS.Registers;

namespace MOS.VirtualMachine
{
    class VirtualMachine
    {
        PTR_Reg PTR;
        PageTable pt;
        IC_Reg IC;
        C_Reg C;
        R_Reg R1, R2, R3, R4;
        SF_Reg SF;

        public VirtualMachine(PTR_Reg ptr, R_Reg r1, R_Reg r2, R_Reg r3, R_Reg r4, IC_Reg ic, SF_Reg sf, C_Reg c)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
            R4 = r4;
            IC = ic;
            SF = sf;
            C = c; 
            PTR = ptr;
            
            pt = new PageTable(PTR._ptr);
        }

        public void RunCode()
        {
            for (int i = 8; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    DoTask(RealMachine.RealMachine.memory.StringAt(RealMachine.RealMachine.memory.IntAt(PTR._ptr.ToHex(),i),j));
                }
            }



        }

        private void DoTask(String com)
        {
            string c = com.Substring(0, 2);

            string x1x2 = com.Substring(2, 2);

            if (com == "HALT")
            {
                //DO SOMETHING
            }

            switch (com.Substring(0, 3))
            {
                case "AND":
                    and();
                    break;
                case "XOR":
                    xor();
                    break;
                case "NOT":
                    not();
                    break;
            }

            switch (c)
            {
                case "LR":
                    lr(x1x2);
                    break;
                case "SR":
                    sr(x1x2);
                    break;
                case "RR":
                    rr();
                    break;
                case "AD":
                    ad(x1x2);
                    break;
                case "SB":
                    sb(x1x2);
                    break;
                case "CR":
                    cr(x1x2);
                    break;
                case "MU":
                    mu(x1x2);
                    break;
                case "DI":
                    di(x1x2);
                    break;
                case "JU":
                    ju(x1x2);
                    break;
                case "JG":
                    jg(x1x2);
                    break;
                case "JE":
                    je(x1x2);
                    break;
                case "JL":
                    jl(x1x2);
                    break;
                case "SM":
                    sm(x1x2);
                    break;
                case "LM":
                    lm(x1x2);
                    break;
                case "OR":
                    or();
                    break;
                case "FC":
                    fc();
                    break;
                case "FO":
                    fo(x1x2);
                    break;
                case "FR":
                    fr(x1x2);
                    break;
                case "FW":
                    fw(x1x2);
                    break;
                case "FD":
                    fd();
                    break;
            }
        }

        private void fd()
        {
            throw new NotImplementedException();
        }

        private void fw(string x1x2)
        {
            throw new NotImplementedException();
        }

        private void fr(string x1x2)
        {
            throw new NotImplementedException();
        }

        private void fo(string x1x2)
        {
            throw new NotImplementedException();
        }

        private void fc()
        {
            throw new NotImplementedException();
        }

        private void lm(string x1x2)
        {
            throw new NotImplementedException();
        }

        private void jg(string x1x2)
        {
            throw new NotImplementedException();
        }

        private void sm(string x1x2)
        {
            throw new NotImplementedException();
        }

        private void not()
        {
            R1.R = ~R1.R;
        }

        private void or()
        {
            R1.R = R1.R | R2.R;
        }

        private void xor()
        {
            R1.R = R1.R ^ R2.R;
        }

        private void and()
        {
            R1.R = R1.R + R2.R;
        }

        private void lr(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            R1.R = RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
        }

        private void sr(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            RealMachine.RealMachine.memory.WriteAt(pt.RealAddress(x1), x2, R1.Hex());
        }

        private void rr()
        {
            R1.R = R1.R + R2.R;
            R2.R = R1.R - R2.R;
            R1.R = R1.R - R2.R;
        }

        private void ad(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            R1.R = R1.R + RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
        }

        private void sb(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            R1.R = R1.R - RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
        }

        private void cr(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            C.C = R1.Hex() == RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2);
        }

        private void mu(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            R1.R = R1.R * RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
        }

        private void di(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            R1.R = R1.R / RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
            R2.R = R2.R % RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
        }

        private void ju(string x1x2)
        {
            IC.IC = (ushort)x1x2.ToHex();
        }

        private void je(string x1x2)
        {
            if (C.C)
            {
                IC.IC = (ushort)x1x2.ToHex();
            }
        }
        private void jl(string x1x2)
        {
            if (C.C)
            {
                IC.IC = (ushort)x1x2.ToHex();
            }
        }
    }
}
