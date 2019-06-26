using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
    public class EmployeeDeduction
    {
        private Int32 intEmpDeductId;

        public Int32 IntEmpDeductId
        {
            get { return intEmpDeductId; }
            set { intEmpDeductId = value; }
        }
        private Int32 intMonth;

        public Int32 IntMonth
        {
            get { return intMonth; }
            set { intMonth = value; }
        }

        private Int32 intYear;

        public Int32 IntYear
        {
            get { return intYear; }
            set { intYear = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private Int32 intCategory;

        public Int32 IntCategory
        {
            get { return intCategory; }
            set { intCategory = value; }
        }

        private Int32 intDeductGroupId;

        public Int32 IntDeductGroupId
        {
            get { return intDeductGroupId; }
            set { intDeductGroupId = value; }
        }
        private Int32 intNoOfMonths;

        public Int32 IntNoOfMonths
        {
            get { return intNoOfMonths; }
            set { intNoOfMonths = value; }
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
        private Decimal decDeductAmount;

        public Decimal DecDeductAmount
        {
            get { return decDeductAmount; }
            set { decDeductAmount = value; }
        }
        private Int32 intDeduction;

        public Int32 IntDeduction
        {
            get { return intDeduction; }
            set { intDeduction = value; }
        }
        private float flAmount;

        public float FlAmount
        {
            get { return flAmount; }
            set { flAmount = value; }
        }        
        private String strEmpNO;

        public String StrEmpNO
        {
            get { return strEmpNO; }
            set { strEmpNO = value;}
        }
        private String strName;

        public String StrName
        {
            get { return strName; }
            set { strName = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }
        private Boolean boolPeriodYesNo;

        public Boolean BoolPeriodYesNo
        {
            get { return boolPeriodYesNo; }
            set { boolPeriodYesNo = value; }
        }
        private DateTime dtPeriodFrom;

        public DateTime DtPeriodFrom
        {
            get { return dtPeriodFrom; }
            set { dtPeriodFrom = value; }
        }
        private DateTime dtPeriodTo;

        public DateTime DtPeriodTo
        {
            get { return dtPeriodTo; }
            set { dtPeriodTo = value; }
        }
        private Boolean boolAllCat;

        public Boolean BoolAllCat
        {
            get { return boolAllCat; }
            set { boolAllCat = value; }
        }
        private Int32 intFixedDeductId;

        public Int32 IntFixedDeductId
        {
            get { return intFixedDeductId; }
            set { intFixedDeductId = value; }
        }

        private Decimal decPrincipalAmount;

        public Decimal DecPrincipalAmount
        {
            get { return decPrincipalAmount; }
            set { decPrincipalAmount = value; }
        }
        private Decimal decBalanceAmount;

        public Decimal DecBalanceAmount
        {
            get { return decBalanceAmount; }
            set { decBalanceAmount = value; }
        }
        private Decimal decRecoveredAmount;

        public Decimal DecRecoveredAmount
        {
            get { return decRecoveredAmount; }
            set { decRecoveredAmount = value; }
        }
        private Decimal decRecoveredInstallments;

        public Decimal DecRecoveredInstallments
        {
            get { return decRecoveredInstallments; }
            set { decRecoveredInstallments = value; }
        }

        private Boolean boolCloseYesNo;

        public Boolean BoolCloseYesNo
        {
            get { return boolCloseYesNo; }
            set { boolCloseYesNo = value; }
        }
        private DateTime dtLastUpdate;

        public DateTime DtLastUpdate
        {
            get { return dtLastUpdate; }
            set { dtLastUpdate = value; }
        }
        private String Guarantor1;

        private String gurantor1;

        public String Gurantor1
        {
            get { return gurantor1; }
            set { gurantor1 = value; }
        }
        private String gurantor2;

        public String Gurantor2
        {
            get { return gurantor2; }
            set { gurantor2 = value; }
        }

        private String strGur1Div;

        public String StrGur1Div
        {
            get { return strGur1Div; }
            set { strGur1Div = value; }
        }
        private String strGur2Div;

        public String StrGur2Div
        {
            get { return strGur2Div; }
            set { strGur2Div = value; }
        }

        private String strReason;

        public String StrReason
        {
            get { return strReason; }
            set { strReason = value; }
        }
        private Boolean boolFixed;

        public Boolean BoolFixed
        {
            get { return boolFixed; }
            set { boolFixed = value; }
        }

        public String InsertEmployeeDeduction()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar,50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Category", SqlDbType.Int, 4);
            param.Value = IntCategory;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductMonth", SqlDbType.Int, 4);
            param.Value = IntMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductYear", SqlDbType.Int, 4);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PeriodYesNo", SqlDbType.Bit);
            param.Value =BoolPeriodYesNo ;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PeriodFrom", SqlDbType.DateTime);
            param.Value = DtPeriodFrom;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PeriodTo", SqlDbType.DateTime);
            param.Value = DtPeriodTo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductionCode", SqlDbType.Int, 4);
            param.Value = IntDeduction;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Amount", SqlDbType.Float);
            param.Value = FlAmount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AllYesNo", SqlDbType.Bit);
            param.Value = BoolAllCat;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNO;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = "admin";
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spInsertEmpDeductions", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }

        public String UpdateEmployeeDeduction()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@EmpDeductId", SqlDbType.Int,4);
            param.Value = IntEmpDeductId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Category", SqlDbType.Int, 4);
            param.Value = IntCategory;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductMonth", SqlDbType.Int, 4);
            param.Value = IntMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductYear", SqlDbType.Int, 4);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PeriodYesNo", SqlDbType.Bit);
            param.Value = BoolPeriodYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PeriodFrom", SqlDbType.DateTime);
            param.Value = DtPeriodFrom;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PeriodTo", SqlDbType.DateTime);
            param.Value = DtPeriodTo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductionCode", SqlDbType.Int, 4);
            param.Value = IntDeduction;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Amount", SqlDbType.Float);
            param.Value = FlAmount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AllYesNo", SqlDbType.Bit);
            param.Value = BoolAllCat;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNO;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = "admin";
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spUpdateEmpDeductions", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }

        public String DeleteEmployeeDeduction()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@EmpDeductId", SqlDbType.Int, 4);
            param.Value = IntEmpDeductId;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spDeleteEmpDeductions", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }
        //public String DeleteFixedDeduction()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter identityParam = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@EmpDeductId", SqlDbType.Int, 4);
        //    param.Value = IntEmpDeductId;
        //    paramList.Add(param);
        //    SqlCommand cmd = SQLHelper.CreateCommand("spDeleteEmpDeductions", CommandType.StoredProcedure, paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    return status;
        //}
        
        public DataTable ListEmpDeductions()
        {
            DataTable dt = new DataTable(); 
            SqlDataReader reader;
            DataRow drow;
            dt.Columns.Add("EmpDeductId");
            dt.Columns.Add("Division");
            dt.Columns.Add("Category");
            dt.Columns.Add("DeductMonth");
            dt.Columns.Add("DeductYear");
            dt.Columns.Add("PeriodYesNo");
            dt.Columns.Add("PeriodFrom");
            dt.Columns.Add("PeriodTo");
            dt.Columns.Add("DeductionCode");
            dt.Columns.Add("Amount");
            dt.Columns.Add("AllYesNo");
            dt.Columns.Add("EmpNo");
            reader = SQLHelper.ExecuteReader("SELECT EmpDeductId, Division, Category, DeductMonth, DeductYear, PeriodYesNo, PeriodFrom, PeriodTo, DeductionCode, Amount, AllYesNo, EmpNo FROM CHKEmpDeductions", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetInt32(3);
                }
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetBoolean(5);
                }
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    drow[7] = reader.GetDateTime(7);
                }
                if (!reader.IsDBNull(8))
                {
                    drow[8] = reader.GetInt32(8);
                }
                if (!reader.IsDBNull(9))
                {
                    drow[9] = reader.GetDecimal(9);
                }
                if (!reader.IsDBNull(10))
                {
                    drow[10] = reader.GetBoolean(10);
                }
                if (!reader.IsDBNull(11))
                {
                    drow[11] = reader.GetString(11).Trim();
                }
                dt.Rows.Add(drow);
            }
            return dt;
        }

        //fixed decution dtList
        public DataTable ListAllFixedDeductions(Int32 intYear,Int32 intMonth,String strDiv,Int32 intDGroup,Int32 intDeduct)
        {
            DataTable dt = new DataTable();
            SqlDataReader reader;
            DataRow drow;

            dt.Columns.Add("EMPNO");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("Amount");
            dt.Columns.Add("NoOfMonths");
            dt.Columns.Add("FromYear");
            dt.Columns.Add("FromMonth");
            dt.Columns.Add("Group");
            dt.Columns.Add("GroupId");
            dt.Columns.Add("DeductId");
            dt.Columns.Add("Division");
            dt.Columns.Add("Ref#");
            dt.Columns.Add("GurantorDiv1");
            dt.Columns.Add("Guarantor1");
            dt.Columns.Add("GurantorDiv2");
            dt.Columns.Add("Guarantor2");
            dt.Columns.Add("Balance");

            reader = SQLHelper.ExecuteReader("SELECT  dbo.CHKFixedDeductions.EmpNo, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount, dbo.CHKFixedDeductions.NoOfMonths, dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKFixedDeductions.DeductionGroupId, dbo.CHKFixedDeductions.DeductionId, dbo.CHKFixedDeductions.DivisionId, dbo.CHKFixedDeductions.FixedDeductionId,dbo.CHKFixedDeductions.Guarantor1Div, dbo.CHKFixedDeductions.Guarantor1, dbo.CHKFixedDeductions.Guarantor2Div, dbo.CHKFixedDeductions.Guarantor2,dbo.CHKFixedDeductions.BalanceAmount FROM dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN  dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKFixedDeductions.StartYear = '" + intYear + "') AND (dbo.CHKFixedDeductions.StartMonth = '" + intMonth + "') AND (dbo.CHKFixedDeductions.DeductionGroupId = '" + intDGroup + "') AND (dbo.CHKFixedDeductions.DeductionId = '" + intDeduct + "') AND (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.CloseYesNo=0) AND  (OldEntryYesNo = 0) ORDER BY dbo.CHKFixedDeductions.FixedDeductionId DESC", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetInt32(3);
                }
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetInt32(5);
                }
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetString(6).Trim();
                }
                if (!reader.IsDBNull(7))
                {
                    drow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    drow[8] = reader.GetInt32(8);
                }
                if (!reader.IsDBNull(9))
                {
                    drow[9] = reader.GetString(9).Trim();
                }
                if (!reader.IsDBNull(10))
                {
                    drow[10] = reader.GetInt32(10);
                }
                if (!reader.IsDBNull(11))
                {
                    drow[11] = reader.GetString(11).Trim();
                }
                if (!reader.IsDBNull(12))
                {
                    drow[12] = reader.GetString(12).Trim();
                }
                if (!reader.IsDBNull(13))
                {
                    drow[13] = reader.GetString(13).Trim();
                }
                if (!reader.IsDBNull(14))
                {
                    drow[14] = reader.GetString(14).Trim();
                }
                if (!reader.IsDBNull(15))
                {
                    drow[15] = reader.GetDecimal(15);
                }

                dt.Rows.Add(drow);
            }
            return dt;
        }
        public void InsertFixedDeductions()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKFixedDeductions( DeductionGroupId, DivisionId, DeductionId, EmpNo, DeductAmount, NoOfMonths, StartMonth,StartYear,UserId,PrincipalAmount,BalanceAmount,RecoveredAmount,RecoveredInstallments,CloseYesNo,Guarantor1Div,Guarantor1,Guarantor2Div,Guarantor2,Fixed)VALUES ('" + IntDeductGroupId + "','" + strDivision + "','" + intDeduction + "','" + strEmpNO + "','" + decDeductAmount + "','" + IntNoOfMonths + "','" + IntFromMonth + "','" + IntFromYear + "','" + StrUserId + "','" + DecPrincipalAmount + "','" + DecBalanceAmount + "','" + DecRecoveredAmount + "','" + decRecoveredInstallments + "','" + BoolCloseYesNo + "','"+StrGur1Div+"','" + Gurantor1 + "','"+StrGur2Div+"','" + Gurantor2 + "','"+BoolFixed+"')", CommandType.Text);
        }

        public void UpdateFixedDeductions()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES (getdate() ,'" + IntFixedDeductId + "' ,'CHKFixedDeductions' ,'" + StrDivision + "' ,'" + StrEmpNO + "' ,'" + DecDeductAmount.ToString() + "' ,'" + IntDeductGroupId.ToString() + "' ,'" + IntDeduction.ToString() + "' ,'" + IntFromYear.ToString() + "' ,'" + IntFromMonth.ToString() + "' ,'" + StrUserId + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE CHKFixedDeductions SET DeductAmount ='" + DecDeductAmount + "', UserId='" + StrUserId + "',UpdatedBy='" + StrUserId + "',UpdatedDate=getdate() WHERE (FixedDeductionId='" + intFixedDeductId + "') AND (Fixed='"+BoolFixed+"')", CommandType.Text);
        }

        public void UpdateFixedDeductions(Boolean BoolUpdateWithBalance)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES (getdate() ,'" + IntFixedDeductId + "' ,'CHKFixedDeductions' ,'" + StrDivision + "' ,'" + StrEmpNO + "' ,'" + DecDeductAmount.ToString() + "' ,'" + IntDeductGroupId.ToString() + "' ,'" + IntDeduction.ToString() + "' ,'" + IntFromYear.ToString() + "' ,'" + IntFromMonth.ToString() + "' ,'" + StrUserId + "')", CommandType.Text);
            if(BoolUpdateWithBalance==true)
            SQLHelper.ExecuteNonQuery("UPDATE CHKFixedDeductions SET DeductAmount ='" + DecDeductAmount + "', NoOfMonths='"+IntNoOfMonths+"', PrincipalAmount='"+DecPrincipalAmount+"', RecoveredAmount=0, RecoveredInstallments=0, BalanceAmount='"+DecBalanceAmount+"', UserId='" + StrUserId + "',UpdatedBy='" + StrUserId + "',UpdatedDate=getdate() WHERE (FixedDeductionId='" + intFixedDeductId + "') AND (Fixed='" + BoolFixed + "')", CommandType.Text);
            else
            SQLHelper.ExecuteNonQuery("UPDATE CHKFixedDeductions SET DeductAmount ='" + DecDeductAmount + "', UserId='" + StrUserId + "',UpdatedBy='" + StrUserId + "',UpdatedDate=getdate() WHERE (FixedDeductionId='" + intFixedDeductId + "') AND (Fixed='" + BoolFixed + "')", CommandType.Text);

        }

        public void DeleteFixedDeductions()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[DeleteLog]([DeletedDate],[RefNo],[ReferenceTable],[EmpNo],[Amount],[Narration1],[Naration2],DeletedBy) VALUES(getdate(),'" + IntFixedDeductId + "','CHKFixedDeductions','"+StrEmpNO+"' ,'"+DecDeductAmount+"','"+IntDeductGroupId.ToString()+"','"+IntDeduction.ToString()+"','"+StrUserId+"')",CommandType.Text);
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKFixedDeductions WHERE (FixedDeductionId  = '" + IntFixedDeductId + "') ", CommandType.Text);
        }

        public void TerminateFixedDeductions()
        {
            SQLHelper.ExecuteNonQuery("UPDATE CHKFixedDeductions SET CloseYesNo =1, TerminatedBy='" + StrUserId + "', TerminateReson='"+StrReason+"' WHERE (FixedDeductionId='" + intFixedDeductId + "')", CommandType.Text);
        }
        
        // EmpDeductId, Division, Category, DeductMonth, DeductYear, PeriodYesNo, PeriodFrom, PeriodTo,
        //DeductionCode, Amount, AllYesNo, EmpNo, CreateDateTime, 
        //    UserId

        public DataSet viewDicvisionWiseDeduction()
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT EmployeeMaster.EmpNo, EmployeeMaster.EMPName, CHKEmpDeductions.DeductYear, CHKEmpDeductions.DeductMonth, CHKDeduction.ShortName,CHKEmpDeductions.Amount, CHKDeductionGroup.ShortName AS GroupId FROM EmployeeMaster INNER JOIN CHKEmpDeductions ON EmployeeMaster.EmpNo = CHKEmpDeductions.EmpNo INNER JOIN CHKDeduction ON CHKEmpDeductions.DeductId = CHKDeduction.DeductionCode INNER JOIN CHKDeductionGroup ON CHKDeduction.DeductionGroupCode = CHKDeductionGroup.DeductionGroupCode WHERE (EmployeeMaster.DivisionID = '" + strDivision + "')", CommandType.Text);
            return ds;
        }

        public Boolean IsProcessedEntry(Int32 intFixedId)
        {
            Boolean ProcessYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("LoanCode");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT EmpDeductId FROM dbo.CHKEmpDeductions WHERE (DeductIdNo = '" + intFixedId + "')", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
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
        //tempory coding on 20120214
        public Boolean IsStartMonthProcessedEntry(Int32 PYear,Int32 PMonth)
        {
            Boolean ProcessYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("PYear");
            dt.Columns.Add("PMonth");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  ProcessYear, ProcessMonth FROM dbo.CHKProcessDetails WHERE (ProcessYear = '"+PYear+"') AND (ProcessMonth = '"+PMonth+"') AND (ProcessedYesNo = 1)", CommandType.Text);
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

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                ProcessYesNo = true;
            else
                ProcessYesNo = false;
            return ProcessYesNo;
        }

        public Decimal GetFixedDeductionTotal(Int32 intYear,Int32 intMonth,Int32 DeductID,Int32 intDeductGroup,String strDivsion)
        {
            SqlDataReader dataReader;
            Decimal decTotAmount = 0;
            dataReader = SQLHelper.ExecuteReader("SELECT SUM(DeductAmount) AS TotalAmount FROM dbo.CHKFixedDeductions WHERE (DivisionId = '" + strDivsion + "') AND (DeductionGroupId = '" + intDeductGroup + "') AND (DeductionId = '" + DeductID + "') AND (StartYear = '" + intYear + "') AND (StartMonth = '" + intMonth + "') ", CommandType.Text);
            while (dataReader.Read())
            {               
                if (!dataReader.IsDBNull(0))
                {
                    decTotAmount = dataReader.GetDecimal(0);
                }
               
            }
            dataReader.Close();
            return decTotAmount;
        }

        public Decimal GetLoanDeductionTotal(Int32 intYear, Int32 intMonth, Int32 DeductID, Int32 intDeductGroup, String strDivsion)
        {
            SqlDataReader dataReader;
            Decimal decTotAmount = 0;
            //dataReader = SQLHelper.ExecuteReader("SELECT SUM(DeductAmount) AS TotalAmount FROM dbo.CHKFixedDeductions WHERE (DivisionId = '" + strDivsion + "') AND (DeductionGroupId = '" + intDeductGroup + "') AND (DeductionId = '" + DeductID + "') AND (StartYear = '" + intYear + "') AND (StartMonth = '" + intMonth + "') ", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT SUM(InstallmentAmount) AS Expr1 FROM dbo.CHKLoan WHERE (DivisionCode = '" + strDivsion + "') AND (StartYear = '" + intYear + "') AND (StartMonth = '" + intMonth + "') AND (DeductionGroup = '" + intDeductGroup + "') AND (DeductionCode = '" + DeductID + "') ", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decTotAmount = dataReader.GetDecimal(0);
                }

            }
            dataReader.Close();
            return decTotAmount;
        }

        public Decimal GetRFTDeductionTotal(Int32 intYear, Int32 intMonth, Int32 DeductID, Int32 intDeductGroup, String strDivsion)
        {
            SqlDataReader dataReader;
            Decimal decTotAmount = 0;
            //dataReader = SQLHelper.ExecuteReader("SELECT SUM(DeductAmount) AS TotalAmount FROM dbo.CHKFixedDeductions WHERE (DivisionId = '" + strDivsion + "') AND (DeductionGroupId = '" + intDeductGroup + "') AND (DeductionId = '" + DeductID + "') AND (StartYear = '" + intYear + "') AND (StartMonth = '" + intMonth + "') ", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT     SUM(RFTDeductAmount) AS Expr1 FROM dbo.CHKRFTDeductions WHERE (Division = '" + strDivsion + "') AND (DYear = '" + intYear + "') AND (DMonth = '" + intMonth + "') AND (DeductGroupId = '" + intDeductGroup + "') AND (DeductId = '" + DeductID + "') ", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decTotAmount = dataReader.GetDecimal(0);
                }

            }
            dataReader.Close();
            return decTotAmount;
        }

        public void DirectPaymentFixedDeductions(DateTime dtDPDate, Int32 intDPYear, Int32 intDPMonth, String strDPDiv, String strDPEmp, Int32 intDPDeduct, Int32 intDPDeductGroup, Decimal decDPAmount, String strDPRefNo, String strDPReason)
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.CHKFixedDeductions SET DirectPayment=DirectPayment+'" + decDPAmount + "',BalanceAmount=BalanceAmount-'" + decDPAmount + "' WHERE (StartYear = '" + intDPYear + "') AND (StartMonth = '" + intDPMonth + "') AND (DivisionId = '" + strDPDiv + "') AND (EmpNo = '" + strDPEmp + "')  AND (DeductionId = '" + intDPDeduct + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKFixedDeductDirectPayment ([DateOfPayment],[DivisionID],[EmpNo],[DeductGroupId],[DeductId],[RefNo],[Amount],[Reason],[CreateDateTime],[UserID]) VALUES('" + dtDPDate + "','" + strDPDiv + "','" + strDPEmp + "','" + intDPDeductGroup + "','" + intDPDeduct + "','" + strDPRefNo + "','" + decDPAmount + "','" + strDPReason + "',getdate(),'" + User.StrUserName + "')", CommandType.Text);
        }

        

        public DataTable GetBalanceAmounts(String strDiv, String strEmp, Int32 intGroup, Int32 intDeduct)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Balance"));
            dt.Columns.Add(new DataColumn("Year"));
            dt.Columns.Add(new DataColumn("Month"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT top 1 BalanceAmount, StartYear, StartMonth FROM dbo.CHKFixedDeductions WHERE (DivisionId = '" + strDiv + "')  AND (EmpNo = '" + strEmp + "') AND (DeductionGroupId = '" + intGroup + "') AND (DeductionId = '" + intDeduct + "') AND (BalanceAmount > 0) AND (CloseYesNo = 0)  ", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetDecimal(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetInt32(2);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public Boolean IsRecoveredDeduction(Int32 PYear, Int32 PMonth,Int32 intDeductGroup,Int32 intDeductId,String strDiv, String strEmp)
        {
            Boolean boolIsRecovered=false;
            DataSet dsRecoveries = new DataSet();
            dsRecoveries = SQLHelper.FillDataSet("SELECT     StartYear, StartMonth, DivisionId, EmpNo, DeductionGroupId, DeductionId, RecoveredAmount FROM dbo.CHKFixedDeductions WHERE     (StartYear = '"+intYear+"') AND (StartMonth = '"+intMonth+"') AND (DivisionId = '"+strDiv+"') AND (EmpNo = '"+strEmp+"') AND (DeductionGroupId = '"+intDeductGroup+"') AND (DeductionId = '"+intDeductId+"') AND (RecoveredAmount > 0)", CommandType.Text);
            if(dsRecoveries.Tables[0].Rows.Count>0)
                boolIsRecovered = true;
            else
                boolIsRecovered = false;
            return boolIsRecovered;
        }

        public Boolean IsRecoveredLoanDeduction(Int32 PYear, Int32 PMonth, Int32 intDeductGroup, Int32 intDeductId, String strDiv, String strEmp)
        {
            Boolean boolIsRecovered = false;
            DataSet dsRecoveries = new DataSet();
            dsRecoveries = SQLHelper.FillDataSet("SELECT     StartYear, StartMonth, DivisionCode, EmployeeNo, DeductionGroup, DeductionCode, RecoveredAmount FROM dbo.CHKLoan WHERE (RecoveredAmount > 0) AND (StartYear = '"+PYear+"') AND (StartMonth = '"+PMonth+"') AND (DivisionCode = '"+strDiv+"') AND (EmployeeNo = '"+strEmp+"') AND (DeductionGroup = '"+intDeductGroup+"') AND  (DeductionCode = '"+intDeductId+"')", CommandType.Text);
            if (dsRecoveries.Tables[0].Rows.Count > 0)
                boolIsRecovered = true;
            else
                boolIsRecovered = false;
            return boolIsRecovered;
        }

        public DataTable ListOutstandingFixedDeductions( String strDiv, Int32 intDGroup, Int32 intDeduct, String strEmp)
        {
            DataTable dt = new DataTable();
            SqlDataReader reader;
            DataRow drow;

            dt.Columns.Add("EMPNO");
            dt.Columns.Add("FromYear");
            dt.Columns.Add("FromMonth");
            dt.Columns.Add("Group");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("Amount");
            dt.Columns.Add("NoOfMonths");
            dt.Columns.Add("Balance");
            dt.Columns.Add("GroupId");
            dt.Columns.Add("DeductId");
            dt.Columns.Add("Ref#");
            dt.Columns.Add("Division");
            dt.Columns.Add("DeductionType");

            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo, dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth,  dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount,  dbo.CHKFixedDeductions.NoOfMonths, dbo.CHKFixedDeductions.BalanceAmount, dbo.CHKFixedDeductions.DeductionGroupId, dbo.CHKFixedDeductions.DeductionId,  dbo.CHKFixedDeductions.FixedDeductionId, dbo.CHKFixedDeductions.DivisionId,'FIXED' FROM         dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKFixedDeductions.DeductionGroupId = '" + intDGroup + "') AND (dbo.CHKFixedDeductions.DeductionId = '" + intDeduct + "') AND  (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.CloseYesNo = 0) AND (dbo.CHKFixedDeductions.BalanceAmount > 0) AND  (dbo.CHKFixedDeductions.EmpNo like '" + strEmp + "') AND (dbo.CHKFixedDeductions.CloseYesNo = 0) ORDER BY dbo.CHKFixedDeductions.FixedDeductionId DESC", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetString(3);
                }
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetString(4);
                }
                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetDecimal(5);
                }
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetInt32(6);
                }
                if (!reader.IsDBNull(7))
                {
                    drow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    drow[8] = reader.GetInt32(8);
                }
                if (!reader.IsDBNull(9))
                {
                    drow[9] = reader.GetInt32(9);
                }
                if (!reader.IsDBNull(10))
                {
                    drow[10] = reader.GetInt32(10);
                }                
                if (!reader.IsDBNull(11))
                {
                    drow[11] = reader.GetString(11);
                }
                if (!reader.IsDBNull(12))
                {
                    drow[12] = reader.GetString(12);
                }

                dt.Rows.Add(drow);
            }
            return dt;
        }

        public Decimal GetOutstandingFixedDeductionTotal(String strDiv, Int32 intDGroup, Int32 intDeduct,String strEmp)
        {
            SqlDataReader fixedTotalReader;
            Decimal decFixedTotal = 0;
            fixedTotalReader = SQLHelper.ExecuteReader("SELECT  sum(dbo.CHKFixedDeductions.DeductAmount) FROM dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKFixedDeductions.DeductionGroupId = '" + intDGroup + "') AND (dbo.CHKFixedDeductions.DeductionId = '" + intDeduct + "') AND  (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.CloseYesNo = 0) AND (dbo.CHKFixedDeductions.BalanceAmount > 0) AND (dbo.CHKFixedDeductions.CloseYesNo = 0) AND  (dbo.CHKFixedDeductions.EmpNo like '" + strEmp + "') ", CommandType.Text);
            while (fixedTotalReader.Read())
            {
                if (!fixedTotalReader.IsDBNull(0))
                {
                    decFixedTotal = fixedTotalReader.GetDecimal(0);
                }
            }
            return decFixedTotal;
        }

        public DataTable ListOutstandingLoanDeductions(String strDiv, Int32 intDGroup, Int32 intDeduct,String strEmp)
        {
            DataTable dt = new DataTable();
            SqlDataReader reader;
            DataRow drow;

            dt.Columns.Add("EMPNO");
            dt.Columns.Add("FromYear");
            dt.Columns.Add("FromMonth");
            dt.Columns.Add("Group");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("Amount");
            dt.Columns.Add("NoOfMonths");
            dt.Columns.Add("Balance");
            dt.Columns.Add("GroupId");
            dt.Columns.Add("DeductId");
            dt.Columns.Add("Ref#");
            dt.Columns.Add("Division");
            dt.Columns.Add("DeductionType");

            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKDeductionGroup.ShortName,  dbo.CHKDeduction.ShortName AS Expr1, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.BalanceAmount,  dbo.CHKLoan.DeductionGroup, dbo.CHKLoan.DeductionCode, dbo.CHKLoan.LoanCode, dbo.CHKLoan.DivisionCode,'LOAN' FROM         dbo.CHKDeduction INNER JOIN dbo.CHKLoan ON dbo.CHKDeduction.DeductionCode = dbo.CHKLoan.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode WHERE     (dbo.CHKLoan.DeductionGroup = '" + intDGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND  (dbo.CHKLoan.EmployeeNo like '" + strEmp + "') AND (dbo.CHKLoan.ClosedYesNo = 0) AND (dbo.CHKLoan.BalanceAmount > 0) ORDER BY dbo.CHKLoan.EmployeeNo, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetString(3);
                }
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetString(4);
                }
                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetDecimal(5);
                }
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    drow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    drow[8] = reader.GetInt32(8);
                }
                if (!reader.IsDBNull(9))
                {
                    drow[9] = reader.GetInt32(9);
                }
                if (!reader.IsDBNull(10))
                {
                    drow[10] = reader.GetInt32(10);
                }
                if (!reader.IsDBNull(11))
                {
                    drow[11] = reader.GetString(11);
                }
                if (!reader.IsDBNull(12))
                {
                    drow[12] = reader.GetString(12);
                }

                dt.Rows.Add(drow);
            }
            return dt;
        }

        public Decimal GetOutstandingLoanDeductionTotal(String strDiv, Int32 intDGroup, Int32 intDeduct,String strEmp)
        {
            SqlDataReader LoanTotalReader;
            Decimal decLoanTotal = 0;
            //LoanTotalReader = SQLHelper.ExecuteReader("SELECT  sum(dbo.CHKLoan.InstallmentAmount) FROM dbo.CHKDeduction INNER JOIN dbo.CHKLoan ON dbo.CHKDeduction.DeductionCode = dbo.CHKLoan.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode WHERE     (dbo.CHKLoan.DeductionGroup = '" + intDGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND  (dbo.CHKLoan.EmployeeNo like '" + strEmp + "') AND (dbo.CHKLoan.ClosedYesNo = 0) ", CommandType.Text);
            LoanTotalReader = SQLHelper.ExecuteReader("SELECT  SUM(dbo.CHKLoan.InstallmentAmount) AS Expr1 FROM            dbo.CHKDeduction INNER JOIN dbo.CHKLoan ON dbo.CHKDeduction.DeductionCode = dbo.CHKLoan.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode WHERE        (dbo.CHKLoan.DeductionGroup = '" + intDGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND  (dbo.CHKLoan.EmployeeNo LIKE '" + strEmp + "') AND (dbo.CHKLoan.ClosedYesNo = 0) AND (dbo.CHKLoan.BalanceAmount > 0)", CommandType.Text);
            while (LoanTotalReader.Read())
            {
                if (!LoanTotalReader.IsDBNull(0))
                {
                    decLoanTotal = LoanTotalReader.GetDecimal(0);
                }
            }
            return decLoanTotal;
        }

        public DataTable ListOutstandingRFTDeductions(String strDiv, Int32 intDGroup, Int32 intDeduct,String strEmp,Int32 intYear,Int32 intMonth)
        {
            DataTable dt = new DataTable();
            SqlDataReader reader;
            DataRow drow;
            dt.Columns.Add("EMPNO");//0
            dt.Columns.Add("FromYear");
            dt.Columns.Add("FromMonth");
            dt.Columns.Add("Group");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("Amount");
            dt.Columns.Add("NoOfMonths");
            dt.Columns.Add("Balance");
            dt.Columns.Add("GroupId");
            dt.Columns.Add("DeductId");
            dt.Columns.Add("Ref#");
            dt.Columns.Add("Division");
            dt.Columns.Add("DeductionType");

            reader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKRFTDeductions.EmpNo, dbo.CHKRFTDeductions.DYear, dbo.CHKRFTDeductions.DMonth, dbo.CHKDeductionGroup.GroupName,  dbo.CHKDeduction.ShortName, dbo.CHKRFTDeductions.RFTDeductAmount, 1 AS Expr1, dbo.CHKRFTDeductions.RFTDeductAmount AS Expr2,  dbo.CHKRFTDeductions.DeductGroupId, dbo.CHKRFTDeductions.DeductId, dbo.CHKRFTDeductions.RFTDeductId, dbo.CHKRFTDeductions.Division,'RFT' FROM         dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE   (dbo.CHKRFTDeductions.DYear = '"+intYear+"') AND  (dbo.CHKRFTDeductions.DMonth = '"+intMonth+"') AND (dbo.CHKRFTDeductions.Division = '" + strDiv + "') AND (dbo.CHKRFTDeductions.DeductGroupId = '" + intDGroup + "') AND (dbo.CHKRFTDeductions.DeductId = '" + intDeduct + "') AND (dbo.CHKRFTDeductions.EmpNo like '"+strEmp+"')", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetString(3);
                }
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetString(4);
                }
                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetDecimal(5);
                }
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetInt32(6);
                }
                if (!reader.IsDBNull(7))
                {
                    drow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    drow[8] = reader.GetInt32(8);
                }
                if (!reader.IsDBNull(9))
                {
                    drow[9] = reader.GetInt32(9);
                }
                if (!reader.IsDBNull(10))
                {
                    drow[10] = reader.GetInt32(10);
                }
                if (!reader.IsDBNull(11))
                {
                    drow[11] = reader.GetString(11);
                }
                if (!reader.IsDBNull(12))
                {
                    drow[12] = reader.GetString(12);
                }

                dt.Rows.Add(drow);
            }
            return dt;
        }

        public Decimal GetOutstandingRFTDeductionTotal(String strDiv, Int32 intDGroup, Int32 intDeduct,String strEmp,Int32 intYear,Int32 intMonth)
        {
            SqlDataReader RFTTotalReader;
            Decimal decRFTTotal = 0;
            RFTTotalReader = SQLHelper.ExecuteReader("SELECT  SUM(dbo.CHKRFTDeductions.RFTDeductAmount) AS Expr1 FROM dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKRFTDeductions.Division = '" + strDiv + "') AND (dbo.CHKRFTDeductions.DeductGroupId = '" + intDGroup + "') AND  (dbo.CHKRFTDeductions.DeductId = '" + intDeduct + "') AND (dbo.CHKRFTDeductions.EmpNo like '" + strEmp + "') AND (dbo.CHKRFTDeductions.DYear = '" + intYear + "') AND  (dbo.CHKRFTDeductions.DMonth = '" + intMonth + "')  ", CommandType.Text);
            while (RFTTotalReader.Read())
            {
                if (!RFTTotalReader.IsDBNull(0))
                {
                    decRFTTotal = RFTTotalReader.GetDecimal(0);
                }
            }
            return decRFTTotal;
        }
        

        public DataTable ListOutstandingRFTDeductionsDetails(String strDiv, Int32 intDGroup, Int32 intDeduct, Int32 intYear,Int32 intMonth,String strEmp)
        {
            DataTable dt = new DataTable();
            SqlDataReader reader;
            DataRow drow;

            dt.Columns.Add("Rate");//0
            dt.Columns.Add("Qty");

            reader = SQLHelper.ExecuteReader("SELECT RFTRate, RFTQty FROM dbo.CHKRFTDeductions WHERE (Division = '"+strDiv+"') AND (DeductGroupId = '"+intDGroup+"') AND (DeductId = '"+intDeduct+"') AND (DYear = '"+intYear+"') AND (DMonth = '"+intMonth+"') AND  (EmpNo = '"+strEmp+"')", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetDecimal(0);
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetDecimal(1);
                }

                dt.Rows.Add(drow);
            }
            return dt;
        }

        public DataTable GetLoanBalanceAmounts(String strDiv, String strEmp, Int32 intGroup, Int32 intDeduct)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Balance"));
            dt.Columns.Add(new DataColumn("Year"));
            dt.Columns.Add(new DataColumn("Month"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     BalanceAmount, StartYear, StartMonth   FROM dbo.CHKLoan WHERE (BalanceAmount > 0) AND (ClosedYesNo = 0) AND (DivisionCode = '" + strDiv + "') AND (EmployeeNo = '" + strEmp + "') AND (DeductionGroup = '" + intGroup + "') AND (DeductionCode = '" + intDeduct + "') ", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetDecimal(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetInt32(2);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        

        public DataSet ListEmployeewiseOutstandingDeductions(String strDiv,Int32 intYear,Int32 intMonth)
        {
            DataSet ds = new DataSet();
            //ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.CHKLoan.EmployeeNo as EmpNO, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKDeductionGroup.ShortName  AS GroupShortName,  dbo.CHKDeduction.ShortName , dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.BalanceAmount,  dbo.CHKLoan.DeductionGroup as DeductionGroupId, dbo.CHKLoan.DeductionCode as DeductionId, dbo.CHKLoan.LoanCode  as Ref, dbo.CHKLoan.DivisionCode as Division,'LOAN' as Type FROM dbo.CHKDeduction INNER JOIN dbo.CHKLoan ON dbo.CHKDeduction.DeductionCode = dbo.CHKLoan.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode  WHERE (dbo.CHKLoan.DivisionCode = '" + strDiv + "')  AND (dbo.CHKLoan.ClosedYesNo = 0) AND (dbo.CHKLoan.BalanceAmount > 0)  union SELECT TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo as EmpNO, dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth,  dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount as InstallmentAmount,  dbo.CHKFixedDeductions.NoOfMonths as NoofInstallments, dbo.CHKFixedDeductions.BalanceAmount as BalanceAmount, dbo.CHKFixedDeductions.DeductionGroupId as DeductionGroupId, dbo.CHKFixedDeductions.DeductionId as DeductionId,  dbo.CHKFixedDeductions.FixedDeductionId as Ref, dbo.CHKFixedDeductions.DivisionId as Division,'FIXED' as Type FROM         dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode WHERE      (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.CloseYesNo = 0) AND (dbo.CHKFixedDeductions.BalanceAmount > 0)  AND (dbo.CHKFixedDeductions.CloseYesNo = 0)  union SELECT     TOP (100) PERCENT dbo.CHKRFTDeductions.EmpNo as EmpNO, dbo.CHKRFTDeductions.DYear as StartYear, dbo.CHKRFTDeductions.DMonth as StartMonth, dbo.CHKDeductionGroup.ShortName AS GroupShortName,  dbo.CHKDeduction.ShortName, dbo.CHKRFTDeductions.RFTDeductAmount  as InstallmentAmount, 1  as NoofInstallments, dbo.CHKRFTDeductions.RFTDeductAmount as BalanceAmount,  dbo.CHKRFTDeductions.DeductGroupId as DeductionGroupId, dbo.CHKRFTDeductions.DeductId as DeductionId, dbo.CHKRFTDeductions.RFTDeductId  as Ref, dbo.CHKRFTDeductions.Division as Division,'RFT' as Type  FROM         dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode  WHERE   (dbo.CHKRFTDeductions.DYear = '" + intYear + "') AND  (dbo.CHKRFTDeductions.DMonth = '" + intMonth + "') AND (dbo.CHKRFTDeductions.Division = '" + strDiv + "')  order by EmpNO,DeductionGroupId,ShortName  ", CommandType.Text);
            ds = SQLHelper.FillDataSet("  SELECT        TOP (100) PERCENT dbo.CHKLoan.EmployeeNo AS EmpNO, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth,  dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.NoofInstallments,  dbo.CHKLoan.BalanceAmount, dbo.CHKLoan.DeductionGroup AS DeductionGroupId, dbo.CHKLoan.DeductionCode AS DeductionId, dbo.CHKLoan.LoanCode AS Ref,  dbo.CHKLoan.DivisionCode AS Division, 'LOAN' AS Type, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM            dbo.CHKDeduction INNER JOIN dbo.CHKLoan ON dbo.CHKDeduction.DeductionCode = dbo.CHKLoan.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKLoan.DivisionCode = dbo.EmployeeMaster.DivisionID AND dbo.CHKLoan.EmployeeNo = dbo.EmployeeMaster.EmpNo WHERE        (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND (dbo.CHKLoan.ClosedYesNo = 0) AND (dbo.CHKLoan.BalanceAmount > 0) union SELECT        TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo AS EmpNO, dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth,  dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount AS InstallmentAmount,  dbo.CHKFixedDeductions.NoOfMonths AS NoofInstallments, dbo.CHKFixedDeductions.BalanceAmount, dbo.CHKFixedDeductions.DeductionGroupId,  dbo.CHKFixedDeductions.DeductionId, dbo.CHKFixedDeductions.FixedDeductionId AS Ref, dbo.CHKFixedDeductions.DivisionId AS Division, 'FIXED' AS Type,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM            dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKFixedDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKFixedDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE        (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.CloseYesNo = 0) AND (dbo.CHKFixedDeductions.BalanceAmount > 0) AND  (dbo.CHKFixedDeductions.CloseYesNo = 0)  union SELECT        TOP (100) PERCENT dbo.CHKRFTDeductions.EmpNo AS EmpNO, dbo.CHKRFTDeductions.DYear AS StartYear, dbo.CHKRFTDeductions.DMonth AS StartMonth,  dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKRFTDeductions.RFTDeductAmount AS InstallmentAmount,  1 AS NoofInstallments, dbo.CHKRFTDeductions.RFTDeductAmount AS BalanceAmount, dbo.CHKRFTDeductions.DeductGroupId AS DeductionGroupId,  dbo.CHKRFTDeductions.DeductId AS DeductionId, dbo.CHKRFTDeductions.RFTDeductId AS Ref, dbo.CHKRFTDeductions.Division, 'RFT' AS Type,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM            dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKRFTDeductions.Division = dbo.EmployeeMaster.DivisionID AND  dbo.CHKRFTDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE        (dbo.CHKRFTDeductions.DYear = '" + intYear + "') AND (dbo.CHKRFTDeductions.DMonth = '" + intMonth + "') AND (dbo.CHKRFTDeductions.Division = '" + strDiv + "') ORDER BY EmpNO, DeductionGroupId, GroupShortName ", CommandType.Text);
            return ds;
            //DataTable dt = new DataTable();
            //SqlDataReader reader;
            //DataRow drow;

            //dt.Columns.Add("EMPNO");
            //dt.Columns.Add("FromYear");
            //dt.Columns.Add("FromMonth");
            //dt.Columns.Add("Group");
            //dt.Columns.Add("DeductCode");
            //dt.Columns.Add("Amount");
            //dt.Columns.Add("NoOfMonths");
            //dt.Columns.Add("Balance");
            //dt.Columns.Add("GroupId");
            //dt.Columns.Add("DeductId");
            //dt.Columns.Add("Ref#");
            //dt.Columns.Add("Division");
            //dt.Columns.Add("DeductionType");

            //reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKLoan.EmployeeNo as EmpNO, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKDeductionGroup.ShortName  AS GroupShortName,  dbo.CHKDeduction.ShortName , dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.BalanceAmount,  dbo.CHKLoan.DeductionGroup as DeductionGroupId, dbo.CHKLoan.DeductionCode as DeductionId, dbo.CHKLoan.LoanCode  as Ref, dbo.CHKLoan.DivisionCode as Division,'LOAN' as Type FROM dbo.CHKDeduction INNER JOIN dbo.CHKLoan ON dbo.CHKDeduction.DeductionCode = dbo.CHKLoan.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode  WHERE (dbo.CHKLoan.DivisionCode = '" + strDiv + "')  AND (dbo.CHKLoan.ClosedYesNo = 0) AND (dbo.CHKLoan.BalanceAmount > 0)  union SELECT TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo as EmpNO, dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth,  dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount as InstallmentAmount,  dbo.CHKFixedDeductions.NoOfMonths as NoofInstallments, dbo.CHKFixedDeductions.BalanceAmount as BalanceAmount, dbo.CHKFixedDeductions.DeductionGroupId as DeductionGroupId, dbo.CHKFixedDeductions.DeductionId as DeductionId,  dbo.CHKFixedDeductions.FixedDeductionId as Ref, dbo.CHKFixedDeductions.DivisionId as Division,'FIXED' as Type FROM         dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode WHERE      (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.CloseYesNo = 0) AND (dbo.CHKFixedDeductions.BalanceAmount > 0)  AND (dbo.CHKFixedDeductions.CloseYesNo = 0)  union SELECT     TOP (100) PERCENT dbo.CHKRFTDeductions.EmpNo as EmpNO, dbo.CHKRFTDeductions.DYear as StartYear, dbo.CHKRFTDeductions.DMonth as StartMonth, dbo.CHKDeductionGroup.ShortName AS GroupShortName,  dbo.CHKDeduction.ShortName, dbo.CHKRFTDeductions.RFTDeductAmount  as InstallmentAmount, 1  as NoofInstallments, dbo.CHKRFTDeductions.RFTDeductAmount as BalanceAmount,  dbo.CHKRFTDeductions.DeductGroupId as DeductionGroupId, dbo.CHKRFTDeductions.DeductId as DeductionId, dbo.CHKRFTDeductions.RFTDeductId  as Ref, dbo.CHKRFTDeductions.Division as Division,'RFT' as Type  FROM         dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode  WHERE   (dbo.CHKRFTDeductions.DYear = '" + intYear + "') AND  (dbo.CHKRFTDeductions.DMonth = '" + intMonth + "') AND (dbo.CHKRFTDeductions.Division = '" + strDiv + "')  order by EmpNO,DeductionGroupId,ShortName  ", CommandType.Text);
            //drow = dt.NewRow();
            //while (reader.Read())
            //{
            //    drow = dt.NewRow();
            //    if (!reader.IsDBNull(0))
            //    {
            //        drow[0] = reader.GetString(0).Trim();
            //    }
            //    if (!reader.IsDBNull(1))
            //    {
            //        drow[1] = reader.GetInt32(1);
            //    }
            //    if (!reader.IsDBNull(2))
            //    {
            //        drow[2] = reader.GetInt32(2);
            //    }
            //    if (!reader.IsDBNull(3))
            //    {
            //        drow[3] = reader.GetString(3);
            //    }
            //    if (!reader.IsDBNull(4))
            //    {
            //        drow[4] = reader.GetString(4);
            //    }
            //    if (!reader.IsDBNull(5))
            //    {
            //        drow[5] = reader.GetDecimal(5);
            //    }
            //    if (!reader.IsDBNull(6))
            //    {
            //        drow[6] = reader.GetInt32(6);
            //    }
            //    if (!reader.IsDBNull(7))
            //    {
            //        drow[7] = reader.GetDecimal(7);
            //    }
            //    if (!reader.IsDBNull(8))
            //    {
            //        drow[8] = reader.GetInt32(8);
            //    }
            //    if (!reader.IsDBNull(9))
            //    {
            //        drow[9] = reader.GetInt32(9);
            //    }
            //    if (!reader.IsDBNull(10))
            //    {
            //        drow[10] = reader.GetInt32(10);
            //    }
            //    if (!reader.IsDBNull(11))
            //    {
            //        drow[11] = reader.GetString(11);
            //    }
            //    if (!reader.IsDBNull(12))
            //    {
            //        drow[12] = reader.GetString(12);
            //    }

            //    dt.Rows.Add(drow);
            //}
            //return dt;
        }

        public DataSet GetMonthPRINorms(String strDiv, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsPriNorm = new DataSet();
            dsPriNorm = SQLHelper.FillDataSet("SELECT DivisionId, NormDate, FieldId, MalePlkNorm, FemalePlkNorm, MalePRINorm, FemalePRINorm FROM dbo.ChkFieldWiseNorm WHERE        (DivisionId LIKE '" + strDiv + "') AND (NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102))", CommandType.Text);
            return dsPriNorm;
        }
        public DataSet GetMonthPRINormsForCrossTab(String strDiv, DateTime dtFrom, DateTime dtTo,String StrCrop)
        {
            DataSet dsPriNorm = new DataSet();
            //dsPriNorm = SQLHelper.FillDataSet("SELECT        DivisionId, NormDate, FieldId, MalePlkNorm, 'Over Kilo Norm' as Type, 'Male' as type2 FROM            dbo.ChkFieldWiseNorm WHERE        (DivisionId LIKE '" + strDiv + "') AND (NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) union SELECT       DivisionId, NormDate, FieldId, FemalePlkNorm, 'Over Kilo Norm' as Type, 'Female' as type2 FROM            dbo.ChkFieldWiseNorm WHERE        (DivisionId LIKE '" + strDiv + "') AND (NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) union SELECT       DivisionId, NormDate, FieldId, MalePRINorm, 'PRI Norm' as Type, 'Male' as type2 FROM            dbo.ChkFieldWiseNorm WHERE        (DivisionId LIKE '" + strDiv + "') AND (NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) union SELECT       DivisionId, NormDate, FieldId, FemalePRINorm, 'PRI Norm' as Type, 'Female' as type2 FROM            dbo.ChkFieldWiseNorm WHERE        (DivisionId LIKE '" + strDiv + "') AND (NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) ", CommandType.Text);
            dsPriNorm = SQLHelper.FillDataSet("SELECT        dbo.ChkFieldWiseNorm.DivisionId, dbo.ChkFieldWiseNorm.NormDate, dbo.ChkFieldWiseNorm.FieldId, dbo.ChkFieldWiseNorm.MalePlkNorm,  'Over Kilo Norm' AS Type, 'Male' AS type2, dbo.EstateField.CropType FROM            dbo.ChkFieldWiseNorm INNER JOIN dbo.EstateField ON dbo.ChkFieldWiseNorm.DivisionId = dbo.EstateField.DivisionID AND dbo.ChkFieldWiseNorm.FieldId = dbo.EstateField.FieldID WHERE        (dbo.ChkFieldWiseNorm.DivisionId LIKE '" + strDiv + "') AND (dbo.ChkFieldWiseNorm.NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND  CONVERT(DATETIME, '" + dtTo + "', 102)) AND (dbo.EstateField.CropType = '" + StrCrop + "')  union   SELECT        dbo.ChkFieldWiseNorm.DivisionId, dbo.ChkFieldWiseNorm.NormDate, dbo.ChkFieldWiseNorm.FieldId, dbo.ChkFieldWiseNorm.FemalePlkNorm,  'Over Kilo Norm' AS Type, 'Female' AS type2, dbo.EstateField.CropType FROM            dbo.ChkFieldWiseNorm INNER JOIN dbo.EstateField ON dbo.ChkFieldWiseNorm.DivisionId = dbo.EstateField.DivisionID AND dbo.ChkFieldWiseNorm.FieldId = dbo.EstateField.FieldID WHERE        (dbo.ChkFieldWiseNorm.DivisionId LIKE '" + strDiv + "') AND (dbo.ChkFieldWiseNorm.NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND  CONVERT(DATETIME, '" + dtTo + "', 102)) AND (dbo.EstateField.CropType = '" + StrCrop + "') union   SELECT        dbo.ChkFieldWiseNorm.DivisionId, dbo.ChkFieldWiseNorm.NormDate, dbo.ChkFieldWiseNorm.FieldId, dbo.ChkFieldWiseNorm.MalePRINorm, 'PRI Norm' AS Type,  'Male' AS type2, dbo.EstateField.CropType FROM            dbo.ChkFieldWiseNorm INNER JOIN dbo.EstateField ON dbo.ChkFieldWiseNorm.DivisionId = dbo.EstateField.DivisionID AND dbo.ChkFieldWiseNorm.FieldId = dbo.EstateField.FieldID WHERE        (dbo.ChkFieldWiseNorm.DivisionId LIKE '" + strDiv + "') AND (dbo.ChkFieldWiseNorm.NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND  CONVERT(DATETIME, '" + dtTo + "', 102)) AND (dbo.EstateField.CropType = '" + StrCrop + "') union   SELECT        dbo.ChkFieldWiseNorm.DivisionId, dbo.ChkFieldWiseNorm.NormDate, dbo.ChkFieldWiseNorm.FieldId, dbo.ChkFieldWiseNorm.FemalePRINorm, 'PRI Norm' AS Type, 'Female' AS type2, dbo.EstateField.CropType FROM            dbo.ChkFieldWiseNorm INNER JOIN dbo.EstateField ON dbo.ChkFieldWiseNorm.DivisionId = dbo.EstateField.DivisionID AND dbo.ChkFieldWiseNorm.FieldId = dbo.EstateField.FieldID WHERE        (dbo.ChkFieldWiseNorm.DivisionId LIKE '" + strDiv + "') AND (dbo.ChkFieldWiseNorm.NormDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND  CONVERT(DATETIME, '" + dtTo + "', 102)) AND (dbo.EstateField.CropType = '" + StrCrop + "')", CommandType.Text);
            return dsPriNorm;
        }

    }
}
