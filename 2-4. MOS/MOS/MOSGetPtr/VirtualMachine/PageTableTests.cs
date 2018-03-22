using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOS.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.VirtualMachine.Tests
{
    [TestClass()]
    public class PageTableTests
    {
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

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
            string ptr = RealMachine.memory.getMemory();
            string[] t = new string[16];
            TestContext.WriteLine(ptr);
            for (int i = 0; i < 16; i++)
            {
                t[i]= RealMachine.memory.StringAt(ptr.TwoLastSymbolsToHex(), i);
                TestContext.WriteLine(t[i].ToHex().ToString());
            }
            PageTable pt = new PageTable(ptr);
            int x = pt.RealAddress(5);
            RealMachine.memory.WriteAt(x, 14, "TEST");
            TestContext.WriteLine(RealMachine.memory.StringAt(x, 14));
            
        }
    }
}