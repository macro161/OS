using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealMachine
{
    class FlashMemory
    {
        public readonly string memory;
        

        public FlashMemory()
        {
            memory = GetData();
        }

        private string GetData()
        {
            string memory = "";

            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Flash.txt");

            string path = file;
          
            if (!File.Exists(path))
            {
                string createText  = Environment.NewLine;
                File.WriteAllText(path, createText);
            }

            string appendText = Environment.NewLine;
            File.AppendAllText(path, appendText);

            string readText = File.ReadAllText(path);
            
            MessageBox.Show(readText);

            return readText;
        }
}
}
