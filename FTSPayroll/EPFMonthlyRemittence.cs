using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace FTSPayroll
{
    public partial class EPFMonthlyRemittence : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.Reports myReport2 = new FTSPayRollBL.Reports();
        FTSPayRollBL.YearMonth myYM = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeMaster empMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();

        public EPFMonthlyRemittence()
        {
            InitializeComponent();
        }

        private void EPFMonthlyRemittence_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myYM.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myYM.getLastYearID();

            cmbYear_SelectedIndexChanged(null, null);
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = myReports.getEPFETFForRemmittence(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            dataSetReport.WriteXml("EPFETFForRemmittence.xml");
            Rpt_EPF_monthly_report myaclist = new Rpt_EPF_monthly_report();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            myaclist.SetParameterValue("companyname", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("companyaddress", FTSPayRollBL.Company.getCompanyAddress());
            myaclist.SetParameterValue("period", cmbMonth.Text + " of " + cmbYear.Text);
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {

        }

        private DataTable  ValidateEmpMasterDetails(DataTable dtTable,Boolean boolEPFEmptyAllow)
        {
            Boolean boolErrorRow = false;
            String empList = "";
            String strError = "OK";
            DataTable dtErrorTable = new DataTable("ErrorTable");
            dtErrorTable.Columns.Add("DivisionID");//0
            dtErrorTable.Columns.Add("EmpNo");//1
            dtErrorTable.Columns.Add("ZoneCode");//2
            dtErrorTable.Columns.Add("OCGrade");//3
            dtErrorTable.Columns.Add("EPFNo");//4
            dtErrorTable.Columns.Add("NIC");//5
            dtErrorTable.Columns.Add("MemberStatus");//6
            dtErrorTable.Columns.Add("EmployerNo");//7
            dtErrorTable.Columns.Add("Errors");//8
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dtErrorTable.NewRow();
            
                for (Int32 i = 0; i <= dtTable.Rows.Count - 1; i++)
                {
                    boolErrorRow = false;
                    strError = "";
                    if (boolEPFEmptyAllow)
                    {
                        if (String.IsNullOrEmpty(dtTable.Rows[i][7].ToString()) || !Regex.IsMatch(dtTable.Rows[i][7].ToString(), @"^[A-Z]$"))
                        {
                            strError += "Zone,";
                            //MessageBox.Show("Zone Code Of " + dtTable.Rows[i].Cells[0].ToString() + " Is Invalid!", "Invalid", MessageBoxButtons.OK);
                            boolErrorRow = true;
                        }
                        else if (!Regex.IsMatch(dtTable.Rows[i][6].ToString(), @"^[0-9]{3}$"))
                        {
                            strError += "OCGrade,";
                            //MessageBox.Show("OC Grade Of " + dtTable.Rows[i].Cells[0].ToString() + " Is Invalid!", "Invalid", MessageBoxButtons.OK);
                            boolErrorRow = true;
                        }
                        else if (!Regex.IsMatch(dtTable.Rows[i][2].ToString(), @"^[0-9]{0,6}$") )
                        {
                            strError += "EPFNo,";
                            //MessageBox.Show(cmbDivision.SelectedValue.ToString() + "-" + dtTable.Rows[i].Cells[0].ToString() + "\r\n EPF No Must Be 1 to 6 Digits");
                            boolErrorRow = true;
                        }
                        else if (!Regex.IsMatch(dtTable.Rows[i][8].ToString(), @"^(E|V|N)$"))
                        {
                            strError += "MemberStatus,";
                            boolErrorRow = true;
                        }
                        else
                        {
                            if (!Regex.IsMatch(dtTable.Rows[i][3].ToString(), @"^\d{9}(X|V)$") && !Regex.IsMatch(dtTable.Rows[i][3].ToString(), @"^\d{12}$"))
                            {
                                if (!String.IsNullOrEmpty(dtTable.Rows[i][3].ToString()))
                                {
                                    //MessageBox.Show("Invalid NIC Number, " + cmbDivision.SelectedValue.ToString() + "-" + dtTable.Rows[i].Cells[0].ToString() + "\r\n Please Correct NIC And Continue.");
                                    strError += "NIC,";
                                    boolErrorRow = true;
                                }
                                //else
                                //{
                                //    //empMaster.UpdateEmployeeDetailsGrid(cmbDivision.SelectedValue.ToString(), dtTable.Rows[i].Cells[0].ToString(), dtTable.Rows[i].Cells[2].ToString(), dtTable.Rows[i].Cells[3].ToString(), dtTable.Rows[i].Cells[4].ToString(), dtTable.Rows[i].Cells[5].ToString(), dtTable.Rows[i].Cells[6].ToString(), dtTable.Rows[i].Cells[7].ToString(), dtTable.Rows[i].Cells[8].ToString(), dtTable.Rows[i].Cells[9].ToString(), dtTable.Rows[i].Cells[10].ToString());
                                //}
                            }
                            //else
                            //{
                            //    //empMaster.UpdateEmployeeDetailsGrid(cmbDivision.SelectedValue.ToString(), dtTable.Rows[i].Cells[0].ToString(), dtTable.Rows[i].Cells[2].ToString(), dtTable.Rows[i].Cells[3].ToString(), dtTable.Rows[i].Cells[4].ToString(), dtTable.Rows[i].Cells[5].ToString(), dtTable.Rows[i].Cells[6].ToString(), dtTable.Rows[i].Cells[7].ToString(), dtTable.Rows[i].Cells[8].ToString(), dtTable.Rows[i].Cells[9].ToString(), dtTable.Rows[i].Cells[10].ToString());
                            //}
                        }
                        if (boolErrorRow == true)
                        {
                            dtRow = dtErrorTable.NewRow();
                            dtRow[0] = dtTable.Rows[i][10].ToString();
                            dtRow[1] = dtTable.Rows[i][0].ToString();
                            dtRow[2] = dtTable.Rows[i][7].ToString();
                            dtRow[3] = dtTable.Rows[i][6].ToString();
                            dtRow[4] = dtTable.Rows[i][2].ToString();
                            dtRow[5] = dtTable.Rows[i][3].ToString();
                            dtRow[6] = dtTable.Rows[i][8].ToString();
                            dtRow[7] = dtTable.Rows[i][9].ToString();
                            dtRow[8] = strError;
                            dtErrorTable.Rows.Add(dtRow);
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(dtTable.Rows[i][7].ToString()) || !Regex.IsMatch(dtTable.Rows[i][7].ToString(), @"^[A-Z]$"))
                        {
                            strError += "Zone,";
                            //MessageBox.Show("Zone Code Of " + dtTable.Rows[i].Cells[0].ToString() + " Is Invalid!", "Invalid", MessageBoxButtons.OK);
                            boolErrorRow = true;
                        }
                        else if (!Regex.IsMatch(dtTable.Rows[i][6].ToString(), @"^[0-9]{3}$"))
                        {
                            strError += "OCGrade,";
                            //MessageBox.Show("OC Grade Of " + dtTable.Rows[i].Cells[0].ToString() + " Is Invalid!", "Invalid", MessageBoxButtons.OK);
                            boolErrorRow = true;
                        }
                        else if (!Regex.IsMatch(dtTable.Rows[i][2].ToString(), @"^[0-9]{0,6}$") )
                        {
                            strError += "EPFNo,";
                            //MessageBox.Show(cmbDivision.SelectedValue.ToString() + "-" + dtTable.Rows[i].Cells[0].ToString() + "\r\n EPF No Must Be 1 to 6 Digits");
                            boolErrorRow = true;
                        }
                        else if (!Regex.IsMatch(dtTable.Rows[i][8].ToString(), @"^(E|V|N)$"))
                        {
                            strError += "MemberStatus,";
                            boolErrorRow = true;
                        }
                        else
                        {
                            if (!Regex.IsMatch(dtTable.Rows[i][3].ToString(), @"^\d{9}(X|V)$") && !Regex.IsMatch(dtTable.Rows[i][3].ToString(), @"^\d{12}$"))
                            {
                                if (!String.IsNullOrEmpty(dtTable.Rows[i][3].ToString()))
                                {
                                    //MessageBox.Show("Invalid NIC Number, " + cmbDivision.SelectedValue.ToString() + "-" + dtTable.Rows[i].Cells[0].ToString() + "\r\n Please Correct NIC And Continue.");
                                    strError += "NIC,";
                                    boolErrorRow = true;
                                }
                                //else
                                //{
                                //    //empMaster.UpdateEmployeeDetailsGrid(cmbDivision.SelectedValue.ToString(), dtTable.Rows[i].Cells[0].ToString(), dtTable.Rows[i].Cells[2].ToString(), dtTable.Rows[i].Cells[3].ToString(), dtTable.Rows[i].Cells[4].ToString(), dtTable.Rows[i].Cells[5].ToString(), dtTable.Rows[i].Cells[6].ToString(), dtTable.Rows[i].Cells[7].ToString(), dtTable.Rows[i].Cells[8].ToString(), dtTable.Rows[i].Cells[9].ToString(), dtTable.Rows[i].Cells[10].ToString());
                                //}
                            }
                            //else
                            //{
                            //    //empMaster.UpdateEmployeeDetailsGrid(cmbDivision.SelectedValue.ToString(), dtTable.Rows[i].Cells[0].ToString(), dtTable.Rows[i].Cells[2].ToString(), dtTable.Rows[i].Cells[3].ToString(), dtTable.Rows[i].Cells[4].ToString(), dtTable.Rows[i].Cells[5].ToString(), dtTable.Rows[i].Cells[6].ToString(), dtTable.Rows[i].Cells[7].ToString(), dtTable.Rows[i].Cells[8].ToString(), dtTable.Rows[i].Cells[9].ToString(), dtTable.Rows[i].Cells[10].ToString());
                            //}
                        }
                        if (boolErrorRow == true)
                        {
                            dtRow = dtErrorTable.NewRow();
                            dtRow[0] = dtTable.Rows[i][10].ToString();
                            dtRow[1] = dtTable.Rows[i][0].ToString();
                            dtRow[2] = dtTable.Rows[i][7].ToString();
                            dtRow[3] = dtTable.Rows[i][6].ToString();
                            dtRow[4] = dtTable.Rows[i][2].ToString();
                            dtRow[5] = dtTable.Rows[i][3].ToString();
                            dtRow[6] = dtTable.Rows[i][8].ToString();
                            dtRow[7] = dtTable.Rows[i][9].ToString();
                            dtRow[8] = strError;
                            dtErrorTable.Rows.Add(dtRow);
                        }
                    }
                    

                }
                return dtErrorTable;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dsDuplicateEPFNo = new DataSet();
            DataSet dsErroEmployerNo = new DataSet();
            DataTable dtEmpDetails = new DataTable("EmployeeTable");
            DataTable dtErrorsRpt = new DataTable("ErrorTable");
            DataSet dsErrorRpt = new DataSet("ErrorTable");
            //dsDuplicateEPFNo = myReport2.GetDuplicateEPFNos();
            //dsErroEmployerNo = myReport2.GetErrorEmployerNosInEmployeeMaster();
            Boolean boolSuccess = true;
            String strDupEntries = "";
            String strErrorEmployerNo = "";
            String strPayrollDuplicates = "";
            String strCheckrollDuplicates = "";
            String strEarningsEmployerErrors = "";
            //strPayrollDuplicates = myReport2.GetDuplicateEPFNosPayroll();
            strCheckrollDuplicates = myReport2.GetDuplicateEPFNosCheckroll();
            strEarningsEmployerErrors = myReport2.GetEmpEarningsWithoutEmployerNo(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));

            //Error Report
            DataSet dsDivisionReport = new DataSet();
            //dtEmpDetails = empMaster.ListEmployeesDetails("%");
            dtEmpDetails = empMaster.ListEmployeesDetailsForEform("%",Convert.ToInt32(cmbYear.SelectedValue.ToString()),Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            dsErrorRpt.Tables.Add(ValidateEmpMasterDetails(dtEmpDetails,false));
            
            //Report
            if (dsErrorRpt.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("There Are Employee Master Data Errors");
                dsErrorRpt.WriteXml("EformErrors.xml");

                EformErrorsReportRPT objReport = new EformErrorsReportRPT();
                objReport.SetDataSource(dsErrorRpt);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                if (strEarningsEmployerErrors.Length > 0)
                {
                    MessageBox.Show("Employer Numbers Of Following Employees Are Wrong, Please Correct Them And Re-generate \n" + strEarningsEmployerErrors, "Invalid Entries", MessageBoxButtons.OK);
                    boolSuccess = false;
                }
                //else if (strPayrollDuplicates.Length > 0)
                //{
                //    MessageBox.Show("Found Following Duplicate EPF Number(s) in Staff Payroll, Please Correct Them And Re-generate \n" + strPayrollDuplicates, "Invalid Entries", MessageBoxButtons.OK);
                //}
                else if (strCheckrollDuplicates.Length > 0)
                {
                    MessageBox.Show("Found Following Duplicate EPF Number(s) in Checkroll, Please Correct Them And Re-generate \n" + strCheckrollDuplicates, "Invalid Entries", MessageBoxButtons.OK);
                    boolSuccess = false;
                }
                //else if (dsDuplicateEPFNo.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dtr in dsDuplicateEPFNo.Tables[0].Rows)
                //    {
                //        strDupEntries += "\n\n" +"Employer NO: "+ dtr[0].ToString() + " " +"EPF NO: "+ dtr[1].ToString();
                //    }
                //    MessageBox.Show("Found Following Duplicate EPF Number(s), Please Correct Them And Re-generate "+strDupEntries,"Invalid Entries",MessageBoxButtons.OK);
                //}
                //else if (dsErroEmployerNo.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dtr1 in dsErroEmployerNo.Tables[0].Rows)
                //    {
                //        strErrorEmployerNo += "\n\n" + "Employer NO: " + dtr1[0].ToString() + " " + "DivisionID: " + dtr1[1].ToString() + " " + "EmpNo: " + dtr1[2].ToString() + " " + "EPFNO: " + dtr1[3].ToString();
                //    }
                //    //MessageBox.Show("Found Following Wrong Employer Number(s), Please Correct Them And Re-generate " + strErrorEmployerNo, "Invalid Entries", MessageBoxButtons.OK);
                //    if(strErrorEmployerNo.Length>100)
                //        MessageBox.Show("Found Following Wrong Employer Number(s), Please Correct Them And Re-generate " + strErrorEmployerNo.Substring(0, 100));
                //    else
                //        MessageBox.Show("Found Following Wrong Employer Number(s), Please Correct Them And Re-generate " + strErrorEmployerNo);
                //    boolSuccess = false;
                //}
                else if (dtEmpDetails.Rows.Count > 0)
                {
                    //employee details

                }
                if (!boolSuccess)
                {
                    MessageBox.Show("Create Failed");
                }
                else
                {
                    String strContributionPeriod = cmbYear.Text.PadLeft(4, '0') + cmbMonth.SelectedValue.ToString().PadLeft(2, '0');
                    DataSet dsEVEMC = new DataSet();
                    try
                    {
                        //wRITE INTO THE EVEMC FILE
                        String DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\EForms\";

                        String dirPath = DesktopPath;
                        String SDirectory = cmbYear.Text.PadLeft(4, '0') + cmbMonth.SelectedValue.ToString().PadLeft(2, '0') + FTSPayRollBL.User.StrEstate.ToString();
                        String filePath = "";
                        String fileName = "";
                        // Create a reference to a directory.
                        DirectoryInfo di = new DirectoryInfo(dirPath);
                        DirectoryInfo SubDirectory = new DirectoryInfo(SDirectory);
                        DirectoryInfo SubDi = new DirectoryInfo(dirPath + SDirectory);
                        // Create the directory only if it does not already exist.
                        if (di.Exists == false)
                        {
                        }
                        else if (SubDi.Exists == false)
                        {
                            SubDi.Create();
                        }
                        //else
                        //{
                        //    MessageBox.Show("Already Generated.\r\n If you want to Re-Generate \r\nPlease Delete The EForms Folder On Desktop And Re-Generate. ");
                        //}
                        if (SubDi.Exists == false)
                        {
                            di.Create();
                            DirectoryInfo dis = di.CreateSubdirectory(SDirectory);

                            fileName = "EVEMC";
                            filePath = dirPath + "\\" + SDirectory + "\\" + fileName + ".TXT";
                            if (File.Exists(filePath))
                            {
                                MessageBox.Show("Already Created");
                                //if (MessageBox.Show("Already Created, Do You Want To Replace", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                //{
                                //}
                            }
                            else
                            {
                                dsEVEMC = myReport2.GetEPF_EVEMC_Data(strContributionPeriod);
                                MessageBox.Show("EVEMC & EVEMP Files Generated Successfully!.");

                                foreach (DataRow dr2 in dsEVEMC.Tables[1].Rows)
                                {
                                    if (dr2.ItemArray[1].ToString() == "4543")
                                        MessageBox.Show("error");
                                    DataSet dsMC = new DataSet();
                                    dsMC = myReport2.getEmployerWiseEVEMC(dr2.ItemArray[2].ToString(), dr2.ItemArray[1].ToString());
                                    filePath = dirPath + "\\" + SDirectory + "\\" + fileName + dr2.ItemArray[1].ToString() + ".TXT";
                                    if (File.Exists(filePath))
                                    {
                                        MessageBox.Show(fileName + "Already Created");
                                        //if (MessageBox.Show("Already Created, Do You Want To Replace", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        //{
                                        //}
                                    }
                                    else
                                    {
                                        foreach (DataRow dr3 in dsMC.Tables[0].Rows)
                                        {
                                            String strLine = "";
                                            //strLine = dr3.ItemArray[0].ToString().PadLeft(1, ' ') + dr3.ItemArray[1].ToString().PadLeft(6, '0') + dr3.ItemArray[2].ToString().PadLeft(6, '0') + dr3.ItemArray[3].ToString().PadLeft(2, '0') + Convert.ToDecimal(dr3.ItemArray[4].ToString()).ToString("F2").PadLeft(12, '0') + dr3.ItemArray[5].ToString().PadLeft(5, '0') + dr3.ItemArray[6].ToString().PadLeft(1, ' ') + "70831150000010054204".PadRight(20, ' ') + dr3.ItemArray[8].ToString().PadLeft(10, ' ') + dr3.ItemArray[9].ToString().PadLeft(2, '0');
                                            //strLine = dr3.ItemArray[0].ToString().PadRight(20, ' ') + dr3.ItemArray[1].ToString().PadRight(40, ' ') + dr3.ItemArray[2].ToString().PadRight(20, ' ') + dr3.ItemArray[3].ToString().PadLeft(6, '0') + Convert.ToDecimal(dr3[4].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[5].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[6].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[7].ToString()).ToString("F2").PadLeft(10, '0') + dr3.ItemArray[8].ToString().PadLeft(1, 'E') + dr3.ItemArray[9].ToString().PadLeft(1, ' ') + dr3.ItemArray[10].ToString().PadLeft(6, '0') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[12].ToString().PadLeft(2, '0') + dr3.ItemArray[13].ToString().PadLeft(5, '0') + dr3.ItemArray[14].ToString().PadLeft(3, '0') + dr3.ItemArray[15].ToString().PadLeft(2, '0');
                                            if (FTSPayRollBL.Company.getCompanyCode().ToUpper().Equals("BPL"))
                                            {
                                                strLine = dr3.ItemArray[0].ToString().PadRight(20, ' ') + dr3.ItemArray[1].ToString().PadRight(40, ' ') + dr3.ItemArray[2].ToString().PadRight(20, ' ') + dr3.ItemArray[3].ToString().PadLeft(6, '0') + Convert.ToDecimal(dr3[4].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[5].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[6].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[7].ToString()).ToString("F2").PadLeft(12, '0') + dr3.ItemArray[8].ToString().PadLeft(1, 'E') + dr3.ItemArray[9].ToString().PadRight(1, ' ') + dr3.ItemArray[10].ToString().PadLeft(6, '0') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[12].ToString().PadLeft(2, ' ') + Convert.ToDecimal(dr3.ItemArray[13].ToString()).ToString().PadLeft(5, '0') + dr3.ItemArray[14].ToString().PadLeft(3, ' ');
                                            }
                                            else
                                            {
                                                strLine = dr3.ItemArray[0].ToString().PadRight(20, ' ') + dr3.ItemArray[1].ToString().PadRight(40, ' ') + dr3.ItemArray[2].ToString().PadRight(20, ' ') + dr3.ItemArray[3].ToString().PadLeft(6, ' ') + Convert.ToDecimal(dr3[4].ToString()).ToString("F2").PadLeft(10, ' ') + Convert.ToDecimal(dr3.ItemArray[5].ToString()).ToString("F2").PadLeft(10, ' ') + Convert.ToDecimal(dr3.ItemArray[6].ToString()).ToString("F2").PadLeft(10, ' ') + Convert.ToDecimal(dr3.ItemArray[7].ToString()).ToString("F2").PadLeft(12, ' ') + dr3.ItemArray[8].ToString().PadLeft(1, 'E') + dr3.ItemArray[9].ToString().PadRight(2, ' ') + dr3.ItemArray[10].ToString().PadLeft(5, '0') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[12].ToString().PadLeft(2, ' ') + Convert.ToDecimal(dr3.ItemArray[13].ToString()).ToString().PadLeft(5, ' ') + dr3.ItemArray[14].ToString().PadLeft(3, ' ');
                                            }
                                            try
                                            {
                                                using (System.IO.StreamWriter writer = new StreamWriter(filePath, true))
                                                {
                                                    writer.WriteLine(strLine);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error, " + ex.Message);
                                            }

                                        }
                                    }


                                }
                                //WRITE ON EVEMP
                                filePath = "";
                                fileName = "";
                                fileName = "EVEMP";
                                filePath = dirPath + "\\" + SDirectory + "\\" + fileName + ".TXT";
                                if (File.Exists(filePath))
                                {
                                    MessageBox.Show(fileName + "Alredy Created");
                                    //if (MessageBox.Show("Already Created, Do You Want To Replace", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    //{
                                    //}
                                }
                                else
                                {
                                    foreach (DataRow dr1 in dsEVEMC.Tables[1].Rows)
                                    {
                                        String strLine = "";
                                        strLine = dr1.ItemArray[0].ToString().PadLeft(1, ' ') + dr1.ItemArray[1].ToString().PadLeft(6, ' ') + dr1.ItemArray[2].ToString().PadLeft(6, '0') + dr1.ItemArray[3].ToString().PadLeft(2, '0') + Convert.ToDecimal(dr1.ItemArray[4].ToString()).ToString("F2").PadLeft(12, '0') + dr1.ItemArray[5].ToString().PadLeft(5, '0') + dr1.ItemArray[6].ToString().PadLeft(1, ' ') + "70831150000010054204".PadRight(20, ' ') + dr1.ItemArray[8].ToString().PadLeft(10, ' ') + dr1.ItemArray[9].ToString().PadLeft(2, '0');
                                        try
                                        {
                                            using (System.IO.StreamWriter writer = new StreamWriter(filePath, true))
                                            {
                                                writer.WriteLine(strLine);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error, " + ex.Message);
                                        }
                                    }
                                }

                            }
                            //

                            MessageBox.Show("EVEMC & EVEMP Files Generated Successfully!.");
                        }
                        else
                        {
                            MessageBox.Show("Already Generated.\r\n If you want to Re-Generate \r\nPlease Delete The EForms Folder On Desktop And Re-Generate. ");
                        }


                    }
                    catch { }
                }
            }
            

        }

        private void btnEVEMP_Click(object sender, EventArgs e)
        {
            DataSet dsReport = new DataSet();
            dsReport = myReport2.GetEVEMP(cmbYear.Text.PadLeft(4, '0') + cmbMonth.SelectedValue.ToString().PadLeft(2, '0'));
            dsReport.WriteXml("EVEMP.xml");

            EPF_EVEMP_RPT objReport = new EPF_EVEMP_RPT();
            objReport.SetDataSource(dsReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void btnEVEMC_Click(object sender, EventArgs e)
        {

            DataSet dsReport = new DataSet();
            dsReport = myReport2.GetEVEMC(cmbYear.Text.PadLeft(4, '0') + cmbMonth.SelectedValue.ToString().PadLeft(2, '0'));
            dsReport.WriteXml("EVEMC.xml");

            EPF_EVEMC_RPT objReport = new EPF_EVEMC_RPT();
            objReport.SetDataSource(dsReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.DataSource = myYM.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myYM.getLastMonthID();
        }

        private void btnETF_Click(object sender, EventArgs e)
        {
            DataSet dsDuplicateEPFNo = new DataSet();
            DataSet dsErroEmployerNo = new DataSet();
            //dsDuplicateEPFNo = myReport2.GetDuplicateEPFNos();
            dsErroEmployerNo = myReport2.GetErrorEmployerNosInEmployeeMaster();
            String strDupEntries = "";
            String strErrorEmployerNo = "";
            String strPayrollDuplicates = "";
            String strCheckrollDuplicates = "";
            String strEarningsEmployerErrors = "";
            //strPayrollDuplicates = myReport2.GetDuplicateEPFNosPayroll();
            strCheckrollDuplicates = myReport2.GetDuplicateEPFNosCheckroll();
            strEarningsEmployerErrors = myReport2.GetEmpEarningsWithoutEmployerNo(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            if (strEarningsEmployerErrors.Length > 0)
            {
                MessageBox.Show("Employer Numbers Of Following Employees Are Wrong, Please Correct Them And Re-generate \n" + strEarningsEmployerErrors, "Invalid Entries", MessageBoxButtons.OK);
            }
            //else if (strPayrollDuplicates.Length > 0)
            //{
            //    MessageBox.Show("Found Following Duplicate EPF Number(s) in Staff Payroll, Please Correct Them And Re-generate \n" + strPayrollDuplicates, "Invalid Entries", MessageBoxButtons.OK);
            //}
            else if (strCheckrollDuplicates.Length > 0)
            {
                MessageBox.Show("Found Following Duplicate EPF Number(s) in Checkroll, Please Correct Them And Re-generate \n" + strCheckrollDuplicates, "Invalid Entries", MessageBoxButtons.OK);
            }
            //else if (dsDuplicateEPFNo.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dtr in dsDuplicateEPFNo.Tables[0].Rows)
            //    {
            //        strDupEntries += "\n\n" +"Employer NO: "+ dtr[0].ToString() + " " +"EPF NO: "+ dtr[1].ToString();
            //    }
            //    MessageBox.Show("Found Following Duplicate EPF Number(s), Please Correct Them And Re-generate "+strDupEntries,"Invalid Entries",MessageBoxButtons.OK);
            //}
            else if (dsErroEmployerNo.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dtr1 in dsErroEmployerNo.Tables[0].Rows)
                {
                    strErrorEmployerNo += "\n\n" + "Employer NO: " + dtr1[0].ToString() + " " + "DivisionID: " + dtr1[1].ToString() + " " + "EmpNo: " + dtr1[2].ToString() + " " + "EPFNO: " + dtr1[3].ToString();
                }
                MessageBox.Show("Found Following Wrong Employer Number(s), Please Correct Them And Re-generate " + strErrorEmployerNo, "Invalid Entries", MessageBoxButtons.OK);
            }
            if (false)
            {
                MessageBox.Show("error");
            }
            else
            {
                String strContributionPeriod = cmbYear.Text.PadLeft(4, '0') + cmbMonth.SelectedValue.ToString().PadLeft(2, '0');
                DataSet dsEVEMC = new DataSet();
                try
                {
                    //wRITE INTO THE EVEMC FILE
                    String DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\EForms_ETF\";

                    String dirPath = DesktopPath;
                    String SDirectory = cmbYear.Text.PadLeft(4, '0') + cmbMonth.SelectedValue.ToString().PadLeft(2, '0') + FTSPayRollBL.User.StrEstate.ToString();
                    String filePath = "";
                    String fileName = "";
                    // Create a reference to a directory.
                    DirectoryInfo di = new DirectoryInfo(dirPath);
                    DirectoryInfo SubDirectory = new DirectoryInfo(SDirectory);
                    DirectoryInfo SubDi = new DirectoryInfo(dirPath + SDirectory);
                    // Create the directory only if it does not already exist.
                    if (di.Exists == false)
                    {
                    }
                    else if (SubDi.Exists == false)
                    {
                        SubDi.Create();
                    }
                    //else
                    //{
                    //    MessageBox.Show("Already Generated.\r\n If you want to Re-Generate \r\nPlease Delete The EForms Folder On Desktop And Re-Generate. ");
                    //}
                    if (SubDi.Exists == false)
                    {
                        di.Create();
                        DirectoryInfo dis = di.CreateSubdirectory(SDirectory);

                        fileName = "ETF";
                        filePath = dirPath + "\\" + SDirectory + "\\" + fileName + ".TXT";
                        if (File.Exists(filePath))
                        {
                            MessageBox.Show("Already Created");
                            //if (MessageBox.Show("Already Created, Do You Want To Replace", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            //{
                            //}
                        }
                        else
                        {
                            dsEVEMC = myReport2.GetEPF_EVEMC_Data(strContributionPeriod);
                            MessageBox.Show("ETF Files Generated Successfully!.");

                            foreach (DataRow dr2 in dsEVEMC.Tables[1].Rows)
                            {
                                DataSet dsMC = new DataSet();
                                dsMC = myReport2.getEmployerWiseEVEMC(dr2.ItemArray[2].ToString(), dr2.ItemArray[1].ToString());
                                filePath = dirPath + "\\" + SDirectory + "\\" + fileName + dr2.ItemArray[1].ToString() + ".TXT";
                                if (File.Exists(filePath))
                                {
                                    MessageBox.Show(fileName + "Already Created");
                                    //if (MessageBox.Show("Already Created, Do You Want To Replace", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    //{
                                    //}
                                }
                                else
                                {
                                    String strLineHeadder = "";
                                    int intCountETF = 0;
                                    Decimal decTotETF = 0;
                                    foreach (DataRow dr3 in dsMC.Tables[0].Rows)
                                    {
                                        String strLine = "";
                                        //strLine = dr3.ItemArray[0].ToString().PadLeft(1, ' ') + dr3.ItemArray[1].ToString().PadLeft(6, '0') + dr3.ItemArray[2].ToString().PadLeft(6, '0') + dr3.ItemArray[3].ToString().PadLeft(2, '0') + Convert.ToDecimal(dr3.ItemArray[4].ToString()).ToString("F2").PadLeft(12, '0') + dr3.ItemArray[5].ToString().PadLeft(5, '0') + dr3.ItemArray[6].ToString().PadLeft(1, ' ') + "70831150000010054204".PadRight(20, ' ') + dr3.ItemArray[8].ToString().PadLeft(10, ' ') + dr3.ItemArray[9].ToString().PadLeft(2, '0');
                                        //strLine = dr3.ItemArray[0].ToString().PadRight(20, ' ') + dr3.ItemArray[1].ToString().PadRight(40, ' ') + dr3.ItemArray[2].ToString().PadRight(20, ' ') + dr3.ItemArray[3].ToString().PadLeft(6, '0') + Convert.ToDecimal(dr3[4].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[5].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[6].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[7].ToString()).ToString("F2").PadLeft(10, '0') + dr3.ItemArray[8].ToString().PadLeft(1, 'E') + dr3.ItemArray[9].ToString().PadLeft(1, ' ') + dr3.ItemArray[10].ToString().PadLeft(6, '0') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[12].ToString().PadLeft(2, '0') + dr3.ItemArray[13].ToString().PadLeft(5, '0') + dr3.ItemArray[14].ToString().PadLeft(3, '0') + dr3.ItemArray[15].ToString().PadLeft(2, '0');

                                        //strLine = dr3.ItemArray[0].ToString().PadRight(20, ' ') + dr3.ItemArray[1].ToString().PadRight(40, ' ') + dr3.ItemArray[2].ToString().PadRight(20, ' ') + dr3.ItemArray[3].ToString().PadLeft(6, '0') + Convert.ToDecimal(dr3[4].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[5].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[6].ToString()).ToString("F2").PadLeft(10, '0') + Convert.ToDecimal(dr3.ItemArray[7].ToString()).ToString("F2").PadLeft(12, '0') + dr3.ItemArray[8].ToString().PadLeft(1, 'E') + dr3.ItemArray[9].ToString().PadLeft(1, ' ') + dr3.ItemArray[10].ToString().PadLeft(6, '0') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[12].ToString().PadLeft(2, '0') + Convert.ToDecimal(dr3.ItemArray[13].ToString()).ToString().PadLeft(5, '0') + dr3.ItemArray[14].ToString().PadLeft(3, '0');
                                        if (dr3.ItemArray[0].ToString().Trim().Length < 10)
                                            strLine = "D" + dr3.ItemArray[9].ToString().PadRight(2, ' ') + dr3.ItemArray[10].ToString().PadLeft(6, '0')  + dr3.ItemArray[3].ToString().PadLeft(6, '0') + dr3.ItemArray[2].ToString().PadRight(20, ' ') + dr3.ItemArray[1].ToString().PadRight(30, ' ') + dr3.ItemArray[0].ToString().Trim().PadLeft(12, ' ') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + Convert.ToInt32((Convert.ToDecimal(dr3.ItemArray[6].ToString()) * 3 * 100)).ToString().PadLeft(9, '0');
                                        else
                                            strLine = "D" + dr3.ItemArray[9].ToString().PadRight(2, ' ') + dr3.ItemArray[10].ToString().PadLeft(6, '0')  + dr3.ItemArray[3].ToString().PadLeft(6, '0') + dr3.ItemArray[2].ToString().PadRight(20, ' ') + dr3.ItemArray[1].ToString().PadRight(30, ' ') + dr3.ItemArray[0].ToString().Trim().PadLeft(12, ' ') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + Convert.ToInt32((Convert.ToDecimal(dr3.ItemArray[6].ToString()) * 3 * 10)).ToString().PadLeft(9, '0');
                                        try
                                        {
                                            using (System.IO.StreamWriter writer = new StreamWriter(filePath, true))
                                            {
                                                writer.WriteLine(strLine);
                                                intCountETF++;
                                                decTotETF = decTotETF + Convert.ToInt32((Convert.ToDecimal(dr3.ItemArray[6].ToString()) * 3 * 10));
                                                strLineHeadder = "";
                                                strLineHeadder = "H" + dr3.ItemArray[9].ToString().PadRight(2, ' ') + dr3.ItemArray[10].ToString().PadLeft(6, '0') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[11].ToString().PadLeft(6, ' ') + intCountETF.ToString().PadLeft(6, '0') + Convert.ToInt32(decTotETF).ToString().PadLeft(14, '0') + "24";
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error, " + ex.Message);
                                        }

                                    }
                                    try
                                    {
                                        //strLineHeadder="H"+ dr3.ItemArray[10].ToString().PadLeft(8, '0')+dr3.ItemArray[11].ToString().PadLeft(6, ' ') + dr3.ItemArray[11].ToString().PadLeft(6, ' ')+intCountETF.ToString().PadLeft(6,'0')+decTotETF.ToString().PadLeft(8,'0')+Convert.ToString((intCountETF+1)).PadLeft(2,'0');
                                        using (System.IO.StreamWriter writer = new StreamWriter(filePath, true))
                                        {
                                            writer.WriteLine(strLineHeadder);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error, " + ex.Message);
                                    }
                                }


                            }
                            ////WRITE ON EVEMP
                            //filePath = "";
                            //fileName = "";
                            //fileName = "EVEMP";
                            //filePath = dirPath + "\\" + SDirectory + "\\" + fileName + ".TXT";
                            //if (File.Exists(filePath))
                            //{
                            //    MessageBox.Show(fileName + "Alredy Created");
                            //    //if (MessageBox.Show("Already Created, Do You Want To Replace", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            //    //{
                            //    //}
                            //}
                            //else
                            //{
                            //    foreach (DataRow dr1 in dsEVEMC.Tables[1].Rows)
                            //    {
                            //        String strLine = "";
                            //        strLine = dr1.ItemArray[0].ToString().PadLeft(1, ' ') + dr1.ItemArray[1].ToString().PadLeft(6, '0') + dr1.ItemArray[2].ToString().PadLeft(6, '0') + dr1.ItemArray[3].ToString().PadLeft(2, '0') + Convert.ToDecimal(dr1.ItemArray[4].ToString()).ToString("F2").PadLeft(12, '0') + dr1.ItemArray[5].ToString().PadLeft(5, '0') + dr1.ItemArray[6].ToString().PadLeft(1, ' ') + "70831150000010054204".PadRight(20, ' ') + dr1.ItemArray[8].ToString().PadLeft(10, ' ') + dr1.ItemArray[9].ToString().PadLeft(2, '0');
                            //        try
                            //        {
                            //            using (System.IO.StreamWriter writer = new StreamWriter(filePath, true))
                            //            {
                            //                writer.WriteLine(strLine);
                            //            }
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            MessageBox.Show("Error, " + ex.Message);
                            //        }
                            //    }
                            //}

                        }
                        //

                        MessageBox.Show("ETF Files Generated Successfully!.");
                    }
                    else
                    {
                        MessageBox.Show("Already Generated.\r\n If you want to Re-Generate \r\nPlease Delete The EForms Folder On Desktop And Re-Generate. ");
                    }


                }
                catch { }
            }
        }

        private void btnCheckErrors_Click(object sender, EventArgs e)
        {
            DataTable dtEmpDetails = new DataTable("EmployeeTable");
            DataTable dtErrorsRpt = new DataTable("ErrorTable");
            DataSet dsErrorRpt = new DataSet("ErrorTable");
            //dsDuplicateEPFNo = myReport2.GetDuplicateEPFNos();
            Boolean boolSuccess = true;
            //strPayrollDuplicates = myReport2.GetDuplicateEPFNosPayroll();
            //Error Report
            DataSet dsDivisionReport = new DataSet();
            //dtEmpDetails = empMaster.ListEmployeesDetails("%");

            dtEmpDetails = empMaster.ListEmployeesDetails("%");
            dsErrorRpt.Tables.Add(ValidateEmpMasterDetails(dtEmpDetails,true));

            //Report
            if (dsErrorRpt.Tables[0].Rows.Count > 0)
            {
                dsErrorRpt.WriteXml("EformErrors.xml");

                EformErrorsReportRPT objReport = new EformErrorsReportRPT();
                objReport.SetDataSource(dsErrorRpt);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                MessageBox.Show("EFROM Employee Master Data Errors Not Found...!");
            }
        }
    }
}