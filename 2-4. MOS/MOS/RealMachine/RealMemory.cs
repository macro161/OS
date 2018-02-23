using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RealMachine
{
    class RealMemory
    {
        Dictionary<int, int[]> realMemory = new Dictionary<int, int[]>();

        public void FillMemory(int blockNumber, int[] blockValues, string location)
        {
            using (StreamReader reader = new StreamReader(location))
            {

            }
        }
    }
}
