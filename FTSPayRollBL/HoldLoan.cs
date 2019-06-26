using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
   public  class HoldLoan
    {
        private String strDivisionId;

        public String StrDivisionId
        {
            get { return strDivisionId; }
            set { strDivisionId = value; }
        }

        private String StrEmpNo;

        public String StrEmpNo1
        {
            get { return StrEmpNo; }
            set { StrEmpNo = value; }
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

        private Boolean bitHoldLoan;

        public Boolean BitHoldLoan
        {
            get { return bitHoldLoan; }
            set { bitHoldLoan = value; }
        }

        private Int32 intLoanId;

        public Int32 IntLoanId
        {
            get { return intLoanId; }
            set { intLoanId = value; }
        }

        private String strReason;
        public String StrReason
        {
            get { return strReason; }
            set { strReason = value; }
        }

        private Boolean bitTermination;

        public Boolean BitTermination
        {
            get { return bitTermination; }
            set { bitTermination = value; }
        }

       public DataTable ListHoldByEmpID()
       {
           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("DivisionID"));
           dt.Columns.Add(new DataColumn("EmpNo"));
           dt.Columns.Add(new DataColumn("Year"));
           dt.Columns.Add(new DataColumn("Month"));
           dt.Columns.Add(new DataColumn("DeductionGroupCode"));
           dt.Columns.Add(new DataColumn("DeductionCode"));
           dt.Columns.Add(new DataColumn("LoanID"));
           dt.Columns.Add(new DataColumn("Status"));
           dt.Columns.Add(new DataColumn("UserId"));

           DataRow dtrow;
           SqlDataReader dataReader;
           dtrow = dt.NewRow();
           dataReader = SQLHelper.ExecuteReader("SELECT DivisionId, EmpNo, Year, Month, DeductionGroupCode, DeductionCode,LoanId,Hold, UserId FROM CHKLoanHold WHERE(DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "')", CommandType.Text);

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
                   dtrow[6] = dataReader.GetInt32(6);
               }
               if (!dataReader.IsDBNull(7))
               {
                   dtrow[7] = dataReader.GetBoolean(7);
               }
               if (!dataReader.IsDBNull(8))
               {
                   dtrow[8] = dataReader.GetString(8);
               }

               dt.Rows.Add(dtrow);
           }
           dataReader.Close();
           return dt;
       }

       public void InsertLoanHoldDeduction()
       {
           try
           {
               SQLHelper.ExecuteNonQuery("INSERT INTO CHKLoanHold (DivisionId, EmpNo, Year, Month, DeductionGroupCode, DeductionCode, Hold, UserId) VALUES ('" + strDivisionId + "','" + StrEmpNo + "','" + IntYear + "','" + IntMonth + "','" + IntDeductionGroupId + "','" + IntDeduction + "','" + bitHoldLoan + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
           }
           catch
           {
               SQLHelper.ExecuteNonQuery("UPDATE CHKLoanHold SET Hold = 1, UserId ='" + FTSPayRollBL.User.StrUserName + "' WHERE (DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "') AND (Year = '" + intYear + "') AND (Month = '" + IntMonth + "') AND (DeductionCode = '" + IntDeduction + "')AND (DeductionGroupCode='" + IntDeductionGroupId + "')", CommandType.Text);
       
           }
       }

       public void DeleteLoanHoldDeduction()
       {
           try
           {
               SQLHelper.ExecuteNonQuery("DELETE FROM CHKLoanHold WHERE (DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "') AND (Year = '" + intYear + "') AND (Month = '" + IntMonth + "') AND (DeductionCode = '" + IntDeduction + "')AND (DeductionGroupCode='" + IntDeductionGroupId + "')", CommandType.Text);
           }
           catch
           {
           }
       }

       public void UpdateLoanHoldDeduction()
       {
           SQLHelper.ExecuteNonQuery("UPDATE CHKLoanHold SET Hold = 0, UserId ='" + FTSPayRollBL.User.StrUserName + "' WHERE (DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "') AND (Year = '" + intYear + "') AND (Month = '" + IntMonth + "') AND (DeductionCode = '" + IntDeduction + "')", CommandType.Text);
       }

       public void InsertLoanTerminatin()
       {
           SQLHelper.ExecuteNonQuery("INSERT INTO CHKLoanTermination  (DivisionId, EmpNo, DeductionGroupCode, DeductionCode, Terminate, Reason, UserId,LoanId) VALUES ('" + strDivisionId + "','" + StrEmpNo + "','" + IntDeductionGroupId + "','" + IntDeduction + "','" + bitTermination + "','"+StrReason+"','" + FTSPayRollBL.User.StrUserName + "','" + IntLoanId + "')", CommandType.Text);
       }

       public void UpdateLoanTerminatin()
       {
           SQLHelper.ExecuteNonQuery("UPDATE CHKLoanTermination SET Terminate  = 0, UserId ='" + FTSPayRollBL.User.StrUserName + "',Reason='"+StrReason+"' WHERE (DivisionId = '" + strDivisionId + "') AND (EmpNo = '" + StrEmpNo + "') AND (DeductionCode = '" + IntDeduction + "')AND (LoanId='" + IntLoanId + "')", CommandType.Text);
       }

       public DataTable ListLoanTermination()
       {
           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("DivisionID"));
           dt.Columns.Add(new DataColumn("EmpNo"));
           dt.Columns.Add(new DataColumn("DeductionGroupCode"));
           dt.Columns.Add(new DataColumn("DeductionCode"));
           dt.Columns.Add(new DataColumn("LoanID"));
           dt.Columns.Add(new DataColumn("Status"));
           dt.Columns.Add(new DataColumn("Reason"));
           dt.Columns.Add(new DataColumn("UserId"));
           dt.Columns.Add(new DataColumn("CreateDateTime"));

           DataRow dtrow;
           SqlDataReader reader;
           dtrow = dt.NewRow();
           reader = SQLHelper.ExecuteReader("SELECT DivisionId, EmpNo, DeductionGroupCode, DeductionCode, LoanId, Terminate, Reason, UserId, CreateDateTime FROM CHKLoanTermination", CommandType.Text);

           while (reader.Read())
           {
               dtrow = dt.NewRow();

               if (!reader.IsDBNull(0))
               {
                   dtrow[0] = reader.GetString(0);
               }

               if (!reader.IsDBNull(1))
               {
                   dtrow[1] = reader.GetString(1);
               }

               if (!reader.IsDBNull(2))
               {
                   dtrow[2] = reader.GetInt32(2);
               }
               if (!reader.IsDBNull(3))
               {
                   dtrow[3] = reader.GetInt32(3);
               }
               if (!reader.IsDBNull(4))
               {
                   dtrow[4] = reader.GetInt32(4);
               }
               if (!reader.IsDBNull(5))
               {
                   dtrow[5] = reader.GetBoolean(5);
               }
               if (!reader.IsDBNull(6))
               {
                   dtrow[6] = reader.GetString(6).Trim();
               }

               if (!reader.IsDBNull(7))
               {
                   dtrow[7] = reader.GetString(7).Trim();
               }

               if (!reader.IsDBNull(8))
               {
                   dtrow[8] = reader.GetDateTime(8);
               }

               dt.Rows.Add(dtrow);
           }
           reader.Close();
           return dt;
       }

    }
}
