using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DataAccess;


namespace FTSPayRollBL
{
    public class Debtors 
{
      
        private string empNO;

        public string EmpNO
        {
            get { return empNO; }
            set { empNO = value; }
        }
        private Int32 debtYear;

        public Int32 DebtYear
        {
            get { return debtYear; }
            set { debtYear = value; }
        }
        private Int32 debtMonth;

        public Int32 DebtMonth
        {
            get { return debtMonth; }
            set { debtMonth = value; }
        }
        private decimal debtAmount;

        public decimal DebtAmount
        {
            get { return debtAmount; }
            set { debtAmount = value; }
        }

        private decimal adjustedvalue;

        public decimal Adjustedvalue
        {
            get { return adjustedvalue; }
            set { adjustedvalue = value; }
        }

        public DataTable ListDebtsToAdjust(String DivisionID, Int32 intYear,Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DebtYear");
            dt.Columns.Add("DebtMonth");
            dt.Columns.Add("EmpNO");
            dt.Columns.Add("PreviousAmount");
            dt.Columns.Add("DebtAmount");
            SqlDataReader dataReader;
            DataRow dtRow;

            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT DebtYear, DebtMonth,EmpNO,  PreviousAmount, DebtAmount FROM dbo.ChkDebtors WHERE (DivisionID = '" + DivisionID + "') AND (DebtYear = '" + intYear + "') AND (DebtMonth = '" + intMonth + "') ORDER BY EmpNo", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[2] = dataReader.GetString(2);
                }                
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetDecimal(3);
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

        public void UpdateDebtsAdjustGrid(String strDiv,String EmpNO, Int32 DebtYear, Int32 DebtMonth, decimal Adjustedvalue)
        {
           // SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + EmpNO + "',  '" + Adjustedvalue + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE ChkDebtors SET DebtAmount='" + Adjustedvalue + "' WHERE (DebtYear='" + DebtYear + "')and (DebtMonth='" + DebtMonth + "')and (DivisionID='" + strDiv + "') and (EmpNO='" + EmpNO + "') ", CommandType.Text);
        }

        public String InsertNewDebitAmount(String strDiv, String strEmp, Int32 intYear, Int32 intMonth,Decimal decAmount)
        {
            DataSet ds=GetPreviousDebtID();
            Int32 debtGroup = Convert.ToInt32(GetPreviousDebtID().Tables[0].Rows[0][0].ToString());
            Int32 debtId = Convert.ToInt32(GetPreviousDebtID().Tables[0].Rows[0][1].ToString());
            Boolean boolAvail = CheckAvailabilityOfDebit(strDiv, strEmp, intYear, intMonth);
            if (!boolAvail)
                {
                    SQLHelper.ExecuteNonQuery("insert into  dbo.ChkDebtors (DivisionId, EmpNO, DebtYear, DebtMonth, DebtAmount, PaidAdditionYesNo, RecoveredYesNO, CreateDateTime, UserId, DebtorType, DeductionGroupId,DeductionId) values('"+strDiv+"','"+strEmp+"','"+intYear+"','"+intMonth+"','"+decAmount+"',0,0,getdate(),'"+User.StrUserName+"','Debtors','"+debtGroup+"','"+debtId+"')", CommandType.Text);
                    return "OK";
                }
            else
                return "EXISTS";

        }

        public Boolean CheckAvailabilityOfDebit(String strDiv1, String strEmp1, Int32 intYear1, Int32 intMonth1)
        {
            DataSet ds;
            ds = SQLHelper.FillDataSet("SELECT TOP (1) DebtYear, DebtMonth, DivisionId, EmpNO FROM dbo.ChkDebtors WHERE (DebtYear = '"+intYear1+"') AND (DebtMonth = '"+intMonth1+"') AND (DivisionId = '"+strDiv1+"') AND (EmpNO = '"+strEmp1+"')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet GetPreviousDebtID()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT top 1 DeductionGroupCode, DeductionCode FROM dbo.CHKDeduction WHERE (DeductionGroupCode = 14)", CommandType.Text);
            return ds;
        }

        public Decimal GetTotal(String strDiv1,  Int32 intYear1, Int32 intMonth1)
        {
            Decimal decRtnVal=0;
            DataSet ds = SQLHelper.FillDataSet("SELECT     TOP (1) SUM(DebtAmount) AS Expr1 FROM dbo.ChkDebtors GROUP BY DebtYear, DebtMonth, DivisionId HAVING (DebtYear = '"+intYear1+"') AND (DebtMonth = '"+intMonth1+"') AND (DivisionId = '"+strDiv1+"')",CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return decRtnVal;
            }
        }

        public Boolean IsProcessedMonth(Int32 intYear, Int32 intMonth)
        {
            Boolean boolProcessed = false;
            DataSet ds = SQLHelper.FillDataSet("SELECT     ProcessYear, ProcessMonth, ProcessedYesNo FROM dbo.CHKProcessDetails WHERE     (ProcessYear = '"+intYear+"') AND (ProcessMonth = '"+intMonth+"') AND (ProcessedYesNo = 1)", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolProcessed = true;
            }
            else
            {
                boolProcessed = false;
            }
            return boolProcessed;
        }

    }
}
