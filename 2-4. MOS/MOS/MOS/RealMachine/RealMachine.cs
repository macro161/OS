using System;
using MOS.Registers;

namespace MOS.RealMachine
{
    public class RealMachine
    {
        public ChannelsDevice cd = new ChannelsDevice();
        public C_Reg c = new C_Reg();
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
        public PTR_Reg ptr = new PTR_Reg();
        public static UserMemory memory = new UserMemory();
        private bool run = true;

        public void PowerOn()
        {
            while (run)
            {
                Console.WriteLine("1. Load test program");
                Console.WriteLine("2. Load by name");
                Console.WriteLine("3. Print registers");
                Console.WriteLine("4. Print Real machine memory");

                switch (Console.ReadLine())
                {
                    case "1":
                        LoadTestProgram();
                        break;
                    case "2":
                        break;
                    case "3":
                        PrintRegisters();
                        break;
                    case "4":
                        PrintMemory();
                        break;
                    default:
                        Console.WriteLine("Bad input");
                        break;
                }
            }
        }

        private void LoadTestProgram()
        {
            //cd.ReadFlash("test.txt");  //naudojames kanalu irenginiu pasiimti programa, ivyksta tikrinimas ar korektiskas kodas
            ptr._ptr = memory.getMemory(); //isskiriami laisvi atminties blokai programai
            VirtualMachine.VirtualMachine vm = new VirtualMachine.VirtualMachine(ptr, r1, r2, r3, r4, ic, sf, c); //sukuriama virtuali masina
            vm.RunCode(); //virtualiai pasinai pasakoma vykdyti koda
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

        public void PrintRegisters()
        {
            Console.WriteLine("C - : " + c.C.ToString()); //ic, ioi, mode, pi, r1, r2, r3, r4, sf, si, ti, ptr, 
            Console.WriteLine("IC - " + ic.IC.ToString());
            Console.WriteLine("IOI - " + ioi.IOI.ToString());
            Console.WriteLine("MODE - " + mode.Mode.ToString());
            Console.WriteLine("PI - " + pi._pi.ToString());
            Console.WriteLine("R1 - " + r1.R.ToString());
            Console.WriteLine("R2 - " + r2.R.ToString());
            Console.WriteLine("R3 - " + r3.R.ToString());
            Console.WriteLine("R4 - " + r4.R.ToString());
            Console.WriteLine("SF - " + sf.Return_Status_Flag().ToString());
            Console.WriteLine("SI - " + si._si.ToString());
            Console.WriteLine("TI - " + ti._ti.ToString());
            Console.WriteLine("PTR - " + ptr._ptr);
        }

        public void PrintMemory()
        {
            Console.WriteLine("0    1    2    3    4    5    6    7    8    9    A    B    C    D    E    F");
            for (int i = 0; i < 256; i++)
            {
                Console.Write(i.ToString("X"));
                for (int j = 0; j < 16; j++)
                {
                    Console.Write(memory.StringAt(i, j) + " ");
                }
                Console.WriteLine("");
            }
        }

    }
}
