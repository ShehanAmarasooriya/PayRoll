using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class CancelMonthProcess : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.PreviewMonthlyWages PreMWages = new FTSPayRollBL.PreviewMonthlyWages();
        FTSPayRollBL.ProcessMonthlyWages proMWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.User myUserOb = new FTSPayRollBL.User();
        FTSPayRollBL.FTSCheckRollSettings mySett = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.UpdateManager myUpdates = new FTSPayRollBL.UpdateManager();

        public CancelMonthProcess()
        {
            InitializeComponent();
        }

        private void CancelMonthProcess_Load(object sender, EventArgs e)
        {
            //cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            //cmbDivision.DisplayMember = "DivisionID";
            //cmbDivision.ValueMember = "DivisionID";

            //cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            //cmbYear.DisplayMember = "Year";
            //cmbYear.ValueMember = "Year";
            //cmbYear.SelectedValue = YMonth.getLastYearID();

            //pboxLoad.Visible = false;

            //cmbYear_SelectedIndexChanged(null, null);

            //btnBackUp.Enabled = true;
            //btnCancelProcess.Enabled = false;
            
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = YMonth.ListMonths(Convert.ToInt32(this.cmbYear.SelectedValue.ToString()));
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MonthKey";
                cmbMonth.SelectedValue = YMonth.getLastMonthID();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnCancelProcess_Click(object sender, EventArgs e)
        {
            if (myUserOb.IsAdminUser(FTSPayRollBL.User.StrUserName))
            {
                if (MessageBox.Show("Cancel Checkroll Process?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                }
            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            //lblEarningsStatus.Text = "Creating BackUp\r\nThis May Take Several Minutes...";
            //Application.DoEvents();
            //string n = string.Format("CHK" + myDivision.ListEstate().Rows[0][0].ToString() + "-{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            //File.WriteAllText(n, "aaa");
            //String dirPath = "C:\\ChkDbBackUps";
            //String SDirectory = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + Convert.ToInt32(cmbYear.SelectedValue.ToString()) + "_" + cmbMonth.Text + "BFProcess";
            //String filePath = "";
            //String fileName = "";
            //// Create a reference to a directory.
            //DirectoryInfo di = new DirectoryInfo(dirPath);
            //DirectoryInfo SubDirectory = new DirectoryInfo(SDirectory);

            //// Create the directory only if it does not already exist.
            //if (di.Exists == false)
            //{
            //    di.Create();
            //    DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
            //}
            //else
            //{
            //    if (SubDirectory.Exists == false)
            //    {
            //        DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
            //    }
            //}



            ////fileName = SDirectory + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            ////fileName = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0')+n;
            //fileName = "BF_CancelProcess_" + n;
            //filePath = dirPath + "\\" + SubDirectory.ToString() + "\\" + fileName + ".bak";
            //if (File.Exists(filePath))
            //{
            //    MessageBox.Show(" Already Backed Up The Checkroll Before Cancelation,\r\n Press 'OK' To Proceed. ");
            //    btnBackUp.Enabled = false;
            //    btnProcess.Enabled = true;
            //}
            //else
            //{
            //    try
            //    {
            //        myUpdates.BackUpDataBase(filePath);
            //        myUpdates.Compress(filePath, fileName + ".ZIP");
            //        //FileInfo existingFile = new FileInfo(filePath);
            //        //existingFile.Delete();
            //        MessageBox.Show("Backup of Checkroll Data completed successfully. ");
            //        lblEarningsStatus.Text = "Ready to Cancel";
            //        btnBackUp.Enabled = false;
            //        btnCancelProcess.Enabled = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        //di.Delete(true);
            //        MessageBox.Show("Back Up Error, " + ex.Message);
            //        btnBackUp.Enabled = false;
            //        btnProcess.Enabled = true;
            //    }
            //}
        }
    }
}