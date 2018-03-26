using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOS;
using MOS.VirtualMachine;

namespace MOSGetPtr.VirtualMachine
{
    [TestClass()]
    public class PageTableTests
    {
        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void PageTableTest()
        {

            Assert.Fail();
        }

        [TestMethod()]
        public void getPtrTest()
        {
            PageTable pt = new PageTable("45E5");

            Assert.AreEqual(289, pt.getPtr());
        }

        [TestMethod()]
        public void HexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RealAddressTest()
        {
            string ptr = MOS.RealMachine.RealMachine.memory.getMemory();
            string[] t = new string[16];
            TestContext.WriteLine(ptr);
            for (int i = 0; i < 16; i++)
            {
                t[i]= MOS.RealMachine.RealMachine.memory.StringAt(ptr.TwoLastbytesToHex(), i);
                TestContext.WriteLine(t[i].ToHex().ToString());
            }
            PageTable pt = new PageTable(ptr);
            int x = pt.RealAddress(5);
            MOS.RealMachine.RealMachine.memory.WriteAt(x, 14, "TEST");
            TestContext.WriteLine(MOS.RealMachine.RealMachine.memory.StringAt(x, 14));
            
        }
    }
}