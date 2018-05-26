using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    public class Program
    {
        public string name { get; set; }
        public List<string> dataSegment { get; set; }
        public List<string> codeSegment { get; set; }

        public Program(string name, List<string> dataSegment, List<string> codeSegment)
        {
            this.name = name;
            this.dataSegment = dataSegment;
            this.codeSegment = codeSegment;
        }
    }
}
