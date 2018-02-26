using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMachine
{
    class InputChannel
    {
        public void GetData(UserMemory loadMemory)
        {
            string memory = "";

            FlashMemory flash = new FlashMemory();

            memory = flash.memory;

            loadMemory.TakeData(memory);
        }
    }
}
