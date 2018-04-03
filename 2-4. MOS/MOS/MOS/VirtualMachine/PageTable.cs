namespace MOS.VirtualMachine
{
    public class PageTable
    {
        readonly string ptr;

        public PageTable(string ptr)
        {
            this.ptr = ptr;
        }

        public int getPtr()
        {
            return ptr.TwoLastbytesToHex();
        }

        public int RealAddress(int x)
        {
            string s = RealMachine.RealMachine.memory.StringAt(getPtr(), x);
            int k = RealMachine.RealMachine.memory.StringAt(getPtr(), x).ToHex();
            return RealMachine.RealMachine.memory.StringAt(getPtr(), x).ToHex();

        }
    }
}
