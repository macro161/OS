﻿using System;
using System.Collections.Generic;
using MOS.OS;

namespace MOS.RealMachine
{
    public class UserMemory
    {
        private readonly bool[] isUsed = new bool[0x256]; // skirstant takelius pasižymim, kurie jau užimti, kai atsilaisvins vėl pažimėsim true. 
        readonly Random rand = new Random();
        private int free = 0x256;
        public Semaphore firstTrackSemaphore = new Semaphore();
        public Semaphore secondTrackSemaphore = new Semaphore();


        public string[,] UserMemoryProp { get; set; } = new string[0x256, 0x10];

        public int GetRandomBlock()
        {
            int i = rand.Next(2, 0x255);
            while (isUsed[i])
            {
                if (i == 0x255)
                {
                    i = 2;
                }
                i++;
            }
            free--;
            isUsed[i] = true;
            return i;
        }
        public void SetFree(int nr)
        {
            isUsed[nr] = false;
            free++;
        }
        public string getMemory() // gražina ptr registrą, kuris rodo į puslapių lentelę
        {
            if (free < 18)
            {
                Console.WriteLine("We are out of memory");
                return "0";
            }
            int ptr = GetRandomBlock();
            string Ptr = ptr.ToString("x8");

            for (int i = 0; i < 0x10; i++)
            {
                int p = GetRandomBlock();
                UserMemoryProp[ptr, i] = p.ToString("X8");
            }
            return Ptr;
        }

        public string StringAt(int x, int y) // 
        {
            if (y == 16)
            {
                x++;
                y = 0;
            }
            return UserMemoryProp[x, y];
        }

        public int IntAt(int x, int y)
        {
            var t = UserMemoryProp[x, y];
            return int.Parse(UserMemoryProp[x, y], System.Globalization.NumberStyles.HexNumber);
        }

        public void WriteAt(int x, int y, string word)
        {
            if (y == 16)
            {
                x++;
                y = 0;
            }
            UserMemoryProp[x, y] = word;
        }

        public static byte[] IntToByte(int a)
        {
            byte[] result = new byte[4];
            result[0] = (byte)(a >> 24);
            result[1] = (byte)(a >> 0x10);
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

            for (int i = 0; i < 0x10; i++)
            {
                for (int j = 0; j < 0x10; j++)
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
