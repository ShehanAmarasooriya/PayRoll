using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class ChkReconciliation
    {

        private static String strYear;

        public static String StrYear
        {
             get { return ChkReconciliation.strYear; }
            set { ChkReconciliation.strYear = value; }
        }

        private static Int32 intMonth;

        public static Int32 IntMonth
        {
            get { return ChkReconciliation.intMonth; }
            set { ChkReconciliation.intMonth = value; }
        }

        public DataTable GetReconciliation(String strYear, Int32 intMonth)
        {
            StrYear = strYear;
            IntMonth = intMonth;
            DateTime dtFromDate = new DateTime(Convert.ToInt32(strYear), intMonth, 1);
            DateTime dtToDate = dtFromDate.AddMonths(1).AddDays(-1);

            DataTable dtMain = new DataTable();
            dtMain.Columns.Add(new DataColumn("CODE"));
            dtMain.Columns.Add(new DataColumn("NAME"));
            dtMain.Columns.Add(new DataColumn("TOTALAMOUNT"));
            dtMain.Columns.Add(new DataColumn("DIVISION"));
            dtMain.Columns.Add(new DataColumn("CATEGORY"));
            dtMain.Columns.Add(new DataColumn("QTY"));            

            DataSet dsTotalEarnings1 = new DataSet();
            dsTotalEarnings1 = SQLHelper.FillDataSet("SELECT 'PWAGE' AS CODE, 'PLUCKINGWAGES' AS Name, SUM(PluckingNamePay) AS PLK, 'PRI' AS CODE1, 'PRISun/PLK' AS Name1, SUM(PRIAmount) AS PRI, 'SWAGE' AS CODE2, 'OTHERDAYSWAGES' AS Name2, SUM(SundryNamePay) AS SUNDRY, 'EXRAT' AS CODE3, 'EXTRARATES' AS Name3, SUM(ExtraRates) AS EXTRA, 'OVKGS' AS CODE4, 'OVERKILOS' As Name4, SUM(OverKilosPay) AS OVERKl, 'INCEN' AS CODE5, 'INCENTIVEPAYMENT' As Name5, SUM(AttIncentive) AS INCENTIVEPAYMENT, DivisionId, '1.Total Earnings' AS Category FROM dbo.EmpMonthlyEarnings where (Year = " + strYear + ") AND (Month = " + intMonth + ") GROUP BY DivisionId, Year, Month", CommandType.Text);
            
            DataSet dsTotalEarnings2 = new DataSet();
            dsTotalEarnings2 = SQLHelper.FillDataSet("SELECT 'AMOTA' AS CODE, 'Fact A.M.O/T' AS Type, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '1.Total Earnings' AS Category FROM dbo.CHKOvertime WHERE (OTFactor = 1) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "')  GROUP BY DivisionCode", CommandType.Text);
            
            DataSet dsTotalEarnings3 = new DataSet();
            dsTotalEarnings3 = SQLHelper.FillDataSet("SELECT 'AMOTI' AS CODE, 'Field A.M.O/T' AS Type, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '1.Total Earnings' AS Category FROM dbo.CHKOvertime WHERE  (OTFactor = 2) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);
            
            DataSet dsEPF = new DataSet();
            dsEPF = SQLHelper.FillDataSet("SELECT 'EPF' As CODE, 'EPF' AS Name, SUM(EPF12) AS EPF12, DivisionId, '2.EPF/ETF15' AS Category FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsETF = new DataSet();
            dsETF = SQLHelper.FillDataSet("SELECT 'ETF' As CODE, 'EPF/ETF15' AS Name, (SUM(ETF3)+ SUM(EPF12)) AS Amount, DivisionId, '2.EPF/ETF15' AS Category FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);
            
            DataSet dsFunds = new DataSet();
            dsFunds = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.ShortName, dbo.CHKDeduction.DeductionName, SUM(dbo.CHKFundDeduction.Amount) AS Amount, dbo.CHKFundDeduction.DivisionId, '3.Non Cost to the company' AS Category FROM dbo.CHKFundDeduction INNER JOIN dbo.CHKDeduction ON dbo.CHKFundDeduction.FundDeductionId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKFundDeduction.FundDedctYear = '" + strYear + "') AND (dbo.CHKFundDeduction.FundDeductMonth = '" + intMonth + "') GROUP BY dbo.CHKDeduction.ShortName, dbo.CHKDeduction.DeductionName, dbo.CHKFundDeduction.DivisionId", CommandType.Text);
            
            DataSet dsDeductions = new DataSet();
            dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.ShortName, dbo.CHKDeduction.DeductionName, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '4.Deductions&Payment' AS Category FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') GROUP BY dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId HAVING (NOT (dbo.CHKDeduction.ShortName IN ('APWC', 'CWC', 'EGSU', 'FUNER', 'NPWC', 'RECCBA', 'RECOB', 'UCWF')))", CommandType.Text);
            
            DataSet dsCF = new DataSet();
            dsCF = SQLHelper.FillDataSet("SELECT 'CF' AS CODE, 'Carried Forward' AS Name, SUM(MadeUpBalance) AS CF, DivisionId, '5.CF/BF' AS Category FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsBF = new DataSet();
            dsBF = SQLHelper.FillDataSet("SELECT 'BF' AS CODE, 'B/Forward' AS Name, SUM(DebitsBF) AS BF, DivisionId, '5.CF/BF' AS Category FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsQTY = new DataSet();
            dsQTY = SQLHelper.FillDataSet("SELECT 'PLKKILOS' AS CODE, 'PLUCKINGKILOS' AS Name, SUM(PluckingKilos) AS PLKKI, 'OVRKILOS' AS CODE1, 'OVER KILOS QTY' AS Name1, SUM(OverKilos) AS OVRKL, DivisionId, '1.Total Earnings' AS Category FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            Int32 colCount = 0;            
            if (dsTotalEarnings1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTotalEarnings1.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsTotalEarnings1.Tables[0].Columns)
                    {
                        DataRow drm = dtMain.NewRow();
                        if (colCount <= 15)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[18].ToString();
                                drm[4] = dr.ItemArray[19].ToString();
                                drm[5] = "0.00";
                                dtMain.Rows.Add(drm); 
                            }
                        }
                        colCount++;
                    }
                }
            }
            if (dsQTY.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in dsQTY.Tables[0].Rows)
                {
                    
                    colCount = 0;

                    foreach (DataColumn dc in dsQTY.Tables[0].Columns)
                    {
                        DataRow drm = dtMain.NewRow();
                        if (colCount <= 5)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = "0.00";
                                drm[3] = dr.ItemArray[6].ToString();
                                drm[4] = dr.ItemArray[7].ToString();
                                drm[5] = dr.ItemArray[colCount + 2].ToString();
                                dtMain.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }
                    
                }
            }
            
            if (dsTotalEarnings2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drT in dsTotalEarnings2.Tables[0].Rows)
                {
                    DataRow drm = dtMain.NewRow();
                    
                        drm[0] = drT.ItemArray[0].ToString();
                        drm[1] = drT.ItemArray[1].ToString();
                        drm[2] = drT.ItemArray[2].ToString();
                        drm[3] = drT.ItemArray[3].ToString();
                        drm[4] = drT.ItemArray[4].ToString();
                        drm[5] = "0.00";
                        dtMain.Rows.Add(drm);
                  
                }
            }
            if (dsTotalEarnings3.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTotalEarnings3.Tables[0].Rows)
                {
                    DataRow drM = dtMain.NewRow();
                  
                        drM[0] = dr.ItemArray[0].ToString();
                        drM[1] = dr.ItemArray[1].ToString();
                        drM[2] = dr.ItemArray[2].ToString();
                        drM[3] = dr.ItemArray[3].ToString();
                        drM[4] = dr.ItemArray[4].ToString();
                        drM[5] = "0.00";
                        dtMain.Rows.Add(drM);                 
                }
            }
            if (dsEPF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsEPF.Tables[0].Rows)
                {
                    DataRow drM = dtMain.NewRow();
                   
                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();
                    drM[5] = "0.00";
                    dtMain.Rows.Add(drM);
                  
                }
            }
            if (dsETF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsETF.Tables[0].Rows)
                {
                    DataRow drM = dtMain.NewRow();
                   
                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();
                    drM[5] = "0.00";
                    dtMain.Rows.Add(drM);
                    
                }
            }
            if (dsFunds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsFunds.Tables[0].Rows)
                {
                    DataRow drM = dtMain.NewRow();
                
                        drM[0] = dr.ItemArray[0].ToString();
                        drM[1] = dr.ItemArray[1].ToString();
                        drM[2] = dr.ItemArray[2].ToString();
                        drM[3] = dr.ItemArray[3].ToString();
                        drM[4] = dr.ItemArray[4].ToString();
                        drM[5] = "0.00";
                        dtMain.Rows.Add(drM);
                  
                }
            }
            if (dsDeductions.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDeductions.Tables[0].Rows)
                {
                    DataRow drM = dtMain.NewRow();
                    
                        drM[0] = dr.ItemArray[0].ToString();
                        drM[1] = dr.ItemArray[1].ToString();
                        drM[2] = dr.ItemArray[2].ToString();
                        drM[3] = dr.ItemArray[3].ToString();
                        drM[4] = dr.ItemArray[4].ToString();
                        drM[5] = "0.00";
                        dtMain.Rows.Add(drM);
                  
                }
            }
            if (dsBF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsBF.Tables[0].Rows)
                {
                    DataRow drM = dtMain.NewRow();
                   
                        drM[0] = dr.ItemArray[0].ToString();
                        drM[1] = dr.ItemArray[1].ToString();
                        drM[2] = dr.ItemArray[2].ToString();
                        drM[3] = dr.ItemArray[3].ToString();
                        drM[4] = dr.ItemArray[4].ToString();
                        drM[5] = "0.00";
                        dtMain.Rows.Add(drM);
                   
                }
            }
            if (dsCF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCF.Tables[0].Rows)
                {
                    DataRow drM = dtMain.NewRow();
                    
                        drM[0] = dr.ItemArray[0].ToString();
                        drM[1] = dr.ItemArray[1].ToString();
                        drM[2] = dr.ItemArray[2].ToString();
                        drM[3] = dr.ItemArray[3].ToString();
                        drM[4] = dr.ItemArray[4].ToString();
                        drM[5] = "0.00";
                        dtMain.Rows.Add(drM);
                   
                }
            }

            return dtMain;

        }

        public DataTable GetAmalgamation(String strYear, Int32 intMonth)
        {
            DataTable dtMain1 = new DataTable();
            DateTime dtFromDate = new DateTime(Convert.ToInt32(strYear), intMonth, 1);
            DateTime dtToDate = dtFromDate.AddMonths(1).AddDays(-1);

            dtMain1.Columns.Add(new DataColumn("NAME"));
            dtMain1.Columns.Add(new DataColumn("QTY"));
            dtMain1.Columns.Add(new DataColumn("TOTALAMOUNT"));
            dtMain1.Columns.Add(new DataColumn("DIVISION"));
            dtMain1.Columns.Add(new DataColumn("GROUP"));


            DataSet dsNormalWork = new DataSet();
            //dsNormalWork = SQLHelper.FillDataSet("SELECT 'Tea Plucking Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays) AS QTY, SUM(PluckingNamePay) AS Amount, 'Grn.Leaf Over kg' AS Name1, SUM(OverKilos) AS QTY1, SUM(OverKilosPay) AS Amount1, 'Tea Sundry Names' AS Name2, SUM(SundryManDays) + SUM(HolidaySundryManDays) AS QTY2, SUM(SundryNamePay) AS Amount2, 'Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3, 'PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, 'Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5, 'OTHERADDITIONS' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD, DivisionId, '01.CheckRoll' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);
            //dsNormalWork = SQLHelper.FillDataSet("SELECT     '1.1.Tea Plk Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays)-isnull(sum(PlkHalfNames),0) AS QTY,  SUM(PluckingNamePay)-isnull(sum(PlkHalfNamePay),0) AS Amount,  '1.2.Plk Half' AS Name,  isnull(sum(PlkHalfNames),0)AS QTY01,  isnull(SUM(PlkHalfNamePay),0) AS Amount01,  '1.3.Grn.Leaf.Okg' AS Name1, SUM(OverKilos) AS QTY1,  SUM(OverKilosPay) AS Amount1,  '1.4.TeaSundryNames' AS Name2,  SUM(SundryManDays)+ SUM(HolidaySundryManDays)-isnull(sum(SundryHalfNames),0)  AS QTY2,  SUM(SundryNamePay)-isnull(sum(SundryHalfNamePay),0) AS Amount2,  '1.5.SundryHalf' AS Name21,  isnull(sum(SundryHalfNames),0)  AS QTY21,  isnull(sum(SundryHalfNamePay),0) AS Amount21,  '3.Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3,  '4.PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, '5.Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5,  '6.OtherAditions' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD,  '4.1.PSS Pay' AS Name7, '0.00' AS QTY7, SUM(PSSAmount) AS Amount7, '7.Scrap Amount' AS Expr2, SUM(MonthlyScrapKgs) AS Expr3, SUM(MonthlyScrapKgAmount) AS ScrapAmount,  DivisionId, '01.CheckRoll' AS GROUP1 FROM         dbo.EmpMonthlyEarnings WHERE     (Year = '" + strYear + "') AND (Month ='" + intMonth + "')  GROUP BY DivisionId, Year, Month ", CommandType.Text);
            dsNormalWork = SQLHelper.FillDataSet("SELECT     '1.1.Plk/TAP Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays)-isnull(sum(PlkHalfNames),0) AS QTY,  SUM(PluckingNamePay)-isnull(sum(PlkHalfNamePay),0) AS Amount,  '1.2.Plk/TAP Half' AS Name,  isnull(sum(PlkHalfNames),0)AS QTY01,  isnull(SUM(PlkHalfNamePay),0) AS Amount01,  '1.3.Over Kilos' AS Name1, SUM(OverKilos) AS QTY1,  SUM(OverKilosPay) AS Amount1,  '1.4.SundryNames' AS Name2,  SUM(SundryManDays)+ SUM(HolidaySundryManDays)-isnull(sum(SundryHalfNames),0)  AS QTY2,  SUM(SundryNamePay)-isnull(sum(SundryHalfNamePay),0) AS Amount2,  '1.5.SundryHalf' AS Name21,  isnull(sum(SundryHalfNames),0)  AS QTY21,  isnull(sum(SundryHalfNamePay),0) AS Amount21,  '3.Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3,  '4.PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, '5.Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5,  '6.OtherAditions' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD,  '4.1.PSS Pay' AS Name7, '0.00' AS QTY7, SUM(PSSAmount) AS Amount7, '7.Scrap Amount' AS Expr2, SUM(MonthlyScrapKgs) AS Expr3, SUM(MonthlyScrapKgAmount) AS ScrapAmount,  DivisionId, '01.CheckRoll' AS GROUP1 FROM         dbo.EmpMonthlyEarnings WHERE     (Year = '" + strYear + "') AND (Month ='" + intMonth + "')  GROUP BY DivisionId, Year, Month ", CommandType.Text);


            DataSet dsRNormalWork = new DataSet();
            dsRNormalWork = SQLHelper.FillDataSet("SELECT     '2.1.Rubber Tap Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays) AS QTY, SUM(PluckingNamePay) AS Amount, '2.2.Rubber.Okg' AS Name1,  SUM(OverKilos) AS QTY1, SUM(OverKilosPay) AS Amount1, '2.3.RubberSundryNames' AS Name2, SUM(SundryManDays) + SUM(HolidaySundryManDays) AS QTY2,  SUM(SundryNamePay) AS Amount2,  DivisionId, '01.CheckRoll' AS GROUP1 FROM dbo.EmpMonthlyEarningsByCrop WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsNormalWork1 = new DataSet();
            dsNormalWork1 = SQLHelper.FillDataSet("SELECT 'Fact A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE (OTFactor = 1) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsNormalWork2 = new DataSet();
            dsNormalWork2 = SQLHelper.FillDataSet("SELECT 'Field A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE  (OTFactor = 2) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsOverTime = new DataSet();
            dsOverTime = SQLHelper.FillDataSet("SELECT 'Over Time' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode, '01.CheckRoll' AS Group1 FROM         dbo.CHKOvertime WHERE     (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsCashWork = new DataSet();
            //dsCashWork = SQLHelper.FillDataSet("SELECT 'Cash Sundry' AS Name, SUM(CashManDays) AS QTY, SUM(CashSundry) AS Amount, 'Cash Kgs' AS Name1, '0.00' AS QTY1, SUM(CashPlucking) AS Amount1, DivisionId, '02.CashWork' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);
            dsCashWork = SQLHelper.FillDataSet(" SELECT 'Cash Sundry' AS Name, SUM(CashManDays) AS QTY, SUM(CashSundry) AS Amount, 'Cash Plk/Tap' AS Name1, isnull(SUM(CashKilos),0) AS QTY1,  isnull(SUM(CashKiloAmount),0) AS Amount1, 'BlockPlucking' AS Name2, isnull(SUM(BlockKilos),0) AS QTY2, isnull(SUM(BlockKiloAmount),0) AS Amount2,  'Contract Plk' AS Name3, SUM(ContractKilos) AS QTY3, SUM(ContractorKgPay +   ContractLabourKgPay)  AS Amount3,  'Contract Sundry' AS Name4, 0.0 AS QTY4, SUM( ContractorSundryPay +  ContractLabourSundryPay)  AS Amount4, 'Cash Scrap Amount' AS cashScrap, SUM(MonthlyCashScrapKgs) AS cashScrapKgs, SUM( monthlyCashScrapKgAmount)  AS CashScrapKgAmount, DivisionId,  '02.CashWork' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsBF = new DataSet();
            dsBF = SQLHelper.FillDataSet("SELECT 'B/Forward' AS Name, '0.00' AS QTY, SUM(PreviousMadeUpCoins) AS BF, DivisionId, '03.CF/BF' AS Category FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "')   GROUP BY DivisionId, Year, Month", CommandType.Text);


            DataSet dsDeductSummery = new DataSet();
            dsDeductSummery = SQLHelper.FillDataSet("SELECT right('00'+convert(varchar(10),dbo.CHKDeductionGroup.DeductionGroupCode),2)+'.'+dbo.CHKDeductionGroup.GroupName AS Name, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '06 Deductions Summery' AS Group1, dbo.CHKDeductionGroup.ShortName, right('00'+dbo.CHKDeductionGroup.DeductionGroupCode,2) as GroupCode FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "')  GROUP BY  dbo.CHKDeductionGroup.DeductionGroupCode,dbo.CHKDeductionGroup.GroupName, dbo.CHKDeductionGroup.ShortName, dbo.CHKEmpDeductions.DivisionId order by GroupCode", CommandType.Text);

            DataSet dsPreDebits = new DataSet();
            dsPreDebits = SQLHelper.FillDataSet("SELECT 'Previous Debits' AS Name, '0.00' AS QTY, SUM(PreviousDebits) AS PD, DivisionId, '05.Previous Debits' AS Category FROM dbo.EmpMonthlyDeductions WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsTotDeduc = new DataSet();
            dsTotDeduc = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalDeductions) AS TotDeduc, DivisionId, '07.TotalDeduc' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear ='" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, TotalDeductions", CommandType.Text);

            DataSet dsCF = new DataSet();
            dsCF = SQLHelper.FillDataSet("SELECT 'Carried Forward' AS Name, '0.00' AS QTY, SUM(MadeUpBalance) AS CF, DivisionId, '08.CF/BF' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);
            
            DataSet ThisMonthDebts = new DataSet();
            ThisMonthDebts = SQLHelper.FillDataSet("SELECT 'Debts C/F' AS Name, '0.00' AS QTY, SUM(DebitsBF) AS DebitsBF, DivisionId, '09.1. Debts C/F' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsPFContributions = new DataSet();
            dsPFContributions = SQLHelper.FillDataSet("SELECT 'Member (10%)' AS Name, '0.00' AS QTY, SUM(EPF10) AS Amount, 'Estate (12%)' AS Name1, '0.00' AS QTY, SUM(EPF12) AS Amount1, 'Total (22%)' AS Name2, '0.00' AS QTY, SUM(EPF10) + SUM(EPF12) AS Amount2, 'ETF (3%)' AS Name3, '0.00' AS QTY, SUM(ETF3) AS Amount3, DivisionId, '10.EPF Contributions Member' AS Group1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsNetPay = new DataSet();
            dsNetPay = SQLHelper.FillDataSet("SELECT '' AS Name, '0.0' AS Qty, SUM(TotalEarnigs) AS Earn, '04.GROSS PAY' AS Group1, DivisionId FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsNetPay2 = new DataSet();
            dsNetPay2 = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalEarnigs) AS Earnigs, '' AS Name2, '0.00' AS QTY2, SUM(TotalDeductions) AS Deductions, '' AS Name3,  '0.00' AS QTY3, SUM(SalaryAmount) AS Balance, '09.Net Pay' AS Group1, DivisionId FROM   dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "')GROUP BY DivisionId", CommandType.Text);

            DataSet dsBalance = new DataSet();
            dsBalance = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(WagePay) AS BAL, DivisionId, '09.0.Net Pay' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsAdditions = new DataSet();
            dsAdditions = SQLHelper.FillDataSet("SELECT dbo.CHKAddition.AdditionShortName AS ADDITION, '0.00' AS QTY, SUM(dbo.CHKEmpAdditions.Amount) AS AdditionAmt, dbo.CHKEmpAdditions.DivisionID, 'a1.Additions' AS Group1 FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.AdditionYear = '" + strYear + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonth + "') GROUP BY dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.DivisionID", CommandType.Text);

            DataSet dsOT = new DataSet();
            dsOT = SQLHelper.FillDataSet("SELECT  dbo.CHKOTParameters.OTType AS OverTime, '0.0' AS QTY, SUM(dbo.CHKOvertime.Expenditure) AS OTAmount, dbo.CHKOvertime.DivisionCode, 'a3.Over Time' AS Group1 FROM dbo.CHKOvertime INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId WHERE (YEAR(dbo.CHKOvertime.OtDate) = '" + strYear + "') AND (MONTH(dbo.CHKOvertime.OtDate) = '" + intMonth + "') GROUP BY dbo.CHKOTParameters.OTType, dbo.CHKOvertime.DivisionCode", CommandType.Text);

            DataSet dsCashPlk = new DataSet();
            dsCashPlk = SQLHelper.FillDataSet("SELECT     'Cash Plucking' AS Name1, CASE WHEN (CashPlkOkgYesNo = 0) THEN ISNULL(SUM(CashKgs), 0) ELSE 0 END AS QTY1, CASE WHEN (CashPlkOkgYesNo = 0)  THEN ISNULL(SUM(CashKgAmount), 0) ELSE 0 END AS Amount1,   DivisionID, 'a4. Cash Plucking' AS GROUP1 FROM dbo.DailyGroundTransactions GROUP BY YEAR(DateEntered), MONTH(DateEntered), DivisionID, CashPlkOkgYesNo HAVING      (YEAR(DateEntered) = '" + strYear + "') AND (MONTH(DateEntered) = '" + intMonth + "') and (CashPlkOkgYesNo=0) ", CommandType.Text);

            DataSet dsCashNamePlk = new DataSet();
            dsCashNamePlk = SQLHelper.FillDataSet(" SELECT      'Cash Name Plk Kilos' AS Name2, CASE WHEN (CashPlkOkgYesNo = 1)  THEN ISNULL(SUM(CashKgs), 0) ELSE 0 END AS QTY2, CASE WHEN (CashPlkOkgYesNo = 1) THEN ISNULL(SUM(CashKgAmount), 0) ELSE 0 END AS Amount2,  DivisionID, 'a6. Cash Name Plk' AS GROUP1 FROM         dbo.DailyGroundTransactions GROUP BY YEAR(DateEntered), MONTH(DateEntered), DivisionID, CashPlkOkgYesNo HAVING      (YEAR(DateEntered) = '" + strYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')  and (CashPlkOkgYesNo=1) ", CommandType.Text);

            DataSet dsCashNamePlkManDays = new DataSet();
            dsCashNamePlkManDays = SQLHelper.FillDataSet("  SELECT      'Cash Name Plk MDays' AS Name2, CASE WHEN (CashPlkOkgYesNo = 1)  THEN ISNULL(SUM(CashManDays), 0) ELSE 0 END AS QTY2, CASE WHEN (CashPlkOkgYesNo = 1) THEN ISNULL(SUM(CashSundryAmount), 0) ELSE 0 END AS Amount2,  DivisionID, 'a8. Cash Name Plk' AS GROUP1 FROM         dbo.DailyGroundTransactions GROUP BY YEAR(DateEntered), MONTH(DateEntered), DivisionID, CashPlkOkgYesNo HAVING      (YEAR(DateEntered) = '" + strYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')  and (CashPlkOkgYesNo=1)", CommandType.Text);

            DataSet dsCashNamePlkOkg = new DataSet();
            dsCashNamePlkOkg = SQLHelper.FillDataSet("  SELECT      'Cash Name Plk Okgs' AS Name2, CASE WHEN (CashPlkOkgYesNo = 1)  THEN ISNULL(SUM(OverKgs), 0) ELSE 0 END AS QTY2, CASE WHEN (CashPlkOkgYesNo = 1) THEN ISNULL(SUM(CashSundryAmount), 0) ELSE 0 END AS Amount2,  DivisionID, 'a9. Cash Name Plk' AS GROUP1 FROM         dbo.DailyGroundTransactions GROUP BY YEAR(DateEntered), MONTH(DateEntered), DivisionID, CashPlkOkgYesNo HAVING      (YEAR(DateEntered) = '" + strYear + "') AND (MONTH(DateEntered) = '" + intMonth + "')  and (CashPlkOkgYesNo=1)", CommandType.Text);


            DataSet dsCashSundry = new DataSet();
            dsCashSundry = SQLHelper.FillDataSet("SELECT     'Cash Sundry' AS Name1, CASE WHEN (CashPlkOkgYesNo = 0) THEN ISNULL(SUM(CashManDays), 0) ELSE 0 END AS QTY1, CASE WHEN (CashPlkOkgYesNo = 0)  THEN ISNULL(SUM(CashSundryAmount), 0) ELSE 0 END AS Amount1,   DivisionID, 'a5. Cash Sundry' AS GROUP1 FROM dbo.DailyGroundTransactions GROUP BY YEAR(DateEntered), MONTH(DateEntered), DivisionID, CashPlkOkgYesNo HAVING      (YEAR(DateEntered) = '" + strYear + "') AND (MONTH(DateEntered) = '" + intMonth + "') and (CashPlkOkgYesNo=0) ", CommandType.Text);


            //Crop wise note-Tea
            DataSet dsCropWiseEarningsTea = new DataSet();
            //dsNormalWork = SQLHelper.FillDataSet("SELECT 'Tea Plucking Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays) AS QTY, SUM(PluckingNamePay) AS Amount, 'Grn.Leaf Over kg' AS Name1, SUM(OverKilos) AS QTY1, SUM(OverKilosPay) AS Amount1, 'Tea Sundry Names' AS Name2, SUM(SundryManDays) + SUM(HolidaySundryManDays) AS QTY2, SUM(SundryNamePay) AS Amount2, 'Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3, 'PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, 'Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5, 'OTHERADDITIONS' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD, DivisionId, '01.CheckRoll' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);
            //dsNormalWork = SQLHelper.FillDataSet("SELECT     '1.1.Tea Plk Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays)-isnull(sum(PlkHalfNames),0) AS QTY,  SUM(PluckingNamePay)-isnull(sum(PlkHalfNamePay),0) AS Amount,  '1.2.Plk Half' AS Name,  isnull(sum(PlkHalfNames),0)AS QTY01,  isnull(SUM(PlkHalfNamePay),0) AS Amount01,  '1.3.Grn.Leaf.Okg' AS Name1, SUM(OverKilos) AS QTY1,  SUM(OverKilosPay) AS Amount1,  '1.4.TeaSundryNames' AS Name2,  SUM(SundryManDays)+ SUM(HolidaySundryManDays)-isnull(sum(SundryHalfNames),0)  AS QTY2,  SUM(SundryNamePay)-isnull(sum(SundryHalfNamePay),0) AS Amount2,  '1.5.SundryHalf' AS Name21,  isnull(sum(SundryHalfNames),0)  AS QTY21,  isnull(sum(SundryHalfNamePay),0) AS Amount21,  '3.Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3,  '4.PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, '5.Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5,  '6.OtherAditions' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD,  '4.1.PSS Pay' AS Name7, '0.00' AS QTY7, SUM(PSSAmount) AS Amount7, '7.Scrap Amount' AS Expr2, SUM(MonthlyScrapKgs) AS Expr3, SUM(MonthlyScrapKgAmount) AS ScrapAmount,  DivisionId, '01.CheckRoll' AS GROUP1 FROM         dbo.EmpMonthlyEarnings WHERE     (Year = '" + strYear + "') AND (Month ='" + intMonth + "')  GROUP BY DivisionId, Year, Month ", CommandType.Text);
            dsCropWiseEarningsTea = SQLHelper.FillDataSet("SELECT         '01.DailyBasic' AS WagesName, SUM(ManDays) AS ManDays, SUM(DailyBasic) AS DailyBasic, '02.OverKilos' AS OkgName, SUM(OverKgs) AS OverKgs,  SUM(OverKgAmount) AS OverKgAmount, '03.CashKilos' AS CashKilosName, SUM(CashKgs) AS CashKgs, SUM(CashKgAmount) AS CashKgAmount,  '04.Cash Sundry' AS CashSunName, SUM(CashManDays) AS CashSunDays, SUM(CashSundryAmount) AS CashSundry, '05.Att.Incentive' AS incentiveName,  0 AS IncenQty, SUM(IncentiveAmount) AS Incentive, '06.ExtraRates' AS exRatesName, 0 AS ExRateQty, SUM(ExtraRates) AS ExtraRates, '07.PRI' AS PRIName,  0 AS PRIQty, SUM(PRIAmount) AS PRI, '08.PSS' AS PSSName, 0 AS PSSQty, SUM(PSSAmount) AS PSS, DivisionID, CASE WHEN (CropType = 1)  THEN 'Tea' ELSE CASE WHEN (CropType = 2) THEN 'Rubber' ELSE CASE WHEN (CropType = 3) THEN 'Oil Palm' ELSE 'Other' END END END AS Crop,  CASE WHEN (WorkCodeID = 'PLK') THEN 'Plucking' ELSE CASE WHEN (WorkCodeID = 'TAP') THEN 'Tapping' ELSE CASE WHEN (WorkCodeID = 'OHV')  THEN 'Harvesting' ELSE 'Sundry' END END END AS HarvestSundry FROM            dbo.DailyGroundTransactions WHERE        (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtToDate + "', 102)) AND (CropType = 1) GROUP BY DivisionID, CropType, CASE WHEN (WorkCodeID = 'PLK') THEN 'Plucking' ELSE CASE WHEN (WorkCodeID = 'TAP')  THEN 'Tapping' ELSE CASE WHEN (WorkCodeID = 'OHV') THEN 'Harvesting' ELSE 'Sundry' END END END", CommandType.Text);

            //Crop wise note-Rubber
            DataSet dsCropWiseEarningsRubber = new DataSet();
            dsCropWiseEarningsRubber = SQLHelper.FillDataSet("SELECT         '01.DailyBasic' AS WagesName, SUM(ManDays) AS ManDays, SUM(DailyBasic) AS DailyBasic, '02.OverKilos' AS OkgName, SUM(OverKgs) AS OverKgs,  SUM(OverKgAmount) AS OverKgAmount, '03.CashKilos' AS CashKilosName, SUM(CashKgs) AS CashKgs, SUM(CashKgAmount) AS CashKgAmount,  '04.Cash Sundry' AS CashSunName, SUM(CashManDays) AS CashSunDays, SUM(CashSundryAmount) AS CashSundry, '05.Att.Incentive' AS incentiveName,  0 AS IncenQty, SUM(IncentiveAmount) AS Incentive, '06.ExtraRates' AS exRatesName, 0 AS ExRateQty, SUM(ExtraRates) AS ExtraRates, '07.PRI' AS PRIName,  0 AS PRIQty, SUM(PRIAmount) AS PRI, '08.PSS' AS PSSName, 0 AS PSSQty, SUM(PSSAmount) AS PSS, DivisionID, CASE WHEN (CropType = 1)  THEN 'Tea' ELSE CASE WHEN (CropType = 2) THEN 'Rubber' ELSE CASE WHEN (CropType = 3) THEN 'Oil Palm' ELSE 'Other' END END END AS Crop,  CASE WHEN (WorkCodeID = 'PLK') THEN 'Plucking' ELSE CASE WHEN (WorkCodeID = 'TAP') THEN 'Tapping' ELSE CASE WHEN (WorkCodeID = 'OHV')  THEN 'Harvesting' ELSE 'Sundry' END END END AS HarvestSundry FROM            dbo.DailyGroundTransactions WHERE        (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtToDate + "', 102)) AND (CropType = 2) GROUP BY DivisionID, CropType, CASE WHEN (WorkCodeID = 'PLK') THEN 'Plucking' ELSE CASE WHEN (WorkCodeID = 'TAP')  THEN 'Tapping' ELSE CASE WHEN (WorkCodeID = 'OHV') THEN 'Harvesting' ELSE 'Sundry' END END END", CommandType.Text);
            //Crop wise note-OilPalm
            DataSet dsCropWiseEarningsOP = new DataSet();
            dsCropWiseEarningsOP = SQLHelper.FillDataSet("SELECT         '01.DailyBasic' AS WagesName, SUM(ManDays) AS ManDays, SUM(DailyBasic) AS DailyBasic, '02.OverKilos' AS OkgName, SUM(OverKgs) AS OverKgs,  SUM(OverKgAmount) AS OverKgAmount, '03.CashKilos' AS CashKilosName, SUM(CashKgs) AS CashKgs, SUM(CashKgAmount) AS CashKgAmount,  '04.Cash Sundry' AS CashSunName, SUM(CashManDays) AS CashSunDays, SUM(CashSundryAmount) AS CashSundry, '05.Att.Incentive' AS incentiveName,  0.00 AS IncenQty, SUM(IncentiveAmount) AS Incentive, '06.ExtraRates' AS exRatesName, 0.00 AS ExRateQty, SUM(ExtraRates) AS ExtraRates, '07.PRI' AS PRIName,  0.00 AS PRIQty, SUM(PRIAmount) AS PRI, '08.PSS' AS PSSName, 0.00 AS PSSQty, SUM(PSSAmount) AS PSS, DivisionID, CASE WHEN (CropType = 1)  THEN 'Tea' ELSE CASE WHEN (CropType = 2) THEN 'Rubber' ELSE CASE WHEN (CropType = 3) THEN 'Oil Palm' ELSE 'Other' END END END AS Crop,  CASE WHEN (WorkCodeID = 'PLK') THEN 'Plucking' ELSE CASE WHEN (WorkCodeID = 'TAP') THEN 'Tapping' ELSE CASE WHEN (WorkCodeID = 'OHV')  THEN 'Harvesting' ELSE 'Sundry' END END END AS HarvestSundry FROM            dbo.DailyGroundTransactions WHERE        (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtToDate + "', 102)) AND (CropType = 3) GROUP BY DivisionID, CropType, CASE WHEN (WorkCodeID = 'PLK') THEN 'Plucking' ELSE CASE WHEN (WorkCodeID = 'TAP')  THEN 'Tapping' ELSE CASE WHEN (WorkCodeID = 'OHV') THEN 'Harvesting' ELSE 'Sundry' END END END", CommandType.Text);


            Int32 colCount = 0;
            if (dsNormalWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNormalWork.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsNormalWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 30)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[33].ToString();
                                drm[4] = dr.ItemArray[34].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }
                }
            }
            //rubber
            colCount = 0;
            if (dsRNormalWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsRNormalWork.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsRNormalWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 6)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[9].ToString();
                                drm[4] = dr.ItemArray[10].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }
                }
            }
            if (dsOverTime.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOverTime.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsCashWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCashWork.Tables[0].Rows)
                {
                    colCount = 0;

                    foreach (DataColumn dc in dsCashWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 15)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[18].ToString();
                                drm[4] = dr.ItemArray[19].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }

                }
            }

            if (dsBF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drT in dsBF.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = drT.ItemArray[0].ToString();
                    drm[1] = drT.ItemArray[1].ToString();
                    drm[2] = drT.ItemArray[2].ToString();
                    drm[3] = drT.ItemArray[3].ToString();
                    drm[4] = drT.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsNetPay.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNetPay.Tables[0].Rows)
                {
                    //colCount = 0;

                    //foreach (DataColumn dc in dsNetPay.Tables[0].Columns)
                    //{
                    DataRow drm = dtMain1.NewRow();
                    //    if (colCount <= 0)
                    //    {
                    //if (colCount % 3 == 0)
                    //{
                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[4].ToString();
                    drm[4] = dr.ItemArray[3].ToString();
                    dtMain1.Rows.Add(drm);
                    //}
                    //}
                    //colCount++;
                    //}
                }
            }

            if (dsPreDebits.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPreDebits.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }

            if (dsDeductSummery.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDeductSummery.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }
            DataSet dsDeductions = new DataSet();
            DataSet dsDeductGroups = new DataSet();
            dsDeductGroups = SQLHelper.FillDataSet("SELECT DeductionGroupCode, ShortName FROM dbo.CHKDeductionGroup order by DeductionGroupCode ", CommandType.Text);
            foreach (DataRow dr in dsDeductGroups.Tables[0].Rows)
            {

                //dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '5.Deductions&Payment' AS Group1 FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') GROUP BY dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ", CommandType.Text);
                dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, 'a2.'+right('00'+'" + dr.ItemArray[0].ToString() + "',2)+'" + dr.ItemArray[1].ToString() + "' AS Group1, dbo.CHKDeduction.DeductionGroupCode,'" + dr.ItemArray[0].ToString() + "' as no  FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') and  (dbo.CHKDeduction.DeductionGroupCode = '" + Convert.ToInt32(dr.ItemArray[0].ToString()) + "') GROUP BY dbo.CHKDeduction.DeductionGroupCode,dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ORDER BY no,dbo.CHKDeduction.DeductionGroupCode", CommandType.Text);
                if (dsDeductions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsDeductions.Tables[0].Rows)
                    {
                        DataRow drM = dtMain1.NewRow();

                        drM[0] = dr1.ItemArray[0].ToString();
                        drM[1] = dr1.ItemArray[1].ToString();
                        drM[2] = dr1.ItemArray[2].ToString();
                        drM[3] = dr1.ItemArray[3].ToString();
                        drM[4] = dr1.ItemArray[4].ToString();


                        dtMain1.Rows.Add(drM);
                    }
                }

            }

            if (dsTotDeduc.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTotDeduc.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }

            }
            if (dsCF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCF.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drM);

                }
            }
            //if (ThisMonthDebts.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ThisMonthDebts.Tables[0].Rows)
            //    {
            //        DataRow drM = dtMain1.NewRow();

            //        drM[0] = dr.ItemArray[0].ToString();
            //        drM[1] = dr.ItemArray[1].ToString();
            //        drM[2] = dr.ItemArray[2].ToString();
            //        drM[3] = dr.ItemArray[3].ToString();
            //        drM[4] = dr.ItemArray[4].ToString();

            //        dtMain1.Rows.Add(drM);

            //    }
            //}
            if (dsBalance.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsBalance.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }
            }
            if (ThisMonthDebts.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ThisMonthDebts.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drM);

                }
            }
            if (dsPFContributions.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPFContributions.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsPFContributions.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 9)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[12].ToString();
                                drm[4] = dr.ItemArray[13].ToString();
                                dtMain1.Rows.Add(drm);

                            }
                        }
                        colCount++;
                    }
                }

                //Crop wise breakdown-Tea
                Int32 colCount1 = 0;
                if (dsCropWiseEarningsTea.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCropWiseEarningsTea.Tables[0].Rows)
                    {
                        colCount1 = 0;
                        foreach (DataColumn dc in dsCropWiseEarningsTea.Tables[0].Columns)
                        {
                            DataRow drm = dtMain1.NewRow();
                            if (colCount1 < 24)
                            {
                                if (colCount1 % 3 == 0)
                                {
                                    drm[0] = dr.ItemArray[colCount1].ToString();
                                    drm[1] = dr.ItemArray[colCount1 + 1].ToString();
                                    drm[2] = dr.ItemArray[colCount1 + 2].ToString();
                                    drm[3] = dr.ItemArray[24].ToString();
                                    drm[4] = "a0.Cropwise Earnings Tea";
                                    dtMain1.Rows.Add(drm);
                                }
                            }
                            colCount1++;
                        }
                    }
                }
                //Crop wise breakdown-Rubber
                colCount1 = 0;
                if (dsCropWiseEarningsRubber.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCropWiseEarningsRubber.Tables[0].Rows)
                    {
                        colCount1 = 0;
                        foreach (DataColumn dc in dsCropWiseEarningsRubber.Tables[0].Columns)
                        {
                            DataRow drm = dtMain1.NewRow();
                            if (colCount1 < 24)
                            {
                                if (colCount1 % 3 == 0)
                                {
                                    drm[0] = dr.ItemArray[colCount1].ToString();
                                    drm[1] = dr.ItemArray[colCount1 + 1].ToString();
                                    drm[2] = dr.ItemArray[colCount1 + 2].ToString();
                                    drm[3] = dr.ItemArray[24].ToString();
                                    drm[4] = "a0.Cropwise Earnings Rubber";
                                    dtMain1.Rows.Add(drm);
                                }
                            }
                            colCount1++;
                        }
                    }
                }
                //Crop wise breakdown-OP
                colCount1 = 0;
                if (dsCropWiseEarningsOP.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCropWiseEarningsOP.Tables[0].Rows)
                    {
                        colCount1 = 0;
                        foreach (DataColumn dc in dsCropWiseEarningsOP.Tables[0].Columns)
                        {
                            DataRow drm = dtMain1.NewRow();
                            if (colCount1 < 24)
                            {
                                if (colCount1 % 3 == 0)
                                {
                                    drm[0] = dr.ItemArray[colCount1].ToString();
                                    drm[1] = dr.ItemArray[colCount1 + 1].ToString();
                                    drm[2] = dr.ItemArray[colCount1 + 2].ToString();
                                    drm[3] = dr.ItemArray[24].ToString();
                                    drm[4] = "a0.Cropwise Earnings OP";
                                    dtMain1.Rows.Add(drm);
                                }
                            }
                            colCount1++;
                        }
                    }
                }

                if (dsAdditions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsAdditions.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = dr.ItemArray[0].ToString();
                        drm[1] = dr.ItemArray[1].ToString();
                        drm[2] = dr.ItemArray[2].ToString();
                        drm[3] = dr.ItemArray[3].ToString();
                        drm[4] = dr.ItemArray[4].ToString();
                        dtMain1.Rows.Add(drm);
                    }

                }
                if (dsOT.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsOT.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = dr.ItemArray[0].ToString();
                        drm[1] = dr.ItemArray[1].ToString();
                        drm[2] = dr.ItemArray[2].ToString();
                        drm[3] = dr.ItemArray[3].ToString();
                        drm[4] = dr.ItemArray[4].ToString();
                        dtMain1.Rows.Add(drm);
                    }

                }

                if (dsCashPlk.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drT in dsCashPlk.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = drT.ItemArray[0].ToString();
                        drm[1] = drT.ItemArray[1].ToString();
                        drm[2] = drT.ItemArray[2].ToString();
                        drm[3] = drT.ItemArray[3].ToString();
                        drm[4] = drT.ItemArray[4].ToString();

                        dtMain1.Rows.Add(drm);

                    }
                }

                if (dsCashSundry.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drT in dsCashSundry.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = drT.ItemArray[0].ToString();
                        drm[1] = drT.ItemArray[1].ToString();
                        drm[2] = drT.ItemArray[2].ToString();
                        drm[3] = drT.ItemArray[3].ToString();
                        drm[4] = drT.ItemArray[4].ToString();

                        dtMain1.Rows.Add(drm);

                    }
                }

                if (dsCashNamePlk.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drT in dsCashNamePlk.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = drT.ItemArray[0].ToString();
                        drm[1] = drT.ItemArray[1].ToString();
                        drm[2] = drT.ItemArray[2].ToString();
                        drm[3] = drT.ItemArray[3].ToString();
                        drm[4] = drT.ItemArray[4].ToString();

                        dtMain1.Rows.Add(drm);

                    }
                }

                if (dsCashNamePlkManDays.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drT in dsCashNamePlkManDays.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = drT.ItemArray[0].ToString();
                        drm[1] = drT.ItemArray[1].ToString();
                        drm[2] = drT.ItemArray[2].ToString();
                        drm[3] = drT.ItemArray[3].ToString();
                        drm[4] = drT.ItemArray[4].ToString();

                        dtMain1.Rows.Add(drm);

                    }
                }

                if (dsCashNamePlkOkg.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drT in dsCashNamePlkOkg.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = drT.ItemArray[0].ToString();
                        drm[1] = drT.ItemArray[1].ToString();
                        drm[2] = drT.ItemArray[2].ToString();
                        drm[3] = drT.ItemArray[3].ToString();
                        drm[4] = drT.ItemArray[4].ToString();

                        dtMain1.Rows.Add(drm);

                    }
                }
            }
            return dtMain1;
        }
        public DataTable GetAmalgamationOld(String strYear, Int32 intMonth)
        {

            DataTable dtMain1 = new DataTable();

            dtMain1.Columns.Add(new DataColumn("NAME"));
            dtMain1.Columns.Add(new DataColumn("QTY"));
            dtMain1.Columns.Add(new DataColumn("TOTALAMOUNT"));
            dtMain1.Columns.Add(new DataColumn("DIVISION"));
            dtMain1.Columns.Add(new DataColumn("GROUP"));


            DataSet dsNormalWork = new DataSet();
            dsNormalWork = SQLHelper.FillDataSet("SELECT 'Tea Plucking Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays) AS QTY, SUM(PluckingNamePay) AS Amount, 'Grn.Leaf Over kg' AS Name1, SUM(OverKilos) AS QTY1, SUM(OverKilosPay) AS Amount1, 'Tea Sundry Names' AS Name2, SUM(SundryManDays) + SUM(HolidaySundryManDays) AS QTY2, SUM(SundryNamePay) AS Amount2, 'Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3, 'PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, 'Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5, 'OTHERADDITIONS' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD, DivisionId, '01.CheckRoll' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsNormalWork1 = new DataSet();
            dsNormalWork1 = SQLHelper.FillDataSet("SELECT 'Fact A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE (OTFactor = 1) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsNormalWork2 = new DataSet();
            dsNormalWork2 = SQLHelper.FillDataSet("SELECT 'Field A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE  (OTFactor = 2) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsOverTime = new DataSet();
            dsOverTime = SQLHelper.FillDataSet("SELECT     'Over Time' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode, '01.CheckRoll' AS Group1 FROM         dbo.CHKOvertime WHERE     (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsCashWork = new DataSet();
            dsCashWork = SQLHelper.FillDataSet("SELECT 'Cash Sundry' AS Name, SUM(CashManDays) AS QTY, SUM(CashSundry) AS Amount, 'Cash Kgs' AS Name1, '0.00' AS QTY1, SUM(CashPlucking) AS Amount1, DivisionId, '02.CashWork' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsBF = new DataSet();
            dsBF = SQLHelper.FillDataSet("SELECT     'B/Forward' AS Name, '0.00' AS QTY, SUM(PreviousMadeUpCoins) AS BF, DivisionId, '03.CF/BF' AS Category FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "')   GROUP BY DivisionId, Year, Month", CommandType.Text);


            DataSet dsDeductSummery = new DataSet();
            dsDeductSummery = SQLHelper.FillDataSet("SELECT right('00'+convert(varchar(10),dbo.CHKDeductionGroup.DeductionGroupCode),2)+'.'+dbo.CHKDeductionGroup.GroupName AS Name, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '06 Deductions Summery' AS Group1, dbo.CHKDeductionGroup.ShortName, right('00'+dbo.CHKDeductionGroup.DeductionGroupCode,2) as GroupCode FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "')  GROUP BY  dbo.CHKDeductionGroup.DeductionGroupCode,dbo.CHKDeductionGroup.GroupName, dbo.CHKDeductionGroup.ShortName, dbo.CHKEmpDeductions.DivisionId order by GroupCode", CommandType.Text);

            DataSet dsPreDebits = new DataSet();
            dsPreDebits = SQLHelper.FillDataSet("SELECT 'Previous Debits' AS Name, '0.00' AS QTY, SUM(PreviousDebits) AS PD, DivisionId, '05.Previous Debits' AS Category FROM dbo.EmpMonthlyDeductions WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsTotDeduc = new DataSet();
            dsTotDeduc = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalDeductions) AS TotDeduc, DivisionId, '07.TotalDeduc' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear ='" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, TotalDeductions", CommandType.Text);

            DataSet dsCF = new DataSet();
            dsCF = SQLHelper.FillDataSet("SELECT 'Carried Forward' AS Name, '0.00' AS QTY, SUM(MadeUpBalance) AS CF, DivisionId, '08.CF/BF' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsPFContributions = new DataSet();
            dsPFContributions = SQLHelper.FillDataSet("SELECT 'Member (10%)' AS Name, '0.00' AS QTY, SUM(EPF10) AS Amount, 'Estate (12%)' AS Name1, '0.00' AS QTY, SUM(EPF12) AS Amount1, 'Total (22%)' AS Name2, '0.00' AS QTY, SUM(EPF10) + SUM(EPF12) AS Amount2, 'ETF (3%)' AS Name3, '0.00' AS QTY, SUM(ETF3) AS Amount3, DivisionId, '10.EPF Contributions Member' AS Group1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsNetPay = new DataSet();
            dsNetPay = SQLHelper.FillDataSet("SELECT '' AS Name, '0.0' AS Qty, SUM(TotalEarnigs) AS Earn, '04.GROSS PAY' AS Group1, DivisionId FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsNetPay2 = new DataSet();
            dsNetPay2 = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalEarnigs) AS Earnigs, '' AS Name2, '0.00' AS QTY2, SUM(TotalDeductions) AS Deductions, '' AS Name3,  '0.00' AS QTY3, SUM(SalaryAmount) AS Balance, '09.Net Pay' AS Group1, DivisionId FROM   dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "')GROUP BY DivisionId", CommandType.Text);

            DataSet dsBalance = new DataSet();
            dsBalance = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(WagePay) AS BAL, DivisionId, '09.Net Pay' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsAdditions = new DataSet();
            dsAdditions = SQLHelper.FillDataSet("SELECT dbo.CHKAddition.AdditionShortName AS ADDITION, '0.00' AS QTY, SUM(dbo.CHKEmpAdditions.Amount) AS AdditionAmt, dbo.CHKEmpAdditions.DivisionID, 'a1.Additions' AS Group1 FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.AdditionYear = '" + strYear + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonth + "') GROUP BY dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.DivisionID", CommandType.Text);

            Int32 colCount = 0;
            if (dsNormalWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNormalWork.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsNormalWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 18)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[21].ToString();
                                drm[4] = dr.ItemArray[22].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }
                }
            }
            if (dsOverTime.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOverTime.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsCashWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCashWork.Tables[0].Rows)
                {
                    colCount = 0;

                    foreach (DataColumn dc in dsCashWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 3)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[6].ToString();
                                drm[4] = dr.ItemArray[7].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }

                }
            }


            if (dsBF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drT in dsBF.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = drT.ItemArray[0].ToString();
                    drm[1] = drT.ItemArray[1].ToString();
                    drm[2] = drT.ItemArray[2].ToString();
                    drm[3] = drT.ItemArray[3].ToString();
                    drm[4] = drT.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsNetPay.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNetPay.Tables[0].Rows)
                {
                    //colCount = 0;

                    //foreach (DataColumn dc in dsNetPay.Tables[0].Columns)
                    //{
                    DataRow drm = dtMain1.NewRow();
                    //    if (colCount <= 0)
                    //    {
                    //if (colCount % 3 == 0)
                    //{
                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[4].ToString();
                    drm[4] = dr.ItemArray[3].ToString();
                    dtMain1.Rows.Add(drm);
                    //}
                    //}
                    //colCount++;
                    //}
                }
            }

            if (dsPreDebits.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPreDebits.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }

            if (dsDeductSummery.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDeductSummery.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }
            DataSet dsDeductions = new DataSet();
            DataSet dsDeductGroups = new DataSet();
            dsDeductGroups = SQLHelper.FillDataSet("SELECT DeductionGroupCode, ShortName FROM dbo.CHKDeductionGroup order by DeductionGroupCode ", CommandType.Text);
            foreach (DataRow dr in dsDeductGroups.Tables[0].Rows)
            {

                //dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '5.Deductions&Payment' AS Group1 FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') GROUP BY dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ", CommandType.Text);
                dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, 'a2.'+right('00'+'" + dr.ItemArray[0].ToString() + "',2)+'" + dr.ItemArray[1].ToString() + "' AS Group1, dbo.CHKDeduction.DeductionGroupCode,'" + dr.ItemArray[0].ToString() + "' as no  FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') and  (dbo.CHKDeduction.DeductionGroupCode = '" + Convert.ToInt32(dr.ItemArray[0].ToString()) + "') GROUP BY dbo.CHKDeduction.DeductionGroupCode,dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ORDER BY no,dbo.CHKDeduction.DeductionGroupCode", CommandType.Text);
                if (dsDeductions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsDeductions.Tables[0].Rows)
                    {
                        DataRow drM = dtMain1.NewRow();

                        drM[0] = dr1.ItemArray[0].ToString();
                        drM[1] = dr1.ItemArray[1].ToString();
                        drM[2] = dr1.ItemArray[2].ToString();
                        drM[3] = dr1.ItemArray[3].ToString();
                        drM[4] = dr1.ItemArray[4].ToString();


                        dtMain1.Rows.Add(drM);
                    }
                }

            }

            if (dsTotDeduc.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTotDeduc.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }

            }
            if (dsCF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCF.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drM);

                }
            }
            if (dsBalance.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsBalance.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }
            }
            if (dsPFContributions.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPFContributions.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsPFContributions.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 9)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[12].ToString();
                                drm[4] = dr.ItemArray[13].ToString();
                                dtMain1.Rows.Add(drm);

                            }
                        }
                        colCount++;
                    }
                }

                if (dsAdditions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsAdditions.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = dr.ItemArray[0].ToString();
                        drm[1] = dr.ItemArray[1].ToString();
                        drm[2] = dr.ItemArray[2].ToString();
                        drm[3] = dr.ItemArray[3].ToString();
                        drm[4] = dr.ItemArray[4].ToString();
                        dtMain1.Rows.Add(drm);
                    }

                }
            }
            return dtMain1;
        }

        public DataTable GetAmalgamationDashBoard(String strYear, Int32 intMonth)
        {

            DataTable dtMain1 = new DataTable();

            dtMain1.Columns.Add(new DataColumn("NAME"));
            dtMain1.Columns.Add(new DataColumn("QTY"));
            dtMain1.Columns.Add(new DataColumn("TOTALAMOUNT"));
            dtMain1.Columns.Add(new DataColumn("DIVISION"));
            dtMain1.Columns.Add(new DataColumn("GROUP"));


            DataSet dsNormalWork = new DataSet();
            //dsNormalWork = SQLHelper.FillDataSet("SELECT 'Tea Plucking Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays) AS QTY, SUM(PluckingNamePay) AS Amount, 'Grn.Leaf Over kg' AS Name1, SUM(OverKilos) AS QTY1, SUM(OverKilosPay) AS Amount1, 'Tea Sundry Names' AS Name2, SUM(SundryManDays) + SUM(HolidaySundryManDays) AS QTY2, SUM(SundryNamePay) AS Amount2, 'Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3, 'PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, 'Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5, 'OTHERADDITIONS' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD, DivisionId, '01.CheckRoll' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);
            dsNormalWork = SQLHelper.FillDataSet("SELECT     '1.Tea Plk Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays)-isnull(sum(PlkHalfNames),0) AS QTY,  SUM(PluckingNamePay)-isnull(sum(PlkHalfNamePay),0) AS Amount,  '2.Plk Half' AS Name,  isnull(sum(PlkHalfNames),0)AS QTY01,  isnull(SUM(PlkHalfNamePay),0) AS Amount01,  '3.Grn.Leaf.Okg' AS Name1, SUM(OverKilos) AS QTY1,  SUM(OverKilosPay) AS Amount1,  '4.TeaSundryNames' AS Name2,  SUM(SundryManDays)+ SUM(HolidaySundryManDays)-isnull(sum(SundryHalfNames),0)  AS QTY2,  SUM(SundryNamePay)-isnull(sum(SundryHalfNamePay),0) AS Amount2,  '5.SundryHalf' AS Name21,  isnull(sum(SundryHalfNames),0)  AS QTY21,  isnull(sum(SundryHalfNamePay),0) AS Amount21,  '6.Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3,  '7.PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, '8.Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5,  '9.OtherAditions' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD, DivisionId, '01.CheckRoll' AS GROUP1 FROM         dbo.EmpMonthlyEarnings WHERE     (Year = '" + strYear + "') AND (Month ='" + intMonth + "') GROUP BY DivisionId, Year, Month ", CommandType.Text);

            DataSet dsNormalWork1 = new DataSet();
            dsNormalWork1 = SQLHelper.FillDataSet("SELECT 'Fact A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE (OTFactor = 1) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsNormalWork2 = new DataSet();
            dsNormalWork2 = SQLHelper.FillDataSet("SELECT 'Field A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE  (OTFactor = 2) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsOverTime = new DataSet();
            dsOverTime = SQLHelper.FillDataSet("SELECT 'Over Time' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode, '01.CheckRoll' AS Group1 FROM         dbo.CHKOvertime WHERE     (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsCashWork = new DataSet();
            //dsCashWork = SQLHelper.FillDataSet("SELECT 'Cash Sundry' AS Name, SUM(CashManDays) AS QTY, SUM(CashSundry) AS Amount, 'Cash Kgs' AS Name1, '0.00' AS QTY1, SUM(CashPlucking) AS Amount1, DivisionId, '02.CashWork' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);
            dsCashWork = SQLHelper.FillDataSet(" SELECT 'Cash Sundry' AS Name, SUM(CashManDays) AS QTY, SUM(CashSundry) AS Amount, 'Cash Plucking' AS Name1, isnull(SUM(CashKilos),0) AS QTY1,  isnull(SUM(CashKiloAmount),0) AS Amount1, 'BlockPlucking' AS Name2, isnull(SUM(BlockKilos),0) AS QTY2, isnull(SUM(BlockKiloAmount),0) AS Amount2, DivisionId,  '02.CashWork' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsBF = new DataSet();
            dsBF = SQLHelper.FillDataSet("SELECT     'B/Forward' AS Name, '0.00' AS QTY, SUM(PreviousMadeUpCoins) AS BF, DivisionId, '03.CF/BF' AS Category FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "')   GROUP BY DivisionId, Year, Month", CommandType.Text);


            DataSet dsDeductSummery = new DataSet();
            dsDeductSummery = SQLHelper.FillDataSet("SELECT right('00'+convert(varchar(10),dbo.CHKDeductionGroup.DeductionGroupCode),2)+'.'+dbo.CHKDeductionGroup.GroupName AS Name, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '06 Deductions Summery' AS Group1, dbo.CHKDeductionGroup.ShortName, right('00'+dbo.CHKDeductionGroup.DeductionGroupCode,2) as GroupCode FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "')  GROUP BY  dbo.CHKDeductionGroup.DeductionGroupCode,dbo.CHKDeductionGroup.GroupName, dbo.CHKDeductionGroup.ShortName, dbo.CHKEmpDeductions.DivisionId order by GroupCode", CommandType.Text);

            DataSet dsPreDebits = new DataSet();
            dsPreDebits = SQLHelper.FillDataSet("SELECT 'Previous Debits' AS Name, '0.00' AS QTY, SUM(PreviousDebits) AS PD, DivisionId, '05.Previous Debits' AS Category FROM dbo.EmpMonthlyDeductions WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsTotDeduc = new DataSet();
            dsTotDeduc = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalDeductions) AS TotDeduc, DivisionId, '07.TotalDeduc' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear ='" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, TotalDeductions", CommandType.Text);

            DataSet dsCF = new DataSet();
            dsCF = SQLHelper.FillDataSet("SELECT 'Carried Forward' AS Name, '0.00' AS QTY, SUM(MadeUpBalance) AS CF, DivisionId, '08.CF/BF' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsPFContributions = new DataSet();
            dsPFContributions = SQLHelper.FillDataSet("SELECT 'Member (10%)' AS Name, '0.00' AS QTY, SUM(EPF10) AS Amount, 'Estate (12%)' AS Name1, '0.00' AS QTY, SUM(EPF12) AS Amount1, 'Total (22%)' AS Name2, '0.00' AS QTY, SUM(EPF10) + SUM(EPF12) AS Amount2, 'ETF (3%)' AS Name3, '0.00' AS QTY, SUM(ETF3) AS Amount3, DivisionId, '10.EPF Contributions Member' AS Group1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsNetPay = new DataSet();
            dsNetPay = SQLHelper.FillDataSet("SELECT '' AS Name, '0.0' AS Qty, SUM(TotalEarnigs) AS Earn, '04.GROSS PAY' AS Group1, DivisionId FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsNetPay2 = new DataSet();
            dsNetPay2 = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalEarnigs) AS Earnigs, '' AS Name2, '0.00' AS QTY2, SUM(TotalDeductions) AS Deductions, '' AS Name3,  '0.00' AS QTY3, SUM(SalaryAmount) AS Balance, '09.Net Pay' AS Group1, DivisionId FROM   dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "')GROUP BY DivisionId", CommandType.Text);

            DataSet dsBalance = new DataSet();
            dsBalance = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(WagePay) AS BAL, DivisionId, '09.Net Pay' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsAdditions = new DataSet();
            dsAdditions = SQLHelper.FillDataSet("SELECT dbo.CHKAddition.AdditionShortName AS ADDITION, '0.00' AS QTY, SUM(dbo.CHKEmpAdditions.Amount) AS AdditionAmt, dbo.CHKEmpAdditions.DivisionID, 'a1.Additions' AS Group1 FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.AdditionYear = '" + strYear + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonth + "') GROUP BY dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.DivisionID", CommandType.Text);

            Int32 colCount = 0;
            if (dsNormalWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNormalWork.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsNormalWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 24)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[27].ToString();
                                drm[4] = dr.ItemArray[28].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }
                }
            }
            if (dsOverTime.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOverTime.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsCashWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCashWork.Tables[0].Rows)
                {
                    colCount = 0;

                    foreach (DataColumn dc in dsCashWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 6)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[9].ToString();
                                drm[4] = dr.ItemArray[10].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }

                }
            }

            if (dsBF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drT in dsBF.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = drT.ItemArray[0].ToString();
                    drm[1] = drT.ItemArray[1].ToString();
                    drm[2] = drT.ItemArray[2].ToString();
                    drm[3] = drT.ItemArray[3].ToString();
                    drm[4] = drT.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsNetPay.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNetPay.Tables[0].Rows)
                {
                    //colCount = 0;

                    //foreach (DataColumn dc in dsNetPay.Tables[0].Columns)
                    //{
                    DataRow drm = dtMain1.NewRow();
                    //    if (colCount <= 0)
                    //    {
                    //if (colCount % 3 == 0)
                    //{
                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[4].ToString();
                    drm[4] = dr.ItemArray[3].ToString();
                    dtMain1.Rows.Add(drm);
                    //}
                    //}
                    //colCount++;
                    //}
                }
            }

            if (dsPreDebits.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPreDebits.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }

            if (dsDeductSummery.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDeductSummery.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }
            DataSet dsDeductions = new DataSet();
            DataSet dsDeductGroups = new DataSet();
            dsDeductGroups = SQLHelper.FillDataSet("SELECT DeductionGroupCode, ShortName FROM dbo.CHKDeductionGroup order by DeductionGroupCode ", CommandType.Text);
            foreach (DataRow dr in dsDeductGroups.Tables[0].Rows)
            {

                //dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '5.Deductions&Payment' AS Group1 FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') GROUP BY dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ", CommandType.Text);
                dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, 'a2.'+right('00'+'" + dr.ItemArray[0].ToString() + "',2)+'" + dr.ItemArray[1].ToString() + "' AS Group1, dbo.CHKDeduction.DeductionGroupCode,'" + dr.ItemArray[0].ToString() + "' as no  FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') and  (dbo.CHKDeduction.DeductionGroupCode = '" + Convert.ToInt32(dr.ItemArray[0].ToString()) + "') GROUP BY dbo.CHKDeduction.DeductionGroupCode,dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ORDER BY no,dbo.CHKDeduction.DeductionGroupCode", CommandType.Text);
                if (dsDeductions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsDeductions.Tables[0].Rows)
                    {
                        DataRow drM = dtMain1.NewRow();

                        drM[0] = dr1.ItemArray[0].ToString();
                        drM[1] = dr1.ItemArray[1].ToString();
                        drM[2] = dr1.ItemArray[2].ToString();
                        drM[3] = dr1.ItemArray[3].ToString();
                        drM[4] = dr1.ItemArray[4].ToString();


                        dtMain1.Rows.Add(drM);
                    }
                }

            }

            if (dsTotDeduc.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTotDeduc.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }

            }
            if (dsCF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCF.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drM);

                }
            }
            if (dsBalance.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsBalance.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }
            }
            if (dsPFContributions.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPFContributions.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsPFContributions.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 9)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[12].ToString();
                                drm[4] = dr.ItemArray[13].ToString();
                                dtMain1.Rows.Add(drm);

                            }
                        }
                        colCount++;
                    }
                }

                if (dsAdditions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsAdditions.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = dr.ItemArray[0].ToString();
                        drm[1] = dr.ItemArray[1].ToString();
                        drm[2] = dr.ItemArray[2].ToString();
                        drm[3] = dr.ItemArray[3].ToString();
                        drm[4] = dr.ItemArray[4].ToString();
                        dtMain1.Rows.Add(drm);
                    }

                }
            }
            return dtMain1;
        }

        public DataTable GetAmalgamationOld11DashBoard(String strYear, Int32 intMonth)
        {
            DataTable dtMain1 = new DataTable();
            dtMain1.Columns.Add(new DataColumn("NAME"));
            dtMain1.Columns.Add(new DataColumn("QTY"));
            dtMain1.Columns.Add(new DataColumn("TOTALAMOUNT"));
            dtMain1.Columns.Add(new DataColumn("DIVISION"));
            dtMain1.Columns.Add(new DataColumn("GROUP"));


            DataSet dsNormalWork = new DataSet();
            dsNormalWork = SQLHelper.FillDataSet("SELECT     'Tea Plucking Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays) AS QTY, SUM(PluckingNamePay) AS Amount, 'Grn.Leaf Over kg' AS Name1, SUM(OverKilos) AS QTY1, SUM(OverKilosPay) AS Amount1, 'Tea Sundry Names' AS Name2, SUM(SundryManDays) + SUM(HolidaySundryManDays) AS QTY2,  SUM(SundryNamePay) AS Amount2, 'Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3, 'PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount)  AS Amount4, 'Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5, 'OTHERADDITIONS' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions)  AS OTHERADD, DivisionId, '01.CheckRoll' AS GROUP1 FROM dbo.CheckRollEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            //DataSet dsNormalWork1 = new DataSet();
            //dsNormalWork1 = SQLHelper.FillDataSet("SELECT 'Fact A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode, '01.CheckRoll' AS Group1 FROM dbo.CheckRollOverTime WHERE     (OTFactor = 1) AND (YEAR(OTDate) = '" + strYear + "') AND (MONTH(OTDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            //DataSet dsNormalWork2 = new DataSet();
            //dsNormalWork2 = SQLHelper.FillDataSet("SELECT 'Field A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE  (OTFactor = 2) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsOverTime = new DataSet();
            dsOverTime = SQLHelper.FillDataSet("SELECT 'Over Time' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode, '01.CheckRoll' AS Group1 FROM dbo.CheckRollOverTime WHERE (YEAR(OTDate) = '" + strYear + "') AND (MONTH(OTDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsCashWork = new DataSet();
            dsCashWork = SQLHelper.FillDataSet("SELECT 'Cash Sundry' AS Name, SUM(CashManDays) AS QTY, SUM(CashSundry) AS Amount, 'Cash Kgs' AS Name1, '0.00' AS QTY1, SUM(CashPlucking) AS Amount1,  DivisionId, '02.CashWork' AS Expr1 FROM dbo.CheckRollEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsBF = new DataSet();
            dsBF = SQLHelper.FillDataSet("SELECT     'B/Forward' AS Name, '0.00' AS QTY, SUM(PreviousMadeUpCoins) AS BF, DivisionId, '03.CF/BF' AS Category FROM dbo.CheckRollEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "')   GROUP BY DivisionId, Year, Month", CommandType.Text);


            DataSet dsDeductSummery = new DataSet();
            dsDeductSummery = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT DeductionGroup AS Name, '0.00' AS QTY, SUM(Amount) AS Amount, DivisionID, '06 Deductions Summery' AS Group1,  DeductionGroup AS ShortName, DeductionGroup AS GroupCode FROM dbo.CheckRollRecoveries WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DeductionGroup, DivisionID ORDER BY Name", CommandType.Text);
            //stop from here 2012-03-09
            DataSet dsPreDebits = new DataSet();
            dsPreDebits = SQLHelper.FillDataSet("SELECT 'Previous Debits' AS Name, '0.00' AS QTY, SUM(PreviousDebits) AS PD, DivisionId, '05.Previous Debits' AS Category FROM dbo.EmpMonthlyDeductions WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsTotDeduc = new DataSet();
            dsTotDeduc = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalDeductions) AS TotDeduc, DivisionId, '07.TotalDeduc' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear ='" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, TotalDeductions", CommandType.Text);

            DataSet dsCF = new DataSet();
            dsCF = SQLHelper.FillDataSet("SELECT 'Carried Forward' AS Name, '0.00' AS QTY, SUM(MadeUpBalance) AS CF, DivisionId, '08.CF/BF' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsPFContributions = new DataSet();
            dsPFContributions = SQLHelper.FillDataSet("SELECT 'Member (10%)' AS Name, '0.00' AS QTY, SUM(EPF10) AS Amount, 'Estate (12%)' AS Name1, '0.00' AS QTY, SUM(EPF12) AS Amount1, 'Total (22%)' AS Name2, '0.00' AS QTY, SUM(EPF10) + SUM(EPF12) AS Amount2, 'ETF (3%)' AS Name3, '0.00' AS QTY, SUM(ETF3) AS Amount3, DivisionId, '10.EPF Contributions Member' AS Group1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsNetPay = new DataSet();
            dsNetPay = SQLHelper.FillDataSet("SELECT '' AS Name, '0.0' AS Qty, SUM(TotalEarnigs) AS Earn, '04.GROSS PAY' AS Group1, DivisionId FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsNetPay2 = new DataSet();
            dsNetPay2 = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalEarnigs) AS Earnigs, '' AS Name2, '0.00' AS QTY2, SUM(TotalDeductions) AS Deductions, '' AS Name3,  '0.00' AS QTY3, SUM(SalaryAmount) AS Balance, '09.Net Pay' AS Group1, DivisionId FROM   dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "')GROUP BY DivisionId", CommandType.Text);

            DataSet dsBalance = new DataSet();
            dsBalance = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(WagePay) AS BAL, DivisionId, '09.Net Pay' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsAdditions = new DataSet();
            dsAdditions = SQLHelper.FillDataSet("SELECT dbo.CHKAddition.AdditionShortName AS ADDITION, '0.00' AS QTY, SUM(dbo.CHKEmpAdditions.Amount) AS AdditionAmt, dbo.CHKEmpAdditions.DivisionID, 'a1.Additions' AS Group1 FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.AdditionYear = '" + strYear + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonth + "') GROUP BY dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.DivisionID", CommandType.Text);

            Int32 colCount = 0;
            if (dsNormalWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNormalWork.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsNormalWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 18)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[21].ToString();
                                drm[4] = dr.ItemArray[22].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }
                }
            }
            if (dsOverTime.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOverTime.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsCashWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCashWork.Tables[0].Rows)
                {
                    colCount = 0;

                    foreach (DataColumn dc in dsCashWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 3)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[6].ToString();
                                drm[4] = dr.ItemArray[7].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }

                }
            }


            if (dsBF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drT in dsBF.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = drT.ItemArray[0].ToString();
                    drm[1] = drT.ItemArray[1].ToString();
                    drm[2] = drT.ItemArray[2].ToString();
                    drm[3] = drT.ItemArray[3].ToString();
                    drm[4] = drT.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsNetPay.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNetPay.Tables[0].Rows)
                {
                    //colCount = 0;

                    //foreach (DataColumn dc in dsNetPay.Tables[0].Columns)
                    //{
                    DataRow drm = dtMain1.NewRow();
                    //    if (colCount <= 0)
                    //    {
                    //if (colCount % 3 == 0)
                    //{
                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[4].ToString();
                    drm[4] = dr.ItemArray[3].ToString();
                    dtMain1.Rows.Add(drm);
                    //}
                    //}
                    //colCount++;
                    //}
                }
            }

            if (dsPreDebits.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPreDebits.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }

            if (dsDeductSummery.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDeductSummery.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }
            DataSet dsDeductions = new DataSet();
            DataSet dsDeductGroups = new DataSet();
            dsDeductGroups = SQLHelper.FillDataSet("SELECT DeductionGroupCode, ShortName FROM dbo.CHKDeductionGroup order by DeductionGroupCode ", CommandType.Text);
            foreach (DataRow dr in dsDeductGroups.Tables[0].Rows)
            {

                //dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '5.Deductions&Payment' AS Group1 FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') GROUP BY dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ", CommandType.Text);
                dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, 'a2.'+right('00'+'" + dr.ItemArray[0].ToString() + "',2)+'" + dr.ItemArray[1].ToString() + "' AS Group1, dbo.CHKDeduction.DeductionGroupCode,'" + dr.ItemArray[0].ToString() + "' as no  FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') and  (dbo.CHKDeduction.DeductionGroupCode = '" + Convert.ToInt32(dr.ItemArray[0].ToString()) + "') GROUP BY dbo.CHKDeduction.DeductionGroupCode,dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ORDER BY no,dbo.CHKDeduction.DeductionGroupCode", CommandType.Text);
                if (dsDeductions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsDeductions.Tables[0].Rows)
                    {
                        DataRow drM = dtMain1.NewRow();

                        drM[0] = dr1.ItemArray[0].ToString();
                        drM[1] = dr1.ItemArray[1].ToString();
                        drM[2] = dr1.ItemArray[2].ToString();
                        drM[3] = dr1.ItemArray[3].ToString();
                        drM[4] = dr1.ItemArray[4].ToString();


                        dtMain1.Rows.Add(drM);
                    }
                }

            }

            if (dsTotDeduc.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTotDeduc.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }

            }
            if (dsCF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCF.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drM);

                }
            }
            if (dsBalance.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsBalance.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }
            }
            if (dsPFContributions.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPFContributions.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsPFContributions.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 9)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[12].ToString();
                                drm[4] = dr.ItemArray[13].ToString();
                                dtMain1.Rows.Add(drm);

                            }
                        }
                        colCount++;
                    }
                }

                if (dsAdditions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsAdditions.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = dr.ItemArray[0].ToString();
                        drm[1] = dr.ItemArray[1].ToString();
                        drm[2] = dr.ItemArray[2].ToString();
                        drm[3] = dr.ItemArray[3].ToString();
                        drm[4] = dr.ItemArray[4].ToString();
                        dtMain1.Rows.Add(drm);
                    }

                }
            }
            return dtMain1;
        }

        public DataTable GetMISAmalgamation(String strYear, Int32 intMonth)
        {

            DataTable dtMain1 = new DataTable();

            dtMain1.Columns.Add(new DataColumn("NAME"));
            dtMain1.Columns.Add(new DataColumn("QTY"));
            dtMain1.Columns.Add(new DataColumn("TOTALAMOUNT"));
            dtMain1.Columns.Add(new DataColumn("DIVISION"));
            dtMain1.Columns.Add(new DataColumn("GROUP"));


            DataSet dsNormalWork = new DataSet();
            //dsNormalWork = SQLHelper.FillDataSet("SELECT 'Tea Plucking Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays) AS QTY, SUM(PluckingNamePay) AS Amount, 'Grn.Leaf Over kg' AS Name1, SUM(OverKilos) AS QTY1, SUM(OverKilosPay) AS Amount1, 'Tea Sundry Names' AS Name2, SUM(SundryManDays) + SUM(HolidaySundryManDays) AS QTY2, SUM(SundryNamePay) AS Amount2, 'Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3, 'PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, 'Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5, 'OTHERADDITIONS' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD, DivisionId, '01.CheckRoll' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);
            dsNormalWork = SQLHelper.FillDataSet("SELECT     '1.Tea Plk Names' AS Name, SUM(PluckingManDays) + SUM(HolidayPLKManDays)-isnull(sum(PlkHalfNames),0) AS QTY,  SUM(PluckingNamePay)-isnull(sum(PlkHalfNamePay),0) AS Amount,  '2.Plk Half' AS Name,  isnull(sum(PlkHalfNames),0)AS QTY01,  isnull(SUM(PlkHalfNamePay),0) AS Amount01,  '3.Grn.Leaf.Okg' AS Name1, SUM(OverKilos) AS QTY1,  SUM(OverKilosPay) AS Amount1,  '4.TeaSundryNames' AS Name2,  SUM(SundryManDays)+ SUM(HolidaySundryManDays)-isnull(sum(SundryHalfNames),0)  AS QTY2,  SUM(SundryNamePay)-isnull(sum(SundryHalfNamePay),0) AS Amount2,  '5.SundryHalf' AS Name21,  isnull(sum(SundryHalfNames),0)  AS QTY21,  isnull(sum(SundryHalfNamePay),0) AS Amount21,  '6.Extra Rate' AS Name3, '0.00' AS QTY3, SUM(ExtraRates) AS Amount3,  '7.PRI Pay' AS Name4, '0.00' AS QTY4, SUM(PRIAmount) AS Amount4, '8.Attn.Incentive' AS Name5, '0.00' AS QTY5, SUM(AttIncentive) AS Amount5,  '9.OtherAditions' AS Name6, '0.0' AS QTY6, SUM(OtherAdditions) AS OTHERADD, DivisionId, '01.CheckRoll' AS GROUP1 FROM         dbo.EmpMonthlyEarnings WHERE     (Year = '" + strYear + "') AND (Month ='" + intMonth + "') GROUP BY DivisionId, Year, Month ", CommandType.Text);

            DataSet dsNormalWork1 = new DataSet();
            dsNormalWork1 = SQLHelper.FillDataSet("SELECT 'Fact A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE (OTFactor = 1) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsNormalWork2 = new DataSet();
            dsNormalWork2 = SQLHelper.FillDataSet("SELECT 'Field A.M.O/T' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode AS DivisionId, '01.CheckRoll' AS Group1 FROM dbo.CHKOvertime WHERE  (OTFactor = 2) AND (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsOverTime = new DataSet();
            dsOverTime = SQLHelper.FillDataSet("SELECT 'Over Time' AS Type, '0.00' AS QTY, SUM(Expenditure) AS Amount, DivisionCode, '01.CheckRoll' AS Group1 FROM         dbo.CHKOvertime WHERE     (YEAR(OtDate) = '" + strYear + "') AND (MONTH(OtDate) = '" + intMonth + "') GROUP BY DivisionCode", CommandType.Text);

            DataSet dsCashWork = new DataSet();
            //dsCashWork = SQLHelper.FillDataSet("SELECT 'Cash Sundry' AS Name, SUM(CashManDays) AS QTY, SUM(CashSundry) AS Amount, 'Cash Kgs' AS Name1, '0.00' AS QTY1, SUM(CashPlucking) AS Amount1, DivisionId, '02.CashWork' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);
            dsCashWork = SQLHelper.FillDataSet(" SELECT 'Cash Sundry' AS Name, SUM(CashManDays) AS QTY, SUM(CashSundry) AS Amount, 'Cash Plucking' AS Name1, isnull(SUM(CashKilos),0) AS QTY1,  isnull(SUM(CashKiloAmount),0) AS Amount1, 'BlockPlucking' AS Name2, isnull(SUM(BlockKilos),0) AS QTY2, isnull(SUM(BlockKiloAmount),0) AS Amount2, DivisionId,  '02.CashWork' AS GROUP1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsBF = new DataSet();
            dsBF = SQLHelper.FillDataSet("SELECT     'B/Forward' AS Name, '0.00' AS QTY, SUM(PreviousMadeUpCoins) AS BF, DivisionId, '03.CF/BF' AS Category FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "')   GROUP BY DivisionId, Year, Month", CommandType.Text);


            DataSet dsDeductSummery = new DataSet();
            dsDeductSummery = SQLHelper.FillDataSet("SELECT right('00'+convert(varchar(10),dbo.CHKDeductionGroup.DeductionGroupCode),2)+'.'+dbo.CHKDeductionGroup.GroupName AS Name, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '06 Deductions Summery' AS Group1, dbo.CHKDeductionGroup.ShortName, right('00'+dbo.CHKDeductionGroup.DeductionGroupCode,2) as GroupCode FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "')  GROUP BY  dbo.CHKDeductionGroup.DeductionGroupCode,dbo.CHKDeductionGroup.GroupName, dbo.CHKDeductionGroup.ShortName, dbo.CHKEmpDeductions.DivisionId order by GroupCode", CommandType.Text);

            DataSet dsPreDebits = new DataSet();
            dsPreDebits = SQLHelper.FillDataSet("SELECT 'Previous Debits' AS Name, '0.00' AS QTY, SUM(PreviousDebits) AS PD, DivisionId, '05.Previous Debits' AS Category FROM dbo.EmpMonthlyDeductions WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId, Year, Month", CommandType.Text);

            DataSet dsTotDeduc = new DataSet();
            dsTotDeduc = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalDeductions) AS TotDeduc, DivisionId, '07.TotalDeduc' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear ='" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, TotalDeductions", CommandType.Text);

            DataSet dsCF = new DataSet();
            dsCF = SQLHelper.FillDataSet("SELECT 'Carried Forward' AS Name, '0.00' AS QTY, SUM(MadeUpBalance) AS CF, DivisionId, '08.CF/BF' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsPFContributions = new DataSet();
            dsPFContributions = SQLHelper.FillDataSet("SELECT 'Member (10%)' AS Name, '0.00' AS QTY, SUM(EPF10) AS Amount, 'Estate (12%)' AS Name1, '0.00' AS QTY, SUM(EPF12) AS Amount1, 'Total (22%)' AS Name2, '0.00' AS QTY, SUM(EPF10) + SUM(EPF12) AS Amount2, 'ETF (3%)' AS Name3, '0.00' AS QTY, SUM(ETF3) AS Amount3, DivisionId, '10.EPF Contributions Member' AS Group1 FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + strYear + "') AND (Month = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsNetPay = new DataSet();
            dsNetPay = SQLHelper.FillDataSet("SELECT '' AS Name, '0.0' AS Qty, SUM(TotalEarnigs) AS Earn, '04.GROSS PAY' AS Group1, DivisionId FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId, WageYear, WageMonth", CommandType.Text);

            DataSet dsNetPay2 = new DataSet();
            dsNetPay2 = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(TotalEarnigs) AS Earnigs, '' AS Name2, '0.00' AS QTY2, SUM(TotalDeductions) AS Deductions, '' AS Name3,  '0.00' AS QTY3, SUM(SalaryAmount) AS Balance, '09.Net Pay' AS Group1, DivisionId FROM   dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "')GROUP BY DivisionId", CommandType.Text);

            DataSet dsBalance = new DataSet();
            dsBalance = SQLHelper.FillDataSet("SELECT '' AS Name, '0.00' AS QTY, SUM(WagePay) AS BAL, DivisionId, '09.Net Pay' AS Group1 FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + strYear + "') AND (WageMonth = '" + intMonth + "') GROUP BY DivisionId", CommandType.Text);

            DataSet dsAdditions = new DataSet();
            dsAdditions = SQLHelper.FillDataSet("SELECT dbo.CHKAddition.AdditionShortName AS ADDITION, '0.00' AS QTY, SUM(dbo.CHKEmpAdditions.Amount) AS AdditionAmt, dbo.CHKEmpAdditions.DivisionID, 'a1.Additions' AS Group1 FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.AdditionYear = '" + strYear + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonth + "') GROUP BY dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.DivisionID", CommandType.Text);

            Int32 colCount = 0;
            if (dsNormalWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNormalWork.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsNormalWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 24)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[27].ToString();
                                drm[4] = dr.ItemArray[28].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }
                }
            }
            if (dsOverTime.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOverTime.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsCashWork.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCashWork.Tables[0].Rows)
                {
                    colCount = 0;

                    foreach (DataColumn dc in dsCashWork.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 6)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[9].ToString();
                                drm[4] = dr.ItemArray[10].ToString();
                                dtMain1.Rows.Add(drm);
                            }
                        }
                        colCount++;
                    }

                }
            }

            if (dsBF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drT in dsBF.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = drT.ItemArray[0].ToString();
                    drm[1] = drT.ItemArray[1].ToString();
                    drm[2] = drT.ItemArray[2].ToString();
                    drm[3] = drT.ItemArray[3].ToString();
                    drm[4] = drT.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drm);

                }
            }
            if (dsNetPay.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNetPay.Tables[0].Rows)
                {
                    //colCount = 0;

                    //foreach (DataColumn dc in dsNetPay.Tables[0].Columns)
                    //{
                    DataRow drm = dtMain1.NewRow();
                    //    if (colCount <= 0)
                    //    {
                    //if (colCount % 3 == 0)
                    //{
                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[4].ToString();
                    drm[4] = dr.ItemArray[3].ToString();
                    dtMain1.Rows.Add(drm);
                    //}
                    //}
                    //colCount++;
                    //}
                }
            }

            if (dsPreDebits.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPreDebits.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }

            if (dsDeductSummery.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDeductSummery.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();


                    dtMain1.Rows.Add(drM);
                }
            }
            DataSet dsDeductions = new DataSet();
            DataSet dsDeductGroups = new DataSet();
            dsDeductGroups = SQLHelper.FillDataSet("SELECT DeductionGroupCode, ShortName FROM dbo.CHKDeductionGroup order by DeductionGroupCode ", CommandType.Text);
            foreach (DataRow dr in dsDeductGroups.Tables[0].Rows)
            {

                //dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, '5.Deductions&Payment' AS Group1 FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') GROUP BY dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ", CommandType.Text);
                dsDeductions = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionName, '0.00' AS QTY, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKEmpDeductions.DivisionId, 'a2.'+right('00'+'" + dr.ItemArray[0].ToString() + "',2)+'" + dr.ItemArray[1].ToString() + "' AS Group1, dbo.CHKDeduction.DeductionGroupCode,'" + dr.ItemArray[0].ToString() + "' as no  FROM dbo.CHKDeduction INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKEmpDeductions.DeductId WHERE (dbo.CHKEmpDeductions.DeductYear = '" + strYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') and  (dbo.CHKDeduction.DeductionGroupCode = '" + Convert.ToInt32(dr.ItemArray[0].ToString()) + "') GROUP BY dbo.CHKDeduction.DeductionGroupCode,dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.DivisionId ORDER BY no,dbo.CHKDeduction.DeductionGroupCode", CommandType.Text);
                if (dsDeductions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsDeductions.Tables[0].Rows)
                    {
                        DataRow drM = dtMain1.NewRow();

                        drM[0] = dr1.ItemArray[0].ToString();
                        drM[1] = dr1.ItemArray[1].ToString();
                        drM[2] = dr1.ItemArray[2].ToString();
                        drM[3] = dr1.ItemArray[3].ToString();
                        drM[4] = dr1.ItemArray[4].ToString();


                        dtMain1.Rows.Add(drM);
                    }
                }

            }

            if (dsTotDeduc.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTotDeduc.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }

            }
            if (dsCF.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCF.Tables[0].Rows)
                {
                    DataRow drM = dtMain1.NewRow();

                    drM[0] = dr.ItemArray[0].ToString();
                    drM[1] = dr.ItemArray[1].ToString();
                    drM[2] = dr.ItemArray[2].ToString();
                    drM[3] = dr.ItemArray[3].ToString();
                    drM[4] = dr.ItemArray[4].ToString();

                    dtMain1.Rows.Add(drM);

                }
            }
            if (dsBalance.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsBalance.Tables[0].Rows)
                {
                    DataRow drm = dtMain1.NewRow();

                    drm[0] = dr.ItemArray[0].ToString();
                    drm[1] = dr.ItemArray[1].ToString();
                    drm[2] = dr.ItemArray[2].ToString();
                    drm[3] = dr.ItemArray[3].ToString();
                    drm[4] = dr.ItemArray[4].ToString();
                    dtMain1.Rows.Add(drm);
                }
            }
            if (dsPFContributions.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPFContributions.Tables[0].Rows)
                {
                    colCount = 0;
                    foreach (DataColumn dc in dsPFContributions.Tables[0].Columns)
                    {
                        DataRow drm = dtMain1.NewRow();
                        if (colCount <= 9)
                        {
                            if (colCount % 3 == 0)
                            {
                                drm[0] = dr.ItemArray[colCount].ToString();
                                drm[1] = dr.ItemArray[colCount + 1].ToString();
                                drm[2] = dr.ItemArray[colCount + 2].ToString();
                                drm[3] = dr.ItemArray[12].ToString();
                                drm[4] = dr.ItemArray[13].ToString();
                                dtMain1.Rows.Add(drm);

                            }
                        }
                        colCount++;
                    }
                }

                if (dsAdditions.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsAdditions.Tables[0].Rows)
                    {
                        DataRow drm = dtMain1.NewRow();

                        drm[0] = dr.ItemArray[0].ToString();
                        drm[1] = dr.ItemArray[1].ToString();
                        drm[2] = dr.ItemArray[2].ToString();
                        drm[3] = dr.ItemArray[3].ToString();
                        drm[4] = dr.ItemArray[4].ToString();
                        dtMain1.Rows.Add(drm);
                    }

                }
            }
            return dtMain1;
        }

        public DataSet GetInactiveEmployeesCoins(String strYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKMadeUpCoins.MadeUpCoins FROM dbo.CHKMadeUpCoins INNER JOIN dbo.EmployeeMaster ON dbo.CHKMadeUpCoins.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.CHKMadeUpCoins.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMadeUpCoins.ProcessYear = '" + strYear + "') AND (dbo.CHKMadeUpCoins.ProcessMonth = '" + intMonth + "') AND (dbo.EmployeeMaster.ActiveEmployee = 0)", CommandType.Text);
            return ds;
        }

        public DataSet GetInactiveEmployeesDebts(String strYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.ChkDebtors.DebtAmount FROM dbo.EmployeeMaster INNER JOIN dbo.ChkDebtors ON dbo.EmployeeMaster.DivisionID = dbo.ChkDebtors.DivisionId AND dbo.EmployeeMaster.EmpNo = dbo.ChkDebtors.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 0) AND (dbo.ChkDebtors.DebtYear = '" + strYear + "') AND (dbo.ChkDebtors.DebtMonth = '" + intMonth + "')", CommandType.Text);
            return ds;
        }


    }
}

