using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace MOS.GUI
{
    public partial class RMform : Form
    {
        //public static UserMemory memory = RealMachine.RealMachine.memory;
        public string[,] memoryArray;
        public string[,] memoryArray2 = new string[0x256, 0x10];
        public string[,] VMArray = new string[16, 16];
        public RealMachine.RealMachine rm;
        DataTable _table = new DataTable();
        DataTable _table2 = new DataTable();
        private Timer timer1;
        public static List<string[]> ptrList = new List<string[]>();
        public BindingSource Rm = new BindingSource();

        public RMform(RealMachine.RealMachine rm)
        {
            this.rm = rm;
            Rm.DataSource = rm;
            rm.PropertyChanged += (sender, args) => { if (args.PropertyName == "Memory" && sender != this) HandleMemoryChanged(); };
            InitializeComponent();
            R1text.DataBindings.Add("Text", Rm, "R1");
            R2text.DataBindings.Add("Text", Rm, "R2");
            R3text.DataBindings.Add("Text", Rm, "R3");
            R4text.DataBindings.Add("Text", Rm, "R4");
            SFtext.DataBindings.Add("Text", Rm, "SF");
            SItext.DataBindings.Add("Text", Rm, "SI");
            TItext.DataBindings.Add("Text", Rm, "TI");
            PTRtext.DataBindings.Add("Text", Rm, "PTR");
            ICtext.DataBindings.Add("Text", Rm, "IC");
            IOItext.DataBindings.Add("Text", Rm, "IOI");
            MODEtext.DataBindings.Add("Text", Rm, "MODE");
            PItext.DataBindings.Add("Text", Rm, "PI");
            komanda.DataBindings.Add("Text", Rm, "Komanda");
            Pertraukimas.DataBindings.Add("Text", Rm, "Pertraukimas");
        }

        private void HandleMemoryChanged()
        {
            ptrList = rm.VMMemory;
            LoadDataGrid();
        }
        
        private void RMform_Load(object sender, EventArgs e)
        {

        }

        private void LoadDataGrid()
        {
            memoryArray = rm.Memory;

            _table = new DataTable();
            for (int block = 0; block < 16; block++)
                _table.Columns.Add(block.ToHex().ToUpper());

            int a = 0, b = 0;
            foreach (string[] cell in ptrList)
            {
                VMArray[a, b] = memoryArray[Int32.Parse(cell[0]), Int32.Parse(cell[1])];
                b++;
                if (b == 16)
                {
                    b = 0;
                    a++;
                    Debug.WriteLine(cell[0]);
                }
            }

            for (int outerIndex = 0; outerIndex < 16; outerIndex++)
            {
                DataRow newRow = _table.NewRow();
                for (int innerIndex = 0; innerIndex < 16; innerIndex++)
                {
                    newRow[innerIndex] = VMArray[outerIndex, innerIndex];
                }
                _table.Rows.Add(newRow);
            }

            dataGrid.DataSource = _table;
            for (var i = 0; i < 16; i++)
                dataGrid.Columns[i].Width = 37;
            _table.RowChanged += Row_Changed;
        }

        private void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            string[,] arr = rm.Memory;
            int a = 0, b = 0;
            foreach (string[] cell in ptrList)
            {
                arr[Int32.Parse(cell[0]), Int32.Parse(cell[1])] = _table.Rows[a][b].ToString();
                b++;
                if (b == 16)
                {
                    b = 0;
                    a++;
                }
            }
            rm.Memory = arr;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string path = ShowFileDialog();
            rm.VMMemory = new List<string[]>();

            _table = new DataTable();
            rm.LoadProgramToSupervisory(path);
            ptrList = rm.VMMemory;
            Rm.ResetBindings(true);
            LoadDataGrid();
            button4.Enabled = false;
            button3.Enabled = true;
            button2.Enabled = true;
        }

        private void ViewBlocktext_TextChanged(object sender, EventArgs e)
        {

        }

        private void viewBlockButton_Click(object sender, EventArgs e)
        {
            int block = ViewBlocktext.Text.ToHex();
            memoryArray = rm.Memory;

            _table = new DataTable();

            for (int b = 0; b < 16; b++)
                _table.Columns.Add(b.ToHex().ToUpper());

            DataRow newRow = _table.NewRow();
            for (int i = 0; i < 16; i++)
            {
                newRow[i] = memoryArray[block, i];
                if (newRow[i] == null)
                    newRow[i] = "";
            }
            _table.Rows.Add(newRow);
            viewBlockGrid.DataSource = _table;
            for (var i = 0; i < 16; i++)
                viewBlockGrid.Columns[i].Width = 37;
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

        private void button2_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            var t = new Thread((ThreadStart)(() =>
            {
                if (RealMachine.RealMachine.run)
                    rm.RunCode = true;
            }));
            t.Start();
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 500; // in miliseconds
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Rm.ResetBindings(true);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            rm.Pertraukimas = "";
            button4.Enabled = false;
            if (RealMachine.RealMachine.run)
            {
            button3.Text = "Next";
            rm.Next = true;
            Rm.ResetBindings(true);
            LoadDataGrid();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            rm.RunCode = false;
            Rm.ResetBindings(true);
            LoadDataGrid();
        }
    }
}
