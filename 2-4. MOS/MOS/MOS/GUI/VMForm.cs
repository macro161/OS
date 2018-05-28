using MOS.OS;
using MOS.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOS.GUI
{
    public partial class VMForm : Form
    {
        public JobGovernor jg;

        public VMForm(JobGovernor jg)
        {
            this.jg = jg;
            InitializeComponent();
        }

        public void Print(string line)
        {
            textBox.AppendText(line);
            textBox.AppendText(Environment.NewLine);
        }

        private void textBoxUser_Enter(object sender, EventArgs e)
        {
           // Console.WriteLine(textBoxUser.Text);
           // jg.Kernel.dynamicResources.First(res => res.Name == "LINEFROMUSER").Elements.Add(new ResourceElement(textBoxUser.Text, receiver:jg));
        }

        private void textBoxUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxUser_Leave(object sender, EventArgs e)
        {
            //Console.WriteLine(textBoxUser.Text);
            jg.Kernel.dynamicResources.First(res => res.Name == "LINEFROMUSER").Elements.Add(new ResourceElement(value : textBoxUser.Text, receiver: jg));
        }
    }
}
