using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class AdditionMaster
    {
        private String strAddition;

        public String StrAddition
        {
            get { return strAddition; }
            set { strAddition = value; }
        }

        private String strShortName;

        public String StrShortName
        {
            get { return strShortName; }
            set { strShortName = value; }
        }

        private Int32 intPriority;

        public Int32 IntPriority
        {
            get { return intPriority; }
            set { intPriority = value; }
        }

        private String strAccCode;

        public String StrAccCode
        {
            get { return strAccCode; }
            set { strAccCode = value; }
        }

        private String strEmpName;

        public String StrEmpName
        {
            get { return strEmpName; }
            set { strEmpName = value; }
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

        private Int32 intAllowanceCode;

        public Int32 IntAllowanceCode
        {
            get { return intAllowanceCode; }
            set { intAllowanceCode = value; }
        }

        private String strDivisionId;

        public String StrDivisionId
        {
            get { return strDivisionId; }
            set { strDivisionId = value; }
        }

        private Int32 cataegoryId;
        public Int32 CataegoryId
        {
            get { return cataegoryId; }
            set { cataegoryId = value; }
        }


        private Decimal decAmount;

        public Decimal DecAmount
        {
            get { return decAmount; }
            set { decAmount = value; }
        }

        private Boolean boolEpfPayble;

        public Boolean BoolEpfPayble
        {
            get { return boolEpfPayble; }
            set { boolEpfPayble = value; }
        }

        public DataTable ListAllAdditions()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AdditionID");
            dt.Columns.Add("AdditionName");
            dt.Columns.Add("AdditionShortName");
            dt.Columns.Add("Priority");
            dt.Columns.Add("AccountCode");
            dt.Columns.Add("UserID");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("EPFPayble");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT AdditionId,AdditionName, AdditionShortName, priority, AccountCode, UserId, CreateDateTime,EPFPayble FROM CHKAddition", CommandType.Text);
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
                    dtRow[2] = reader.GetString(2).Trim();
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetInt32(3);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetInt32(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetBoolean(7);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public void InsertAddition()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKAddition (AdditionName, AdditionShortName, priority, AccountCode, UserId,EPFPayble)VALUES ('" + strAddition + "','" + strShortName + "','" + intPriority + "','" + StrAccCode + "','" + FTSPayRollBL.User.StrUserName + "','"+BoolEpfPayble+"')", CommandType.Text);
        }

        public void UpdateAddition()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[UpdatedUser],[Narration5])  SELECT     GETDATE() AS Expr1, AdditionId, 'AdditionMaster' AS Expr2, 'NA' AS Expr3, 'NA' AS Expr4, 'NA' AS Expr5, AdditionShortName, AdditionName,  '" + FTSPayRollBL.User.StrUserName + "' AS Expr6, 'Updated' AS Expr7 FROM dbo.CHKAddition WHERE     (AdditionName = '" + strAddition + "') ", CommandType.Text);            
            SQLHelper.ExecuteNonQuery("UPDATE CHKAddition SET AdditionShortName ='" + strShortName + "', priority ='" + intPriority + "', AccountCode ='" + StrAccCode + "', UserId ='" + FTSPayRollBL.User.StrUserName + "',EPFPayble='" + BoolEpfPayble + "' WHERE (AdditionName = '" + strAddition + "')", CommandType.Text);
        }

        public void DeleteAddition()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[UpdatedUser],[Narration5])  SELECT     GETDATE() AS Expr1, AdditionId, 'AdditionMaster' AS Expr2, 'NA' AS Expr3, 'NA' AS Expr4, 'NA' AS Expr5, AdditionShortName, AdditionName,  '" + FTSPayRollBL.User.StrUserName + "' AS Expr6, 'Deleted' AS Expr7 FROM dbo.CHKAddition WHERE     (AdditionName = '" + strAddition + "') ", CommandType.Text);            
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKAddition WHERE (AdditionName = '"+strAddition+"')", CommandType.Text);
        }

        public DataTable ListAllAllowances()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AutoID");
            dt.Columns.Add("AllowanceID");
            dt.Columns.Add("DivisionId");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("AllowanceAmount");
            dt.Columns.Add("Month");
            dt.Columns.Add("Year");
            dt.Columns.Add("UserID");
            dt.Columns.Add("CreateDateTime");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT AutoID, AllowanceID, DivisionId, Category, EmpNo, AllowanceAmount, Month, Year, UserId, CreateDateTime FROM CHKAllowance", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();

                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetString(2).Trim();
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetInt32(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetDecimal(5);
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetInt32(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8).Trim();
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetDateTime(9);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public void InsertAllowances()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKAllowance  (AllowanceID, DivisionId, Category, EmpNo, AllowanceAmount, Month, Year, UserId) VALUES('"+IntAllowanceCode+"','"+strDivisionId+"','"+cataegoryId+"','"+strEmpName+"','"+DecAmount+"','"+intMonth+"','"+intYear+"','"+FTSPayRollBL.User.StrUserName+"')", CommandType.Text);
        }

        public void UpdateAllowances()
        {
            SQLHelper.ExecuteNonQuery("UPDATE    CHKAllowance SET DivisionId ='"+strDivisionId+"', Category ='"+cataegoryId+"', EmpNo ='"+strEmpName+"', AllowanceAmount ='"+DecAmount+"', Month ='"+intMonth+"', Year ='"+intYear+"', UserId ='"+FTSPayRollBL.User.StrUserName+"' WHERE (AllowanceID = '"+IntAllowanceCode+"')", CommandType.Text);
        }

        public void DeleteAllowances()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKAllowance WHERE  (AllowanceID = '" + IntAllowanceCode + "')", CommandType.Text);
        }
        public Boolean IsEPFPaybleAdditionAllowed()
        {
            Boolean boolOk = false;
            SqlDataReader reader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'EPFAdditions')",CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetString(0).ToUpper().Equals("AVAILABLE"))
                    {
                        boolOk = true;
                    }                    
                }
            }
            reader.Close();
            return boolOk;
        }

    }
}
