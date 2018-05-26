using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    public class Semaphore
    {
        private Guid processThatHasAccess;
        private int trackNumber;

        public Semaphore()
        {
            processThatHasAccess = Guid.Empty;
            trackNumber = 0;
        }

        public bool Block(Guid id)
        {

            if (processThatHasAccess == Guid.Empty)
            {
                processThatHasAccess = id;
                return true;
            }
            return false;
        }

        public void Release()
        {
            processThatHasAccess = Guid.Empty;
        }
    }
}
