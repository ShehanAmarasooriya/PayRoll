using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class FTSCheckRollSettings
    {
        /*Get Settings Data From Table*/
        //Call with SettingDescription - CropType,WorkType,PaymentType,PaymentMode,FullHalfType
        //returns DataTable with Code(int) and Name(String) columns
        public DataTable ListDataFromSettings(String SettingType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Code"));
            dt.Columns.Add(new DataColumn("Name"));
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Code, Name FROM  dbo.FTSCheckRollSettings WHERE (Type = '"+SettingType+"') ORDER BY Code", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    
                    dtRow[0] = (dataReader["Code"] != null) ? dataReader["Code"].ToString() : string.Empty;
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = (dataReader["Name"] != null) ? dataReader["Name"].ToString(): string.Empty;
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListDataFromSettingsOtherCrops(String SettingType,int MinVal)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Code"));
            dt.Columns.Add(new DataColumn("Name"));
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Code, Name FROM  dbo.FTSCheckRollSettings WHERE (Type = '" + SettingType + "') AND (Code>'"+MinVal+"') ORDER BY Code", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {

                    dtRow[0] = (dataReader["Code"] != null) ? dataReader["Code"].ToString() : string.Empty;
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = (dataReader["Name"] != null) ? dataReader["Name"].ToString() : string.Empty;
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListDataFromSettings(String SettingType,String strType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Code"));
            dt.Columns.Add(new DataColumn("Name"));
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Code, Name FROM  dbo.FTSCheckRollSettings WHERE (Type like '" + SettingType + "') AND (Name='"+strType+"') ORDER BY Code", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetInt32(0);
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

        public DataTable ListDataFromSettingsOtherCrops(String SettingType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Code"));
            dt.Columns.Add(new DataColumn("Name"));
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Code, Name FROM  dbo.FTSCheckRollSettings WHERE (Type like '" + SettingType + "') AND (Name not in ('Tea','Rubber','Oil palm')) ORDER BY Code", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetInt32(0);
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

        public String GetNormValue()
        {
            String strNormVal = "";
            SqlDataReader dataReader;
            dataReader=SQLHelper.ExecuteReader("SELECT Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'Norm') AND (EmpType = 'All')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    strNormVal = dataReader.GetDecimal(0).ToString();
                }
            }
            dataReader.Close();
            return strNormVal;
        }

        public String GetFieldNormValue()
        {
            String strNormVal = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'Norm') AND (EmpType = 'All')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    strNormVal = dataReader.GetDecimal(0).ToString();
                }
            }
            dataReader.Close();
            return strNormVal;
        }

        public Int32 getCropType()
        {
            Int32 intCrop=1;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Code, Name FROM  dbo.FTSCheckRollSettings WHERE (Type = 'CropType') and (Code=1)  ORDER BY Code", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    intCrop = dataReader.GetInt32(0);
                }
            }
            dataReader.Close();
            return intCrop;
        }

        public String GetAccountNo(String acName)
        {
            String strACNo = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Code FROM dbo.FTSCheckRollAccounts WHERE (Name = 'Wages Control')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    strACNo = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return strACNo;
        }

        public void StoreBackup(String FullPath)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramlist = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@fullpath", SqlDbType.VarChar);
            param.Value = FullPath;
            paramlist.Add(param);

            SQLHelper.ExecuteNonQuery("SPBackupDB", CommandType.StoredProcedure, paramlist);
        }

        public String GetBackUpFilePath()
        {
            String strFilePath = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT FilePath FROM dbo.FTSCheckrollPaths WHERE (Name = 'DBBackUp')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    strFilePath = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return strFilePath;
        }

        public String GetIpAddress(String Type)
        {
            String ipAddress = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'IP')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    ipAddress = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return ipAddress;
        }

        public Boolean IsCashOverKgsAvailable()
        {
            String Status = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'CashOkgAvailable')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    Status = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            if (Status.ToUpper().Equals("YES"))
                return true;
            else
                return false;
        }

        public Boolean CashWorkPayslipAvailable()
        {
            Boolean boolCWPaySlip = false;
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'CashWorkPaySlip')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetString(0).Equals("Available"))
                    {
                        boolCWPaySlip = true;
                    }
                }
            }
            return boolCWPaySlip;
        }

        public DataTable ListDataFromCheckrollRates(String strName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Amount"));
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Type, Amount FROM dbo.FTSCheckRollRates WHERE (Name = 'DailyBasic') ORDER BY Name", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetDecimal(1);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public Decimal ListDataFromCheckrollRatesDefault(String strtype)
        {
            Decimal decRate = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT TOP 1   Amount FROM dbo.FTSCheckRollRates WHERE (Name = 'DailyBasic') and (Type='" + strtype + "')ORDER BY Name ", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decRate = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();
            return decRate;
        }

        public DataSet GetFieldCropType()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT Code, Name FROM FTSCheckRollSettings WHERE (Type='FieldCropType')", CommandType.Text);
            return ds;
        }

        public String getNameByCode(String strType,int intCode)
        {
            String strName = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT  Name FROM  dbo.FTSCheckRollSettings WHERE (Type = '"+strType+"') and (Code='"+intCode+"')  ORDER BY Code", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    strName = dataReader.GetString(0);
                }
            }
            dataReader.Close();
            return strName;
        }

        public Decimal GetCheckrollRateValueByType(String strType)
        {
            Decimal ChkRateValue = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Amount FROM dbo.FTSCheckRollRates WHERE (Type = '" + strType + "') ORDER BY Type", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    ChkRateValue = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();
            return ChkRateValue;
        }

        public DataTable ListCashKiloRatesFromCheckrollRates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Amount"));
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Type, Amount FROM dbo.FTSCheckRollRates WHERE (Name = 'cashKgRate') ORDER BY Name", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetDecimal(1);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public String GetNormType()
        {
            String Status = "Division";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'NormType')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    Status = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return Status;
        }

        public String GetPayslipCropType()
        {
            String Status = "Tea";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'payslipcrop')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    Status = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return Status;
        }

        public String GetOilPalmEntryUOM()
        {
            String Status = "Kilos";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'OilPalmHarvestQty')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    Status = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return Status;
        }

        public Boolean IsHolidaypayPHDeducting()
        {
            Boolean boolRtnVal = false;
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'HP_PH_DEDUCTION')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetString(0).Equals("YES"))
                    {
                        boolRtnVal = true;
                    }
                }
            }
            return boolRtnVal;
        }

        public Boolean  GetDisplayClusterSettingValue()
        {
            Boolean boolDisplayCluster = false;
            SqlDataReader readerValue = SQLHelper.ExecuteReader("SELECT TOP (1) Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'DisplayCluster')", CommandType.Text);
            while (readerValue.Read())
            {
                if (!readerValue.IsDBNull(0))
                {
                    if (readerValue.GetString(0).Equals("YES"))
                    {
                        boolDisplayCluster = true;
                    }
                }
            }
            return boolDisplayCluster;
        }

        public Boolean IsEntryValidationAgainstMusterEmpCount()
        {
            Boolean  BoolReturnVal = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'MusterEmpCountOnly')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    if (dataReader.GetString(0).Trim().ToUpper().Equals("YES"))
                    {
                        BoolReturnVal = true;
                    }
                }
            }
            dataReader.Close();
            return BoolReturnVal;
        }

        /*Get Settings Data From Table*/
        //Call with SettingDescription - CropType,WorkType,PaymentType,PaymentMode,FullHalfType
        //returns DataTable with Code(int) and Name(String) columns
        public DataTable ListDataFromSettingsExceptGiven(String SettingType,String Exclude)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Code"));
            dt.Columns.Add(new DataColumn("Name"));
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Code, Name FROM  dbo.FTSCheckRollSettings WHERE (Type = '" + SettingType + "') and (Name<>'"+Exclude+"') ORDER BY Code", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {

                    dtRow[0] = (dataReader["Code"] != null) ? dataReader["Code"].ToString() : string.Empty;
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = (dataReader["Name"] != null) ? dataReader["Name"].ToString() : string.Empty;
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable GetInactiveAbsentsDays(String SettingType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Inactive"));
            dt.Columns.Add(new DataColumn("Terminate"));
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) Amount FROM dbo.FTSCheckRollRates WHERE (Type = '" + SettingType + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = Convert.ToInt32(dataReader.GetDecimal(0));
                }
                

                //dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'TerminateDuration')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[1] = Convert.ToInt32(dataReader.GetDecimal(0));
                }

                
            }
            dt.Rows.Add(dtRow);
            dataReader.Close();
            return dt;
        }

        public Boolean IsAvailableAutoStatusChange()
        {
            Boolean BoolReturnVal = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'AutoStatusChange')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    if (dataReader.GetString(0).Trim().ToUpper().Equals("AVAILABLE"))
                    {
                        BoolReturnVal = true;
                    }
                }
            }
            dataReader.Close();
            return BoolReturnVal;
        }



        


        

       
    }
}
