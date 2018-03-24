using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.RealMachine
{
    static class Printer //Jeigu reiks pabaigsim
    {
        private static string[,] output;
        private static StreamWriter writer = new StreamWriter("printerOutput.txt");

        static void PrintStuff(string [,] output)
        {
            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                        writer.Write(output[i,j]);                    
                }
                writer.WriteLine("");
            }
        }
    }
}
