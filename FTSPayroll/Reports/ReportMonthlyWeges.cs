using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;


namespace FTSPayroll.Reports
{
    public partial class ReportMonthlyWeges : Form
    {
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.CheckRollReports ChkRep = new FTSPayRollBL.CheckRollReports();
        ReportDocument rep = new ReportDocument();
        public ReportMonthlyWeges()
        {
            InitializeComponent();
        }

        private void ReportMonthlyWeges_Load(object sender, EventArgs e)
        {
            //cmbCategory.DataSource = EmpCat.ListCategories();
            //cmbCategory.DisplayMember = "CategoryName";
            //cmbCategory.ValueMember = "CategoryID";

            DataTable dtCat = EmpCat.ListCategories();
            if (dtCat.Rows.Count > 0)
            {
                for (int i = 0; i < dtCat.Rows.Count; i++)
                {
                    chkListCategory.Items.Add(dtCat.Rows[i][1].ToString());
                }
            }
            else
                chkListCategory.Items.Clear();
            DataTable dtDiv = EstDivBlock.ListEstateDivisions();
            if (dtDiv.Rows.Count > 0)
            {
                for (int i = 0; i < dtDiv.Rows.Count; i++)
                {
                    chkListDivision.Items.Add(dtDiv.Rows[i][0].ToString());
                }
            }
            else
                chkListDivision.Items.Clear();
            
        }

        private void chkListCategory_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            String strCategories = "";
            String strDivisions = "";
            if (rbtnCatAll.Checked)
            {
                strCategories = "ALL";
            }
            else if(rbtnCatSelected.Checked)
            {
                for (int i = 0; i < chkListCategory.Items.Count; i++)
                {
                    if (chkListCategory.GetItemChecked(i) == true)
                    {
                        if (strCategories != "")
                            strCategories += ",";
                        strCategories += chkListCategory.Items[i].ToString();
                    }
                }
            }
            if (rbtnDivAll.Checked)
            {
                strDivisions = "ALL";
            }
            else if (rbtnDivSelected.Checked)
            {
                for (int i = 0; i < chkListDivision.Items.Count; i++)
                {
                    if (chkListDivision.GetItemChecked(i) == true)
                    {
                        if (strDivisions != "")
                            strDivisions += ",";
                        strDivisions += chkListDivision.Items[i].ToString();
                    }
                }
            }
            ChkRep.StrCategory = strCategories;
            ChkRep.StrDivision = strDivisions;
            DataSet dataSetReport = new DataSet();
            dataSetReport = ChkRep.GetMonthlyWeges(strCategories, strDivisions, int.Parse(cmbYear.SelectedItem.ToString()), int.Parse(cmbMonth.SelectedItem.ToString()));
            dataSetReport.WriteXml("MonthlyWeges.xml");
            Reports.rptMonthlyWeges repMWeges = new Reports.rptMonthlyWeges();
            repMWeges.SetDataSource(dataSetReport);
            Reports.ReportViewer repView = new Reports.ReportViewer();
            repView.crystalReportViewer1.ReportSource = repMWeges;
            repView.Show();

        }


    }
}