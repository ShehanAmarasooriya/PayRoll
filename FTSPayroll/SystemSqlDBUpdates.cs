using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

namespace FTSPayroll
{
    public partial class SystemSqlDBUpdates : Form
    {
        FTSPayRollBL.UpdateManager myUpdates = new FTSPayRollBL.UpdateManager();
        public SystemSqlDBUpdates()
        {
            InitializeComponent();
        }

        private void SystemSqlDBUpdates_Load(object sender, EventArgs e)
        {
            String DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\updates\"; 
            chkListUpdates.Items.Clear();
            DataTable myTable = myUpdates.GetUpdateFiles(DesktopPath);
            if (myTable.Rows.Count > 0)
            {
                if (myTable.Rows.Count > 0)
                {
                    for (int i = 0; i < myTable.Rows.Count; i++)
                    {
                        chkListUpdates.Items.Add(myTable.Rows[i][0].ToString());
                    }
                    chkSelectAllUpdates();

                }
                else
                    chkListUpdates.Items.Clear();
            }
            else
            {
                MessageBox.Show("No Update Found.");
            }

        }
        public static int GetWeekNumber(DateTime dtPassed)
{
        CultureInfo ciCurr = CultureInfo.CurrentCulture;
        int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        return weekNum;
}

        private void chkSelectAllUpdates()
        {
            chkSelectAll.Checked = true;
            for (int i = 0; i < chkListUpdates.Items.Count; i++)
            {
                chkListUpdates.SetItemChecked(i, true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            try
            {
                myUpdates.ExecuteScript1();
                MessageBox.Show("Script1 Executed Successfully!");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Script1 Error!, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript2();
                MessageBox.Show("Script2 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script2 Error!, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript3();
                MessageBox.Show("Script3 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script3 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript4();
                MessageBox.Show("Script4 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script4 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript5();
                MessageBox.Show("Script5 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script5 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript6();
                MessageBox.Show("Script6 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script6 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript7();
                MessageBox.Show("Script7 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script7 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript8();
                MessageBox.Show("Script8 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script8 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript9();
                MessageBox.Show("Script9 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script9 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript10();
                MessageBox.Show("Script10 Executed Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Script10 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript11();
                MessageBox.Show("Script11 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script11 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript12();
                MessageBox.Show("Script12 Executed Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Script12 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript13();
                MessageBox.Show("Script13 Executed Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Script13 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript14();
                MessageBox.Show("Script14 Executed Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Script14 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript15();
                MessageBox.Show("Script15 Executed Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Script15 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript16();
                MessageBox.Show("Script16 Executed Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Script16 Error, " + ex.Message);
            }
            try
            {
                myUpdates.ExecuteScript17();
                MessageBox.Show("Script17 Executed Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Script17 Error, " + ex.Message);
            }
            //FileInfo sqlfile = new FileInfo("FTSPayRollBL.dll");
            //Boolean booldeleted = myUpdates.DeleteSqlFileExist(sqlfile);
            //if (booldeleted == true)
            //{
            //    MessageBox.Show("Update files removed successfully");
            //}
                  

            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FileInfo myfile = new FileInfo("FTSPayRollBL.dll");
            Boolean booldeleted = myUpdates.DeleteSqlFileExist(myfile);
            if (booldeleted == true)
            {
                MessageBox.Show("Update files removed successfully");
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < chkListUpdates.Items.Count; i++)
                {
                    chkListUpdates.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < chkListUpdates.Items.Count; i++)
                {
                    chkListUpdates.SetItemChecked(i, false);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
                String strUpdates = "";
                for (int i = 0; i < chkListUpdates.Items.Count; i++)
                {
                    if (chkListUpdates.GetItemChecked(i) == true)
                    {
                        try
                        {
                            myUpdates.ExecuteScript(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\updates\"+chkListUpdates.Items[i].ToString());
                            //MessageBox.Show(chkListUpdates.Items[i].ToString()+" Script Executed Successfully!");
                            strUpdates = strUpdates + chkListUpdates.Items[i].ToString() + "\r\n ";

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Update Failed," + chkListUpdates.Items[i].ToString() + " - " + ex.Message);
                            //break;
                        }
                    }
                }
                if (String.IsNullOrEmpty(strUpdates))
                {
                    MessageBox.Show("Nothing Executed.");                    
                }
                else
                {
                    MessageBox.Show(strUpdates + " Update(s) Successfully Completed.");
                    this.Close();
                }
            
     
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\updates\");
            try
            {
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                   file.Delete();
                }
                MessageBox.Show("Update files removed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}