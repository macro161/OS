using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Registers;
using MOS.VirtualMachine;

namespace MOS.OS
{
    public class Descriptor
    {
        public Guid descriptorId;

        public IC_Reg IC { get; set; }
        public Mode_Reg MODE { get; set; }
        public PTR_Reg PTR { get; set; }
        public R_Reg R1 { get; set; }
        public R_Reg R2 { get; set; }
        public R_Reg R3 { get; set; }
        public R_Reg R4 { get; set; }
        public SF_Reg SF { get; set; }
        public TI_Reg TI { get; set; }
        public PageTable pageTable { get; set; }
        public Descriptor(string ptr)
        {
            IC = new IC_Reg();
            MODE = new Mode_Reg();
            PTR = new PTR_Reg
            {
                PTR = ptr
            };
            R1 = new R_Reg();
            R2 = new R_Reg();
            R3 = new R_Reg();
            R4 = new R_Reg();
            SF = new SF_Reg();
            TI = new TI_Reg();
            pageTable = new PageTable(ptr);

        }

        public void SaveVMState(VirtualMachine.VirtualMachine virtualMachine)
        {
            IC = virtualMachine.IC;
            MODE = virtualMachine.MODE;
            PTR = virtualMachine.PTR;
            R1 = virtualMachine.R1;
            R2 = virtualMachine.R2;
            R3 = virtualMachine.R3;
            R4 = virtualMachine.R4;
            SF = virtualMachine.SF;
            TI = virtualMachine.TI;
            pageTable = virtualMachine.pt;

        }

        public void LoadVMState(VirtualMachine.VirtualMachine virtualMachine)
        {
            virtualMachine.IC = IC;
            virtualMachine.MODE = MODE;
            virtualMachine.PTR = PTR;
            virtualMachine.R1 = R1;
            virtualMachine.R2 = R2;
            virtualMachine.R3 = R3;
            virtualMachine.R4 = R4;
            virtualMachine.SF = SF;
            virtualMachine.TI = TI;
            virtualMachine.pt = pageTable;

        }
    }
}
