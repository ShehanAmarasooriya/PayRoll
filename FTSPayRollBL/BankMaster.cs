using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace FTSPayRollBL
{
    public class BankMaster
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
            SQLHelper.ExecuteNonQuery("INSERT INTO [BanksMaster] ([BankCode] ,[BankName] ,[BranchName]) VALUES ('"+StrBankCode+"' ,'"+strBankName+"' ,'"+StrBranchName+"') ",CommandType.Text);
        }
        public void UpdateBank()
        {
            SQLHelper.ExecuteNonQuery("UPDATE  [BanksMaster] SET [BankName]='"+strBankName+"' ,[BranchName]='"+StrBranchName+"' WHERE ([BankCode]='"+strBankCode+"')",CommandType.Text);
        }
        public void DeleteBank()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM [BanksMaster] WHERE ([BankCode]='" + strBankCode + "') ",CommandType.Text);
        }

    }
}
