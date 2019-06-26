using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EmployeeCategory
    {
        private Int32 intCategoryID;

        public Int32 IntCategoryID
        {
            get { return intCategoryID; }
            set { intCategoryID = value; }
        }
        private String strCategoryName;

        public String StrCategoryName
        {
            get { return strCategoryName; }
            set { strCategoryName = value; }
        }

        public DataTable ListCategories()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("CategoryID"));
            dt.Columns.Add(new DataColumn("CategoryName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT CategoryID, CategoryName FROM dbo.EmployeeCategory", CommandType.Text);

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
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public Boolean IsContractor(Int32 intCat)
        {
            Boolean ContractorYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("CategoryId");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'C') AND (CategoryID = '"+intCat+"') ", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                ContractorYesNo = true;
            else
                ContractorYesNo = false;
            return ContractorYesNo;
        }

        public Boolean IsContractCashWorker(Int32 intCat)
        {
            Boolean ContractorYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("CategoryId");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('CW','CCW')) AND (CategoryID = '" + intCat + "') ", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                ContractorYesNo = true;
            else
                ContractorYesNo = false;
            return ContractorYesNo;
        }

        public DataTable ListWorkCategories()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("CategoryCode"));
            dt.Columns.Add(new DataColumn("CategoryName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT Code, Name FROM dbo.CHKWorkCategory", CommandType.Text);

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
    }
}
