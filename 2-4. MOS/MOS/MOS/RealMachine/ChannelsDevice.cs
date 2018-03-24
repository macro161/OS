using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MOS.RealMachine
{
    class ChannelsDevice
    {
        private int SB = 0; //Takelio,iš kurio kopijuosime numeris. 
        private int DB = 0; //Takelio,į kurį kopijuosime numeris 
        private int ST = 0; //Objekto,iš kurio kopijuosime numeris 
        private int DT = 0; //Objekto,įkurįkopijuosime,numeris
        // 1. Vartotojoatmintis; 2. Supervizorinėatmintis; 3. Išorinėatmintis; 4. Įvedimosrautas; 

        public string [,] ReadFlash(string flashName)
        {
            string[,] flashOutput = null;

            return flashOutput;
        }

        public void PrinterOutuput(string [,] input)
        {
            Printer.PrintStuff(input);
        }

        public void DoTheBeep()
        {
            Speaker.Beep();
        }
    }
}