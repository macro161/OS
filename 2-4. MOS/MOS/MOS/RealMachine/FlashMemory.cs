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
        public string GetFromScreen(int kiekis)
        {
            string s = Console.ReadLine();
            if (s.Length > 15)
            {
                return s.Substring(0, 16);
            }
            return s;
        }
    }
}