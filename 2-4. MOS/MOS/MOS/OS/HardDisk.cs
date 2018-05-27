﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class HardDisk
    {
        private static List<Program> programList;

        public static List<Program> ProgramList
        {
            get => programList; set
            {
                programList = value;
                GUI.SystemForm.SetPrograms(GetNames());
            }
        }

        public static List<string> GetNames()
        {
            List<string> names = new List<string>();
            foreach (var pr in ProgramList)
            {
                names.Add(pr.name);
            }
            return names;
        }

        public static event PropertyChangedEventHandler PropertyChanged;

        private static void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null) handler(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
