using System;
using MOS.Registers;

namespace MOS.VirtualMachine
{
    class VirtualMachine
    {
        string ptr;
        PageTable pt;
        IC_Reg IC;
        C_Reg C;
        R_Reg R1, R2, R3, R4;
        SF_Reg sf;

        public VirtualMachine(string ptr)
        {
            this.ptr = ptr;
            pt = new PageTable(ptr);
            IC = new IC_Reg();
            C = new C_Reg();
            R1 = new R_Reg();
            R2 = new R_Reg();
            R3 = new R_Reg();
            R4 = new R_Reg();
            sf = new SF_Reg();
        }

        private void DoTask(String com)
        {
            string c = com.Substring(0, 1);

            string x1x2 = com.Substring(2, 3); ;




            if (com == "HALT")
            {
                //DO SOMETHING
            }

            switch (com.Substring(0, 2))
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

        private void lm(string x1x2)
        {
            throw new NotImplementedException();
        }

        //dabar bus komandoms apdoroti skirti metodai.
        private void lr(string x1x2)
        {

            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(2, 3).ToHex();
            R1.R = RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
        }

        private void sr(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(2, 3).ToHex();
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
            int x2 = x1x2.Substring(2, 3).ToHex();
            R1.R = R1.R + RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
        }

        private void sb(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(2, 3).ToHex();
            R1.R = R1.R - RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
        }

        private void cr(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(2, 3).ToHex();
            C.C = R1.Hex() == RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2);
        }

        private void mu(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(2, 3).ToHex();
            R1.R = R1.R * RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
        }

        private void di(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(2, 3).ToHex();
            R1.R = R1.R / RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
            R2.R = R2.R % RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
        }

        private void ju(string x1x2)
        {
            IC.IC = (ushort)x1x2.ToHex();
        }

        private void jg(string x1x2)
        {

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

        private void sm(string x1x2)
        {

        }
    }
}
