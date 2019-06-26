
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
   
    public class SkipDeductionsCls
    {
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }

        private int intYear;

        public int IntYear
        {
            get { return intYear; }
            set { intYear = value; }
        }

        private int intMonth;

        public int IntMonth
        {
            get { return intMonth; }
            set { intMonth = value; }
        }

        private String strEmpNo;

        public String StrEmpNo
        {
            get { return strEmpNo; }
            set { strEmpNo = value; }
        }

        private String strEmpName;

        public String StrEmpName
        {
            get { return strEmpName; }
            set { strEmpName = value; }
        }

        private int intDeductionCode;

        public int IntDeductionCode
        {
            get { return intDeductionCode; }
            set { intDeductionCode = value; }
        }

        

        private decimal decDeductAmount;

        public decimal DecDeductAmount
        {
            get { return decDeductAmount; }
            set { decDeductAmount = value; }
        }

        private int intDeductGroup;

        public int IntDeductGroup
        {
            get { return intDeductGroup; }
            set { intDeductGroup = value; }
        }

        private bool boolSkipped;

        public bool BoolSkipped
        {
            get { return boolSkipped; }
            set { boolSkipped = value; }
        }


        private String strUserID;

        public String StrUserID
        {
            get { return strUserID; }
            set { strUserID = value; }
        }

       


      //ublic DataTable ListSkippedDeduction( String divitions, String Years, string months, string deductgroup,string deductId)
       public DataTable ListSkippedDeduction()
       {
           DataTable dt = new DataTable();
           dt.Columns.Add("Skip", typeof(bool));//0
           dt.Columns.Add("EmpNo", typeof(string));
           dt.Columns.Add("Deduction", typeof(string));//2
           dt.Columns.Add("DeductAmount", typeof(Decimal));
           dt.Columns.Add("DeductGroup ", typeof(Int32));//4
           dt.Columns.Add("DeductCode", typeof(Int32));
           dt.Columns.Add("SkippedDate ", typeof(DateTime));//7
           
           DataRow dtrow;
           SqlDataReader dr;
           SqlDataReader drEmp;

           //dr = SQLHelper.ExecuteReader("SELECT    Skipped,EmpNo, EmpName, DeductionCode, DeductionAmount, deductionGroupCode,  UserID,  CreatedDateTime FROM         dbo.CHKSkippedDeduction WHERE     (Division = '" + StrDivision + "') AND (Month = " + IntMonth + ") AND (Year = " + IntYear + ") AND (deductionGroupCode = " + IntDeductGroup + ") AND (DeductionCode = " + IntDeductionCode + ")", CommandType.Text);
           dr = SQLHelper.ExecuteReader("SELECT        dbo.CHKSkippedDeduction.Skipped, dbo.CHKSkippedDeduction.EmpNo, dbo.CHKDeduction.ShortName,  dbo.CHKSkippedDeduction.DeductionAmount, dbo.CHKSkippedDeduction.deductionGroupCode, dbo.CHKSkippedDeduction.DeductionCode, dbo.CHKSkippedDeduction.CreatedDateTime FROM dbo.CHKSkippedDeduction INNER JOIN dbo.CHKDeduction ON dbo.CHKSkippedDeduction.DeductionCode = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKSkippedDeduction.Division = 'Amb') AND (dbo.CHKSkippedDeduction.Month = " + IntMonth + ") AND (dbo.CHKSkippedDeduction.Year = " + IntYear + ") AND (dbo.CHKSkippedDeduction.deductionGroupCode =  " + IntDeductGroup + ") AND  (dbo.CHKSkippedDeduction.DeductionCode = " + IntDeductionCode + ")", CommandType.Text);

           while (dr.Read())
           {
               dtrow = dt.NewRow();

               if (!dr.IsDBNull(0))//skkiped
               {
                   dtrow[0] = false;
               }

               if (!dr.IsDBNull(1))// empno
               {
                   dtrow[1] = dr.GetString(1);
               }

               if (!dr.IsDBNull(2))//deduction Short Code
               {
                   dtrow[2] = dr.GetString(2);
               }
             
               if (!dr.IsDBNull(3))//deduction amount
               {
                   dtrow[3] = dr.GetDecimal(3);
               }
               if (!dr.IsDBNull(4))//deduction group
               {
                   dtrow[4] = dr.GetInt32(4);
               }
               if (!dr.IsDBNull(5))//DeductionCode
               {
                   dtrow[5] = dr.GetInt32(5);
               }
               if (!dr.IsDBNull(6))//Date
               {
                   dtrow[6] = dr.GetDateTime(6);
               }
               dt.Rows.Add(dtrow); 
               
           }
          dr.Close();
           return dt;
       }



       public DataTable ListDeduction()
       {
           DataTable dt = new DataTable();
           dt.Columns.Add("Skip", typeof(Boolean));
           dt.Columns.Add("EmpNo", typeof(String));//1
           dt.Columns.Add("GroupName", typeof(String));//2
           dt.Columns.Add("Deduction", typeof(String));//3
           dt.Columns.Add("DeductionName", typeof(String));
           dt.Columns.Add("DeductionAmount", typeof(Decimal));
           dt.Columns.Add("DeductionGroupRef ", typeof(Int32));//6
           dt.Columns.Add("DeductionRef ", typeof(Int32));//7

           DataRow dtrow;
           SqlDataReader dr;
           SqlDataReader drEmp;

           //dr = SQLHelper.ExecuteReader("SELECT dbo.CHKFixedDeductions.EmpNo, dbo.CHKFixedDeductions.FixedDeductionId, dbo.CHKDeduction.DeductionName, dbo.CHKFixedDeductions.DeductAmount, dbo.CHKDeductionGroup.GroupName FROM         dbo.CHKFixedDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKFixedDeductions.DeductionGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE     (dbo.CHKDeductionGroup.DeductionGroupCode = " + deductionGroupCode + ") AND (dbo.CHKFixedDeductions.DivisionId = '" + Devision + "') AND (dbo.CHKFixedDeductions.DeductAmount > 0) AND  (dbo.CHKFixedDeductions.DeductionId = " + deductionCode + ") AND (dbo.CHKFixedDeductions.StartMonth = " + month + ") AND (dbo.CHKFixedDeductions.StartYear = " + year + " ) AND  EmpNo NOT IN(SELECT     EmpNo FROM   dbo.CHKSkippedDeduction) ", CommandType.Text);
           dr = SQLHelper.ExecuteReader("SELECT  dbo.CHKFixedDeductions.EmpNo, dbo.CHKDeductionGroup.GroupName, dbo.CHKDeduction.ShortName, dbo.CHKDeduction.DeductionName, dbo.CHKFixedDeductions.DeductAmount,  dbo.CHKDeductionGroup.DeductionGroupCode, dbo.CHKFixedDeductions.DeductionId, dbo.CHKFixedDeductions.FixedDeductionId FROM dbo.CHKFixedDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKFixedDeductions.DeductionGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKFixedDeductions.DivisionId = '"+StrDivision+"') AND (dbo.CHKFixedDeductions.DeductAmount > 0) AND (dbo.CHKDeductionGroup.DeductionGroupCode = '"+IntDeductGroup+"') AND (dbo.CHKFixedDeductions.DeductionId = '"+IntDeductionCode+"') AND (dbo.CHKFixedDeductions.BalanceAmount > 0) AND (dbo.CHKFixedDeductions.CloseYesNo = 0) ", CommandType.Text); 
           
           
           while (dr.Read())
           {
               dtrow = dt.NewRow();


               dtrow[0] = false;


               if (!dr.IsDBNull(0))// empno
               {
                   dtrow[1] = dr.GetString(0);

                   //drEmp = SQLHelper.ExecuteReader("SELECT  EMPName FROM dbo.EmployeeMaster WHERE (EmpNo = '" + dr.GetString(0) + "')", CommandType.Text);

                   //while (drEmp.Read())
                   //{
                   //    dtrow[2] = drEmp.GetString(0);//emp name
                   //}
               }
               if (!dr.IsDBNull(1))//deduction group
               {
                   dtrow[2] = dr.GetString(1);
               }
               if (!dr.IsDBNull(2))//deduction Short code
               {
                   dtrow[3] = dr.GetString(2);
               }
               if (!dr.IsDBNull(3))//deduction Name
               {
                   dtrow[4] = dr.GetString(3);
               }
               if (!dr.IsDBNull(4))//deduction amount
               {
                   dtrow[5] = dr.GetDecimal(4);
               }
               if (!dr.IsDBNull(5))//deduction Group Ref
               {
                   dtrow[6] = dr.GetInt32(5);
               }
               if (!dr.IsDBNull(6))//deduction Ref
               {
                   dtrow[7] = dr.GetInt32(6);
               }

               
               dt.Rows.Add(dtrow);
           }
           dr.Close();
           return dt;
       }



       //public void InsertSkipedDeduction()
       //{
       //    SQLHelper.ExecuteNonQuery(" INSERT INTO [dbo].[CHKSkippedDeduction] ([Division] ,[Year] ,[Month],[EmpNo],[EmpName],[DeductionCode],[DeductionAmount],[deductionGroupCode],[Skipped] ,[UserID]) VALUES ('"+StrDivision+"',"+IntYear+","+IntMonth+ ",'"+StrEmpNo+"','"+StrEmpName+"',"+IntDeductionCode+","+DecDeductAmount+",'"+IntDeductGroup+"','"+BoolSkipped+"','"+StrUserID+"')", CommandType.Text);
 
       //}

        public String InsertSkipDeduction()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Year", SqlDbType.Int, 4);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@month", SqlDbType.Int, 4);
            param.Value = IntMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@deductCode", SqlDbType.Int, 4);
            param.Value = IntDeductionCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@deductGroupCode", SqlDbType.Int, 4);
            param.Value = IntDeductGroup;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@decDeductAmount", SqlDbType.Float);
            param.Value = DecDeductAmount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Skip", SqlDbType.Bit);
            param.Value = BoolSkipped;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserID;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spSkipDeductionsInsert", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@status", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }

        public String DeleteSkipDeduction()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Year", SqlDbType.Int, 4);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@month", SqlDbType.Int, 4);
            param.Value = IntMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@deductCode", SqlDbType.Int, 4);
            param.Value = IntDeductionCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@deductGroupCode", SqlDbType.Int, 4);
            param.Value = IntDeductGroup;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@decDeductAmount", SqlDbType.Float);
            param.Value = DecDeductAmount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Skip", SqlDbType.Bit);
            param.Value = BoolSkipped;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserID;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spSkipDeductionsDelete", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@status", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }

       public void UpdateSkipDeduction()
       {
          // SQLHelper.ExecuteNonQuery("UPDATE CHKSkipDeduction SET Skip = 0, UserId ='" + FTSPayRollBL.User.StrUserName + "' WHERE (DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "') AND (Year = '" + intYear + "') AND (Month = '" + IntMonth + "') AND (DeductionCode = '" + IntDeduction + "')", CommandType.Text);
       }


       public void DeleteFromSkippedDeduction()
       {
           SQLHelper.ExecuteNonQuery("DELETE FROM [dbo].[CHKSkippedDeduction] WHERE(Division = '" + StrDivision + "') AND (Year = " + IntYear + ") AND (Month = " + IntMonth + ") AND (EmpNo = '" + StrEmpNo + "') AND (DeductionCode = " + IntDeductionCode + ") AND (deductionGroupCode = " + IntDeductGroup + ") ", CommandType.Text);
       }
    }
}
