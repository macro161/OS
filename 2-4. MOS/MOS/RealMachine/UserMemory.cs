using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMachine
{
    class UserMemory
    {
        public string[,] userMemory = new string[16,16]; // pirmas 16 eiles antras 16 stulpeliai

        public void TakeData(string data)
        {
            int count = 0;

            for (int i = 0; i < 16 ; i++)
            {
                for (int j = 0; j < 16 ; j++ )
                {
                    if (count + 4 < data.Length)
                    {
                        userMemory[i, j] = data[count].ToString() + data[count + 1].ToString() + data[count + 2].ToString() + data[count + 3].ToString();
                        count += 4;
                    }
                    else
                    {
                        break;
                    }
                }
            }


        }
    }
}
