﻿using System;
using System.Security.Cryptography;
using MOS.Registers;

namespace MOS.VirtualMachine
{
    class VirtualMachine
    {
        readonly PTR_Reg PTR;
        readonly PageTable pt;
        readonly IC_Reg IC;
        readonly R_Reg R1;
        readonly R_Reg R2;
        R_Reg R3;
        readonly R_Reg R4;
        SF_Reg SF;

        public VirtualMachine(PTR_Reg ptr, R_Reg r1, R_Reg r2, R_Reg r3, R_Reg r4, IC_Reg ic, SF_Reg sf)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
            R4 = r4;
            IC = ic;
            SF = sf;
            PTR = ptr;

            pt = new PageTable(PTR.PTR);
        }
        public void RunCommand()
        {
            string command = RealMachine.RealMachine.memory.StringAt(RealMachine.RealMachine.memory.StringAt(PTR.PTR.ToHex(), IC.GetX()).ToHex(), IC.GetY());
            RealMachine.RealMachine.paskutineKomanda = command;
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

        private void halt()
        {
            RealMachine.RealMachine.si.SI = 3;
        }
        private void DoTask(String com)
        {
            while (com.Length < 4)
            {
                com += " ";
            }
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
                case "PY":
                    py(x1x2);
                    break;
                case "LO":
                    lo(x1x2);
                    break;
                default:
                    RealMachine.RealMachine.pi.PI = 2;
                    break;
            }
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
            RealMachine.RealMachine.ti.DecrementTI();

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
            RealMachine.RealMachine.ti.DecrementTI();
        }


        private void jg(string x1x2) // patikrinti ar jumpas veikia teisingai(Justui Tvarijonui)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
            if (SF.Get_ZF() == false && SF.Get_SF() == SF.Get_OF())
            {

                if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
                {
                    RealMachine.RealMachine.pi.PI = 1;
                    return;
                }
                    IC.IC = (ushort) x1x2.ToHex();
            }
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void not()
        {
            R1.R = ~R1.R;
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void or()
        {
            R1.R = R1.R | R2.R;
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void xor()
        {
            R1.R = R1.R ^ R2.R;
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void and()
        {
            Modify_CF(R1.R,R2.R);
            R1.R = R1.R + R2.R;
            Modify_SF(R1.R);
            Modify_ZF(R1.R);
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void lr(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
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
            RealMachine.RealMachine.ti.DecrementTI();
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
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void rr()
        {
            R1.R = R1.R + R2.R;
            R2.R = R1.R - R2.R;
            R1.R = R1.R - R2.R;
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void ad(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
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
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void sb(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
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
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void py(string x1x2)
        {
            R4.R = x1x2.ToHex();
            RealMachine.RealMachine.si.SI = 4;
            RealMachine.RealMachine.ti.DecrementTI();

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
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void mu(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
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
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void di(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
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
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void ju(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
            if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }
            IC.IC = (ushort)x1x2.ToHex();
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void je(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
            if (SF.Get_ZF() == false && SF.Get_SF() == SF.Get_OF())
            {

                if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
                {
                    RealMachine.RealMachine.pi.PI = 1;
                    return;
                }
                    IC.IC = (ushort) x1x2.ToHex();
            }
            RealMachine.RealMachine.ti.DecrementTI();
        }
        private void jl(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
            if (SF.Get_SF() != SF.Get_OF())
            {
                if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
                {
                    RealMachine.RealMachine.pi.PI = 1;
                    return;
                }

                    IC.IC = (ushort) x1x2.ToHex();
            }
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void lo(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).IsHex())
            {
                RealMachine.RealMachine.pi.PI = 3;
                return;
            }
            if (x1x2.ToHex() > IC.IC || x1x2.ToHex() < 0x80)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }

            R2.R -= 1;
            Modify_ZF(R2.R);
            if (!SF.Get_ZF())
            {
                IC.IC = (ushort)x1x2.ToHex();
            }
            RealMachine.RealMachine.ti.DecrementTI();
        }
        #region{Flags}
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
                SF.Set_CF();
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
        #endregion
    }
}
