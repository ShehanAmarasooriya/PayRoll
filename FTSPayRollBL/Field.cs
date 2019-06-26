using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;


namespace FTSPayRollBL
{
    public class Field
    {
        private String strFieldID;

        public String StrFieldID
        {
            get { return strFieldID; }
            set { strFieldID = value; }
        }
        private String strDivisionID;

        public String StrDivisionID
        {
            get { return strDivisionID; }
            set { strDivisionID = value; }
        }
        private String strEstateID;

        public String StrEstateID
        {
            get { return strEstateID; }
            set { strEstateID = value; }
        }
        private Decimal decExtent;

        public Decimal DecExtent
        {
            get { return decExtent; }
            set { decExtent = value; }
        }
        private String strFieldType;

        public String StrFieldType
        {
            get { return strFieldType; }
            set { strFieldType = value; }
        }

        private String strFieldName;

        public String StrFieldName
        {
            get { return strFieldName; }
            set { strFieldName = value; }
        }

        private String strCropType;

        public String StrCropType
        {
            get { return strCropType; }
            set { strCropType = value; }
        }

        private Decimal dNorm;

        public Decimal DNorm
        {
            get { return dNorm; }
            set { dNorm = value; }
        }

        private Decimal dTree;

        public Decimal DTree
        {
            get { return dTree; }
            set { dTree = value; }
        }
        private String strMapField;

        public String StrMapField
        {
            get { return strMapField; }
            set { strMapField = value; }
        }

        private String strExpenditureType;

        public String StrExpenditureType
        {
            get { return strExpenditureType; }
            set { strExpenditureType = value; }
        }

        public void InsertField()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO EstateField(EstateID, DivisionID, CropType, FieldID, Norm, Remarks, Type, Extent, Tree, FieldType, FieldName,mapField) VALUES ('" + StrEstateID + "','" + StrDivisionID + "','" + StrCropType + "','" + StrFieldID + "','" + DNorm + "','" + StrFieldName + "','" + StrExpenditureType + "','" + DecExtent + "','" + DTree + "','" + StrFieldType + "','" + StrFieldName + "','NA')", CommandType.Text);
        }

        public void UpdateField()
        {
            SQLHelper.ExecuteNonQuery("UPDATE EstateField SET CropType ='" + StrCropType + "', Norm ='" + DNorm + "', Remarks ='" + StrFieldName + "', Extent ='" + DecExtent + "', Tree ='" + DTree + "', FieldType ='" + StrFieldType + "', FieldName ='" + StrFieldName + "',Type='"+StrExpenditureType+"' WHERE (DivisionID = '" + StrDivisionID + "') AND (FieldID = '" + StrFieldID + "')", CommandType.Text);
        }

        public void DeleteField()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM EstateField WHERE (DivisionID = '" + StrDivisionID + "') AND (FieldID = '" + StrFieldID + "')", CommandType.Text);
        }

        public void InsertFieldHeadOffice()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO EstateField (EstateID, DivisionID, FieldID, Extent, FieldType) VALUES ('" + strEstateID + "','" + strDivisionID + "','" + strFieldID + "','" + decExtent + "','" + strFieldType + "')", CommandType.Text);
        }

        public DataTable ListAllFields()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionName"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("FieldName"));
            dt.Columns.Add(new DataColumn("CropType"));
            dt.Columns.Add(new DataColumn("Norm"));
            dt.Columns.Add(new DataColumn("Tree"));
            dt.Columns.Add(new DataColumn("Extends"));
            dt.Columns.Add(new DataColumn("FieldType"));
            dt.Columns.Add(new DataColumn("MapField"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT EstateDivision.DivisionName, EstateField.FieldID, EstateField.Remarks, EstateField.CropType, EstateField.Norm, EstateField.Tree, EstateField.Extent, EstateField.FieldType,mapField FROM EstateField INNER JOIN EstateDivision ON EstateField.DivisionID = EstateDivision.DivisionID", CommandType.Text);

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
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDecimal(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetDecimal(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7).Trim();
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListFieldDetails(String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("FieldName"));
            dt.Columns.Add(new DataColumn("Remarks"));
            dt.Columns.Add(new DataColumn("Extent"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID, FieldID, FieldName, Remarks, Extent FROM dbo.EstateField WHERE        (DivisionID like '"+strDiv+"')", CommandType.Text);

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
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDecimal(4);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable SearchFieldDetails( String strSearchBy,String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("FieldName"));
            dt.Columns.Add(new DataColumn("Remarks"));
            dt.Columns.Add(new DataColumn("Extent"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader(" select * from (SELECT        DivisionID, FieldID, FieldName, Remarks, Extent FROM            dbo.EstateField WHERE        (DivisionID = '"+strDiv+"') ) as T1 where  (T1.FieldID LIKE '%" + strSearchBy + "%') OR (T1.FieldName LIKE '%" + strSearchBy + "%') OR (T1.Remarks LIKE '%" + strSearchBy + "%')", CommandType.Text);

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
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDecimal(4);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }


        public DataTable ListAllFieldsByDivision()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FieldID"));
            DataRow dtRow = dt.NewRow();
            SqlDataReader reader;

            reader = SQLHelper.ExecuteReader("SELECT FieldID FROM EstateField WHERE (DivisionID = '"+strDivisionID+"')", CommandType.Text);

            while (reader.Read())
            {
                dtRow = dt.NewRow();

                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            reader.Close();
            return dt;
        }

        public DataTable ListAllFieldsTypes(String strDivisionID, String strFieldID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Type"));
            DataRow dtRow = dt.NewRow();
            SqlDataReader reader;

            reader = SQLHelper.ExecuteReader("SELECT Type FROM dbo.EstateField Where (DivisionID='" + strDivisionID + "') AND (FieldID='" + strFieldID + "')  GROUP BY Type", CommandType.Text);

            while (reader.Read())
            {
                dtRow = dt.NewRow();

                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            reader.Close();
            return dt;
        }

        public DataTable GetWorkCodeType(String strWorkCode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Type"));
            DataRow dtRow = dt.NewRow();
            SqlDataReader reader;

            reader = SQLHelper.ExecuteReader("SELECT ExpenditureType FROM JobMaster WHERE (JobShortName = '" + strWorkCode + "')", CommandType.Text);

            while (reader.Read())
            {
                dtRow = dt.NewRow();

                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            reader.Close();
            return dt;
        }

        public DataSet IsAvailableEntry(String DivisionID, String FieldID)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT * FROM DailyGroundTransactions WHERE (DivisionID = '" + DivisionID + "') AND (FieldID = '" + FieldID + "')", CommandType.Text);
            return ds;
        }
    }
}
