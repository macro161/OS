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
        public static string[] supervisoryMemory;

        public List<Program> cleanPrograms = new List<Program>();

        public static void PutData(string data)
        {
            supervisoryMemory[size] = data;
            size++;
        }

        public static void GetData(string name) { }
    }
}
