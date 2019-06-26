using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EstateDivisionBlock
    {
        /// <summary>
        /// List all divisions of the Estate 
        /// </summary>
        /// <returns>DataTable of DivisionId and DivisionName, Order by DivisionID</returns>
        public DataTable ListEstates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstateName");
            dt.Columns.Add("EstateID");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT EstateName, EstateID FROM  dbo.Estate ORDER BY EstateName", CommandType.Text);
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEstates(String Est)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstateName");
            dt.Columns.Add("EstateID");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT EstateName, EstateID FROM   dbo.Estate WHERE EstateID='"+Est+"' ORDER BY EstateName", CommandType.Text);
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListOtherEstates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstateName");
            dt.Columns.Add("EstateID");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT EstateName, EstateID FROM  dbo.OtherEstates ORDER BY EstateName", CommandType.Text);
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEstateDivisions()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("DivisionName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT DivisionID, DivisionName FROM  dbo.EstateDivision where (ActiveDivision = 1)  ORDER BY DivisionID", CommandType.Text);
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEstateDivisions(String DivisionId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("DivisionName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT DivisionID, DivisionName FROM  dbo.EstateDivision where DivisionID='" + DivisionId + "' AND (ActiveDivision = 1) ORDER BY DivisionID", CommandType.Text);
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }
        /// <summary>
        /// List all Fields of the division
        /// </summary>
        /// <returns>DataTable of FieldId, FieldName</returns>
        public DataTable ListDivisionFields(String DivID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FieldId");
            dt.Columns.Add("FieldName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT FieldID, FieldName FROM dbo.EstateField WHERE (DivisionID = '"+DivID+"') ORDER BY FieldID", CommandType.Text);
            dtRow = dt.NewRow();
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListDivisionFieldsByCrop(String DivID,String strCrop)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FieldId");
            dt.Columns.Add("FieldName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT FieldID, FieldName FROM dbo.EstateField WHERE (DivisionID = '" + DivID + "') AND (CropType in ( '"+strCrop+"','None')) ORDER BY FieldID", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT FieldID, FieldName FROM dbo.EstateField WHERE (DivisionID = '" + DivID + "') AND (CropType IN ('"+strCrop+"', 'None'))   union SELECT        TOP (100) PERCENT FieldID, FieldName FROM  dbo.EstateField WHERE (DivisionID = '" + DivID + "') AND (NOT (CropType IN (SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'CropType')))) ORDER BY FieldID", CommandType.Text);
            dtRow = dt.NewRow();
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListOtherDivisionFields(String DivID,String Est)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FieldId");
            dt.Columns.Add("FieldName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT FieldID, FieldName FROM dbo.OtherEstateField WHERE (EstateID = '" + Est + "') AND (DivisionID = '" + DivID + "')", CommandType.Text);
            dtRow = dt.NewRow();
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListOtherDivisionFieldsByCrop(String DivID, String Est,String strCropType)
        {
            //Dont validate crop type as per the Saman's Request
            DataTable dt = new DataTable();
            dt.Columns.Add("FieldId");
            dt.Columns.Add("FieldName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT FieldID, FieldName FROM dbo.OtherEstateField WHERE (EstateID = '" + Est + "') AND (DivisionID = '" + DivID + "') AND (CropType like '%')", CommandType.Text);
            dtRow = dt.NewRow();
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;

        }

        public String getEstateId()
        {
            String EstateID = "";
            SqlDataReader dataReader;

            dataReader = SQLHelper.ExecuteReader("SELECT EstateID, EstateName FROM Estate", CommandType.Text);

            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    EstateID = dataReader.GetString(0);
                }
            }
            dataReader.Close();
            return EstateID;
        }

        public DataSet getFieldName(String strFieldID, String StrDivisionID)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT FieldName,CropType, Type FROM EstateField WHERE (FieldID = '" + strFieldID + "') AND (DivisionID = '" + StrDivisionID + "')", CommandType.Text);
            return ds;

        }

        public DataSet getOtherEstateFieldName(String strFieldID, String StrDivisionID)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT FieldName,CropType, Type FROM OtherEstateField WHERE (FieldID = '" + strFieldID + "') AND (DivisionID = '" + StrDivisionID + "')", CommandType.Text);
            return ds;

        }

        public DataSet EstateDivision(String strDivID)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT DivisionName FROM  dbo.EstateDivision where DivisionID='" + strDivID + "' ORDER BY DivisionID", CommandType.Text);
            return ds;
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
            return dt;
        }

        public Boolean IsFieldAvailable(String FieldId, String strDiv)
        {
            String FieldID = "";
            Boolean boolAvail = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID, FieldID FROM dbo.EstateField WHERE     (DivisionID = '" + strDiv + "') AND (FieldID = '" + FieldId + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(1))
                {
                    FieldID = dataReader.GetString(1).Trim();
                    boolAvail = true;
                }

            }
            dataReader.Close();
            return boolAvail;
        
        }

        public String[] getEmailAddresses()
        {
            String[] EmailAddress = new string[2];
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT ManagerEmailaddress,EmailAddress FROM Estate", CommandType.Text);

            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    EmailAddress[0] = dataReader.GetString(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    EmailAddress[1] = dataReader.GetString(1);
                }
            }
            dataReader.Close();
            return EmailAddress;
        }

        public DataTable ListActiveOtherDivisions(String OtherEst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DivisionName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID, DivisionName FROM dbo.OtherEstateDivision where (EstateID='"+OtherEst+"') AND (ActiveDivision=1)", CommandType.Text);

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

        public DataTable ListOtherDivisionFields(String otherDivID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FieldId");
            dt.Columns.Add("FieldName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT FieldID, FieldName FROM dbo.OtherEstateField WHERE (DivisionID = '" + otherDivID + "') ORDER BY FieldID", CommandType.Text);
            dtRow = dt.NewRow();
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
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

       
        public String GetClusterName(String EstateID,String StrDivision)
        {
            String ClusterName = EstateID;

            ClusterName = ListEstates(EstateID).Rows[0][0].ToString();
            String DivisionID = StrDivision;
            if (EstateID.ToUpper().Equals("RA"))
            {
                switch (DivisionID)
                {
                    case "CR":
                        ClusterName = "SLEONARD ESTATE";
                        break;
                    case "SLL":
                        ClusterName = "SLEONARD ESTATE";
                        break;
                    case "SLU":
                        ClusterName = "SLEONARD ESTATE";
                        break;
                    default:
                        ClusterName = ListEstates(EstateID).Rows[0][0].ToString();
                        break;
                }
            }
            //else if (EstateID.ToUpper().Equals("FRO"))
            //{
            //    switch (DivisionID)
            //    {
            //        case "FRO":
            //            ClusterName = "SLEONARD ESTATE";
            //            break;
            //        case "EYR":
            //            ClusterName = "SLEONARD ESTATE";
            //            break;
            //        case "SLU":
            //            ClusterName = "SLEONARD ESTATE";
            //            break;
            //        default:
            //            ClusterName = ListEstates(EstateID).Rows[0][0].ToString();
            //            break;
            //    }
            //}
            else
            {
                ClusterName = ListEstates(EstateID).Rows[0][0].ToString();
            }
            return ClusterName;
        }

        public Boolean IsNonCropField(String strDiv, String strField)
        {
            Boolean IsNonCrop = false;
            SqlDataReader reader1 = SQLHelper.ExecuteReader("SELECT CropType FROM dbo.EstateField WHERE (NOT (CropType IN (SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'CropType')))) AND (FieldID = '"+strField+"') AND (DivisionID = '"+strDiv+"') OR (CropType = 'None')", CommandType.Text);
            while (reader1.Read())
            {
                if (!reader1.IsDBNull(0))
                {
                    IsNonCrop = true;
                }

            }
            reader1.Dispose();
            return IsNonCrop;
        }

        public String GetDivisionNameByID(String divID)
        {
            String DivName = "";
            SqlDataReader readerDiv;
            readerDiv = SQLHelper.ExecuteReader("SELECT top 1   DivisionName FROM dbo.EstateDivision WHERE (DivisionID = '"+divID+"')", CommandType.Text);
            while (readerDiv.Read())
            {
                if (!readerDiv.IsDBNull(0))
                {
                    DivName = readerDiv.GetString(0).Trim();
                }
                else
                    DivName = "";
            }
            readerDiv.Dispose();
            return DivName;
        }

       

        


       
    }
}
