using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace BusinessLayer
{
   public class MISReport
    {
       //public DataSet ViewIntakePerPlucker(DateTime FromDate, DateTime ToDate)
       //{
       //    DataSet ds = new DataSet();
       //    ds = SQLHelper.FillDataSet("", CommandType.Text);
       //    return ds;
       //}
       public DataTable ViewIntakePerPlucker(DateTime FromDate, DateTime ToDate)
       {
           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("Date"));
           dt.Columns.Add(new DataColumn("GreenLeaf"));
           dt.Columns.Add(new DataColumn("CashKillos"));
           dt.Columns.Add(new DataColumn("PluckingManDays"));

           SqlDataReader reader;
           SqlDataReader readerNew;
           
           DataRow dtrow = dt.NewRow();
           reader = EstateProductionSQLHelper.ExecuteReader("SELECT DateEntered,SUM(GreenLeaf) AS GreenLeaf FROM  DailyYieldBookDetails GROUP BY DateEntered HAVING (DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))ORDER BY DateEntered", CommandType.Text);
           while (reader.Read())
           {
               dtrow = dt.NewRow();
               if (!reader.IsDBNull(0))
               {
                   dtrow[0] = reader.GetDateTime(0);
               }
               if (!reader.IsDBNull(1))
               {
                   dtrow[1] = reader.GetDecimal(1);
               }
               dtrow[2] = "0";
               dtrow[3] = "0";

               readerNew = SQLHelper.ExecuteReader("SELECT SUM(CashKgs) AS CashKillos, SUM(ManDays - HolidayHalfNames) AS Mandays, WorkCodeID FROM DailyGroundTransactions GROUP BY DateEntered, CropType, WorkCodeID HAVING (DateEntered = CONVERT(DATETIME, '"+reader.GetDateTime(0)+"', 102)) AND (CropType = 1) AND (WorkCodeID = 'PLK')", CommandType.Text);
               while (readerNew.Read())
               {
                   if (!readerNew.IsDBNull(0))
                   {
                       dtrow[2] = readerNew.GetDecimal(0);
                   }
                   if (!readerNew.IsDBNull(1))
                   {
                       dtrow[3] = readerNew.GetDecimal(1);
                   }
               }
               readerNew.Close();
               dt.Rows.Add(dtrow);
           }
           reader.Close();
           return dt;
       }

       public DataSet ViewYph(DateTime FromDate, DateTime ToDate)
       {
           DataSet ds = new DataSet();
           ds = EstateProductionSQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.DailyYieldBookDetails.DateEntered, dbo.EstateDivision.DivisionName, dbo.DailyYieldBookDetails.FieldID,  dbo.DailyYieldBookDetails.MadeTea, dbo.EstateField.Extent FROM  dbo.DailyYieldBookDetails INNER JOIN   dbo.EstateDivision ON dbo.DailyYieldBookDetails.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN  dbo.EstateField ON dbo.DailyYieldBookDetails.FieldID = dbo.EstateField.FieldID AND   dbo.DailyYieldBookDetails.DivisionID = dbo.EstateField.DivisionID WHERE  (dbo.DailyYieldBookDetails.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME,  '" + ToDate + "', 102))ORDER BY dbo.DailyYieldBookDetails.DateEntered", CommandType.Text);
           return ds;
       }

       public DataSet  ViewYphToDate(Int32 Year, Int32 Month)
       {
           DataSet ds = new DataSet();

           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("Month"));
           dt.Columns.Add(new DataColumn("MonthID"));
           dt.Columns.Add(new DataColumn("Division"));
           dt.Columns.Add(new DataColumn("Field"));
           dt.Columns.Add(new DataColumn("FieldType"));
           dt.Columns.Add(new DataColumn("Extenet"));
           dt.Columns.Add(new DataColumn("GreenLeafMonth"));
           dt.Columns.Add(new DataColumn("MadeTeaMonth"));
           dt.Columns.Add(new DataColumn("YPHMonth"));
           dt.Columns.Add(new DataColumn("GreenLeafPrevious"));
           dt.Columns.Add(new DataColumn("MadeTeaPrevious"));
           dt.Columns.Add(new DataColumn("YPHPrevious"));
       
           DataRow dtrow = dt.NewRow();
           SqlDataReader reader;
           SqlDataReader newReader;
           Decimal MadeTea = 0;
           Decimal Extent = 0;
           Decimal PreviousMadeTea = 0;
           Decimal PreviousGreenLeaf = 0;
           String Division = "";
           String Field = "";
           
           Int32 monthId = 0;
           String MonthName = "";

           reader = SQLHelper.ExecuteReader("SELECT MonthName, MonthCode FROM Month WHERE (MonthCode = '" + Month + "')", CommandType.Text);
           while (reader.Read())
           {
              
               if (!reader.IsDBNull(0))
               {
                 MonthName= reader.GetString(0).Trim();
               }
               if (!reader.IsDBNull(1))
               {
                  monthId = reader.GetInt32(1);
               }
           }
           reader.Dispose();

           reader = SQLHelper.ExecuteReader("SELECT EstateDivision.DivisionName, EstateField.FieldName, EstateField.FieldType, EstateField.Extent,EstateDivision.DivisionID FROM  EstateField INNER JOIN  EstateDivision ON EstateField.DivisionID = EstateDivision.DivisionID", CommandType.Text);
           while (reader.Read())
           {
               dtrow = dt.NewRow();

               dtrow[0] = MonthName;
               dtrow[1] = monthId;

               if (!reader.IsDBNull(0))
               {
                   dtrow[2] = reader.GetString(0).Trim();
               }
               if (!reader.IsDBNull(1))
               {
                   dtrow[3] = reader.GetString(1).Trim();
                   Field = reader.GetString(1).Trim();
               }
               if (!reader.IsDBNull(2))
               {
                   dtrow[4] = reader.GetString(2).Trim();
               }
               if (!reader.IsDBNull(3))
               {
                   dtrow[5] = reader.GetDecimal(3);
                   Extent = reader.GetDecimal(3);
               }
               if (!reader.IsDBNull(4))
               {
                   Division = reader.GetString(4).Trim();
               }
               dtrow[6] = "0";
               dtrow[7] = "0";

               newReader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(GreenLeaf) AS GreenLeaf, SUM(MadeTea) AS MadeTea FROM DailyYieldBookDetails WHERE  (MONTH(DateEntered) = '" + monthId + "') AND (YEAR(DateEntered) = '" + Year + "')GROUP BY DivisionID, FieldID HAVING(SUM(GreenLeaf) > 0) AND (DivisionID = '" + reader.GetString(4) + "') AND (FieldID = '" + reader.GetString(1).Trim() + "')", CommandType.Text);
               {
                   while (newReader.Read())
                   {
                       if (!newReader.IsDBNull(0))
                       {
                           dtrow[6] = newReader.GetDecimal(0);
                       }
                       if (!newReader.IsDBNull(1))
                       {
                           dtrow[7] = newReader.GetDecimal(1);
                           MadeTea = newReader.GetDecimal(1);
                       }
                   }
               }
               dtrow[8] = MadeTea / Extent;

               for (int i = 1; i < monthId; i++)
               {
                   newReader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(GreenLeaf) AS GreenLeaf, SUM(MadeTea) AS MadeTea FROM DailyYieldBookDetails WHERE  (MONTH(DateEntered) = '" + i + "') AND (YEAR(DateEntered) = '" + Year + "')GROUP BY DivisionID, FieldID HAVING(SUM(GreenLeaf) > 0) AND (DivisionID = '" + Division + "') AND (FieldID = '" + Field + "')", CommandType.Text);
                   {
                       while (newReader.Read())
                       {
                           if (!newReader.IsDBNull(0))
                           {
                              PreviousGreenLeaf += newReader.GetDecimal(0);
                               
                           }
                           if (!newReader.IsDBNull(1))
                           {
                               PreviousMadeTea += newReader.GetDecimal(1);   
                           }
                       }
                   }
                   newReader.Dispose();
               }
               dtrow[9]  = PreviousGreenLeaf;
               dtrow[10] = PreviousMadeTea;
               dtrow[11] = PreviousMadeTea / Extent;

               PreviousGreenLeaf = 0;
               PreviousMadeTea = 0;
               Extent = 0;
               MadeTea = 0;
               Division = "";
               Field = "";

               dt.Rows.Add(dtrow);
               newReader.Dispose();
           }
           reader.Dispose();
          // return dt;

           ///////////////////////////Division/////////////////////////
           DataTable dtNew = new DataTable();
           dtNew.Columns.Add(new DataColumn("Division"));
           dtNew.Columns.Add(new DataColumn("Extenet"));
           dtNew.Columns.Add(new DataColumn("GreenLeafMonth"));
           dtNew.Columns.Add(new DataColumn("MadeTeaMonth"));
           dtNew.Columns.Add(new DataColumn("YPHMonth"));
           dtNew.Columns.Add(new DataColumn("GreenLeafPrevious"));
           dtNew.Columns.Add(new DataColumn("MadeTeaPrevious"));
           dtNew.Columns.Add(new DataColumn("YPHPrevious"));

           DataRow dtrownew = dtNew.NewRow();
           //SqlDataReader reader1;
           //SqlDataReader newReader;
           Decimal DivisionMadeTea = 0;
           Decimal DivisionExtenet = 0;

           reader = SQLHelper.ExecuteReader("SELECT EstateDivision.DivisionID, EstateDivision.DivisionName, SUM(EstateField.Extent) AS Extent FROM EstateDivision INNER JOIN EstateField ON EstateDivision.DivisionID = EstateField.DivisionID GROUP BY EstateDivision.DivisionID, EstateDivision.DivisionName",CommandType.Text);
           while (reader.Read())
           {
               dtrownew = dtNew.NewRow();
               if (!reader.IsDBNull(1))
               {
                   dtrownew[0] = reader.GetString(1).Trim();
               }
               if (!reader.IsDBNull(2))
               {
                   dtrownew[1] = reader.GetDecimal(2);
                   DivisionExtenet = reader.GetDecimal(2);
               }
               dtrownew[2] = "0";
               dtrownew[3] = "0";
               dtrownew[4] = "0";
               dtrownew[5] = "0";
               dtrownew[6] = "0";
               dtrownew[7] = "0";

               newReader = EstateProductionSQLHelper.ExecuteReader("SELECT  SUM(GreenLeaf) AS GreenLeaf, SUM(MadeTea) AS MadeTea FROM  DailyYieldBookDetails WHERE (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "')GROUP BY DivisionID HAVING(DivisionID = '" + reader.GetString(0) + "')", CommandType.Text);
               while (newReader.Read())
               {
                   if (!newReader.IsDBNull(0))
                   {
                       dtrownew[2] = newReader.GetDecimal(0);
                       
                   }
                   if (!newReader.IsDBNull(1))
                   {
                       dtrownew[3] = newReader.GetDecimal(1);
                       DivisionMadeTea = newReader.GetDecimal(1);
                   }
               }
               dtrownew[4] = DivisionMadeTea / DivisionExtenet;
             
               Decimal DivisionPreviousGreenLeaf = 0;
               Decimal DivisionPreviousMadeTea = 0;

               for (int i = 1; i < Month; i++)
               {
                   newReader = EstateProductionSQLHelper.ExecuteReader("SELECT  SUM(GreenLeaf) AS GreenLeaf, SUM(MadeTea) AS MadeTea FROM  DailyYieldBookDetails WHERE (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + i + "')GROUP BY DivisionID HAVING(DivisionID = '" + reader.GetString(0) + "')", CommandType.Text);
                   {
                       while (newReader.Read())
                       {
                           if (!newReader.IsDBNull(0))
                           {
                               DivisionPreviousGreenLeaf += newReader.GetDecimal(0);

                           }
                           if (!newReader.IsDBNull(1))
                           {
                               DivisionPreviousMadeTea += newReader.GetDecimal(1);
                           }
                       }
                   }
                   newReader.Dispose();
               }
               dtrownew[5] = DivisionPreviousGreenLeaf;
               dtrownew[6] = DivisionPreviousMadeTea;
               dtrownew[7] = DivisionPreviousMadeTea / DivisionExtenet;

               DivisionExtenet = 0;
               DivisionMadeTea = 0;

               dtNew.Rows.Add(dtrownew);
               newReader.Dispose();
           }
         
           reader.Close();
           //////////////////////////////////////////////////vp and sd

           DataTable dtNew1 = new DataTable();
           dtNew1.Columns.Add(new DataColumn("Type"));
           dtNew1.Columns.Add(new DataColumn("Extenet"));
           dtNew1.Columns.Add(new DataColumn("GreenLeafMonth"));
           dtNew1.Columns.Add(new DataColumn("MadeTeaMonth"));
           dtNew1.Columns.Add(new DataColumn("YPHMonth"));
           dtNew1.Columns.Add(new DataColumn("GreenLeafPrevious"));
           dtNew1.Columns.Add(new DataColumn("MadeTeaPrevious"));
           dtNew1.Columns.Add(new DataColumn("YPHPrevious"));
           dtNew1.Columns.Add(new DataColumn("TotalYPHMonth"));
           dtNew1.Columns.Add(new DataColumn("TotalYPHPrevious"));

           DataRow dtrownew1 = dtNew1.NewRow();
           Decimal Madetea = 0;
           Decimal TypeExtent = 0;
           //Decimal TotalMadetea = 0;
           Decimal TotalExtent = 0;

           reader = SQLHelper.ExecuteReader("SELECT DISTINCT FieldType, SUM(Extent) AS Extent FROM EstateField GROUP BY FieldType", CommandType.Text);
           while (reader.Read())
           {
               dtrownew1 = dtNew1.NewRow();
               if (!reader.IsDBNull(1))
               {
                   dtrownew1[0] = reader.GetString(0).Trim();
               }
               if (!reader.IsDBNull(1))
               {
                   dtrownew1[1] = reader.GetDecimal(1);
                   TypeExtent = reader.GetDecimal(1);
               }
               dtrownew1[2] = "0";
               dtrownew1[3] = "0";
               dtrownew1[4] = "0";
               dtrownew1[5] = "0";
               dtrownew1[6] = "0";
               dtrownew1[7] = "0";
               dtrownew1[8] = "0";
               dtrownew1[9] = "0";

               newReader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(GreenLeaf) AS GreenLeaf, SUM(MadeTea) AS MadeTea, FieldType FROM DailyYieldBookDetails WHERE (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "')GROUP BY FieldType HAVING (FieldType = '" + reader.GetString(0).Trim() + "')", CommandType.Text);
               while (newReader.Read())
               {
                   if (!newReader.IsDBNull(0))
                   {
                       dtrownew1[2] = newReader.GetDecimal(0);

                   }
                   if (!newReader.IsDBNull(1))
                   {
                       dtrownew1[3] = newReader.GetDecimal(1);
                       Madetea = newReader.GetDecimal(1);
                   }
               }
               dtrownew1[4] = Madetea / TypeExtent;

               Decimal PreviousGreenLeafType = 0;
               Decimal PreviousMadeTeaType = 0;

               for (int i = 1; i < Month; i++)
               {
                   newReader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(GreenLeaf) AS GreenLeaf, SUM(MadeTea) AS MadeTea, FieldType FROM DailyYieldBookDetails WHERE (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + i + "')GROUP BY FieldType HAVING (FieldType = '" + reader.GetString(0).Trim() + "')", CommandType.Text);
                   {
                       while (newReader.Read())
                       {
                           if (!newReader.IsDBNull(0))
                           {
                               PreviousGreenLeafType += newReader.GetDecimal(0);

                           }
                           if (!newReader.IsDBNull(1))
                           {
                               PreviousMadeTeaType += newReader.GetDecimal(1);
                           }
                       }
                   }
                   newReader.Dispose();
               }
               dtrownew1[5] = PreviousGreenLeafType;
               dtrownew1[6] = PreviousMadeTeaType;
               dtrownew1[7] = PreviousMadeTeaType / TypeExtent;

               TotalExtent=
               TypeExtent = 0;
               Madetea = 0;

               dtNew1.Rows.Add(dtrownew1);
               newReader.Dispose();
           }

           reader.Close();

           ds.Tables.Add(dt);
           ds.Tables.Add(dtNew);
           ds.Tables.Add(dtNew1);

           return ds;
       }
       public DataTable MonthlyPluckingRounds(String DivisionID, Int32 Year, Int32 Month)
       {           
           Int32 roundNo = 0;
           Decimal FieldWeight = 0;
           Decimal Mandays = 0;
           Boolean chk = false;
           int count = 0;
           SqlDataReader reader;
          
           
           DataSet ds = new DataSet();
           ds = SQLHelper.FillDataSet("SELECT DivisionID,FieldID FROM EstateField WHERE(DivisionID = '" + DivisionID + "')", CommandType.Text);


           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("Division"));
           dt.Columns.Add(new DataColumn("Field"));
           dt.Columns.Add(new DataColumn("RoundNo"));
           dt.Columns.Add(new DataColumn("FieldWeight"));
           dt.Columns.Add(new DataColumn("ManDays"));
           dt.Columns.Add(new DataColumn("IntakePerPlucker"));

           DataRow dtrow=dt.NewRow();

           foreach (DataRow drRow in ds.Tables[0].Rows)
           {
               roundNo = 0;
               String strDivisionID = drRow[0].ToString();
               String strField = drRow[1].ToString();

               DataSet dsnew = new DataSet();
               dsnew = EstateProductionSQLHelper.FillDataSet("SELECT FieldWeight, RoundCompleted,FieldID,EnteredDate FROM DailyPluckingRounds WHERE (FieldID = '" + strField + "') AND (MONTH(EnteredDate) = '" + Month + "') AND (YEAR(EnteredDate) = '" + Year + "') AND (DivisionID = '" + strDivisionID + "') ORDER BY AutoKey", CommandType.Text);

               foreach (DataRow drR in dsnew.Tables[0].Rows)
               {
                   count++;
                   if (count == 1)
                   {
                       dtrow = dt.NewRow();
                   }
                   if(drR.ItemArray[1].ToString()=="True")
                   {
                       roundNo++;
                       FieldWeight += Convert.ToDecimal(drR.ItemArray[0].ToString());
                       reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.ManDays + dbo.DailyGroundTransactions.CashManDays) AS ManDays, dbo.EstateField.MapField,dbo.DailyGroundTransactions.DateEntered FROM dbo.DailyGroundTransactions INNER JOIN  dbo.EstateField ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID AND dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.EstateField.MapField, dbo.DailyGroundTransactions.WorkCodeID,dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.CashManDays HAVING        (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + drR.ItemArray[3].ToString() + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + strDivisionID + "') AND (dbo.EstateField.MapField = '" + strField + "') AND (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')", CommandType.Text);
                       while (reader.Read())
                       {
                           Mandays += reader.GetDecimal(0);
                       }
                       reader.Close();
                       dtrow[0] = strDivisionID;
                       dtrow[1] = strField;
                       dtrow[2] = roundNo;
                       dtrow[3] = FieldWeight;
                       dtrow[4] = Mandays;
                       dtrow[5] = "0";


                       dt.Rows.Add(dtrow); 
                       count = 0;
                       FieldWeight = 0;
                       Mandays = 0;
                   }
                   else
                   {
                       FieldWeight += Convert.ToDecimal(drR.ItemArray[0].ToString());
                       DateTime date = Convert.ToDateTime(drR.ItemArray[3].ToString());
                       reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.ManDays + dbo.DailyGroundTransactions.CashManDays) AS ManDays, dbo.EstateField.MapField,dbo.DailyGroundTransactions.DateEntered FROM dbo.DailyGroundTransactions INNER JOIN  dbo.EstateField ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID AND dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.EstateField.MapField, dbo.DailyGroundTransactions.WorkCodeID,dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.CashManDays HAVING        (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + drR.ItemArray[3].ToString() + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + strDivisionID + "') AND (dbo.EstateField.MapField = '" + strField + "') AND (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')", CommandType.Text);
                       while (reader.Read())
                       {
                           Mandays += reader.GetDecimal(0);
                       }
                       reader.Close();
                   }
              }
               
         }
            return dt;
       }

       public DataTable Harvesting(String DivisionID,Int32 Week,Int32 Year,DateTime FromDate,DateTime ToDate)
       {
           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("Crop"));
           dt.Columns.Add(new DataColumn("LPH"));
           dt.Columns.Add(new DataColumn("BelowNorm"));
           dt.Columns.Add(new DataColumn("ManDays"));
           dt.Columns.Add(new DataColumn("Extent"));
           dt.Columns.Add(new DataColumn("TodateCrop"));
           dt.Columns.Add(new DataColumn("TodateLPH"));
           dt.Columns.Add(new DataColumn("TodateBelowNorm"));
           dt.Columns.Add(new DataColumn("TodateManDays"));
           dt.Columns.Add(new DataColumn("TodateExtent"));
           dt.Columns.Add(new DataColumn("BelowNormPercentage"));
           dt.Columns.Add(new DataColumn("TodateBelowNormPercentage"));
           dt.Columns.Add(new DataColumn("FactoryWeightToFieldWeight"));
           dt.Columns.Add(new DataColumn("FactoryWeightToCRollWeight"));
           dt.Columns.Add(new DataColumn("FactoryWeightToFieldWeightPercentage"));           
           dt.Columns.Add(new DataColumn("FactoryWeightToCRollWeightPercentage"));
           dt.Columns.Add(new DataColumn("TodateFactoryWeightToFieldWeight"));
           dt.Columns.Add(new DataColumn("TodateFactoryWeightToCRollWeight"));
           dt.Columns.Add(new DataColumn("TodateFactoryWeightToFieldWeightPercentage"));
           dt.Columns.Add(new DataColumn("TodateFactoryWeightToCRollWeightPercentage"));
           dt.Columns.Add(new DataColumn("Average"));
           dt.Columns.Add(new DataColumn("TodateAverage"));
           //dt.Columns.Add(new DataColumn("CropBudjet"));
           //dt.Columns.Add(new DataColumn("ExtentBudjet"));

           SqlDataReader reader;
           DataRow dtrow = dt.NewRow();

           Decimal Extent=0;
           Int32 LabourCount=0;
           Decimal TodateExtent = 0;
           Int32 TodateLabourCount = 0;
           Decimal BelownormPercentage= 0;
           Decimal TodateBelownormPercentage = 0;
           Int32 Belownorm= 0;
           Int32 TodateBelownorm= 0;
           Int32 empCount = 0;
           Int32 TodateempCount = 0;
           Decimal FieldWeight = 0;
           Decimal FactoryWeight = 0;
           Decimal CheckRollWeight = 0;
           Decimal TodateFieldWeight = 0;
           Decimal TodateFactoryWeight = 0;
           Decimal TodateCheckRollWeight = 0;
           Decimal FactoryWeightToFieldWeight = 0;
           Decimal FactoryWeightToFieldWeightPercentage = 0;
           Decimal FactoryWeightToCRollWeight = 0;
           Decimal FactoryWeightToCRollWeightPercentage = 0;
           Decimal TodateFactoryWeightToFieldWeight = 0;
           Decimal TodateFactoryWeightToFieldWeightPercentage = 0;
           Decimal TodateFactoryWeightToCRollWeight = 0;
           Decimal TodateFactoryWeightToCRollWeightPercentage = 0;


           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(FieldWeight) AS Total, YEAR(EnteredDate) AS Year FROM  DailyPluckingRounds GROUP BY { fn WEEK(EnteredDate) }, DivisionID, YEAR(EnteredDate)HAVING (YEAR(EnteredDate) = '" + Year + "') AND ({ fn WEEK(EnteredDate) } = '" + Week + "') AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   dtrow[0] = reader.GetDecimal(0);
                   FieldWeight = reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(HectaresCompleted) AS Total FROM  DailyPluckingRounds GROUP BY { fn WEEK(EnteredDate) }, DivisionID, YEAR(EnteredDate) HAVING (YEAR(EnteredDate) = '" + Year + "') AND ({ fn WEEK(EnteredDate) } = '" + Week + "') AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   Extent = reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(AreaCompletedExtent) AS total FROM DailyFieldWorkDetails GROUP BY DivisionID, { fn WEEK(EnteredDate) }, YEAR(EnteredDate) HAVING (YEAR(EnteredDate) = '" + Year + "') AND ({ fn WEEK(EnteredDate) } = '" + Week + "') AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   Extent += reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           reader = SQLHelper.ExecuteReader("SELECT COUNT(DailyGroundTransactions.EmpNo) AS Count FROM DailyGroundTransactions INNER JOIN JobMaster ON DailyGroundTransactions.WorkCodeID = JobMaster.JobShortName WHERE (JobMaster.ExpenditureType <> 'Capital') AND (DailyGroundTransactions.DivisionID = '"+DivisionID+"') AND ({ fn WEEK(DailyGroundTransactions.DateEntered) } = '"+Week+"') AND (YEAR(DailyGroundTransactions.DateEntered) = '"+Year+"')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   LabourCount = reader.GetInt32(0); ;
               }
           }
           reader.Dispose();

           try
           {
               Decimal LPH = Extent / LabourCount;
               dtrow[1] = Math.Round(LPH, 2);
           }
           catch { }

           reader = SQLHelper.ExecuteReader("SELECT  COUNT(EmpNo) AS count FROM  DailyGroundTransactions WHERE (WorkQty - NormKilos < 0) AND (DivisionID = '"+DivisionID+"') AND ({ fn WEEK(DateEntered) } = '"+Week+"') AND (WorkCodeID = 'PLK') AND (YEAR(DateEntered) = '"+Year+"')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   dtrow[2] = reader.GetInt32(0);
                   Belownorm = reader.GetInt32(0);
               }
               
           }
           reader.Dispose();

           reader = SQLHelper.ExecuteReader("SELECT  SUM(DailyGroundTransactions.ManDays) AS Count FROM  DailyGroundTransactions INNER JOIN JobMaster ON DailyGroundTransactions.WorkCodeID = JobMaster.JobShortName WHERE (JobMaster.ExpenditureType <> 'Capital') AND (DailyGroundTransactions.WorkCodeID = 'PLK') AND (YEAR(DailyGroundTransactions.DateEntered) = '"+Year+"') AND ({ fn WEEK(DailyGroundTransactions.DateEntered) } = '"+Week+"') AND (DailyGroundTransactions.DivisionID = '"+DivisionID+"')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   dtrow[3] =  reader.GetDecimal(0);
               }
           }
           reader.Dispose();


           reader = SQLHelper.ExecuteReader("SELECT SUM(Extent) AS extent FROM EstateField GROUP BY DivisionID HAVING (DivisionID = '"+DivisionID+"')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   dtrow[4] = reader.GetDecimal(0);
               }
           }
           reader.Dispose();
          
           //////////////////////////////// Todate///////////////////////////////////////////////////////////////////////

           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(FieldWeight) AS Total FROM DailyPluckingRounds WHERE (EnteredDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))GROUP BY DivisionID HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   dtrow[5] = reader.GetDecimal(0);
                   TodateFieldWeight = reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(HectaresCompleted) AS Total FROM  DailyPluckingRounds WHERE(EnteredDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))GROUP BY DivisionID HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   TodateExtent = reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(AreaCompletedExtent) AS total FROM  DailyFieldWorkDetails WHERE(EnteredDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))GROUP BY DivisionID HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   TodateExtent += reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           reader = SQLHelper.ExecuteReader("SELECT  COUNT(DailyGroundTransactions.EmpNo) AS Count, DailyGroundTransactions.DivisionID FROM  DailyGroundTransactions INNER JOIN JobMaster ON DailyGroundTransactions.WorkCodeID = JobMaster.JobShortName WHERE (JobMaster.ExpenditureType <> 'Capital') AND (DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102))GROUP BY DailyGroundTransactions.DivisionID HAVING (DailyGroundTransactions.DivisionID = '"+DivisionID+"')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   TodateLabourCount = reader.GetInt32(0); ;
               }
           }
           reader.Dispose();

           Decimal LPHTodate = TodateExtent / TodateLabourCount;
           dtrow[6] = Math.Round(LPHTodate, 2);

           reader = SQLHelper.ExecuteReader("SELECT COUNT(EmpNo) AS count FROM DailyGroundTransactions WHERE  (WorkQty - NormKilos < 0) AND (DivisionID = '"+DivisionID+"') AND (WorkCodeID = 'PLK') AND (DateEntered BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102))", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   dtrow[7] = reader.GetInt32(0);
                   TodateBelownorm = reader.GetInt32(0);
               }

           }
           reader.Dispose();

           reader = SQLHelper.ExecuteReader("SELECT SUM(DailyGroundTransactions.ManDays) AS Count FROM  DailyGroundTransactions INNER JOIN JobMaster ON DailyGroundTransactions.WorkCodeID = JobMaster.JobShortName WHERE  (DailyGroundTransactions.DivisionID = '"+DivisionID+"') AND (DailyGroundTransactions.WorkCodeID = 'PLK') AND (JobMaster.ExpenditureType <> 'Capital') AND (DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102))", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   dtrow[8] = reader.GetDecimal(0);
               }
           }

           dtrow[9] = dtrow[4];

           ///////////////////////////////////belownorm Percentage/////////////////

           reader = SQLHelper.ExecuteReader("SELECT COUNT(EmpNo) AS count FROM DailyGroundTransactions WHERE (DivisionID = '" + DivisionID + "') AND (WorkCodeID = 'PLK') AND ({ fn WEEK(DateEntered) } = '" + Week + "') AND (YEAR(DateEntered) = '" + Year + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   empCount = reader.GetInt32(0);
               }
           }
           reader.Dispose();
           try
           {
               BelownormPercentage = (Convert.ToDecimal(Belownorm) / Convert.ToDecimal(empCount)) * 100;
           }
           catch { }

           dtrow[10] = BelownormPercentage;

           reader = SQLHelper.ExecuteReader("SELECT COUNT(EmpNo) AS count FROM DailyGroundTransactions WHERE (DivisionID = '" + DivisionID + "') AND (WorkCodeID = 'PLK') AND (DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME,'" + ToDate + "', 102))", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   TodateempCount = reader.GetInt32(0);
               }

           }
           reader.Dispose();
           try
           {
               TodateBelownormPercentage = (Convert.ToDecimal(TodateBelownorm) / Convert.ToDecimal(TodateempCount)) * 100;
           }
           catch { }

           dtrow[11] = TodateBelownormPercentage;
           ///////////////////////////////////Factory and Checkroll weight/////////////////

           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(GreenLeaf) AS Qty FROM DailyYieldBookDetails WHERE (YEAR(DateEntered) = '" + Year + "') AND ({ fn WEEK(DateEntered) } = '" + Week + "')GROUP BY DivisionID HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                  FactoryWeight = reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           reader = SQLHelper.ExecuteReader("SELECT SUM(WorkQty) AS Qty FROM dbo.DailyGroundTransactions WHERE ({ fn WEEK(DateEntered) } = '"+Week+"') AND (YEAR(DateEntered) = '"+Year+"')GROUP BY WorkCodeID, DivisionID HAVING  (WorkCodeID = 'PLK') AND (DivisionID = '"+DivisionID+"')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                  CheckRollWeight = reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           FactoryWeightToFieldWeight = FieldWeight - FactoryWeight;
           dtrow[12] = FactoryWeightToFieldWeight;

           FactoryWeightToCRollWeight = CheckRollWeight - FactoryWeight;
           dtrow[13] = FactoryWeightToCRollWeight;

           try
           {
               dtrow[14] = (FactoryWeightToFieldWeight / FieldWeight) * 100;
           }
           catch { }

           try
           {
               dtrow[15] = (FactoryWeightToCRollWeight / CheckRollWeight) * 100;
           }
           catch { }

           ////////////////////////Todate Factory and Checkroll weight/////////////////

           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(GreenLeaf) AS Qty FROM DailyYieldBookDetails WHERE  (DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))GROUP BY DivisionID HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   TodateFactoryWeight = reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           reader = SQLHelper.ExecuteReader("SELECT SUM(WorkQty) AS Qty FROM dbo.DailyGroundTransactions WHERE  (DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))GROUP BY WorkCodeID, DivisionID HAVING (WorkCodeID = 'PLK') AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   TodateCheckRollWeight = reader.GetDecimal(0);
               }
           }
           reader.Dispose();

           TodateFactoryWeightToFieldWeight= TodateFieldWeight - TodateFactoryWeight;
           dtrow[16] = TodateFactoryWeightToFieldWeight;

           TodateFactoryWeightToCRollWeight = TodateCheckRollWeight - TodateFactoryWeight;
           dtrow[17] = TodateFactoryWeightToCRollWeight;

           try
           {
               dtrow[18] = (TodateFactoryWeightToFieldWeight / TodateFieldWeight) * 100;
           }
           catch { }
           try
           {
               dtrow[19] = (TodateFactoryWeightToCRollWeight / TodateCheckRollWeight) * 100;
           }
           catch { }

           //////////////////////////////////////average/////////////////////////////////////////////

           Decimal Average= (FactoryWeight / Convert.ToDecimal(empCount));
           dtrow[20] = Average;

           dtrow[21] = TodateFactoryWeight / Convert.ToDecimal(TodateempCount);

           ////////////////////////////////budjet////////////////////////////////////////////////////////


           dt.Rows.Add(dtrow);
           return dt;
       }

       public DataTable ViewDailyCop(DateTime FromDate,DateTime Todate)
       {
           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("Day"));
           dt.Columns.Add(new DataColumn("JobnNme"));
           dt.Columns.Add(new DataColumn("JobGroup"));
           dt.Columns.Add(new DataColumn("Cost"));
           dt.Columns.Add(new DataColumn("MadeTea"));

           DataRow dtrow = dt.NewRow();
           SqlDataReader reader;
           SqlDataReader readerNew;

           reader = SQLHelper.ExecuteReader("SELECT DAY(DailyGroundTransactions.DateEntered) AS Day,JobMaster.JobName, JobGroup.GroupName, DailyGroundTransactions.Expenditure + DailyGroundTransactions.PluckingExpenditure AS Cost,DailyGroundTransactions.DateEntered FROM JobMaster INNER JOIN JobGroup ON JobMaster.JobGroup = JobGroup.GroupID INNER JOIN DailyGroundTransactions ON JobMaster.JobShortName = DailyGroundTransactions.WorkCodeID GROUP BY JobMaster.JobShortName, JobGroup.GroupName, DailyGroundTransactions.Expenditure, DailyGroundTransactions.PluckingExpenditure,JobMaster.JobName, DAY(DailyGroundTransactions.DateEntered), DailyGroundTransactions.DateEntered HAVING(DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + Todate + "',102))", CommandType.Text);
           while (reader.Read())
           {
               dtrow = dt.NewRow();
               if (!reader.IsDBNull(0))
               {
                   dtrow[0] = reader.GetInt32(0);
               }
               if (!reader.IsDBNull(1))
               {
                   dtrow[1] = reader.GetString(1).Trim();
               }
               if (!reader.IsDBNull(2))
               {
                   dtrow[2] = reader.GetString(2).Trim();
               }
               if (!reader.IsDBNull(3))
               {
                   dtrow[3] = reader.GetDecimal(3);
               }
               if (!reader.IsDBNull(4))
               {
                   dtrow[4] = "0";
               }

               readerNew = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(NettMadeTea) AS MadeTea FROM DailyYieldBookDetails GROUP BY DateEntered HAVING(DateEntered = CONVERT(DATETIME, '" + reader.GetDateTime(4) + "', 102))", CommandType.Text);
               while (readerNew.Read())
               {
                   if (!readerNew.IsDBNull(0))
                   {
                       dtrow[4] = readerNew.GetDecimal(0);
                   }
               }
               readerNew.Close();
               dt.Rows.Add(dtrow);
           }
           reader.Close();
           return dt;
       }

       public Decimal GetMadeTea(DateTime FromDate,DateTime Todate)
       {
           Decimal Madetea = 0;
           SqlDataReader reader;
           reader = EstateProductionSQLHelper.ExecuteReader("SELECT SUM(NettMadeTea) AS MadeTea FROM DailyYieldBookDetails WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + Todate + "', 102))", CommandType.Text);
           while (reader.Read())
           {
               if (!reader.IsDBNull(0))
               {
                   Madetea = reader.GetDecimal(0);
               }
           }
           reader.Close();
           return Madetea;
       }

       public DataSet ViewCheckrollAnalysis(DateTime FromDate, DateTime ToDate, String WorkCodeName)
       {
           DataSet ds = new DataSet();
           SqlDataAdapter da = new SqlDataAdapter();
           SqlCommand command = new SqlCommand();
           da.SelectCommand = SQLHelper.CreateCommand("SELECT DailyGroundTransactions.EstateID, DailyGroundTransactions.FieldID, SUM(DailyGroundTransactions.ManDays) AS ManDays,DAY(DailyGroundTransactions.DateEntered) AS Day, JobMaster.JobName, EstateDivision.DivisionName FROM DailyGroundTransactions INNER JOIN JobMaster ON DailyGroundTransactions.WorkCodeID = JobMaster.JobShortName INNER JOIN EstateDivision ON DailyGroundTransactions.DivisionID = EstateDivision.DivisionID GROUP BY DailyGroundTransactions.EstateID, DailyGroundTransactions.DateEntered, DailyGroundTransactions.FieldID, JobMaster.JobName,DailyGroundTransactions.WorkCodeID, EstateDivision.DivisionName HAVING (DailyGroundTransactions.WorkCodeID = '" + WorkCodeName + "') AND (DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "',102) AND CONVERT(DATETIME, '" + ToDate + "', 102))AND (SUM(DailyGroundTransactions.ManDays) > 0)", CommandType.Text);
           da.Fill(ds, "CheckrollAnalysis");
           return ds;
       }
   }
}
