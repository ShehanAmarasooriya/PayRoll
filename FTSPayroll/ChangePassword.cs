using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;

namespace FTSPayroll
{
    public partial class ChangePassword : Form
    {

        FTSPayRollBL.User myUser = new FTSPayRollBL.User();

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            txtUserName.Text = FTSPayRollBL.User.StrUserName;
            txtOldPassword.Text = FTSPayRollBL.User.StrUserPassword;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (MessageBox.Show("Confirm Password Change", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (this.txtNewPassword.Text == this.txtConfirmPassword.Text)
                    {
                        User.StrUserName = txtUserName.Text;
                        User.StrUserPassword = txtNewPassword.Text;
                        myUser.PasswordChange(myUser);
                        //myUser.CreateLog("Password Change", "Password changed of user : " + txtUserName.Text, DateTime.Now.Date);
                        MessageBox.Show("Password has changed successfully");
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Your Password is not matching...!");
                    }
                    //myAccount.StrAccountCode = txtCode.Text;
                    //myAccount.DeleteAccount(myAccount);
                    //ClearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            //txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}