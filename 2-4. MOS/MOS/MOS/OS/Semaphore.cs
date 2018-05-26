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
        private int pointer = 0;

        private List<Guid> processesInQueue = new List<Guid>();

        public Semaphore(int trackNumber)
        {
            this.trackNumber = trackNumber;
        }

        public void Block(Guid id) {

            processesInQueue.Add(id);
        }

        public bool Write(Guid id) {
            processThatHasAccess = processesInQueue[pointer];

            if (processThatHasAccess == id)
            {
                pointer++;
                return true;
            }
            return false;
        }
    }
}
