﻿using System.IO;

namespace MOS.RealMachine
{
    static class Printer //Jeigu reiks pabaigsim
    {
        private static string[,] output;
        private static StreamWriter writer = new StreamWriter("printerOutput.txt");

        public static void PrintStuff(string[,] outputForPrinter)
        {
            for (int i = 0; i < outputForPrinter.GetLength(0); i++)
            {
                for (int j = 0; j < outputForPrinter.GetLength(1); j++)
                {
                    writer.Write(outputForPrinter[i, j]);
                }
                writer.WriteLine("");
            }
        }
    }
}
