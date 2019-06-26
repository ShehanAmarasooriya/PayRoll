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
    public partial class UpdateAttendanceBonus : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        public UpdateAttendanceBonus()
        {
            InitializeComponent();
        }

        private void UpdateAttendanceBonus_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = "2012";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";


        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                cmbEmpNo.DataSource = myEmp.ListAllEmployees(cmbDivision.SelectedValue.ToString());
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";
            }
            catch { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)

        {
            Decimal decAvailAmount = 0;
            if (String.IsNullOrEmpty(txtAmount.Text))
            {
                MessageBox.Show("Amount Cannot be Empty");
            }
            else
            {
                try
                {
                    decAvailAmount = HoliPay.getAvailableAttIncentiveAmount(Convert.ToInt32(cmbYear.Text), cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString());
                    HoliPay.UpdateAttBonus(Convert.ToInt32(cmbYear.Text), cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString(), Convert.ToDecimal(txtAmount.Text),decAvailAmount);
                    MessageBox.Show("Updated Successfully.");
                    txtAmount.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ," + ex.Message);
                }
            }
        }
    }
}