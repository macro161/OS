using System;
using System.Windows.Forms;

namespace MOS.RealMachine
{
    class RMStartPoint
    {
        [STAThread]
        public void Start()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // Application.Run(new RealMachineGUI());
        }
    }
}
