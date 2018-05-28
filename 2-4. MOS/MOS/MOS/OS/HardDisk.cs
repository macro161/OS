using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    public class HardDisk
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
    }
}
