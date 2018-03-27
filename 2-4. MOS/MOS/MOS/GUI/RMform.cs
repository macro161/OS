using MOS.RealMachine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.Registers;

namespace MOS.GUI
{
    public partial class RMform : Form
    {
        //public static UserMemory memory = RealMachine.RealMachine.memory;
        //public static string[,] memoryArray = memory.UserMemoryProp;
        public RealMachine.RealMachine RM = new RealMachine.RealMachine();

        public RMform(RealMachine.RealMachine rm)
        {
            RM = rm;
            InitializeComponent();
        }

        private void RMform_Load(object sender, EventArgs e)
        {
            R1text.DataBindings.Add("Text", RM, "R1");
            R2text.DataBindings.Add("Text", RM, "R2");
            R3text.DataBindings.Add("Text", RM, "R3");
            R4text.DataBindings.Add("Text", RM, "R4");
            SFtext.DataBindings.Add("Text", RM, "SF");
            SItext.DataBindings.Add("Text", RM, "SI");
            TItext.DataBindings.Add("Text", RM, "TI");
            PTRtext.DataBindings.Add("Text", RM, "PTR");
            Ctext.DataBindings.Add("Text", RM, "C");
            ICtext.DataBindings.Add("Text", RM, "IC");
            IOItext.DataBindings.Add("Text", RM, "IOI");
            MODEtext.DataBindings.Add("Text", RM, "MODE");
            PItext.DataBindings.Add("Text", RM, "PI");
            
            LoadRMMemory();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void RMmemory_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadRMMemory()
        {
            //var file = new ChannelsDevice();
            //string[,] memoryArray = file.ReadFromFlash();
            UserMemory uMemory = RealMachine.RealMachine.memory;
            string[,] memoryArray = uMemory.UserMemoryProp; 
            RMmemory.Text = "\t0\t1\t2\t3\t4\t5\t6\t7\t8\t9\tA\tB\tC\tD\tE\tF\r\n";
            for (int block = 0; block <= 15; block++)
            {
                RMmemory.Text += block.ToHex().ToUpper() + "\t";
                for (int word = 0; word <= 15; word++)
                {
                    RMmemory.Text += memoryArray[block, word];
                    if (word != 15)
                        RMmemory.Text += "\t";
                }
                RMmemory.Text += "\r\n";
            }
            RMmemory.Update();
        }
        
    }
}
