using MOS.OS;
using MOS.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOS.GUI
{
    public partial class VMForm : Form
    {
        public JobGovernor jg;
        public List<string> list = new List<String>();
        public BindingSource Vm = new BindingSource();

        public VMForm(JobGovernor jg)
        {
            this.jg = jg;
            Vm.DataSource = jg;
            InitializeComponent();
            label1.Text = jg.name;
            jg.PropertyChanged += (sender, args) => { if (args.PropertyName == "VMList" && sender != this) { HandleChanged(); } };

        }

        void HandleChanged()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            textBox.BeginInvoke((MethodInvoker)delegate {
                textBox.Text += jg.VMList.Last();
                textBox.Text += "\r\n";
            });
        }


        private void textBoxUser_Enter(object sender, EventArgs e)
        {
        }

        private void textBoxUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxUser_Leave(object sender, EventArgs e)
        {
            jg.Kernel.dynamicResources.First(res => res.Name == "LINEFROMUSER").Elements.Add(new ResourceElement(value: textBoxUser.Text, receiver: jg));
        }
    }
}
