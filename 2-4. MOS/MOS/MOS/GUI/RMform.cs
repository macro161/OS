using MOS.RealMachine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace MOS.GUI
{
    public partial class RMform : Form
    {
        //public static UserMemory memory = RealMachine.RealMachine.memory;
        //public static string[,] memoryArray = memory.UserMemoryProp;
        public RealMachine.RealMachine RM;
        DataTable table = new DataTable();

        public RMform(RealMachine.RealMachine rm)
        {
            RM = rm;
            RM.PropertyChanged += (sender, args) => { if(args.PropertyName == "Memory" && sender!=this) HandleMemoryChanged(); };
            InitializeComponent();
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
            LoadDataGrid();
        }

        private void HandleMemoryChanged()
        {
            Debug.WriteLine("*******");
            UpdateTable();
        }

        void UpdateTable()
        {
            table = new DataTable();
            for (int block = 0; block < 16; block++)
                table.Columns.Add(block.ToHex());
            for (int outerIndex = 0; outerIndex < 16; outerIndex++)
            {
                DataRow newRow = table.NewRow();
                for (int innerIndex = 0; innerIndex < 16; innerIndex++)
                {
                    newRow[innerIndex] = RM.Memory[outerIndex, innerIndex];
                }
                table.Rows.Add(newRow);
            }
            dataGrid.DataSource = table;
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
               table.Columns.Add(block.ToHex());

            for (int outerIndex = 0; outerIndex < 16; outerIndex++)
            {
                DataRow newRow = table.NewRow();
                for (int innerIndex = 0; innerIndex < 16; innerIndex++)
                {
                    newRow[innerIndex] = memoryArray[outerIndex, innerIndex];
                }
                table.Rows.Add(newRow);
            }
            dataGrid.DataSource = table;
            for(var i = 0;i<16;i++)
                dataGrid.Columns[i].Width = 37;
            table.RowChanged += new DataRowChangeEventHandler(Row_Changed);
            RM.Memory = memoryArray;
            string[,] whatever = RM.Memory;
            whatever[0, 1] = "5";
            RM.Memory = whatever;
            //Debug.WriteLine(RM.Memory[0, 1]);
        }
        private void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            string[,] arr = new string[16, 16];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    arr[i, j] = table.Rows[i][j].ToString();
                }
            }
            //RealMachine.RealMachine.memory.UserMemoryProp = arr;
            RM.Memory = arr;
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
            Debug.WriteLine(RM.Memory[0, 0]);
        }
    }
}
