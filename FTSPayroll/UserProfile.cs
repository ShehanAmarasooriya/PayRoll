using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class UserProfile : Form
    {
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();
        FTSPayRollBL.MenuLoader objMenuItem = new FTSPayRollBL.MenuLoader();

        public UserProfile()
        {
            InitializeComponent();
        }

        public void UserProfile_Load(object sender, EventArgs e)
        {
            cmbEstates.DataSource = myUser.ListEstates();
            cmbEstates.DisplayMember = "EstateID";
            cmbEstates.ValueMember = "EstateID";

            cmbDivisions.DataSource = myUser.ListEstateDivisions();
            cmbDivisions.DisplayMember = "DivisionID";
            cmbDivisions.ValueMember = "DivisionID";

            if (FTSPayRollBL.User.StrUserRole.ToUpper().Equals("ADMIN"))
            {
                cmbRole.DataSource = myUser.ListAllRolesAdmin();
                cmbRole.DisplayMember = "RoleName";
                cmbRole.ValueMember = "RoleName";
            }
            else
            {
                cmbRole.DataSource = myUser.ListAllRoles();
                cmbRole.DisplayMember = "RoleName";
                cmbRole.ValueMember = "RoleName";
            }

            //lstPermissions.Items.Clear();
            //DataTable myTable = myUser.ListAllMenuItems();
            //if (myTable.Rows.Count > 0)
            //{
            //    for (int i = 0; i < myTable.Rows.Count; i++)
            //    {
            //        lstPermissions.Items.Add(myTable.Rows[i][1].ToString());
            //    }
            //}
            //else
            //    lstPermissions.Items.Clear();
            if (FTSPayRollBL.User.StrUserRole.ToUpper().Equals("ADMIN"))
            {
                gvUsers.DataSource = myUser.ListAllUsers();
            }
            else
            {
                gvUsers.DataSource = myUser.ListAllUsersForNotAdmin();
            }

            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtUserID.Clear();
            //lstPermissions.Items.Clear();
            //DataTable myTable = myUser.ListAllMenuItems();
            //if (myTable.Rows.Count > 0)
            //{
            //    for (int i = 0; i < myTable.Rows.Count; i++)
            //    {
            //        lstPermissions.Items.Add(myTable.Rows[i][1].ToString());
            //    }
            //}
            //else
            //    lstPermissions.Items.Clear();
            if (FTSPayRollBL.User.StrUserRole.ToUpper().Equals("ADMIN"))
            {
                gvUsers.DataSource = myUser.ListAllUsers();
            }
            else
            {
                gvUsers.DataSource = myUser.ListAllUsersForNotAdmin();
            }
            //gvUsers.DataSource = myUser.ListAllUsers();

            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm Deletion?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    myUser.SUserName = txtUserID.Text;
                    myUser.DeleteUser(myUser);
                    //myUser.DeleteUserPermission(myUser);
                    cmdClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void gvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtUserID.Text = gvUsers.Rows[e.RowIndex].Cells[0].Value.ToString();
                cmbEstates.SelectedValue = gvUsers.Rows[e.RowIndex].Cells[2].Value.ToString();
                cmbDivisions.SelectedValue = gvUsers.Rows[e.RowIndex].Cells[3].Value.ToString();
                cmbRole.SelectedValue = gvUsers.Rows[e.RowIndex].Cells[5].Value.ToString();
                //lstPermissions.Items.Clear();
                //DataTable myTable = myUser.ListAllMenuItems();
                //if (myTable.Rows.Count > 0)
                //{
                //    for (int i = 0; i < myTable.Rows.Count; i++)
                //    {
                //        lstPermissions.Items.Add(myTable.Rows[i][1].ToString());
                //    }
                //}
                //else
                //    lstPermissions.Items.Clear();

                myUser.SUserName = txtUserID.Text;
                //DataTable myPermission = myUser.ListUserPermissionbyUserID(myUser);
                //if (myPermission.Rows.Count > 0)
                //{
                //    txtPassword.Text = myPermission.Rows[0][0].ToString();
                //    for (int x = 0; x < myPermission.Rows.Count; x++)
                //    {
                //        for (int y = 0; y < lstPermissions.Items.Count; y++)
                //        {
                //            if (myPermission.Rows[x][1].ToString() == lstPermissions.Items[y].ToString())
                //            {
                //                lstPermissions.SetItemChecked(y, true);
                //            }
                //        }
                //    }
                //}

                cmdAdd.Enabled = false;

                cmdUpdate.Enabled = true;
                cmdDelete.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                myUser.SUserName = txtUserID.Text;
                myUser.SEstate = cmbEstates.Text;
                myUser.SDivision = cmbDivisions.Text;
                myUser.StrRole = cmbRole.SelectedValue.ToString();
                myUser.UpdateUser(myUser.StrRole, myUser.SUserName);
                //myUser.DeleteUserPermission(myUser);

                //for (int i = 0; i < lstPermissions.Items.Count; i++)
                //{
                //    if (lstPermissions.GetItemChecked(i) == true)
                //    {
                //        myUser.StrMenuName = lstPermissions.Items[i].ToString();
                //        myUser.IntMenuId = myUser.FindMenuIdbyMenuName(myUser);
                //        myUser.InsertUserPermission(myUser);
                //    }
                //}
                MessageBox.Show("User Updated Successfully");
                cmdClear.PerformClick();
                //Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                myUser.SUserName = txtUserID.Text;
                myUser.SUserPassword = txtPassword.Text;
                myUser.SEstate = cmbEstates.SelectedValue.ToString();
                myUser.SDivision = cmbDivisions.SelectedValue.ToString();
                myUser.StrRole = cmbRole.SelectedValue.ToString();
                //myUser.Str = cmbEstates.Text;
                //myUser.StrDivision = cmbDivisions.Text;
                myUser.InsertUser(myUser);

                //for (int i = 0; i < lstPermissions.Items.Count; i++)
                //{
                //    if (lstPermissions.GetItemChecked(i) == true)
                //    {
                //        myUser.StrMenuName = lstPermissions.Items[i].ToString();
                //       myUser.IntMenuId = myUser.FindMenuIdbyMenuName(myUser);
                //        myUser.InsertUserPermission(myUser);
                //    }
                //}
                MessageBox.Show("User Added Successfully");
                cmdClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkSelectAll.Checked)
            //{
            //     for (int i = 0; i < lstPermissions.Items.Count; i++)
            //    {
            //        lstPermissions.SetItemChecked(i, true);                  
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < lstPermissions.Items.Count; i++)
            //    {
            //        lstPermissions.SetItemChecked(i, false);
            //    }
            //}
        }

        private void lstPermissions_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserRole objUserRole = new UserRole(this);
            objUserRole.Show();
            
            
        }
    }
}