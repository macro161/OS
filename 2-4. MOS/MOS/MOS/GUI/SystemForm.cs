using MOS.OS;
using MOS.Resources;
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
        public BindingSource sys = new BindingSource();
        public static List<string> list = new List<string>();
        public List<string> names = new List<string>();
        public Kernel Kernel { get; set; }
        public string path;

        public SystemForm(Kernel kernel)
        {
            Kernel = kernel;
            InitializeComponent();
            sys.DataSource = kernel;
            kernel.PropertyChanged += (sender, args) => { if (args.PropertyName == "ProgramList" && sender != this) { HandleChanged(); } };
        }

        void HandleChanged()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            listBox1.BeginInvoke((MethodInvoker)delegate {
                listBox1.Items.Clear();
                foreach (var ln in Kernel.ProgramList)
                {
                    listBox1.Items.Add(ln);
                }
            });
        }

        public static void SetPrograms(List<string> names)
        {
            list = names;
        }

        private void mountFlashB_Click(object sender, EventArgs e)
        {
            path = ShowFileDialog();
            Kernel.dynamicResources.First(res => res.Name == "FILEINPUT").Elements.Add(new Resources.ResourceElement(path));
        }

        private void runAllProgramsB_Click(object sender, EventArgs e)
        {

            Kernel.dynamicResources.First(res => res.Name == "FILEINPUT").Elements.Add(new Resources.ResourceElement(path));
        }

        private void runSelectedB_Click(object sender, EventArgs e)
        {

        }

        public static string ShowFileDialog()
        {
            string selectedPath = "";
            var t = new Thread(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.ShowDialog();
                selectedPath = openFileDialog.FileName;
                Console.WriteLine(openFileDialog.FileName);
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            t.Join();
            return selectedPath;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Kernel.dynamicResources.First(res => res.Name == "TASKINDISK").Elements.Add(new ResourceElement(value: listBox1.SelectedItem.ToString()));
        }
    }
}
