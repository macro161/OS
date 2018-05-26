using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class SupervisoryMemory
    {
        public static int size = 0;
        private static List<String> memory;
        private static List<Program> programList = new List<Program>();

        public static List<string> Memory { get => memory; set => memory = value; }
        public static List<Program> ProgramList { get => programList; set => programList = value; }


    }
}
