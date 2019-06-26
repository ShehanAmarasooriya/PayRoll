using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class ACSubCategoryList : Form
    {
        FTSPayRollBL.AccountInformation DHAccounts = new FTSPayRollBL.AccountInformation();
        DailyHarvest objDailyHarvest;
        DailyHarvestCW objDailyHarvestCW;
        DailyHarvestCW1 objDailyHarvestCW1;
        MusterChitEntry objMC;
        MapEntryToAccounts objMapEntry;
        OverTime objOT;
        public static Int32 intFormID = 0;

        public ACSubCategoryList()
        {
            InitializeComponent();
        }

        public ACSubCategoryList(DailyHarvest DH)
        {
            objDailyHarvest = DH;
            intFormID = 1;
            InitializeComponent();
            try
            {
                gvSubCategoryList.DataSource = DHAccounts.GetAccountSubCategories();
            }
            catch
            {
            }
        }

        public ACSubCategoryList(DailyHarvestCW DHCW)
        {
            objDailyHarvestCW = DHCW;
            intFormID = 2;
            InitializeComponent();
            try
            {
                gvSubCategoryList.DataSource = DHAccounts.GetAccountSubCategories();
            }
            catch
            {
            }
        }

        public ACSubCategoryList(DailyHarvestCW1 DHCW1)
        {
            objDailyHarvestCW1 = DHCW1;
            intFormID = 3;
            InitializeComponent();
            try
            {
                gvSubCategoryList.DataSource = DHAccounts.GetAccountSubCategories();
            }
            catch
            {
            }
        }

        public ACSubCategoryList(OverTime OT)
        {
            objOT = OT;
            intFormID = 4;
            InitializeComponent();
            try
            {
                gvSubCategoryList.DataSource = DHAccounts.GetAccountSubCategories();
            }
            catch
            {
            }
        }

        public ACSubCategoryList(MusterChitEntry MC)
        {
            objMC = MC;
            intFormID = 5;
            InitializeComponent();
            try
            {
                gvSubCategoryList.DataSource = DHAccounts.GetAccountSubCategories();
            }
            catch
            {
            }
        }
        public ACSubCategoryList(MapEntryToAccounts MA)
        {
            objMapEntry = MA;
            intFormID = 6;
            InitializeComponent();
            try
            {
                gvSubCategoryList.DataSource = DHAccounts.GetAccountSubCategories();
            }
            catch
            {
            }
        }

        private void ACSubCategoryList_Load(object sender, EventArgs e)
        {
            try
            {
                gvSubCategoryList.DataSource = DHAccounts.GetAccountSubCategories();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddToForm_Click(object sender, EventArgs e)
        {
            if (intFormID == 1)
            {
                objDailyHarvest.txtACCode.Text = txtSubCategory.Text;
                objDailyHarvest.txtACCodeName.Text = txtName.Text;
                this.Close();
            }
            else if (intFormID == 2)
            {
                objDailyHarvestCW.txtACCode.Text = txtSubCategory.Text;
                objDailyHarvestCW.txtACCodeName.Text = txtName.Text;
                this.Close();
            }
            else if (intFormID == 3)
            {
                objDailyHarvestCW1.txtACCode.Text = txtSubCategory.Text;
                objDailyHarvestCW1.txtACCodeName.Text = txtName.Text;
                this.Close();
            }
           
            else if (intFormID == 6)
            {
                objMapEntry.txtMainACCode.Text = txtSubCategory.Text;
                objMapEntry.txtMainACCodeName.Text = txtName.Text;
                this.Close();
            }
            else
            {
                objMC.txtACCode.Text = txtSubCategory.Text;
                objMC.txtACCodeName.Text = txtName.Text;
                this.Close();
            }

        }

        private void gvSubCategoryList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSubCategory.Text = gvSubCategoryList.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtName.Text = gvSubCategoryList.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
    }
}