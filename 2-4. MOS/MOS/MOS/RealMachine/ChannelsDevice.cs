
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.RealMachine
{
    public class ChannelsDevice
    {
        private int SB = 0; //Takelio,iš kurio kopijuosime numeris. 
        private int DB = 0; //Takelio,į kurį kopijuosime numeris 
        private int ST = 0; //Objekto,iš kurio kopijuosime numeris 
        private int DT = 0; //Objekto,įkurįkopijuosime,numeris
        // 1. Vartotojoatmintis; 2. Supervizorinėatmintis; 3. Išorinėatmintis; 4. Įvedimosrautas; 

        public string [,] ReadFlash(string flashName)
        {
            string[,] flashOutput = null;

        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
        string[,] matrix = new string[16, 16];
        string[] flash;
        FlashMemory flashMemory = new FlashMemory();

        public string[,] ReadFromFlash()
        {
            flash = flashMemory.getFlashData(filePath);
            if (!CheckValidity(flash))
            {
                Debug.WriteLine("Bad input!");
                return null;
            }
            makeMatrix(flash);
            return matrix;
        }

        public string[,] ReadFromFlash(string path)
        {
            flash = flashMemory.getFlashData(path);
            if (!CheckValidity(flash))
            {
                Debug.WriteLine("Bad input!");
                return null;
            }
            makeMatrix(flash);
            return matrix;
        }

        bool CheckValidity(string[] array)
        {
            bool isData = false;
            bool isCode = false;
            bool isHalt = false;
            bool isOff = false;
            bool isSize = false;

            if (array.Length < 256)
                isSize = true;

            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(' ');
            }

            string arrayStr = builder.ToString();

            if (arrayStr.Contains("DATA"))
                isData = true;
            if (arrayStr.Contains("CODE"))
                isCode = true;
            if (arrayStr.Contains("HALT"))
                isHalt = true;
            if (arrayStr.Contains("0FF"))
                isOff = true;

            if (isData && isCode && isHalt && isOff && isSize)
                return true;
            else return false;
        }

        bool checkCommands(List<string> code)
        {
            string[] commands = new string[] { "LR", "SR", "RR", "AD", "SB", "CR", "MU", "DI", "PY", "JU", "JG", "JE", "JL", "SM", "LM", "HALT" };
            bool isCorrect = false;

            foreach (string str in code)
            {
                foreach (string command in commands)
                {
                    if (str.Length > 3)
                    {
                        string strCom = str.Substring(0, 2);
                        string strAdd = str.Substring(2, 2);
                        //Debug.WriteLine(strCom + "*" + strAdd);
                        if (strCom == command && System.Text.RegularExpressions.Regex.IsMatch(strAdd, @"\A\b[0-9a-fA-F]+\b\Z"))
                        {
                            isCorrect = true;
                            break;
                        }
                    }
                }
                if (!isCorrect)
                    return false;
            }
            return true;
        }

        void makeMatrix(string[] array)
        {
            string[] array2 = array;
            List<string> data = new List<string>();
            List<string> code = new List<string>();

            int i = 0, j = 0, k = 0;

            foreach (string str in array)
            {
                if (str.Length > 6)
                {
                    array2[i] = str.Substring(6);
                    i++;
                }
            }

            bool flag = false;
            foreach (string str in array2)
            {
                if (str.Contains("CODE"))
                {
                    flag = true;
                    continue;
                }
                if (!flag && !str.Contains("DATA"))
                    data.Add(str);
                else if (flag && !str.Contains("0FF"))
                    code.Add(str);
            }

            if (!checkCommands(code))
                Debug.WriteLine("Bad input!");
            //else Debug.WriteLine("GOOD INPUT!!");
            //foreach (string str in data)
            //    Debug.WriteLine(str);
            //foreach (string str in code)
            //    Debug.WriteLine(str);

            foreach (string str in data.ToArray())
            {
                matrix[j, k] = str;
                if (k == 15)
                {
                    k = 0;
                    j++;
                }
                if (j == 7 && k == 15)
                    break;
                k++;
            }

            j = 8; k = 0;
            foreach (string str in code.ToArray())
            {
                matrix[j, k] = str;
                if (k == 15)
                {
                    k = 0;
                    j++;
                }
                if (j == 15 && k == 15)
                    break;
                k++;
            }

            //for (int z = 0; z < 16; z++)
            //{
            //    for (int f = 0; f < 16; f++)
            //    {
            //        Debug.Write(matrix[z, f]);
            //    }
            //    Debug.WriteLine("");
            //}
            return flashOutput;
        }

        public void PrinterOutuput(string [,] input)
        {
            Printer.PrintStuff(input);
        }

        public void DoTheBeep()
        {
            Speaker.Beep();
        }
    }
}