using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class HardDisk
    {
        private static List<Program> programList;

        internal static List<Program> ProgramList { get => programList; set => programList = value; }
    }
}
