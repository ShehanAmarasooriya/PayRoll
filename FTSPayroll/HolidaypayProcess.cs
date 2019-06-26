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
    public partial class HolidaypayProcess : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();

        public HolidaypayProcess()
        {
            InitializeComponent();
        }

        private void HolidaypayProcess_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();
            cmbYear.Enabled = false;

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Process Holidaypay Of\r\nYear:" + cmbYear.SelectedValue.ToString() + "\r\nDivision:" + cmbDivision.SelectedValue.ToString(), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //this.Close();
                //HolidaypayConfirmation HPayConfirm = new HolidaypayConfirmation(this);
                //HPayConfirm.Show();

                String state = "";
                try
                {
                    HoliPay.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    HoliPay.StrDivision = cmbDivision.SelectedValue.ToString();
                    state=HoliPay.processHolidaypay(HoliPay.IntYear, HoliPay.StrDivision);
                    if (state.Equals("COMPLETED"))
                    {
                        MessageBox.Show(HoliPay.StrDivision + " Division - Holidaypay Processed Successfully!");
                    }
                    else
                    {
                        MessageBox.Show(HoliPay.StrDivision + " Division - Holidaypay Process Failed!\r\nPlease Inform To OLAX Team","Failed!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Process Failed, "+ex.Message,"Error!");
                }
            }
        }
    }
}