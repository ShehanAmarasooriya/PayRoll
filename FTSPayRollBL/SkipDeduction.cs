using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
   public class SkipDeduction
    {
        private String strDivisionId;

        public String StrDivisionId
        {
            get { return strDivisionId; }
            set { strDivisionId = value; }
        }

        private String strEmpNo;

        public String StrEmpNo
        {
            get { return strEmpNo; }
            set { strEmpNo = value; }
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

        private Int32 intDeductionGroupId;

        public Int32 IntDeductionGroupId
        {
            get { return intDeductionGroupId; }
            set { intDeductionGroupId = value; }
        }

        private Int32 intDeduction;

        public Int32 IntDeduction
        {
            get { return intDeduction; }
            set { intDeduction = value; }
        }

        private Boolean bitSkip;

        public Boolean BitSkip
        {
            get { return bitSkip; }
            set { bitSkip = value; }
        }


       public DataTable ListSkipByEmpID()
       {
           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("DivisionID"));
           dt.Columns.Add(new DataColumn("EmpNo"));
           dt.Columns.Add(new DataColumn("Year"));
           dt.Columns.Add(new DataColumn("Month"));
           dt.Columns.Add(new DataColumn("DeductionGroupCode"));
           dt.Columns.Add(new DataColumn("DeductionCode"));
           dt.Columns.Add(new DataColumn("Status"));
           dt.Columns.Add(new DataColumn("UserId"));

           DataRow dtrow;
           SqlDataReader dataReader;
           dtrow = dt.NewRow();
           dataReader = SQLHelper.ExecuteReader("SELECT DivisionId, EmpNo, Year, Month, DeductionGroupCode, DeductionCode, Skip, UserId FROM CHKSkipDeduction WHERE(DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "')", CommandType.Text);

           while (dataReader.Read())
           {
               dtrow = dt.NewRow();

               if (!dataReader.IsDBNull(0))
               {
                   dtrow[0] = dataReader.GetString(0);
               }

               if (!dataReader.IsDBNull(1))
               {
                   dtrow[1] = dataReader.GetString(1);
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
                   dtrow[4] = dataReader.GetInt32(4);
               }
               if (!dataReader.IsDBNull(5))
               {
                   dtrow[5] = dataReader.GetInt32(5);
               }
               if (!dataReader.IsDBNull(6))
               {
                   dtrow[6] = dataReader.GetBoolean(6);
               }
               if (!dataReader.IsDBNull(7))
               {
                   dtrow[7] = dataReader.GetString(7);
               }

               dt.Rows.Add(dtrow);
           }
           dataReader.Close();
           return dt;
       }

       public void InsertSkipDeduction()
       {
           try
           {
               SQLHelper.ExecuteNonQuery("INSERT INTO CHKSkipDeduction(DivisionId, EmpNo, Year, Month, DeductionGroupCode, DeductionCode, Skip, UserId) VALUES ('" + strDivisionId + "','" + StrEmpNo + "','" + IntYear + "','" + IntMonth + "','" + IntDeductionGroupId + "','" + IntDeduction + "','" + bitSkip + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
           }
           catch
           {
               SQLHelper.ExecuteNonQuery("UPDATE CHKSkipDeduction SET Skip = 1, UserId ='" + FTSPayRollBL.User.StrUserName + "' WHERE (DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "') AND (Year = '" + intYear + "') AND (Month = '" + IntMonth + "') AND (DeductionCode = '" + IntDeduction + "')", CommandType.Text);
           }
       }

       public void UpdateSkipDeduction()
       {
           SQLHelper.ExecuteNonQuery("UPDATE CHKSkipDeduction SET Skip = 0, UserId ='" + FTSPayRollBL.User.StrUserName + "' WHERE (DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "') AND (Year = '" + intYear + "') AND (Month = '" + IntMonth + "') AND (DeductionCode = '" + IntDeduction + "')", CommandType.Text);
       }


    }
}
