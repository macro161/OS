using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMachine
{
    class UserMemory
    {
        private byte[, ,] userMemory = new byte[272,16, 4]; // 272 takeliai po 16 žodžių po baitus.
        private bool[] isEmpty = new bool[272]; // skirstant takelius pasižymim, kurie jau užimti, kai atsilaisvins vėl pažimėsim true. 

        public int GetRandomBlock()
        {
            int i = (new Random()).Next(0, 273);
            while(isEmpty[i] == false)
            {
                if (i == 272)
                {
                    i = 0;
                }
            }
            isEmpty[i] = false;
            return i;
        }
        public byte[] getMemory()
        {
            int ptr = GetRandomBlock();
            userMemory[ptr, 0, 0] = IntToByte(ptr)[0];
            userMemory[ptr, 0, 1] = IntToByte(ptr)[1];
            userMemory[ptr, 0, 2] = IntToByte(ptr)[2];
            userMemory[ptr, 0, 3] = IntToByte(ptr)[3];
            for (int i = 1; i < 16; i++)
            {
                int p = GetRandomBlock();
                userMemory[ptr, i, 0] = IntToByte(p)[0];
                userMemory[ptr, i, 1] = IntToByte(p)[1];
                userMemory[ptr, i, 2] = IntToByte(p)[2];
                userMemory[ptr, i, 3] = IntToByte(p)[3];
            }
            return IntToByte(ptr); ;
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

            for (int i = 0; i < 16 ; i++)
            {
                for (int j = 0; j < 16 ; j++ )
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
