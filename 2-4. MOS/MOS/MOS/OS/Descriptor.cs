using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Registers;
using MOS.VirtualMachine;

namespace MOS.OS
{
    class Descriptor
    {
        public Guid descriptorId;

        public IC_Reg IC;
        public Mode_Reg MODE;
        public PTR_Reg PTR;
        public R_Reg R1;
        public R_Reg R2;
        public R_Reg R3;
        public R_Reg R4;
        public SF_Reg SF;
        public TI_Reg TI;
        public PageTable pageTable;

        public void SaveVMState(VirtualMachine.VirtualMachine virtualMachine) {
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

        public void LoadVMState(VirtualMachine.VirtualMachine virtualMachine) {
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
