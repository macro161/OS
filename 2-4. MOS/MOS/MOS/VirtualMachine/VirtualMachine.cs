using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.VirtualMachine
{
    class VirtualMachine
    {
        string ptr;
        PageTable pt;
        IC_Reg IC;
        C_Reg C;
        R_Reg R1, R2, R3, R4;
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
        }


        private void DoTask(String com)
        {
            string c = com[0].ToString() + com[1].ToString();
            switch (c)
            {
                case "LR":
                    lr();
                    break;
                case "SR":
                    sr();
                    break;
                case "RR":
                    rr();
                    break;
                case "AD":
                    ad();
                    break;
                case "SB":
                    sb();
                    break;
                case "CR":
                    cr();
                    break;
                case "MU":
                    mu();
                    break;
                case "DI":
                    di();
                    break;
                case "JU":
                    ju();
                    break;
                case: "JG":
                    jg();
                    break;
                case "JE":
                    je();
                    break;
                case "JL":
                    jl();
                    break;
                case "SM":
                    sm();
                    break;
                case "LM":
                    lm();
                    break;
                case "AND":
                    and();
                    break;
                case "XOR":
                    xor();
                    break;
                case "OR":
                    or();
                    break;
                case "NOT":
                    not();
                    break;
                case "FC":
                    fc();
                    break;
                case "FO":
                    fo();
                    break;
                case "FR":
                    fr();
                    break;
                case "FW":
                    fw();
                    break;
                case "FD":
                    fd();
                    break;
            }
        }
        //dabar bus komandoms apdoroti skirti metodai.
        private void lr(string x1x2)
        {
            int addr = x1x2.ToHex();
            int x1 = addr / 16;
            int x2 = addr / 16 % 16;
            R1.R = RealMachine.memory.StringAt(pt.RealAddress(x1), x2);       
        }

        private void sr(string x1x2)
        {
            int addr = x1x2.ToHex();
            int x1 = addr / 16;
            int x2 = addr / 16 % 16;
            RealMachine.memory.WriteAt(pt.RealAddress(x1), x2, R1.R);
        }
        
        private void rr()
        {
            R1.R = (R1.R.ToHex() + R2.R.ToHex()).ToHex();
            R2.R = (R1.R.ToHex() - R2.R.ToHex()).ToHex();
            R1.R = (R1.R.ToHex() - R2.R.ToHex()).ToHex();
        }

        private void ad(string x1x2)
        {
            int addr = x1x2.ToHex();
            int x1 = addr / 16;
            int x2 = addr / 16 % 16;
            R1.R = (R1.R.ToHex() + RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex()).ToHex();
        }

        private void sb(string x1x2)
        {
            int addr = x1x2.ToHex();
            int x1 = addr / 16;
            int x2 = addr / 16 % 16;
            R1.R = (R1.R.ToHex() - RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex()).ToHex();
        }

        private void cr(string x1x2)
        {
            int addr = x1x2.ToHex();
            int x1 = addr / 16;
            int x2 = addr / 16 % 16;
            if (R1.R.ToHex() > RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex())
                C.C = false;
            else
                RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
        }

        private void mu(string x1x2)
        {
            int addr = x1x2.ToHex();
            int x1 = addr / 16;
            int x2 = addr / 16 % 16;
            R1.R = (R1.R.ToHex() * RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex()).ToHex();
        }

        private void di(string x1x2)
        {
            int addr = x1x2.ToHex();
            int x1 = addr / 16;
            int x2 = addr / 16 % 16;
            R1.R = (R1.R.ToHex() / RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex()).ToHex();
            R2.R = (R2.R.ToHex() % RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex()).ToHex();
        }

        private void ju(string x1x2)
        {
            IC.IC = (ushort)x1x2.ToHex();
        }

        private void jg(string x1x2)
        {
            if (C.C == false)
            {
                IC.IC = (ushort)x1x2.ToHex();
            }
        }

        private void je(string x1x2)
        {
            if (C.C == true)
            {
                IC.IC = (ushort)x1x2.ToHex();
            }
        }
    }
}
