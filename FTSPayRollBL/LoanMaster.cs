using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class LoanMaster
    {
        private Int32 intDeductCode;

        public Int32 IntDeductCode
        {
            get { return intDeductCode; }
            set { intDeductCode = value; }
        }
        private Int32 intLoanCode;

        public Int32 IntLoanCode
        {
            get { return intLoanCode; }
            set { intLoanCode = value; }
        }
        private Int32 intCategoryCode;

        public Int32 IntCategoryCode
        {
            get { return intCategoryCode; }
            set { intCategoryCode = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private String strEmpNo;

        public String StrEmpNo
        {
            get { return strEmpNo; }
            set { strEmpNo = value; }
        }
        private String strName;

        public String StrName
        {
            get { return strName; }
            set { strName = value; }
        }
        private float flPrincipalAmount;

        public float FlPrincipalAmount
        {
            get { return flPrincipalAmount; }
            set { flPrincipalAmount = value; }
        }
        private float flNoOfInstallments;

        public float FlNoOfInstallments
        {
            get { return flNoOfInstallments; }
            set { flNoOfInstallments = value; }
        }
        private float flDirectAmount;

        public float FlDirectAmount
        {
            get { return flDirectAmount; }
            set { flDirectAmount = value; }
        }
        private float flInstallmentAmount;

        public float FlInstallmentAmount
        {
            get { return flInstallmentAmount; }
            set { flInstallmentAmount = value; }
        }
        private float flRecoveredInstallments;

        public float FlRecoveredInstallments
        {
            get { return flRecoveredInstallments; }
            set { flRecoveredInstallments = value; }
        }
        private float flRecoveredAmount;

        public float FlRecoveredAmount
        {
            get { return flRecoveredAmount; }
            set { flRecoveredAmount = value; }
        }
        private float flBalanceAmount;

        public float FlBalanceAmount
        {
            get { return flBalanceAmount; }
            set { flBalanceAmount = value; }
        }
        private Int32 intFromMonth;

        public Int32 IntFromMonth
        {
            get { return intFromMonth; }
            set { intFromMonth = value; }
        }
        private Int32 intFromYear;

        public Int32 IntFromYear
        {
            get { return intFromYear; }
            set { intFromYear = value; }
        }

        private Int32 intToMonth;

        public Int32 IntToMonth
        {
            get { return intToMonth; }
            set { intToMonth = value; }
        }
        private Int32 intToYear;

        public Int32 IntToYear
        {
            get { return intToYear; }
            set { intToYear = value; }
        }
        private DateTime dtLoanDate;

        public DateTime DtLoanDate
        {
            get { return dtLoanDate; }
            set { dtLoanDate = value; }
        }
        
        private Int32 intLoanId;

        public Int32 IntLoanId
        {
            get { return intLoanId; }
            set { intLoanId = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }
        private String AccountNo;

        public String AccountNo1
        {
            get { return AccountNo; }
            set { AccountNo = value; }
        }

        private Int32 intDeductionGroup;

        public Int32 IntDeductionGroup
        {
            get { return intDeductionGroup; }
            set { intDeductionGroup = value; }
        }

        private String strTerminateReason;

        public String StrTerminateReason
        {
            get { return strTerminateReason; }
            set { strTerminateReason = value; }
        }

        private String strPayeeACNo;

        public String StrPayeeACNo
        {
            get { return strPayeeACNo; }
            set { strPayeeACNo = value; }
        }
        private String strPayeeACName;

        public String StrPayeeACName
        {
            get { return strPayeeACName; }
            set { strPayeeACName = value; }
        }
        private String strPayeeAmount;

        public String StrPayeeAmount
        {
            get { return strPayeeAmount; }
            set { strPayeeAmount = value; }
        }
        private Decimal decPayeeAmount;

        public Decimal DecPayeeAmount
        {
            get { return decPayeeAmount; }
            set { decPayeeAmount = value; }
        }
        //public String InsertLoan()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();           
        //    param = SQLHelper.CreateParameter("@DeductionCode", SqlDbType.Int, 4);
        //    param.Value = IntDeductCode;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@DivisionCode", SqlDbType.VarChar,50);
        //    param.Value = StrDivision;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@CategoryCode", SqlDbType.Int, 50);
        //    param.Value = IntCategoryCode;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@EmployeeNo", SqlDbType.VarChar, 50);
        //    param.Value = StrEmpNo;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@LoanDate", SqlDbType.DateTime);
        //    param.Value = DtLoanDate;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@Principalamount", SqlDbType.Float);
        //    param.Value=FlPrincipalAmount;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@NoofInstallments", SqlDbType.Float);
        //    param.Value=FlNoOfInstallments;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@DirectPayment", SqlDbType.Float);
        //    param.Value=FlDirectAmount;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@InstallmentAmount", SqlDbType.Float);
        //    param.Value=FlInstallmentAmount;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@RecovredInstallments", SqlDbType.Float);
        //    param.Value = FlRecoveredInstallments;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@RecoveredAmount", SqlDbType.Float);
        //    param.Value = FlRecoveredAmount;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@loanFromMonth", SqlDbType.Int,4);
        //    param.Value = IntFromMonth;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@loanFromYear", SqlDbType.Int,4);
        //    param.Value = IntFromYear;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@loanToMonth", SqlDbType.Int,4);
        //    param.Value = IntToMonth;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@loanToYear", SqlDbType.Int,4);
        //    param.Value = IntToYear;
        //    paramList.Add(param);            
        //    param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar,50);
        //    param.Value = "admin";
        //    paramList.Add(param);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd = SQLHelper.CreateCommand("spInsertLoan", CommandType.StoredProcedure, paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    cmd.Dispose();
        //    return status;
        //}

        //public String UpdateLoan()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@LoanId", SqlDbType.Int, 4);
        //    param.Value = IntLoanId;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@DeductionCode", SqlDbType.Int, 4);
        //    param.Value = IntDeductCode;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@DivisionCode", SqlDbType.VarChar, 50);
        //    param.Value = StrDivision;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@CategoryCode", SqlDbType.Int, 50);
        //    param.Value = IntCategoryCode;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@EmployeeNo", SqlDbType.VarChar, 50);
        //    param.Value = StrEmpNo;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@LoanDate", SqlDbType.DateTime);
        //    param.Value = DtLoanDate;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@Principalamount", SqlDbType.Float);
        //    param.Value = FlPrincipalAmount;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@NoofInstallments", SqlDbType.Float);
        //    param.Value = FlNoOfInstallments;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@DirectPayment", SqlDbType.Float);
        //    param.Value = FlDirectAmount;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@InstallmentAmount", SqlDbType.Float);
        //    param.Value = FlInstallmentAmount;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@RecovredInstallments", SqlDbType.Float);
        //    param.Value = FlRecoveredInstallments;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@RecoveredAmount", SqlDbType.Float);
        //    param.Value = FlRecoveredAmount;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@loanFromMonth", SqlDbType.Int, 4);
        //    param.Value = IntFromMonth;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@loanFromYear", SqlDbType.Int, 4);
        //    param.Value = IntFromYear;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@loanToMonth", SqlDbType.Int, 4);
        //    param.Value = IntToMonth;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@loanToYear", SqlDbType.Int, 4);
        //    param.Value = IntToYear;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
        //    param.Value = "admin";
        //    paramList.Add(param);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd = SQLHelper.CreateCommand("spUpdateLoan", CommandType.StoredProcedure, paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    cmd.Dispose();
        //    return status;
        //}

        //public String DeleteLoan()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@LoanId", SqlDbType.Int,4);
        //    param.Value = IntLoanId;
        //    paramList.Add(param);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd = SQLHelper.CreateCommand("spDeleteLoan", CommandType.StoredProcedure, paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    cmd.Dispose();
        //    return status;
        //}

        public DataTable ListLoans()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductionGroup");
            dt.Columns.Add("DeductionCode");
            dt.Columns.Add("CategoryCode");
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("EmployeeNo");
            dt.Columns.Add("LoanName");
            dt.Columns.Add("Principalamount");
            dt.Columns.Add("NoofInstallments");
            dt.Columns.Add("InstallmentAmount");
            dt.Columns.Add("AccountNo");
            dt.Columns.Add("LoanDate");
            dt.Columns.Add("LoanRefNo");
            DataRow drow;
            drow = dt.NewRow();
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT DeductionGroup, DeductionCode, CategoryCode, DivisionCode, EmployeeNo, LoanName, Principalamount, NoofInstallments, InstallmentAmount,AccountNo, LoanDate,LoanCode FROM CHKLoan", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    drow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    drow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    drow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    drow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    drow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    drow[8] = dataReader.GetDecimal(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    drow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    drow[10] = dataReader.GetDateTime(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    drow[11] = dataReader.GetInt32(11);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListLoans(String Division,Int32 intYear,Int32 intMonth,Int32 intGroup,Int32 intDeduct)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Group");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("InstallmentAmount");
            dt.Columns.Add("StartYear");
            dt.Columns.Add("StartMonth");
            dt.Columns.Add("NoofInstallments");
            dt.Columns.Add("Principalamount");
            dt.Columns.Add("AccountNo");
            dt.Columns.Add("LoanName");
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("GroupRef");
            dt.Columns.Add("DeductRef");
            dt.Columns.Add("LoanRefNo");
            dt.Columns.Add("Balance");
            DataRow drow;
            drow = dt.NewRow();
            SqlDataReader dataReader;
            //dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName,  dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount,  dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName, dbo.CHKLoan.DivisionCode, dbo.CHKLoan.DeductionGroup, dbo.CHKLoan.DeductionCode,  dbo.CHKLoan.LoanCode,  dbo.CHKLoan.BalanceAmount FROM dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKLoan.DivisionCode = '" + Division + "') AND (CONVERT(datetime, CONVERT(varchar(10), '" + intMonth + "') + '/1/' + CONVERT(varchar(10),  '" + intYear + "'), 102) BETWEEN CONVERT(datetime, dbo.CHKLoan.LoanDate, 102) AND CONVERT(datetime, DATEADD(month,  dbo.CHKLoan.NoofInstallments - 1, dbo.CHKLoan.LoanDate), 102)) AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND  (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (dbo.CHKLoan.ClosedYesNo = 0) ORDER BY dbo.CHKLoan.LoanCode DESC ", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName,  dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount,  dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName, dbo.CHKLoan.DivisionCode, dbo.CHKLoan.DeductionGroup, dbo.CHKLoan.DeductionCode,  dbo.CHKLoan.LoanCode,  dbo.CHKLoan.BalanceAmount FROM dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKLoan.DivisionCode = '" + Division + "') AND  (dbo.CHKLoan.StartYear = '"+intYear+"') AND (dbo.CHKLoan.StartMonth = '"+intMonth+"') AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND  (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (dbo.CHKLoan.ClosedYesNo = 0) ORDER BY dbo.CHKLoan.LoanCode DESC ", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetString(0).Trim();
                }                
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    drow[3] = dataReader.GetDecimal(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    drow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    drow[5] = dataReader.GetInt32(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    drow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    drow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    drow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    drow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    drow[10] = dataReader.GetString(10).Trim();
                }
                if (!dataReader.IsDBNull(11))
                {
                    drow[11] = dataReader.GetInt32(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    drow[12] = dataReader.GetInt32(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    drow[13] = dataReader.GetInt32(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    drow[14] = dataReader.GetDecimal(14);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }


        public DataTable getLoan()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LoanName");
            dt.Columns.Add("LoanCode");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT LoanName,LoanCode FROM CHKLoan WHERE (DeductionCode = '" + intDeductCode + "') AND (DeductionGroup = '" + IntDeductionGroup + "')AND (EmployeeNo = '"+StrEmpNo+"')", CommandType.Text);
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

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable getLoan(String EmpNo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LoanName");
            dt.Columns.Add("LoanCode");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT LoanName,LoanCode FROM CHKLoan WHERE  (EmployeeNo = '" + EmpNo + "')", CommandType.Text);
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

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable getLoanDetails(Int32 LoanCode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LoanCode");
            dt.Columns.Add("LoanName");
            dt.Columns.Add("Principalamount");
            dt.Columns.Add("RecoveredAmount");
            dt.Columns.Add("BalanceAmount");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT     LoanCode, LoanName, Principalamount, RecoveredAmount, BalanceAmount FROM CHKLoan WHERE     (LoanCode = '" + LoanCode + "') AND (ClosedYesNo = 0)", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetDecimal(4);
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public void InsertLoan()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKLoan (DeductionGroup, DeductionCode, DivisionCode, EmployeeNo, LoanName, Principalamount, NoofInstallments, InstallmentAmount, RecovredInstallments, RecoveredAmount, BalanceAmount,AccountNo, LoanDate,ClosedYesNo,StartMonth,StartYear,CategoryCode) VALUES ('" + intDeductionGroup + "','" + IntDeductCode + "','" + strDivision + "','" + strEmpNo + "','" + strName + "','" + flPrincipalAmount + "','" + flNoOfInstallments + "','" + flInstallmentAmount + "','" + 0 + "','" + 0 + "','" + flPrincipalAmount + "','" + "NA" + "','" + dtLoanDate + "','" + '0' + "','"+IntFromMonth+"','"+IntFromYear+"','"+IntCategoryCode+"')", CommandType.Text);
        }

        public void UpdateLoan()
        {
            SQLHelper.ExecuteNonQuery("UPDATE CHKLoan SET DivisionCode ='" + strDivision + "', EmployeeNo ='" + StrEmpNo + "', LoanName ='" + strName + "', Principalamount ='" + flPrincipalAmount + "', NoofInstallments ='" + FlNoOfInstallments + "', InstallmentAmount ='" + FlInstallmentAmount + "', AccountNo ='" + "NA" + "',LoanDate ='" + dtLoanDate + "', StartMonth='"+IntFromMonth+"',StartYear='"+IntFromYear+"'  WHERE (LoanCode = '" + IntLoanCode + "')", CommandType.Text);
        }
        public void UpdateLoanOnlyDeductAmount()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES (getdate() ,'" + IntLoanCode + "' ,'CHKLoan' ,'" + StrDivision + "' ,'" + StrEmpNo + "' ,'" + FlInstallmentAmount.ToString() + "' ,'" + IntDeductionGroup.ToString() + "' ,'" + IntDeductCode.ToString() + "' ,'" + IntFromYear.ToString() + "' ,'" + IntFromMonth.ToString() + "' ,'" + StrUserId + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE CHKLoan SET  LoanName ='" + strName + "',  InstallmentAmount ='" + FlInstallmentAmount + "', AccountNo ='" + "NA" + "'  WHERE (LoanCode = '" + IntLoanCode + "') and (DivisionCode ='" + strDivision + "') and (EmployeeNo ='" + StrEmpNo + "') and (StartMonth='" + IntFromMonth + "') and (StartYear='" + IntFromYear + "')", CommandType.Text);
        }

        public void UpdateLoanOnlyDeductAmount(Boolean boolOnlyBalanceUpdate)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES (getdate() ,'" + IntLoanCode + "' ,'CHKLoan' ,'" + StrDivision + "' ,'" + StrEmpNo + "' ,'" + FlInstallmentAmount.ToString() + "' ,'" + IntDeductionGroup.ToString() + "' ,'" + IntDeductCode.ToString() + "' ,'" + IntFromYear.ToString() + "' ,'" + IntFromMonth.ToString() + "' ,'" + StrUserId + "')", CommandType.Text);
            if (boolOnlyBalanceUpdate)
                SQLHelper.ExecuteNonQuery("UPDATE CHKLoan SET  LoanName ='" + strName + "',  InstallmentAmount ='" + FlInstallmentAmount + "', AccountNo ='" + "NA" + "'  WHERE (LoanCode = '" + IntLoanCode + "') and (DivisionCode ='" + strDivision + "') and (EmployeeNo ='" + StrEmpNo + "') and (StartMonth='" + IntFromMonth + "') and (StartYear='" + IntFromYear + "')", CommandType.Text);
            else
                SQLHelper.ExecuteNonQuery("UPDATE CHKLoan SET  LoanName ='" + strName + "',  InstallmentAmount ='" + FlInstallmentAmount + "', AccountNo ='" + "NA" + "',Principalamount='" + flPrincipalAmount + "', NoofInstallments='" + FlNoOfInstallments + "', RecovredInstallments=0, RecoveredAmount=0, BalanceAmount='" + flPrincipalAmount + "'  WHERE (LoanCode = '" + IntLoanCode + "') and (DivisionCode ='" + strDivision + "') and (EmployeeNo ='" + StrEmpNo + "') and (StartMonth='" + IntFromMonth + "') and (StartYear='" + IntFromYear + "')", CommandType.Text);
        }

        //public void UpdateLoanOnlyDeductAmount()
        //{
        //    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES (getdate() ,'" + IntLoanCode + "' ,'CHKLoan' ,'" + StrDivision + "' ,'" + StrEmpNo + "' ,'" + FlInstallmentAmount.ToString() + "' ,'" + IntDeductionGroup.ToString() + "' ,'" + IntDeductCode.ToString() + "' ,'" + IntFromYear.ToString() + "' ,'" + IntFromMonth.ToString() + "' ,'" + StrUserId + "')", CommandType.Text);
        //    SQLHelper.ExecuteNonQuery("UPDATE CHKLoan SET  LoanName ='" + strName + "',  InstallmentAmount ='" + FlInstallmentAmount + "', AccountNo ='" + "NA" + "'  WHERE (LoanCode = '" + IntLoanCode + "') and (DivisionCode ='" + strDivision + "') and (EmployeeNo ='" + StrEmpNo + "') and (StartMonth='" + IntFromMonth + "') and (StartYear='" + IntFromYear + "')", CommandType.Text);
        //}

        public void DeleteLoan()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[DeleteLog]([DeletedDate],[RefNo],[ReferenceTable],[EmpNo],[Amount],[Narration1],[Naration2],DeletedBy) VALUES(getdate(),'" + IntLoanCode + "','CHKLoan','" + StrEmpNo + "' ,'" + FlInstallmentAmount + "','" + IntDeductionGroup.ToString() + "','" + IntDeductCode.ToString() + "','" + StrUserId + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKLoan WHERE (LoanCode = '" + IntLoanCode + "')", CommandType.Text);
        }

        public Boolean IsProcessedEntry(Int32 intLoanId)
        {
            Boolean ProcessYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            SqlDataReader dataReader1;

            dt.Columns.Add("LoanCode");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT EmpDeductId FROM dbo.CHKEmpDeductions WHERE (DeductIdNo = '" + intLoanId + "')", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                    dataReader1 = SQLHelper.ExecuteReader("SELECT top 1 StartYear, StartMonth FROM dbo.CHKLoan WHERE (LoanCode = '" + intLoanId + "')", CommandType.Text);
                    while (dataReader1.Read())
                    {

                    }
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                ProcessYesNo = true;
            else
                ProcessYesNo = false;
            return ProcessYesNo;
        }

        public void TerminateLoan()
        {
            SQLHelper.ExecuteNonQuery("update dbo.CHKLoan set ClosedYesNo=1, TerminateReason='"+StrTerminateReason+"',TerminatedBy='"+FTSPayRollBL.User.StrUserName+"' where LoanCode='"+IntLoanCode+"'", CommandType.Text);
        }


        public void InsertLoanPayeeDetails()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKLoanPayeeDetails] ([DivisionID] ,[EmpNo] ,[DeductionGroupId] ,[DeductId] ,[StartYear] ,[StartMonth] ,[PayeeName] ,[PayeeAccount] ,[CreateDateTime] ,[UserID] ,PayeeAmount) VALUES ('" +strDivision + "' ,'" + strEmpNo + "' , '" + intDeductionGroup + "' ,'" + IntDeductCode + "' ,'" + IntFromYear + "' ,'" + IntFromMonth + "' ,'"+StrPayeeACName+"' ,'"+StrPayeeACNo+"' ,GETDATE() ,'"+FTSPayRollBL.User.StrUserName+"' ,'"+DecPayeeAmount+"')", CommandType.Text);
        }

        public Boolean IsPayeeExists()
        {
            Boolean boolExists = false;
            DataSet PayeeDs=new DataSet();
            PayeeDs=SQLHelper.FillDataSet("SELECT     DivisionID, EmpNo, DeductionGroupId, DeductId, StartYear, StartMonth FROM dbo.CHKLoanPayeeDetails WHERE     (DivisionID = '" +StrDivision + "') AND (EmpNo = '" + strEmpNo + "') AND (DeductionGroupId = '" + intDeductionGroup + "') AND (DeductId = '" + IntDeductCode + "') AND (StartYear = '" + IntFromYear + "') AND (StartMonth = '" + IntFromMonth + "')", CommandType.Text);
            if (PayeeDs.Tables[0].Rows.Count > 0)
            {
                boolExists = true;
            }
            PayeeDs.Dispose();
            return boolExists;
        }

        public void DeletePayeeDetails()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM dbo.CHKLoanPayeeDetails WHERE     (DivisionID = '" + strDivision + "') AND (EmpNo = '" + strEmpNo + "') AND (DeductionGroupId = '" + intDeductionGroup + "') AND (DeductId = '" + IntDeductCode + "') AND (StartYear = '" + IntFromYear + "') AND (StartMonth = '" + IntFromMonth + "') AND (PayeeAccount = '" + StrPayeeACNo + "')", CommandType.Text);
        }

        public void UpdateLoanPayeeDetails()
        {
            SQLHelper.ExecuteNonQuery("UPDATE CHKLoan SET DivisionCode ='" + strDivision + "', EmployeeNo ='" + StrEmpNo + "', LoanName ='" + strName + "', Principalamount ='" + flPrincipalAmount + "', NoofInstallments ='" + FlNoOfInstallments + "', InstallmentAmount ='" + FlInstallmentAmount + "', AccountNo ='" + AccountNo + "',LoanDate ='" + dtLoanDate + "', StartMonth='" + IntFromMonth + "',StartYear='" + IntFromYear + "'  WHERE (LoanCode = '" + IntLoanCode + "')", CommandType.Text);
        }
        
        public void DeleteLoanPayeeDetails()
        {
            if (IsPayeeExists())
            {
                SQLHelper.ExecuteNonQuery("DELETE FROM dbo.CHKLoanPayeeDetails WHERE     (DivisionID = '" + strDivision + "') AND (EmpNo = '" + strEmpNo + "')  AND (DeductionGroupId = '" + intDeductionGroup + "') AND (DeductId = '" + IntDeductCode + "')  AND (StartYear = '" + IntFromYear + "') AND (StartMonth = '" + IntFromMonth + "') ", CommandType.Text);
            }
           
        }

        public DataTable ListLoanPayeeDetails(String strDiv,String strEmp,Int32 intStYear, Int32 intStMonth,Int32 intDeductGroup,Int32 intDeduct)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PayeeName");
            dt.Columns.Add("PayeeAccountNo");
            dt.Columns.Add("PayeeAmount");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT     PayeeName, PayeeAccount, PayeeAmount FROM         dbo.CHKLoanPayeeDetails WHERE     (StartYear = '"+intStYear+"') AND (StartMonth = '"+intStMonth+"') AND (DivisionID = '"+strDiv+"') AND (EmpNo = '"+strEmp+"') AND (DeductionGroupId = '"+intDeductGroup+"') AND (DeductId = '"+intDeduct+"')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetDecimal(2);
                }
                

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

       
    }
}
