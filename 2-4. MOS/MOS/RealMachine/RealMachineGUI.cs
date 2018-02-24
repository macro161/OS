using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealMachine
{
    public partial class RealMachineGUI : Form
    {
        
        
        RealMachineModel rm = new RealMachineModel();

        public RealMachineGUI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void CH1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Stop_Program_Button_Click(object sender, EventArgs e)
        {

        }

        private void Load_Program_Button_Click(object sender, EventArgs e)
        {
            ReDrawRMGUI();
        }

        private void RealMachineGUI_Load(object sender, EventArgs e)
        {
            
        }

        private void ReDrawRMGUI()
        {
            R1_Value_Box.Text = rm.r1.R.ToString();
            R2_Value_Box.Text = rm.r2.R.ToString();
            R3_Value_Box.Text = rm.r3.R.ToString();
            R4_Value_Box.Text = rm.r4.R.ToString();
            IC_Value_Box.Text = rm.ic.IC.ToString();
            C_Value_Box.Text = rm.c.C.ToString();
            SF_Value_Box.Text = rm.sf.Get_SF().ToString();
            PTR_Value_Box.Text = rm.ptr.PTR.ToString();
            MODE_Value_Box.Text = rm.mode.Mode.ToString();
            CH1_Value_Box.Text = rm.ch.CH1.ToString();
            CH2_Value_Box.Text = rm.ch.CH2.ToString();
            CH3_Value_Box.Text = rm.ch.CH3.ToString();
            PI_Value_Box.Text = rm.pi._pi.ToString();
            SI_Value_Box.Text = rm.si._si.ToString();
            TI_Value_Box.Text = rm.ti._ti.ToString();
            IOI_Value_Box.Text = rm.ioi._ioi.ToString();
            DS_Value_Box.Text = rm.ds._ds.ToString();
            CS_Value_Box.Text = rm.cs._cs.ToString();

        }
    }
}
