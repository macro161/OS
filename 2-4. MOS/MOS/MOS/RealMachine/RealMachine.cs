using MOS.GUI;
using MOS.OS;
using MOS.Registers;
using MOS.VirtualMachine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MOS.RealMachine
{
    public class RealMachine : INotifyPropertyChanged
    {
        public static ChannelsDevice cd = new ChannelsDevice();
        private static SupervisoryMemory sm1 = new SupervisoryMemory();
        public Kernel Kernel { get; set; }
        public static IC_Reg ic = new IC_Reg();
        public static IOI_Reg ioi = new IOI_Reg();
        public static Mode_Reg mode = new Mode_Reg();
        public static PI_Reg pi = new PI_Reg();
        public static R_Reg r1 = new R_Reg();
        public static R_Reg r2 = new R_Reg();
        public static R_Reg r3 = new R_Reg();
        public static R_Reg r4 = new R_Reg();
        public static SF_Reg sf = new SF_Reg();
        public static SI_Reg si = new SI_Reg();
        public static TI_Reg ti = new TI_Reg();
        public static PTR_Reg ptr = new PTR_Reg();
        public static UserMemory memory = new UserMemory();
        public static bool run = true;
        private static List<string[]> ptrArray = new List<string[]>();
        public static string filePath = "";
        private static VirtualMachine.VirtualMachine vm;
        public static string paskutineKomanda;
        private static string pertraukimas;
        private static bool _runCode = false;
        private static bool _next;
        public RealMachine(Kernel kernel)
        {
            Kernel = kernel;
        }
        public string Komanda
        {
            get => paskutineKomanda;
            set
            {
                paskutineKomanda = value;
                RaisePropertyChangedEvent("Komanda");
            }
        }
        public string Pertraukimas
        {
            get => pertraukimas;
            set
            {
                pertraukimas = value;
                RaisePropertyChangedEvent("Pertraukimas");
            }
        }
        public bool Next
        {
            get { return _next; }
            set
            {
                _next = value;
                if (_next)
                {
                    _next = false;

                    vm.RunCommand();
                    if (test())
                    {
                        if (!Test())
                        {
                            run = false;
                        }
                    }
                }

            }
        }
        public bool RunCode
        {
            get { return _runCode; }
            set
            {
                _runCode = value;
                if (_runCode)
                {
                    while (true)
                    {
                        vm.RunCommand(); //virtualiai pasinai pasakoma vykdyti komandą
                        if (test())
                        {
                            if (!Test())
                            {
                                run = false;
                                break;
                            }
                        }
                    }
                }
            }
        }
        #region gui
        public string[,] Memory
        {
            get => memory.UserMemoryProp;
            //get => mematrix;
            set
            {
                memory.UserMemoryProp = value;
                //mematrix = value;
                RaisePropertyChangedEvent("Memory");
            }
        }

        public List<string[]> VMMemory
        {
            //get => memory.UserMemoryProp;
            get => ptrArray;
            set
            {
                //memory.UserMemoryProp = value;
                ptrArray = value;
                RaisePropertyChangedEvent("VMMemory");
            }
        }

        public int R1
        {
            get => r1.R;
            set
            {
                r1.R = value;
                RaisePropertyChangedEvent("R1");
            }
        }

        public int R2
        {
            get => r2.R;
            set
            {
                r2.R = value;
                RaisePropertyChangedEvent("R2");
            }
        }

        public int R3
        {
            get => r3.R;
            set
            {
                r3.R = value;
                RaisePropertyChangedEvent("R3");
            }
        }

        public int R4
        {
            get => r4.R;
            set
            {
                r4.R = value;
                RaisePropertyChangedEvent("R4");
            }
        }

        public byte SF
        {
            get => sf.SF;
            set
            {
                sf.SF = value;
                RaisePropertyChangedEvent("SF");
            }
        }

        public short SI
        {
            get => si.SI;
            set
            {
                si.SI = value;
                RaisePropertyChangedEvent("SI");
            }
        }

        public ushort TI
        {
            get => ti.TI;
            set
            {
                ti.TI = value;
                RaisePropertyChangedEvent("TI");
            }
        }

        public string PTR
        {
            get => ptr.PTR;
            set
            {
                ptr.PTR = value;
                RaisePropertyChangedEvent("PTR");
            }
        }

        public ushort IC
        {
            get => ic.IC;
            set
            {
                ic.IC = value;
                RaisePropertyChangedEvent("IC");
            }
        }

        public ushort IOI
        {
            get => ioi.IOI;
            set
            {
                ioi.IOI = value;
                RaisePropertyChangedEvent("IOI");
            }
        }

        public byte MODE
        {
            get => mode.Mode;
            set
            {
                mode.Mode = value;
                RaisePropertyChangedEvent("MODE");
            }
        }

        public short PI
        {
            get => pi.PI;
            set
            {
                pi.PI = value;
                RaisePropertyChangedEvent("PI");
            }
        }

        internal static SupervisoryMemory Sm { get => Sm1; set => Sm1 = value; }
        internal static SupervisoryMemory Sm1 { get => sm1; set => sm1 = value; }

        #endregion

        public void TransferProgramToMemory(string[,] flash)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++) // x - 256 y - 16
                {
                    memory.WriteAt(memory.IntAt(PTR.ToHex(), i), j, flash[i, j]);
                    var str = new string[3];
                    str[0] = memory.IntAt(PTR.ToHex(), i).ToString();
                    str[1] = j.ToString();
                    str[2] = flash[i, j];
                    ptrArray.Add(str);
                    //Console.WriteLine(str[0]+str[1]+str[2]);
                }
            }
        }

        public void PrintRegisters()
        { //ic, ioi, mode, pi, r1, r2, r3, r4, sf, si, ti, ptr, 
            Console.WriteLine("IC - " + ic.IC);
            Console.WriteLine("IOI - " + ioi.IOI);
            Console.WriteLine("MODE - " + mode.Mode);
            Console.WriteLine("PI - " + PI);
            Console.WriteLine("R1 - " + R1);
            Console.WriteLine("R2 - " + R2);
            Console.WriteLine("R3 - " + R3);
            Console.WriteLine("R4 - " + R4);
            Console.WriteLine("SF - " + SF);
            Console.WriteLine("SI - " + SI);
            Console.WriteLine("TI - " + TI);
            Console.WriteLine("PTR - " + ptr.PTR);

            Console.ReadLine();
        }
        public bool Test() // metodas apdorojantis pertraukimus, cont kintamasis parodo, ar tęsime VM veiklą atsižvelgiant į pertraukimų tipus.
        {
            pertraukimas = "";
            bool cont = true;
            switch (pi.PI)
            {
                case 1:
                    Printer.PrintToScreen("Neteisingai įvestas adresas!");
                    pertraukimas = "Programinis pertraukimas (neteisingas adresas)";
                    cont = false;
                    break;
                case 2:
                    Printer.PrintToScreen("Neteisingas operacijos kodas!");
                    pertraukimas = "Programinis pertraukimas (neteisingas operacijos kodas)";
                    cont = false;
                    break;
                case 3:
                    Printer.PrintToScreen("Negalimas priskyrimas!");
                    pertraukimas = "Programinis pertraukimas (negalimas priskyrimas)";
                    cont = false;
                    break;
                case 4:
                    Printer.PrintToScreen("overflow!");
                    pertraukimas = "Programinis pertraukimas (overflow)";
                    break;
            }
            switch (si.SI)
            {
                case 1:
                    pertraukimas = "Sisteminis pertraukimas (nuskaitymas)";
                    GetData(r4.R);
                    break;
                case 2:
                    pertraukimas = "Sisteminis pertraukimas (išvedimas)";
                    WriteData(r4.R);
                    break;
                case 4:
                    pertraukimas = "Sisteminis pertraukimas (Pyp)";
                    cd.DoTheBeep(r4.R);
                    break;
                case 3:
                    pertraukimas = "Sisteminis pertraukimas (HALT)";
                    Halt();
                    MessageBox.Show("HALT");
                    cont = false;
                    break;
            }
            if (ti.TI == 0)
            {
                pertraukimas += Environment.NewLine + "Taimerio pertraukimas";
                ti.TI = 10;
            }
            pi.PI = 0;
            si.SI = 0;
            return cont;
        }

        private void Halt()
        {
            /*PageTable pt = new PageTable(ptr);
            for (int i = 0; i < 16; i++)
            {
                int x = pt.RealAddress(i);
                for (int j = 0; j < 16; j++)
                {
                    memory.WriteAt(x, j, "");
                }
                memory.WriteAt(ptr.PTR.TwoLastbytesToHex(), i, "");
                memory.SetFree(x);
            }
            memory.SetFree(ptrTwoLastbytesToHex());
            ptr.Clear();
            ClearRegisters();
            paskutineKomanda = "";
            pertraukimas = "";*/
        }

        private void GetData(int x1x2) // perskaito 4 žodžius ir įrašo pradedant x1 * 16 + x2
        {
            cd.ST = 4;
            cd.DT = 1;
            cd.SB = x1x2;
            cd.XCHG();
        }

        private void WriteData(int x1x2)  // išveda 4 žodžius pradedant x1*16 + x2
        {
            cd.ST = 1;
            cd.DT = 4;
            cd.DB = x1x2;
            cd.XCHG();
        }

        public static bool test()
        {
            if (((pi.PI + si.SI) > 0) || (ti.TI == 0))
            {
                return true;
            }

            return false;
        }
        public static void PrintMemory()
        {
            Console.WriteLine("0    1    2    3    4    5    6    7    8    9    A    B    C    D    E    F");
            for (int i = 0; i < 0x255; i++)
            {
                Console.Write(i.ToString("X") + " ");
                for (int j = 0; j < 16; j++)
                {
                    Console.Write(memory.StringAt(i, j) + " ");
                }

                Console.WriteLine("");
            }

            Console.ReadLine();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool LoadProgramToSupervisory(string path)
        {
            ptrArray = new List<string[]>();
            ClearRegisters();
            bool succes = true;
            FlashMemory fl = new FlashMemory();
            string[] file = fl.getFlashData(path);

            var data = sm1.CheckAndLoad(file);
            if (data == null)
            {
                return false;
            }
            ptr.PTR = memory.getMemory();
            TransferProgramToMemory(data);
            //vm = new VirtualMachine.VirtualMachine(ptr, r1, r2, r3, r4, ic, sf); //sukuriama virtuali masina /*CIA DAR REIKS PATAISYTI */
            RMform.ptrList = VMMemory;
            run = true;
            return succes;
        }
        private void ClearRegisters()
        {
            ptr.Clear();
            r1.Clear();
            r2.Clear();
            r3.Clear();
            r4.Clear();
            si.Clear();
            pi.Clear();
            sf.SF = 0;
            ti.TI = 10;
            ic.Clear();
        }
    }
}

