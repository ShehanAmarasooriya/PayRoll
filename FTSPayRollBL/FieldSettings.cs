
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using DataAccess;
using System.Data.SqlClient;

namespace FTSPayRollBL
{
    public class FieldSettings
    {
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private String strField;

        public String StrField
        {
            get { return strField; }
            set { strField = value; }
        }

        private String strType;

        public String StrType
        {
            get { return strType; }
            set { strType = value; }
        }
        private Decimal decOkgRate;

        public Decimal DecOkgRate
        {
            get { return decOkgRate; }
            set { decOkgRate = value; }
        }

        public void InsertFieldOkgRate()
        {
            SQLHelper.ExecuteNonQuery(" INSERT INTO [dbo].[CHKFieldSettings] ([DivisionID] ,[FieldID] ,[Type] ,[Rate]) VALUES ('"+StrDivision+"' ,'"+StrField+"','OKGRATE' ,'"+DecOkgRate+"')",CommandType.Text);
        }

        public void UpdateFieldOkgRate()
        {
            SQLHelper.ExecuteNonQuery(" UPDATE [dbo].[CHKFieldSettings] SET [Rate]='" + DecOkgRate + "' WHERE ([DivisionID]='"+StrDivision+"') AND ([FieldID]='"+StrField+"') AND ([Type]='"+StrType+"')",CommandType.Text);
        }

        public void DeleteFieldOkgRate()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM [dbo].[CHKFieldSettings] WHERE ([DivisionID]='" + StrDivision + "') AND ([FieldID]='" + StrField + "') AND ([Type]='" + StrType + "')",CommandType.Text);
        }

        public DataTable ListFieldOverKgRates(String strDiv,String strRateType)
        {
            DataTable dtFieldSettings = new DataTable();
            dtFieldSettings.Columns.Add("FieldID");
            dtFieldSettings.Columns.Add("Rate");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dtFieldSettings.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT FieldID, Rate FROM dbo.CHKFieldSettings WHERE (DivisionID = '" + strDiv + "') AND (Type = '" + strRateType + "')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dtFieldSettings.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetDecimal(1);
                }

                dtFieldSettings.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dtFieldSettings;
        }

        public DataSet GetFieldSettings()
        {
            DataSet dsFSettings = new DataSet();
            dsFSettings = SQLHelper.FillDataSet("SELECT  DivisionID, Type, FieldID, Rate FROM dbo.CHKFieldSettings",CommandType.Text);
            return dsFSettings;
        }

       

    }
}
