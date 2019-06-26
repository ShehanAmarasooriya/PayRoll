using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class MonthlyHoliday : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.MonthlyHoliday myHoli = new FTSPayRollBL.MonthlyHoliday();  
 
        public MonthlyHoliday()
        {
            InitializeComponent();
        }

        private void btnDELETE_Click(object sender, EventArgs e)
        {
            txtHoliName.Clear();

            //cmbMonth.SelectedIndex = 0;
           
            cmbHoliType.SelectedIndex = 0;
            button1.Enabled = false;
            btnADD.Enabled = true;

            //gvlist.DataSource = myHoli.ListMHoliday(intYear,intMonth);
            refreshGrid();
            
        }

        private void MonthlyHoliday_Load(object sender, EventArgs e)
        {
            //cmbMonth.DataSource = myMonth.ListMonths();
            //cmbMonth.DisplayMember = "Month";
            //cmbMonth.ValueMember = "MID";
            

            cmbHoliType.DataSource = myHoli.ListHolidayType();
            cmbHoliType.DisplayMember = "Name";
            cmbHoliType.ValueMember = "Name";

            //cmbYear.SelectedIndex = 0;
            button1.Enabled = false;

            //gvlist.DataSource = myHoli.ListMHoliday(intYear, intMonth);
            refreshGrid();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHoliName.Text == "")
                {
                    MessageBox.Show("Enter Holiday Name to proceed..!");
                    txtHoliName.Focus();
                }
                else
                {
                    myHoli.Dtday = dateTimePicker1.Value.Date;
                    myHoli.StrHoliType = cmbHoliType.SelectedValue.ToString();
                    myHoli.StrHoliName = txtHoliName.Text;
                    myHoli.IntYear = Convert.ToInt32(dateTimePicker1.Value.Year);
                    myHoli.IntMID = Convert.ToInt32(dateTimePicker1.Value.Month);

                    myHoli.InsertMHoliday();

                    MessageBox.Show("Holiday Created Successfully..!");
                    btnClear.PerformClick();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtHoliName.Text == "")
                {
                    MessageBox.Show("Select Date to Delete..!");
                }
                else
                {
                    myHoli.Dtday = dateTimePicker1.Value.Date;
                    myHoli.StrHoliType = cmbHoliType.SelectedValue.ToString();
                    myHoli.DeleteMHoliday();

                    MessageBox.Show("Holiday Date deleted Successfully..!");
                    btnClear.PerformClick();
                   
                }
            }
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(gvlist.Rows[e.RowIndex].Cells[0].Value.ToString());            
            cmbHoliType.SelectedValue = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();

            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[2].Value.ToString()))
            {
                txtHoliName.Text = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            }

            button1.Enabled = true;
            btnADD.Enabled = false;
            
        }

        //private void cmbHoliType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if ((cmbHoliType.SelectedValue.ToString()) == "Sunday")
        //    {
        //        txtHoliName.Enabled = false;
        //        txtHoliName.Text = "Sunday";
        //    }
        //    else if ((cmbHoliType.SelectedValue.ToString()) == "Independant Day")
        //    {
        //        txtHoliName.Enabled = false;
        //        txtHoliName.Text = "Independant Day";
        //    }
        //    else
        //    {
        //        txtHoliName.Enabled = true;
        //        txtHoliName.Clear();
        //    }
        
        //}

        public void refreshGrid()
        {
            Int32 intYear = Convert.ToInt32(myMonth.GetLastYearMonth().Rows[0][0].ToString());
            Int32 intMonth = Convert.ToInt32(myMonth.GetLastYearMonth().Rows[0][1].ToString());
            if (chkThisMonth.Checked)
            {
                gvlist.DataSource = myHoli.ListMHoliday(intYear, intMonth);
            }
            else
            {
                gvlist.DataSource = myHoli.ListMHoliday(intYear);
            }

        }

        private void chkThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            refreshGrid();

        }
    }
}