using System;
using MOS.Registers;

namespace MOS.VirtualMachine
{
    class VirtualMachine
    {
        readonly PTR_Reg PTR;
        readonly PageTable pt;
        readonly IC_Reg IC;
        readonly C_Reg C;
        readonly R_Reg R1;
        readonly R_Reg R2;
        R_Reg R3;
        readonly R_Reg R4;
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

            pt = new PageTable(PTR.PTR);
        }
        public void RunCommand()
        {
            string command = RealMachine.RealMachine.memory.StringAt(RealMachine.RealMachine.memory.StringAt(PTR.PTR.ToHex(), IC.GetX()).ToHex(), IC.GetY());
            IC.Increase();
            if (command[0] == 'H' && command[1] == 'A')
            {
                halt();
            }
            else
            {
                DoTask(command);
            }
        }
        public void RunCode()
        {
            while (true)
            {
                string command = RealMachine.RealMachine.memory.StringAt(RealMachine.RealMachine.memory.StringAt(PTR.PTR.ToHex(), IC.GetX()).ToHex(), IC.GetY());
                // Console.WriteLine("Command: " + command);
                IC.Increase();
                if (command[0] == 'H' && command[1] == 'A')
                {
                    halt();
                }
                else
                {
                    DoTask(command);
                }
                if (RealMachine.RealMachine.test())
                {
                    return;
                }
            }
        }
        private void halt()
        {
            RealMachine.RealMachine.si.SI = 3;
        }
        private void DoTask(String com)
        {
            string c = com.Substring(0, 2);

            string x1x2 = com.Substring(2, 2);

            if (com == "HALT")
            {
                RealMachine.RealMachine.si.SI = 3;
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
                case "GD":
                    gd(x1x2);
                    break;
                case "PD":
                    pd(x1x2);
                    break;
                case "OR":
                    or();
                    break;
                case "LOOP":
                    loop(x1x2);
                    break;
            }
        }

        private void loop(string x1x2)
        {
            throw new NotImplementedException();
        }

        private void gd(string x1x2) // CF ZF SF
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            x1 = pt.RealAddress(x1);

            Modify_CF(R4.R, x1 * 16 + x2);

            R4.R = x1 * 16 + x2;

            Modify_SF(R4.R);
            Modify_ZF(R4.R);
            RealMachine.RealMachine.si.SI = 1;


        }
        private void pd(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            x1 = pt.RealAddress(x1);

            Modify_CF(R4.R, x1 * 16 + x2);
            R4.R = x1 * 16 + x2;

            Modify_SF(R4.R);
            Modify_ZF(R4.R);

            RealMachine.RealMachine.si.SI = 2;
        }


        private void jg(string x1x2) // patikrinti ar jumpas veikia teisingai(Justui Tvarijonui)
        {
            if (SF.Get_ZF() == false && SF.Get_SF() == SF.Get_OF())
            {

                if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
                {
                    RealMachine.RealMachine.pi.PI = 1;
                    return;
                }

                if (C.C)
                {
                    IC.IC = (ushort) x1x2.ToHex();
                }
            }
        }

        private void not()
        {
            R1.R = ~R1.R;
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
        }

        private void or()
        {
            R1.R = R1.R | R2.R;
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
        }

        private void xor()
        {
            R1.R = R1.R ^ R2.R;
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
        }

        private void and()
        {
            Modify_CF(R1.R,R2.R);
            R1.R = R1.R + R2.R;
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
        }

        private void lr(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            if (x1 > 8 || x1 < 0)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            int x2 = x1x2.Substring(1, 1).ToHex();
            if (x2 > 16)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            R1.R = RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
        }

        private void sr(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            if (x1 > 8 || x1 < 0)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            int x2 = x1x2.Substring(1, 1).ToHex();
            if (x2 > 16)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
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
            if (x1 > 8 || x1 < 0)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            int x2 = x1x2.Substring(1, 1).ToHex();
            if (x2 > 16)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }

            Modify_CF(R1.R, RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex());
            R1.R = R1.R + RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
        }

        private void sb(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            if (x1 > 8 || x1 < 0)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            int x2 = x1x2.Substring(1, 1).ToHex();
            if (x2 > 16)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            Modify_CF(R1.R, -RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2));
            R1.R = R1.R - RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
        }

        private void cr(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            if (x1 > 8 || x1 < 0)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            int x2 = x1x2.Substring(1, 1).ToHex();
            if (x2 > 16)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            C.C = R1.Hex() == RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2);
        }

        private void mu(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            if (x1 > 8 || x1 < 0)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            int x2 = x1x2.Substring(1, 1).ToHex();
            if (x2 > 16)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }

            checked
            {
                try
                {
                    SF.Unset_OF();
                    R1.R = R1.R * RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
                }
                catch (OverflowException)
                {
                    SF.Set_OF();
                }
                
            }

            //R1.R = R1.R * RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
            Modify_SF(R1.R);
            Modify_ZF(R1.R);

        }

        private void di(string x1x2)
        {
            int x1 = x1x2.Substring(0, 1).ToHex();
            if (x1 > 8 || x1 < 0)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            int x2 = x1x2.Substring(1, 1).ToHex();
            if (x2 > 16)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }

            checked
            {
                try
                {
                    SF.Unset_OF();
                    R1.R = R1.R / RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();
                }
                catch (OverflowException)
                {
                    SF.Set_OF();
                }

            }

            

            Modify_SF(R1.R);
            Modify_ZF(R1.R);

            checked
            {
                try
                {
                    SF.Unset_OF();
                    R2.R = R2.R % RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);
                }
                catch (OverflowException)
                {
                    SF.Set_OF();
                }
            }
            

            Modify_SF(R2.R);
            Modify_ZF(R2.R);
        }

        private void ju(string x1x2)
        {
            if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            IC.IC = (ushort)x1x2.ToHex();
        }

        private void je(string x1x2)
        {
            if (SF.Get_ZF() == false && SF.Get_SF() == SF.Get_OF())
            {

                if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
                {
                    RealMachine.RealMachine.pi.PI = 1;
                    return;
                }

                if (C.C)
                {
                    IC.IC = (ushort) x1x2.ToHex();
                }
            }
        }
        private void jl(string x1x2)
        {
            if (SF.Get_SF() != SF.Get_OF())
            {
                if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
                {
                    RealMachine.RealMachine.pi.PI = 1;
                    return;
                }

                if (C.C)
                {
                    IC.IC = (ushort) x1x2.ToHex();
                }
            }
        }

        private void Modify_ZF(int value)
        {
            if (value == 0)
            {
                SF.Set_ZF();
            }
            else
            {
                SF.Unset_ZF();
            }
        }

        private void Modify_CF(int value, int command)
        {
            long sum = value + command;

            if (sum > 2147483647 || sum < -2147483648)
            {
                SF.Get_CF();
            }
            else
            {
                SF.Unset_CF();
            }
        }

        private void Modify_SF(int value)
        {
            if (value < 0)
            {
                SF.Set_SF();
            }
            else
            {
                SF.Unset_SF();
            }
        }
    }
}
