using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CopyUserMemory(int sb)
        {
            ST = 1;
        }

        public void CopySupervisorMemory(int sb)
        {
            ST = 2;
        }

        public void CopyExternalMemory(int sb)
        {
            ST = 3;
        }

        public void CopyInputStream(int sb)
        {
            ST = 4;
        }









    }
}
