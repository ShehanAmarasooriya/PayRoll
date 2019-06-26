using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace FTSPayRollBL
{
    public class ClsBankMaster
    {
        private String strBankCode;

        public String StrBankCode
        {
            get { return strBankCode; }
            set { strBankCode = value; }
        }
        private String strBankName;

        public String StrBankName
        {
            get { return strBankName; }
            set { strBankName = value; }
        }
        private String strBranchName;

        public String StrBranchName
        {
            get { return strBranchName; }
            set { strBranchName = value; }
        }

        public void InsertBank()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [BanksMaster] ([BankCode] ,[BankName] ,[BranchName]) VALUES ('" + StrBankCode + "' ,'" + strBankName + "' ,'" + StrBranchName + "') ", CommandType.Text);
        }
        public void UpdateBank()
        {
            SQLHelper.ExecuteNonQuery("UPDATE  [BanksMaster] SET [BankName]='" + strBankName + "' ,[BranchName]='" + StrBranchName + "' WHERE ([BankCode]='" + strBankCode + "')", CommandType.Text);
        }
        public void DeleteBank()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM [BanksMaster] WHERE ([BankCode]='" + strBankCode + "') ", CommandType.Text);
        }
        public DataSet ListBanks()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT  BankCode, BankName, BranchName FROM BanksMaster", CommandType.Text);
            return ds;
        }
        public DataSet ListBankBranch()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT  BankCode, BankName+', '+ BranchName as BankBranch FROM BanksMaster", CommandType.Text);
            return ds;
        }
        public DataSet GetEmployeesOfBankBranch(String strBankBranch)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT BankBranch, DivisionID, EmpNo FROM dbo.EmployeeMaster WHERE (BankBranch = '"+strBankBranch+"')", CommandType.Text);
            return ds;
        }

        public Boolean IsBanksAvailable()
        {
            Boolean boolBanksAvail = false;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT BankCode FROM dbo.BanksMaster", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolBanksAvail = true;
            }
            return boolBanksAvail;
        }
    }
}
