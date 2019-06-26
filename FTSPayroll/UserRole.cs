using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class UserRole : Form
    {
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();
        FTSPayRollBL.MenuLoader objMenuItem = new FTSPayRollBL.MenuLoader();
        UserProfile ObjUserProfile = new UserProfile();

        public UserRole()
        {
            InitializeComponent();
        }

        public UserRole(UserProfile objusrPro)
        {
            ObjUserProfile = objusrPro;
            InitializeComponent();
        }

        private void UserRole_Load(object sender, EventArgs e)
        {
            lstPermissions.Items.Clear();
            DataTable myTable = objMenuItem.ListAllMenuItems();
            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    lstPermissions.Items.Add(myTable.Rows[i][1].ToString());
                }
            }
            else
                lstPermissions.Items.Clear();
            gvRoles.DataSource = myUser.ListAllRoles();

            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void gvRoles_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtRole.Text = gvRoles.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Convert.ToBoolean(gvRoles.Rows[e.RowIndex].Cells[2].Value.ToString()) == true)
                chkActiveRole.Checked = true;
            else
                chkActiveRole.Checked = false;
            if (Convert.ToBoolean(gvRoles.Rows[e.RowIndex].Cells[2].Value.ToString()) == true)
                chkIsAdmin.Checked = true;
            else
                chkIsAdmin.Checked = false;

            
            lstPermissions.Items.Clear();
            DataTable myTable = myUser.ListAllMenuItems();
            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    lstPermissions.Items.Add(myTable.Rows[i][1].ToString());
                }
            }
            else
                lstPermissions.Items.Clear();

            myUser.SUserName = txtRole.Text;
            DataTable myPermission = myUser.ListUserPermissionbyRole(gvRoles.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (myPermission.Rows.Count > 0)
            {
                //txtPassword.Text = myPermission.Rows[0][0].ToString();
                for (int x = 0; x < myPermission.Rows.Count; x++)
                {
                    for (int y = 0; y < lstPermissions.Items.Count; y++)
                    {
                        if (myPermission.Rows[x][0].ToString() == lstPermissions.Items[y].ToString())
                        {
                            lstPermissions.SetItemChecked(y, true);
                        }
                    }
                }
            }

            cmdAdd.Enabled = false;

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtRole.Clear();
            chkActiveRole.Checked = true;
            chkIsAdmin.Checked = false;
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            ObjUserProfile.UserProfile_Load(null,null);
            try
            {
                gvRoles.DataSource = myUser.ListAllRoles();
            }
            catch
            {
            }


        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtRole.Text))
            {
                MessageBox.Show("Role Name Cannot Be Empty");
            }
            else
            {
                try
                {
                    Boolean ActiveRole = false;
                    Boolean IsAdmin = false;
                    if (chkIsAdmin.Checked)
                        IsAdmin = true;
                    else
                        IsAdmin = false;
                    if (chkActiveRole.Checked)
                        ActiveRole = true;
                    else
                        ActiveRole = false;
                    myUser.InsertRole(txtRole.Text, ActiveRole, IsAdmin);

                    for (int i = 0; i < lstPermissions.Items.Count; i++)
                    {
                        if (lstPermissions.GetItemChecked(i) == true)
                        {
                            myUser.InsertRolePermission(txtRole.Text, lstPermissions.Items[i].ToString());
                        }
                    }
                    MessageBox.Show("Role Added Successfully");
                    cmdClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                myUser.DeleteRolePermission(txtRole.Text);

                for (int i = 0; i < lstPermissions.Items.Count; i++)
                {
                    if (lstPermissions.GetItemChecked(i) == true)
                    {
                        myUser.InsertRolePermission(txtRole.Text, lstPermissions.Items[i].ToString());
                    }
                }
                MessageBox.Show("User Updated Successfully");
                cmdClear.PerformClick();
                //Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm Deletion?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    myUser.DeleteRole(txtRole.Text);
                    myUser.DeleteRolePermission(txtRole.Text);
                    cmdClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < lstPermissions.Items.Count; i++)
                {
                    lstPermissions.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < lstPermissions.Items.Count; i++)
                {
                    lstPermissions.SetItemChecked(i, false);
                }
            }
        }
    }
}