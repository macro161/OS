using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MOS.RealMachine
{
    public class ChannelsDevice
    {
       /* private int SB = 0; //Takelio,iš kurio kopijuosime numeris. 
        private int DB = 0; //Takelio,į kurį kopijuosime numeris 
        private int ST = 0; //Objekto,iš kurio kopijuosime numeris 
        private int DT = 0; //Objekto,įkurįkopijuosime,numeris
                  FlashMemory flashMemory = new FlashMemory();           // 1. Vartotojoatmintis; 2. Supervizorinėatmintis; 3. Išorinėatmintis; 4. Įvedimosrautas; 
*/
        readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
        string[,] matrix = new string[16, 16];
        readonly FlashMemory flashMemory = new FlashMemory();
        public string[,] Matrix
        {
            get => matrix;
            set
            {
                matrix = value;
                RaisePropertyChangedEvent("matrix");
            }
        }

        public int SB { get; set; }

        public int DB { get; set; }

        public int ST { get; set; }

        public int DT { get; set; }

        public void XCHG()
        {
            string returnString = "";
            if (ST == 1 && DT == 4) //išvedimas į vartotojo ekraną, turi kreiptis į spausdintuvą, dėl išvedimo.
            {
                for (int i = 0; i < 4; i++)
                {
                    returnString += RealMachine.memory.StringAt(DB / 16, DB % 16);
                    DB++;
                }
                Printer.PrintToScreen(returnString);
            }
            if (ST == 4 && DT == 1) //skaitymas iš ekrano, kreipiasi į flash dėl duomenų.
            {
                returnString = flashMemory.GetFromScreen();
                while (returnString.Length < 16)
                {
                    returnString += " ";
                }
                for (int i = 0; i < 4; i++)
                {
                    RealMachine.memory.WriteAt(SB / 16, SB % 16, returnString.Substring(4*i, 4));
                    SB++;
                }
            }
        }

        string[] flash;
       


        public string[,] ReadFromFlash()
        {
            flash = flashMemory.getFlashData(filePath);
            if (!CheckValidity(flash))
            {
                Debug.WriteLine("Bad input!");
                return null;
            }
            makeMatrix(flash);

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    //Console.Write(matrix[i, j]);
                }
            }

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
            bool isSize = array.Length < 256;

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

            if (isData && isCode && isHalt && isSize)
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
            //string[] array2 = array.ToList<string>();
            List<string> list = array.ToList();
            List<string> data = new List<string>();
            List<string> code = new List<string>();
            bool flag = false;

            int i = 0, j = 0, k = 0;

            foreach (string str in list.ToList())
            {
                if(flag) list.RemoveAt(i);
                if (str.Contains("HALT"))
                    flag = true;
                    i++;
            }

            flag = false;
            foreach (string str in list)
            {
                if (str.Contains("CODE"))
                {
                    flag = true;
                    continue;
                }
                if (!flag && !str.Contains("DATA"))
                    data.Add(str);
                else if (flag)
                    code.Add(str);
            }

            if (!checkCommands(code))
                Debug.WriteLine("Bad input!");
 

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
        }

        public void PrinterOutuput(string[,] input)
        {
            Printer.PrintStuff(input);
        }

        public void DoTheBeep()
        {
            Speaker.Beep();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null)
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}