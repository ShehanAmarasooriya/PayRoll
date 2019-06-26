using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyHarvestRegisters : Form
    {
        public DailyHarvestRegisters()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpNoTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkEmpRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEmpRange.Checked)
            {
                gbEmpRange.Enabled = true;
            }
            else
                gbEmpRange.Enabled = false;
        }
    }
}