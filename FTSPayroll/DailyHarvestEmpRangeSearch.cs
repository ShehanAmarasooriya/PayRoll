using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyHarvestEmpRangeSearch : Form
    {
        public DailyHarvestEmpRangeSearch()
        {
            InitializeComponent();
        }

        private void chkEmpRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEmpRange.Checked)
            {
                gbEmpRange.Enabled = true;               
            }
            else
            {
                gbEmpRange.Enabled = false;               
            }
        }

        private void txtEmpNoFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (String.IsNullOrEmpty(txtEmpNoFrom.Text))
                {
                    txtEmpNoFrom.Clear();
                    txtEmpNoFrom.Focus();
                }
                else
                {
                    txtEmpNoFrom.Text = txtEmpNoFrom.Text.PadLeft(5, '0');
                    txtEmpNoTo.Focus();
                }
            }
        }

        private void txtEmpNoTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (String.IsNullOrEmpty(txtEmpNoTo.Text))
                {
                    txtEmpNoTo.Clear();
                    txtEmpNoTo.Focus();
                }
                else
                {
                    txtEmpNoTo.Text = txtEmpNoTo.Text.PadLeft(5, '0');
                    btnDisplay.Focus();
                }
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {

        }

       
    }
}