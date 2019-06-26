using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccess;

namespace FTSPayRollBL
{
    public class ListingDetails
    {

        private String strStatus;

        public String StrStatus
        {
            get { return strStatus; }
            set { strStatus = value; }
        }
        public DataSet getEstatesFields()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT  EstateField.FieldID, EstateField.FieldName, EstateField.DivisionID, EstateField.EstateID, EstateField.Extent, EstateField.FieldType, CHKCompany.CompanyName FROM EstateField CROSS JOIN CHKCompany", CommandType.Text);
            return ds;
        }

        public DataSet getEstatesDivisions()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.CHKCompany.CompanyName, dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EstateField.FieldID, dbo.EstateField.Remarks, dbo.EstateField.Extent,  ISNULL(dbo.EstateField.MapField, '-') AS MapField, dbo.EstateField.CropType, dbo.EstateField.Type FROM dbo.EstateDivision INNER JOIN dbo.EstateField ON dbo.EstateDivision.DivisionID = dbo.EstateField.DivisionID CROSS JOIN dbo.CHKCompany GROUP BY dbo.CHKCompany.CompanyName, dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EstateField.FieldID, dbo.EstateField.Remarks, dbo.EstateField.Extent, dbo.EstateField.MapField, dbo.EstateField.CropType, dbo.EstateField.Type", CommandType.Text);
            return ds;
        }

        public DataSet getEstatesDetails()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT  dbo.Estate.EstateID, dbo.Estate.EstateName, dbo.Estate.RegNo, dbo.Estate.Norm, dbo.CHKCompany.CompanyName FROM  dbo.Estate CROSS JOIN  dbo.CHKCompany", CommandType.Text);
            return ds;
        }

        public DataSet getEmployeeCategory()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT EmployeeCategory.CategoryID, EmployeeCategory.CategoryName, EmployeeCategory.CreateDateTime, EmployeeCategory.UserId, CHKCompany.CompanyName FROM  EmployeeCategory CROSS JOIN CHKCompany", CommandType.Text);
            return ds;
        }

        public DataSet getOvertimeParameters()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT CHKOTParameters.OtSettingId, CHKOTParameters.OTType, CHKOTParameters.OTFactor, CHKOTParameters.CreateDateTime, CHKOTParameters.UserId, CHKCompany.CompanyName FROM CHKOTParameters CROSS JOIN CHKCompany", CommandType.Text);
            return ds;
        }

        public DataSet getFixedParameters()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT     FTSCheckRollRates.Id_Auto, FTSCheckRollRates.Type, FTSCheckRollRates.Name, FTSCheckRollRates.Amount, FTSCheckRollRates.Description, FTSCheckRollRates.Category, FTSCheckRollRates.EmpType, FTSCheckRollRates.CreateDateTime, FTSCheckRollRates.UserId, CHKCompany.CompanyName FROM FTSCheckRollRates CROSS JOIN CHKCompany", CommandType.Text);
            return ds;
        }

        public DataSet getJob()
        {
            //DataSet ds = SQLHelper.FillDataSet("SELECT  JobGroup.GroupName, JobMaster.JobName, JobMaster.JobShortName, JobMaster.JobType, JobMaster.ExpenditureType, CHKCompany.CompanyName FROM JobGroup INNER JOIN JobMaster ON JobGroup.GroupID = JobMaster.JobGroup CROSS JOIN  CHKCompany", CommandType.Text);

            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.JobGroup.GroupName, dbo.JobMaster.JobName, dbo.JobMaster.JobShortName, dbo.JobMaster.JobType, dbo.JobMaster.ExpenditureType, dbo.CHKCompany.CompanyName, dbo.JobWithCropType.CropType,  dbo.JobGroupExpenditureType.ExpenditureType AS JobGroupExType FROM dbo.JobGroup INNER JOIN dbo.JobMaster ON dbo.JobGroup.GroupID = dbo.JobMaster.JobGroup INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobGroup.GroupID = dbo.JobGroupExpenditureType.JobGroupCode CROSS JOIN dbo.CHKCompany", CommandType.Text);
            return ds;
        }

        public DataSet getEmployeeGangs()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT CHKCompany.CompanyName, EmployeeGang.GangId, EmployeeGang.EstateId, EmployeeGang.DivisionId, EmployeeGang.gangName FROM  CHKCompany CROSS JOIN EmployeeGang", CommandType.Text);
            return ds;
        }

        public DataSet getEmployeeUnions(String Div,String Union)
        {
            //DataSet ds = SQLHelper.FillDataSet("SELECT CHKCompany.CompanyName, EmployeeUnions.UnionId, EmployeeUnions.UnionName, EmployeeUnions.UnionCode FROM  CHKCompany CROSS JOIN EmployeeUnions", CommandType.Text);
            DataSet ds = SQLHelper.FillDataSet("SELECT  dbo.EmployeeUnions.Deduction, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster RIGHT OUTER JOIN dbo.EmployeeUnions ON dbo.EmployeeMaster.UnionNameCode = dbo.EmployeeUnions.Deduction GROUP BY dbo.EmployeeUnions.Deduction, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName HAVING      (dbo.EmployeeUnions.Deduction LIKE '"+Union+"') AND (dbo.EmployeeMaster.DivisionID LIKE '"+Div+"')", CommandType.Text);            
            return ds;
        }
        public DataSet getEmployeeDetails(String strDiv, String strStatus)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.CHKCompany.CompanyName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeCategory.CategoryName, dbo.EmployeeMaster.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.DateJoined, dbo.JobMaster.JobName FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.JobMaster ON dbo.EmployeeMaster.BasicJob = dbo.JobMaster.JobID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.EmployeeStatus = '"+strStatus+"') AND (dbo.EmployeeMaster.DivisionID = '" + strDiv + "')", CommandType.Text);
            return ds;
        }
        public DataSet getEmployeeDetails(String strStatus)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.CHKCompany.CompanyName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeCategory.CategoryName, dbo.EmployeeMaster.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.DateJoined, dbo.JobMaster.JobName FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.JobMaster ON dbo.EmployeeMaster.BasicJob = dbo.JobMaster.JobID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.EmployeeStatus = '" + strStatus + "')", CommandType.Text);
            return ds;
        }
        public DataSet getEmployeeDetailsByDivision(String strDiv)
        {
            //DataSet ds = SQLHelper.FillDataSet("SELECT dbo.CHKCompany.CompanyName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeCategory.CategoryName, dbo.EmployeeMaster.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.DateJoined, dbo.JobMaster.JobName FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.JobMaster ON dbo.EmployeeMaster.BasicJob = dbo.JobMaster.JobID CROSS JOIN dbo.CHKCompany", CommandType.Text);
            DataSet ds = SQLHelper.FillDataSet("SELECT     dbo.CHKCompany.CompanyName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryName, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN  dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany where DivisionID='"+strDiv+"'", CommandType.Text);
            return ds;
        }
        public DataSet getLoanDetails()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT  CHKCompany.CompanyName, CHKLoan.LoanCode, CHKLoan.Bank, CHKLoan.DivisionCode, CHKLoan.CategoryCode, CHKLoan.EmployeeNo, CHKLoan.LoanName, CHKLoan.LoanDate, CHKLoan.Principalamount, CHKLoan.NoofInstallments, CHKLoan.InstallmentAmount, CHKLoan.ClosedYesNo FROM CHKCompany CROSS JOIN CHKLoan", CommandType.Text);
            return ds;
        }

        public DataSet getDeductionsDetails()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.CHKCompany.CompanyName, dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKDeduction.Priority, dbo.CHKDeduction.AccountCode, dbo.CHKDeductionGroup.GroupName FROM dbo.CHKDeductionGroup INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode CROSS JOIN dbo.CHKCompany", CommandType.Text);
            return ds;
        }

        public DataSet getFundsDetails()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT  CHKCompany.CompanyName, EmployeeFunds.FundId, EmployeeFunds.FundCode, EmployeeFunds.FundName FROM CHKCompany CROSS JOIN  EmployeeFunds", CommandType.Text);
            return ds;
        }
        public DataSet getEmployeeDetails()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.CHKCompany.CompanyName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeCategory.CategoryName, dbo.EmployeeMaster.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.UnionNameCode FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany", CommandType.Text);
            return ds;
        }
        public DataSet getEmployeeDetails(String strDiv,String strCat,String strGender)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.EmployeeCategory.CategoryName, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, ISNULL(dbo.EmployeeMaster.UnionNameCode, '-')  AS Expr1, ISNULL(dbo.EmployeeMaster.NICNo, '-') AS Expr2, dbo.EmployeeMaster.dateOfBirth,dbo.EmployeeMaster.EmpCategory, dbo.EmployeeMaster.EmployeeStatus,  dbo.EmployeeMaster.ResignedDate, dbo.EmployeeMaster.Contractor FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (CONVERT(varchar(50), dbo.EmployeeMaster.EmpCategory) LIKE '" + strCat + "') AND (dbo.EmployeeMaster.Gender LIKE '" + strGender + "') AND  (dbo.EmployeeMaster.DivisionID LIKE '" + strDiv + "') order by dbo.EmployeeMaster.EmpCategory", CommandType.Text);
            return ds;
        }

        public DataSet getEPFEmployeeDetails(String strDiv)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.OCGrade, dbo.EmployeeMaster.MemberStatus, dbo.EmployeeMaster.EmployerNo, dbo.CHKEPFEmployer.ZoneCode,  dbo.CHKEPFEmployer.PaymentRef, dbo.CHKEPFEmployer.DistrictOfficeCode, dbo.EmployeeMaster.NICNo FROM dbo.EmployeeMaster INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE (dbo.EmployeeMaster.DivisionID = '" + strDiv + "')", CommandType.Text);
            return ds;
        }

        public DataSet ListSundryTasks()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT        dbo.CHKTaskList.Job, dbo.CHKTaskList.Task, dbo.CHKTaskList.UnitCode, dbo.JobMaster.JobName FROM dbo.CHKTaskList INNER JOIN dbo.JobMaster ON dbo.CHKTaskList.Job = dbo.JobMaster.JobShortName", CommandType.Text);
            return ds;
        }

        public DataSet GetJobCropList()
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT        dbo.JobMaster.JobShortName as JobCode, dbo.JobMaster.JobName as Name, dbo.JobWithCropType.CropType, dbo.JobGroup.GroupName as JobGroupName  FROM            dbo.JobGroup INNER JOIN dbo.JobMaster ON dbo.JobGroup.GroupID = dbo.JobMaster.JobGroup INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode", CommandType.Text);
            return ds;
        }

        public DataSet viewAllFields()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT dbo.EstateField.DivisionID, dbo.EstateField.Type, dbo.EstateField.FieldID, dbo.EstateField.FieldName, dbo.EstateField.CropType, dbo.EstateField.FieldType, dbo.EstateField.CostCentreType, dbo.EstateField.Extent, dbo.EstateField.YOP, dbo.EstateField.Clone, dbo.EstateField.Tree, dbo.EstateField.Remarks, dbo.EstateField.MainAccountCode, dbo.EstateField.MainAccountName, dbo.EstateDivision.DivisionName, dbo.Estate.EstateName, dbo.EstateField.ExpenditureType FROM dbo.EstateField INNER JOIN dbo.EstateDivision ON dbo.EstateField.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.Estate", CommandType.Text);
            return ds;
        }

        public DataSet getFieldSettings()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT        dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.CHKFieldSettings.FieldID, dbo.EstateField.FieldName, dbo.CHKFieldSettings.Type,  dbo.CHKFieldSettings.Rate FROM dbo.CHKFieldSettings INNER JOIN dbo.EstateField ON dbo.CHKFieldSettings.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKFieldSettings.FieldID = dbo.EstateField.FieldID INNER JOIN dbo.EstateDivision ON dbo.CHKFieldSettings.DivisionID = dbo.EstateDivision.DivisionID", CommandType.Text);
            return ds;
        }



    }
}
