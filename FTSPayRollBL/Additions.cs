using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace FTSPayRollBL
{
    public class Additions
    {
        private String strDivision;
        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }

        private String strEmpno;
        public String StrEmpno
        {
            get { return strEmpno; }
            set { strEmpno = value; }
        }

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

        private Decimal decAmount;
        public Decimal DecAmount
        {
            get { return decAmount; }
            set { decAmount = value; }
        }

        private Boolean bitType;
        public Boolean BitType
        {
            get { return bitType; }
            set { bitType = value; }
        }

        private Int32 additionId;

        public Int32 AdditionId
        {
            get { return additionId; }
            set { additionId = value; }
        }

        private Decimal addAmount;

        public Decimal AddAmount
        {
            get { return addAmount; }
            set { addAmount = value; }
        }

        private Int32 noOfMonths;

        public Int32 NoOfMonths
        {
            get { return noOfMonths; }
            set { noOfMonths = value; }
        }

        private String strDescription;

        public String StrDescription
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        private Int32 intAdditionRefId;

        public Int32 IntAdditionRefId
        {
            get { return intAdditionRefId; }
            set { intAdditionRefId = value; }
        }

        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }





        public DataTable ListAllAdditions()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DebtorId"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DebtYear"));
            dt.Columns.Add(new DataColumn("DebtMonth"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("DebtAmount"));
            dt.Columns.Add(new DataColumn("PaidAdditionYesNo"));
            dt.Columns.Add(new DataColumn("RecoveredYesNO"));
            dt.Columns.Add(new DataColumn("DebtorType"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DebtorId, DivisionID, DebtYear, DebtMonth, EmpNo, DebtAmount, PaidAdditionYesNo, RecoveredYesNO,DebtorType, UserId, CreateDateTime FROM ChkDebtors", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetInt32(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetDecimal(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetBoolean(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDateTime(10);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public void UpdatePaidAdditionYesNoAsTrue()
        {
            SQLHelper.ExecuteNonQuery("UPDATE ChkDebtors SET PaidAdditionYesNo = 1 WHERE  (DivisionID = '" + strDivision + "') AND (DebtYear = '" + intYear + "') AND (DebtMonth = '" + intMonth + "') AND (EmpNo = '" + StrEmpno + "')", CommandType.Text);
        }

        public void UpdatePaidAdditionYesNoAsFalse()
        {
            SQLHelper.ExecuteNonQuery("UPDATE ChkDebtors SET PaidAdditionYesNo = 0 WHERE  (DivisionID = '" + strDivision + "') AND (DebtYear = '" + intYear + "') AND (DebtMonth = '" + intMonth + "') AND (EmpNo = '" + StrEmpno + "')", CommandType.Text);
        }

        public Decimal getAmount()
        {
            Decimal amount = 0;
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT DebtAmount FROM ChkDebtors WHERE (DebtYear = '" + intYear + "') AND (DebtMonth = '" + intMonth + "') AND (EmpNO = '" + StrEmpno + "') AND (DivisionId = '" + strDivision + "') AND (DebtorType = 'Debtors')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    amount = reader.GetDecimal(0);
                }
            }

            return amount;
        }

        public DataTable getAddition()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AdditionName");
            dt.Columns.Add("AdditionId");
            dt.Columns.Add("ShortName");


            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT  AdditionName,AdditionId, AdditionShortName FROM CHKAddition ", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetString(2).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public void InsertAdditions()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKEmpAdditions (EmpNo, AdditionYear, AdditionMonth, AdditionId, Amount, UserId,DivisionID) VALUES ('" + StrEmpno + "','" + IntYear + "','" + IntMonth + "','" + AdditionId + "','" + DecAmount + "','" + StrUserId + "','" + StrDivision + "')", CommandType.Text);
        }

        public void UpdateAdditions()
        {
            SQLHelper.ExecuteNonQuery("UPDATE CHKEmpAdditions SET Amount ='" + DecAmount + "', UserId ='" + StrUserId + "' WHERE dbo.CHKEmpAdditions.EmpAddID='" + IntAdditionRefId + "' ", CommandType.Text);
        }

        public void DeleteAdditions()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKEmpAdditions WHERE dbo.CHKEmpAdditions.EmpAddID='" + IntAdditionRefId + "' ", CommandType.Text);
        }

        public DataTable ListAllEmpAdditions()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("AdditionYear"));
            dt.Columns.Add(new DataColumn("AdditionMonth"));
            dt.Columns.Add(new DataColumn("AdditionId"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("Cancel"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("DivsionId"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  EmpNo, AdditionYear, AdditionMonth, AdditionId, Description, Amount, CancelYesNo, UserId, CreateDateTime, DivisionID FROM CHKEmpAdditions", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetInt32(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetDecimal(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetBoolean(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7).Trim();
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetDateTime(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEmpAdditions(String strDiv, Int32 intYear, Int32 intMonth, Int32 intAddition)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("AdditionCode"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("AdditionYear"));
            dt.Columns.Add(new DataColumn("AdditionMonth"));
            dt.Columns.Add(new DataColumn("DivsionId"));
            dt.Columns.Add(new DataColumn("AddRef"));
            dt.Columns.Add(new DataColumn("RefNo"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKEmpAdditions.EmpNo, dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.Amount, dbo.CHKEmpAdditions.AdditionYear, dbo.CHKEmpAdditions.AdditionMonth, dbo.CHKEmpAdditions.DivisionID, dbo.CHKEmpAdditions.AdditionId, dbo.CHKEmpAdditions.EmpAddID " +
                                                " FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.DivisionID = '" + strDiv + "') AND (dbo.CHKEmpAdditions.AdditionYear = '" + intYear + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonth + "') AND (dbo.CHKEmpAdditions.AdditionId = '" + intAddition + "')", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetDecimal(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetInt32(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetInt32(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEmpAdditions(String strDiv, Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("AdditionCode"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("AdditionYear"));
            dt.Columns.Add(new DataColumn("AdditionMonth"));
            dt.Columns.Add(new DataColumn("DivsionId"));
            dt.Columns.Add(new DataColumn("AddRef"));
            dt.Columns.Add(new DataColumn("RefNo"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKEmpAdditions.EmpNo, dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.Amount, dbo.CHKEmpAdditions.AdditionYear, dbo.CHKEmpAdditions.AdditionMonth, dbo.CHKEmpAdditions.DivisionID, dbo.CHKEmpAdditions.AdditionId, dbo.CHKEmpAdditions.EmpAddID " +
                                                " FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.DivisionID = '" + strDiv + "') AND (dbo.CHKEmpAdditions.AdditionYear = '" + intYear + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonth + "') ", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetDecimal(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetInt32(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetInt32(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public void UpdateOtherEPFAdditions(String Division, String EmpNo, Int32 intYear, Int32 intMonth, Decimal decAmount)
        {
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE [dbo].[EmpMonthlyEarnings]  SET [OtherEPFPay]='" + decAmount + "' WHERE ([DivisionId]='" + Division + "') AND ([EmpNO]='" + EmpNo + "') AND  ([Month]='" + intMonth + "') AND ([Year]='" + intYear + "')", CommandType.Text);
        }


        public void DeleteAdditionsBulk(int _year, int _month, int _addtionCode, String _division)
        {
            string str = " DELETE FROM dbo.CHKEmpAdditions WHERE  (AdditionYear = " + _year + ") AND (AdditionMonth = " + _month + ") AND (AdditionId = " + _addtionCode + ")  AND (DivisionID = '" + _division + "')";
            SQLHelper.ExecuteNonQuery(str, CommandType.Text);
        }
        
        //update amount of selected items at onece
        public void UpdateAdditionsBulk(string _empNo)
        {

            string str = "UPDATE CHKEmpAdditions SET Amount ='200' WHERE EmpNo='"+_empNo+"'";
                SQLHelper.ExecuteNonQuery(str, CommandType.Text);


        }
        //
        public void InsertAdditionsBulk(int _year, int _month, int _addtionCode, String _division, decimal _amount,string _empNo,string _UserID)
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();


            param = SQLHelper.CreateParameter("@StrEmpno", SqlDbType.VarChar, 200);
            param.Value = _empNo;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@IntYear", SqlDbType.Int);
            param.Value = _year;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@IntMonth", SqlDbType.Int);
            param.Value = _month;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@AdditionId", SqlDbType.Int);
            param.Value = _addtionCode;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@DecAmount", SqlDbType.Decimal);
            param.Value = _amount;
            paramList.Add(param);



            param = SQLHelper.CreateParameter("@StrUserId", SqlDbType.VarChar, 200);
            param.Value = _UserID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@StrDivision", SqlDbType.VarChar, 200);
            param.Value = _division;
            paramList.Add(param);
            SQLHelper.ExecuteNonQuery("[dbo].[SP_SaveAddition]", CommandType.StoredProcedure, paramList);

        

        }



        public DataSet getAditionEmpListForBulk(int _year, int _month, int _addtionCode, String _division)
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();


            param = SQLHelper.CreateParameter("@AdditionYear", SqlDbType.Int);
            param.Value = _year;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@AdditionMonth", SqlDbType.Int);
            param.Value = _month;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@AdditionCode", SqlDbType.Int);
            param.Value = _addtionCode;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar, 200);
            param.Value = _division;
            paramList.Add(param);

            DataSet ds = SQLHelper.FillDataSet("[dbo].[SP_GetEMPAdditionList]", CommandType.StoredProcedure, paramList);







            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Amount"].ToString()))
                {
                    ds.Tables[0].Rows[i]["Amount"] = 0;
                }

            }









            return ds;
        }


        public String InsertInactiveOtherEPFAdditions(String strDiv, String strEmpNo, Int32 intYear, Int32 intMonth, Decimal decAmount)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = strDiv;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@eid", SqlDbType.VarChar, 50);
            param.Value = strEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@OtherEPFPay", SqlDbType.Decimal);
            param.Value = decAmount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesYear", SqlDbType.Int);
            param.Value = intYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesMonth", SqlDbType.Int);
            param.Value = intMonth;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertInactiveEmployeeOtherAddition", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;

        }

        public void AddLastYearHolidayPayEPFPayable(String strDiv, int intYear, int intMonth,Boolean IsThisYear)
        {            
            SqlDataReader dataReader;
            int intLastYear = intYear;
            if (IsThisYear)
            {
                intLastYear = intYear;
            }
            else
            {
                intLastYear = intYear - 1;
            }
            
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID, EmpNo, EPFPaybleAverage * HPQualifyDays AS Expr1 FROM dbo.HolidayPayData WHERE (Year = '"+intLastYear+"') AND (DivisionID = '"+strDiv+"') AND (EPFPaybleAverage * HPQualifyDays > 0)", CommandType.Text);
            while (dataReader.Read())
            {
                SQLHelper.ExecuteNonQuery("UPDATE [dbo].[EmpMonthlyEarnings]   SET [OtherEPFPay]=0 WHERE ([DivisionId]='" + strDiv + "')  AND  ([Year]='" + intYear + "') AND ([Month]='" + intMonth + "') AND (OtherEPFPay is null)", CommandType.Text);
                if (!dataReader.IsDBNull(0) && !dataReader.IsDBNull(1))
                {
                    InsertLastYearHPOtherEPFAdditions(dataReader.GetString(0).Trim(), dataReader.GetString(1).Trim(), intYear, intMonth, dataReader.GetDecimal(2));
                }                
            }
            dataReader.Close();
        }

        public Decimal GetEPFOtherAdditionTotal(int intYear, int intMonth, String strDiv)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT SUM(OtherEPFPay) AS Expr1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '"+intYear+"') AND (Month = '"+intMonth+"') AND (DivisionId = '"+strDiv+"')", CommandType.Text);
            return Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
        }

        public String InsertLastYearHPOtherEPFAdditions(String strDiv, String strEmpNo, Int32 intYear, Int32 intMonth, Decimal decAmount)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = strDiv;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@eid", SqlDbType.VarChar, 50);
            param.Value = strEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@OtherEPFPay", SqlDbType.Decimal);
            param.Value = decAmount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesYear", SqlDbType.Int);
            param.Value = intYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesMonth", SqlDbType.Int);
            param.Value = intMonth;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertLastYearEmployeeHPOtherAddition", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;

        }

        public void UpdateInactiveEmpOtherEPFAdditions(String Division, String EmpNo, Int32 intYear, Int32 intMonth, Decimal decAmount)
        {
            ////SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            //SQLHelper.ExecuteNonQuery("UPDATE [dbo].[EmpMonthlyEarnings]  SET [OtherEPFPay]='" + decAmount + "' WHERE ([DivisionId]='" + Division + "') AND ([EmpNO]='" + EmpNo + "') AND  ([Month]='" + intMonth + "') AND ([Year]='" + intYear + "')", CommandType.Text);
        }


        public DataTable ListEmployeeOtherEPFAdditions(String strDiv, Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Division");
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("OtherEPFAmt");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionId,Year, Month, EmpNO, OtherEPFPay FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + intYear + "') AND (Month = '" + intMonth + "') AND (DivisionId = '" + strDiv + "')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetDecimal(4);
                }

                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListInactiveEmployeeOtherEPFAdditions(String strDiv, Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Division");
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("OtherEPFAmt");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     DivisionID, '" + intYear + "' AS Expr1,  '" + intMonth + "' AS Expr2, EmpNo, 0 AS Expr3 FROM dbo.EmployeeMaster WHERE (NOT (EmpNo IN (SELECT EmpNO FROM dbo.EmpMonthlyEarnings WHERE      (Year = '" + intYear + "') AND (Month = '" + intMonth + "') AND (DivisionId = '" + strDiv + "')))) AND (DivisionID = '" + strDiv + "')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = intYear;
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = intMonth;
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    //dtRow[4] = GetOtherEPFAddition(strDiv, IntYear, IntMonth, dataReader.GetString(3).Trim());
                    dtRow[4] = "0";
                }

                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public Decimal GetOtherEPFAddition(String strDiv, Int32 intYear, Int32 intMonth, String stEmp)
        {
            Decimal decAmount = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT isnull(SUM(OtherEPFPay),0) AS Expr1 FROM  dbo.EmpMonthlyEarnings WHERE     (Year = '" + IntYear + "') AND (Month = '" + IntMonth + "') AND (DivisionId = '" + strDiv + "') AND (EmpNO = '" + stEmp + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decAmount = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();
            return decAmount;
        }

        public String GetAdditionNameByID(Int32 intAdditionId)
        {
            String AdditionName = "";
            SqlDataReader reader = SQLHelper.ExecuteReader("SELECT AdditionShortName FROM dbo.CHKAddition WHERE  (AdditionId ='" + intAdditionId + "')", CommandType.Text);
            while (reader.Read())
            {
                AdditionName = reader.GetString(0).Trim();
            }
            reader.Close();
            return AdditionName;
        }
    }
}
