using System.Diagnostics;
using System.IO;
using System;

namespace MOS.RealMachine
{
    public class FlashMemory
    {
        string[] flash;

        public string[] getFlashData(string path)
        {
            try
            {
                flash = File.ReadAllLines(path);
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return flash;
        }
        public string GetFromScreen()
        {
            string s = Console.ReadLine();
            return s;
        }
    }
}