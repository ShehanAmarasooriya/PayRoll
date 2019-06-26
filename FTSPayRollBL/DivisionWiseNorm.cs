using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class DivisionWiseNorm
    {
        private Int32 intYear;

        public Int32 IntYear
        {
            get { return intYear; }
            set { intYear = value; }
        }
        private Int32 intMonthId;

        public Int32 IntMonthId
        {
            get { return intMonthId; }
            set { intMonthId = value; }
        }
        private String strDivisionId;

        public String StrDivisionId
        {
            get { return strDivisionId; }
            set { strDivisionId = value; }
        }
        private float flNorm;

        public float FlNorm
        {
            get { return flNorm; }
            set { flNorm = value; }
        }
        private Int32 intDivisionNormId;

        public Int32 IntDivisionNormId
        {
            get { return intDivisionNormId; }
            set { intDivisionNormId = value; }
        }
        private DateTime dtNormDate;

        public DateTime DtNormDate
        {
            get { return dtNormDate; }
            set { dtNormDate = value; }
        }
        private Int32 intMalePlkNorm;

        public Int32 IntMalePlkNorm
        {
            get { return intMalePlkNorm; }
            set { intMalePlkNorm = value; }
        }
        private Int32 intMaleSunNorm;

        public Int32 IntMaleSunNorm
        {
            get { return intMaleSunNorm; }
            set { intMaleSunNorm = value; }
        }
        private Int32 intFemalePlkNorm;

        public Int32 IntFemalePlkNorm
        {
            get { return intFemalePlkNorm; }
            set { intFemalePlkNorm = value; }
        }
        private Int32 intFemaleSunNorm;

        public Int32 IntFemaleSunNorm
        {
            get { return intFemaleSunNorm; }
            set { intFemaleSunNorm = value; }
        }

        public void InsertDivisionNorm()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[ChkDivisionWiseNorm](NormDate,[DivisionId],[Year],[Month],[Norm],[UserId],[MalePlkNorm],[MaleSunNorm],[FemalePlkNorm] ,[FemaleSunNorm]) VALUES('" + dtNormDate + "','" + StrDivisionId + "','" + IntYear + "','" + IntMonthId + "','" + FlNorm + "','" + FTSPayRollBL.User.StrUserName + "','"+IntMalePlkNorm+"','"+IntMaleSunNorm+"','"+IntFemalePlkNorm+"','"+IntFemaleSunNorm+"')", CommandType.Text);
        }
        public void UpdateDivisionNorm()
        {
            SQLHelper.ExecuteNonQuery("UPDATE [dbo].[ChkDivisionWiseNorm] SET [CreateDateTime]=getdate(),[UserId]='" + FTSPayRollBL.User.StrUserName + "',[MalePlkNorm]='"+IntMalePlkNorm+"',[MaleSunNorm]='"+IntMaleSunNorm+"',[FemalePlkNorm]='"+IntFemalePlkNorm+"' ,[FemaleSunNorm]='"+IntFemaleSunNorm+"' WHERE NormDate='" + dtNormDate + "' AND DivisionId='" + StrDivisionId + "' and Year='" + IntYear + "' and Month='" + IntMonthId + "'", CommandType.Text);
        }
        public void DeleteDivisionNorm()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM [dbo].[ChkDivisionWiseNorm] WHERE NormDate='" + dtNormDate + "' AND  DivisionId='" + StrDivisionId + "' and Year='" + IntYear + "' and Month='" + IntMonthId + "'", CommandType.Text);
        }
        public DataTable ListDivisionwiseNorm()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Division");
            dt.Columns.Add("MalePlkNorm");
            dt.Columns.Add("FemalePlkNorm");
            dt.Columns.Add("NormDate");
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("RefNo");
            DataRow drow;
            SqlDataReader reader;
            drow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT DivisionId,[MalePlkNorm],[FemalePlkNorm],NormDate,Year, Month, AutoId   FROM dbo.ChkDivisionWiseNorm ORDER BY DivisionId, NormDate DESC ", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetDateTime(3);
                }
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetInt32(5);
                }
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetInt32(6);
                }
                dt.Rows.Add(drow);
            }
            reader.Close();
            return dt;

        }

        public Decimal getLatestNormOfDivision(String strDiv)
        {
            Decimal decNorm = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Norm FROM dbo.ChkDivisionWiseNorm WHERE (DivisionId = '" + strDiv + "') ORDER BY NormDate DESC", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decNorm = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();
            return decNorm;
        }

        public String getLatestNormsStringOfDivision(String strDiv)
        { 
            String strNormValues = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT MalePlkNorm, MaleSunNorm, FemalePlkNorm, FemaleSunNorm FROM dbo.ChkDivisionWiseNorm WHERE (DivisionId = '" + strDiv + "') ORDER BY NormDate DESC", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    strNormValues = "MalePlucker - "+dataReader.GetInt32(0).ToString();
                }
                if (!dataReader.IsDBNull(1))
                {
                    strNormValues = ", MaleSundry - " + dataReader.GetInt32(1).ToString();
                }
                if (!dataReader.IsDBNull(2))
                {
                    strNormValues = ", FemalePlucker - " + dataReader.GetInt32(2).ToString();
                }
                if (!dataReader.IsDBNull(3))
                {
                    strNormValues = ", FemaleSundry - " + dataReader.GetInt32(3).ToString();
                }
            }
            dataReader.Close();
            return strNormValues;
        }

        public Int32 getLatestRelavantNormOfDivision(String strDiv,String strGender,String strPlkSundry,DateTime dtDate)
        {
            Int32 intNorm = 0;
            SqlDataReader dataReader;
            if(strGender.Equals("M"))
            {
                if (strPlkSundry.Equals("PLK"))
                {
                   
                    dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) MalePlkNorm FROM dbo.ChkDivisionWiseNorm WHERE  (DivisionId = '" + strDiv + "') AND (NormDate <= CONVERT(DATETIME, '"+dtDate+"', 102)) ORDER BY NormDate DESC", CommandType.Text);
                }
                else
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) MaleSunNorm FROM dbo.ChkDivisionWiseNorm WHERE  (DivisionId = '" + strDiv + "') AND (NormDate <= CONVERT(DATETIME, '" + dtDate + "', 102)) ORDER BY NormDate DESC", CommandType.Text);
                }
            }
            else
            {
                if (strPlkSundry.Equals("PLK"))
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) FemalePlkNorm FROM dbo.ChkDivisionWiseNorm WHERE  (DivisionId = '" + strDiv + "') AND (NormDate <= CONVERT(DATETIME, '" + dtDate + "', 102)) ORDER BY NormDate DESC", CommandType.Text);
                }
                else
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) FemaleSunNorm FROM dbo.ChkDivisionWiseNorm WHERE  (DivisionId = '" + strDiv + "') AND (NormDate <= CONVERT(DATETIME, '" + dtDate + "', 102)) ORDER BY NormDate DESC", CommandType.Text);
                }
            }
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    intNorm = dataReader.GetInt32(0);
                }
            }
            dataReader.Close();
            return intNorm;
        }

        public Int32 getLatestRelavantNormOfDivision(String strDiv, String strGender, String strPlkSundry, DateTime dtDate,String strField)
        {
            Int32 intNorm = 0;
            SqlDataReader dataReader;
            if (strGender.Equals("M"))
            {
                if (strPlkSundry.Equals("PLK"))
                {

                    dataReader = SQLHelper.ExecuteReader("SELECT top 1  MalePlkNorm FROM  dbo.ChkFieldWiseNorm WHERE (NormDate = CONVERT(DATETIME, '"+dtDate+"', 102)) AND (DivisionId = '"+strDiv+"') AND (FieldId = '"+strField+"')", CommandType.Text);
                }
                else
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT top 1  MalePlkNorm FROM  dbo.ChkFieldWiseNorm WHERE (NormDate = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionId = '" + strDiv + "') AND (FieldId = '" + strField + "')", CommandType.Text);
                }
            }
            else
            {
                if (strPlkSundry.Equals("PLK"))
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT top 1   FemalePlkNorm FROM  dbo.ChkFieldWiseNorm WHERE (NormDate = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionId = '" + strDiv + "') AND (FieldId = '" + strField + "')", CommandType.Text);
                }
                else
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT top 1   FemalePlkNorm FROM  dbo.ChkFieldWiseNorm WHERE (NormDate = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionId = '" + strDiv + "') AND (FieldId = '" + strField + "')", CommandType.Text);
                }
            }
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    intNorm = dataReader.GetInt32(0);
                }
            }
            dataReader.Close();
            return intNorm;
        }

        public Int32 getLatestRelavantNormOfDivision(String strDiv, String strGender, String strPlkSundry)
        {
            Int32 intNorm = 0;
            SqlDataReader dataReader;
            if (strGender.Equals("M"))
            {
                if (strPlkSundry.Equals("PLK"))
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) MalePlkNorm FROM dbo.ChkDivisionWiseNorm WHERE  (DivisionId = '" + strDiv + "') ORDER BY NormDate DESC", CommandType.Text);
                }
                else
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) MaleSunNorm FROM dbo.ChkDivisionWiseNorm WHERE  (DivisionId = '" + strDiv + "') ORDER BY NormDate DESC", CommandType.Text);
                }
            }
            else
            {
                if (strPlkSundry.Equals("PLK"))
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) FemalePlkNorm FROM dbo.ChkDivisionWiseNorm WHERE  (DivisionId = '" + strDiv + "') ORDER BY NormDate DESC", CommandType.Text);
                }
                else
                {
                    dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) FemaleSunNorm FROM dbo.ChkDivisionWiseNorm WHERE  (DivisionId = '" + strDiv + "') ORDER BY NormDate DESC", CommandType.Text);
                }
            }
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    intNorm = dataReader.GetInt32(0);
                }
            }
            dataReader.Close();
            return intNorm;
        }

        public Int32 getCashWorkNorm()
        {
            Int32 intNorm = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'CashKgNorm')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    intNorm = Convert.ToInt32(dataReader.GetDecimal(0));
                }
            }
            dataReader.Close();
            return intNorm;
        }

        public Boolean IsFieldWiseNormAvailable(String strDiv)
        {
            Boolean boolAvailable = false;
            DataSet ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT ISNULL(dbo.ChkFieldWiseNorm.NormDate, GETDATE()) AS Expr4, dbo.EstateField.DivisionID, dbo.EstateField.FieldID, 0 AS Norm,  dbo.ChkFieldWiseNorm.MalePlkNorm AS Expr1, dbo.ChkFieldWiseNorm.FemalePlkNorm AS Expr2, dbo.ChkFieldWiseNorm.PINorm AS Expr3 FROM            dbo.EstateField LEFT OUTER JOIN dbo.ChkFieldWiseNorm ON dbo.EstateField.DivisionID = dbo.ChkFieldWiseNorm.DivisionId AND dbo.EstateField.FieldID = dbo.ChkFieldWiseNorm.FieldId WHERE        (dbo.EstateField.DivisionID = '"+strDiv+"') AND (dbo.ChkFieldWiseNorm.PINorm IS NULL) AND (dbo.ChkFieldWiseNorm.FemalePlkNorm IS NULL) AND  (dbo.ChkFieldWiseNorm.MalePlkNorm IS NULL) ORDER BY dbo.ChkFieldWiseNorm.NormDate DESC",CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
                return false;
            else
                return true;
        }

        public DataTable ListFieldwiseNorm(String strDiv,String strCrop)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("NormDate");
            dt.Columns.Add("Division");
            dt.Columns.Add("Field");
            dt.Columns.Add("MalePlkNorm");
            dt.Columns.Add("FemalePlkNorm");
            dt.Columns.Add("MalePINorm");
            dt.Columns.Add("FemalePINorm");
            dt.Columns.Add("GasTappingNorm");
            DataRow drow;
            SqlDataReader reader;
            drow = dt.NewRow();
            if (IsFieldWiseNormAvailable(strDiv))
            {
                reader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.ChkFieldWiseNorm.NormDate, dbo.EstateField.DivisionID, dbo.EstateField.FieldID,  ISNULL(dbo.ChkFieldWiseNorm.MalePlkNorm, 0) AS MaleNorm, ISNULL(dbo.ChkFieldWiseNorm.FemalePlkNorm, 0) AS FemaleNorm,isnull(dbo.ChkFieldWiseNorm.MalePRINorm,0) as MalePRINorm, isnull(dbo.ChkFieldWiseNorm.FemalePRINorm,0) as FemalePRINorm,isnull(GasTappingNorm,0) as gas FROM            dbo.EstateField LEFT OUTER JOIN dbo.ChkFieldWiseNorm ON dbo.EstateField.DivisionID = dbo.ChkFieldWiseNorm.DivisionId AND dbo.EstateField.FieldID = dbo.ChkFieldWiseNorm.FieldId WHERE        (dbo.EstateField.DivisionID like '" + strDiv + "') AND (dbo.EstateField.CropType = '" + strCrop + "') ORDER BY dbo.ChkFieldWiseNorm.NormDate DESC", CommandType.Text);
            }
            else
            {
                reader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.ChkFieldWiseNorm.NormDate, dbo.EstateField.DivisionID, dbo.EstateField.FieldID,  ISNULL(dbo.ChkFieldWiseNorm.MalePlkNorm, 0) AS MaleNorm, ISNULL(dbo.ChkFieldWiseNorm.FemalePlkNorm, 0) AS FemaleNorm,isnull(dbo.ChkFieldWiseNorm.MalePRINorm,0) as MalePRINorm, isnull(dbo.ChkFieldWiseNorm.FemalePRINorm,0) as FemalePRINorm,isnull(GasTappingNorm,0) as gas  FROM            dbo.EstateField LEFT OUTER JOIN dbo.ChkFieldWiseNorm ON dbo.EstateField.DivisionID = dbo.ChkFieldWiseNorm.DivisionId AND dbo.EstateField.FieldID = dbo.ChkFieldWiseNorm.FieldId WHERE        (dbo.EstateField.DivisionID like '" + strDiv + "') AND (dbo.EstateField.CropType = '" + strCrop + "') ORDER BY dbo.ChkFieldWiseNorm.NormDate DESC", CommandType.Text);
            }
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetDateTime(0);
                }
                else
                    drow[0] = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
             

                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetString(1).Trim();
                }
                else
                    drow[1] = "NA";
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetString(2).Trim();
                }
                else
                    drow[1] = "NA";

                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetInt32(3);
                }
                else
                    drow[3] = 0;
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetInt32(4);
                }
                else
                    drow[4] = 0;

                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetInt32(5);
                }
                else
                    drow[5] = 0;
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetInt32(6);
                }
                else
                    drow[6] = 0;
                if (!reader.IsDBNull(7))
                {
                    drow[7] = reader.GetInt32(7);
                }
                else
                    drow[7] = 0;
               


                //if (!reader.IsDBNull(5))
                //{
                //    drow[5] = reader.GetInt32(5);
                //}
                //if (!reader.IsDBNull(6))
                //{
                //    drow[6] = reader.GetInt32(6);
                //}
                dt.Rows.Add(drow);
            }
            reader.Close();
            return dt;

        }

        public DataTable ListFieldwiseNormEmpty(String strDiv,DateTime dtDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NormDate");
            dt.Columns.Add("Division");
            dt.Columns.Add("Field");
            dt.Columns.Add("MalePlkNorm");
            dt.Columns.Add("FemalePlkNorm");
            dt.Columns.Add("PINorm");
            DataRow drow;
            SqlDataReader reader;
            drow = dt.NewRow();
            if (IsFieldWiseNormAvailable(strDiv))
            {
                reader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.ChkFieldWiseNorm.NormDate, dbo.EstateField.DivisionID, dbo.EstateField.FieldID,  ISNULL(dbo.ChkFieldWiseNorm.MalePlkNorm, 0) AS MaleNorm, ISNULL(dbo.ChkFieldWiseNorm.FemalePlkNorm, 0) AS FemaleNorm, ISNULL(dbo.ChkFieldWiseNorm.PINorm, 0)  AS PINorm FROM            dbo.EstateField LEFT OUTER JOIN dbo.ChkFieldWiseNorm ON dbo.EstateField.DivisionID = dbo.ChkFieldWiseNorm.DivisionId AND dbo.EstateField.FieldID = dbo.ChkFieldWiseNorm.FieldId WHERE        (dbo.EstateField.DivisionID like '" + strDiv + "')  AND (dbo.ChkFieldWiseNorm.NormDate = CONVERT(DATETIME, '"+dtDate+"', 102)) ORDER BY dbo.ChkFieldWiseNorm.NormDate DESC", CommandType.Text);
            }
            else
            {
                reader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.ChkFieldWiseNorm.NormDate, dbo.EstateField.DivisionID, dbo.EstateField.FieldID,  ISNULL(dbo.ChkFieldWiseNorm.MalePlkNorm, 0) AS MaleNorm, ISNULL(dbo.ChkFieldWiseNorm.FemalePlkNorm, 0) AS FemaleNorm, ISNULL(dbo.ChkFieldWiseNorm.PINorm, 0)  AS PINorm FROM            dbo.EstateField LEFT OUTER JOIN dbo.ChkFieldWiseNorm ON dbo.EstateField.DivisionID = dbo.ChkFieldWiseNorm.DivisionId AND dbo.EstateField.FieldID = dbo.ChkFieldWiseNorm.FieldId WHERE        (dbo.EstateField.DivisionID like '" + strDiv + "')  AND (dbo.ChkFieldWiseNorm.NormDate = CONVERT(DATETIME, '" + dtDate + "', 102)) ORDER BY dbo.ChkFieldWiseNorm.NormDate DESC", CommandType.Text);
            }
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetDateTime(0);
                }
                else
                    drow[0] = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());

                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetString(1).Trim();
                }
                else
                    drow[1] = "NA";
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetString(2).Trim();
                }
                else
                    drow[2] = "NA";

                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetInt32(3);
                }
                else
                    drow[2] = 0;
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetInt32(4);
                }
                else
                    drow[4] = 0;

                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetInt32(5);
                }
                else
                    drow[4] = 0;



                //if (!reader.IsDBNull(5))
                //{
                //    drow[5] = reader.GetInt32(5);
                //}
                //if (!reader.IsDBNull(6))
                //{
                //    drow[6] = reader.GetInt32(6);
                //}
                dt.Rows.Add(drow);
            }
            reader.Close();
            return dt;
        }


        public DataTable ListFieldwiseNorm(String strDiv,DateTime dtDate,String strCrop)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NormDate");
            dt.Columns.Add("Division");
            dt.Columns.Add("Field");
            dt.Columns.Add("MalePlkNorm");
            dt.Columns.Add("FemalePlkNorm");
            dt.Columns.Add("MalePINorm");
            dt.Columns.Add("FemalePINorm");
            dt.Columns.Add("GasTappingNorm");
            //dt.Columns.Add("Year");
            //dt.Columns.Add("Month");
            DataRow drow;
            SqlDataReader reader;
            SqlDataReader reader1;
            drow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT DivisionID, FieldID FROM dbo.EstateField WHERE (DivisionID LIKE '" + strDiv + "') AND (CropType = '"+strCrop+"')", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = dtDate;
                }
                if (!reader.IsDBNull(0))
                {
                    drow[1] = reader.GetString(0).Trim();
                }
                else
                    drow[1] = "NA";

                if (!reader.IsDBNull(1))
                {
                    drow[2] = reader.GetString(1).Trim();
                }
                else
                    drow[2] = "NA";
                drow[3] = 0;
                drow[4] = 0;
                drow[5] = 0;
                drow[6] = 0;
                drow[7] = 0;
                reader1 = SQLHelper.ExecuteReader("SELECT NormDate, DivisionId, FieldId, MalePlkNorm, FemalePlkNorm, MalePRINorm, FemalePRINorm,isnull(GasTappingNorm,0) as gas FROM dbo.ChkFieldWiseNorm WHERE (NormDate = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionId = '" + strDiv + "') AND (FieldId = '" + reader.GetString(1).Trim() + "')", CommandType.Text);
                while (reader1.Read())
                {
                    if (!reader1.IsDBNull(0))
                    {
                        drow[3] = reader1.GetInt32(3);
                    }
                    else
                        drow[3] = 0;
                    if (!reader1.IsDBNull(4))
                    {
                        drow[4] = reader1.GetInt32(4);
                    }
                    else
                        drow[4] = 0;
                    if (!reader1.IsDBNull(5))
                    {
                        drow[5] = reader1.GetInt32(5);
                    }
                    else
                        drow[5] = 0;
                    if (!reader1.IsDBNull(6))
                    {
                        drow[6] = reader1.GetInt32(6);
                    }
                    else
                        drow[6] = 0;
                    if (!reader1.IsDBNull(7))
                    {
                        drow[7] = reader1.GetInt32(7);
                    }
                    else
                        drow[7] = 0;
                    
                }
                reader1.Close();
               

                //if (!reader.IsDBNull(5))
                //{
                //    drow[5] = reader.GetInt32(5);
                //}
                //if (!reader.IsDBNull(6))
                //{
                //    drow[6] = reader.GetInt32(6);
                //}
                dt.Rows.Add(drow);
            }
            reader.Close();
            return dt;

        }

        

        public void DeleteAllFieldNorm(String strDiv,DateTime dtDate,String strCrop)
        {
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("DELETE from dbo.ChkFieldWiseNorm WHERE (DivisionId = '" + strDiv + "') AND (NormDate='" + dtDate + "') AND (FieldId in (SELECT FieldID FROM dbo.EstateField WHERE (CropType = '" + strCrop + "') AND (DivisionID = '" + strDiv + "')))", CommandType.Text);
        }
        public void InsertNormToAllField(DateTime dtDate, String strDiv, String strField, Int32 intYear, Int32 intMonth, Int32 intNorm, String strUserID, Int32 intMaleNorm, Int32 intFemalNorm, Int32 intPIMaleNorm, Int32 intPIFeMaleNorm,Int32  intGasTapNorm)
        {
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO dbo.ChkFieldWiseNorm ( NormDate, DivisionId, FieldId, Year, Month, Norm, CreateDateTime, UserId, MalePlkNorm, FemalePlkNorm,MalePRINorm,FemalePRINorm,GasTappingNorm) VALUES('" + dtDate + "', '" + strDiv + "', '" + strField + "', '" + intYear + "', '" + intMonth + "', '" + intNorm + "', getdate(), '" + strUserID + "', '" + intMaleNorm + "', '" + intFemalNorm + "','" + intPIMaleNorm + "','" + intPIFeMaleNorm + "','" + intGasTapNorm + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO dbo.ChkFieldWiseNorm_ChangeLog ( NormDate, DivisionId, FieldId, Year, Month, Norm, CreateDateTime, UserId, MalePlkNorm, FemalePlkNorm,MalePRINorm,FemalePRINorm,GasTappingNorm,ChangedUser) VALUES('" + dtDate + "', '" + strDiv + "', '" + strField + "', '" + intYear + "', '" + intMonth + "', '" + intNorm + "', getdate(), '" + strUserID + "', '" + intMaleNorm + "', '" + intFemalNorm + "','" + intPIMaleNorm + "','" + intPIFeMaleNorm + "','" + intGasTapNorm + "','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
        }

        public DataSet GetFieldwisePlkNorm(String strDiv,String strField,DateTime dtCurrentDate)
        {
            DataSet dsFieldNorm = SQLHelper.FillDataSet("SELECT  TOP (1) MalePlkNorm, FemalePlkNorm, MalePRINorm,FemalePRINorm,GasTappingNorm FROM dbo.ChkFieldWiseNorm WHERE (DivisionId = '" + strDiv + "') AND (FieldId = '" + strField + "') AND (NormDate='" + dtCurrentDate + "') ORDER BY NormDate DESC", CommandType.Text);
            return dsFieldNorm;
        }

        public DataSet GetFieldNormUpdateLog(DateTime dtDate, String strDiv, String strCrop,String strFieldText)
        {
            DataSet dsFieldNormLog = SQLHelper.FillDataSet("SELECT        dbo.ChkFieldWiseNorm_ChangeLog.NormDate, dbo.ChkFieldWiseNorm_ChangeLog.DivisionId, dbo.ChkFieldWiseNorm_ChangeLog.FieldId, dbo.EstateField.CropType, dbo.ChkFieldWiseNorm_ChangeLog.MalePlkNorm,  dbo.ChkFieldWiseNorm_ChangeLog.FemalePlkNorm, dbo.ChkFieldWiseNorm_ChangeLog.MalePRINorm, dbo.ChkFieldWiseNorm_ChangeLog.FemalePRINorm, dbo.ChkFieldWiseNorm_ChangeLog.GasTappingNorm,  dbo.ChkFieldWiseNorm_ChangeLog.ChangedDate, dbo.ChkFieldWiseNorm_ChangeLog.ChangedUser FROM dbo.ChkFieldWiseNorm_ChangeLog INNER JOIN dbo.EstateField ON dbo.ChkFieldWiseNorm_ChangeLog.DivisionId = dbo.EstateField.DivisionID AND dbo.ChkFieldWiseNorm_ChangeLog.FieldId = dbo.EstateField.FieldID WHERE (dbo.ChkFieldWiseNorm_ChangeLog.NormDate = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (dbo.ChkFieldWiseNorm_ChangeLog.DivisionId = '" + strDiv + "') AND (dbo.EstateField.CropType = '" + strCrop + "') AND (dbo.ChkFieldWiseNorm_ChangeLog.FieldId LIKE '%"+strFieldText+"%')", CommandType.Text);
            return dsFieldNormLog;
        }
    }
}
