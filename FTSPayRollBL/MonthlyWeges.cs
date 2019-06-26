using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using System.Transactions;


namespace FTSPayRollBL
{
    public class MonthlyWeges
    {
        private DateTime dtWegesFromDate;

        public DateTime DtWegesFromDate
        {
            get { return dtWegesFromDate; }
            set { dtWegesFromDate = value; }
        }
        private DateTime dtWegesToDate;

        public DateTime DtWegesToDate
        {
            get { return dtWegesToDate; }
            set { dtWegesToDate = value; }
        }
        private String strCategory;

        public String StrCategory
        {
            get { return strCategory; }
            set { strCategory = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }

        
        
        public String processMonthlyWeges()
        {
           // using (TransactionScope trnScope = new TransactionScope())
            //{
                String Status = "";
                SqlParameter param = new SqlParameter();
                SqlParameter statusParam = new SqlParameter();
                SqlParameter identityParam = new SqlParameter();
                List<SqlParameter> paramList = new List<SqlParameter>();
                param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
                param.Value = DtWegesFromDate;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
                param.Value = DtWegesToDate;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
                param.Value = StrDivision;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@category", SqlDbType.VarChar, 50);
                param.Value = StrCategory;
                paramList.Add(param);
                param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
                param.Value = "ToBeAdd";
                paramList.Add(param);
                SqlCommand cmd = new SqlCommand();
                cmd = SQLHelper.CreateCommand("spProcessMonthlyWeges", CommandType.StoredProcedure, paramList);
                identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
                statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
                identityParam.Direction = ParameterDirection.ReturnValue;
                statusParam.Direction = ParameterDirection.Output;
                SQLHelper.ExecuteNonQuery(cmd);
                int trnScope = int.Parse(identityParam.Value.ToString());
                Status = statusParam.Value.ToString();
                cmd.Dispose();
                return Status;
               // trnScope.Complete();
            //}

        }


        ////////////////////////////Debtors Gurentee List///////////////////////////////////


        public DataTable GetDebtorsGuaranteeRecoveryList(String DivisionID, String Year, String MonthID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");//0
            dt.Columns.Add("EmployeeName");//1
            dt.Columns.Add("Deduction\nGroupName");//2
            dt.Columns.Add("DeductionName");//3
            dt.Columns.Add("Deduction\nGroupCode");//4
            dt.Columns.Add("DeductionCode");//5
            dt.Columns.Add("Amount");//6
            dt.Columns.Add("Guarantee\nEmpNo");//7
            dt.Columns.Add("Guarantee\nEmpName");//8

            DataRow drRow;
            SqlDataReader Reader;


            Reader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeductionGroup.ShortName AS [Group], dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.DeductionGroupCode, dbo.CHKDeduction.DeductionCode FROM dbo.ChkDebtors INNER JOIN dbo.CHKEmpDeductions ON dbo.ChkDebtors.DebtMonth = dbo.CHKEmpDeductions.DeductMonth AND dbo.ChkDebtors.DebtYear = dbo.CHKEmpDeductions.DeductYear AND dbo.ChkDebtors.EmpNO = dbo.CHKEmpDeductions.EmpNo AND dbo.ChkDebtors.DivisionId = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode AND dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeduction.DeductionGroupCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.EmployeeMaster ON dbo.ChkDebtors.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.ChkDebtors.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.ChkDebtors.DebtYear = '" + Year + "') AND (dbo.ChkDebtors.DebtMonth = '" + MonthID + "') AND (dbo.ChkDebtors.DivisionId = '" + DivisionID + "') AND (dbo.CHKDeductionGroup.ShortName IN ('FA', 'MA', 'FS', 'TCO')) ORDER BY dbo.ChkDebtors.DivisionId, dbo.CHKEmpDeductions.EmpNo", CommandType.Text);
            //Reader = SQLHelper.ExecuteReader("SELECT  dbo.ChkDebtors.EmpNO, dbo.EmployeeMaster.EMPName, dbo.CHKDeductionGroup.GroupName, dbo.ChkDebtors.DebtAmount, 'NA', 'NA' FROM dbo.ChkDebtors INNER JOIN dbo.EstateDivision ON dbo.ChkDebtors.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.ChkDebtors.EmpNO = dbo.EmployeeMaster.EmpNo AND dbo.ChkDebtors.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.CHKDeductionGroup ON dbo.ChkDebtors.DeductionGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE     (dbo.ChkDebtors.DebtYear = '" + Year + "') AND (dbo.ChkDebtors.DebtMonth = '" + MonthID + "') AND (dbo.ChkDebtors.DivisionId = '" + DivisionID + "')", CommandType.Text);

            while (Reader.Read())
            {
                drRow = dt.NewRow();

                if (!Reader.IsDBNull(0))
                {
                    drRow[0] = Reader.GetString(0).Trim();
                }
                if (!Reader.IsDBNull(1))
                {
                    drRow[1] = Reader.GetString(1).Trim();
                }
                if (!Reader.IsDBNull(2))
                {
                    drRow[2] = Reader.GetString(2).Trim();
                }
                if (!Reader.IsDBNull(3))
                {
                    drRow[3] = Reader.GetString(3).Trim();
                }
                if (!Reader.IsDBNull(5))
                {
                    drRow[4] = Reader.GetInt32(5);
                }
                if (!Reader.IsDBNull(6))
                {
                    drRow[5] = Reader.GetInt32(6);
                }
                if (!Reader.IsDBNull(4))
                {
                    drRow[6] = Reader.GetDecimal(4);
                }
                
                
                drRow[7] = "NA";
                drRow[8] = "NA";
                dt.Rows.Add(drRow);
            }

            return dt;
        }

        public Boolean IsGuarantorEarningsAvailable(String DivisionID, String EmpNo, String Year, String Month, Decimal DeductionAmount)
        {
            Boolean blYesNo = false;
            Decimal decAmount = 0;

            SqlDataReader Reader;

            Reader = SQLHelper.ExecuteReader("SELECT ISNULL(WagePay,0) WagePay, EmpNo FROM EmpMonthlyFinalWeges WHERE (DivisionId = '" + DivisionID + "') AND (EmpNo = '" + EmpNo + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);

            while (Reader.Read())
            {
                if (!Reader.IsDBNull(0))
                {
                    decAmount = Reader.GetDecimal(0);

                    if (decAmount >= DeductionAmount)
                    {
                        blYesNo = true;
                    }                   
                }
            }

            return blYesNo;
        }

        public void InsertDebtorsDetails(String DivisionID, String EmpNo, String Year, String Month, Int32 DeductionGroupCode, Int32 DeductionCode, Decimal Amount, String GuarenteeEmpNo, Boolean Confirmation)
        {
            SqlDataReader Reader;
            Int32 intDeductionGroupCode = 0;
            Int32 intDeductionCode = 0;
            Int32 intAdditionCode = 0;

            //FoodStuff
            if (DeductionGroupCode == 8)
            {
                Reader = SQLHelper.ExecuteReader("SELECT AdditionId FROM CHKAddition WHERE (AdditionShortName  = 'GRAFS')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intAdditionCode = Reader.GetInt32(0);
                    }
                }
                Reader.Close();

                Reader = SQLHelper.ExecuteReader("SELECT DeductionCode, DeductionGroupCode FROM CHKDeduction WHERE (ShortName = 'GRFS')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intDeductionCode = Reader.GetInt32(0);
                    }
                    if (!Reader.IsDBNull(1))
                    {
                        intDeductionGroupCode = Reader.GetInt32(1);
                    }
                }
                Reader.Close();

            }

            //FestivalAdvance
            if (DeductionGroupCode == 2)
            {
                Reader = SQLHelper.ExecuteReader("SELECT AdditionId FROM CHKAddition WHERE (AdditionShortName  = 'GRAFA')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intAdditionCode = Reader.GetInt32(0);
                    }
                }
                Reader.Close();

                Reader = SQLHelper.ExecuteReader("SELECT DeductionCode, DeductionGroupCode FROM CHKDeduction WHERE (ShortName = 'GRFA')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intDeductionCode = Reader.GetInt32(0);
                    }
                    if (!Reader.IsDBNull(1))
                    {
                        intDeductionGroupCode = Reader.GetInt32(1);
                    }
                }
                Reader.Close();

            }


            //MonthlyAdvance
            if (DeductionGroupCode == 3)
            {
                Reader = SQLHelper.ExecuteReader("SELECT AdditionId FROM CHKAddition WHERE (AdditionShortName  = 'GRAMA')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intAdditionCode = Reader.GetInt32(0);
                    }
                }
                Reader.Close();

                Reader = SQLHelper.ExecuteReader("SELECT DeductionCode, DeductionGroupCode FROM CHKDeduction WHERE (ShortName = 'GRMA')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intDeductionCode = Reader.GetInt32(0);
                    }
                    if (!Reader.IsDBNull(1))
                    {
                        intDeductionGroupCode = Reader.GetInt32(1);
                    }
                }
                Reader.Close();

            }

            //TCO
            if (DeductionGroupCode == 7)
            {
                Reader = SQLHelper.ExecuteReader("SELECT AdditionId FROM CHKAddition WHERE (AdditionShortName  = 'GRATCO')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intAdditionCode = Reader.GetInt32(0);
                    }
                }
                Reader.Close();

                Reader = SQLHelper.ExecuteReader("SELECT DeductionCode, DeductionGroupCode FROM CHKDeduction WHERE (ShortName = 'GRTCO')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intDeductionCode = Reader.GetInt32(0);
                    }
                    if (!Reader.IsDBNull(1))
                    {
                        intDeductionGroupCode = Reader.GetInt32(1);
                    }
                }
                Reader.Close();

            }

            
            //Insert For CHKEmpAddition
            //if (!IsAdditionAvailable(DivisionID, Year, Month, EmpNo, intAdditionCode))
            //{
                SQLHelper.ExecuteNonQuery("INSERT INTO CHKEmpAdditions (EmpNo, AdditionYear, AdditionMonth, AdditionId, Amount, UserId,DivisionID) VALUES ('" + EmpNo + "','" + Year + "','" + Month + "','" + intAdditionCode + "','" + Amount + "','" + FTSPayRollBL.User.StrUserName + "','" + DivisionID + "')", CommandType.Text);
            //}
            //else
            //{
            //    SQLHelper.ExecuteNonQuery("Update CHKEmpAdditions set Amount=Amount+'" + Amount + "' WHERE (AdditionYear = '" + intYear + "') AND (AdditionMonth = '" + intMonth + "') AND (DivisionID = '" + StrDivID + "') AND (EmpNo = '" + strEmp + "') AND (AdditionId = '" + intAdditionId + "') ", CommandType.Text);
            //}

            
            try
            {
                //Insert For CHKFixedDeductions
                //if(!IsDeductionsAvailable(DivisionID,Year,Month,EmpNo,intDeductionGroupCode,intDeductionCode))
                //{
                    SQLHelper.ExecuteNonQuery("INSERT INTO CHKFixedDeductions( DeductionGroupId, DivisionId, DeductionId, EmpNo, DeductAmount, NoOfMonths, StartMonth,StartYear,UserId,PrincipalAmount,BalanceAmount,RecoveredAmount,RecoveredInstallments,CloseYesNo,Guarantor1Div,Guarantor1,Guarantor2Div,Guarantor2)VALUES ('" + intDeductionGroupCode + "','" + DivisionID + "','" + intDeductionCode + "','" + GuarenteeEmpNo + "','" + Amount + "','" + 1 + "','" + Month + "','" + Year + "','" + FTSPayRollBL.User.StrUserName + "','" + Amount + "','" + Amount + "','" + 0 + "','" + 0 + "','" + 0 + "','" + DivisionID + "','" + GuarenteeEmpNo + "','NA','NA')", CommandType.Text);
                //}
                //else
                //{
                //    SQLHelper.ExecuteNonQuery("update CHKFixedDeductions set DeductAmount=DeductAmount+Amount, BalanceAmount=BalanceAmount+'" + Amount + "', PrincipalAmount=PrincipalAmount+'" + Amount + "'  WHERE     (StartYear = '" + intYear + "') AND (StartMonth = '" + intMonth + "') AND (DivisionId = '" + StrDivID + "') AND (EmpNo = '" + strEmp + "') AND (DeductionGroupId = '"+intDeductionGroupCode+"') AND (DeductionId = '"+intDeductionCode+"')",CommandType.Text);
                //}

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    SQLHelper.ExecuteNonQuery("UPDATE CHKFixedDeductions SET DeductAmount=DeductAmount+'" + Amount + "', BalanceAmount=BalanceAmount+'" + Amount + "', PrincipalAmount=PrincipalAmount+'" + Amount + "' WHERE (DeductionGroupId='" + DeductionGroupCode + "') AND (DivisionId='" + DivisionID + "') AND (DeductionId='" + DeductionGroupCode + "') AND (EmpNo='" + GuarenteeEmpNo + "') AND (StartMonth='" + Month + "') AND (StartYear='" + Year + "')", CommandType.Text);
                }
            }

            //MasterTable
            SQLHelper.ExecuteNonQuery("INSERT INTO EmpGuaranteeRecovery (DivisionID, EmpNo, Year, MonthID, AdditionCode, AdditionAmount, GuarenteeEmpNo, DeductionGroup, DeductionCode, DeductionAmount, Confirmation, UserID, GuarenteeDeductionGroupCode, GuarenteeDeductionCode) VALUES ('" + DivisionID + "','" + EmpNo + "','" + Year + "','" + Month + "','" + intAdditionCode + "','" + Amount + "','" + GuarenteeEmpNo + "','" + DeductionGroupCode + "','" + DeductionCode + "','" + Amount + "','" + Confirmation + "','" + FTSPayRollBL.User.StrUserName + "','" + intDeductionGroupCode + "','" + intDeductionCode + "')", CommandType.Text);
        }

        

        public DataSet GetGuaranteeRecoveryList(String DivisionID, String Year, String Month)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.EmpGuaranteeRecovery.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKAddition.AdditionShortName, dbo.EmpGuaranteeRecovery.AdditionAmount, dbo.EmpGuaranteeRecovery.GuarenteeEmpNo, dbo.CHKDeductionGroup.ShortName AS DeductionGroup, dbo.CHKDeduction.ShortName AS Deduction, dbo.EmpGuaranteeRecovery.DeductionAmount, dbo.EmpGuaranteeRecovery.Confirmation, dbo.EmpGuaranteeRecovery.UserID, dbo.EmpGuaranteeRecovery.CreatedDate, CHKDeductionGroup_1.ShortName AS GuaranteeDeductionGroup, CHKDeduction_1.ShortName AS GuaranteeDeduction FROM dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.EmpGuaranteeRecovery INNER JOIN dbo.CHKAddition ON dbo.EmpGuaranteeRecovery.AdditionCode = dbo.CHKAddition.AdditionId ON  dbo.CHKDeduction.DeductionCode = dbo.EmpGuaranteeRecovery.DeductionCode AND  dbo.CHKDeduction.DeductionGroupCode = dbo.EmpGuaranteeRecovery.DeductionGroup INNER JOIN dbo.EmployeeMaster ON dbo.EmpGuaranteeRecovery.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.EmpGuaranteeRecovery.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.CHKDeduction AS CHKDeduction_1 ON dbo.EmpGuaranteeRecovery.GuarenteeDeductionGroupCode = CHKDeduction_1.DeductionGroupCode AND dbo.EmpGuaranteeRecovery.GuarenteeDeductionCode = CHKDeduction_1.DeductionCode INNER JOIN dbo.CHKDeductionGroup AS CHKDeductionGroup_1 ON CHKDeduction_1.DeductionGroupCode = CHKDeductionGroup_1.DeductionGroupCode WHERE (dbo.EmpGuaranteeRecovery.DivisionID = '"+DivisionID+"') AND (dbo.EmpGuaranteeRecovery.Year = '"+Year+"') AND (dbo.EmpGuaranteeRecovery.MonthID = '"+Month+"')", CommandType.Text);
            return ds;
        }

        public DataSet ListGuaranteeRecoveryData(String DivisionID, String Year, String Month)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT * FROM EmpGuaranteeRecovery WHERE (DivisionID='" + DivisionID + "') AND (Year='" + Year + "') AND (MonthID='" + Month + "')", CommandType.Text);
            return ds;
        }

        public void ResetGuaranteeRecoveryData(String DivisionID, String Year, String Month,String DebtEmpNo, Int32 AdditionID, String GuaranteeEmpNo, Int32 DeductionGroupID, Int32 DedcutionID)
        {
            //DELETE data form CHKEmpAddition
            SQLHelper.ExecuteNonQuery("DELETE CHKEmpAdditions WHERE (EmpNo='"+DebtEmpNo+"') AND (AdditionYear='"+Year+"') AND (AdditionMonth='"+Month+"') AND (AdditionId='"+AdditionID+"') AND (DivisionID='"+DivisionID+"')", CommandType.Text);

            //DELETE data CHKFixedDeductions
            SQLHelper.ExecuteNonQuery("DELETE CHKFixedDeductions WHERE (DeductionGroupId='"+DeductionGroupID+"') AND (DivisionId='"+DivisionID+"') AND (DeductionId='"+DedcutionID+"') AND (EmpNo='"+GuaranteeEmpNo+"') AND (StartMonth='"+Month+"') AND (StartYear='"+Year+"')", CommandType.Text);

            //DELETE data MasterTable
            SQLHelper.ExecuteNonQuery("DELETE EmpGuaranteeRecovery WHERE (DivisionID='" + DivisionID + "') AND (EmpNo='" + DebtEmpNo + "') AND (Year='" + Year + "') AND (MonthID='" + Month + "')", CommandType.Text);        
        }

        public Boolean IsDebtorInDebtList(String DivisionID, String EmpNo, Int32 intYear, Int32 intMonthID)
        {
            Boolean blYesNo = false;

            SqlDataReader Reader;

            Reader = SQLHelper.ExecuteReader("SELECT * FROM ChkDebtors WHERE (DivisionId = '" + DivisionID + "') AND (EmpNO = '" + EmpNo + "') AND (DebtYear = '" + intYear + "') AND (DebtMonth = '" + intMonthID + "')", CommandType.Text);

            while (Reader.Read())
            {
                if (!Reader.IsDBNull(0))
                    blYesNo = true;
            }

            return blYesNo;
        }

        public Boolean IsAdditionAvailable(String StrDivID, Int32 intYear, Int32 intMonth, String strEmp, Int32 intAdditionId)
        {
            DataSet dsAdditions;
            Boolean boolAvail = false;
            dsAdditions = SQLHelper.FillDataSet("SELECT * FROM dbo.CHKEmpAdditions WHERE (AdditionYear = '" + intYear + "') AND (AdditionMonth = '" + intMonth + "') AND (DivisionID = '" + StrDivID + "') AND (EmpNo = '" + strEmp + "') AND (AdditionId = '" + intAdditionId + "')", CommandType.Text);
            if (dsAdditions.Tables[0].Rows.Count > 0)
            {
                boolAvail = true;
            }
            return boolAvail;
            
        }

        public Boolean IsDeductionsAvailable(String StrDivID, Int32 intYear, Int32 intMonth, String strEmp, Int32 intDeductGroup,Int32 intDeduct)
        {
            DataSet dsDeductions;
            Boolean boolAvail = false;
            dsDeductions = SQLHelper.FillDataSet("SELECT StartYear, StartMonth, DivisionId, EmpNo, DeductionGroupId, DeductionId FROM dbo.CHKFixedDeductions WHERE (StartYear = '"+intYear+"') AND (StartMonth = '"+intMonth+"') AND (DivisionId = '"+StrDivID+"') AND (EmpNo = '"+strEmp+"') AND (DeductionGroupId = '"+intDeductGroup+"') AND (DeductionId = '"+intDeduct+"')", CommandType.Text);
            if (dsDeductions.Tables[0].Rows.Count > 0)
            {
                boolAvail = true;
            }
            return boolAvail;
        }

        //---------------
        public void InsertDebtorsDeductionDetails(String DivisionID, Int32 Year, Int32 Month, String DeductionGroupCode, Decimal Amount, String GuarenteeEmpNo)
        {
            SqlDataReader Reader;
            Int32 intDeductionGroupCode = 0;
            Int32 intDeductionCode = 0;
            Int32 intAdditionCode = 0;

            //FoodStuff
            if (DeductionGroupCode == "FS")
            {
               

                Reader = SQLHelper.ExecuteReader("SELECT DeductionCode, DeductionGroupCode FROM CHKDeduction WHERE (ShortName = 'GRFS')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intDeductionCode = Reader.GetInt32(0);
                    }
                    if (!Reader.IsDBNull(1))
                    {
                        intDeductionGroupCode = Reader.GetInt32(1);
                    }
                }
                Reader.Close();

            }

            //FestivalAdvance
            if (DeductionGroupCode == "FA")
            {
                

                Reader = SQLHelper.ExecuteReader("SELECT DeductionCode, DeductionGroupCode FROM CHKDeduction WHERE (ShortName = 'GRFA')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intDeductionCode = Reader.GetInt32(0);
                    }
                    if (!Reader.IsDBNull(1))
                    {
                        intDeductionGroupCode = Reader.GetInt32(1);
                    }
                }
                Reader.Close();

            }


            //MonthlyAdvance
            if (DeductionGroupCode == "MA")
            {
                

                Reader = SQLHelper.ExecuteReader("SELECT DeductionCode, DeductionGroupCode FROM CHKDeduction WHERE (ShortName = 'GRMA')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intDeductionCode = Reader.GetInt32(0);
                    }
                    if (!Reader.IsDBNull(1))
                    {
                        intDeductionGroupCode = Reader.GetInt32(1);
                    }
                }
                Reader.Close();

            }

            //TCO
            if (DeductionGroupCode == "TCO")
            {
                

                Reader = SQLHelper.ExecuteReader("SELECT DeductionCode, DeductionGroupCode FROM CHKDeduction WHERE (ShortName = 'GRTCO')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intDeductionCode = Reader.GetInt32(0);
                    }
                    if (!Reader.IsDBNull(1))
                    {
                        intDeductionGroupCode = Reader.GetInt32(1);
                    }
                }
                Reader.Close();

            }


           

            try
            {
                //Insert For CHKFixedDeductions
                //if(!IsDeductionsAvailable(DivisionID,Year,Month,EmpNo,intDeductionGroupCode,intDeductionCode))
                //{
                SQLHelper.ExecuteNonQuery("INSERT INTO CHKFixedDeductions( DeductionGroupId, DivisionId, DeductionId, EmpNo, DeductAmount, NoOfMonths, StartMonth,StartYear,UserId,PrincipalAmount,BalanceAmount,RecoveredAmount,RecoveredInstallments,CloseYesNo,Guarantor1Div,Guarantor1,Guarantor2Div,Guarantor2)VALUES ('" + intDeductionGroupCode + "','" + DivisionID + "','" + intDeductionCode + "','" + GuarenteeEmpNo + "','" + Amount + "','" + 1 + "','" + Month + "','" + Year + "','" + FTSPayRollBL.User.StrUserName + "','" + Amount + "','" + Amount + "','" + 0 + "','" + 0 + "','" + 0 + "','" + DivisionID + "','" + GuarenteeEmpNo + "','NA','NA')", CommandType.Text);
                //}
                //else
                //{
                //    SQLHelper.ExecuteNonQuery("update CHKFixedDeductions set DeductAmount=DeductAmount+Amount, BalanceAmount=BalanceAmount+'" + Amount + "', PrincipalAmount=PrincipalAmount+'" + Amount + "'  WHERE     (StartYear = '" + intYear + "') AND (StartMonth = '" + intMonth + "') AND (DivisionId = '" + StrDivID + "') AND (EmpNo = '" + strEmp + "') AND (DeductionGroupId = '"+intDeductionGroupCode+"') AND (DeductionId = '"+intDeductionCode+"')",CommandType.Text);
                //}

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    //SQLHelper.ExecuteNonQuery("UPDATE CHKFixedDeductions SET DeductAmount=DeductAmount+'" + Amount + "', BalanceAmount=BalanceAmount+'" + Amount + "', PrincipalAmount=PrincipalAmount+'" + Amount + "' WHERE (DeductionGroupId='" + DeductionGroupCode + "') AND (DivisionId='" + DivisionID + "') AND (DeductionId='" + intDeductionCode + "') AND (EmpNo='" + GuarenteeEmpNo + "') AND (StartMonth='" + Month + "') AND (StartYear='" + Year + "')", CommandType.Text);
                }
            }

           
        }

        public void InsertDebtorsAdditionDetails(String DivisionID, String EmpNo, Int32 Year, Int32 Month, String DeductionGroupCode, Decimal Amount)
        {
            SqlDataReader Reader;
            Int32 intDeductionGroupCode = 0;
            Int32 intDeductionCode = 0;
            Int32 intAdditionCode = 0;

            //FoodStuff
            if (DeductionGroupCode == "FS")
            {
                Reader = SQLHelper.ExecuteReader("SELECT AdditionId FROM CHKAddition WHERE (AdditionShortName  = 'GRAFS')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intAdditionCode = Reader.GetInt32(0);
                    }
                }
                Reader.Close();

                
            }

            //FestivalAdvance
            if (DeductionGroupCode == "FA")
            {
                Reader = SQLHelper.ExecuteReader("SELECT AdditionId FROM CHKAddition WHERE (AdditionShortName  = 'GRAFA')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intAdditionCode = Reader.GetInt32(0);
                    }
                }
                Reader.Close();

                

            }


            //MonthlyAdvance
            if (DeductionGroupCode == "MA")
            {
                Reader = SQLHelper.ExecuteReader("SELECT AdditionId FROM CHKAddition WHERE (AdditionShortName  = 'GRAMA')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intAdditionCode = Reader.GetInt32(0);
                    }
                }
                Reader.Close();

                

            }

            //TCO
            if (DeductionGroupCode == "TCO")
            {
                Reader = SQLHelper.ExecuteReader("SELECT AdditionId FROM CHKAddition WHERE (AdditionShortName  = 'GRATCO')", CommandType.Text);

                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0))
                    {
                        intAdditionCode = Reader.GetInt32(0);
                    }
                }
                Reader.Close();

                

            }


            //Insert For CHKEmpAddition
            //if (!IsAdditionAvailable(DivisionID, Year, Month, EmpNo, intAdditionCode))
            //{
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKEmpAdditions (EmpNo, AdditionYear, AdditionMonth, AdditionId, Amount, UserId,DivisionID) VALUES ('" + EmpNo + "','" + Year + "','" + Month + "','" + intAdditionCode + "','" + Amount + "','" + FTSPayRollBL.User.StrUserName + "','" + DivisionID + "')", CommandType.Text);
            //}
            //else
            //{
            //    SQLHelper.ExecuteNonQuery("Update CHKEmpAdditions set Amount=Amount+'" + Amount + "' WHERE (AdditionYear = '" + intYear + "') AND (AdditionMonth = '" + intMonth + "') AND (DivisionID = '" + StrDivID + "') AND (EmpNo = '" + strEmp + "') AND (AdditionId = '" + intAdditionId + "') ", CommandType.Text);
            //}


        }



    }

}
