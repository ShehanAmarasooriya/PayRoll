using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;

namespace FTSPayroll
{
    public partial class OpenBlockedDates : Form
    {
        BlockEntries myEntries = new BlockEntries();
        public OpenBlockedDates()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date < dateTimePicker2.Value.Date)
            {
                myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
                myEntries.DtCloseDate = dateTimePicker2.Value.Date;
                myEntries.OpenBlockedDate();
                gvList.DataSource = myEntries.ListOpenedBlockedDates();
            }
            else
            {
                MessageBox.Show("Close Date Invalid. ");
            }
        }

        private void OpenBlockedDates_Load(object sender, EventArgs e)
        {
            if (User.StrUserName == "hoadmin")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            dateTimePicker2.Value = DateTime.Now.AddDays(2);
            gvList.DataSource = myEntries.ListOpenedBlockedDates();
            
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimePicker1.Value=Convert.ToDateTime( gvList.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            myEntries.BlockBlockedDate();
            gvList.DataSource = myEntries.ListOpenedBlockedDates();
        }
    }
}