using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class RFTRateMaster : Form
    {
        FTSPayRollBL.DeductionMaster objDeduct = new FTSPayRollBL.DeductionMaster();
        public RFTRateMaster()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RFTRateMaster_Load(object sender, EventArgs e)
        {
            bindData();
        }

        public void bindData()
        {
            try
            {
                gvRates.DataSource = objDeduct.GetFoodStuffRates().Tables[0];
                gvRates.Columns[0].ReadOnly = true;
                gvRates.Columns[1].ReadOnly = true;
                gvRates.Columns[2].ReadOnly = true;
                gvRates.Columns[3].ReadOnly = true;
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= gvRates.Rows.Count-1; i++)
            {
                try
                {
                    Int32 str1 = Convert.ToInt32(gvRates.Rows[i].Cells[0].Value.ToString());
                    Decimal str2 = Convert.ToDecimal(gvRates.Rows[i].Cells[4].Value.ToString());
                    objDeduct.SaveFoodStuffRates(str1, str2);
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Save Rate For " + gvRates.Rows[i].Cells[3].ToString() + " Failed...");
                }
            }
            MessageBox.Show("Completed");
        }
    }
}