using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class CHKWages
    {
        EstateDivisionBlock EstDiv = new EstateDivisionBlock();

        private Int32 intYear;

        public Int32 IntYear
        {
            get { return intYear; }
            set { intYear = value; }
        }

        private Int32 intMonth;

        public Int32 IntMonth
        {
            get { return intMonth; }
            set { intMonth = value; }
        }

        private DateTime processFromDate;

        public DateTime ProcessFromDate
        {
            get { return processFromDate; }
            set { processFromDate = value; }
        }

        private DateTime processToDate;

        public DateTime ProcessToDate
        {
            get { return processToDate; }
            set { processToDate = value; }
        }

        private String strDivID;

        public String StrDivID
        {
            get { return strDivID; }
            set { strDivID = value; }
        }

        public Boolean isAvailable(String strDiv, Int32 Year, Int32 Month)
        {
            Boolean Available = false;
            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");

            DataRow dtrow;
            SqlDataReader datareader;
            dtrow = dt.NewRow();

            datareader = SQLHelper.ExecuteReader("SELECT [ProcessYear] ,[ProcessMonth] FROM [dbo].[CHKGLEntriesNEW] WHERE [ProcessYear] = '" + Year + "' AND [ProcessMonth] = '" + Month + "' AND [DivisionId] = '" + strDiv + "' GROUP BY [ProcessYear] ,[ProcessMonth] ", CommandType.Text);

            while (datareader.Read())
            {
                dtrow = dt.NewRow();

                if (!datareader.IsDBNull(0))
                {
                    dtrow[0] = datareader.GetInt32(0);
                }
                if (!datareader.IsDBNull(1))
                {
                    dtrow[1] = datareader.GetInt32(1);
                }
                dt.Rows.Add(dtrow);
            }
            datareader.Close();

            if (dt.Rows.Count > 0)
            {
                Available = true;
            }
            else
            {
                Available = false;
            }
            return Available;
        }

        public String processGLEntriesNEW(String WorkCode, String FieldType)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(processFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(processToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivID;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workCode", SqlDbType.VarChar, 50);
            param.Value = WorkCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldType", SqlDbType.VarChar, 50);
            param.Value = FieldType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spProcessGLEntriesNEW", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            if (Status.Equals("COMPLETED"))
            {
                //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKProcessDetails] ([Division] ,[Category] ,[ProcessYear] ,[ProcessMonth] ,[PreviewYesNo] ,[ProcessedYesNo] ,[ProcessedDate] ,[CreateDateTime] ,[UserId]) VALUES ('" + StrDivision + "' ,1 ,'" + Convert.ToDateTime(DtProcessFromDate.ToShortDateString()).Year + "' ,'" + Convert.ToDateTime(DtProcessFromDate.ToShortDateString()).Month + "' ,1 ,1 ,GETDATE() ,GETDATE() ,'ADMIN' )", CommandType.Text);
            }
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public DateTime GetDate(int intYear, int intMonth)
        {
            return new DateTime(intYear, intMonth, 1);
        }

        public DateTime GetEndDate(DateTime dtDate)
        {
            if (dtDate != null)
            {
                return dtDate.AddMonths(1).AddDays(-1);
            }
        }


        public DataTable GetCHKWages(Int32 intYear, Int32 intMonth)
        {
            DateTime dtStart = GetDate(intYear, intMonth);
            DateTime dtEnd = GetEndDate(dtStart);
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add(new DataColumn("ProcessYear"));//0
            dtMain.Columns.Add(new DataColumn("ProcessMonth"));//1
            dtMain.Columns.Add(new DataColumn("DivisionId"));//2
            dtMain.Columns.Add(new DataColumn("PlkHoliday"));//3
            dtMain.Columns.Add(new DataColumn("PlkMen"));//4            
            dtMain.Columns.Add(new DataColumn("PlkWomen"));//5
            dtMain.Columns.Add(new DataColumn("TotPlkManDays"));//6
            dtMain.Columns.Add(new DataColumn("SundryHoliday"));
            dtMain.Columns.Add(new DataColumn("SundryMen"));//8
            dtMain.Columns.Add(new DataColumn("SundryWomen"));
            dtMain.Columns.Add(new DataColumn("TotSundryManDays"));//10
            dtMain.Columns.Add(new DataColumn("PlkNames"));
            dtMain.Columns.Add(new DataColumn("SundryNames"));//12
            dtMain.Columns.Add(new DataColumn("TotalLabourName"));//13
            dtMain.Columns.Add(new DataColumn("CashPlk"));//14
            dtMain.Columns.Add(new DataColumn("BlockPlk"));//15
            dtMain.Columns.Add(new DataColumn("TotalCashPlk"));
            dtMain.Columns.Add(new DataColumn("TotCashSundry"));//17
            dtMain.Columns.Add(new DataColumn("OverKilosAmount"));
            dtMain.Columns.Add(new DataColumn("PSSAmount"));//19
            dtMain.Columns.Add(new DataColumn("EPF12"));
            dtMain.Columns.Add(new DataColumn("ETF3"));//21
            dtMain.Columns.Add(new DataColumn("OverTime"));
            dtMain.Columns.Add(new DataColumn("ExtraRate"));//23
            dtMain.Columns.Add(new DataColumn("IncentiveAmount"));//24
            dtMain.Columns.Add(new DataColumn("HalfMandays"));//25
            DataRow dtRow;
            DataSet ds = new DataSet();

            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = dtStart;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = dtEnd;
            paramList.Add(param);
            ds = SQLHelper.FillDataSet("spGetCheckrollWagesDataFieldWise", CommandType.StoredProcedure, paramList);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dtRow = dtMain.NewRow();
                    dtRow[0] = dr.ItemArray[1].ToString();
                    dtRow[1] = dr.ItemArray[2].ToString();
                    dtRow[2] = dr.ItemArray[3].ToString();
                    dtRow[3] = dr.ItemArray[4].ToString();
                    dtRow[4] = dr.ItemArray[5].ToString();
                    dtRow[5] = dr.ItemArray[6].ToString();
                    dtRow[6] = dr.ItemArray[7].ToString();
                    dtRow[7] = dr.ItemArray[8].ToString();
                    dtRow[8] = dr.ItemArray[9].ToString();
                    dtRow[9] = dr.ItemArray[10].ToString();
                    dtRow[10] = dr.ItemArray[11].ToString();
                    dtRow[11] = dr.ItemArray[12].ToString();
                    dtRow[12] = dr.ItemArray[13].ToString();
                    dtRow[13] = dr.ItemArray[14].ToString();
                    dtRow[14] = dr.ItemArray[15].ToString();
                    dtRow[15] = dr.ItemArray[16].ToString();
                    dtRow[16] = dr.ItemArray[17].ToString();
                    dtRow[17] = dr.ItemArray[18].ToString();
                    dtRow[18] = dr.ItemArray[19].ToString();
                    dtRow[19] = dr.ItemArray[20].ToString();
                    dtRow[20] = dr.ItemArray[21].ToString();
                    dtRow[21] = dr.ItemArray[22].ToString();
                    dtRow[22] = dr.ItemArray[23].ToString();
                    dtRow[23] = dr.ItemArray[24].ToString();
                    dtRow[24] = dr.ItemArray[25].ToString();
                    dtRow[25] = dr.ItemArray[26].ToString();
                    dtMain.Rows.Add(dtRow);
                }
            }
            return dtMain;
        }

        public DataTable GetFieldWiseCHKWages(Int32 intYear, Int32 intMonth, String strDiv)
        {
            DateTime dtStart = GetDate(intYear, intMonth);
            DateTime dtEnd = GetEndDate(dtStart);
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add(new DataColumn("ProcessYear"));//0
            dtMain.Columns.Add(new DataColumn("ProcessMonth"));//1
            dtMain.Columns.Add(new DataColumn("DivisionId"));//2
            dtMain.Columns.Add(new DataColumn("FieldID"));//3
            dtMain.Columns.Add(new DataColumn("JobCode"));//4            
            dtMain.Columns.Add(new DataColumn("JobName"));//5
            dtMain.Columns.Add(new DataColumn("HolidayManDays"));//6
            dtMain.Columns.Add(new DataColumn("Men"));
            dtMain.Columns.Add(new DataColumn("Women"));//8
            dtMain.Columns.Add(new DataColumn("TotManDays"));
            dtMain.Columns.Add(new DataColumn("LabourNames"));//10
            dtMain.Columns.Add(new DataColumn("CashWork"));
            dtMain.Columns.Add(new DataColumn("OverKilosAmount"));//12
            dtMain.Columns.Add(new DataColumn("PSSAmount"));//13
            dtMain.Columns.Add(new DataColumn("EPF12"));//14
            dtMain.Columns.Add(new DataColumn("ETF3"));//15
            dtMain.Columns.Add(new DataColumn("OverTime"));
            dtMain.Columns.Add(new DataColumn("ExtraRate"));//17
            dtMain.Columns.Add(new DataColumn("IncentiveAmount"));
            dtMain.Columns.Add(new DataColumn("HalfMandays"));//19
            DataRow dtRow;
            DataTable DivisionTbl;
            //DataSet ds = new DataSet();
            if (strDiv.Equals("ALL"))
            {
                DivisionTbl = EstDiv.ListEstateDivisions();
            }
            else
            {
                DivisionTbl = EstDiv.ListEstateDivisions(strDiv);
            }

            foreach (DataRow drow in DivisionTbl.Rows)
            {
                DataSet ds = new DataSet();
                SqlParameter param = new SqlParameter();
                List<SqlParameter> paramList = new List<SqlParameter>();
                param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
                param.Value = dtStart;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
                param.Value = dtEnd;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@strDiv", SqlDbType.VarChar, 50);
                param.Value = drow[0].ToString();
                paramList.Add(param);
                ds = SQLHelper.FillDataSet("spGetFieldWiseCheckrollWagesData", CommandType.StoredProcedure, paramList);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dtRow = dtMain.NewRow();
                        dtRow[0] = dr.ItemArray[1].ToString();
                        dtRow[1] = dr.ItemArray[2].ToString();
                        dtRow[2] = dr.ItemArray[3].ToString();
                        dtRow[3] = dr.ItemArray[4].ToString();
                        dtRow[4] = dr.ItemArray[5].ToString();
                        dtRow[5] = dr.ItemArray[6].ToString();
                        dtRow[6] = dr.ItemArray[7].ToString();
                        dtRow[7] = dr.ItemArray[8].ToString();
                        dtRow[8] = dr.ItemArray[9].ToString();
                        dtRow[9] = dr.ItemArray[10].ToString();
                        dtRow[10] = dr.ItemArray[11].ToString();
                        dtRow[11] = dr.ItemArray[12].ToString();
                        dtRow[12] = dr.ItemArray[13].ToString();
                        dtRow[13] = dr.ItemArray[14].ToString();
                        dtRow[14] = dr.ItemArray[15].ToString();
                        dtRow[15] = dr.ItemArray[16].ToString();
                        dtRow[16] = dr.ItemArray[17].ToString();
                        dtRow[17] = dr.ItemArray[18].ToString();
                        dtRow[18] = dr.ItemArray[19].ToString();
                        dtRow[19] = dr.ItemArray[20].ToString();
                        dtMain.Rows.Add(dtRow);
                    }
                    ds.Dispose();
                }

            }
            return dtMain;
        }

        public DataTable GetDivisionWiseCHKWages(Int32 intYear, Int32 intMonth, String strDiv)
        {
            DateTime dtStart = GetDate(intYear, intMonth);
            DateTime dtEnd = GetEndDate(dtStart);
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add(new DataColumn("ProcessYear"));//0
            dtMain.Columns.Add(new DataColumn("ProcessMonth"));//1
            dtMain.Columns.Add(new DataColumn("DivisionId"));//2
            dtMain.Columns.Add(new DataColumn("FieldID"));//3
            dtMain.Columns.Add(new DataColumn("JobCode"));//4            
            dtMain.Columns.Add(new DataColumn("JobName"));//5
            dtMain.Columns.Add(new DataColumn("HolidayManDays"));//6
            dtMain.Columns.Add(new DataColumn("Men"));
            dtMain.Columns.Add(new DataColumn("Women"));//8
            dtMain.Columns.Add(new DataColumn("TotManDays"));
            dtMain.Columns.Add(new DataColumn("LabourNames"));//10
            dtMain.Columns.Add(new DataColumn("CashWork"));
            dtMain.Columns.Add(new DataColumn("OverKilosAmount"));//12
            dtMain.Columns.Add(new DataColumn("PSSAmount"));//13
            dtMain.Columns.Add(new DataColumn("EPF12"));//14
            dtMain.Columns.Add(new DataColumn("ETF3"));//15
            dtMain.Columns.Add(new DataColumn("OverTime"));
            dtMain.Columns.Add(new DataColumn("ExtraRate"));//17
            dtMain.Columns.Add(new DataColumn("IncentiveAmount"));
            dtMain.Columns.Add(new DataColumn("HalfMandays"));//19
            dtMain.Columns.Add(new DataColumn("MenHolidayManDays"));//20
            dtMain.Columns.Add(new DataColumn("WomenHolidayManDays"));//21
            dtMain.Columns.Add(new DataColumn("MenHalfMandays"));
            dtMain.Columns.Add(new DataColumn("WomenHalfMandays"));//23
            DataRow dtRow;
            DataTable DivisionTbl;
            //DataSet ds = new DataSet();
            if (strDiv.Equals("ALL"))
            {
                DivisionTbl = EstDiv.ListEstateDivisions();
            }
            else
            {
                DivisionTbl = EstDiv.ListEstateDivisions(strDiv);
            }

            foreach (DataRow drow in DivisionTbl.Rows)
            {
                DataSet ds = new DataSet();
                SqlParameter param = new SqlParameter();
                List<SqlParameter> paramList = new List<SqlParameter>();
                param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
                param.Value = dtStart;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
                param.Value = dtEnd;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@strDiv", SqlDbType.VarChar, 50);
                param.Value = drow[0].ToString();
                paramList.Add(param);
                ds = SQLHelper.FillDataSet("spGetDivisionWiseCheckrollWagesData", CommandType.StoredProcedure, paramList);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dtRow = dtMain.NewRow();
                        dtRow[0] = dr.ItemArray[1].ToString();
                        dtRow[1] = dr.ItemArray[2].ToString();
                        dtRow[2] = dr.ItemArray[3].ToString();
                        dtRow[3] = dr.ItemArray[4].ToString();
                        dtRow[4] = dr.ItemArray[5].ToString();
                        dtRow[5] = dr.ItemArray[6].ToString();
                        dtRow[6] = dr.ItemArray[7].ToString();
                        dtRow[7] = dr.ItemArray[8].ToString();
                        dtRow[8] = dr.ItemArray[9].ToString();
                        dtRow[9] = dr.ItemArray[10].ToString();
                        dtRow[10] = dr.ItemArray[11].ToString();
                        dtRow[11] = dr.ItemArray[12].ToString();
                        dtRow[12] = dr.ItemArray[13].ToString();
                        dtRow[13] = dr.ItemArray[14].ToString();
                        dtRow[14] = dr.ItemArray[15].ToString();
                        dtRow[15] = dr.ItemArray[16].ToString();
                        dtRow[16] = dr.ItemArray[17].ToString();
                        dtRow[17] = dr.ItemArray[18].ToString();
                        dtRow[18] = dr.ItemArray[19].ToString();
                        dtRow[19] = dr.ItemArray[20].ToString();
                        dtRow[20] = dr.ItemArray[23].ToString();
                        dtRow[21] = dr.ItemArray[24].ToString();
                        dtRow[22] = dr.ItemArray[25].ToString();
                        dtRow[23] = dr.ItemArray[26].ToString();
                        dtMain.Rows.Add(dtRow);
                    }
                    ds.Dispose();
                }

            }
            return dtMain;
        }
        public DataTable GetLentLabourDetails(Int32 IntYear, Int32 IntMonth)
        {
            DateTime dtStart = GetDate(IntYear, IntMonth);
            DateTime dtEnd = GetEndDate(dtStart);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("LentFromDiv"));
            dt.Columns.Add(new DataColumn("LentToDiv"));
            dt.Columns.Add(new DataColumn("NoOfEmployees"));
            dt.Columns.Add(new DataColumn("WagesAmount"));
            dt.Columns.Add(new DataColumn("CashWorkAmount"));
            dt.Columns.Add(new DataColumn("ExtraRatesAmount"));
            dt.Columns.Add(new DataColumn("OverTimeAmount"));
            dt.Columns.Add(new DataColumn("OverKilosAmount"));
            dt.Columns.Add(new DataColumn("PSSAmount"));
            dt.Columns.Add(new DataColumn("E.P.F12%"));
            dt.Columns.Add(new DataColumn("E.T.F3%"));
            dt.Columns.Add(new DataColumn("IncentiveAmount"));
            dt.Columns.Add(new DataColumn("LentLabourTotal"));

            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName,  dbo.DailyGroundTransactions.DivisionID, COUNT(dbo.DailyGroundTransactions.LabourDivision) AS NoOfLabours, SUM(dbo.DailyGroundTransactions.Expenditure + dbo.DailyGroundTransactions.PluckingExpenditure) AS Expenditure, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, " +
                                       "SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRIAmount, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS IncentiveAmount FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND " +
                                       "dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE (dbo.DailyGroundTransactions.LabourDivision <> 'NA') AND (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtStart + "', 102) AND CONVERT(DATETIME, '" + dtEnd + "', 102)) GROUP BY dbo.JobMaster.JobShortName, dbo.JobMaster.JobName,  dbo.DailyGroundTransactions.DivisionID", CommandType.Text);

            DataSet ds1 = new DataSet();
            ds1 = SQLHelper.FillDataSet("SELECT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourDivision,  dbo.DailyGroundTransactions.DivisionID FROM dbo.EmployeeMaster INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID AND dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName " +
                                        "WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtStart + "', 102) AND CONVERT(DATETIME, '" + dtEnd + "', 102)) GROUP BY dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourDivision,  dbo.DailyGroundTransactions.DivisionID HAVING (dbo.DailyGroundTransactions.LabourDivision <> 'NA')", CommandType.Text);

            DataSet ds2 = new DataSet();
            ds2 = SQLHelper.FillDataSet("SELECT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.CHKOvertime.DivisionCode, SUM(dbo.CHKOvertime.Expenditure) AS Overtime FROM dbo.CHKOvertime INNER JOIN dbo.DailyGroundTransactions ON dbo.CHKOvertime.EmployeeNo = dbo.DailyGroundTransactions.EmpNo AND dbo.CHKOvertime.DivisionCode = dbo.DailyGroundTransactions.DivisionID INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtStart + "', 102) AND CONVERT(DATETIME, '" + dtEnd + "', 102)) AND (dbo.CHKOvertime.LabourDivision <> 'NA') GROUP BY dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.LabourDivision", CommandType.Text);


            DataRow dtRow;

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dtRow = dt.NewRow();

                    dtRow[0] = dr.ItemArray[0].ToString();
                    dtRow[1] = dr.ItemArray[1].ToString();
                    dtRow[2] = dr.ItemArray[2].ToString();
                    dtRow[3] = "NA";
                    dtRow[4] = dr.ItemArray[3].ToString();
                    dtRow[5] = dr.ItemArray[4].ToString();
                    dtRow[6] = dr.ItemArray[5].ToString();
                    dtRow[7] = dr.ItemArray[6].ToString();
                    dtRow[8] = "0.00";
                    dtRow[9] = dr.ItemArray[7].ToString();
                    dtRow[10] = dr.ItemArray[8].ToString();
                    dtRow[11] = dr.ItemArray[9].ToString();
                    dtRow[12] = dr.ItemArray[10].ToString();
                    dtRow[13] = dr.ItemArray[11].ToString();
                    dtRow[14] = "0.00";
                    dt.Rows.Add(dtRow);
                }
            }
            if (ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr.ItemArray[0].ToString() == dr1.ItemArray[0].ToString() && dr.ItemArray[1].ToString() == dr1.ItemArray[1].ToString() && dr.ItemArray[2].ToString() == dr1.ItemArray[3].ToString())
                        {
                            dr[3] = dr1.ItemArray[2].ToString();
                        }
                    }
                }
            }
            if (ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataRow dr1 in ds2.Tables[0].Rows)
                    {
                        if (dr.ItemArray[0].ToString() == dr1.ItemArray[0].ToString() && dr.ItemArray[1].ToString() == dr1.ItemArray[2].ToString() && dr.ItemArray[3].ToString() == dr1.ItemArray[3].ToString())
                        {
                            dr[8] = dr1.ItemArray[4].ToString();
                        }
                        else
                        {
                            dr[8] = "0.00";
                        }
                    }
                }
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[8] = "0.00";
                }
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[14] = Math.Round(Convert.ToDecimal(dr.ItemArray[5].ToString()) + Convert.ToDecimal(dr.ItemArray[6].ToString()) + Convert.ToDecimal(dr.ItemArray[7].ToString()) + Convert.ToDecimal(dr.ItemArray[8].ToString()) + Convert.ToDecimal(dr.ItemArray[9].ToString()) + Convert.ToDecimal(dr.ItemArray[10].ToString()) + Convert.ToDecimal(dr.ItemArray[11].ToString()) + Convert.ToDecimal(dr.ItemArray[12].ToString()), 2);
                }
            }
            return dt;
        }

        public DataTable GetLentLabourData(Int32 IntYear, Int32 IntMonth, String strDiv)
        {
            DateTime dtStart = GetDate(IntYear, IntMonth);
            DateTime dtEnd = GetEndDate(dtStart);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LabourType"));
            dt.Columns.Add(new DataColumn("FromDivision"));
            dt.Columns.Add(new DataColumn("ToEstate"));
            dt.Columns.Add(new DataColumn("ToDivision"));
            dt.Columns.Add(new DataColumn("ToField"));
            dt.Columns.Add(new DataColumn("Job"));
            dt.Columns.Add(new DataColumn("NoOfLabours"));
            dt.Columns.Add(new DataColumn("Kilos"));
            dt.Columns.Add(new DataColumn("NamePay"));
            dt.Columns.Add(new DataColumn("OverKgAmount"));
            dt.Columns.Add(new DataColumn("CashKgAmount"));
            dt.Columns.Add(new DataColumn("CashSundryAmount"));
            dt.Columns.Add(new DataColumn("ExtraRates"));
            dt.Columns.Add(new DataColumn("PSS"));
            dt.Columns.Add(new DataColumn("EPF12"));
            dt.Columns.Add(new DataColumn("ETF3"));
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT  LabourType, DivisionID AS FromDivision, LabourEstate AS ToEstate, LabourDivision AS ToDivision, LabourField AS ToField, WorkCodeID,  COUNT(EmpNo) AS NoOfLabours, SUM(WorkQty) AS Kilos, SUM(DailyBasic) AS NamePay, SUM(OverKgAmount) AS OverKgAmount,  SUM(CashKgAmount) AS CashKgAmount, SUM(CashSundryAmount) AS CashSundryAmount, SUM(ExtraRates) AS ExtraRates, SUM(PRIAmount) AS PSS,  SUM(EPF12) AS EPF12, SUM(ETF3) AS ETF3 FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtStart + "', 102) AND CONVERT(DATETIME, '" + dtEnd + "', 102)) and (DivisionID like '" + strDiv + "') GROUP BY LabourType, DivisionID, LabourEstate, LabourDivision, LabourField, WorkCodeID HAVING      (LabourType <> 'general')", CommandType.Text);
            DataRow dtRow;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = dr.ItemArray[0].ToString();
                    dtRow[1] = dr.ItemArray[1].ToString();
                    dtRow[2] = dr.ItemArray[2].ToString();
                    dtRow[3] = dr.ItemArray[3].ToString();
                    dtRow[4] = dr.ItemArray[4].ToString();
                    dtRow[5] = dr.ItemArray[5].ToString();
                    dtRow[6] = dr.ItemArray[6].ToString();
                    dtRow[7] = dr.ItemArray[7].ToString();
                    dtRow[8] = dr.ItemArray[8].ToString();
                    dtRow[9] = dr.ItemArray[9].ToString();
                    dtRow[10] = dr.ItemArray[10].ToString();
                    dtRow[11] = dr.ItemArray[11].ToString();
                    dtRow[12] = dr.ItemArray[12].ToString();
                    dtRow[13] = dr.ItemArray[13].ToString();
                    dtRow[14] = dr.ItemArray[14].ToString();
                    dtRow[15] = dr.ItemArray[15].ToString();
                    dt.Rows.Add(dtRow);
                }
            }
            return dt;
        }

        public DataTable GetLentLabourDataDayWise(Int32 IntYear, Int32 IntMonth, String strDiv)
        {
            DateTime dtStart = GetDate(IntYear, IntMonth);
            DateTime dtEnd = GetEndDate(dtStart);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Date"));
            dt.Columns.Add(new DataColumn("LabourType"));
            dt.Columns.Add(new DataColumn("FromDivision"));
            dt.Columns.Add(new DataColumn("ToEstate"));
            dt.Columns.Add(new DataColumn("ToDivision"));
            dt.Columns.Add(new DataColumn("ToField"));
            dt.Columns.Add(new DataColumn("Job"));
            dt.Columns.Add(new DataColumn("NoOfLabours"));
            dt.Columns.Add(new DataColumn("Kilos"));
            dt.Columns.Add(new DataColumn("NamePay"));
            dt.Columns.Add(new DataColumn("OverKgAmount"));
            dt.Columns.Add(new DataColumn("CashKgAmount"));
            dt.Columns.Add(new DataColumn("CashSundryAmount"));
            dt.Columns.Add(new DataColumn("ExtraRates"));
            dt.Columns.Add(new DataColumn("PSS"));
            dt.Columns.Add(new DataColumn("EPF12"));
            dt.Columns.Add(new DataColumn("ETF3"));
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT        DateEntered, LabourType, DivisionID AS FromDivision, LabourEstate AS ToEstate, LabourDivision AS ToDivision, LabourField AS ToField, WorkCodeID,  COUNT(EmpNo) AS NoOfLabours, SUM(WorkQty) AS Kilos, SUM(DailyBasic) AS NamePay, SUM(OverKgAmount) AS OverKgAmount, SUM(CashKgAmount)  AS CashKgAmount, SUM(CashSundryAmount) AS CashSundryAmount, SUM(ExtraRates) AS ExtraRates, SUM(PRIAmount) AS PSS, SUM(EPF12) AS EPF12, SUM(ETF3)  AS ETF3 FROM            dbo.DailyGroundTransactions WHERE        (DivisionID LIKE '" + strDiv + "') AND (DateEntered BETWEEN CONVERT(DATETIME, '" + dtStart + "', 102) AND CONVERT(DATETIME, '" + dtEnd + "', 102)) GROUP BY DateEntered, LabourType, DivisionID, LabourEstate, LabourDivision, LabourField, WorkCodeID HAVING        (LabourType <> 'general')", CommandType.Text);
            DataRow dtRow;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = dr.ItemArray[0].ToString();
                    dtRow[1] = dr.ItemArray[1].ToString();
                    dtRow[2] = dr.ItemArray[2].ToString();
                    dtRow[3] = dr.ItemArray[3].ToString();
                    dtRow[4] = dr.ItemArray[4].ToString();
                    dtRow[5] = dr.ItemArray[5].ToString();
                    dtRow[6] = dr.ItemArray[6].ToString();
                    dtRow[7] = dr.ItemArray[7].ToString();
                    dtRow[8] = dr.ItemArray[8].ToString();
                    dtRow[9] = dr.ItemArray[9].ToString();
                    dtRow[10] = dr.ItemArray[10].ToString();
                    dtRow[11] = dr.ItemArray[11].ToString();
                    dtRow[12] = dr.ItemArray[12].ToString();
                    dtRow[13] = dr.ItemArray[13].ToString();
                    dtRow[14] = dr.ItemArray[14].ToString();
                    dtRow[15] = dr.ItemArray[15].ToString();
                    dtRow[16] = dr.ItemArray[16].ToString();
                    dt.Rows.Add(dtRow);
                }
            }
            return dt;
        }


        public DataTable getExtraRates(Int32 intYear, Int32 intMonth)
        {
            DataTable dtMain = new DataTable();


            dtMain.Columns.Add(new DataColumn("JobName"));
            dtMain.Columns.Add(new DataColumn("JobShortName"));
            dtMain.Columns.Add(new DataColumn("ExtraRate"));
            dtMain.Columns.Add(new DataColumn("Mandays"));

            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.JobMaster.JobName, dbo.JobMaster.JobShortName FROM dbo.JobMaster INNER JOIN dbo.FTSCheckRollExtraRates ON dbo.JobMaster.JobShortName = dbo.FTSCheckRollExtraRates.WorkCode", CommandType.Text);

            DataSet dsDetails = new DataSet();
            dsDetails = SQLHelper.FillDataSet("SELECT WorkCodeID, SUM(ExtraRates) AS EX, SUM(ManDays) AS M FROM dbo.DailyGroundTransactions WHERE (ExtraRates > 0) AND (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "') GROUP BY WorkCodeID", CommandType.Text);

            DataRow dtRow;

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dtRow = dtMain.NewRow();

                    dtRow[0] = dr.ItemArray[0].ToString();
                    dtRow[1] = dr.ItemArray[1].ToString();
                    dtRow[2] = 0.00;
                    dtRow[3] = 0.00;
                    dtMain.Rows.Add(dtRow);
                }
            }
            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dtMain.Rows)
                {
                    foreach (DataRow dr in dsDetails.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[1].ToString() == dr.ItemArray[0].ToString())
                        {
                            drMain[2] = Convert.ToDecimal(dr.ItemArray[1].ToString());
                            drMain[3] = Convert.ToDecimal(dr.ItemArray[2].ToString());
                        }
                    }
                }
            }

            return dtMain;
        }

        public DataTable FieldWiseCheckrollWages(DateTime dtstartDate, DateTime dtEndDate, String strFieldType)
        {
            DataSet ds = new DataSet();
            DataTable dtFin = new DataTable();
            dtFin.Columns.Add(new DataColumn("Autokey"));
            dtFin.Columns.Add(new DataColumn("ProcessYear"));
            dtFin.Columns.Add(new DataColumn("ProcessMonth"));//2
            dtFin.Columns.Add(new DataColumn("FieldDivision"));
            dtFin.Columns.Add(new DataColumn("FieldId"));//4
            dtFin.Columns.Add(new DataColumn("WorkCodeId"));
            dtFin.Columns.Add(new DataColumn("WorkCodeName"));//6
            dtFin.Columns.Add(new DataColumn("Mandays"));
            dtFin.Columns.Add(new DataColumn("TotalCashManDays"));//8
            dtFin.Columns.Add(new DataColumn("NormalAmount"));
            dtFin.Columns.Add(new DataColumn("CashWork"));//10
            dtFin.Columns.Add(new DataColumn("IncentiveAmount"));
            dtFin.Columns.Add(new DataColumn("PRIAmount"));//12
            dtFin.Columns.Add(new DataColumn("EPF12"));
            dtFin.Columns.Add(new DataColumn("ETF3"));//14
            dtFin.Columns.Add(new DataColumn("OverKilosAmount"));
            dtFin.Columns.Add(new DataColumn("OverTime"));//16
            dtFin.Columns.Add(new DataColumn("ScrapAmount"));
            dtFin.Columns.Add(new DataColumn("ExtraRate"));//18
            dtFin.Columns.Add(new DataColumn("HalfNames"));
            dtFin.Columns.Add(new DataColumn("CreatedDate"));//20
            dtFin.Columns.Add(new DataColumn("UserID"));


            DataSet ds1 = new DataSet();
            ds1 = SQLHelper.FillDataSet("SELECT DivisionID, Type, FieldID, Extent FROM dbo.EstateField WHERE (Type = '" + strFieldType + "') GROUP BY DivisionID, Type, FieldID, Extent", CommandType.Text);
            //ds1 = SQLHelper.FillDataSet("SELECT DivisionID, Type, FieldID FROM dbo.EstateField GROUP BY DivisionID, Type, FieldID ",CommandType.Text);

            foreach (DataRow drDivField in ds1.Tables[0].Rows)
            {

                String strDiv = drDivField.ItemArray[0].ToString();
                String strDivField = drDivField.ItemArray[2].ToString();

                String Status = "";
                DataRow dtRow;
                dtRow = dtFin.NewRow();

                SqlParameter param = new SqlParameter();
                List<SqlParameter> paramList = new List<SqlParameter>();
                param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
                param.Value = dtstartDate;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
                param.Value = dtEndDate;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@strDiv", SqlDbType.VarChar, 50);
                param.Value = strDiv;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@strDivField", SqlDbType.VarChar, 50);
                param.Value = strDivField;
                paramList.Add(param);
                ds = SQLHelper.FillDataSet("spGetCheckrollWagesDataFieldWise", CommandType.StoredProcedure, paramList);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dtRow = dtFin.NewRow();

                    dtRow[0] = dr.ItemArray[0].ToString();
                    dtRow[1] = dr.ItemArray[1].ToString();
                    dtRow[2] = dr.ItemArray[2].ToString();
                    dtRow[3] = dr.ItemArray[3].ToString();
                    dtRow[4] = dr.ItemArray[4].ToString() + "  " + drDivField.ItemArray[3].ToString();
                    dtRow[5] = dr.ItemArray[5].ToString();
                    dtRow[6] = dr.ItemArray[6].ToString();
                    dtRow[7] = dr.ItemArray[7].ToString();
                    dtRow[8] = dr.ItemArray[8].ToString();
                    dtRow[9] = dr.ItemArray[9].ToString();
                    dtRow[10] = dr.ItemArray[10].ToString();
                    dtRow[11] = dr.ItemArray[11].ToString();
                    dtRow[12] = dr.ItemArray[12].ToString();
                    dtRow[13] = dr.ItemArray[13].ToString();
                    dtRow[14] = dr.ItemArray[14].ToString();
                    dtRow[15] = dr.ItemArray[15].ToString();
                    dtRow[16] = dr.ItemArray[16].ToString();
                    dtRow[17] = dr.ItemArray[17].ToString();
                    dtRow[18] = dr.ItemArray[18].ToString();
                    dtRow[19] = dr.ItemArray[19].ToString();
                    dtRow[20] = dr.ItemArray[20].ToString();
                    dtRow[21] = dr.ItemArray[21].ToString();
                    dtFin.Rows.Add(dtRow);
                }
            }

            return dtFin;
        }


        public DataTable GetDivisionWiseCheckrollwages(Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("TAPMAN"));
            dt.Columns.Add(new DataColumn("SUNMan"));
            dt.Columns.Add(new DataColumn("TAPHalfDays"));//2
            dt.Columns.Add(new DataColumn("SUNHalfDays"));
            dt.Columns.Add(new DataColumn("TAPNamePay"));//4
            dt.Columns.Add(new DataColumn("SUNNamePay"));
            dt.Columns.Add(new DataColumn("SCRAP"));//6
            dt.Columns.Add(new DataColumn("dsCashTAP"));
            dt.Columns.Add(new DataColumn("ExtraRate"));//8
            dt.Columns.Add(new DataColumn("OverKilos"));
            dt.Columns.Add(new DataColumn("dsCashSun"));//10
            dt.Columns.Add(new DataColumn("AttIncentive"));
            dt.Columns.Add(new DataColumn("dsUnpaid"));//12
            dt.Columns.Add(new DataColumn("GrossEarnings"));
            dt.Columns.Add(new DataColumn("DivisionId"));//14
            dt.Columns.Add(new DataColumn("ScrapQty"));
            dt.Columns.Add(new DataColumn("DoubleTappingkgs"));//16
            dt.Columns.Add(new DataColumn("DoubleTappingAmt"));
            dt.Columns.Add(new DataColumn("PSS"));//18
            dt.Columns.Add(new DataColumn("OverTime"));
            dt.Columns.Add(new DataColumn("OverKilosAmt"));//20      
            dt.Columns.Add(new DataColumn("TAPHoliFull"));
            dt.Columns.Add(new DataColumn("SUNHoliFull"));//22
            dt.Columns.Add(new DataColumn("TAPHoliHalf"));
            dt.Columns.Add(new DataColumn("SUNHoliHalf"));//24


            DataSet dsEMPMON = new DataSet();
            dsEMPMON = SQLHelper.FillDataSet("SELECT SUM(PluckingManDays) AS TAPMAN, SUM(SundryManDays) AS SUNMan, SUM(PluckingNamePay) AS TAPNamePay, SUM(SundryNamePay) AS SUNNamePay,  SUM(ExtraRates) AS ExtraRate, SUM(OverKilos) AS OverKilos,  DivisionId, SUM(PRIAmount) AS PSS, SUM(OverTime) AS Overtime, SUM(OverKilosPay) AS OverKilosAmt, SUM(AttIncentive) AS AttIncentive  FROM dbo.EmpMonthlyEarnings GROUP BY DivisionId, Year, Month HAVING (Year = '" + intYear + "') AND (Month = '" + intMonth + "')", CommandType.Text);

            DataSet dsTapHalf = new DataSet();
            dsTapHalf = SQLHelper.FillDataSet("SELECT SUM(HolidayHalfNames) AS TAPHalfDays, DivisionID FROM dbo.DailyGroundTransactions WHERE (WorkCodeID = 'PLK') GROUP BY DivisionID, YEAR(DateEntered), MONTH(DateEntered) HAVING (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')", CommandType.Text);
            //dsTapHalf = SQLHelper.FillDataSet("SELECT SUM(HolidayPLKManDays) AS tapholi, DivisionId FROM dbo.EmpMonthlyEarnings WHERE (Month = '"+intMonth+"') AND (Year = '"+intYear+"') GROUP BY DivisionId, Year", CommandType.Text);

            DataSet dsSunHalf = new DataSet();
            dsSunHalf = SQLHelper.FillDataSet("SELECT SUM(HolidayHalfNames) AS SUNHalfDays, DivisionID FROM dbo.DailyGroundTransactions WHERE (WorkCodeID <> 'PLK') GROUP BY DivisionID, YEAR(DateEntered), MONTH(DateEntered) HAVING (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')", CommandType.Text);
            //dsSunHalf = SQLHelper.FillDataSet("SELECT SUM(HolidaySundryManDays) AS holisun, DivisionId FROM dbo.EmpMonthlyEarnings WHERE (Month = '" + intMonth + "') AND (Year = '" + intYear + "') GROUP BY DivisionId, Year", CommandType.Text);

            DataSet dsCashTAP = new DataSet();
            dsCashTAP = SQLHelper.FillDataSet("SELECT SUM(Expenditure) AS CASHTAPNamePay, DivisionID FROM dbo.DailyGroundTransactions WHERE (WorkType = 2) AND (WorkCodeID = 'PLK') AND (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "') GROUP BY DivisionID, YEAR(DateEntered), MONTH(DateEntered)", CommandType.Text);

            DataSet dsCashSun = new DataSet();
            dsCashSun = SQLHelper.FillDataSet("SELECT SUM(Expenditure) AS CASHSundry, DivisionID FROM dbo.DailyGroundTransactions WHERE (WorkType = 2) AND (WorkCodeID <> 'PLK') AND (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "') GROUP BY DivisionID, YEAR(DateEntered), MONTH(DateEntered)", CommandType.Text);

            DataSet dsUnpaid = new DataSet();
            dsUnpaid = SQLHelper.FillDataSet("SELECT SUM(MadeUpBalance) AS UnPaid, DivisionId FROM dbo.EmpMonthlyFinalWeges GROUP BY DivisionId, WageMonth, WageYear HAVING (WageYear = '" + intYear + "') AND (WageMonth = '" + intMonth + "')", CommandType.Text);

            DataSet dsIncentive = new DataSet();
            dsIncentive = SQLHelper.FillDataSet("SELECT SUM(IncentiveAmount) AS incentive, DivisionID AS Div FROM DailyGroundTransactions WHERE (MONTH(DateEntered) = '" + intMonth + "') AND (YEAR(DateEntered) = '" + intYear + "') GROUP BY DivisionID, YEAR(DateEntered)", CommandType.Text);

            DataSet TAPHoliFull = new DataSet();
            TAPHoliFull = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS TAPHalfDays, DivisionID FROM dbo.DailyGroundTransactions WHERE (WorkCodeID = 'PLK') AND (FullHalf = 2) AND (HolidayYesNo = 1) GROUP BY DivisionID, YEAR(DateEntered), MONTH(DateEntered) HAVING (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')", CommandType.Text);

            DataSet SUNHoliFull = new DataSet();
            SUNHoliFull = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS SUNHalfDays, DivisionID FROM dbo.DailyGroundTransactions WHERE (WorkCodeID <> 'PLK') AND (FullHalf = 2) AND (HolidayYesNo = 1) GROUP BY DivisionID, YEAR(DateEntered), MONTH(DateEntered) HAVING (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')", CommandType.Text);

            DataSet TAPHoliHalf = new DataSet();
            TAPHoliHalf = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS SUNHalfDays, DivisionID FROM dbo.DailyGroundTransactions WHERE (WorkCodeID = 'PLK') AND (FullHalf = 1) AND (HolidayYesNo = 1) GROUP BY DivisionID, YEAR(DateEntered), MONTH(DateEntered) HAVING (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')", CommandType.Text);

            DataSet SUNHoliHalf = new DataSet();
            SUNHoliHalf = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS SUNHalfDays, DivisionID FROM dbo.DailyGroundTransactions WHERE (WorkCodeID <> 'PLK') AND (FullHalf = 1) AND (HolidayYesNo = 1) GROUP BY DivisionID, YEAR(DateEntered), MONTH(DateEntered) HAVING (YEAR(DateEntered) = '" + intYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')", CommandType.Text);

            DataRow dtRow;

            if (dsEMPMON.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsEMPMON.Tables[0].Rows)
                {
                    dtRow = dt.NewRow();

                    dtRow[0] = dr.ItemArray[0].ToString();
                    dtRow[1] = dr.ItemArray[1].ToString();
                    dtRow[2] = "0.00";
                    dtRow[3] = "0.00";
                    dtRow[4] = dr.ItemArray[2].ToString();
                    dtRow[5] = dr.ItemArray[3].ToString();
                    dtRow[6] = "0.00";
                    dtRow[7] = "0.00";
                    dtRow[8] = dr.ItemArray[4].ToString();
                    dtRow[9] = dr.ItemArray[5].ToString();
                    dtRow[10] = "0.00";
                    dtRow[11] = "0.00";
                    dtRow[12] = "0.00";
                    dtRow[13] = "0.00";
                    dtRow[14] = dr.ItemArray[6].ToString();
                    dtRow[15] = "0.00";
                    dtRow[16] = "0.00";
                    dtRow[17] = "0.00";
                    dtRow[18] = dr.ItemArray[7].ToString();
                    dtRow[19] = dr.ItemArray[8].ToString();
                    dtRow[20] = dr.ItemArray[9].ToString();
                    dtRow[21] = "0.00";
                    dtRow[22] = "0.00";
                    dtRow[23] = "0.00";
                    dtRow[24] = "0.00";
                    dt.Rows.Add(dtRow);
                }
            }

            if (dsTapHalf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in dsTapHalf.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[2] = Math.Round(Convert.ToDecimal(dr.ItemArray[0].ToString()), 2);
                        }
                    }
                }
            }

            if (dsSunHalf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in dsSunHalf.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[3] = Math.Round(Convert.ToDecimal(dr.ItemArray[0].ToString()), 2);
                        }
                    }
                }
            }
            if (dsCashTAP.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in dsCashTAP.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[7] = dr.ItemArray[0].ToString();
                        }
                    }
                }
            }
            if (dsCashSun.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in dsCashSun.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[10] = dr.ItemArray[0].ToString();
                        }
                    }
                }
            }

            if (dsUnpaid.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in dsUnpaid.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[12] = dr.ItemArray[0].ToString();
                        }
                    }
                }
            }


            if (dsIncentive.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in dsIncentive.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[11] = dr.ItemArray[0].ToString();
                        }
                    }
                }
            }
            if (TAPHoliFull.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in TAPHoliFull.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[21] = Math.Round(Convert.ToDecimal(dr.ItemArray[0].ToString()), 2);
                        }
                    }
                }
            }
            if (SUNHoliFull.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in SUNHoliFull.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[22] = Math.Round(Convert.ToDecimal(dr.ItemArray[0].ToString()), 2);
                        }
                    }
                }
            }
            if (TAPHoliHalf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in TAPHoliHalf.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[23] = Math.Round(Convert.ToDecimal(dr.ItemArray[0].ToString()), 2);
                        }
                    }
                }
            }
            if (SUNHoliHalf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMain in dt.Rows)
                {
                    foreach (DataRow dr in SUNHoliHalf.Tables[0].Rows)
                    {
                        if (drMain.ItemArray[14].ToString() == dr.ItemArray[1].ToString())
                        {
                            drMain[24] = Math.Round(Convert.ToDecimal(dr.ItemArray[0].ToString()), 2);
                        }
                    }
                }
            }

            return dt;

        }

        public DataSet GetOverTimeSummaryRep(String strYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT Job, SUM(Expenditure) AS AMT, DivisionCode FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY Job, DivisionCode, YEAR(OtDate), MONTH(OtDate)", CommandType.Text);
            return ds;
        }

        public DataSet GetCheckrollWagesForMonth(Int32 intYear, Int32 intMonth, String strDiv)
        {
            //DataSet ds=new DataSet("CheckrollWages");
            //ds=SQLHelper.FillDataSet("SELECT dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalMandays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, dbo.CHKGLEntries.WagesPay, dbo.CHKGLEntries.CashWork, dbo.CHKGLEntries.ExtraRate,  dbo.CHKGLEntries.Overtime, dbo.CHKGLEntries.OverKGAmount, dbo.CHKGLEntries.PRIAmount, dbo.CHKGLEntries.EPF12, dbo.CHKGLEntries.ETF3,  dbo.CHKGLEntries.TotalWOIncentive, dbo.CHKGLEntries.IncentiveAmount, dbo.CHKGLEntries.TotalWithIncentive FROM  dbo.DailyGroundTransactions INNER JOIN dbo.CHKGLEntries ON YEAR(dbo.DailyGroundTransactions.DateEntered) = dbo.CHKGLEntries.ProcessYear AND MONTH(dbo.DailyGroundTransactions.DateEntered)  = dbo.CHKGLEntries.ProcessMonth AND dbo.DailyGroundTransactions.DivisionID = dbo.CHKGLEntries.DivisionId AND  dbo.DailyGroundTransactions.WorkCodeID = dbo.CHKGLEntries.WorkCodeId WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '"+intYear+"') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '"+intMonth+"') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKGLEntries.WagesPay, dbo.CHKGLEntries.CashWork,  dbo.CHKGLEntries.ExtraRate, dbo.CHKGLEntries.Overtime, dbo.CHKGLEntries.OverKGAmount, dbo.CHKGLEntries.PRIAmount, dbo.CHKGLEntries.EPF12,  dbo.CHKGLEntries.ETF3, dbo.CHKGLEntries.TotalWOIncentive, dbo.CHKGLEntries.IncentiveAmount, dbo.CHKGLEntries.TotalWithIncentive HAVING (dbo.DailyGroundTransactions.DivisionID = '"+strDiv+"')",CommandType.Text);
            //return ds;
            DataSet ds = new DataSet("CheckrollWages");
            ds = SQLHelper.FillDataSet("SELECT     dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalMandays,                       SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, dbo.CHKGLEntries.WagesPay, dbo.CHKGLEntries.CashWork, dbo.CHKGLEntries.ExtraRate,dbo.CHKGLEntries.Overtime, dbo.CHKGLEntries.OverKGAmount, dbo.CHKGLEntries.PRIAmount, dbo.CHKGLEntries.EPF12, dbo.CHKGLEntries.ETF3,dbo.CHKGLEntries.TotalWOIncentive, dbo.CHKGLEntries.IncentiveAmount, dbo.CHKGLEntries.TotalWithIncentive, ISNULL(dbo.CHKJobAnalysisCodes.Description, 'NA') AS Description, ISNULL(dbo.JobMaster.AnalyzeCode, 'NA') AS AnalyzeCode,dbo.JobMaster.JobName  FROM         dbo.DailyGroundTransactions INNER JOIN                        dbo.CHKGLEntries ON YEAR(dbo.DailyGroundTransactions.DateEntered) = dbo.CHKGLEntries.ProcessYear AND MONTH(dbo.DailyGroundTransactions.DateEntered)= dbo.CHKGLEntries.ProcessMonth AND dbo.DailyGroundTransactions.DivisionID = dbo.CHKGLEntries.DivisionId AND dbo.DailyGroundTransactions.WorkCodeID = dbo.CHKGLEntries.WorkCodeId INNER JOIN dbo.JobMaster ON dbo.CHKGLEntries.WorkCodeId = dbo.JobMaster.JobShortName LEFT OUTER JOIN                        dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')  GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKGLEntries.WagesPay, dbo.CHKGLEntries.CashWork,dbo.CHKGLEntries.ExtraRate, dbo.CHKGLEntries.Overtime, dbo.CHKGLEntries.OverKGAmount, dbo.CHKGLEntries.PRIAmount, dbo.CHKGLEntries.EPF12,                         dbo.CHKGLEntries.ETF3, dbo.CHKGLEntries.TotalWOIncentive, dbo.CHKGLEntries.IncentiveAmount, dbo.CHKGLEntries.TotalWithIncentive,                         dbo.CHKJobAnalysisCodes.Description, dbo.JobMaster.AnalyzeCode,dbo.JobMaster.JobName HAVING (dbo.DailyGroundTransactions.DivisionID like '" + strDiv + "')  ", CommandType.Text);
            return ds;
        }

        //public DataSet GetCheckrollWagesForMonthBFProcess( Int32 intYear, Int32 intMonth, String strDiv)
        //{
        //    DateTime FromDate=new DateTime(intYear,intMonth,1);
        //    DateTime ToDate=FromDate.AddMonths(1).AddDays(-1);
        //    //DataSet ds=new DataSet("CheckrollWages");
        //    //ds=SQLHelper.FillDataSet("SELECT dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalMandays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, dbo.CHKGLEntries.WagesPay, dbo.CHKGLEntries.CashWork, dbo.CHKGLEntries.ExtraRate,  dbo.CHKGLEntries.Overtime, dbo.CHKGLEntries.OverKGAmount, dbo.CHKGLEntries.PRIAmount, dbo.CHKGLEntries.EPF12, dbo.CHKGLEntries.ETF3,  dbo.CHKGLEntries.TotalWOIncentive, dbo.CHKGLEntries.IncentiveAmount, dbo.CHKGLEntries.TotalWithIncentive FROM  dbo.DailyGroundTransactions INNER JOIN dbo.CHKGLEntries ON YEAR(dbo.DailyGroundTransactions.DateEntered) = dbo.CHKGLEntries.ProcessYear AND MONTH(dbo.DailyGroundTransactions.DateEntered)  = dbo.CHKGLEntries.ProcessMonth AND dbo.DailyGroundTransactions.DivisionID = dbo.CHKGLEntries.DivisionId AND  dbo.DailyGroundTransactions.WorkCodeID = dbo.CHKGLEntries.WorkCodeId WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '"+intYear+"') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '"+intMonth+"') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKGLEntries.WagesPay, dbo.CHKGLEntries.CashWork,  dbo.CHKGLEntries.ExtraRate, dbo.CHKGLEntries.Overtime, dbo.CHKGLEntries.OverKGAmount, dbo.CHKGLEntries.PRIAmount, dbo.CHKGLEntries.EPF12,  dbo.CHKGLEntries.ETF3, dbo.CHKGLEntries.TotalWOIncentive, dbo.CHKGLEntries.IncentiveAmount, dbo.CHKGLEntries.TotalWithIncentive HAVING (dbo.DailyGroundTransactions.DivisionID = '"+strDiv+"')",CommandType.Text);
        //    //return ds;
        //    DataSet ds = new DataSet("CheckrollWagesBFProcess");
        //    //ds = SQLHelper.FillDataSet("SELECT     DivisionID, WorkCodeID, SUM(ManDays) AS NormalManDays, SUM(CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk')  THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  AS CashSundry, SUM(ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime WHERE      (OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionCode like '"+strDiv+"')  AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(OverKgAmount) AS OverKgAmount, SUM(PRIAmount) AS PSS, SUM(EPF12)  AS EPF12, SUM(ETF3) AS ETF3, SUM(IncentiveAmount) AS Incentive, SUM(DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionCode like '"+strDiv+"')  AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(OverKgAmount) + SUM(PRIAmount) + SUM(EPF12) + SUM(ETF3) + SUM(IncentiveAmount)  AS TotalWithIncentive, SUM(DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionCode like '"+strDiv+"')  AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(OverKgAmount) + SUM(PRIAmount) + SUM(EPF12) + SUM(ETF3)  AS totalWithoutIncentive FROM         dbo.DailyGroundTransactions WHERE     (DateEntered BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionID like '"+strDiv+"') GROUP BY DivisionID, WorkCodeID ", CommandType.Text);
        //    //ds = SQLHelper.FillDataSet("SELECT     dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName ", CommandType.Text);

        //    ds = SQLHelper.FillDataSet("SELECT      dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY  dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName order by dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
        //    return ds;
        //}

        public DataSet GetCheckrollWagesForMonthBFProcessFieldwise(Int32 intYear, Int32 intMonth, String strDiv, String Field, String intCropType)
        {
            DateTime FromDate = new DateTime(intYear, intMonth, 1);
            DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);

            DataSet ds = new DataSet("CheckrollWagesBFProcess");

            if (intCropType == "1")
            {

                //NIsalads = SQLHelper.FillDataSet("SELECT      dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY  dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName order by dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT      dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.CHKGLEntriesNEW.FieldType, dbo.DailyGroundTransactions.CropType FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.CHKGLEntriesNEW ON dbo.DailyGroundTransactions.DivisionID = dbo.CHKGLEntriesNEW.DivisionId LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.CHKGLEntriesNEW.FieldType LIKE '" + Field + "') AND (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') GROUP BY  dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName,dbo.CHKGLEntriesNEW.FieldType,dbo.DailyGroundTransactions.CropType order by dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            }
            else if (intCropType == "2")
            {
                ds = SQLHelper.FillDataSet("SELECT dbo.DailyGroundTransactionsRubber.WorkCodeID, SUM(dbo.DailyGroundTransactionsRubber.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactionsRubber.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeID = 'TAP') THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactionsRubber.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactionsRubber.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactionsRubber.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) + SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount)  + SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) + SUM(dbo.DailyGroundTransactionsRubber.EPF12) + SUM(dbo.DailyGroundTransactionsRubber.ETF3)  + SUM(dbo.DailyGroundTransactionsRubber.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) + (SELECT ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) + SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount)  + SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) + SUM(dbo.DailyGroundTransactionsRubber.EPF12) + SUM(dbo.DailyGroundTransactionsRubber.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.CHKGLEntriesNEW.FieldType, dbo.DailyGroundTransactionsRubber.CropType FROM dbo.DailyGroundTransactionsRubber INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactionsRubber.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.CHKGLEntriesNEW ON dbo.DailyGroundTransactionsRubber.DivisionID = dbo.CHKGLEntriesNEW.DivisionId LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactionsRubber.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactionsRubber.DivisionID LIKE '" + strDiv + "') AND (dbo.CHKGLEntriesNEW.FieldType LIKE '" + Field + "') AND (dbo.DailyGroundTransactionsRubber.CropType LIKE '" + intCropType + "') GROUP BY  dbo.DailyGroundTransactionsRubber.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName,dbo.CHKGLEntriesNEW.FieldType,dbo.DailyGroundTransactionsRubber.CropType order by dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            }
            else if (intCropType == "%")
            {
                ds = SQLHelper.FillDataSet("(SELECT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.CHKGLEntriesNEW.FieldType, dbo.DailyGroundTransactions.CropType FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.CHKGLEntriesNEW ON dbo.DailyGroundTransactions.DivisionID = dbo.CHKGLEntriesNEW.DivisionId LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.CHKGLEntriesNEW.FieldType LIKE '" + Field + "') AND (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') GROUP BY  dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName,dbo.CHKGLEntriesNEW.FieldType,dbo.DailyGroundTransactions.CropType UNION ALL (SELECT dbo.DailyGroundTransactionsRubber.WorkCodeID, SUM(dbo.DailyGroundTransactionsRubber.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactionsRubber.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeID = 'TAP') THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'TAP') THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'TAP')  THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactionsRubber.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactionsRubber.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactionsRubber.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'TAP')  THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'TAP') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) + SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount)  + SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) + SUM(dbo.DailyGroundTransactionsRubber.EPF12) + SUM(dbo.DailyGroundTransactionsRubber.ETF3)  + SUM(dbo.DailyGroundTransactionsRubber.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'TAP')  THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'TAP') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) + (SELECT ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) + SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount)  + SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) + SUM(dbo.DailyGroundTransactionsRubber.EPF12) + SUM(dbo.DailyGroundTransactionsRubber.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.CHKGLEntriesNEW.FieldType, dbo.DailyGroundTransactionsRubber.CropType FROM dbo.DailyGroundTransactionsRubber INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactionsRubber.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.CHKGLEntriesNEW ON dbo.DailyGroundTransactionsRubber.DivisionID = dbo.CHKGLEntriesNEW.DivisionId LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactionsRubber.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactionsRubber.DivisionID LIKE '" + strDiv + "') AND (dbo.CHKGLEntriesNEW.FieldType LIKE '" + Field + "') AND (dbo.DailyGroundTransactionsRubber.CropType LIKE '" + intCropType + "') GROUP BY  dbo.DailyGroundTransactionsRubber.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName,dbo.CHKGLEntriesNEW.FieldType,dbo.DailyGroundTransactionsRubber.CropType))", CommandType.Text);
            }

            return ds;
        }

        public DataSet GetCheckrollWagesForMonthBFProcess(Int32 intYear, Int32 intMonth, String strDiv, String intCropType)
        {
            DateTime FromDate = new DateTime(intYear, intMonth, 1);
            DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);

            DataSet ds = new DataSet("CheckrollWagesBFProcess");

            if (intCropType == "1")
            {

                //NIsalads = SQLHelper.FillDataSet("SELECT      dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY  dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName order by dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive, ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.ScrapKgAmount) AS Scrap Amount FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            }
            else if (intCropType == "2")
            {
                ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactionsRubber.WorkCodeID, SUM(dbo.DailyGroundTransactionsRubber.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactionsRubber.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeID = 'TAP') THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactionsRubber.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactionsRubber.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactionsRubber.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) + SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount) + SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) + SUM(dbo.DailyGroundTransactionsRubber.EPF12) + SUM(dbo.DailyGroundTransactionsRubber.ETF3) + SUM(dbo.DailyGroundTransactionsRubber.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) + SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount) + SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) + SUM(dbo.DailyGroundTransactionsRubber.EPF12) + SUM(dbo.DailyGroundTransactionsRubber.ETF3) AS totalWithoutIncentive, ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.DailyGroundTransactionsRubber.CropType FROM dbo.DailyGroundTransactionsRubber INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactionsRubber.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactionsRubber.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (dbo.DailyGroundTransactionsRubber.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactionsRubber.CropType LIKE '" + intCropType + "') GROUP BY dbo.DailyGroundTransactionsRubber.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName, dbo.DailyGroundTransactionsRubber.CropType ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            }
            else if (intCropType == "%")
            {
                //ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) + SUM(dbo.DailyGroundTransactions.IncentiveAmount)+SUM(dbo.DailyGroundTransactions.PSSAmount)  AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive, ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS1 FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType  ORDER BY SequenceNO", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT  TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk')  THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_2 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) + SUM(dbo.DailyGroundTransactions.PSSAmount) AS TotalWithIncentive,  SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_1 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS1, dbo.JobGroup.GroupName,dbo.DailyGroundTransactions.FieldID FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE        (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND  (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.FieldID ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT        TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk')  THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_2 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) + SUM(dbo.DailyGroundTransactions.PSSAmount) AS TotalWithIncentive,  SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_1 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS1, dbo.JobGroup.GroupName,  dbo.DailyGroundTransactions.FieldID, dbo.EstateField.FieldName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID AND  dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE        (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND  (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.FieldID,  dbo.EstateField.FieldName ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            }

            return ds;
        }

        public DataSet GetCheckrollWagesForMonthBFProcessJobWise(Int32 intYear, Int32 intMonth, String strDiv, String intCropType)
        {
            DateTime FromDate = new DateTime(intYear, intMonth, 1);
            DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);

            DataSet ds = new DataSet("CheckrollWagesBFProcess");

            if (intCropType == "1")
            {

                //NIsalads = SQLHelper.FillDataSet("SELECT      dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY  dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName order by dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive, ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.ScrapKgAmount) AS Scrap Amount FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            }
            else if (intCropType == "2")
            {
                ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactionsRubber.WorkCodeID, SUM(dbo.DailyGroundTransactionsRubber.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactionsRubber.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeID = 'TAP') THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactionsRubber.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactionsRubber.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactionsRubber.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) + SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount) + SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) + SUM(dbo.DailyGroundTransactionsRubber.EPF12) + SUM(dbo.DailyGroundTransactionsRubber.ETF3) + SUM(dbo.DailyGroundTransactionsRubber.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactionsRubber.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactionsRubber.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactionsRubber.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactionsRubber.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactionsRubber.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactionsRubber.WorkCodeID)) + SUM(dbo.DailyGroundTransactionsRubber.OverKgAmount) + SUM(dbo.DailyGroundTransactionsRubber.PRIAmount) + SUM(dbo.DailyGroundTransactionsRubber.EPF12) + SUM(dbo.DailyGroundTransactionsRubber.ETF3) AS totalWithoutIncentive, ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.DailyGroundTransactionsRubber.CropType FROM dbo.DailyGroundTransactionsRubber INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactionsRubber.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactionsRubber.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (dbo.DailyGroundTransactionsRubber.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactionsRubber.CropType LIKE '" + intCropType + "') GROUP BY dbo.DailyGroundTransactionsRubber.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName, dbo.DailyGroundTransactionsRubber.CropType ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            }
            else if (intCropType == "%")
            {
                //ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) + SUM(dbo.DailyGroundTransactions.IncentiveAmount)+SUM(dbo.DailyGroundTransactions.PSSAmount)  AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive, ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS1 FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType  ORDER BY SequenceNO", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT  TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk')  THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_2 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) + SUM(dbo.DailyGroundTransactions.PSSAmount) AS TotalWithIncentive,  SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_1 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS1, dbo.JobGroup.GroupName,dbo.DailyGroundTransactions.FieldID FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE        (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND  (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.FieldID ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT        TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk')  THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_2 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) + SUM(dbo.DailyGroundTransactions.PSSAmount) AS TotalWithIncentive,  SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_1 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS1, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID AND  dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE        (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND  (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, dbo.JobGroup.GroupName ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT        TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'plk')  THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_2 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) + SUM(dbo.DailyGroundTransactions.PSSAmount) AS TotalWithIncentive,  SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT        ISNULL(SUM(Expenditure), 0) AS Expr1 FROM            dbo.CHKOvertime AS CHKOvertime_1 WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS1, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE        (dbo.DailyGroundTransactions.CropType LIKE '" + intCropType + "') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND  (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.CropType, dbo.JobGroup.GroupName ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            }

            return ds;
        }

        public DataSet GetCheckrollWagesForMonthBFProcessLentLabour(Int32 intYear, Int32 intMonth, String strDiv)
        {
            DateTime FromDate = new DateTime(intYear, intMonth, 1);
            DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);
            //DataSet ds=new DataSet("CheckrollWages");
            //ds=SQLHelper.FillDataSet("SELECT dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalMandays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, dbo.CHKGLEntries.WagesPay, dbo.CHKGLEntries.CashWork, dbo.CHKGLEntries.ExtraRate,  dbo.CHKGLEntries.Overtime, dbo.CHKGLEntries.OverKGAmount, dbo.CHKGLEntries.PRIAmount, dbo.CHKGLEntries.EPF12, dbo.CHKGLEntries.ETF3,  dbo.CHKGLEntries.TotalWOIncentive, dbo.CHKGLEntries.IncentiveAmount, dbo.CHKGLEntries.TotalWithIncentive FROM  dbo.DailyGroundTransactions INNER JOIN dbo.CHKGLEntries ON YEAR(dbo.DailyGroundTransactions.DateEntered) = dbo.CHKGLEntries.ProcessYear AND MONTH(dbo.DailyGroundTransactions.DateEntered)  = dbo.CHKGLEntries.ProcessMonth AND dbo.DailyGroundTransactions.DivisionID = dbo.CHKGLEntries.DivisionId AND  dbo.DailyGroundTransactions.WorkCodeID = dbo.CHKGLEntries.WorkCodeId WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '"+intYear+"') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '"+intMonth+"') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKGLEntries.WagesPay, dbo.CHKGLEntries.CashWork,  dbo.CHKGLEntries.ExtraRate, dbo.CHKGLEntries.Overtime, dbo.CHKGLEntries.OverKGAmount, dbo.CHKGLEntries.PRIAmount, dbo.CHKGLEntries.EPF12,  dbo.CHKGLEntries.ETF3, dbo.CHKGLEntries.TotalWOIncentive, dbo.CHKGLEntries.IncentiveAmount, dbo.CHKGLEntries.TotalWithIncentive HAVING (dbo.DailyGroundTransactions.DivisionID = '"+strDiv+"')",CommandType.Text);
            //return ds;
            DataSet ds = new DataSet("CheckrollWagesBFProcessLent");
            //ds = SQLHelper.FillDataSet("SELECT     DivisionID, WorkCodeID, SUM(ManDays) AS NormalManDays, SUM(CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk')  THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  AS CashSundry, SUM(ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime WHERE      (OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionCode like '"+strDiv+"')  AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(OverKgAmount) AS OverKgAmount, SUM(PRIAmount) AS PSS, SUM(EPF12)  AS EPF12, SUM(ETF3) AS ETF3, SUM(IncentiveAmount) AS Incentive, SUM(DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionCode like '"+strDiv+"')  AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(OverKgAmount) + SUM(PRIAmount) + SUM(EPF12) + SUM(ETF3) + SUM(IncentiveAmount)  AS TotalWithIncentive, SUM(DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionCode like '"+strDiv+"')  AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(OverKgAmount) + SUM(PRIAmount) + SUM(EPF12) + SUM(ETF3)  AS totalWithoutIncentive FROM         dbo.DailyGroundTransactions WHERE     (DateEntered BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionID like '"+strDiv+"') GROUP BY DivisionID, WorkCodeID ", CommandType.Text);
            //ds = SQLHelper.FillDataSet("SELECT     dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount)  AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM          dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description,  dbo.CHKJobAnalysisCodes.SequenceNO, dbo.JobMaster.JobName ", CommandType.Text);

            ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk')  THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_2 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3)  + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic)  + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2)  THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END)  + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime AS CHKOvertime_1 WHERE      (OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (DivisionCode LIKE '" + strDiv + "') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount)  + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive,  ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.DivisionID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.LabourType = 'Lent Labour') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.DivisionID ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            //            if (intCropType == "1")
            //            {
            //                ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactions.WorkCodeID, SUM(dbo.DailyGroundTransactions.ManDays) AS NormalManDays, SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (WorkCodeID = 'plk') THEN CASE WHEN (CashPlkOkgYesNo = 1) THEN CASE WHEN (CashManDays > 0) THEN CashKgAmount ELSE 0 END ELSE 0 END ELSE 0 END) AS CashNamePlk, SUM(dbo.DailyGroundTransactions.DailyBasic) AS Wages, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) AS CashPlucking, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) AS CashSundry, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates, (SELECT ISNULL(SUM(Expenditure), 0) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '2015-06-01', 102) AND CONVERT(DATETIME, '2015-06-30', 102)) AND (DivisionCode LIKE 'TM') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) AS Overtime, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PSS, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS Incentive, SUM(dbo.DailyGroundTransactions.DailyBasic) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) + SUM(dbo.DailyGroundTransactions.ExtraRates) + (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1
            //                            FROM          dbo.CHKOvertime AS CHKOvertime_2
            //                            WHERE      (OtDate BETWEEN CONVERT(DATETIME, '2015-06-01', 102) AND CONVERT(DATETIME, '2015-06-30', 102)) AND 
            //                                                   (DivisionCode LIKE 'TM') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) 
            //                      + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) 
            //                      + SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS TotalWithIncentive, SUM(dbo.DailyGroundTransactions.DailyBasic) 
            //                      + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId = 'plk') 
            //                      THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE 0 END) + SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) 
            //                      THEN CASE WHEN (dbo.DailyGroundTransactions.WorkCodeId <> 'plk') THEN dbo.DailyGroundTransactions.CashSundryAmount ELSE 0 END ELSE 0 END) 
            //                      + SUM(dbo.DailyGroundTransactions.ExtraRates) +
            //                          (SELECT     ISNULL(SUM(Expenditure), 0) AS Expr1
            //                            FROM          dbo.CHKOvertime AS CHKOvertime_1
            //                            WHERE      (OtDate BETWEEN CONVERT(DATETIME, '2015-06-01', 102) AND CONVERT(DATETIME, '2015-06-30', 102)) AND 
            //                                                   (DivisionCode LIKE 'TM') AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) + SUM(dbo.DailyGroundTransactions.OverKgAmount) 
            //                      + SUM(dbo.DailyGroundTransactions.PRIAmount) + SUM(dbo.DailyGroundTransactions.EPF12) + SUM(dbo.DailyGroundTransactions.ETF3) AS totalWithoutIncentive, 
            //                      ISNULL(dbo.CHKJobAnalysisCodes.Description, 'Default') AS SeqDesc, ISNULL(dbo.CHKJobAnalysisCodes.SequenceNO, 0) AS SequenceNo, dbo.JobMaster.JobName,
            //                       dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.CropType
            //FROM         dbo.DailyGroundTransactions INNER JOIN
            //                      dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN
            //                      dbo.CHKJobAnalysisCodes ON dbo.JobMaster.AnalyzeCode = dbo.CHKJobAnalysisCodes.AnalyzeShortCode
            //WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '2015-06-01', 102) AND CONVERT(DATETIME, '2015-06-30', 102)) AND 
            //                      (dbo.DailyGroundTransactions.DivisionID LIKE 'TM') AND (dbo.DailyGroundTransactions.CropType like '1') AND (dbo.DailyGroundTransactions.LabourType = 'Lent Labour')
            //GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.CHKJobAnalysisCodes.Description, dbo.CHKJobAnalysisCodes.SequenceNO, 
            //                      dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.CropType
            //ORDER BY dbo.CHKJobAnalysisCodes.SequenceNO", CommandType.Text);
            //            }


            return ds;
        }

        public DataTable GetEstateFieldsbyDivision(String strDiv)
        {
            DataTable dtFields = new DataTable();
            dtFields.Columns.Add("FieldID");
            dtFields.Columns.Add("FieldName");

            DataRow dtrow;
            SqlDataReader datareader;

            //datareader = SQLHelper.ExecuteReader("SELECT [FieldID] ,[FieldName] FROM [dbo].[EstateField] WHERE DivisionID = '"+ strDiv +"' GROUP BY [FieldID] ,[FieldName]", CommandType.Text);
            datareader = SQLHelper.ExecuteReader("SELECT [FieldID] FROM dbo.EstateField WHERE DivisionID = '" + strDiv + "' GROUP BY FieldID ORDER BY FieldID", CommandType.Text);

            while (datareader.Read())
            {
                dtrow = dtFields.NewRow();

                if (!datareader.IsDBNull(0))
                {
                    dtrow[0] = datareader.GetString(0);
                }
                dtFields.Rows.Add(dtrow);
            }
            datareader.Close();
            return dtFields;
        }

        public DataSet GetCashWorkBreakDown(String strDiv, String strCrop, DateTime dFrom, DateTime dTo)
        {
            DataSet dsCW = new DataSet();
            //dsWages = SQLHelper.FillDataSet("SELECT        CASE WHEN (dbo.DailyGroundTransactions.CropType = 1) THEN 'Tea' ELSE 'Rubber' END AS Crop, dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.WorkCodeID, CASE WHEN (dbo.DailyGroundTransactions.CropType = 1) THEN CASE WHEN (WorkCodeID = 'Plucking')  THEN 'Plucking' ELSE 'TEA Sundry' END ELSE CASE WHEN (WorkCodeID = 'TAP') THEN 'Tapping' ELSE 'Rubber Sundry' END END AS Type1,  SUM(CASE WHEN (ManDays = 1.5) THEN 1 ELSE ManDays END) AS ManDays, SUM(CASE WHEN (ManDays = 1.5) THEN 0.5 ELSE 0 END) AS HalfNames,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount,  SUM(dbo.DailyGroundTransactions.CashKgs) AS CashKgs, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundryAmount,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.OverKgs) AS OverKgs,  SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRIAmount, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSSAmount,  SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS IncentiveAmount, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, dbo.JobMaster.JobName,(SELECT        sum(Expenditure) FROM  dbo.CHKOvertime WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) as OT FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE        (dbo.DailyGroundTransactions.CropType LIKE '" + strCrop + "') AND (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.CropType, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName ", CommandType.Text);
            dsCW = SQLHelper.FillDataSet("SELECT  CASE WHEN (CropType = 1) THEN 'TEA' ELSE 'RUBBER' END AS Crop, DivisionID, CASE WHEN (WorkType = 1) THEN 'Normal' ELSE 'Cash Work' END AS Type1,  CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID = 'PLK') THEN CASE WHEN (CashPlkOkgYesNo = 1)  THEN 'Cash Name Plk' ELSE CASE WHEN (CashBlockYesNo = 1)  THEN 'BlockPlk' ELSE 'Cash Plucking' END END ELSE 'TEA Sundry' END ELSE CASE WHEN (WorkCodeID = 'TAP')  THEN 'Tapping' ELSE 'Rubber Sundry' END END AS Type2, CASE WHEN (CashWorkType = 1) THEN 'Double Tapping' ELSE CASE WHEN (CashWorkType = 2)  THEN 'Cash Tapping' ELSE CASE WHEN (CashWorkType = 3) THEN 'Cash Work' ELSE CASE WHEN (CashWorkType = 4)  THEN 'Contract Tapping' ELSE CASE WHEN (CashWorkType = 5) THEN 'Contract Name' ELSE 'Other' END END END END END AS [Cash Work Type],  SUM(CashKgAmount) AS [Cash Kilo Amount], SUM(CashSundryAmount) AS [Cash Sundry Amount], SUM(CashManDays) AS [Cash Man Days], SUM(CashKgs)  AS [Cash Kilos] FROM            dbo.DailyGroundTransactions WHERE        (DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (WorkType = 2) GROUP BY CropType, DivisionID, WorkType, WorkCodeID, CashWorkType, CashPlkOkgYesNo, CashBlockYesNo HAVING        (DivisionID LIKE '" + strDiv + "') AND (CropType like '" + strCrop + "')", CommandType.Text);
            return dsCW;
        }

        public DataSet GetCropWiseWages(String strDiv, String strCrop, DateTime dFrom, DateTime dTo)
        {
            DataSet dsWages = new DataSet();
            //dsWages = SQLHelper.FillDataSet("SELECT        CASE WHEN (dbo.DailyGroundTransactions.CropType = 1) THEN 'Tea' ELSE 'Rubber' END AS Crop, dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.WorkCodeID, CASE WHEN (dbo.DailyGroundTransactions.CropType = 1) THEN CASE WHEN (WorkCodeID = 'Plucking')  THEN 'Plucking' ELSE 'TEA Sundry' END ELSE CASE WHEN (WorkCodeID = 'TAP') THEN 'Tapping' ELSE 'Rubber Sundry' END END AS Type1,  SUM(CASE WHEN (ManDays = 1.5) THEN 1 ELSE ManDays END) AS ManDays, SUM(CASE WHEN (ManDays = 1.5) THEN 0.5 ELSE 0 END) AS HalfNames,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount,  SUM(dbo.DailyGroundTransactions.CashKgs) AS CashKgs, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundryAmount,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.OverKgs) AS OverKgs,  SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRIAmount, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSSAmount,  SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS IncentiveAmount, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, dbo.JobMaster.JobName,(SELECT        sum(Expenditure) FROM  dbo.CHKOvertime WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (Job = dbo.DailyGroundTransactions.WorkCodeID)) as OT, dbo.DailyGroundTransactions.SubCategoryCode, FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE        (dbo.DailyGroundTransactions.CropType LIKE '" + strCrop + "') AND (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.CropType, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName ", CommandType.Text);
            dsWages = SQLHelper.FillDataSet("SELECT CASE WHEN (dbo.DailyGroundTransactions.CropType = 1) THEN 'Tea' ELSE 'Rubber' END AS Crop, dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.WorkCodeID, CASE WHEN (dbo.DailyGroundTransactions.CropType = 1) THEN CASE WHEN (WorkCodeID = 'Plucking')  THEN 'Plucking' ELSE 'TEA Sundry' END ELSE CASE WHEN (WorkCodeID = 'TAP') THEN 'Tapping' ELSE 'Rubber Sundry' END END AS Type1,  SUM(CASE WHEN (ManDays = 1.5) THEN 1 ELSE ManDays END) AS ManDays, SUM(CASE WHEN (ManDays = 1.5) THEN 0.5 ELSE 0 END) AS HalfNames,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount,  SUM(dbo.DailyGroundTransactions.CashKgs) AS CashKgs, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundryAmount,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKgAmount, SUM(dbo.DailyGroundTransactions.OverKgs) AS OverKgs,  SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRIAmount, SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSSAmount,  SUM(dbo.DailyGroundTransactions.IncentiveAmount) AS IncentiveAmount, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12,  SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, dbo.JobMaster.JobName,isnull((SELECT        sum(Expenditure) FROM  dbo.CHKOvertime WHERE        (OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (Job = dbo.DailyGroundTransactions.WorkCodeID)),0) as OT FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE        (dbo.DailyGroundTransactions.CropType LIKE '" + strCrop + "') AND (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.CropType, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName  union SELECT        'Tea' AS Crop, dbo.CHKOvertime.DivisionCode AS DivisionID, dbo.CHKOvertime.Job, 'OT' AS Type1, 0 AS ManDays, 0 AS HalfNames, 0 AS DailyBasic,  0 AS CashKgAmount, 0 AS CashKgs, 0 AS CashSundryAmount, 0 AS CashManDays, 0 AS ExtraRates, 0 AS OverKgAmount, 0 AS OverKgs, 0 AS PRIAmount,  0 AS PSSAmount, 0 AS IncentiveAmount, 0 AS EPF12, 0 AS ETF3, dbo.JobMaster.JobName, SUM(dbo.CHKOvertime.Expenditure) AS Expr1 FROM dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName WHERE    (dbo.CHKOvertime.DivisionCode like '" + strDiv + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) GROUP BY dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.Job, dbo.JobMaster.JobName HAVING        (NOT (dbo.CHKOvertime.Job IN (SELECT        WorkCodeID FROM            dbo.DailyGroundTransactions WHERE   (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "') and       (DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)))))", CommandType.Text);
            return dsWages;
        }


        //public object GetEstateFieldsbyDivision(string p)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public string processGLEntriesNEW(string p, string p_2)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public DataTable GetCropWiseFieldWiseWages(String strDiv, String strCrop, DateTime dFrom, DateTime dTo)
        {
            SqlDataReader readerJobs;
            SqlDataReader readerDailyEntries;
            SqlDataReader readerOvertime;
            DataTable dtWages = new DataTable();
            dtWages.Columns.Add("CropType");//0
            dtWages.Columns.Add("Division");//1
            dtWages.Columns.Add("Field");
            dtWages.Columns.Add("Job");
            dtWages.Columns.Add("JobName");
            dtWages.Columns.Add("ManDays");//5
            dtWages.Columns.Add("HalfNames");
            dtWages.Columns.Add("DailyBasic");
            dtWages.Columns.Add("OverKgAmount");
            dtWages.Columns.Add("ExtraRates");
            dtWages.Columns.Add("ScrapAmount");//10
            dtWages.Columns.Add("PSS");
            dtWages.Columns.Add("PRI");
            dtWages.Columns.Add("Att.Incentive");
            dtWages.Columns.Add("CashPlucking");
            dtWages.Columns.Add("CashSundry");//15
            dtWages.Columns.Add("EPF12");//16
            dtWages.Columns.Add("ETF3");//17
            dtWages.Columns.Add("LabourType");//18

            dtWages.Columns.Add("CashManDays");//19
            dtWages.Columns.Add("CashNonDaysAmount");//20
            dtWages.Columns.Add("CashDaysAmount");//21
            dtWages.Columns.Add("Overtime");//22

            DataRow dtRow;
            dtRow = dtWages.NewRow();

            readerJobs = SQLHelper.ExecuteReader("SELECT Job FROM dbo.CHKOvertime WHERE  (OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (DivisionCode like '" + strDiv + "') GROUP BY Job union SELECT        WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (DivisionID like '" + strDiv + "') GROUP BY WorkCodeID order by Job", CommandType.Text);
            while (readerJobs.Read())
            {
                if (!readerJobs.IsDBNull(0))
                {
                    readerDailyEntries = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.CropType, CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.DivisionID ELSE dbo.DailyGroundTransactions.LabourDivision END AS DivisionID,  CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID,   dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays,  SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf, SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic,  SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount, SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRINorm) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, 'ALL' as LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'TAP')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'TAP')  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND  (dbo.DailyGroundTransactions.LabourType = 'General') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') GROUP BY dbo.DailyGroundTransactions.CropType, CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.DivisionID ELSE dbo.DailyGroundTransactions.LabourDivision END,  CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName", CommandType.Text);
                    while (readerDailyEntries.Read())
                    {
                        dtRow = dtWages.NewRow();
                        //Crop type
                        if (!readerDailyEntries.IsDBNull(0))
                        {
                            dtRow[0] = readerDailyEntries.GetInt32(0);
                        }
                        //Division
                        if (!readerDailyEntries.IsDBNull(1))
                        {
                            dtRow[1] = readerDailyEntries.GetString(1).Trim();
                        }
                        //Field
                        if (!readerDailyEntries.IsDBNull(2))
                        {
                            dtRow[2] = readerDailyEntries.GetString(2).Trim();
                        }
                        //Work Code
                        if (!readerDailyEntries.IsDBNull(3))
                        {
                            dtRow[3] = readerDailyEntries.GetString(3).Trim();
                        }
                        //Job Name
                        if (!readerDailyEntries.IsDBNull(4))
                        {
                            dtRow[4] = readerDailyEntries.GetString(4).Trim();
                        }
                        //ManDays
                        if (!readerDailyEntries.IsDBNull(5))
                        {
                            dtRow[5] = readerDailyEntries.GetDecimal(5);
                        }
                        //HolidayHalf
                        if (!readerDailyEntries.IsDBNull(6))
                        {
                            dtRow[6] = readerDailyEntries.GetDecimal(6);
                        }
                        //DailyBasic
                        if (!readerDailyEntries.IsDBNull(7))
                        {
                            dtRow[7] = readerDailyEntries.GetDecimal(7);
                        }
                        //Over kg Amount
                        if (!readerDailyEntries.IsDBNull(8))
                        {
                            dtRow[8] = readerDailyEntries.GetDecimal(8);
                        }
                        //Extra Rate
                        if (!readerDailyEntries.IsDBNull(9))
                        {
                            dtRow[9] = readerDailyEntries.GetDecimal(9);
                        }
                        //Scrap Amount
                        if (!readerDailyEntries.IsDBNull(10))
                        {
                            dtRow[10] = readerDailyEntries.GetDecimal(10);
                        }
                        //Pss
                        if (!readerDailyEntries.IsDBNull(11))
                        {
                            dtRow[11] = readerDailyEntries.GetDecimal(11);
                        }
                        //PRI
                        if (!readerDailyEntries.IsDBNull(12))
                        {
                            dtRow[12] = readerDailyEntries.GetInt32(12);
                        }

                        //Att.Incentive
                        if (!readerDailyEntries.IsDBNull(13))
                        {
                            dtRow[13] = readerDailyEntries.GetDecimal(13);
                        }
                        //Cash Kg Amount
                        if (!readerDailyEntries.IsDBNull(14))
                        {
                            dtRow[14] = readerDailyEntries.GetDecimal(14);
                        }
                        //Cash Sundry Amount
                        if (!readerDailyEntries.IsDBNull(15))
                        {
                            dtRow[15] = readerDailyEntries.GetDecimal(15);
                        }
                        //EPF12
                        if (!readerDailyEntries.IsDBNull(16))
                        {
                            dtRow[16] = readerDailyEntries.GetDecimal(16);
                        }
                        //ETF3
                        if (!readerDailyEntries.IsDBNull(17))
                        {
                            dtRow[17] = readerDailyEntries.GetDecimal(17);
                        }

                        //Labour Type
                        if (!readerDailyEntries.IsDBNull(18))
                        {
                            dtRow[18] = readerDailyEntries.GetString(18).Trim();
                        }

                        //Cash man Days
                        if (!readerDailyEntries.IsDBNull(19))
                        {
                            dtRow[19] = readerDailyEntries.GetDecimal(19);
                        }
                        //Cash Non Days Amount
                        if (!readerDailyEntries.IsDBNull(20))
                        {
                            dtRow[20] = readerDailyEntries.GetDecimal(20);
                        }

                        //Cash Days Amount
                        if (!readerDailyEntries.IsDBNull(21))
                        {
                            dtRow[21] = readerDailyEntries.GetDecimal(21);
                        }
                        dtRow[22] = 0;
                        readerOvertime = SQLHelper.ExecuteReader("SELECT SUM(Expenditure) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (CropCode = '" + readerDailyEntries.GetInt32(0) + "') AND (DivisionCode like '" + strDiv + "') AND (Field = '" + readerDailyEntries.GetString(2).Trim() + "') AND (Job = '" + readerJobs.GetString(0).Trim() + "')", CommandType.Text);
                        while (readerOvertime.Read())
                        {
                            if (!readerOvertime.IsDBNull(0))
                            {
                                dtRow[22] = readerOvertime.GetDecimal(0);
                            }
                            else
                            {
                                dtRow[22] = 0;
                            }
                        }
                        readerOvertime.Close();
                        dtWages.Rows.Add(dtRow);
                    }
                    readerDailyEntries.Close();


                }


            }
            readerJobs.Close();
            return dtWages;

        }

        public DataTable GetWages(String strDiv, String strCrop, DateTime dFrom, DateTime dTo)
        {
            SqlDataReader readerJobs;
            SqlDataReader readerDailyEntries;
            SqlDataReader readerOvertime;
            DataTable dtWages = new DataTable();
            dtWages.Columns.Add("CropType");//0
            dtWages.Columns.Add("Division");//1
            dtWages.Columns.Add("Field");
            dtWages.Columns.Add("Job");
            dtWages.Columns.Add("JobName");
            dtWages.Columns.Add("ManDays");//5
            dtWages.Columns.Add("HalfNames");
            dtWages.Columns.Add("DailyBasic");
            dtWages.Columns.Add("OverKgAmount");
            dtWages.Columns.Add("ExtraRates");
            dtWages.Columns.Add("ScrapAmount");//10
            dtWages.Columns.Add("PSS");
            dtWages.Columns.Add("PRI");
            dtWages.Columns.Add("Att.Incentive");
            dtWages.Columns.Add("CashPlucking");
            dtWages.Columns.Add("CashSundry");//15
            dtWages.Columns.Add("EPF12");//16
            dtWages.Columns.Add("ETF3");//17
            dtWages.Columns.Add("LabourType");//18

            dtWages.Columns.Add("CashManDays");//19
            dtWages.Columns.Add("CashNonDaysAmount");//20
            dtWages.Columns.Add("CashDaysAmount");//21
            dtWages.Columns.Add("Overtime");//22
            dtWages.Columns.Add("GroupCode");//23
            dtWages.Columns.Add("GroupName");//24

            DataRow dtRow;
            dtRow = dtWages.NewRow();

            //readerJobs = SQLHelper.ExecuteReader("SELECT        WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (DivisionID like '" + strDiv + "') GROUP BY WorkCodeID ", CommandType.Text);
            readerJobs = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName", CommandType.Text);
            while (readerJobs.Read())
            {
                if (!readerJobs.IsDBNull(0))
                {

                    #region ValueInitialize
                    ////dgt entries initialize
                    ////Crop type

                    //    dtRow[0] = readerDailyEntries.GetInt32(0);
                    ////Division

                    //    dtRow[1] = readerDailyEntries.GetString(1).Trim();
                    ////Field

                    //    dtRow[2] = readerDailyEntries.GetString(2).Trim();
                    ////Work Code

                    //    dtRow[3] = readerDailyEntries.GetString(3).Trim();
                    ////Job Name
                    //    dtRow[4] = readerDailyEntries.GetString(4).Trim();
                    ////ManDays

                    //    dtRow[5] = readerDailyEntries.GetDecimal(5);
                    ////HolidayHalf

                    //    dtRow[6] = readerDailyEntries.GetDecimal(6);
                    ////DailyBasic

                    //    dtRow[7] = readerDailyEntries.GetDecimal(7);
                    ////Over kg Amount

                    //    dtRow[8] = readerDailyEntries.GetDecimal(8);
                    ////Extra Rate

                    //    dtRow[9] = readerDailyEntries.GetDecimal(9);
                    ////Scrap Amount

                    //    dtRow[10] = readerDailyEntries.GetDecimal(10);
                    ////Pss

                    //    dtRow[11] = readerDailyEntries.GetDecimal(11);
                    ////PRI

                    //    dtRow[12] = readerDailyEntries.GetInt32(12);

                    ////Att.Incentive

                    //    dtRow[13] = readerDailyEntries.GetDecimal(13);
                    ////Cash Kg Amount

                    //    dtRow[14] = readerDailyEntries.GetDecimal(14);
                    ////Cash Sundry Amount

                    //    dtRow[15] = readerDailyEntries.GetDecimal(15);
                    ////EPF12

                    //    dtRow[16] = 0;

                    ////ETF3

                    //    dtRow[17] = 0;

                    ////Labour Type

                    //    dtRow[18] = "NA";


                    ////Cash man Days

                    //    dtRow[19] = 0;

                    ////Cash Non Days Amount

                    //    dtRow[20] = 0;


                    ////Cash Days Amount

                    //    dtRow[21] = 0;
                    ////---------- 
                    #endregion
                    //readerDailyEntries = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.CropType, CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.DivisionID ELSE dbo.DailyGroundTransactions.LabourDivision END AS DivisionID,  CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, 'ALL' AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'TAP')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'TAP')  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '"+strCrop+"') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') GROUP BY dbo.DailyGroundTransactions.CropType, CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.DivisionID ELSE dbo.DailyGroundTransactions.LabourDivision END,  CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName", CommandType.Text);
                    readerDailyEntries = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.CropType, CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.DivisionID ELSE dbo.DailyGroundTransactions.LabourDivision END AS DivisionID,  CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, 'ALL' AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') GROUP BY dbo.DailyGroundTransactions.CropType, CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.DivisionID ELSE dbo.DailyGroundTransactions.LabourDivision END,  CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName", CommandType.Text);
                    while (readerDailyEntries.Read())
                    {
                        #region DailyEntriesRead
                        dtRow = dtWages.NewRow();

                        //Crop type
                        if (!readerDailyEntries.IsDBNull(0))
                        {
                            dtRow[0] = readerDailyEntries.GetInt32(0);
                        }
                        //Division
                        if (!readerDailyEntries.IsDBNull(1))
                        {
                            dtRow[1] = readerDailyEntries.GetString(1).Trim();
                        }
                        //Field
                        if (!readerDailyEntries.IsDBNull(2))
                        {
                            dtRow[2] = readerDailyEntries.GetString(2).Trim();
                        }
                        //Work Code
                        if (!readerDailyEntries.IsDBNull(3))
                        {
                            dtRow[3] = readerDailyEntries.GetString(3).Trim();
                        }
                        //Job Name
                        if (!readerDailyEntries.IsDBNull(4))
                        {
                            dtRow[4] = readerDailyEntries.GetString(4).Trim();
                        }
                        //ManDays
                        if (!readerDailyEntries.IsDBNull(5))
                        {
                            dtRow[5] = readerDailyEntries.GetDecimal(5);
                        }
                        //HolidayHalf
                        if (!readerDailyEntries.IsDBNull(6))
                        {
                            dtRow[6] = readerDailyEntries.GetDecimal(6);
                        }
                        //DailyBasic
                        if (!readerDailyEntries.IsDBNull(7))
                        {
                            dtRow[7] = readerDailyEntries.GetDecimal(7);
                        }
                        //Over kg Amount
                        if (!readerDailyEntries.IsDBNull(8))
                        {
                            dtRow[8] = readerDailyEntries.GetDecimal(8);
                        }
                        //Extra Rate
                        if (!readerDailyEntries.IsDBNull(9))
                        {
                            dtRow[9] = readerDailyEntries.GetDecimal(9);
                        }
                        //Scrap Amount
                        if (!readerDailyEntries.IsDBNull(10))
                        {
                            dtRow[10] = readerDailyEntries.GetDecimal(10);
                        }
                        //Pss
                        if (!readerDailyEntries.IsDBNull(11))
                        {
                            dtRow[11] = readerDailyEntries.GetDecimal(11);
                        }
                        //PRI
                        if (!readerDailyEntries.IsDBNull(12))
                        {
                            dtRow[12] = readerDailyEntries.GetDecimal(12);
                        }

                        //Att.Incentive
                        if (!readerDailyEntries.IsDBNull(13))
                        {
                            dtRow[13] = readerDailyEntries.GetDecimal(13);
                        }
                        //Cash Kg Amount
                        if (!readerDailyEntries.IsDBNull(14))
                        {
                            dtRow[14] = readerDailyEntries.GetDecimal(14);
                        }
                        //Cash Sundry Amount
                        if (!readerDailyEntries.IsDBNull(15))
                        {
                            dtRow[15] = readerDailyEntries.GetDecimal(15);
                        }
                        //EPF12
                        if (!readerDailyEntries.IsDBNull(16))
                        {
                            dtRow[16] = readerDailyEntries.GetDecimal(16);
                        }
                        //ETF3
                        if (!readerDailyEntries.IsDBNull(17))
                        {
                            dtRow[17] = readerDailyEntries.GetDecimal(17);
                        }

                        //Labour Type
                        if (!readerDailyEntries.IsDBNull(18))
                        {
                            dtRow[18] = readerDailyEntries.GetString(18).Trim();
                        }

                        //Cash man Days
                        if (!readerDailyEntries.IsDBNull(19))
                        {
                            dtRow[19] = readerDailyEntries.GetDecimal(19);
                        }
                        //Cash Non Days Amount
                        if (!readerDailyEntries.IsDBNull(20))
                        {
                            dtRow[20] = readerDailyEntries.GetDecimal(20);
                        }

                        //Cash Days Amount
                        if (!readerDailyEntries.IsDBNull(21))
                        {
                            dtRow[21] = readerDailyEntries.GetDecimal(21);
                        }
                        dtRow[22] = 0;
                        //Job Group Code
                        if (!readerJobs.IsDBNull(1))
                        {
                            dtRow[23] = readerJobs.GetString(1).Trim();
                        }
                        else
                            dtRow[23] = "NA";
                        //Job Group name
                        if (!readerJobs.IsDBNull(2))
                        {
                            dtRow[24] = readerJobs.GetString(2).Trim();
                        }
                        else
                            dtRow[24] = "NA";
                        //readerOvertime = SQLHelper.ExecuteReader("SELECT SUM(Expenditure) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (CropCode = '" + readerDailyEntries.GetInt32(0) + "') AND (DivisionCode like '" + strDiv + "') AND (Field = '" + readerDailyEntries.GetString(2).Trim() + "') AND (Job = '" + readerJobs.GetString(0).Trim() + "')", CommandType.Text);
                        //while (readerOvertime.Read())
                        //{

                        //    if (!readerOvertime.IsDBNull(0))
                        //    {
                        //        dtRow[22] = readerOvertime.GetDecimal(0);
                        //    }
                        //    else
                        //    {
                        //        dtRow[22] = 0;
                        //    }
                        //}
                        //readerOvertime.Close();
                        dtWages.Rows.Add(dtRow);
                        #endregion
                    }
                    readerDailyEntries.Close();
                }
            }
            readerJobs.Close();

            //readerJobs = SQLHelper.ExecuteReader("SELECT Job FROM dbo.CHKOvertime WHERE  (OtDate BETWEEN CONVERT(DATETIME, '"+dFrom+"', 102) AND CONVERT(DATETIME, '"+dTo+"', 102))  AND (Job NOT IN (SELECT WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dFrom+"', 102) AND CONVERT(DATETIME, '"+dTo+"', 102))  GROUP BY WorkCodeID ) ) GROUP BY Job ", CommandType.Text);

            //readerOvertime = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropType, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.DivisionCode else  dbo.CHKOvertime.LabourDivision end as Division, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.Field else dbo.CHKOvertime.LabourField end as FieldId, dbo.CHKOvertime.Job,  dbo.JobMaster.JobName, 'ALL' as LabourType FROM dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName WHERE        (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102))  AND (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "')   GROUP BY dbo.CHKOvertime.CropType, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.DivisionCode else  dbo.CHKOvertime.LabourDivision end, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.Field else dbo.CHKOvertime.LabourField end, dbo.CHKOvertime.Job, dbo.JobMaster.JobName", CommandType.Text);
            readerOvertime = SQLHelper.ExecuteReader("SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode, CASE WHEN (dbo.CHKOvertime.LabourType = 'General')  THEN dbo.CHKOvertime.DivisionCode ELSE dbo.CHKOvertime.LabourDivision END AS Division, CASE WHEN (dbo.CHKOvertime.LabourType = 'General')  THEN dbo.CHKOvertime.Field ELSE dbo.CHKOvertime.LabourField END AS FieldId, dbo.CHKOvertime.Job, dbo.JobMaster.JobName, 'ALL' AS LabourType,  dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "') GROUP BY dbo.CHKOvertime.CropCode, CASE WHEN (dbo.CHKOvertime.LabourType = 'General')  THEN dbo.CHKOvertime.DivisionCode ELSE dbo.CHKOvertime.LabourDivision END, CASE WHEN (dbo.CHKOvertime.LabourType = 'General')  THEN dbo.CHKOvertime.Field ELSE dbo.CHKOvertime.LabourField END, dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName", CommandType.Text);
            while (readerOvertime.Read())
            {
                #region OvertimeOnly
                dtRow = dtWages.NewRow();
                //Crop type
                if (!readerOvertime.IsDBNull(1))
                {
                    dtRow[0] = readerOvertime.GetInt32(1);
                }
                //Division
                if (!readerOvertime.IsDBNull(2))
                {
                    dtRow[1] = readerOvertime.GetString(2).Trim();
                }
                //Field
                if (!readerOvertime.IsDBNull(3))
                {
                    dtRow[2] = readerOvertime.GetString(3).Trim();
                }
                //Work Code
                if (!readerOvertime.IsDBNull(4))
                {
                    dtRow[3] = readerOvertime.GetString(4).Trim();
                }
                //Job Name
                if (!readerOvertime.IsDBNull(5))
                {
                    dtRow[4] = readerOvertime.GetString(5).Trim();
                }
                //ManDays                    
                dtRow[5] = 0;
                //HolidayHalf
                dtRow[6] = 0;
                //DailyBasic
                dtRow[7] = 0;
                //Over kg Amount
                dtRow[8] = 0;
                //Extra Rate
                dtRow[9] = 0;
                //Scrap Amount
                dtRow[10] = 0;
                //Pss
                dtRow[11] = 0;
                //PRI
                dtRow[12] = 0;
                //Att.Incentive
                dtRow[13] = 0;
                //Cash Kg Amount
                dtRow[14] = 0;
                //Cash Sundry Amount
                dtRow[15] = 0;
                //EPF12
                dtRow[16] = 0;
                //ETF3
                dtRow[17] = 0;
                //Labour Type
                if (!readerOvertime.IsDBNull(6))
                {
                    dtRow[18] = readerOvertime.GetString(6).Trim();
                }
                //Cash man Days
                dtRow[19] = 0;
                //Cash Non Days Amount
                dtRow[20] = 0;
                //Cash Days Amount
                dtRow[21] = 0;
                //overtime
                if (!readerOvertime.IsDBNull(0))
                {
                    dtRow[22] = readerOvertime.GetDecimal(0);
                }
                else
                {
                    dtRow[22] = 0;
                }
                //Job Group Code
                if (!readerOvertime.IsDBNull(7))
                {
                    dtRow[23] = readerOvertime.GetString(7).Trim();
                }
                else
                    dtRow[23] = "NA";
                //Job Group name
                if (!readerOvertime.IsDBNull(8))
                {
                    dtRow[24] = readerOvertime.GetString(8).Trim();
                }
                else
                    dtRow[24] = "NA";
                dtWages.Rows.Add(dtRow);
                #endregion
            }
            readerOvertime.Close();


            return dtWages;

        }

        public DataTable GetWagesIncludingBothLentBorrow(String strDiv, String strCrop, DateTime dFrom, DateTime dTo, int intRPTType)
        {
            SqlDataReader readerJobs;
            SqlDataReader readerDailyEntries;
            SqlDataReader readerOvertime;
            SqlDataReader readerAdditions;
            DataTable dtWages = new DataTable();
            dtWages.Columns.Add("CropType");//0
            dtWages.Columns.Add("Division");//1
            dtWages.Columns.Add("Field");
            dtWages.Columns.Add("Job");
            dtWages.Columns.Add("JobName");
            dtWages.Columns.Add("ManDays");//5
            dtWages.Columns.Add("HalfNames");
            dtWages.Columns.Add("DailyBasic");
            dtWages.Columns.Add("OverKgAmount");
            dtWages.Columns.Add("ExtraRates");
            dtWages.Columns.Add("ScrapAmount");//10
            dtWages.Columns.Add("PSS");
            dtWages.Columns.Add("PRI");
            dtWages.Columns.Add("Att.Incentive");
            dtWages.Columns.Add("CashPlucking");
            dtWages.Columns.Add("CashSundry");//15
            dtWages.Columns.Add("EPF12");//16
            dtWages.Columns.Add("ETF3");//17
            dtWages.Columns.Add("LabourType");//18

            dtWages.Columns.Add("CashManDays");//19
            dtWages.Columns.Add("CashNonDaysAmount");//20
            dtWages.Columns.Add("CashDaysAmount");//21
            dtWages.Columns.Add("Overtime");//22
            dtWages.Columns.Add("GroupCode");//23
            dtWages.Columns.Add("GroupName");//24
            dtWages.Columns.Add("OtherAddition");//25

            DataRow dtRow;
            dtRow = dtWages.NewRow();

            //readerJobs = SQLHelper.ExecuteReader("SELECT        WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (DivisionID like '" + strDiv + "') GROUP BY WorkCodeID ", CommandType.Text);
            //readerJobs = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND    (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName"+
            //                                        " union SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND    (dbo.DailyGroundTransactions.LabourDivision  LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName", CommandType.Text);
            if (intRPTType == 1)
            {
                readerJobs = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND    (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') and (dbo.DailyGroundTransactions.LabourType not in ('Lent Labour','Inter Estate Lent Labour')) " +
                " union SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102))  AND (dbo.DailyGroundTransactions.LabourType<>'General') AND    (dbo.DailyGroundTransactions.LabourDivision  LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName", CommandType.Text);
            }
            else
            {
                readerJobs = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND    (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName ", CommandType.Text);

            }
            while (readerJobs.Read())
            {
                if (!readerJobs.IsDBNull(0))
                {

                    #region ValueInitialize

                    #endregion
                    //readerDailyEntries = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID  AS DivisionID,   dbo.DailyGroundTransactions.FieldID  AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, case when (dbo.DailyGroundTransactions.DivisionID='" + strDiv + "') then case when (dbo.DailyGroundTransactions.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays, dbo.DailyGroundTransactions.LabourType AS LabourType1 FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND   (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') GROUP BY dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID ,   dbo.DailyGroundTransactions.FieldID , dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourType,dbo.DailyGroundTransactions.DivisionID,dbo.DailyGroundTransactions.LabourDivision"+
                    //                                                " union SELECT        dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.LabourDivision  AS DivisionID,   dbo.DailyGroundTransactions.LabourField  AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then case when (dbo.DailyGroundTransactions.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays, dbo.DailyGroundTransactions.LabourType AS LabourType1 FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND   (dbo.DailyGroundTransactions.LabourDivision  LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') GROUP BY dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.LabourDivision ,   dbo.DailyGroundTransactions.LabourField , dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourType,dbo.DailyGroundTransactions.DivisionID,dbo.DailyGroundTransactions.LabourDivision", CommandType.Text);
                    if (intRPTType == 1)
                    {
                        readerDailyEntries = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID  AS DivisionID,   dbo.DailyGroundTransactions.FieldID  AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, case when (dbo.DailyGroundTransactions.DivisionID='" + strDiv + "') then case when (dbo.DailyGroundTransactions.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays, dbo.DailyGroundTransactions.LabourType AS LabourType1, dbo.DailyGroundTransactions.LabourDivision AS DivisionRef FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND   (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') and (dbo.DailyGroundTransactions.LabourType not in ('Lent Labour','Inter Estate Lent Labour')) GROUP BY dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID ,   dbo.DailyGroundTransactions.FieldID , dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourType,dbo.DailyGroundTransactions.DivisionID,dbo.DailyGroundTransactions.LabourDivision" +
                                                                       " union SELECT        dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.LabourDivision  AS DivisionID,   dbo.DailyGroundTransactions.LabourField  AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then case when (dbo.DailyGroundTransactions.LabourType='Lent Labour') then '2.Borrowing From Other' else 'Other' end else 'Other' end AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays, dbo.DailyGroundTransactions.LabourType AS LabourType1, dbo.DailyGroundTransactions.DivisionID AS DivisionRef FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (dbo.DailyGroundTransactions.LabourType<>'General') AND   (dbo.DailyGroundTransactions.LabourDivision  LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') GROUP BY dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.LabourDivision ,   dbo.DailyGroundTransactions.LabourField , dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourType,dbo.DailyGroundTransactions.DivisionID,dbo.DailyGroundTransactions.LabourDivision", CommandType.Text);
                    }
                    else
                    {
                        readerDailyEntries = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID  AS DivisionID,   dbo.DailyGroundTransactions.FieldID  AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, case when (dbo.DailyGroundTransactions.DivisionID='" + strDiv + "') then case when (dbo.DailyGroundTransactions.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays, dbo.DailyGroundTransactions.LabourType AS LabourType1 FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND   (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "')  GROUP BY dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID ,   dbo.DailyGroundTransactions.FieldID , dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourType,dbo.DailyGroundTransactions.DivisionID,dbo.DailyGroundTransactions.LabourDivision", CommandType.Text);

                    }
                    while (readerDailyEntries.Read())
                    {
                        #region DailyEntriesRead
                        dtRow = dtWages.NewRow();

                        //Crop type
                        if (!readerDailyEntries.IsDBNull(0))
                        {
                            dtRow[0] = readerDailyEntries.GetInt32(0);
                        }
                        //Division
                        if (!readerDailyEntries.IsDBNull(1))
                        {
                            dtRow[1] = readerDailyEntries.GetString(1).Trim();
                        }
                        //Field
                        if (!readerDailyEntries.IsDBNull(2))
                        {
                            dtRow[2] = readerDailyEntries.GetString(2).Trim();
                        }
                        //Work Code
                        if (!readerDailyEntries.IsDBNull(3))
                        {
                            dtRow[3] = readerDailyEntries.GetString(3).Trim();
                        }
                        //Job Name
                        if (!readerDailyEntries.IsDBNull(4))
                        {
                            dtRow[4] = readerDailyEntries.GetString(4).Trim();
                        }
                        //ManDays
                        if (!readerDailyEntries.IsDBNull(5))
                        {
                            dtRow[5] = readerDailyEntries.GetDecimal(5);
                        }
                        //HolidayHalf
                        if (!readerDailyEntries.IsDBNull(6))
                        {
                            dtRow[6] = readerDailyEntries.GetDecimal(6);
                        }
                        //DailyBasic
                        if (!readerDailyEntries.IsDBNull(7))
                        {
                            dtRow[7] = readerDailyEntries.GetDecimal(7);
                        }
                        //Over kg Amount
                        if (!readerDailyEntries.IsDBNull(8))
                        {
                            dtRow[8] = readerDailyEntries.GetDecimal(8);
                        }
                        //Extra Rate
                        if (!readerDailyEntries.IsDBNull(9))
                        {
                            dtRow[9] = readerDailyEntries.GetDecimal(9);
                        }
                        //Scrap Amount
                        if (!readerDailyEntries.IsDBNull(10))
                        {
                            dtRow[10] = readerDailyEntries.GetDecimal(10);
                        }
                        //Pss
                        if (!readerDailyEntries.IsDBNull(11))
                        {
                            dtRow[11] = readerDailyEntries.GetDecimal(11);
                        }
                        //PRI
                        if (!readerDailyEntries.IsDBNull(12))
                        {
                            dtRow[12] = readerDailyEntries.GetDecimal(12);
                        }

                        //Att.Incentive
                        if (!readerDailyEntries.IsDBNull(13))
                        {
                            dtRow[13] = readerDailyEntries.GetDecimal(13);
                        }
                        //Cash Kg Amount
                        if (!readerDailyEntries.IsDBNull(14))
                        {
                            dtRow[14] = readerDailyEntries.GetDecimal(14);
                        }
                        //Cash Sundry Amount
                        if (!readerDailyEntries.IsDBNull(15))
                        {
                            dtRow[15] = readerDailyEntries.GetDecimal(15);
                        }
                        //EPF12
                        if (!readerDailyEntries.IsDBNull(16))
                        {
                            dtRow[16] = readerDailyEntries.GetDecimal(16);
                        }
                        //ETF3
                        if (!readerDailyEntries.IsDBNull(17))
                        {
                            dtRow[17] = readerDailyEntries.GetDecimal(17);
                        }

                        //Labour Type
                        if (!readerDailyEntries.IsDBNull(18))
                        {
                            dtRow[18] = readerDailyEntries.GetString(18).Trim();
                        }

                        //Cash man Days
                        if (!readerDailyEntries.IsDBNull(19))
                        {
                            dtRow[19] = readerDailyEntries.GetDecimal(19);
                        }
                        //Cash Non Days Amount
                        if (!readerDailyEntries.IsDBNull(20))
                        {
                            dtRow[20] = readerDailyEntries.GetDecimal(20);
                        }

                        //Cash Days Amount
                        if (!readerDailyEntries.IsDBNull(21))
                        {
                            dtRow[21] = readerDailyEntries.GetDecimal(21);
                        }
                        dtRow[22] = 0;

                        //Job Group Code
                        if (!readerJobs.IsDBNull(1))
                        {
                            dtRow[23] = readerJobs.GetString(1).Trim();
                        }
                        else
                            dtRow[23] = "NA";
                        //Job Group name
                        if (!readerJobs.IsDBNull(2))
                        {
                            dtRow[24] = readerJobs.GetString(2).Trim();
                        }
                        else
                            dtRow[24] = "NA";
                        dtRow[25] = 0;
                        //readerOvertime = SQLHelper.ExecuteReader("SELECT SUM(Expenditure) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (CropCode = '" + readerDailyEntries.GetInt32(0) + "') AND (DivisionCode like '" + strDiv + "') AND (Field = '" + readerDailyEntries.GetString(2).Trim() + "') AND (Job = '" + readerJobs.GetString(0).Trim() + "')", CommandType.Text);
                        //while (readerOvertime.Read())
                        //{

                        //    if (!readerOvertime.IsDBNull(0))
                        //    {
                        //        dtRow[22] = readerOvertime.GetDecimal(0);
                        //    }
                        //    else
                        //    {
                        //        dtRow[22] = 0;
                        //    }
                        //}
                        //readerOvertime.Close();
                        dtWages.Rows.Add(dtRow);
                        #endregion
                    }
                    readerDailyEntries.Close();
                }
            }
            readerJobs.Close();

            //readerJobs = SQLHelper.ExecuteReader("SELECT Job FROM dbo.CHKOvertime WHERE  (OtDate BETWEEN CONVERT(DATETIME, '"+dFrom+"', 102) AND CONVERT(DATETIME, '"+dTo+"', 102))  AND (Job NOT IN (SELECT WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dFrom+"', 102) AND CONVERT(DATETIME, '"+dTo+"', 102))  GROUP BY WorkCodeID ) ) GROUP BY Job ", CommandType.Text);

            //readerOvertime = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropType, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.DivisionCode else  dbo.CHKOvertime.LabourDivision end as Division, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.Field else dbo.CHKOvertime.LabourField end as FieldId, dbo.CHKOvertime.Job,  dbo.JobMaster.JobName, 'ALL' as LabourType FROM dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName WHERE        (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102))  AND (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "')   GROUP BY dbo.CHKOvertime.CropType, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.DivisionCode else  dbo.CHKOvertime.LabourDivision end, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.Field else dbo.CHKOvertime.LabourField end, dbo.CHKOvertime.Job, dbo.JobMaster.JobName", CommandType.Text);
            //readerOvertime = SQLHelper.ExecuteReader("SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode,  dbo.CHKOvertime.DivisionCode  AS Division, dbo.CHKOvertime.Field  AS FieldId, dbo.CHKOvertime.Job, dbo.JobMaster.JobName,case when (dbo.CHKOvertime.DivisionCode='" + strDiv + "') then Case when( dbo.CHKOvertime.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "') GROUP BY dbo.CHKOvertime.CropCode, dbo.CHKOvertime.DivisionCode ,  dbo.CHKOvertime.Field , dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourType,dbo.CHKOvertime.DivisionCode,dbo.CHKOvertime.LabourDivision"+
            //                                            " union SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode,  dbo.CHKOvertime.LabourDivision  AS Division, dbo.CHKOvertime.LabourField  AS FieldId, dbo.CHKOvertime.Job, dbo.JobMaster.JobName,case when (dbo.CHKOvertime.DivisionCode='" + strDiv + "') then Case when( dbo.CHKOvertime.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.CHKOvertime.LabourDivision LIKE '" + strDiv + "') GROUP BY dbo.CHKOvertime.CropCode, dbo.CHKOvertime.DivisionCode ,  dbo.CHKOvertime.LabourField , dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourType,dbo.CHKOvertime.DivisionCode,dbo.CHKOvertime.LabourDivision", CommandType.Text);
            if (intRPTType == 1)
            {
                readerOvertime = SQLHelper.ExecuteReader("SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode,  dbo.CHKOvertime.DivisionCode  AS Division, dbo.CHKOvertime.Field  AS FieldId, dbo.CHKOvertime.Job, dbo.JobMaster.JobName,case when (dbo.CHKOvertime.DivisionCode='" + strDiv + "') then Case when( dbo.CHKOvertime.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "') and  ( dbo.CHKOvertime.LabourType not in ('Lent Labour','Inter Estate')) GROUP BY dbo.CHKOvertime.CropCode, dbo.CHKOvertime.DivisionCode ,  dbo.CHKOvertime.Field , dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourType,dbo.CHKOvertime.DivisionCode,dbo.CHKOvertime.LabourDivision" +
                                                            " union SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode,  dbo.CHKOvertime.LabourDivision  AS Division,  dbo.CHKOvertime.LabourField  AS FieldId, dbo.CHKOvertime.Job,  dbo.JobMaster.JobName, case when (dbo.CHKOvertime.LabourDivision='" + strDiv + "')  then  Case when( dbo.CHKOvertime.LabourType='Lent Labour') then '2.Borrowing From Other' else 'Other' end  else  'Other'  end AS LabourType,   dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName  FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster  ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup  ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID  WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (dbo.CHKOvertime.LabourType<>'General') AND  (dbo.CHKOvertime.LabourDivision LIKE '" + strDiv + "') GROUP BY dbo.CHKOvertime.CropCode, dbo.CHKOvertime.DivisionCode ,  dbo.CHKOvertime.Field , dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourType,dbo.CHKOvertime.DivisionCode,dbo.CHKOvertime.LabourDivision,dbo.CHKOvertime.LabourField", CommandType.Text);
            }
            else
            {
                readerOvertime = SQLHelper.ExecuteReader("SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode,  dbo.CHKOvertime.DivisionCode  AS Division, dbo.CHKOvertime.Field  AS FieldId, dbo.CHKOvertime.Job, dbo.JobMaster.JobName,case when (dbo.CHKOvertime.DivisionCode='" + strDiv + "') then Case when( dbo.CHKOvertime.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "')  GROUP BY dbo.CHKOvertime.CropCode, dbo.CHKOvertime.DivisionCode ,  dbo.CHKOvertime.Field , dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourType,dbo.CHKOvertime.DivisionCode,dbo.CHKOvertime.LabourDivision", CommandType.Text);

            }
            while (readerOvertime.Read())
            {
                #region OvertimeOnly
                dtRow = dtWages.NewRow();
                //Crop type
                if (!readerOvertime.IsDBNull(1))
                {
                    dtRow[0] = readerOvertime.GetInt32(1);
                }
                //Division
                if (!readerOvertime.IsDBNull(2))
                {
                    dtRow[1] = readerOvertime.GetString(2).Trim();
                }
                //Field
                if (!readerOvertime.IsDBNull(3))
                {
                    dtRow[2] = readerOvertime.GetString(3).Trim();
                }
                //Work Code
                if (!readerOvertime.IsDBNull(4))
                {
                    dtRow[3] = readerOvertime.GetString(4).Trim();
                }
                //Job Name
                if (!readerOvertime.IsDBNull(5))
                {
                    dtRow[4] = readerOvertime.GetString(5).Trim();
                }
                //ManDays                    
                dtRow[5] = 0;
                //HolidayHalf
                dtRow[6] = 0;
                //DailyBasic
                dtRow[7] = 0;
                //Over kg Amount
                dtRow[8] = 0;
                //Extra Rate
                dtRow[9] = 0;
                //Scrap Amount
                dtRow[10] = 0;
                //Pss
                dtRow[11] = 0;
                //PRI
                dtRow[12] = 0;
                //Att.Incentive
                dtRow[13] = 0;
                //Cash Kg Amount
                dtRow[14] = 0;
                //Cash Sundry Amount
                dtRow[15] = 0;
                //EPF12
                dtRow[16] = 0;
                //ETF3
                dtRow[17] = 0;
                //Labour Type
                if (!readerOvertime.IsDBNull(6))
                {
                    dtRow[18] = readerOvertime.GetString(6).Trim();
                }
                //Cash man Days
                dtRow[19] = 0;
                //Cash Non Days Amount
                dtRow[20] = 0;
                //Cash Days Amount
                dtRow[21] = 0;
                //overtime
                if (!readerOvertime.IsDBNull(0))
                {
                    dtRow[22] = readerOvertime.GetDecimal(0);
                }
                else
                {
                    dtRow[22] = 0;
                }
                //Job Group Code
                if (!readerOvertime.IsDBNull(7))
                {
                    dtRow[23] = readerOvertime.GetString(7).Trim();
                }
                else
                    dtRow[23] = "NA";
                //Job Group name
                if (!readerOvertime.IsDBNull(8))
                {
                    dtRow[24] = readerOvertime.GetString(8).Trim();
                }
                else
                    dtRow[24] = "NA";
                dtRow[25] = 0;
                dtWages.Rows.Add(dtRow);
                #endregion
            }
            readerOvertime.Close();

            if (strCrop.Equals("%"))
            {
                readerAdditions = SQLHelper.ExecuteReader("SELECT  dbo.CHKEmpAdditions.AdditionYear, dbo.CHKEmpAdditions.AdditionMonth, dbo.CHKEmpAdditions.DivisionID, dbo.CHKAddition.AdditionShortName,  dbo.CHKAddition.AdditionName, dbo.CHKEmpAdditions.Amount FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE        (dbo.CHKEmpAdditions.AdditionYear = '" + dFrom.Year + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + dFrom.Month + "') AND (dbo.CHKEmpAdditions.DivisionID LIKE '" + strDiv + "') AND (NOT (dbo.CHKAddition.AdditionShortName IN ('GRAFA', 'GRAMA', 'GRAFS')))", CommandType.Text);
                while (readerAdditions.Read())
                {
                    dtRow = dtWages.NewRow();
                    dtRow[0] = 5;
                    if (!readerAdditions.IsDBNull(2))
                    {
                        dtRow[1] = readerAdditions.GetString(2);
                    }
                    dtRow[2] = "NA";
                    if (!readerAdditions.IsDBNull(3))
                    {
                        dtRow[3] = readerAdditions.GetString(3);
                    }
                    if (!readerAdditions.IsDBNull(4))
                    {
                        dtRow[4] = readerAdditions.GetString(4);
                    }
                    dtRow[5] = 0;
                    dtRow[6] = 0;
                    dtRow[7] = 0;
                    dtRow[8] = 0;
                    dtRow[9] = 0;
                    dtRow[10] = 0;
                    dtRow[11] = 0;
                    dtRow[12] = 0;
                    dtRow[13] = 0;
                    dtRow[14] = 0;
                    dtRow[15] = 0;
                    dtRow[16] = 0;
                    dtRow[17] = 0;
                    dtRow[18] = "1.General";
                    dtRow[19] = 0;
                    dtRow[20] = 0;
                    dtRow[21] = 0;
                    dtRow[22] = 0;
                    dtRow[23] = "Additions";
                    dtRow[24] = "Additions";
                    if (!readerAdditions.IsDBNull(5))
                    {
                        dtRow[25] = readerAdditions.GetDecimal(5);
                    }
                    else
                        dtRow[25] = 0;
                    dtWages.Rows.Add(dtRow);
                }
                readerAdditions.Close();
            }

            return dtWages;

        }

        public DataSet CheckrollWagesAdditions(String strDiv, String strCrop, DateTime dFrom, DateTime dTo)
        {

            DataSet dsAdditions = new DataSet("WagesAdditions");
            dsAdditions = SQLHelper.FillDataSet("SELECT dbo.CHKEmpAdditions.AdditionYear, dbo.CHKEmpAdditions.AdditionMonth, dbo.CHKEmpAdditions.DivisionID, dbo.CHKAddition.AdditionShortName,  dbo.CHKAddition.AdditionName, dbo.CHKEmpAdditions.Amount FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.AdditionYear = '" + dFrom.Year + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + dFrom.Month + "') AND (NOT (dbo.CHKAddition.AdditionShortName IN ('GRAFA', 'GRAMA',  'GRAFS')))", CommandType.Text);
            return dsAdditions;
        }

        public DataTable GetWagesLentLabourSummary(String strDiv, String strCrop, DateTime dFrom, DateTime dTo, int intRPTType)
        {
            SqlDataReader readerJobs;
            SqlDataReader readerDailyEntries;
            SqlDataReader readerOvertime;
            SqlDataReader readerAdditions;
            DataTable dtWages = new DataTable();
            dtWages.Columns.Add("CropType");//0
            dtWages.Columns.Add("Division");//1
            dtWages.Columns.Add("Field");
            dtWages.Columns.Add("Job");
            dtWages.Columns.Add("JobName");
            dtWages.Columns.Add("ManDays");//5
            dtWages.Columns.Add("HalfNames");
            dtWages.Columns.Add("DailyBasic");
            dtWages.Columns.Add("OverKgAmount");
            dtWages.Columns.Add("ExtraRates");
            dtWages.Columns.Add("ScrapAmount");//10
            dtWages.Columns.Add("PSS");
            dtWages.Columns.Add("PRI");
            dtWages.Columns.Add("Att.Incentive");
            dtWages.Columns.Add("CashPlucking");
            dtWages.Columns.Add("CashSundry");//15
            dtWages.Columns.Add("EPF12");//16
            dtWages.Columns.Add("ETF3");//17
            dtWages.Columns.Add("LabourType");//18

            dtWages.Columns.Add("CashManDays");//19
            dtWages.Columns.Add("CashNonDaysAmount");//20
            dtWages.Columns.Add("CashDaysAmount");//21
            dtWages.Columns.Add("Overtime");//22
            dtWages.Columns.Add("GroupCode");//23
            dtWages.Columns.Add("GroupName");//24
            dtWages.Columns.Add("OtherAddition");//25
            dtWages.Columns.Add("LabourEstate");//26
            dtWages.Columns.Add("LabourDivision");//27
            dtWages.Columns.Add("LabourField");//28

            DataRow dtRow;
            dtRow = dtWages.NewRow();

            //readerJobs = SQLHelper.ExecuteReader("SELECT        WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (DivisionID like '" + strDiv + "') GROUP BY WorkCodeID ", CommandType.Text);
            //readerJobs = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND    (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName"+
            //                                        " union SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND    (dbo.DailyGroundTransactions.LabourDivision  LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName", CommandType.Text);

            readerJobs = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND    (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') and (dbo.DailyGroundTransactions.LabourType not in ('General')) GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName ", CommandType.Text);


            while (readerJobs.Read())
            {
                if (!readerJobs.IsDBNull(0))
                {

                    #region ValueInitialize

                    #endregion
                    //readerDailyEntries = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID  AS DivisionID,   dbo.DailyGroundTransactions.FieldID  AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, case when (dbo.DailyGroundTransactions.DivisionID='" + strDiv + "') then case when (dbo.DailyGroundTransactions.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays, dbo.DailyGroundTransactions.LabourType AS LabourType1 FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND   (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') GROUP BY dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID ,   dbo.DailyGroundTransactions.FieldID , dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourType,dbo.DailyGroundTransactions.DivisionID,dbo.DailyGroundTransactions.LabourDivision"+
                    //                                                " union SELECT        dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.LabourDivision  AS DivisionID,   dbo.DailyGroundTransactions.LabourField  AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then case when (dbo.DailyGroundTransactions.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (dbo.DailyGroundTransactions.LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays, dbo.DailyGroundTransactions.LabourType AS LabourType1 FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND   (dbo.DailyGroundTransactions.LabourDivision  LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') GROUP BY dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.LabourDivision ,   dbo.DailyGroundTransactions.LabourField , dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourType,dbo.DailyGroundTransactions.DivisionID,dbo.DailyGroundTransactions.LabourDivision", CommandType.Text);

                    readerDailyEntries = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID  AS DivisionID,   dbo.DailyGroundTransactions.FieldID  AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays, SUM(dbo.DailyGroundTransactions.HolidayHalfNames) AS HolidayHalf,  SUM(dbo.DailyGroundTransactions.DailyBasic) AS DailyBasic, SUM(dbo.DailyGroundTransactions.OverKgAmount) AS OverKGAmount,  SUM(dbo.DailyGroundTransactions.ExtraRates) AS ExtraRates,  SUM(dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount) AS ScrapKgAmount,  SUM(dbo.DailyGroundTransactions.PSSAmount) AS PSS, SUM(dbo.DailyGroundTransactions.PRIAmount) AS PRI, SUM(dbo.DailyGroundTransactions.IncentiveAmount)  AS AttIncentive, SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount, SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundry,  SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3, CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN '1.General'  ELSE  CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'Lent Labour')   THEN '2.Lent Labour'  ELSE   CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'Inter Estate Lent Labour')  THEN '3.Inter Estate Lent Labour'  ELSE '4.Other'  END  END  END  AS LabourType,  SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 0)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END END) AS CashNonDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN CASE WHEN (dbo.DailyGroundTransactions.CashPlkOkgYesNo = 1)  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE 0 END ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID IN ('TAP','OHV'))  THEN 0 ELSE dbo.DailyGroundTransactions.CashSundryAmount END END) AS CashDays, dbo.DailyGroundTransactions.LabourType AS LabourType1,dbo.DailyGroundTransactions.LabourEstate, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.LabourField FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE    (CONVERT(varchar(50), dbo.DailyGroundTransactions.CropType) LIKE '" + strCrop + "') AND    (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND   (dbo.DailyGroundTransactions.DivisionID  LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkCodeID LIKE '" + readerJobs.GetString(0).Trim() + "') and (dbo.DailyGroundTransactions.LabourType not in ('General')) GROUP BY dbo.DailyGroundTransactions.CropType,  dbo.DailyGroundTransactions.DivisionID ,   dbo.DailyGroundTransactions.FieldID , dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.LabourType,dbo.DailyGroundTransactions.DivisionID,dbo.DailyGroundTransactions.LabourEstate, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.LabourField", CommandType.Text);


                    while (readerDailyEntries.Read())
                    {
                        #region DailyEntriesRead
                        dtRow = dtWages.NewRow();

                        //Crop type
                        if (!readerDailyEntries.IsDBNull(0))
                        {
                            dtRow[0] = readerDailyEntries.GetInt32(0);
                        }
                        //Division
                        if (!readerDailyEntries.IsDBNull(1))
                        {
                            dtRow[1] = readerDailyEntries.GetString(1).Trim();
                        }
                        //Field
                        if (!readerDailyEntries.IsDBNull(2))
                        {
                            dtRow[2] = readerDailyEntries.GetString(2).Trim();
                        }
                        //Work Code
                        if (!readerDailyEntries.IsDBNull(3))
                        {
                            dtRow[3] = readerDailyEntries.GetString(3).Trim();
                        }
                        //Job Name
                        if (!readerDailyEntries.IsDBNull(4))
                        {
                            dtRow[4] = readerDailyEntries.GetString(4).Trim();
                        }
                        //ManDays
                        if (!readerDailyEntries.IsDBNull(5))
                        {
                            dtRow[5] = readerDailyEntries.GetDecimal(5);
                        }
                        //HolidayHalf
                        if (!readerDailyEntries.IsDBNull(6))
                        {
                            dtRow[6] = readerDailyEntries.GetDecimal(6);
                        }
                        //DailyBasic
                        if (!readerDailyEntries.IsDBNull(7))
                        {
                            dtRow[7] = readerDailyEntries.GetDecimal(7);
                        }
                        //Over kg Amount
                        if (!readerDailyEntries.IsDBNull(8))
                        {
                            dtRow[8] = readerDailyEntries.GetDecimal(8);
                        }
                        //Extra Rate
                        if (!readerDailyEntries.IsDBNull(9))
                        {
                            dtRow[9] = readerDailyEntries.GetDecimal(9);
                        }
                        //Scrap Amount
                        if (!readerDailyEntries.IsDBNull(10))
                        {
                            dtRow[10] = readerDailyEntries.GetDecimal(10);
                        }
                        //Pss
                        if (!readerDailyEntries.IsDBNull(11))
                        {
                            dtRow[11] = readerDailyEntries.GetDecimal(11);
                        }
                        //PRI
                        if (!readerDailyEntries.IsDBNull(12))
                        {
                            dtRow[12] = readerDailyEntries.GetDecimal(12);
                        }

                        //Att.Incentive
                        if (!readerDailyEntries.IsDBNull(13))
                        {
                            dtRow[13] = readerDailyEntries.GetDecimal(13);
                        }
                        //Cash Kg Amount
                        if (!readerDailyEntries.IsDBNull(14))
                        {
                            dtRow[14] = readerDailyEntries.GetDecimal(14);
                        }
                        //Cash Sundry Amount
                        if (!readerDailyEntries.IsDBNull(15))
                        {
                            dtRow[15] = readerDailyEntries.GetDecimal(15);
                        }
                        //EPF12
                        if (!readerDailyEntries.IsDBNull(16))
                        {
                            dtRow[16] = readerDailyEntries.GetDecimal(16);
                        }
                        //ETF3
                        if (!readerDailyEntries.IsDBNull(17))
                        {
                            dtRow[17] = readerDailyEntries.GetDecimal(17);
                        }

                        //Labour Type
                        if (!readerDailyEntries.IsDBNull(18))
                        {
                            dtRow[18] = readerDailyEntries.GetString(18).Trim();
                        }

                        //Cash man Days
                        if (!readerDailyEntries.IsDBNull(19))
                        {
                            dtRow[19] = readerDailyEntries.GetDecimal(19);
                        }
                        //Cash Non Days Amount
                        if (!readerDailyEntries.IsDBNull(20))
                        {
                            dtRow[20] = readerDailyEntries.GetDecimal(20);
                        }

                        //Cash Days Amount
                        if (!readerDailyEntries.IsDBNull(21))
                        {
                            dtRow[21] = readerDailyEntries.GetDecimal(21);
                        }
                        dtRow[22] = 0;

                        //Job Group Code
                        if (!readerJobs.IsDBNull(1))
                        {
                            dtRow[23] = readerJobs.GetString(1).Trim();
                        }
                        else
                            dtRow[23] = "NA";
                        //Job Group name
                        if (!readerJobs.IsDBNull(2))
                        {
                            dtRow[24] = readerJobs.GetString(2).Trim();
                        }
                        else
                            dtRow[24] = "NA";
                        dtRow[25] = 0;
                        //Borrowing Estate
                        if (!readerDailyEntries.IsDBNull(23))
                        {
                            dtRow[26] = readerDailyEntries.GetString(23).Trim();
                        }
                        else
                            dtRow[26] = "NA";
                        //Borrowing Division
                        if (!readerDailyEntries.IsDBNull(24))
                        {
                            dtRow[27] = readerDailyEntries.GetString(24).Trim();
                        }
                        else
                            dtRow[27] = "NA";
                        //Borrowing Field
                        if (!readerDailyEntries.IsDBNull(25))
                        {
                            dtRow[28] = readerDailyEntries.GetString(25).Trim();
                        }
                        else
                            dtRow[28] = "NA";

                        //readerOvertime = SQLHelper.ExecuteReader("SELECT SUM(Expenditure) AS Expr1 FROM dbo.CHKOvertime WHERE (OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND (CropCode = '" + readerDailyEntries.GetInt32(0) + "') AND (DivisionCode like '" + strDiv + "') AND (Field = '" + readerDailyEntries.GetString(2).Trim() + "') AND (Job = '" + readerJobs.GetString(0).Trim() + "')", CommandType.Text);
                        //while (readerOvertime.Read())
                        //{

                        //    if (!readerOvertime.IsDBNull(0))
                        //    {
                        //        dtRow[22] = readerOvertime.GetDecimal(0);
                        //    }
                        //    else
                        //    {
                        //        dtRow[22] = 0;
                        //    }
                        //}
                        //readerOvertime.Close();
                        dtWages.Rows.Add(dtRow);
                        #endregion
                    }
                    readerDailyEntries.Close();
                }
            }
            readerJobs.Close();

            //readerJobs = SQLHelper.ExecuteReader("SELECT Job FROM dbo.CHKOvertime WHERE  (OtDate BETWEEN CONVERT(DATETIME, '"+dFrom+"', 102) AND CONVERT(DATETIME, '"+dTo+"', 102))  AND (Job NOT IN (SELECT WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dFrom+"', 102) AND CONVERT(DATETIME, '"+dTo+"', 102))  GROUP BY WorkCodeID ) ) GROUP BY Job ", CommandType.Text);

            //readerOvertime = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropType, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.DivisionCode else  dbo.CHKOvertime.LabourDivision end as Division, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.Field else dbo.CHKOvertime.LabourField end as FieldId, dbo.CHKOvertime.Job,  dbo.JobMaster.JobName, 'ALL' as LabourType FROM dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName WHERE        (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102))  AND (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "')   GROUP BY dbo.CHKOvertime.CropType, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.DivisionCode else  dbo.CHKOvertime.LabourDivision end, case when (dbo.CHKOvertime.LabourType='General') then dbo.CHKOvertime.Field else dbo.CHKOvertime.LabourField end, dbo.CHKOvertime.Job, dbo.JobMaster.JobName", CommandType.Text);
            //readerOvertime = SQLHelper.ExecuteReader("SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode,  dbo.CHKOvertime.DivisionCode  AS Division, dbo.CHKOvertime.Field  AS FieldId, dbo.CHKOvertime.Job, dbo.JobMaster.JobName,case when (dbo.CHKOvertime.DivisionCode='" + strDiv + "') then Case when( dbo.CHKOvertime.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "') GROUP BY dbo.CHKOvertime.CropCode, dbo.CHKOvertime.DivisionCode ,  dbo.CHKOvertime.Field , dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourType,dbo.CHKOvertime.DivisionCode,dbo.CHKOvertime.LabourDivision"+
            //                                            " union SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode,  dbo.CHKOvertime.LabourDivision  AS Division, dbo.CHKOvertime.LabourField  AS FieldId, dbo.CHKOvertime.Job, dbo.JobMaster.JobName,case when (dbo.CHKOvertime.DivisionCode='" + strDiv + "') then Case when( dbo.CHKOvertime.LabourType='General') then '1.General' else '3.Lent To Other' end else case when (LabourDivision='" + strDiv + "') then '2.Borrowing From Other' else 'Other' end end AS LabourType,  dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.CHKOvertime.LabourDivision LIKE '" + strDiv + "') GROUP BY dbo.CHKOvertime.CropCode, dbo.CHKOvertime.DivisionCode ,  dbo.CHKOvertime.LabourField , dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourType,dbo.CHKOvertime.DivisionCode,dbo.CHKOvertime.LabourDivision", CommandType.Text);

            readerOvertime = SQLHelper.ExecuteReader("SELECT  SUM(dbo.CHKOvertime.Expenditure) AS Expr1, dbo.CHKOvertime.CropCode,  dbo.CHKOvertime.DivisionCode  AS Division, dbo.CHKOvertime.Field  AS FieldId, dbo.CHKOvertime.Job, dbo.JobMaster.JobName,CASE WHEN (dbo.CHKOvertime.LabourType = 'General')  THEN '1.General' ELSE CASE WHEN (dbo.CHKOvertime.LabourType = 'Lent Labour')  THEN '2.Lent Labour' ELSE CASE WHEN (dbo.CHKOvertime.LabourType = 'Inter Estate')  THEN '3.Inter Estate Lent Labour' ELSE '4.Other' END END END AS LabourType,  dbo.JobGroup.GroupShortName, dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourEstate, dbo.CHKOvertime.LabourDivision, dbo.CHKOvertime.LabourField FROM            dbo.CHKOvertime INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE    (dbo.CHKOvertime.CropCode like '" + strCrop + "') and      (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + dFrom + "', 102) AND CONVERT(DATETIME, '" + dTo + "', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "') and  ( dbo.CHKOvertime.LabourType not in ('General')) GROUP BY dbo.CHKOvertime.CropCode, dbo.CHKOvertime.DivisionCode ,  dbo.CHKOvertime.Field , dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.JobGroup.GroupShortName,  dbo.JobGroup.GroupName,dbo.CHKOvertime.LabourType,dbo.CHKOvertime.DivisionCode,dbo.CHKOvertime.LabourEstate, dbo.CHKOvertime.LabourDivision, dbo.CHKOvertime.LabourField", CommandType.Text);


            while (readerOvertime.Read())
            {
                #region OvertimeOnly
                dtRow = dtWages.NewRow();
                //Crop type
                if (!readerOvertime.IsDBNull(1))
                {
                    dtRow[0] = readerOvertime.GetInt32(1);
                }
                //Division
                if (!readerOvertime.IsDBNull(2))
                {
                    dtRow[1] = readerOvertime.GetString(2).Trim();
                }
                //Field
                if (!readerOvertime.IsDBNull(3))
                {
                    dtRow[2] = readerOvertime.GetString(3).Trim();
                }
                //Work Code
                if (!readerOvertime.IsDBNull(4))
                {
                    dtRow[3] = readerOvertime.GetString(4).Trim();
                }
                //Job Name
                if (!readerOvertime.IsDBNull(5))
                {
                    dtRow[4] = readerOvertime.GetString(5).Trim();
                }
                //ManDays                    
                dtRow[5] = 0;
                //HolidayHalf
                dtRow[6] = 0;
                //DailyBasic
                dtRow[7] = 0;
                //Over kg Amount
                dtRow[8] = 0;
                //Extra Rate
                dtRow[9] = 0;
                //Scrap Amount
                dtRow[10] = 0;
                //Pss
                dtRow[11] = 0;
                //PRI
                dtRow[12] = 0;
                //Att.Incentive
                dtRow[13] = 0;
                //Cash Kg Amount
                dtRow[14] = 0;
                //Cash Sundry Amount
                dtRow[15] = 0;
                //EPF12
                dtRow[16] = 0;
                //ETF3
                dtRow[17] = 0;
                //Labour Type
                if (!readerOvertime.IsDBNull(6))
                {
                    dtRow[18] = readerOvertime.GetString(6).Trim();
                }
                //Cash man Days
                dtRow[19] = 0;
                //Cash Non Days Amount
                dtRow[20] = 0;
                //Cash Days Amount
                dtRow[21] = 0;
                //overtime
                if (!readerOvertime.IsDBNull(0))
                {
                    dtRow[22] = readerOvertime.GetDecimal(0);
                }
                else
                {
                    dtRow[22] = 0;
                }
                //Job Group Code
                if (!readerOvertime.IsDBNull(7))
                {
                    dtRow[23] = readerOvertime.GetString(7).Trim();
                }
                else
                    dtRow[23] = "NA";
                //Job Group name
                if (!readerOvertime.IsDBNull(8))
                {
                    dtRow[24] = readerOvertime.GetString(8).Trim();
                }
                else
                    dtRow[24] = "NA";
                dtRow[25] = 0;
                //Borrowing Estate
                if (!readerOvertime.IsDBNull(9))
                {
                    dtRow[26] = readerOvertime.GetString(9).Trim();
                }
                else
                    dtRow[26] = "NA";
                //Borrowing Division
                if (!readerOvertime.IsDBNull(10))
                {
                    dtRow[27] = readerOvertime.GetString(10).Trim();
                }
                else
                    dtRow[27] = "NA";
                //Borrowing Field
                if (!readerOvertime.IsDBNull(11))
                {
                    dtRow[28] = readerOvertime.GetString(11).Trim();
                }
                else
                    dtRow[28] = "NA";
                dtWages.Rows.Add(dtRow);
                #endregion
            }
            readerOvertime.Close();

            //if (strCrop.Equals("%"))
            //{
            //    readerAdditions = SQLHelper.ExecuteReader("SELECT  dbo.CHKEmpAdditions.AdditionYear, dbo.CHKEmpAdditions.AdditionMonth, dbo.CHKEmpAdditions.DivisionID, dbo.CHKAddition.AdditionShortName,  dbo.CHKAddition.AdditionName, dbo.CHKEmpAdditions.Amount FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE        (dbo.CHKEmpAdditions.AdditionYear = '" + dFrom.Year + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + dFrom.Month + "') AND (NOT (dbo.CHKAddition.AdditionShortName IN ('GRAFA', 'GRAMA', 'GRAFS')))", CommandType.Text);
            //    while (readerAdditions.Read())
            //    {
            //        dtRow = dtWages.NewRow();
            //        dtRow[0] = 5;
            //        if (!readerAdditions.IsDBNull(2))
            //        {
            //            dtRow[1] = readerAdditions.GetString(2);
            //        }
            //        dtRow[2] = "NA";
            //        if (!readerAdditions.IsDBNull(3))
            //        {
            //            dtRow[3] = readerAdditions.GetString(3);
            //        }
            //        if (!readerAdditions.IsDBNull(4))
            //        {
            //            dtRow[4] = readerAdditions.GetString(4);
            //        }
            //        dtRow[5] = 0;
            //        dtRow[6] = 0;
            //        dtRow[7] = 0;
            //        dtRow[8] = 0;
            //        dtRow[9] = 0;
            //        dtRow[10] = 0;
            //        dtRow[11] = 0;
            //        dtRow[12] = 0;
            //        dtRow[13] = 0;
            //        dtRow[14] = 0;
            //        dtRow[15] = 0;
            //        dtRow[16] = 0;
            //        dtRow[17] = 0;
            //        dtRow[18] = "1.General";
            //        dtRow[19] = 0;
            //        dtRow[20] = 0;
            //        dtRow[21] = 0;
            //        dtRow[22] = 0;
            //        dtRow[23] = "Additions";
            //        dtRow[24] = "Additions";
            //        if (!readerAdditions.IsDBNull(5))
            //        {
            //            dtRow[25] = readerAdditions.GetDecimal(5);
            //        }
            //        else
            //            dtRow[25] = 0;
            //        dtWages.Rows.Add(dtRow);
            //    }
            //    readerAdditions.Close();
            //}

            return dtWages;

        }


    }
}
