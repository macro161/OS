using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace RealMachine
{
    class ReadProgram
    {
        public static void ReadFlashMemory()
        {
            string fileName = GetFileName();
        }

        public static string GetFileName()
        {
            string fileName = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
            }

            return fileName;
        }
    }
}
