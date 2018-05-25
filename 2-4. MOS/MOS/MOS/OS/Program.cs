using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Program
    {
        public string name;
        public List<string> dataSegment;
        public List<string> codeSegment;

        public Program(string name, List<string> dataSegment, List<string> codeSegment)
        {
            this.name = name;
            this.dataSegment = dataSegment;
            this.codeSegment = codeSegment;
        }
    }
}
