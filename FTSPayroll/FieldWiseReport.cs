using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FieldWiseReport : Form
    {
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myYear = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CHKWages myWage = new FTSPayRollBL.CHKWages();
        FTSPayRollBL.Field myField = new FTSPayRollBL.Field();
        
        public FieldWiseReport()
        {
            InitializeComponent();
        }

        private void FieldWiseReport_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = myYear.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = myYear.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            //cmbFieldType.DataSource = myField.ListAllFieldsTypes();
            //cmbFieldType.DisplayMember = "Type";
            //cmbFieldType.ValueMember = "Type";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strFieldCategory="";
            if (cmbFieldType.SelectedValue.ToString() == "C")
            {
                strFieldCategory = "Capital Fields";
            }
            else if (cmbFieldType.SelectedValue.ToString() == "O")
            {
                strFieldCategory = "Other Fields";
            }
            else if (cmbFieldType.SelectedValue.ToString() == "R")
            {
                strFieldCategory = "Revenue Fields";
            }

            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();

            DataTable dt = myYear.getOpenCloseDates(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()));

            dt1 = myWage.FieldWiseCheckrollWages(Convert.ToDateTime(dt.Rows[0][0].ToString()), Convert.ToDateTime(dt.Rows[0][1].ToString()), cmbFieldType.SelectedValue.ToString());
            ds.Tables.Add(dt1);

            if (dt1.Rows.Count>0)
            {
                ds.WriteXml("FieldWiseCheckrollWages.xml");
                FieldWiseCheckRollWages myReport = new FieldWiseCheckRollWages();
                myReport.SetDataSource(ds);

                ReportViewer myViewer = new ReportViewer();
                myReport.SetParameterValue("Company",FTSPayRollBL.Company.getCompanyName());
                myReport.SetParameterValue("Estate", "Estate : " + FTSPayRollBL.User.StrEstate);
                myReport.SetParameterValue("FieldType", "Field : " + strFieldCategory);
                myReport.SetParameterValue("Date", "Month of : " + cmbMonth.Text + " / " + cmbYear.Text);
                myViewer.crystalReportViewer1.ReportSource = myReport;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }
    }
}