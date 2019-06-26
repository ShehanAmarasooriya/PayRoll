using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class AccountInformation
    {
        public DataTable ListDeductionAccounts()
        {
            DataTable dt=new DataTable();
            dt.Columns.Add("ACId");
            dt.Columns.Add("ACName");
            dt.Columns.Add("ACNo");
            dt.Columns.Add("AccountType");
            DataRow dRow;
            SqlDataReader dataReader;
            dRow=dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT ACId_Auto, ACName, ACNo, AccountType FROM dbo.AccountInfo WHERE (AccountType = 'Deductions')", CommandType.Text);
            while (dataReader.Read())
            {
                dRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dRow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dRow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dRow[3] = dataReader.GetString(3).Trim();
                }
                dt.Rows.Add(dRow);
            }
            return dt; 
        }

        public Boolean IsAvailableSubCategory(String strACCode)
        {
            Boolean boolIsACAvail = false;
            DataSet ds = new DataSet();
            ds= SQLHelperGL.FillDataSet("SELECT SubCategoryCode, SubCategoryName FROM dbo.AccountSubCategory WHERE (SubCategoryCode = '"+strACCode+"')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolIsACAvail = true;
            }
            return boolIsACAvail;
        }

        public String GETSubCategoryNameByCode(String strACCode)
        {
            String strName = "";
            DataSet ds = new DataSet();
            ds = SQLHelperGL.FillDataSet("SELECT SubCategoryCode, SubCategoryName FROM dbo.AccountSubCategory WHERE (SubCategoryCode = '" + strACCode + "')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                strName = ds.Tables[0].Rows[0][1].ToString();
            }
            else
                strName = "NA";
            return strName;
        }

        public Boolean IsJobAvaialbleInACMaster(String strJob,String strSubCode)
        {
            Boolean boolIsJobAvail = false;
            DataSet ds = new DataSet();
            ds = SQLHelperGL.FillDataSet("SELECT     SubCategoryCode FROM dbo.AccountMaster WHERE (JobCode = '" + strJob + "') AND (SubCategoryCode = '" + strSubCode + "')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolIsJobAvail = true;
            }
            return boolIsJobAvail;
        }

        public Boolean IsJobAccountAvaialbleInACMaster(String strJob, String strSubCode,String strAcCode)
        {
            Boolean boolIsJobAvail = false;
            DataSet ds = new DataSet();
            ds = SQLHelperGL.FillDataSet("SELECT     AccountCode FROM  dbo.AccountMaster WHERE (JobCode = '" + strJob + "') AND (SubCategoryCode = '" + strSubCode + "') AND (AccountCode = '"+strAcCode+"')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolIsJobAvail = true;
            }
            return boolIsJobAvail;
        }

        public DataTable GetAccountSubCategories()
        {
            //DataSet ds  = SQLHelperGL.FillDataSet("SELECT dbo.AccountCategory.CategoryCode, dbo.AccountCategory.CategoryName, dbo.AccountSubCategory.SubCategoryCode,  dbo.AccountSubCategory.SubCategoryName FROM dbo.AccountSubCategory INNER JOIN dbo.AccountCategory ON dbo.AccountSubCategory.CategoryCode = dbo.AccountCategory.CategoryCode", CommandType.Text);
            //return ds;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("CategoryCode"));
            dt.Columns.Add(new DataColumn("CategoryName"));
            dt.Columns.Add(new DataColumn("SubCategoryCode"));
            dt.Columns.Add(new DataColumn("SubCategoryName"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelperGL.ExecuteReader("SELECT dbo.AccountCategory.CategoryCode, dbo.AccountCategory.CategoryName, dbo.AccountSubCategory.SubCategoryCode,  dbo.AccountSubCategory.SubCategoryName FROM dbo.AccountSubCategory INNER JOIN dbo.AccountCategory ON dbo.AccountSubCategory.CategoryCode = dbo.AccountCategory.CategoryCode", CommandType.Text);

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
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable GetAvailableAccountsForJob(String strMainCode,String strJob)
        {           
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AccountCode"));
            dt.Columns.Add(new DataColumn("AccountName"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelperGL.ExecuteReader("SELECT     AccountCode, AccountName FROM  dbo.AccountMaster WHERE (SubCategoryCode = '"+strMainCode+"') AND (JobCode = '"+strJob+"')", CommandType.Text);

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

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable GetAvailableSubCategoriesForJob( String strJob)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("MainCode"));
            dt.Columns.Add(new DataColumn("Name"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelperGL.ExecuteReader("SELECT dbo.AccountMaster.SubCategoryCode, dbo.AccountSubCategory.SubCategoryName FROM dbo.AccountMaster INNER JOIN dbo.AccountSubCategory ON dbo.AccountMaster.SubCategoryCode = dbo.AccountSubCategory.SubCategoryCode WHERE     (dbo.AccountMaster.JobCode = '"+strJob+"') GROUP BY dbo.AccountMaster.SubCategoryCode, dbo.AccountSubCategory.SubCategoryName", CommandType.Text);

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

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable GetAvailableJobsForSubCategory(String strMainCode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobCode"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelperGL.ExecuteReader("SELECT JobCode FROM dbo.AccountMaster inner join MadulsimaCheckrollDB.dbo.JobMaster on JobCode=MadulsimaCheckrollDB.dbo.JobMaster.JobShortName WHERE (SubCategoryCode like '" + strMainCode + "') AND (dbo.AccountMaster.AccountType = 'Labour') group by JobCode", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }


    }
}
