using System;

namespace MOS.RealMachine
{
    public class UserMemory
    {
        private string[,] userMemory = new string[256, 16]; // 255 takeliai po 16 žodžių po 4 baitus.
        private bool[] isUsed = new bool[256]; // skirstant takelius pasižymim, kurie jau užimti, kai atsilaisvins vėl pažimėsim true. 
        Random rand = new Random();
        public int GetRandomBlock()
        {
            int i = rand.Next(0, 257);
            while (isUsed[i])
            {
                if (i == 256)
                {
                    i = 0;
                }
                i++;
            }
            isUsed[i] = true;
            return i;
        }
        public string getMemory() // gražina ptr registrą, kuris rodo į puslapių lentelę
        {
            int ptr = GetRandomBlock();
            string Ptr = "00" + (ptr / 16 % 16).ToHex() + (ptr % 16).ToHex();

            for (int i = 0; i < 16; i++)
            {
                int p = GetRandomBlock();
                userMemory[ptr, i] = p.ToString("X4");
            }
            return Ptr;
        }

        public string StringAt(int x, int y) // 
        {
            return userMemory[x, y];
        }

        public int IntAt(int x, int y)
        {
            return int.Parse(userMemory[x, y], System.Globalization.NumberStyles.HexNumber);
        }

        /*public string HexAt(int x, int y)
        {
            return Int32.Parse(userMemory[x,y]).ToString("X");
        }*/

        public void WriteAt(int x, int y, string word)
        {
            userMemory[x, y] = word.Substring(0, 4);
        }

        public static byte[] IntToByte(int a)
        {
            byte[] result = new byte[4];
            result[0] = (byte)(a >> 24);
            result[1] = (byte)(a >> 16);
            result[2] = (byte)(a >> 8);
            result[3] = (byte)(a);
            return result;
        }

        public static int ByteToInt(byte[] a)
        {
            int l = 0;
            l |= a[0] & 0xFF;
            l <<= 8;
            l |= a[1] & 0xFF;
            l <<= 8;
            l |= a[2] & 0xFF;
            l <<= 8;
            l |= a[3] & 0xFF;
            return l;
        }

        public void TakeData(string data)
        {
            int count = 0;

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (count + 4 < data.Length)
                    {
                        //userMemory[i, j] = data[count].ToString() + data[count + 1].ToString() + data[count + 2].ToString() + data[count + 3].ToString();
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
