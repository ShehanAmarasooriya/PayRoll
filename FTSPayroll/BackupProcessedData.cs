using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class BackupProcessedData : Form
    {
        FTSPayRollBL.UpdateManager UpdateMgr = new FTSPayRollBL.UpdateManager();
        public BackupProcessedData()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            UpdateMgr.BackupProcessedData();
        }
    }
}