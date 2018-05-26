using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Semaphore
    {
        private Guid processThatHasAccess;
        public int trackNumber;

        public bool Block(Guid id) {
            if (processThatHasAccess == default)
            {
                processThatHasAccess = id;
                return true;
            }
            return false;
        }

        public void Release()
        {
            processThatHasAccess = default;
        }
    }
}
