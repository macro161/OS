using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}