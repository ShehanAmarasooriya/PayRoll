using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EmployeeUnion
    {
        private String strUnionID;
        public String StrUnionID
        {
            get { return strUnionID; }
            set { strUnionID = value; }
        }

        private String strUnionName;
        public String StrUnionName
        {
            get { return strUnionName; }
            set { strUnionName = value; }
        }

        private float flMonthlyAmt;

        public float FlMonthlyAmt
        {
            get { return flMonthlyAmt; }
            set { flMonthlyAmt = value; }
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

        private String strDeductionGroup;

        public String StrDeductionGroup
        {
            get { return strDeductionGroup; }
            set { strDeductionGroup = value; }
        }

        private String strDeduction;

        public String StrDeduction
        {
            get { return strDeduction; }
            set { strDeduction = value; }
        }

        public DataTable ListAllUnion()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("UnionID"));
            dt.Columns.Add(new DataColumn("UnionCode"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("MonthlyAmount"));
            
            //dt.Columns.Add(new DataColumn("Id"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  UnionID, Deduction, CreateDateTime, UserId,MonthlyAmount FROM   EmployeeUnions", CommandType.Text);

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
                    dtrow[2] = dataReader.GetDateTime(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDecimal(4);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListAllUnionMaster()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DeductionGroup"));
            dt.Columns.Add(new DataColumn("Deduction"));
            dt.Columns.Add(new DataColumn("MonthlyAmount"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("UserID")); 

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  DeductionGroup, Deduction, MonthlyAmount, CreateDateTime, UserId FROM   EmployeeUnions", CommandType.Text);

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
                    dtrow[3] = dataReader.GetDateTime(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }               

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }


        public void InsertUnion()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeUnions   (UnionCode, UnionName, UserId,MonthlyAmount)VALUES ('" + strUnionID + "','" + strUnionName + "','" + FTSPayRollBL.User.StrUserName + "','" + FlMonthlyAmt + "' )", CommandType.Text);
        }

        public String InsertUnionMaster()
        {
            if (IsAvaialble(StrDeduction))
            {
                return "Exists";
            }
            else
            {
                SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeUnions   (DeductionGroup, Deduction, MonthlyAmount, UserId)VALUES ('" + StrDeductionGroup + "','" + StrDeduction + "','" + FlMonthlyAmt + "','" + FTSPayRollBL.User.StrUserName + "' )", CommandType.Text);
                return "OK";
            }
        }

        public Boolean IsAvaialble(String strUnion)
        {
            DataSet ds = SQLHelper.FillDataSet("select Deduction from EmployeeUnions where (Deduction = '" + strUnion + "')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public void UpdateUnion()
        {
            SQLHelper.ExecuteNonQuery("UPDATE    EmployeeUnions SET UnionName ='" + strUnionName + "', UserId ='" + FTSPayRollBL.User.StrUserName + "',MonthlyAmount='"+FlMonthlyAmt+"' WHERE     (UnionId = '" + strUnionID + "')", CommandType.Text);
        }

        public void UpdateUnionMaster()
        {
            SQLHelper.ExecuteNonQuery("UPDATE    EmployeeUnions SET DeductionGroup ='" + StrDeductionGroup + "', Deduction ='" + StrDeduction + "',MonthlyAmount='" + FlMonthlyAmt + "', UserId= '" + FTSPayRollBL.User.StrUserName + "' WHERE    (Deduction = '" + StrDeduction + "') AND (DeductionGroup = '" + StrDeductionGroup + "')", CommandType.Text);
        }

        public void DeleteUnion()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM EmployeeUnions  WHERE     (UnionId ='" + strUnionID + "')", CommandType.Text);
        }

        public void DeleteUnionMaster()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM EmployeeUnions  WHERE     (DeductionGroup ='" + StrDeductionGroup + "') AND (Deduction ='" + StrDeduction + "')", CommandType.Text);
        }


        public DataTable getUnionID()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("UnionID"));
            dt.Columns.Add(new DataColumn("UnionName"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
           
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  UnionId, UnionName, CreateDateTime FROM EmployeeUnions", CommandType.Text);

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
                    dtrow[2] = dataReader.GetDateTime(2);
                }
              
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public void InsertUnionToEmployee()
        {
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET UnionNameCode = '" + StrUnionID + "' WHERE DivisionId='"+StrDivision+"' and EmpNo='"+StrEmpNo+"' ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[EmployeeUnionAssign] ([Date] ,[DivisionId] ,[EmpNo] ,[UnionCode] ,[Status] ,[CreateDateTime] ,[UserId])  VALUES (GETDATE(),'"+StrDivision+"','"+StrEmpNo+"','"+StrUnionID+"','ADD',GETDATE(),'"+FTSPayRollBL.User.StrUserName+"')",CommandType.Text);
        }

        public void UpdateUnionToEmployee()
        {
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET UnionNameCode = '" + StrUnionID + "' WHERE DivisionId='" + StrDivision + "' and EmpNo='" + StrEmpNo + "' ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[EmployeeUnionAssign] ([Date] ,[DivisionId] ,[EmpNo] ,[UnionCode] ,[Status] ,[CreateDateTime] ,[UserId])  VALUES (GETDATE(),'" + StrDivision + "','" + StrEmpNo + "','" + StrUnionID + "','UPDATE',GETDATE(),'" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }

        public void DeleteUnionToEmployee()
        {
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET UnionNameCode = '' WHERE DivisionId='" + StrDivision + "' and EmpNo='" + StrEmpNo + "' ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[EmployeeUnionAssign] ([Date] ,[DivisionId] ,[EmpNo] ,[UnionCode] ,[Status] ,[CreateDateTime] ,[UserId])  VALUES (GETDATE(),'" + StrDivision + "','" + StrEmpNo + "','','DELETE',GETDATE(),'" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }


    }
}
