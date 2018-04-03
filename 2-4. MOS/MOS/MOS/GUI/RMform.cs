using MOS.RealMachine;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace MOS.GUI
{
    public partial class RMform : Form
    {
        //public static UserMemory memory = RealMachine.RealMachine.memory;
        //public static string[,] memoryArray = memory.UserMemoryProp;
        public RealMachine.RealMachine Rm;
        DataTable _table = new DataTable();

        public RMform(RealMachine.RealMachine rm)
        {
            Rm = rm;
            Rm.PropertyChanged += (sender, args) => { if(args.PropertyName == "Memory" && sender!=this) HandleMemoryChanged(); };
            InitializeComponent();
            R1text.DataBindings.Add("Text", Rm, "R1");
            R2text.DataBindings.Add("Text", Rm, "R2");
            R3text.DataBindings.Add("Text", Rm, "R3");
            R4text.DataBindings.Add("Text", Rm, "R4");
            SFtext.DataBindings.Add("Text", Rm, "SF");
            SItext.DataBindings.Add("Text", Rm, "SI");
            TItext.DataBindings.Add("Text", Rm, "TI");
            PTRtext.DataBindings.Add("Text", Rm, "PTR");
            Ctext.DataBindings.Add("Text", Rm, "C");
            ICtext.DataBindings.Add("Text", Rm, "IC");
            IOItext.DataBindings.Add("Text", Rm, "IOI");
            MODEtext.DataBindings.Add("Text", Rm, "MODE");
            PItext.DataBindings.Add("Text", Rm, "PI");
            LoadDataGrid();
        }

        private void HandleMemoryChanged()
        {
            Debug.WriteLine("*******");
            UpdateTable();
        }

        void UpdateTable()
        {
            _table = new DataTable();
            for (int block = 0; block < 16; block++)
                _table.Columns.Add(block.ToHex());
            for (int outerIndex = 0; outerIndex < 16; outerIndex++)
            {
                DataRow newRow = _table.NewRow();
                for (int innerIndex = 0; innerIndex < 16; innerIndex++)
                {
                    newRow[innerIndex] = Rm.Memory[outerIndex, innerIndex];
                }
                _table.Rows.Add(newRow);
            }
            dataGrid.DataSource = _table;
        }

        private void RMform_Load(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void RMmemory_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadDataGrid()
        {
            var file = new ChannelsDevice();
            string[,] memoryArray = file.ReadFromFlash();
            
            for (int block = 0; block < 16; block++)
               _table.Columns.Add(block.ToHex());

            for (int outerIndex = 0; outerIndex < 16; outerIndex++)
            {
                DataRow newRow = _table.NewRow();
                for (int innerIndex = 0; innerIndex < 16; innerIndex++)
                {
                    newRow[innerIndex] = memoryArray[outerIndex, innerIndex];
                }
                _table.Rows.Add(newRow);
            }
            dataGrid.DataSource = _table;
            for(var i = 0;i<16;i++)
                dataGrid.Columns[i].Width = 37;
            _table.RowChanged += Row_Changed;
            Rm.Memory = memoryArray;
            string[,] whatever = Rm.Memory;
            whatever[0, 1] = "5";
            Rm.Memory = whatever;
            //Debug.WriteLine(RM.Memory[0, 1]);
        }
        private void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            string[,] arr = new string[16, 16];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    arr[i, j] = _table.Rows[i][j].ToString();
                }
            }
            //RealMachine.RealMachine.memory.UserMemoryProp = arr;
            Rm.Memory = arr;
        }

        private void R2label_Click(object sender, EventArgs e)
        {
           // RM.PowerOn();
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(Rm.Memory[0, 0]);
        }
    }
}
