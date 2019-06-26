using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using OlaxToolSet;

namespace FTSPayroll
{
    public partial class Login : Form
    {
        FTSPayRollBL.User objUser = new FTSPayRollBL.User();
        FTSPayRollBL.UpdateManager myUpdates = new FTSPayRollBL.UpdateManager();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //click Once Configurations
            //OlaxToolsSet.SystemInformation SI = new OlaxToolsSet.SystemInformation();

            //SI.dbServer = @"10.10.6.100\SQLEXPRESS";
            ////SI.dbServer = @"10.10.7.100\SQLEXPRESS";
            ////SI.dbUserId = "sa";
            ////SI.dbPassWord = "pass1234";
            ////SI.module = "Agalawatte Checkroll";
            ////SI.latestVersion = "1.0.0.30";



            //SI.dbServer = @"10.10.18.100";
            //SI.dbUserId = "sa";
            //SI.dbPassWord = "pass1234";
            //SI.module = "Agalawatte Checkroll";
            //SI.latestVersion = "1.0.0.31";


            //OlaxToolsSet.VersionUpdateManage.updateSystem(SI);
            //OlaxToolsSet.VersionUpdateManage.validateVersion(SI);
            //this.Text = OlaxToolsSet.VersionUpdateManage.getCurrentVersion(SI);
            ////click Once Configurations end
            //lblVersion.Text = "V1.0.0.12";


            //FileInfo myFile = new FileInfo("FTSPayRollBL.dll");
            //myUpdates.DeleteSqlFileExist(myFile);
            cmbEstate.DataSource = objUser.ListEstates();
            cmbEstate.ValueMember = "EstateID";
            cmbEstate.DisplayMember = "EstateID";

            cmbDivision.DataSource = objUser.ListEstateDivisions();
            cmbDivision.ValueMember = "DivisionID";
            cmbDivision.DisplayMember = "DivisionID";

            cmbYear.DataSource = objUser.GetYear().Tables[0];
            cmbYear.ValueMember = "Year";
            cmbYear.DisplayMember = "Year";

            cmbYear_SelectedIndexChanged(null, null);



            //cmbMonth.DataSource = objUser.GetMonth().Tables[0];
            //cmbMonth.ValueMember = "MId";
            //cmbMonth.DisplayMember = "Month";
            cmbYear.Text = DateTime.Now.Year.ToString();
            cmbMonth.SelectedValue = Convert.ToInt32(DateTime.Now.Month.ToString());
            //txtUserName.Text="admin";
            //txtPassword.Text="1234";
        }

        private void btnLogin_Click(object sender, EventArgs e)        
        {
            if (cmbMonth.DataSource != null)
            {
                FTSPayRollBL.User.StrUserName = txtUserName.Text;
                FTSPayRollBL.User.StrUserPassword = txtPassword.Text;
                FTSPayRollBL.User.StrDivision = cmbDivision.Text;
                FTSPayRollBL.User.StrEstate = cmbEstate.Text;
                FTSPayRollBL.User.StrMonth = cmbMonth.Text;
                FTSPayRollBL.User.StrYear = cmbYear.Text;
                FTSPayRollBL.User.StrUserRole = objUser.getUserRole().Tables[0].Rows[0][0].ToString();

                if (objUser.checkValidUser())
                {
                    if (objUser.checkValidMonth())
                    {
                        this.Hide();
                        
                        FTSPayrollMain Main = new FTSPayrollMain();
                        //Main.MdiParent = this;
                        Main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Month.", "Login Fail", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("User Login Fail.", "Login Fail", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Select a Valid Year And Month","Login Fail",MessageBoxButtons.OK);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbMonth.Focus();
            }
        }

        private void cmbEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbYear.Focus();
            }
        }

        private void cmbMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtUserName.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPassword.Focus();
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 intYear = Convert.ToInt32(cmbYear.Text);
                cmbMonth.DataSource = objUser.GetMonth(intYear).Tables[0];
                cmbMonth.ValueMember = "MId";
                cmbMonth.DisplayMember = "Month";
            }
            catch (Exception ex)
            { }
        }


    }
}