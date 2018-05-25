using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOS.GUI
{
    public partial class SystemForm : Form
    {
        public SystemForm()
        {
            InitializeComponent();
        }

        private void mountFlashB_Click(object sender, EventArgs e)
        {
            string path = ShowFileDialog();
        }

        private void runAllProgramsB_Click(object sender, EventArgs e)
        {

        }

        private void runSelectedB_Click(object sender, EventArgs e)
        {

        }

        public static string ShowFileDialog()
        {
            string selectedPath = "";
            var t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.ShowDialog();
                selectedPath = openFileDialog.FileName;
                Console.WriteLine(openFileDialog.FileName);
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            t.Join();
            return selectedPath;
        }
    }
}
