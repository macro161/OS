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
    partial class VMForm : Form
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
            jg.Kernel.dynamicResources.First(res => res.Name == "LINEFROMUSER")
                .ReleaseResource(new IOResourceElements(textBoxUser.Text,"", 0, jg, null, this));
        }
    }
}
