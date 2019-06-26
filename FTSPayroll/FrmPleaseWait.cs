using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FrmPleaseWait : Form
    {
        MusterChitEntry objMuster;
        Boolean boolCompleted = false;
        DateTime dtTime;

        public FrmPleaseWait()
        {
            InitializeComponent();
        }

        public FrmPleaseWait(MusterChitEntry objMust)
        {
            objMuster = objMust;
            InitializeComponent();
            Application.DoEvents();
        }

        private void FrmPleaseWait_Load(object sender, EventArgs e)
        {
            checkStatus();
        }
        public void checkStatus()
        {
            while (objMuster.boolDownloadOk == true)
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}