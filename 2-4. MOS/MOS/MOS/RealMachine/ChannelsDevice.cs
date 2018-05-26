using MOS.OS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MOS.RealMachine
{
    public class ChannelsDevice
    {
        /* private int SB = 0; //Takelio,iš kurio kopijuosime numeris. 
         private int DB = 0; //Takelio,į kurį kopijuosime numeris 
         private int ST = 0; //Objekto,iš kurio kopijuosime numeris 
         private int DT = 0; //Objekto,įkurįkopijuosime,numeris
                   FlashMemory flashMemory = new FlashMemory();           // 1. Vartotojoatmintis; 2. Supervizorinėatmintis; 3. Išorinėatmintis; 4. Įvedimosrautas; 
 */
        public ChannelsDevice() { }
        readonly FlashMemory flashMemory = new FlashMemory();

        public int SB { get; set; }

        public int DB { get; set; }

        public int ST { get; set; }

        public int DT { get; set; }

        public void XCHG(OS.Program program)
        {
            if (!HardDisk.ProgramList.Any(p => p.name == program.name))
            {
                HardDisk.ProgramList.Add(program);
            }
        }
        public void XCHG()
        {
            string returnString = "";
            if (ST == 1 && DT == 4) //išvedimas į vartotojo ekraną, turi kreiptis į spausdintuvą, dėl išvedimo.
            {
                for (int i = 0; i < 4; i++)
                {
                    returnString += RealMachine.memory.StringAt(DB / 16, DB % 16);
                    DB++;
                }
                Printer.PrintToScreen(returnString);
            }
            if (ST == 4 && DT == 1) //skaitymas iš ekrano, kreipiasi į flash dėl duomenų.
            {
                returnString = flashMemory.GetFromScreen();
                while (returnString.Length < 16)
                {
                    returnString += " ";
                }
                for (int i = 0; i < 4; i++)
                {
                    RealMachine.memory.WriteAt(SB / 16, SB % 16, returnString.Substring(4 * i, 4));
                    SB++;
                }
            }
        }
        public void PrinterOutuput(string[,] input)
        {
            Printer.PrintStuff(input);
        }

        public void DoTheBeep(int x)
        {
            Speaker.Beep(x);
        }
    }
}