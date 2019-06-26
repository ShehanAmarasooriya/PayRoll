using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace FTSPayroll
{
    public partial class NewSystemUpdate : Form
    {
        FTSPayRollBL.UpdateManager myUpdates = new FTSPayRollBL.UpdateManager();
        public NewSystemUpdate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo f = new FileInfo("SqlUpdate.sql");
                //myUpdates.ExecuteUpdate(f);
                myUpdates.test(f);
                MessageBox.Show("Executed Successfully!");
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!, " + ex.Message);
            }
          

        }
    }
}