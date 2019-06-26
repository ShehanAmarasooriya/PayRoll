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
    public partial class ResetHolidaypay : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        public ResetHolidaypay()
        {
            InitializeComponent();
        }

        private void ResetHolidaypay_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            if (User.StrUserName.ToUpper().Equals("DIT") || User.StrUserName.ToUpper().Equals("ADMIN")) 
            {
                btnCancelConfirm.Enabled = true;
                btnReset.Enabled = true;
            }
            else
            {
                btnCancelConfirm.Enabled = false;
                btnReset.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cancel Confirmation ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    HoliPay.CancelConfirmation(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                    MessageBox.Show("Successfully Canceled Confirmation .");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cancel Confirmation Failed\r\n" + ex.Message, "Error");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset Holidaypay?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    HoliPay.ResetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                    MessageBox.Show("Successfully Reset Holidaypay Data");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Reset Failed\r\n" + ex.Message, "Error");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}