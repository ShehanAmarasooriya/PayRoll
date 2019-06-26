using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FieldList : Form
    {
        FTSPayRollBL.Field myField = new FTSPayRollBL.Field();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.EstateDivisionBlock myEstateDivisionBlock = new FTSPayRollBL.EstateDivisionBlock();
        DailyHarvest DHNormal = new DailyHarvest();
        DailyHarvestCW DHCash = new DailyHarvestCW();
        DailyHarvestCW1 DHNamePlk = new DailyHarvestCW1();
        MusterChitEntry MChit = new MusterChitEntry();

        public int formType = 1;
        String strFDivision = "%";

        public FieldList()
        {
            InitializeComponent();
        }

        public FieldList(MusterChitEntry MC, String strDiv)
        {
            MChit=MC;
            strFDivision = strDiv;
            formType = 1;
            InitializeComponent();
            try
            {
                cmbDivision.SelectedValue = strFDivision;
                cmbDivison_SelectedIndexChanged(null, null);
            }
            catch { }

        }

        public FieldList(DailyHarvest DH,String strDiv)
        {
            DHNormal = DH;
            cmbDivision.SelectedValue = strDiv;
            formType = 2;
            InitializeComponent();
        }
        public FieldList(DailyHarvestCW DHC, String strDiv)
        {
            DHCash = DHC;
            cmbDivision.SelectedValue = strDiv;
            formType = 3;
            InitializeComponent();
        }
        public FieldList(DailyHarvestCW1 DHC1, String strDiv)
        {
            DHNamePlk = DHC1;
            cmbDivision.SelectedValue = strDiv;
            formType = 4;
            InitializeComponent();
        }

        private void FieldList_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivison_SelectedIndexChanged(null, null);

        }

        private void cmbDivison_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = myField.ListFieldDetails(cmbDivision.SelectedValue.ToString());
            }
            catch { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = myField.SearchFieldDetails(txtSearchKeyWord.Text,cmbDivision.SelectedValue.ToString());
            }
            catch { }
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (formType)
            {
                case 1:
                    {
                        MChit.cmbField.SelectedValue= gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
                        DataSet ds = new DataSet();
                        ds = myEstateDivisionBlock.getFieldName(gvList.Rows[e.RowIndex].Cells[1].Value.ToString(), cmbDivision.SelectedValue.ToString());
                        if (ds.Tables.Count != 0)
                        {
                            MChit.lblFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
                        }
                        ds.Dispose();
                        MChit.cmbField.Focus();
                        this.Close();
                        break;
                    }
               
                default:
                    {
                        MessageBox.Show("Invalid request!");
                        break;
                    }
            }
        }

        private void txtSearchKeyWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSearch.PerformClick();
        }
    }
}