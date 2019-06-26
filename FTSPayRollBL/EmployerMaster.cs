using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EmployerMaster
    {
        /*
        //kalana begins
        public DataTable ListEmployers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstateID");
            dt.Columns.Add("Division");
            dt.Columns.Add("EmployerNO");
            dt.Columns.Add("ZoneCode");
            dt.Columns.Add("PayMode");
            dt.Columns.Add("DOCode");
            dt.Columns.Add("BankCode");
            dt.Columns.Add("BranchCode");
            dt.Columns.Add("AccountCode");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT estateID,divisionID,employerNo,zoneCode,payMode,doc,bankCode,branchCode,accountNo FROM dbo.EmployerMaster ORDER BY EmployerNO", CommandType.Text);
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
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtRow[7] = dataReader.GetString(7).Trim();
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtRow[8] = dataReader.GetString(8).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }
        //kalana ends.

        public void insertEmployer(String EstateId, String DivisionId,String EmployerNo, String ZoneCode,String payMode,String doc,String bankCode,String branchCode,String accountNo)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[EmployerMaster](estateID,divisionID,employerNo,zoneCode,payMode,doc,bankCode,branchCode,accountNo) VALUES ('"+EstateId.PadLeft(2,' ')+"','"+DivisionId+"','"+EmployerNo+"','"+ZoneCode.PadLeft(1,' ')+"','"+payMode.PadLeft(1,' ')+"','"+doc.PadLeft(2,'0')+"','"+bankCode+"','"+branchCode+"','"+accountNo+"')", CommandType.Text);
        }

        public void DeleteEmployer(String EstateID, String DivisionId, String EmployerNo)
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM [dbo].[EmployerMaster] WHERE employerNo='"+EmployerNo+"'AND estateID='"+EstateID+"'AND divisionID='"+DivisionId+"'", CommandType.Text);
        }

        public void updateEmployer(String EstateId, String DivisionId, String EmployerNo, String ZoneCode, String payMode, String doc, String bankCode, String branchCode, String accountNo)
        {
            SQLHelper.ExecuteNonQuery("UPDATE [dbo].[EmployerMaster] SET divisionID='" + DivisionId + "', zoneCode='" + ZoneCode.PadLeft(1, ' ') + "',payMode='" + payMode.PadLeft(1, ' ') + "',bankCode='" + bankCode +"',branchCode='"+branchCode+"',accountNo='"+accountNo+"',doc='" + doc.PadLeft(2, '0') + "' WHERE employerNo='" + EmployerNo + "'", CommandType.Text);
        }
         */

        public void InsertEmployer(String EstateID, String Division, String EmployerNo, String ZoneCode, String PayMode, String payref, String DOCode)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEPFEmployer] ([EstateID] ,[DivisionID] ,[EmployerNO] ,[ZoneCode] ,[PayMode] ,[PaymentRef] ,[DistrictOfficeCode]) VALUES ('" + EstateID.PadLeft(2, ' ') + "' ,'" + Division + "' ,'" + EmployerNo + "' ,'" + ZoneCode.PadLeft(1, ' ') + "' ,'" + PayMode.PadLeft(1, ' ') + "' ,'" + payref.PadLeft(20, '0') + "' ,'" + DOCode.PadLeft(2, '0') + "')", CommandType.Text);
        }

        public void DeleteEmployer(String EmployerNo)
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM [dbo].[CHKEPFEmployer] WHERE [EmployerNO]='" + EmployerNo + "'", CommandType.Text);
        }

        public void UpdateEmployer(String Division, String EmployerNo, String ZoneCode, String PayMode, String payref, String DOCode)
        {
            SQLHelper.ExecuteNonQuery("UPDATE [dbo].[CHKEPFEmployer] SET DivisionID='" + Division + "', ZoneCode='" + ZoneCode.PadLeft(1, ' ') + "',PayMode='" + PayMode.PadLeft(1, ' ') + "',PaymentRef='" + payref.PadLeft(20, '0') + "',DistrictOfficeCode='" + DOCode.PadLeft(2, '0') + "' WHERE (EmployerNO='" + EmployerNo + "')", CommandType.Text);

        }

        public DataTable ListEmployers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstateID");
            dt.Columns.Add("Division");
            dt.Columns.Add("EmployerNO");
            dt.Columns.Add("ZoneCode");
            dt.Columns.Add("PayMode");
            dt.Columns.Add("PaymentRef");
            dt.Columns.Add("DOCode");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EmployerNO, ZoneCode, PayMode, PaymentRef, DistrictOfficeCode FROM dbo.CHKEPFEmployer ORDER BY EmployerNO", CommandType.Text);
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
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }
    }
}
