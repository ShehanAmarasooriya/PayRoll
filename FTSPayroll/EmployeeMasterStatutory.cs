using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeMasterStatutory : Form
    {
        FTSPayRollBL.EstateDivisionBlock myDivision = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeCategory myCategory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();

        public EmployeeMasterStatutory()
        {
            InitializeComponent();
        }

        private void EmployeeMasterStatutory_Load(object sender, EventArgs e)
        {

            cmbDivision.DataSource = myDivision.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";
            
            cmbCategory.DataSource = myCategory.ListCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            cmbEmpStatus.DataSource = FTSSettings.ListDataFromSettings("EmpStatus");
            cmbEmpStatus.DisplayMember = "Name";
            cmbEmpStatus.ValueMember = "Code";

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {

        }

        
    }
}