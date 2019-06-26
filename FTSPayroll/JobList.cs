using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class JobList : Form
    {
        FTSPayRollBL.Job myjob = new FTSPayRollBL.Job();
        DailyHarvest myDHarvest = new DailyHarvest();
        DailyHarvestCW myDHarvestCW = new DailyHarvestCW();
        DailyHarvestRubber myDharvestR = new DailyHarvestRubber();
        MusterChitEntry myMusterChit = new MusterChitEntry();
        OverTime myOT = new OverTime();
        FTSPayRollBL.Job myJobM = new FTSPayRollBL.Job();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        
        FTSPayRollBL.JobGroup myJobGroup = new FTSPayRollBL.JobGroup();

        public Int32 intFrmType;
        public String strCrop = "";
        public String strExpenditureType = "";

        public JobList()
        {
            InitializeComponent();
        }

        public JobList(DailyHarvest dHarvest)
        {
            myDHarvest = dHarvest;
            intFrmType = 1;
            InitializeComponent();
        }

        public JobList(DailyHarvestCW dHarvestCW)
        {
            myDHarvestCW = dHarvestCW;
            intFrmType = 2;
            InitializeComponent();
        }

        public JobList(DailyHarvestRubber dHarvestR)
        {
            myDharvestR = dHarvestR;
            intFrmType = 3;
            InitializeComponent();
        }

        public JobList(MusterChitEntry MusterC,String Crop,String exType)
        {
            myMusterChit = MusterC;
            intFrmType = 4;
            strCrop = Crop;
            strExpenditureType = exType;
            InitializeComponent();
        }

        public JobList(MusterChitEntry MusterC, String exType)
        {
            myMusterChit = MusterC;
            intFrmType = 5;
            strCrop = "None";
            strExpenditureType = exType;
            InitializeComponent();
        }
        
        public JobList(MusterChitEntry MusterC)
        {
            myMusterChit = MusterC;
            intFrmType = 6;
            strCrop = "None";
            strExpenditureType = "None";
            InitializeComponent();
        }

        public JobList(OverTime OT, String Crop, String exType)
        {
            myOT = OT;
            intFrmType = 7;
            strCrop = Crop;
            strExpenditureType = exType;
            InitializeComponent();
        }

        public JobList(OverTime OT)
        {
            myOT = OT;
            intFrmType = 8;
            InitializeComponent();
        }



        private void JobList_Load(object sender, EventArgs e)
        {
            
            //cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
            //cmbCropType.DisplayMember = "Name";
            //cmbCropType.ValueMember = "Code";

            if (intFrmType == 4)
            {
                //cmbCropType.Text = strCrop;
                gvList.DataSource = myjob.ListJobMasterByCropAndExType(strCrop, strExpenditureType);
                if (gvList.Rows.Count < 1)
                {
                    MessageBox.Show("Jobs Not Available For " + strCrop + " Crop AND " + strExpenditureType);
                    this.Close();
                }
                txtSearchKeyWord.Focus();
            }
            else if (intFrmType == 5)
            {
                gvList.DataSource = myjob.ListJobMasterByCropAndExType("NONE", strExpenditureType);
                if (gvList.Rows.Count < 1)
                {
                    MessageBox.Show("Jobs Not Available For " + strCrop + " Crop AND " + strExpenditureType);
                    this.Close();
                }
            }
            else if (intFrmType == 6)
            {
                gvList.DataSource = myjob.ListJobMasterByCropAndExType("NONE", "NONE");
                if (gvList.Rows.Count < 1)
                {
                    MessageBox.Show("Jobs Not Available For " + strCrop + " Crop AND " + strExpenditureType);
                    this.Close();
                }
            }
            else if (intFrmType == 7)
            {
                gvList.DataSource = myjob.ListJobMasterByCropAndExType(strCrop, strExpenditureType);
                if (gvList.Rows.Count < 1)
                {
                    MessageBox.Show("Jobs Not Available For " + strCrop + " Crop AND " + strExpenditureType);
                    this.Close();
                }
            }
            else if (intFrmType == 8)
            {
                gvList.DataSource = myjob.ListJobMasterByCropAndExType("NONE", "NONE");
                if (gvList.Rows.Count < 1)
                {
                    MessageBox.Show("Jobs Not Available For " + strCrop + " Crop AND " + strExpenditureType);
                    this.Close();
                }
            }
            else
                gvList.DataSource = myjob.ListJobMaster();
            this.txtSearchKeyWord.Focus();
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (intFrmType)
            {
                case 1:
                    {
                        myDHarvest.cmbJobCode.SelectedValue = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                        break;
                    }
                case 2:
                    {
                        myDHarvestCW.cmbJobCode.SelectedValue = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                        break;
                    }
                case 3:
                    {
                        myDharvestR.cmbJobCode.SelectedValue = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                        break;
                    }
                case 4:
                    {
                        myMusterChit.txtJobShortName.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                        break;
                    }
                case 5:
                    {
                        myMusterChit.txtJobShortName.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                        break;
                    }
                case 6:
                    {
                        myMusterChit.txtJobShortName.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                        break;
                    }
                case 7:
                    {
                        myOT.txtJobShortName.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                        break;
                    }
                case 8:
                    {
                        myOT.txtJobShortName.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
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

        private void cmbJobGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    gvList.DataSource = myJobM.ListJobs(Convert.ToInt32(cmbJobGroup.SelectedValue.ToString()));
            //}
            //catch
            //{
            //}
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchJobs();
        }

        private void btnSearch_Click()
        {
            SearchJobs();
        }

        private void SearchJobs()
        {
            try
            {
                if (intFrmType == 4)
                {
                    gvList.DataSource = myjob.ListJobMasterByCropAndExType(strCrop, strExpenditureType,txtSearchKeyWord.Text);
                    if (gvList.Rows.Count < 1)
                    {
                        MessageBox.Show("Jobs Not Available For " + strCrop + " Crop AND " + strExpenditureType);
                        this.Close();
                    }
                    txtSearchKeyWord.Focus();
                }
                else if (intFrmType == 5)
                {
                    gvList.DataSource = myjob.ListJobMasterByCropAndExType("NONE", strExpenditureType, txtSearchKeyWord.Text);
                    if (gvList.Rows.Count < 1)
                    {
                        MessageBox.Show("Jobs Not Available For " + strCrop + " Crop AND " + strExpenditureType);
                        this.Close();
                    }
                }
                else if (intFrmType == 6)
                {
                    gvList.DataSource = myjob.ListJobMasterByCropAndExType("NONE", "NONE", txtSearchKeyWord.Text);
                    if (gvList.Rows.Count < 1)
                    {
                        MessageBox.Show("Jobs Not Available For " + strCrop + " Crop AND " + strExpenditureType);
                        this.Close();
                    }
                }
                else
                    gvList.DataSource = myjob.ListJobMaster();  

                //if (!String.IsNullOrEmpty(txtSearchKeyWord.Text))
                //{
                //    //gvList.DataSource = myJobM.ListJobs(txtSearchKeyWord.Text);
                //    myjob.ListJobMasterByCropAndExType(strCrop, strExpenditureType);
                //}
            }
            catch
            {
            }
        }

        private void txtSearchKeyWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSearch_Click();
        }

        private void txtSearchKeyWord_TextChanged(object sender, EventArgs e)
        {
            SearchJobs();
        }
    }
}