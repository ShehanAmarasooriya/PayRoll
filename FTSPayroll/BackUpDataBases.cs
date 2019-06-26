using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;

namespace FTSPayroll
{
    public partial class BackUpDataBases : Form
    {
        FTSPayRollBL.UpdateManager UpdateMgr = new FTSPayRollBL.UpdateManager();
        FTSPayRollBL.EstateDivisionBlock EstDiv=new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();
        public BackUpDataBases()
        {
            InitializeComponent();
        }

        private void BackUpDataBases_Load(object sender, EventArgs e)
        {
            ComboBoxserverName.DataSource = UpdateMgr.GetServerName("SQL Server");
            ComboBoxserverName.DisplayMember = "server";
            ComboBoxserverName.ValueMember = "server";

            ComboBoxDatabaseName.DataSource = UpdateMgr.GetDataBaseName();
            ComboBoxDatabaseName.DisplayMember = "Database";
            ComboBoxDatabaseName.ValueMember = "Database";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            lblPath.Text = folderBrowserDialog1.SelectedPath.ToString()+@"\"+ComboBoxDatabaseName.Text+EstDiv.getEstateId()+DateTime.Now.Date.Year+DateTime.Now.Date.Month+DateTime.Now.Date.Day+".bak";
        }

        private void cmdBackUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ComboBoxserverName.Text) || string.IsNullOrEmpty(ComboBoxDatabaseName.Text))
            {
                MessageBox.Show("Server Name & Database can not be Blank");
                return;
            }
            else
            {
                UpdateMgr.BackupSelectedDatabase(lblPath.Text, ComboBoxDatabaseName.Text);
                MessageBox.Show("Backup Created Successfully!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FTSPayRollBL.User.StrUserName.ToUpper().Equals("ADMIN"))
            {
                String strError = "";
                ////strError = UpdateMgr.BackupProcessedData();
                ////if (strError == "OK")
                ////{
                ////    MessageBox.Show("Daily Entries Backup Completed Successfully!");
                ////}
                ////else if (strError.Equals("PKE"))
                ////{
                ////    MessageBox.Show("Identity Insert Failed.");
                ////}
                ////else
                ////    MessageBox.Show("Unknown Error Occured.");
                //fixed deductions
                strError = UpdateMgr.BackupProcessedFixedDeductionsData();
                if (strError == "OK")
                {
                    MessageBox.Show("Fixed Deduction Backups Created Successfully!");
                }
                else if (strError.Equals("PKE"))
                {
                    MessageBox.Show("Identity Insert Failed.");
                }
                else if (strError.Equals("Err"))
                {
                    MessageBox.Show("1.Unknown Error Occured.");
                }
                else
                    MessageBox.Show("Unknown Error Occured.");

                //loan deductions
                strError = UpdateMgr.BackupProcessedLoanDeductionsData();
                if (strError == "OK")
                {
                    MessageBox.Show("Loan Deduction Backup Completed Successfully!");
                }
                else if (strError.Equals("PKE"))
                {
                    MessageBox.Show("Identity Insert Failed.");
                }
                else if (strError.Equals("Err"))
                {
                    MessageBox.Show("1.Unknown Error Occured.");
                }
                else
                    MessageBox.Show("Unknown Error Occured.");
            }
            else
            {
                MessageBox.Show("Restricted...!");
            }
        }
    }
}