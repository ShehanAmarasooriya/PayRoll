using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace FTSPayRollBL
{
    public class CheckRollTransfer
    {
        private String strEstateID;

        public String StrEstateID
        {
            get { return strEstateID; }
            set { strEstateID = value; }
        }
        private DateTime datDateEntered;

        public DateTime DatDateEntered
        {
            get { return datDateEntered; }
            set { datDateEntered = value; }
        }
        private String strDivisionID;

        public String StrDivisionID
        {
            get { return strDivisionID; }
            set { strDivisionID = value; }
        }
        private String strEPFNo;

        public String StrEPFNo
        {
            get { return strEPFNo; }
            set { strEPFNo = value; }
        }
        private String strWorkCodeID;

        public String StrWorkCodeID
        {
            get { return strWorkCodeID; }
            set { strWorkCodeID = value; }
        }
        private String strFieldID;

        public String StrFieldID
        {
            get { return strFieldID; }
            set { strFieldID = value; }
        }
        private Decimal decWorkQty;

        public Decimal DecWorkQty
        {
            get { return decWorkQty; }
            set { decWorkQty = value; }
        }
        private String strUserID;

        public String StrUserID
        {
            get { return strUserID; }
            set { strUserID = value; }
        }

        private String strEMPName;

        public String StrEMPName
        {
            get { return strEMPName; }
            set { strEMPName = value; }
        }
        private String strGender;

        public String StrGender
        {
            get { return strGender; }
            set { strGender = value; }
        }
        private DateTime datDateJoined;

        public DateTime DatDateJoined
        {
            get { return datDateJoined; }
            set { datDateJoined = value; }
        }
        private Boolean blnActiveEmployee;

        public Boolean BlnActiveEmployee
        {
            get { return blnActiveEmployee; }
            set { blnActiveEmployee = value; }
        }

        private Decimal decManDays;

        public Decimal DecManDays
        {
            get { return decManDays; }
            set { decManDays = value; }
        }

        private DateTime datToDate;

        public DateTime DatToDate
        {
            get { return datToDate; }
            set { datToDate = value; }
        }

        private Decimal decOverKilos;

        public Decimal DecOverKilos
        {
            get { return decOverKilos; }
            set { decOverKilos = value; }
        }

        private Decimal decScrap;

        public Decimal DecScrap
        {
            get { return decScrap; }
            set { decScrap = value; }
        }

        private String strCropType;

        public String StrCropType
        {
            get { return strCropType; }
            set { strCropType = value; }
        }

        public void OpenCheckRollDays(DateTime MyDate)
        {
            SQLHelper.ExecuteNonQuery("update DailyGroundTransactions set DayClosed = 0 where DateEntered = '" + MyDate + "'", CommandType.Text);
        }
        public void CloseCheckRollDays(DateTime MyDate)
        {
            SQLHelper.ExecuteNonQuery("update DailyGroundTransactions set DayClosed = 1 where DateEntered = '" + MyDate + "'", CommandType.Text);
        }
        public void OpenBoughtLeafDays(DateTime MyDate)
        {
            SQLHelper.ExecuteNonQuery("update DailyBoughtLeaf set DayClosed = 0 where DateEntered = '" + MyDate + "'", CommandType.Text);
        }
        public void CloseBoughtLeafDays(DateTime MyDate)
        {
            SQLHelper.ExecuteNonQuery("update DailyBoughtLeaf set DayClosed = 1 where DateEntered = '" + MyDate + "'", CommandType.Text);
        }
        public void DeleteData(DateTime myDate)
        {
            SQLHelper.ExecuteNonQuery("delete from DailyGroundTransactions where DateEntered ='" + myDate + "'", CommandType.Text);
        }
        public void DeleteDataForCashWork(Int32 Year, Int32 Month)
        {
            SQLHelper.ExecuteNonQuery("delete from CashWorkDetails where month(DateEntered) ='" + Month + "' and year(DateEntered) ='" + Year + "'", CommandType.Text);
        }
        public void DeleteDataInter(Int32 WeekNo)
        {
            SQLHelper.ExecuteNonQuery("delete from DailyGroundTransactions where ({ fn WEEK(DateEntered) } = '" + WeekNo + "')", CommandType.Text);
        }
        public void DeleteBoughtLeafData(Int32 myMonth,Int32 myYear, String EstateID)
        {
            SQLHelper.ExecuteNonQuery("delete from DailyBoughtLeaf where month(DateEntered) ='" + myMonth + "' and year(DateEntered)='" + myYear + "'", CommandType.Text);
        }
        public void DeleteBoughtLeafPaymentData(Int32 myMonth, Int32 myYear, String EstateID)
        {
            SQLHelper.ExecuteNonQuery("delete from BoughtLeafPayments where Month ='" + myMonth + "' and Year='" + myYear + "'", CommandType.Text);
        }
        //public void DeleteDataHODB(Int32 WeekNO)
        //{
        //    SQLHelperInsert.ExecuteNonQuery("DELETE FROM DailyGroundTransactions WHERE  ({ fn WEEK(DateEntered) } = '"+WeekNO+"')", CommandType.Text);
        //}

        //public void DeleteDataHODB(DateTime myDate, String EstateID)
        //{
        //    SQLHelperInsert.ExecuteNonQuery("delete from DailyGroundTransactions where DateEntered ='" + myDate + "' and EstateID='" + EstateID + "'", CommandType.Text);
        //}

        //public void DeleteData(DateTime myDate, String EstateID)
        //{
        //    SQLHelper.ExecuteNonQuery("delete from DailyGroundTransactions where DateEntered ='" + myDate + "' and EstateID='" + EstateID + "'", CommandType.Text);
        //}
        //public void DeleteDataRubber(DateTime myDate, String EstateID)
        //{
        //    SQLHelper.ExecuteNonQuery("delete from DailyGroundTransactionsRubber where DateEntered ='" + myDate + "' and EstateID='" + EstateID + "'", CommandType.Text);
        //}
        //public void DeleteEmployeeData()
        //{
        //    SQLHelper.ExecuteNonQuery("delete from EmployeeMaster", CommandType.Text);
        //}

        //public void DeleteSupplierData()
        //{
        //    SQLHelper.ExecuteNonQuery("delete from Supplier", CommandType.Text);
        //}

        //public void InsertData(CheckRollTransfer myTransfer)
        //{
        //    SqlParameter param = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();

        //    param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrEstateID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DateEntered", SqlDbType.DateTime);
        //    param.Value = myTransfer.DatDateEntered;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrDivisionID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EPFNo", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrEPFNo;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@WorkCodeID", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrWorkCodeID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@FieldID", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrFieldID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@WorkQty", SqlDbType.Decimal);
        //    param.Value = myTransfer.DecWorkQty;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrUserID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@ManDays", SqlDbType.Decimal);
        //    param.Value = myTransfer.DecManDays;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@OverKilos", SqlDbType.Decimal);
        //    param.Value = myTransfer.DecOverKilos;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@Scrap", SqlDbType.Decimal);
        //    param.Value = myTransfer.DecScrap;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@CropType", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrCropType;
        //    paramList.Add(param);

        //    SQLHelper.ExecuteNonQuery("sp_InsertCheckRollData", CommandType.StoredProcedure, paramList);
        //}

        //public void InsertBoughtLeafData(String EstateID, DateTime Date, Int32 SupplierCode, Decimal GreenLeaf)
        //{
        //    SqlParameter param = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();

        //    param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
        //    param.Value = EstateID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DateEntered", SqlDbType.DateTime);
        //    param.Value = Date;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@SupplierCode", SqlDbType.Int);
        //    param.Value = SupplierCode;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@GreenLeaf", SqlDbType.Decimal);
        //    param.Value = GreenLeaf;
        //    paramList.Add(param);

        //    //SQLHelper.ExecuteNonQuery("delete from DailyBoughtLeaf where DateEntered = '" + Date + "' and SupplierCode = '" + SupplierCode + "'", CommandType.Text);
        //    SQLHelper.ExecuteNonQuery("sp_InsertBoughtLeafData", CommandType.StoredProcedure, paramList);
        //}

        //public void InsertBoughtLeafPaymentData(Int32 Year, Int32 Month, String EstateID, Int32 SupplierCode, Decimal TotalKilos, Decimal TotalAmount, Decimal AdvanceTaken, Decimal TotalDeduction, Decimal Balance, Decimal NextMonthDeduction)
        //{
        //    SQLHelper.ExecuteNonQuery("insert into BoughtLeafPayments (Year,Month,EstateID,SupplierCode,TotalKilos,TotalAmount,AdvanceTaken,TotalDeduction,Balance,NextMonthDeduction) values ('" + Year + "','" + Month + "','" + EstateID + "','" + SupplierCode + "','" + TotalKilos + "','" + TotalAmount + "','" + AdvanceTaken + "','" + TotalDeduction + "','" + Balance + "','" + NextMonthDeduction + "')", CommandType.Text);
        //}

        //public void InsertCashWorkData(String DateEntered, String DivisionCode, String Field, String EmpNo, String AdvanceCode, Decimal Amount, String WorkCode, String LentDivisionCode, String LentWorkCode, String AdditionType, String EstateID, Decimal Kilos, Decimal ManDays)
        //{
        //    SQLHelper.ExecuteNonQuery("insert into CashWorkDetails (DateEntered,DivisionCode,Field,EmpNo,AdvanceCode,Amount,WorkCode,LentDivisionCode,LentWorkCode,AdditionType,EstateID, Kilos, ManDays) values ('" + DateEntered + "','" + DivisionCode + "','" + Field + "','" + EmpNo + "','" + AdvanceCode + "','" + Amount + "','" + WorkCode + "','" + LentDivisionCode + "','" + LentWorkCode + "','" + AdditionType + "','" + EstateID + "','" + Kilos + "','" + ManDays + "')", CommandType.Text);
        //}

        //public void InsertHOData(String EstateName, String DivisionName, String WorkCodeName, Decimal Mandays, String EmpName, String Gender, Decimal OverKilos)
        //{
        //    SqlParameter param = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();

        //    param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
        //    param.Value = this.StrEstateID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DateEntered", SqlDbType.DateTime);
        //    param.Value = this.DatDateEntered;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar);
        //    param.Value = this.StrDivisionID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EPFNo", SqlDbType.VarChar);
        //    param.Value = this.StrEPFNo;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@WorkCodeID", SqlDbType.VarChar);
        //    param.Value = this.StrWorkCodeID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@FieldID", SqlDbType.VarChar);
        //    param.Value = this.StrFieldID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@WorkQty", SqlDbType.Decimal);
        //    param.Value = this.DecWorkQty;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
        //    param.Value = this.StrUserID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EstateName", SqlDbType.VarChar);
        //    param.Value = EstateName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DivisionName", SqlDbType.VarChar);
        //    param.Value = DivisionName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@WorkCodeName", SqlDbType.VarChar);
        //    param.Value = WorkCodeName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@Mandays", SqlDbType.Decimal);
        //    param.Value = Mandays;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EmpName", SqlDbType.VarChar);
        //    param.Value = EmpName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@Gender", SqlDbType.VarChar);
        //    param.Value = Gender;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@OverKilos", SqlDbType.Decimal);
        //    param.Value = OverKilos;
        //    paramList.Add(param);

        //    SQLHelper.ExecuteNonQuery("sp_InsertCheckRollHOData", CommandType.StoredProcedure, paramList); 
        //}
        //public void InsertHODataAnhettigama(String EstateName, String DivisionName, String WorkCodeName, Decimal Mandays, String EmpName, String Gender, Decimal OverKilos, String CropType, Decimal Scrap)
        //{
        //    SqlParameter param = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();

        //    param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
        //    param.Value = this.StrEstateID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DateEntered", SqlDbType.DateTime);
        //    param.Value = this.DatDateEntered;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar);
        //    param.Value = this.StrDivisionID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EPFNo", SqlDbType.VarChar);
        //    param.Value = this.StrEPFNo;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@WorkCodeID", SqlDbType.VarChar);
        //    param.Value = this.StrWorkCodeID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@FieldID", SqlDbType.VarChar);
        //    param.Value = this.StrFieldID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@WorkQty", SqlDbType.Decimal);
        //    param.Value = this.DecWorkQty;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
        //    param.Value = this.StrUserID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EstateName", SqlDbType.VarChar);
        //    param.Value = EstateName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DivisionName", SqlDbType.VarChar);
        //    param.Value = DivisionName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@WorkCodeName", SqlDbType.VarChar);
        //    param.Value = WorkCodeName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@Mandays", SqlDbType.Decimal);
        //    param.Value = Mandays;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EmpName", SqlDbType.VarChar);
        //    param.Value = EmpName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@Gender", SqlDbType.VarChar);
        //    param.Value = Gender;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@OverKilos", SqlDbType.Decimal);
        //    param.Value = OverKilos;
        //    paramList.Add(param);

            

        //    if (CropType == "T")
        //    {
        //        SQLHelper.ExecuteNonQuery("sp_InsertCheckRollHOData", CommandType.StoredProcedure, paramList);
        //    }
        //    else
        //    {

        //        param = SQLHelper.CreateParameter("@Scrap", SqlDbType.Decimal);
        //        param.Value = Scrap;
        //        paramList.Add(param);


        //        SQLHelper.ExecuteNonQuery("sp_InsertCheckRollHODataRubber", CommandType.StoredProcedure, paramList);
        //    }
        //}

        //public void InsertCheckrollHo(String EstateName, String DivisionName, String WorkCodeName, Decimal Mandays, String EmpName, String Gender, Decimal OverKilos)
        //{
        //    //SQLHelper.ExecuteNonQuery("insert into DailyGroundTransactions (EstateID,DateEntered,DivisionID,EPFNo,WorkCodeID,FieldID,WorkQty,UserID,EstateName,DivisionName,WorkCodeName,Mandays) values ('" + StrEstateID + "','" + DatDateEntered + "','" + StrDivisionID + "','" + StrEPFNo + "','" + StrWorkCodeID + "','" + StrFieldID + "','" + DecWorkQty + "','" + StrUserID + "','" + EstateName + "','" + DivisionName + "','" + WorkCodeName + "','" + Mandays + "')", CommandType.Text);

        //    SqlParameter param = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();

        //    param = SQLHelperInsert.CreateParameter("@EstateID", SqlDbType.VarChar);
        //    param.Value = this.StrEstateID;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@DateEntered", SqlDbType.DateTime);
        //    param.Value = this.DatDateEntered;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@DivisionID", SqlDbType.VarChar);
        //    param.Value = this.StrDivisionID;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@EPFNo", SqlDbType.VarChar);
        //    param.Value = this.StrEPFNo;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@WorkCodeID", SqlDbType.VarChar);
        //    param.Value = this.StrWorkCodeID;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@FieldID", SqlDbType.VarChar);
        //    param.Value = this.StrFieldID;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@WorkQty", SqlDbType.Decimal);
        //    param.Value = this.DecWorkQty;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@UserID", SqlDbType.VarChar);
        //    param.Value = this.StrUserID;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@EstateName", SqlDbType.VarChar);
        //    param.Value = EstateName;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@DivisionName", SqlDbType.VarChar);
        //    param.Value = EstateName;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@WorkCodeName", SqlDbType.VarChar);
        //    param.Value = WorkCodeName;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@Mandays", SqlDbType.Decimal);
        //    param.Value = Mandays;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@EmpName", SqlDbType.VarChar);
        //    param.Value = EmpName;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@Gender", SqlDbType.VarChar);
        //    param.Value = Gender;
        //    paramList.Add(param);

        //    param = SQLHelperInsert.CreateParameter("@OverKilos", SqlDbType.Decimal);
        //    param.Value = OverKilos;
        //    paramList.Add(param);

        //    SQLHelperInsert.ExecuteNonQuery("sp_InsertCheckRollHOData", CommandType.StoredProcedure, paramList); 
        //}

        //public void UpdateUploadFlag(Int32 intAutoKey)
        //{
        //    SQLHelper.ExecuteNonQuery("update DailyGroundTransactions set Upload=1 where AutoKey='" + intAutoKey + "'", CommandType.Text);
        //}

        //public void UpdateTeaBookUploadFlag(Int32 intAutoKey)
        //{
        //    SQLHelper.ExecuteNonQuery("update DailyOfficeTransactions set Upload=1 where AutoKey='" + intAutoKey + "'", CommandType.Text);
        //}

        //public void UpdateFieldDiaryFlag(Int32 intAutoKey)
        //{
        //    SQLHelper.ExecuteNonQuery("update DailyPluckingRounds set Upload=1 where AutoKey='" + intAutoKey + "'", CommandType.Text);
        //}

        //public void UpdateSiftedBookFlag(String strEstateID, DateTime datEnteredDate, String strGradeCode)
        //{
        //    SQLHelper.ExecuteNonQuery("update DailySiftedTea set Upload=1 where EstateID='" + strEstateID + "' and EnteredDate = '" + datEnteredDate + "' and GradeCode = '" + strGradeCode + "'", CommandType.Text);
        //}

        //public void UpdateStoresBookFlag(Int32 intAutoKey)
        //{
        //    SQLHelper.ExecuteNonQuery("update DailyStoresTransactions set Upload=1 where AutoKey='" + intAutoKey + "'", CommandType.Text);
        //}

        //public void UpdateEstateInvoiceFlag(String EstateInvoiceNo, Int32 YearCode, String EstateID)
        //{
        //    SQLHelper.ExecuteNonQuery("update EstateInvoice set Upload=1 where EstateInvoiceNo='" + EstateInvoiceNo + "' and YearCode='" + YearCode + "' and EstateID='" + EstateID + "'", CommandType.Text);
        //}

        //public void UpdateGLFlag(Int32 intAutoKey)
        //{
        //    SQLHelper.ExecuteNonQuery("update GLTransaction set Upload=1 where AutoKey='" + intAutoKey + "'", CommandType.Text);
        //}

        //public void InsertEmployeeData(CheckRollTransfer myTransfer)
        //{
        //    SqlParameter param = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();

        //    param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrEstateID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrDivisionID;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EPFNo", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrEPFNo;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@EMPName", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrEMPName;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@Gender", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrGender;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@DateJoined", SqlDbType.DateTime);
        //    param.Value = myTransfer.DatDateJoined;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@ActiveEmployee", SqlDbType.Bit);
        //    param.Value = myTransfer.BlnActiveEmployee;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
        //    param.Value = myTransfer.StrUserID;
        //    paramList.Add(param);

        //    //SQLHelper.ExecuteNonQuery("insert into EmployeeMaster (EstateID,DivisionID,EPFNo,EMPName,Gender,DateJoined,ActiveEmployee,UserID) values ('" + myTransfer.StrEstateID + "','" + myTransfer.StrDivisionID + "','" + myTransfer.StrEPFNo + "','" + myTransfer.StrEMPName + "','" + myTransfer.StrGender + "','" + myTransfer.DatDateJoined + "','" + myTransfer.BlnActiveEmployee + "','" + myTransfer.StrUserID + "')", CommandType.StoredProcedure, paramList);
        //    SQLHelper.ExecuteNonQuery("sp_InsertEmployees", CommandType.StoredProcedure, paramList);
        //    //SQLHelper.ExecuteNonQuery("[sp_InsertEmployeesHO", CommandType.StoredProcedure, paramList);
        //}

        //public void InsertSupplierData(Int32 Code,String Name)
        //{
        //    SqlParameter param = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();

        //    param = SQLHelper.CreateParameter("@SupplierCode", SqlDbType.VarChar);
        //    param.Value = Code;
        //    paramList.Add(param);

        //    param = SQLHelper.CreateParameter("@SupplierName", SqlDbType.VarChar);
        //    param.Value = Name;
        //    paramList.Add(param);

        //    SQLHelper.ExecuteNonQuery("sp_InsertSuppliers", CommandType.StoredProcedure, paramList);
        //}

        public DataTable getCashWork()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DateEntered"));
            dt.Columns.Add(new DataColumn("DivisionCode"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("AdvanceCode"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("LentDivisionCode"));
            dt.Columns.Add(new DataColumn("LentWorkCode"));
            dt.Columns.Add(new DataColumn("AdditionType"));
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("ManDays"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  DateEntered, DivisionCode, Field, EmpNo, AdvanceCode, Amount, WorkCode, LentDivisionCode, LentWorkCode, AdditionType, EstateID, Kilos, ManDays FROM   dbo.CashWorkDetails WHERE  (DateEntered BETWEEN CONVERT(DATETIME, '" + DatDateEntered + "', 102) AND CONVERT(DATETIME, '" + DatToDate + "', 102))", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).ToString();
                }

                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                else
                    dtrow[1] = "NA";

                if (!dataReader.IsDBNull(2))
                {
                    if (dataReader.GetString(2).Trim() == "")
                        dtrow[2] = "NA";
                    else
                        dtrow[2] = dataReader.GetString(2);
                }
                else
                    dtrow[2] = "NA";

                if (!dataReader.IsDBNull(3))
                {
                    if (dataReader.GetString(3).Trim() == "")
                        dtrow[3] = "NA";
                    else
                        dtrow[3] = dataReader.GetString(3);
                }
                else
                    dtrow[3] = "NA";

                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                else
                    dtrow[4] = "NA";

                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetDecimal(5);
                }
                else
                    dtrow[5] = 0;

                if (!dataReader.IsDBNull(6))
                {
                    if (dataReader.GetString(6).Trim() == "")
                        dtrow[6] = "NA";
                    else
                        dtrow[6] = dataReader.GetString(6);
                }
                else
                    dtrow[6] = "NA";

                if (!dataReader.IsDBNull(7))
                {
                    if (dataReader.GetString(7).Trim() == "")
                        dtrow[7] = "NA";
                    else
                        dtrow[7] = dataReader.GetString(7);
                }
                else
                    dtrow[7] = "NA";

                if (!dataReader.IsDBNull(8))
                {
                    if (dataReader.GetString(8).Trim() == "")
                        dtrow[8] = "NA";
                    else
                        dtrow[8] = dataReader.GetString(8);
                }
                else
                    dtrow[8] = "NA";

                if (!dataReader.IsDBNull(9))
                {
                    if (dataReader.GetString(9).Trim() == "")
                        dtrow[9] = "NA";
                    else
                        dtrow[9] = dataReader.GetString(9);
                }
                else
                    dtrow[9] = "NA";

                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetString(10).Trim();
                }
                else
                    dtrow[10] = "NA";
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                else
                    dtrow[11] = 0;
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                else
                    dtrow[12] = 0;
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();


            return dt;

        }

        /*Cash Work From OLAX checkroll*/
        public DataTable getCashWorkFromOLAX()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DateEntered"));
            dt.Columns.Add(new DataColumn("DivisionCode"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("AdvanceCode"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("LentDivisionCode"));
            dt.Columns.Add(new DataColumn("LentWorkCode"));
            dt.Columns.Add(new DataColumn("AdditionType"));
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("ManDays"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     DateEntered, DivisionID AS DivisionCode, FieldID AS Field, EmpNo, CASE WHEN (WorkCodeID = 'PLK') THEN 'CASHP' ELSE 'CASHW' END AS AdvanceCode, CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN dbo.DailyGroundTransactions.CashKgAmount ELSE dbo.DailyGroundTransactions.CashSundryAmount END AS Amount,  CASE WHEN (LabourType <> 'LentLabour') THEN dbo.DailyGroundTransactions.WorkCodeID ELSE CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  THEN 'LLP' ELSE 'LLO' END END AS WorkCode, LabourDivision AS LentDivisionCode, CASE WHEN (LabourType <> 'LentLabour')  THEN 'NA' ELSE dbo.DailyGroundTransactions.WorkCodeID END AS LentWorkCode, 'A' AS AdditionType, EstateID,  WorkQty AS Qty,CashManDays as ManDays FROM dbo.DailyGroundTransactions WHERE    (DateEntered BETWEEN CONVERT(DATETIME, '" + DatDateEntered + "', 102) AND CONVERT(DATETIME, '" + DatToDate + "', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 2)", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetDateTime(0);
                }

                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                else
                    dtrow[1] = "NA";

                if (!dataReader.IsDBNull(2))
                {
                    if (dataReader.GetString(2).Trim() == "")
                        dtrow[2] = "NA";
                    else
                        dtrow[2] = dataReader.GetString(2);
                }
                else
                    dtrow[2] = "NA";

                if (!dataReader.IsDBNull(3))
                {
                    if (dataReader.GetString(3).Trim() == "")
                        dtrow[3] = "NA";
                    else
                        dtrow[3] = dataReader.GetString(3);
                }
                else
                    dtrow[3] = "NA";

                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                else
                    dtrow[4] = "NA";

                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetDecimal(5);
                }
                else
                    dtrow[5] = 0;

                if (!dataReader.IsDBNull(6))
                {
                    if (dataReader.GetString(6).Trim() == "")
                        dtrow[6] = "NA";
                    else
                        dtrow[6] = dataReader.GetString(6);
                }
                else
                    dtrow[6] = "NA";

                if (!dataReader.IsDBNull(7))
                {
                    if (dataReader.GetString(7).Trim() == "")
                        dtrow[7] = "NA";
                    else
                        dtrow[7] = dataReader.GetString(7);
                }
                else
                    dtrow[7] = "NA";

                if (!dataReader.IsDBNull(8))
                {
                    if (dataReader.GetString(8).Trim() == "")
                        dtrow[8] = "NA";
                    else
                        dtrow[8] = dataReader.GetString(8);
                }
                else
                    dtrow[8] = "NA";

                if (!dataReader.IsDBNull(9))
                {
                    if (dataReader.GetString(9).Trim() == "")
                        dtrow[9] = "NA";
                    else
                        dtrow[9] = dataReader.GetString(9);
                }
                else
                    dtrow[9] = "NA";

                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetString(10).Trim();
                }
                else
                    dtrow[10] = "NA";
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                else
                    dtrow[11] = 0;
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                else
                    dtrow[12] = 0;
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();


            return dt;

        }

        public Boolean IsCheckrollFromOLAXCheckroll()
        {
            Boolean boolChkFromOlax = false;
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT ISNULL(Name,'JRL') FROM dbo.FTSCheckRollSettings WHERE (Type = 'CheckrollBy')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetString(0).Equals("OLAX"))
                    {
                        boolChkFromOlax = true;
                    }
                }
            }
            return boolChkFromOlax;

        }

        public DataTable ListEmpMasterData()
        {
            DataTable dt = new DataTable("EmpMaster");
            dt.Columns.Add(new DataColumn("EstateID"));//0
            dt.Columns.Add(new DataColumn("DivisionID"));//1
            dt.Columns.Add(new DataColumn("EMPNo"));//2
            dt.Columns.Add(new DataColumn("EPFNo"));//3
            dt.Columns.Add(new DataColumn("EMPName"));//4
            dt.Columns.Add(new DataColumn("Gender"));//5
            dt.Columns.Add(new DataColumn("EmployeeStatus"));//6
            dt.Columns.Add(new DataColumn("DateJoined"));//7
            dt.Columns.Add(new DataColumn("ActiveEmployee"));//8
            dt.Columns.Add(new DataColumn("EmpCategory"));//9
            dt.Columns.Add(new DataColumn("ConfirmedDate"));//910
            dt.Columns.Add(new DataColumn("ResignedDate"));//11
            dt.Columns.Add(new DataColumn("dateOfBirth"));//12
            dt.Columns.Add(new DataColumn("NICNo"));//13
            dt.Columns.Add(new DataColumn("UnionNameCode"));//14
            dt.Columns.Add(new DataColumn("LastName"));//15
            dt.Columns.Add(new DataColumn("Initials"));//16
            dt.Columns.Add(new DataColumn("OCGrade"));//17
            dt.Columns.Add(new DataColumn("ZoneCode"));//18
            dt.Columns.Add(new DataColumn("MemberStatus"));//19
            dt.Columns.Add(new DataColumn("EmployerNo"));//20
            dt.Columns.Add(new DataColumn("EmpCategoryName"));//20

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EstateID, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee,  dbo.EmployeeMaster.EmpCategory, dbo.EmployeeMaster.ConfirmDate, dbo.EmployeeMaster.ResignedDate, dbo.EmployeeMaster.dateOfBirth,  dbo.EmployeeMaster.NICNo, dbo.EmployeeMaster.UnionNameCode, dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.OCGrade,  dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.MemberStatus, dbo.EmployeeMaster.EmployerNo, dbo.EmployeeCategory.CategoryName FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID", CommandType.Text);
            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                else
                    dtrow[0] = "NA";

                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                else
                    dtrow[1] = "NA";
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                else
                    dtrow[2] = "NA";
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                else
                    dtrow[3] = "NA";
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                else
                    dtrow[4] = "NA";
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                else
                    dtrow[5] = "NA";
                if (!dataReader.IsDBNull(6))
                {
                    switch (dataReader.GetString(6).Trim())
                    {
                        case "I":
                            dtrow[6] = "Inactive";
                            break;
                        case "A":
                            dtrow[6] = "Active";
                            break;
                        case "P":
                            dtrow[6] = "Paid Off";
                            break;
                        default:
                            dtrow[6] = dataReader.GetString(6).Trim();
                            break;
                    }

                }
                else
                    dtrow[6] = "NA";
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7);
                }
                else
                    dtrow[7] = Convert.ToDateTime("1901-1-1");
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetBoolean(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetInt32(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDateTime(10);
                }
                else
                    dtrow[10] = Convert.ToDateTime("1901-1-1");
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDateTime(11);
                }
                else
                    dtrow[11] = Convert.ToDateTime("1901-1-1");
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDateTime(12);
                }
                else
                    dtrow[12] = Convert.ToDateTime("1901-1-1");

                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetString(13).Trim();
                }
                else
                    dtrow[13] = "NA";

                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetString(14).Trim();
                }
                else
                    dtrow[14] = "NA";
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15).Trim();
                }
                else
                    dtrow[15] = "NA";
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetString(16).Trim();
                }
                else
                    dtrow[16] = "NA";
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetString(17).Trim();
                }
                else
                    dtrow[17] = "NA";
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetString(18).Trim();
                }
                else
                    dtrow[18] = "NA";
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetString(19).Trim();
                }
                else
                    dtrow[19] = "NA";
                if (!dataReader.IsDBNull(20))
                {
                    dtrow[20] = dataReader.GetString(20).Trim();
                }
                else
                    dtrow[20] = "NA";
                if (!dataReader.IsDBNull(21))
                {
                    dtrow[21] = dataReader.GetString(21).Trim();
                }
                else
                    dtrow[21] = "NA";

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            return dt;

        }

        //public DataTable getEmpMasterData()
        //{
        //    DataTable dt = new DataTable("EmpMaster");
        //    dt.Columns.Add(new DataColumn("EstateID"));//0
        //    dt.Columns.Add(new DataColumn("DivisionID"));//1
        //    dt.Columns.Add(new DataColumn("EPFNo"));//2
        //    dt.Columns.Add(new DataColumn("EMPName"));//3
        //    dt.Columns.Add(new DataColumn("Gender"));//4
        //    dt.Columns.Add(new DataColumn("DateJoined"));//5
        //    dt.Columns.Add(new DataColumn("ActiveEmployee"));//6

        //    DataRow dtrow;
        //    SqlDataReader dataReader;
        //    dtrow = dt.NewRow();
        //    dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EPFNo, EMPName, Gender, DateJoined, ActiveEmployee FROM dbo.EmployeeMaster", CommandType.Text);
        //    while (dataReader.Read())
        //    {
        //        dtrow = dt.NewRow();

        //        if (!dataReader.IsDBNull(0))
        //        {
        //            dtrow[0] = dataReader.GetString(0).Trim();
        //        }
        //        if (!dataReader.IsDBNull(1))
        //        {
        //            dtrow[1] = dataReader.GetString(1).Trim();
        //        }
        //        if (!dataReader.IsDBNull(2))
        //        {
        //            dtrow[2] = dataReader.GetString(2).Trim();
        //        }
        //        if (!dataReader.IsDBNull(3))
        //        {
        //            dtrow[3] = dataReader.GetString(3).Trim();
        //        }
        //        if (!dataReader.IsDBNull(4))
        //        {
        //            dtrow[4] = dataReader.GetString(4).Trim();
        //        }
        //        if (!dataReader.IsDBNull(5))
        //        {
        //            dtrow[5] = dataReader.GetDateTime(5);
        //        }
        //        if (!dataReader.IsDBNull(6))
        //        {
        //            dtrow[6] = dataReader.GetBoolean(6);
        //        }

        //        dt.Rows.Add(dtrow);
        //    }
        //    dataReader.Close();

        //    return dt;

        //}

        public Boolean IsConfirmationsOK(DateTime dtFrom, DateTime dtTo)
        {
            Boolean boolpending = true;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     EntryDate, ConfirmYesNo FROM dbo.CHKDateConfirmations WHERE (EntryDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (ConfirmYesNo = 0)", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolpending = false;
            }
            return boolpending;

        }

        public Boolean IsCheckrollProcessed(Int32 intYear, Int32 intMonth)
        {
            Boolean boolProcessed = false;
            Int32 intCount = 0;
            SqlDataReader datareader;
            datareader = SQLHelper.ExecuteReader("SELECT  ISNULL(COUNT(Division), 0) AS entrycount FROM dbo.CHKProcessDetails WHERE (ProcessYear = '" + intYear + "') AND (ProcessMonth = '" + intMonth + "') AND  (ProcessedYesNo = 1)", CommandType.Text);
            while (datareader.Read())
            {
                if (!datareader.IsDBNull(0))
                {
                    intCount = datareader.GetInt32(0);
                }
            }
            if (intCount > 0)
                boolProcessed = true;

            return boolProcessed;
        }

        public DataSet ListCheckRollMonthlyEarnings(Int32 Year, Int32 Month)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT dbo.EmpMonthlyEarnings.Month, dbo.EmpMonthlyEarnings.Year, dbo.EmpMonthlyEarnings.DivisionId, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.Sex, dbo.EmpMonthlyEarnings.Category, dbo.EmpMonthlyEarnings.PluckingManDays, dbo.EmpMonthlyEarnings.SundryManDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays, dbo.EmpMonthlyEarnings.HolidaySundryManDays, dbo.EmpMonthlyEarnings.PluckingKilos, dbo.EmpMonthlyEarnings.OverKilos, dbo.EmpMonthlyEarnings.AttIncentive, dbo.EmpMonthlyEarnings.WorkedPercentage, dbo.EmpMonthlyEarnings.PluckingNamePay, dbo.EmpMonthlyEarnings.OverKilosPay, dbo.EmpMonthlyEarnings.SundryNamePay, dbo.EmpMonthlyEarnings.ExtraRates, dbo.EmpMonthlyEarnings.PRIAmount, dbo.EmpMonthlyEarnings.OtherEPFAdditions, dbo.EmpMonthlyEarnings.EPFPaybleAmount, dbo.EmpMonthlyEarnings.OverTime, dbo.EmpMonthlyEarnings.CashPlucking, dbo.EmpMonthlyEarnings.CashSundry, dbo.EmpMonthlyEarnings.TotalEarnings, dbo.EmpMonthlyEarnings.TotalPayEPF, dbo.EmpMonthlyEarnings.EPF12, dbo.EmpMonthlyEarnings.EPF10, dbo.EmpMonthlyEarnings.ETF3, dbo.EmpMonthlyEarnings.HolidayHalfNames, dbo.EmpMonthlyEarnings.EPFAmount, dbo.EmpMonthlyEarnings.ETFAmount, dbo.EmpMonthlyEarnings.CashManDays, dbo.EmpMonthlyEarnings.PaidHolidays, dbo.EmpMonthlyEarnings.OtherAdditions, dbo.EmpMonthlyEarnings.PreviousMadeUpCoins, dbo.EstateDivision.EstateID, isnull(dbo.EmpMonthlyEarnings.BlockKilos,0) as BlockKilos, isnull(dbo.EmpMonthlyEarnings.BlockKiloAmount,0) as BlockKiloAmount, isnull(dbo.EmpMonthlyEarnings.CashKilos,0) as CashKilos, isnull(dbo.EmpMonthlyEarnings.CashKiloAmount,0) as CashKiloAmount, isnull(dbo.EmpMonthlyEarnings.PlkHalfNames,0) as PlkHalfNames,isnull( dbo.EmpMonthlyEarnings.PlkHalfNamePay,0) as plkHalfNamePay, isnull(dbo.EmpMonthlyEarnings.SundryHalfNames,0) as SunHalfNames, isnull(dbo.EmpMonthlyEarnings.SundryHalfNamePay,0) as SundryHalfNamePay , isnull(dbo.EmpMonthlyEarnings.ContractorKgPay,0) as ContractorKgPay, isnull(dbo.EmpMonthlyEarnings.ContractorSundryPay,0) as ContractorSundryPay,isnull(dbo.EmpMonthlyEarnings.ContractLabourKgPay,0) as ContractLabourKgPay ,isnull(dbo.EmpMonthlyEarnings.ContractLabourSundryPay,0) as  ContractLabourSundryPay,isnull(dbo.EmpMonthlyEarnings.ContractKilos,0) as ContractKilos,isnull( dbo.EmpMonthlyEarnings.QualifyDays,0) as QualifyDays, isnull(dbo.EmpMonthlyEarnings.OfferedDays,0) as OfferedDays FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EstateDivision ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "')", CommandType.Text);
            da.Fill(ds, "MonthlyEarnings");
            return ds;
        }

        public DataTable ListCheckRollRecoveries(Int32 Year, Int32 Month)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("EMPNo"));
            dt.Columns.Add(new DataColumn("EMPName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("Year"));
            dt.Columns.Add(new DataColumn("Month"));
            dt.Columns.Add(new DataColumn("DeductionGroup"));
            dt.Columns.Add(new DataColumn("Deduction"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DivisionName"));
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("DeductGroup"));
            dt.Columns.Add(new DataColumn("DeductId"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.CHKEmpDeductions.DeductYear, dbo.CHKEmpDeductions.DeductMonth, dbo.CHKDeductionGroup.GroupName, replace(dbo.CHKDeduction.DeductionName,'''',''), dbo.CHKEmpDeductions.Amount, dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EstateID, dbo.CHKEmpDeductions.DeductGroupId, dbo.CHKEmpDeductions.DeductId FROM dbo.CHKEmpDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EstateDivision ON dbo.CHKEmpDeductions.DivisionId = dbo.EstateDivision.DivisionID WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "')", CommandType.Text);

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
                    dtrow[3] = dataReader.GetInt32(3);
                }

                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }

                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }

                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetString(10).Trim();
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetInt32(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetInt32(12);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataSet ListCheckRollMonthlyDeductions(Int32 Year, Int32 Month)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT dbo.EmpMonthlyDeductions.EmpNo, dbo.EmpMonthlyDeductions.Month, dbo.EmpMonthlyDeductions.Year, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyDeductions.EPFAmount, dbo.EmpMonthlyDeductions.FestivalAdvance, dbo.EmpMonthlyDeductions.MonthlyAdvance, dbo.EmpMonthlyDeductions.UnionSubscription, dbo.EmpMonthlyDeductions.Cooperative, dbo.EmpMonthlyDeductions.ReligiousActivities, dbo.EmpMonthlyDeductions.TeaCoconutOther, dbo.EmpMonthlyDeductions.FoodStuff, dbo.EmpMonthlyDeductions.Welfare, dbo.EmpMonthlyDeductions.Dhoby, dbo.EmpMonthlyDeductions.Barber, dbo.EmpMonthlyDeductions.Insuarance, dbo.EmpMonthlyDeductions.penalty, dbo.EmpMonthlyDeductions.PreviousDebits, dbo.EmpMonthlyDeductions.PayDetailSlip, dbo.EmpMonthlyDeductions.BankLoan, dbo.EmpMonthlyDeductions.Others, dbo.EmpMonthlyDeductions.TotalDeductions, dbo.EmpMonthlyDeductions.PeriodFrom, dbo.EmpMonthlyDeductions.PeriodTo, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EstateID FROM dbo.EmpMonthlyDeductions INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.EmpMonthlyDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyDeductions.Month = '" + Month + "') AND (dbo.EmpMonthlyDeductions.Year = '" + Year + "')", CommandType.Text);
            da.Fill(ds, "MonthlyDeductions");
            return ds;
        }

        public DataSet ListCheckRollFinalPay(Int32 Year, Int32 Month)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyFinalWeges.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyFinalWeges.TotalEarnigs, dbo.EmpMonthlyFinalWeges.TotalDeductions, dbo.EmpMonthlyFinalWeges.SalaryAmount, dbo.EmpMonthlyFinalWeges.NetWages, dbo.EmpMonthlyFinalWeges.WagePay, dbo.EmpMonthlyFinalWeges.MadeUpBalance, dbo.EmpMonthlyFinalWeges.WageYear, dbo.EmpMonthlyFinalWeges.WageMonth, dbo.EmpMonthlyFinalWeges.DebitsBF, dbo.EstateDivision.EstateID FROM dbo.EmpMonthlyFinalWeges INNER JOIN dbo.EstateDivision ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyFinalWeges.WageYear = '" + Year + "') AND (dbo.EmpMonthlyFinalWeges.WageMonth = '" + Month + "')", CommandType.Text);
            da.Fill(ds, "FinalPay");
            return ds;
        }

        public DataSet ListCheckRollRFTDeductions(Int32 Year, Int32 Month)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT dbo.CHKRFTDeductions.DYear, dbo.CHKRFTDeductions.DMonth, dbo.CHKRFTDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.DeductionName, dbo.CHKRFTDeductions.Division, dbo.CHKRFTDeductions.RFTType, dbo.CHKRFTDeductions.RFTRate, dbo.CHKRFTDeductions.RFTQty, dbo.CHKRFTDeductions.RFTDeductAmount, isnull(dbo.CHKRFTDeductions.OldEntryYesNo,0) as OldEntry FROM dbo.CHKRFTDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKRFTDeductions.EmpNo = dbo.EmployeeMaster.EmpNo AND dbo.CHKRFTDeductions.Division = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKRFTDeductions.DYear = '" + Year + "') AND (dbo.CHKRFTDeductions.DMonth = '" + Month + "')", CommandType.Text);
            da.Fill(ds, "RFTDeductions");
            return ds;
        }

        public DataTable ListCheckRollMonthlyOverTime(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("otDate"));
            dt.Columns.Add(new DataColumn("WorkType"));
            dt.Columns.Add(new DataColumn("CategoryName"));
            dt.Columns.Add(new DataColumn("DivisionCode"));
            dt.Columns.Add(new DataColumn("EMPName"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("Job"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("Hours"));
            dt.Columns.Add(new DataColumn("Expenditure"));
            dt.Columns.Add(new DataColumn("LabourType"));//10
            dt.Columns.Add(new DataColumn("LabourEstate"));
            dt.Columns.Add(new DataColumn("LabourDivision"));
            dt.Columns.Add(new DataColumn("LabourField"));
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("OTType"));
            dt.Columns.Add(new DataColumn("EmployeeNO"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT   DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.CHKOvertime.OtDate)) as otDate, dbo.CHKOvertime.WorkType, dbo.EmployeeCategory.CategoryName, dbo.CHKOvertime.DivisionCode, dbo.EmployeeMaster.EMPName,  dbo.CHKOvertime.Field, dbo.CHKOvertime.Job, replace(dbo.JobMaster.JobName,'''','') as JobName, dbo.CHKOvertime.Hours, dbo.CHKOvertime.Expenditure, dbo.CHKOvertime.LabourType,  dbo.CHKOvertime.LabourEstate, dbo.CHKOvertime.LabourDivision, dbo.CHKOvertime.LabourField, dbo.EmployeeMaster.EstateID, dbo.CHKOTParameters.OTType,  dbo.CHKOvertime.EmployeeNo FROM dbo.CHKOvertime INNER JOIN dbo.EmployeeCategory ON dbo.CHKOvertime.CategoryCode = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EmployeeMaster ON dbo.CHKOvertime.DivisionCode = dbo.EmployeeMaster.DivisionID AND  dbo.CHKOvertime.EmployeeNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId WHERE (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102))", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetString(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetDecimal(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetString(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetString(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetString(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15);
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetString(16);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        /*GET CHECKROLL FROM OLAX CHECKROLL*/
        public DataTable ListPendingCheckRollDataFromOLAX()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EstateID"));//0
            dt.Columns.Add(new DataColumn("DateEntered"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EMPNo"));
            dt.Columns.Add(new DataColumn("WorkCodeID"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("WorkQty"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("EstateName"));
            dt.Columns.Add(new DataColumn("DivisionName"));
            dt.Columns.Add(new DataColumn("WorkCodeName"));//10
            dt.Columns.Add(new DataColumn("Mandays"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("OverKilos"));//14
            dt.Columns.Add(new DataColumn("CropType"));//15
            dt.Columns.Add(new DataColumn("WorkCodeCategory"));
            dt.Columns.Add(new DataColumn("WorkType"));
            dt.Columns.Add(new DataColumn("EPFNo"));
            dt.Columns.Add(new DataColumn("LabourType"));
            dt.Columns.Add(new DataColumn("LabourEstate"));//20
            dt.Columns.Add(new DataColumn("LabourDivision"));
            dt.Columns.Add(new DataColumn("LabourField"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("Expenditure"));
            dt.Columns.Add(new DataColumn("PluckingExpenditure"));//25
            dt.Columns.Add(new DataColumn("PRIAmount"));
            dt.Columns.Add(new DataColumn("ExtraRates"));
            dt.Columns.Add(new DataColumn("OverKgAmount"));
            dt.Columns.Add(new DataColumn("CashKgAmount"));
            dt.Columns.Add(new DataColumn("CashSundryAmount"));//30
            dt.Columns.Add(new DataColumn("CashKgs"));
            dt.Columns.Add(new DataColumn("DailyBasic"));
            dt.Columns.Add(new DataColumn("HolidayYesNo"));
            dt.Columns.Add(new DataColumn("CashManDays"));
            dt.Columns.Add(new DataColumn("EPF10"));//35
            dt.Columns.Add(new DataColumn("EPF12"));
            dt.Columns.Add(new DataColumn("ETF3"));
            dt.Columns.Add(new DataColumn("IncentiveAmount"));
            dt.Columns.Add(new DataColumn("NormKilos"));
            dt.Columns.Add(new DataColumn("HolidayManDays"));//40
            dt.Columns.Add(new DataColumn("HollidayHalfNames"));
            dt.Columns.Add(new DataColumn("PaidHoliday"));
            dt.Columns.Add(new DataColumn("CashBlockYesNo"));
            dt.Columns.Add(new DataColumn("BlockPlkAmount"));
            dt.Columns.Add(new DataColumn("BlockPlkKgs"));//45
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));
            dt.Columns.Add(new DataColumn("EasyweighYesNo"));
            dt.Columns.Add(new DataColumn("CashPlkOkgYesNo"));
            dt.Columns.Add(new DataColumn("SpecialHalfYesNo"));//50
            dt.Columns.Add(new DataColumn("DailyBasicAmount"));//51
           

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EstateID, dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo,  dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.UserID, dbo.Estate.EstateName, dbo.EstateDivision.DivisionName,  dbo.JobMaster.JobName AS WorkCodeName, dbo.DailyGroundTransactions.ManDays, dbo.EmployeeMaster.EMPName AS EmpName, dbo.EmployeeMaster.Gender,  dbo.DailyGroundTransactions.OverKgs AS OverKilos, dbo.DailyGroundTransactions.CropType, dbo.JobMaster.JobGroup AS WorkCodeCategory FROM dbo.DailyGroundTransactions INNER JOIN dbo.Estate ON dbo.DailyGroundTransactions.EstateID = dbo.Estate.EstateID INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + DatDateEntered + "', 102) AND CONVERT(DATETIME, '" + DatToDate + "', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 1) ", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT     dbo.DailyGroundTransactions.EstateID, dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo,  dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.UserID,  dbo.Estate.EstateName, dbo.EstateDivision.DivisionName, dbo.JobMaster.JobName AS WorkCodeName, dbo.DailyGroundTransactions.ManDays,  dbo.EmployeeMaster.EMPName AS EmpName, dbo.EmployeeMaster.Gender, dbo.DailyGroundTransactions.OverKgs AS OverKilos,  dbo.DailyGroundTransactions.CropType, dbo.JobMaster.JobGroup AS WorkCodeCategory, dbo.DailyGroundTransactions.WorkType,  dbo.DailyGroundTransactions.EPFNo, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.LabourEstate,  dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.LabourField, dbo.DailyGroundTransactions.FullHalf,  dbo.DailyGroundTransactions.Expenditure, dbo.DailyGroundTransactions.PluckingExpenditure, dbo.DailyGroundTransactions.PRIAmount,  dbo.DailyGroundTransactions.ExtraRates, dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.CashKgAmount,  dbo.DailyGroundTransactions.CashSundryAmount, dbo.DailyGroundTransactions.CashKgs, dbo.DailyGroundTransactions.DailyBasic,  dbo.DailyGroundTransactions.HolidayYesNo, dbo.DailyGroundTransactions.CashManDays, dbo.DailyGroundTransactions.EPF10,  dbo.DailyGroundTransactions.EPF12, dbo.DailyGroundTransactions.ETF3, dbo.DailyGroundTransactions.IncentiveAmount, dbo.DailyGroundTransactions.NormKilos,  dbo.DailyGroundTransactions.HolidayManDays, dbo.DailyGroundTransactions.HolidayHalfNames, dbo.DailyGroundTransactions.PaidHoliday,  dbo.DailyGroundTransactions.CashBlockYesNo, dbo.DailyGroundTransactions.BlockPlkAmount, dbo.DailyGroundTransactions.BlockPlkKgs,  dbo.DailyGroundTransactions.AreaCovered, dbo.DailyGroundTransactions.FieldWeight, dbo.DailyGroundTransactions.EasyweighYesNo,  dbo.DailyGroundTransactions.CashPlkOkgYesNo, dbo.DailyGroundTransactions.SpeciallHalfYesNo, dbo.DailyGroundTransactions.DailyBasicAmount FROM         dbo.DailyGroundTransactions INNER JOIN dbo.Estate ON dbo.DailyGroundTransactions.EstateID = dbo.Estate.EstateID INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + DatDateEntered + "', 102) AND CONVERT(DATETIME, '" + DatToDate + "', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 1)", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }

                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetDateTime(1).ToString();
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
                    dtrow[4] = dataReader.GetString(4).Trim();
                }

                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }

                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6).ToString();
                }

                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7).Trim();
                }

                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }

                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetString(10).Trim();
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11).ToString();
                }

                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetString(12).Trim();
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetString(13).Trim();
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDecimal(14).ToString();
                }
                //CropType
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetInt32(15).ToString();
                }
                //WorkCodeCategory
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetInt32(16).ToString();
                }
                else
                    dtrow[16] = 0;

                //WorkType
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetInt32(17).ToString();
                }
                //EPFNo
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetString(18);
                }
                else
                    dtrow[18] = "000000";
                //Labour Type
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetString(19);
                }

                //Labour Estate
                if (!dataReader.IsDBNull(20))
                {
                    dtrow[20] = dataReader.GetString(20);
                }
                else
                    dtrow[20] = "NA";
                //Labour Division
                if (!dataReader.IsDBNull(21))
                {
                    dtrow[21] = dataReader.GetString(21);
                }
                else
                    dtrow[21] = "NA";
                //Labour Field
                if (!dataReader.IsDBNull(22))
                {
                    dtrow[22] = dataReader.GetString(22);
                }
                else
                    dtrow[22] = "NA";
                //Full Half
                if (!dataReader.IsDBNull(23))
                {
                    dtrow[23] = dataReader.GetInt32(23);
                }
                //Expenditure
                if (!dataReader.IsDBNull(24))
                {
                    dtrow[24] = dataReader.GetDecimal(24).ToString();
                }
                else
                    dtrow[24] = 0;
                //Plk Expenditure
                if (!dataReader.IsDBNull(25))
                {
                    dtrow[25] = dataReader.GetDecimal(25).ToString();
                }
                else
                    dtrow[25] = 0;
                //PRI
                if (!dataReader.IsDBNull(26))
                {
                    dtrow[26] = dataReader.GetDecimal(26).ToString();
                }
                else
                    dtrow[26] = 0;
                //ExtraRate
                if (!dataReader.IsDBNull(27))
                {
                    dtrow[27] = dataReader.GetDecimal(27).ToString();
                }
                else
                    dtrow[27] = 0;
                //OverKgAmount
                if (!dataReader.IsDBNull(28))
                {
                    dtrow[28] = dataReader.GetDecimal(28).ToString();
                }
                else
                    dtrow[28] = 0;
                //CashKgAmount
                if (!dataReader.IsDBNull(29))
                {
                    dtrow[29] = dataReader.GetDecimal(29).ToString();
                }
                else
                    dtrow[29] = 0;
                //CashSundryAmount
                if (!dataReader.IsDBNull(30))
                {
                    dtrow[30] = dataReader.GetDecimal(30).ToString();
                }
                else
                    dtrow[30] = 0;
                //CashKgs
                if (!dataReader.IsDBNull(31))
                {
                    dtrow[31] = dataReader.GetDecimal(31).ToString();
                }
                else
                    dtrow[31] = 0;
                //DailyBasic
                if (!dataReader.IsDBNull(32))
                {
                    dtrow[32] = dataReader.GetDecimal(32).ToString();
                }
                else
                    dtrow[32] = 0;
                //HolidayYesNo
                if (!dataReader.IsDBNull(33))
                {
                    dtrow[33] = dataReader.GetBoolean(33).ToString();
                }

                //CashMandays
                if (!dataReader.IsDBNull(34))
                {
                    dtrow[34] = dataReader.GetDecimal(34).ToString();
                }
                else
                    dtrow[34] = 0;
                //EFP10
                if (!dataReader.IsDBNull(35))
                {
                    dtrow[35] = dataReader.GetDecimal(35).ToString();
                }
                else
                    dtrow[35] = 0;
                //EFP12
                if (!dataReader.IsDBNull(36))
                {
                    dtrow[36] = dataReader.GetDecimal(36).ToString();
                }
                else
                    dtrow[36] = 0;
                //ETF3
                if (!dataReader.IsDBNull(37))
                {
                    dtrow[37] = dataReader.GetDecimal(37).ToString();
                }
                else
                    dtrow[37] = 0;
                //Incentive
                if (!dataReader.IsDBNull(38))
                {
                    dtrow[38] = dataReader.GetDecimal(38).ToString();
                }
                else
                    dtrow[38] = 0;
                //NormKilos
                if (!dataReader.IsDBNull(39))
                {
                    dtrow[39] = dataReader.GetDecimal(39).ToString();
                }
                else
                    dtrow[39] = 0;
                //HolidayManDays
                if (!dataReader.IsDBNull(40))
                {
                    dtrow[40] = dataReader.GetDecimal(40).ToString();
                }
                else
                    dtrow[40] = 0;
                //HolidayHalfNames
                if (!dataReader.IsDBNull(41))
                {
                    dtrow[41] = dataReader.GetDecimal(41).ToString();
                }
                else
                    dtrow[41] = 0;
                //PaidHoliday
                if (!dataReader.IsDBNull(42))
                {
                    dtrow[42] = dataReader.GetBoolean(42).ToString();
                }
                else
                    dtrow[42] = false;
                //CashBlockYesNo
                if (!dataReader.IsDBNull(43))
                {
                    dtrow[43] = dataReader.GetBoolean(43).ToString();
                }
                else
                    dtrow[43] = false;
                //BlockPlkAmt
                if (!dataReader.IsDBNull(44))
                {
                    dtrow[44] = dataReader.GetDecimal(44).ToString();
                }
                else
                    dtrow[44] = 0;
                //BlockPlkKgs
                if (!dataReader.IsDBNull(45))
                {
                    dtrow[45] = dataReader.GetDecimal(45).ToString();
                }
                else
                    dtrow[45] = 0;
                //AreaCovered
                if (!dataReader.IsDBNull(46))
                {
                    dtrow[46] = dataReader.GetDecimal(46).ToString();
                }
                else
                    dtrow[46] = 0;
                //FieldWeight
                if (!dataReader.IsDBNull(47))
                {
                    dtrow[47] = dataReader.GetDecimal(47).ToString();
                }
                else
                    dtrow[47] = 0;
                //EasyWeighYesNo
                if (!dataReader.IsDBNull(48))
                {
                    dtrow[48] = dataReader.GetBoolean(48).ToString();
                }
                else
                    dtrow[48] = false;
                //CashPlkOkg
                if (!dataReader.IsDBNull(49))
                {
                    dtrow[49] = dataReader.GetBoolean(49).ToString();
                }
                else
                    dtrow[49] = 0;
                //SpecialHalf
                if (!dataReader.IsDBNull(50))
                {
                    dtrow[50] = dataReader.GetBoolean(50).ToString();
                }
                else
                    dtrow[50] = false;
                //DailyBasicAmount
                if (!dataReader.IsDBNull(51))
                {
                    dtrow[51] = dataReader.GetDecimal(51).ToString();
                }
                else
                    dtrow[51] = 0;

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataSet ListPendingAmalgamationSummary(Int32 intYear, Int32 intMonth)
        {

            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.EmpMonthlyEarnings.Year, dbo.EmpMonthlyEarnings.Month as MonthID, dbo.Estate.EstateID, dbo.EmpMonthlyEarnings.DivisionId as DivisionID, dbo.EmployeeMaster.EPFNo,  dbo.EmpMonthlyEarnings.EmpNO as EmpCode, dbo.EmployeeMaster.EMPName as EmpName, dbo.EmployeeCategory.CategoryName as EmpCategory, dbo.EmpMonthlyEarnings.PluckingManDays as PLKDays,  dbo.EmpMonthlyEarnings.SundryManDays as SundryDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays as HolidayPLKDays, dbo.EmpMonthlyEarnings.HolidaySundryManDays as HolidaySundryDays,  dbo.EmpMonthlyEarnings.OverKilos as OverKilos, 0 as AMOT, 0 AS PMOT, dbo.EmpMonthlyEarnings.OverTime AS AMOTAmount, 0 AS PMOTAmount,  dbo.EmpMonthlyEarnings.AttIncentive + dbo.EmpMonthlyEarnings.ExtraRates AS IncentiveWithExtraRate, dbo.EmpMonthlyEarnings.OtherEPFAdditions as ExtraAddition,  dbo.EmpMonthlyEarnings.CashSundry / 450 AS CashWorkDays, 0 AS EPFAddition15, dbo.EmpMonthlyDeductions.TeaCoconutOther as TeaDeduction, dbo.EmpMonthlyDeductions.FoodStuff as FoodStuff,  dbo.EmpMonthlyDeductions.MonthlyAdvance as CashAdvance, dbo.EmpMonthlyDeductions.Others as Stamps, dbo.EmpMonthlyDeductions.ReligiousActivities as Religion,  dbo.EmpMonthlyDeductions.FestivalAdvance as Festival, dbo.EmpMonthlyDeductions.Cooperative as CoOperativeLoan, dbo.EmpMonthlyDeductions.PayDetailSlip as PaySlip,  dbo.EmpMonthlyDeductions.Welfare as Welfare, dbo.EmpMonthlyDeductions.Insuarance as Insurance, dbo.EmpMonthlyDeductions.penalty as Penalty, dbo.EmpMonthlyDeductions.Dhoby as Dhoby,  dbo.EmpMonthlyDeductions.Barber as Barber, dbo.EmpMonthlyDeductions.BankLoan as BankLoan, 0 AS Funeral, 0 AS NameCard, dbo.EmpMonthlyEarnings.EPFPaybleAmount as EPFPay,  dbo.EmpMonthlyEarnings.EPF10 as EPF10, dbo.EmployeeMaster.UnionNameCode as UnionCode, dbo.EmpMonthlyDeductions.UnionSubscription as UnionPayment, dbo.EmpMonthlyFinalWeges.NetWages as EmpSalary,  dbo.EmpMonthlyFinalWeges.TotalDeductions as GrandTotalDeduction, dbo.EmpMonthlyEarnings.SundryNamePay as SundryNamesPay, dbo.EmpMonthlyEarnings.PluckingNamePay as PluckingNamesPay,  dbo.EmpMonthlyEarnings.EPF10 + dbo.EmpMonthlyEarnings.EPF12 AS EPF22, dbo.EmpMonthlyEarnings.EPF12 as EPF12,  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.ETF3 AS EPF15, dbo.EmpMonthlyEarnings.PreviousMadeUpCoins as BFAmount,  dbo.EmpMonthlyDeductions.PreviousDebits as PreviouDebts, dbo.EmpMonthlyEarnings.PRIAmount AS PSSIncentivesPLK, 0 AS PSSIncentivesSundry, 0 AS BankRecovery, dbo.EmpMonthlyFinalWeges.DebitsBF as CurrentMonthDebts,  dbo.EmpMonthlyFinalWeges.MadeUpBalance as CFAmount,dbo.EmpMonthlyFinalWeges.WagePay as PaymentDue, dbo.EmpMonthlyEarnings.AttIncentive as IncentiveWithoutExRate, dbo.EmpMonthlyEarnings.OfferedDays as OfferedDays,  dbo.EmpMonthlyEarnings.WorkedPercentage as WorkedPerc,0 as EPFRemitence,0 as NormalDays,0 as NormalDaysAmount, 0 as Holidays, 0 as HolidaysAmount FROM         dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmpMonthlyEarnings.Category = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EmpMonthlyDeductions ON dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyDeductions.EmpNo AND  dbo.EmpMonthlyEarnings.DivisionId = dbo.EmpMonthlyDeductions.DivisionId AND dbo.EmpMonthlyEarnings.EmpNO = dbo.EmpMonthlyDeductions.EmpNo AND  dbo.EmpMonthlyEarnings.Year = dbo.EmpMonthlyDeductions.Year AND dbo.EmpMonthlyEarnings.Month = dbo.EmpMonthlyDeductions.Month INNER JOIN dbo.EmpMonthlyFinalWeges ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmpMonthlyFinalWeges.DivisionId AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmpMonthlyFinalWeges.EmpNo AND dbo.EmpMonthlyEarnings.Year = dbo.EmpMonthlyFinalWeges.WageYear AND  dbo.EmpMonthlyEarnings.Month = dbo.EmpMonthlyFinalWeges.WageMonth CROSS JOIN dbo.Estate WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') ", CommandType.Text);
            return ds;
        }

    }
}
