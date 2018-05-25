using System;
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

        public static List<Program> ProgramList { get => programList; set {
                programList = value; RaisePropertyChangedEvent("ProgramList");
            } }

        public static event PropertyChangedEventHandler PropertyChanged;

        private static void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null) handler(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
