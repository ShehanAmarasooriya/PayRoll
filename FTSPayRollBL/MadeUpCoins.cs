using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
namespace FTSPayRollBL
{
    public class MadeUpCoins
    {
        public DataTable ListInactiveEmpYearMonthCoins(String strDiv,Int32 intYear,Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Division");
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Coins");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID,ProcessYear, ProcessMonth,  EmpNo, MadeUpCoins FROM dbo.CHKMadeUpCoins WHERE (ProcessYear = '" + intYear + "') AND (ProcessMonth = '" + intMonth + "') AND (DivisionID = '" + strDiv + "')", CommandType.Text);
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

        public DataTable ListInactiveEmpCoins(String strDiv, Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Coins");
            dt.Columns.Add("TransferTo");
            dt.Columns.Add("TransferEmpDivision");
            dt.Columns.Add("PaidYesNo");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  EmpNo, MadeUpCoins, TransferToEmp, TrnEmpDivision,PaidYesNo FROM dbo.CHKMadeUpCoins WHERE (ProcessYear = '" + intYear + "') AND (ProcessMonth = '" + intMonth + "') AND (DivisionID = '" + strDiv + "')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetDecimal(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetBoolean(4);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        /*---------------------*/

        public void UpdateUnpaidCoinsTransfer(String strDiv, String empNo,Int32 intYear,Int32 intMonth, String TrnEmp, String TrnEmpDiv)
        {
            try
            {
                //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'CHKMadeUpCoins' ,'" + strDiv + "', '" + empNo + "',  '" + TrnEmpDiv + "', '"+TrnEmp+"', '"+intYear+"',  '"+intMonth+"' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
                SQLHelper.ExecuteNonQuery("UPDATE [dbo].[CHKMadeUpCoins] SET [TransferToEmp]='" + TrnEmp + "',TrnEmpDivision='" + TrnEmpDiv + "' WHERE [ProcessYear]='" + intYear + "' AND [ProcessMonth]='" + intMonth + "' AND [DivisionID]='" + strDiv + "' AND [EmpNo]='" + empNo + "'", CommandType.Text);
            }
            catch (Exception ex)
            {
                String strerror = ex.Message;
            }
        
        }

        public DataTable ListOpeningDebts(String strDiv, Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("IsActive");
            dt.Columns.Add("Amount");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT EmpNo, EMPName, ActiveEmployee, ISNULL ((SELECT     DebtAmount FROM dbo.ChkDebtors WHERE     (DebtYear = '" + intYear + "') AND (DebtMonth = '" + intMonth + "') AND (DivisionID  = dbo.EmployeeMaster.DivisionID ) AND (EmpNO = dbo.EmployeeMaster.EmpNo)), 0) AS DebtAmount FROM dbo.EmployeeMaster WHERE (DivisionID = '" + strDiv + "')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetBoolean(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetDecimal(3);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListOpeningCoins(String strDiv, Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("IsActive");
            dt.Columns.Add("Amount");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  EmpNo, EMPName, ActiveEmployee, ISNULL ((SELECT     MadeUpCoins FROM dbo.CHKMadeUpCoins WHERE     (ProcessYear = '" + intYear + "') AND (ProcessMonth = '" + intMonth + "') AND (DivisionID  = dbo.EmployeeMaster.DivisionID ) AND (EmpNo = dbo.EmployeeMaster.EmpNo)), 0) AS MadeUpCoins FROM dbo.EmployeeMaster WHERE (DivisionID = '" + strDiv + "')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetBoolean(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetDecimal(3);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public void UpdateOpeningDebts(String strEmp, String strDiv, Int32 intYear, Int32 intMonth, Decimal decAmount)
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.ChkDebtors SET DebtAmount='" + decAmount + "' WHERE (DivisionId='" + strDiv + "') AND (EmpNO='" + strEmp + "') AND (DebtYear='" + intYear + "') AND (DebtMonth='" + intMonth + "') ", CommandType.Text);
        }

        public void UpdateOpeningCoins(String strEmp, String strDiv, Int32 intYear, Int32 intMonth, Decimal decAmount)
        {
            SQLHelper.ExecuteNonQuery("UPDATE  dbo.CHKMadeUpCoins SET MadeUpCoins='" + decAmount + "' WHERE (DivisionID='" + strDiv + "') AND (EmpNo='" + strEmp + "') AND (ProcessYear='" + intYear + "') AND (ProcessMonth='" + intMonth + "')", CommandType.Text);
        }

        public void InsertOpeningDebts(String strEmp, String strDiv, Int32 intYear, Int32 intMonth, Decimal decAmount)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO dbo.ChkDebtors([DivisionId] ,[EmpNO] ,[DebtYear] ,[DebtMonth] ,[DebtAmount] ,[PaidAdditionYesNo] ,[RecoveredYesNO] ,[CreateDateTime] ,[UserId] ,[DebtorType] ,[DeductionGroupId] ,[DeductionId] ,[PreviousAmount])  VALUES  ('" + strDiv + "','"+strEmp+"', '" + intYear + "', '" + intMonth + "', '"+decAmount+"',0,0,GETDATE(),'"+FTSPayRollBL.User.StrUserName+"','Debtors',(SELECT     DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE (ShortName = 'PD')),(SELECT     TOP (1) dbo.CHKDeduction.DeductionCode FROM dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKDeductionGroup.ShortName = 'pd')),0) ", CommandType.Text);
        }

        public void InsertOpeningCoins(String strEmp, String strDiv, Int32 intYear, Int32 intMonth, Decimal decAmount)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKMadeUpCoins] ([ProcessYear] ,[ProcessMonth] ,[EmpNo] ,[MadeUpCoins] ,[CreateDateTime] ,[UserId] ,[DivisionID] ,[TransferToEmp] ,[TrnEmpDivision] ,[PaidYesNo] ,[TransferdAmount] ) VALUES ('" + intYear + "' ,'" + intMonth + "' ,'" + strEmp + "' ,'" + decAmount + "' ,GETDATE() ,'" + FTSPayRollBL.User.StrUserName + "' ,'" + strDiv + "' ,'NA' ,'NA' ,0 ,0 )", CommandType.Text);
        }

        public Boolean IsEntryAvailable(String strEmp, String strDiv, Int32 intYear, Int32 intMonth, String DebtCoin)
        {
            DataSet dsData = new DataSet();
            Boolean boolAvail = false;
            if (DebtCoin.Equals("Debt"))
            {
                dsData = SQLHelper.FillDataSet("SELECT  DebtYear, DebtMonth, DivisionId, EmpNO, DebtAmount FROM dbo.ChkDebtors WHERE (DebtYear = '" + intYear + "') AND (DebtMonth = '" + intMonth + "') AND (DivisionId = '" + strDiv + "') AND (EmpNO = '" + strEmp + "')", CommandType.Text);
            }
            else
            {
                dsData = SQLHelper.FillDataSet("SELECT ProcessYear, ProcessMonth, DivisionID, EmpNo FROM dbo.CHKMadeUpCoins WHERE     (ProcessYear = '"+intYear+"') AND (ProcessMonth = '"+intMonth+"') AND (DivisionID = '"+strDiv+"') AND (EmpNo = '"+strEmp+"')",CommandType.Text);
            }

            if (dsData.Tables[0].Rows.Count > 0)
            {
                boolAvail = true;
            }
            return boolAvail;
        }

        
    }
}
