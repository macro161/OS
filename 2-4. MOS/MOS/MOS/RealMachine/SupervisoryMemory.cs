using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.RealMachine
{
    class SupervisoryMemory
    {
        string[] memory;
        string[,] matrix = new string[16, 16];
        public string[,] Matrix
        {
            get => matrix;
            set
            {
                matrix = value;
                RaisePropertyChangedEvent("matrix");
            }
        }

        public string[,] CheckAndLoad(string[] memory)
        {
            if (!CheckValidity(memory))
            {
                Debug.WriteLine("Bad input!");
                return null;
            }
            makeMatrix(memory);


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
            string[] commands = new string[] { "LR", "SR", "RR", "AD", "SB", "CR", "MU", "DI", "PY", "JU", "JG", "JE", "JL", "SM", "LM", "LO", "PY", "HALT" };
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
                if (flag) list.RemoveAt(i);
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
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null)
            {
                handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
