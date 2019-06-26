using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;


namespace FTSPayRollBL
{
    public class Division
    {
        private String strDivisionID;

        public String StrDivisionID
        {
            get { return strDivisionID; }
            set { strDivisionID = value; }
        }
        private String strDivisionName;

        public String StrDivisionName
        {
            get { return strDivisionName; }
            set { strDivisionName = value; }
        }
        private String strEstateID;

        public String StrEstateID
        {
            get { return strEstateID; }
            set { strEstateID = value; }
        }
        private Boolean boolActiveDivision;

        public Boolean BoolActiveDivision
        {
            get { return boolActiveDivision; }
            set { boolActiveDivision = value; }
        }

        public DataTable ListDivisionsWithQty()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DivisionName"));
            dt.Columns.Add(new DataColumn("IssueQty"));
            dt.Columns.Add(new DataColumn("Unit Price"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID, DivisionName FROM dbo.EstateDivision", CommandType.Text);

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

                dtrow[2] = "00";
                dtrow[3] = "00";

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListDivisionsWithCROP()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("CROP Budget"));
            dt.Columns.Add(new DataColumn("Made Tea Budget"));
            dt.Columns.Add(new DataColumn("CROP Forecast"));
            dt.Columns.Add(new DataColumn("Made Forecast"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.EstateDivision.DivisionID, dbo.EstateField.FieldName  FROM  dbo.EstateDivision INNER JOIN  dbo.EstateField ON dbo.EstateDivision.DivisionID = dbo.EstateField.DivisionID WHERE (dbo.EstateDivision.DivisionID = '"+strDivisionID+"')", CommandType.Text);

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

                dtrow[2] = "0";
                dtrow[3] = "0";
                dtrow[4] = "0";
                dtrow[5] = "0";

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListDivisions()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DivisionName"));
            dt.Columns.Add(new DataColumn("Active"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID, DivisionName,ActiveDivision FROM dbo.EstateDivision", CommandType.Text);

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
                    dtrow[2] = dataReader.GetBoolean(2);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            dtrow = dt.NewRow();
            dtrow[0] = "<Not Applicable>";
            dtrow[1] = "<Not Applicable>";
            dt.Rows.Add(dtrow);
            return dt;
        }
        public DataTable ListActiveDivisions()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DivisionName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID, DivisionName FROM dbo.EstateDivision where ActiveDivision=1", CommandType.Text);

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
                
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            dtrow = dt.NewRow();
            dtrow[0] = "<Not Applicable>";
            dtrow[1] = "<Not Applicable>";
            dt.Rows.Add(dtrow);
            return dt;
        }
        

        public void InsertCategory()
        {
            SQLHelper.ExecuteNonQuery("insert into EstateDivision (DivisionID,DivisionName,EstateID,ActiveDivision) values ('" + StrDivisionID + "','" + StrDivisionName + "','" + StrEstateID + "','"+BoolActiveDivision+"')", CommandType.Text);
        }

        public void UpdateCategory()
        {
            SQLHelper.ExecuteNonQuery("update EstateDivision set DivisionName = '" + StrDivisionName + "',ActiveDivision='"+BoolActiveDivision+"' where DivisionID='" + StrDivisionID + "'", CommandType.Text);
        }

        public void DeleteCategory()
        {
            SQLHelper.ExecuteNonQuery("delete from EstateDivision where DivisionID='" + StrDivisionID + "'", CommandType.Text);
        }

        public DataTable ListEstate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("EstateName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT EstateID, EstateName FROM dbo.Estate", CommandType.Text);

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

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataSet GetDivisionName(String divName)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT DivisionName FROM dbo.EstateDivision WHERE (DivisionID = '" + divName + "')", CommandType.Text);
            return ds;
        }

        public DataTable ListBlocks(String strDivisionID, String strFieldID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BlockID");
            dt.Columns.Add("BlockName");

            SqlDataReader datareader;
            DataRow dtRow;
            dtRow = dt.NewRow();

            datareader = SQLHelper.ExecuteReader("SELECT BlockID, BlockName FROM EstateBlock WHERE (DivisionID = '" + strDivisionID + "') AND (FieldID = '" + strFieldID + "')", CommandType.Text);
            while (datareader.Read())
            {
                dtRow = dt.NewRow();
                if (!datareader.IsDBNull(0))
                {
                    dtRow[0] = datareader.GetString(0);
                }
                if (!datareader.IsDBNull(1))
                {
                    dtRow[1] = datareader.GetString(1);
                }
                dt.Rows.Add(dtRow);
            }
            datareader.Close();
            return dt;
        }

        public DataSet GetBlockName(String strDivisionID, String strFieldID, String strBlockID)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT BlockID, BlockName FROM EstateBlock WHERE (DivisionID = '" + strDivisionID + "') AND (FieldID = '" + strFieldID + "') AND (BlockID = '" + strBlockID + "')", CommandType.Text);
            return ds;
        }
    }
}
