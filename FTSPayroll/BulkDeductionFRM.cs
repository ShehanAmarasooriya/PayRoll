using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class BulkDeductionFRM : Form
    {

        public static String DeductionGrp;
        public static String DeductionGrpID;

        public static String Deduction;
        public static String DeductionID;

        public static String Division;
        String DivisionId;

        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();

        public BulkDeductionFRM(String _division,String _deductionGrp, String _deduction)
        {
            InitializeComponent();
        }

        private void BulkDeductionFRM_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = YMonth.getLastMonthID();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}