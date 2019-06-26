using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeWiseEpfEtfSummery2 : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        public EmployeeWiseEpfEtfSummery2()
        {
            InitializeComponent();
        }

        private void EmployeeWiseEpfEtfSummery2_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbEmployeeCategory.DataSource = myCatagory.ListCategories();
            cmbEmployeeCategory.DisplayMember = "CategoryName";
            cmbEmployeeCategory.ValueMember = "CategoryID";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {

        }
    }
}