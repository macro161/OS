using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Resource
    {
        public string name;
        public string creator;
        public int elements;
        public string data;

        public Resource(string name, string creator, int elements, string data)
        {
            this.name = name;
            this.creator = creator;
            this.elements = elements;
            this.data = data;
        }
    }
}
