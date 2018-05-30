using System;
using System.Security.Cryptography;
using MOS.Registers;
using MOS.OS;
using MOS.Resources;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MOS.VirtualMachine
{
    public class VirtualMachine : Process
    {
        public PTR_Reg PTR { get; set; }
        public PageTable pt { get; set; }
        public IC_Reg IC { get; set; }
        public R_Reg R1 { get; set; }
        public R_Reg R2 { get; set; }
        public R_Reg R3 { get; set; }
        public R_Reg R4 { get; set; }
        public SF_Reg SF { get; set; }
        public Mode_Reg MODE{ get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public TI_Reg TI { get; set; }

        private int sharedTrack;

        public VirtualMachine(PTR_Reg ptr, R_Reg r1, R_Reg r2, R_Reg r3, R_Reg r4, IC_Reg ic, SF_Reg sf, Kernel kernel, Process father, int priority, int status, List<Resource> resources, Guid id, int pointer, string name) 
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
        public VirtualMachine(Kernel kernel, Process father, int priority, int status, List<Resource> resources, Guid id, int pointer, string name)
        {
        IC = new IC_Reg();
        MODE = new Mode_Reg();
        R1 = new R_Reg();
        R2 = new R_Reg();
        R3 = new R_Reg();
        R4 = new R_Reg();
        SF = new SF_Reg();
        TI = new TI_Reg();
        PTR = new PTR_Reg();
    }
        public override void Run()
        {
            Log.Info("Virtual Machine " + Name + " is running");
            RealMachine.RealMachine.si.SI = 0;
            RealMachine.RealMachine.pi.PI = 0;
            RealMachine.RealMachine.ti = TI;
            while (true)
            {
                RunCommand();
                if (test())
                {
                    break;
                }
            }
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

        bool test()
        {
            if (RealMachine.RealMachine.si.SI > 0 || RealMachine.RealMachine.ti.TI == 0 || RealMachine.RealMachine.pi.PI > 0)
            {
                TI = RealMachine.RealMachine.ti;
                ((JobGovernor)Father).Descriptor.SaveVMState(this);
                Kernel.dynamicResources.First(res => res.Name == "INTERUPT").ReleaseResource(new InterruptResourceElement(Father));
                return true;
            }
            return false;  
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
                case "BC":
                    bc(x1x2); // blokuoti priejima prie takelio x1x2
                    break;
                case "RE":
                    re(x1x2); //atblokuoti takeli x1x2
                    break;
                case "WS":
                    ws(x1x2); //WriteShared memory
                    break;
                case "RS":
                    rs(x1x2); //ReadShared memory
                    break;
                case "KK":
                    KK(x1x2); 
                    break;
                default:
                    RealMachine.RealMachine.pi.PI = 2;
                    break;
            }
        }
        private void KK(string x1x2)
        {
            if (x1x2.IsHex())
            {
                R3.R = x1x2.ToHex();
            }
        }
        private void rs(string x1x2)
        {
            R1.R = RealMachine.RealMachine.memory.StringAt(sharedTrack, x1x2.ToHex()).ToHex();
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void ws(string x1x2)
        {
            RealMachine.RealMachine.memory.WriteAt(sharedTrack, x1x2.ToHex(), R1.Hex());
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void re(string x1x2)
        {
            if (x1x2 == "01")
            {
                RealMachine.RealMachine.memory.firstTrackSemaphore.Release();
            }

            if (x1x2 == "02")
            {
                RealMachine.RealMachine.memory.secondTrackSemaphore.Release();

            }
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void bc(string x1x2)
        {
            if (x1x2 == "01")
            {

                if (!RealMachine.RealMachine.memory.firstTrackSemaphore.Block(Id))
                {
                    HandleFalseBlock();
                }
                else
                {
                    sharedTrack = 1;
                }
            }

            if (x1x2 == "02")
            {
                if (!RealMachine.RealMachine.memory.secondTrackSemaphore.Block(Id))
                {
                    HandleFalseBlock();
                }
                else
                {
                    sharedTrack = 2;
                }
            }
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void HandleFalseBlock()
        {
            IC.IC--; // Is it good? Ciuju reiks pakeist
        }

        private void gd(string x1x2) // CF ZF SF
        {

            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            x1 = pt.RealAddress(x1);

            R4.R = x1 * 16 + x2;

            RealMachine.RealMachine.si.SI = 1;
            RealMachine.RealMachine.ti.DecrementTI();

        }

        private void pd(string x1x2)
        {

            int x1 = x1x2.Substring(0, 1).ToHex();
            int x2 = x1x2.Substring(1, 1).ToHex();
            x1 = pt.RealAddress(x1);

            R4.R = x1 * 16 + x2;

            RealMachine.RealMachine.si.SI = 2;
            RealMachine.RealMachine.ti.DecrementTI();
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
                IC.IC = (ushort)x1x2.ToHex();
            }
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void not()
        {
            R1.R = ~R1.R;

            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void or()
        {
            R1.R = R1.R | R2.R;

            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void xor()
        {
            R1.R = R1.R ^ R2.R;

            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void and()
        {

            R1.R = R1.R & R2.R;

            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void lr(string x1x2)
        {
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).Trim().IsHex())
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
            if (!(RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).Trim().IsHex())
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


            R1.R = R1.R + RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1), x2).ToHex();

            cr(x1x2);

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

            R1.R = R1.R - RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);

            cr(x1x2);

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
            SF.SF = 0;
            if (R1.R > (RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).ToHex())
            {

            }

            if (R1.R == (RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).ToHex())
            {
                SF.Flip_ZF();
            }

            if (R1.R < (RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex())).ToHex())
            {
                SF.Flip_CF();
            }

            var bitOne = (R1.R & (1 << 7)) != 0; //casting some black magic stright from depths of stackoverflow
            var bitTwo = (RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex()).ToHex() & (1 << 7)) != 0;

            int sum = R1.R + RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex()).ToHex();
            int sub = R1.R - RealMachine.RealMachine.memory.StringAt(pt.RealAddress(x1x2.Substring(0, 1).ToHex()), x1x2.Substring(1, 1).ToHex()).ToHex();

            var sumBit = (sum & (1 << 7)) != 0;
            var subBit = (sub & (1 << 7)) != 0;

            //Check for overflow

            if ((bitOne != bitTwo) || (bitOne == sumBit && bitTwo == sumBit))
            {

                //SF.Unset_OF();
            }


            if ((bitOne == bitTwo) && (bitOne != sumBit))
            {
                SF.Flip_OF();
            }



            if (subBit == true)
            {
                SF.Flip_SF();
            }
            else
            {
                //SF.Unset_SF();
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

            R1.R = R1.R * RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);

            cr(x1x2);

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

            R1.R = R1.R * RealMachine.RealMachine.memory.IntAt(pt.RealAddress(x1), x2);

            cr(x1x2);

            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void ju(string x1x2)
        {
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
            if (SF.Get_ZF() == true)
            {

                if (x1x2.ToHex() < 0x80 || x1x2.ToHex() > 0xFF)
                {
                    RealMachine.RealMachine.pi.PI = 1;
                    return;
                }
                IC.IC = (ushort)x1x2.ToHex();
            }
            RealMachine.RealMachine.ti.DecrementTI();
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
                IC.IC = (ushort)x1x2.ToHex();
            }
            RealMachine.RealMachine.ti.DecrementTI();
        }

        private void lo(string x1x2)
        {
            if (x1x2.ToHex() > IC.IC || x1x2.ToHex() < 0x80)
            {
                RealMachine.RealMachine.pi.PI = 1;
                return;
            }

            R2.R -= 1;

            if (!SF.Get_ZF())
            {
                IC.IC = (ushort)x1x2.ToHex();
            }
            RealMachine.RealMachine.ti.DecrementTI();
        }

        public override void DecrementPriority()
        {
            Priority--;
        }
    }
}
