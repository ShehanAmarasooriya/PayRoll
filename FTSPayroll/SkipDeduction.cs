using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class SkipDeduction : Form
    {
        public SkipDeduction()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.SkipDeductionsCls mySkipDeduction = new FTSPayRollBL.SkipDeductionsCls();

        private void SkipDeduction_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            try
            {
                cmbYear.SelectedValue = YMonth.getLastYearID();
                cmbMonth.SelectedValue = YMonth.getLastMonthID();
            }
            catch { }

            cmbDeductionGroup.DataSource = DeductMaster.getDeductionGroupWithoutFAMAFSTCO();
            cmbDeductionGroup.DisplayMember = "DeductGroupName";
            cmbDeductionGroup.ValueMember = "DeductGroupCode";

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDeductionGroup_SelectedIndexChanged(null, null);
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.refreshGrid();
            }
            catch (Exception ex) { }
        }



        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DeductMaster.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                cmbDeductions.DataSource = null;
                cmbDeductions.DataSource = DeductMaster.ListDeduction(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                cmbDeductions.DisplayMember = "DeductShortName";
                cmbDeductions.ValueMember = "DeductCode";
                cmbDeductions_SelectedIndexChanged(null, null);
            }
            catch { }
        }

        private void xmdSkip_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean boolInsertStatus = true;
                String strStatus = "";
                for (int i = 0; i < gvlist.Rows.Count; i++)
                {

                    if (true == ((bool)gvlist.Rows[i].Cells["Skip"].Value))
                    {
                        this.variableAssigning(i);
                        try
                        {
                            strStatus= mySkipDeduction.InsertSkipDeduction();
                            if (!strStatus.ToUpper().Equals("ADDED"))
                            {
                                boolInsertStatus = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error on Insert Skip Deduction");
                            boolInsertStatus = false;
                        }
                    }

                }
                if (boolInsertStatus == true)
                    MessageBox.Show("Selected Deduction  successfully Skipped");
                else
                    MessageBox.Show("Selected Deduction Skip Completed With Errors....");

                this.refreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.."+ ex.Message.ToString());
            }

            chkSelectAllDed.Checked = false;
        }


        private void variableAssigning(int index)
        {
            try
            {
                mySkipDeduction.StrDivision = cmbDivision.SelectedValue.ToString();
                mySkipDeduction.IntYear = Convert.ToInt32(cmbYear.SelectedValue);
                mySkipDeduction.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue);
                mySkipDeduction.StrEmpNo = gvlist.Rows[index].Cells["EmpNo"].Value.ToString();
                mySkipDeduction.IntDeductionCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                mySkipDeduction.DecDeductAmount = Convert.ToDecimal((gvlist.Rows[index].Cells["DeductionAmount"].Value));
                mySkipDeduction.IntDeductGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                mySkipDeduction.BoolSkipped = (bool)gvlist.Rows[index].Cells["Skip"].Value;
                mySkipDeduction.StrUserID = FTSPayRollBL.User.StrUserName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void variableAssigningToRefresh()
        {
            mySkipDeduction.StrDivision = cmbDivision.SelectedValue.ToString();
            mySkipDeduction.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            mySkipDeduction.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
            mySkipDeduction.IntDeductGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
            mySkipDeduction.IntDeductionCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
        }


        private void variableAssigningForSelection(int index)
        {

            //mySkipDeduction.StrDivision = cmbDivision.SelectedValue.ToString();
            //mySkipDeduction.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            //mySkipDeduction.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
            //mySkipDeduction.IntDeductGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
            //mySkipDeduction.IntDeductionCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());

            mySkipDeduction.StrDivision = cmbDivision.SelectedValue.ToString();
            mySkipDeduction.IntYear = Convert.ToInt32(cmbYear.SelectedValue);
            mySkipDeduction.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue);
            mySkipDeduction.StrEmpNo = gvSkippedDeduction.Rows[index].Cells["EmpNo"].Value.ToString();
            mySkipDeduction.IntDeductionCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
            mySkipDeduction.DecDeductAmount = Convert.ToDecimal((gvSkippedDeduction.Rows[index].Cells["DeductAmount"].Value));
            mySkipDeduction.IntDeductGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
            mySkipDeduction.BoolSkipped = (bool)gvSkippedDeduction.Rows[index].Cells["Skip"].Value;
            mySkipDeduction.StrUserID = FTSPayRollBL.User.StrUserName;


        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }






        private void refreshGrid()
        {

            this.variableAssigningToRefresh();

            gvlist.DataSource = mySkipDeduction.ListDeduction();
            gvSkippedDeduction.DataSource = mySkipDeduction.ListSkippedDeduction();

            chkSelectAllSkipped.Checked = false;



        }





        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.refreshGrid();
            }
            catch { }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.refreshGrid();
            }
            catch { }
        }

        private void cmbDeductions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.refreshGrid();
            }
            catch (Exception ex)
            {

            }
        }





        private void gvlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvSkippedDeduction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkSelectAllDed_CheckedChanged(object sender, EventArgs e)
        {

            if (chkSelectAllDed.Checked == true)
            {
                for (int i = 0; i < gvlist.Rows.Count; i++)
                {

                    gvlist.Rows[i].Cells[0].Value = true;

                }
            }
            if (chkSelectAllDed.Checked == false)
            {
                for (int i = 0; i < gvlist.Rows.Count; i++)
                {

                    gvlist.Rows[i].Cells[0].Value = false;

                }
            }


        }

        private void chkSelectAllSkipped_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllSkipped.Checked == true)
            {
                for (int i = 0; i < gvSkippedDeduction.Rows.Count; i++)
                {

                    gvSkippedDeduction.Rows[i].Cells[0].Value = true;

                }
            }
            if (chkSelectAllSkipped.Checked == false)
            {
                for (int i = 0; i < gvSkippedDeduction.Rows.Count; i++)
                {

                    gvSkippedDeduction.Rows[i].Cells[0].Value = false;

                }
            }

        }

        private void btnCancelSkip_Click(object sender, EventArgs e)
        {
            String strDeleteStatus = "";
            Boolean boolDeleteStatus = true;
            for (int i = 0; i < gvSkippedDeduction.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(gvSkippedDeduction.Rows[i].Cells["Skip"].Value)) == true)
                {
                    try
                    {
                        this.variableAssigningForSelection(i);
                        mySkipDeduction.StrEmpNo = gvSkippedDeduction.Rows[i].Cells["EmpNo"].Value.ToString();
                        strDeleteStatus = mySkipDeduction.DeleteSkipDeduction();
                        if (!strDeleteStatus.ToUpper().Equals("DELETED"))
                        {
                            boolDeleteStatus = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error On Delete Skip, "+ex.Message);
                        boolDeleteStatus = false;
                    }
                }
            }
            if (boolDeleteStatus == true)
                MessageBox.Show("Selected Skipped Deduction Canceled  Successfully ");
            else
                MessageBox.Show("Selected Deduction Skip Canceled With Errors....");

            this.refreshGrid();

        

        }


    }
}