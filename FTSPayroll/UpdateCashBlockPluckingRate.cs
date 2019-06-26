using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class UpdateCashBlockPluckingRate : Form
    {
        FTSPayRollBL.YearMonth myYear = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Division myDiv = new FTSPayRollBL.Division();
        FTSPayRollBL.DailyHarvest myDailyHavest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.ProcessMonthlyWages proMWages = new FTSPayRollBL.ProcessMonthlyWages();

        public UpdateCashBlockPluckingRate()
        {
            InitializeComponent();
        }

        private void UpdateBlockPluckingRate_Load(object sender, EventArgs e)
        {
            txtYear.Text =  DateTime.Now.Year.ToString();
            txtMonth.Text =   DateTime.Now.Month.ToString();

            cmbDivision.DataSource = myDiv.ListDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!proMWages.IsProcessed(cmbDivision.SelectedValue.ToString(),Convert.ToInt32( txtYear.Text),Convert.ToInt32( txtMonth.Text)) )
                {

                    if (chkAllDiv.Checked)
                    {
                        myDailyHavest.StrDivision = "%";
                    }
                    else
                    {
                        myDailyHavest.StrDivision = cmbDivision.SelectedValue.ToString();
                    }
                    myDailyHavest.IntYear = Convert.ToInt32(txtYear.Text);
                    myDailyHavest.IntMonth = Convert.ToInt32(txtMonth.Text);
                    myDailyHavest.DecBockPluckRate = Convert.ToDecimal(txtRate.Text);
                    myDailyHavest.updateBlockPluckingRate();

                    MessageBox.Show(" Updated successfully");
                }


                else
                {
                    MessageBox.Show(" Already Processed Thhis Month");
                }
            }

            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message);
            }
        }
    }
}