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
        public Guid id;

        public VMForm(Guid id)
        {
            this.id = id;
            InitializeComponent();
        }

        public void Print(Guid id, string line)
        {
            if(this.id == id)
            {
                textBox.AppendText(line);
                textBox.AppendText(Environment.NewLine);
            }
        }

        private void textBoxUser_Enter(object sender, EventArgs e)
        {

        }
    }
}
