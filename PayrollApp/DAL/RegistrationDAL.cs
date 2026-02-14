using PayrollApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PayrollApp.DAL
{
    public class RegistrationDAL
    {
        public static int InsertEmployeeMaster(EmployeeMasterModel Model)
        {
            SqlParameter[] sqlparam = new SqlParameter[3];
            sqlparam[0] = new SqlParameter("@EmpName", Model.EMPNAME);
            sqlparam[1] = new SqlParameter("@EmpDesgnation", Model.EMPDESIGNATION);
            sqlparam[2] = new SqlParameter("@EmpMobile", Model.MOBILE);
            return DBHelper.ExecuteNonQuery("SP_INSERTEMPLOYEE", sqlparam);
        }

        public static DataTable GetEmployeeNames()
        {
            SqlParameter[] sqlparam = new SqlParameter[0];
            return DBHelper.GetDataTable("SP_SELECTEMPLOYEE", sqlparam);
        }

        public static int InsertEmployeeDetails(EmployeeDetailsModel Model)
        {
            SqlParameter[] sqlparam = new SqlParameter[5];
            sqlparam[0] = new SqlParameter("@EmpId", Model.EMPID);
            sqlparam[1] = new SqlParameter("@BasicSalary", Model.BASICSALARY);
            sqlparam[2] = new SqlParameter("@HRA", Model.HRA);
            sqlparam[3] = new SqlParameter("@DA", Model.DA);
            sqlparam[4] = new SqlParameter("@Deduction", Model.DEDUCTION);
            return DBHelper.ExecuteNonQuery("SP_INSERTEMPLOYEEDETAILS", sqlparam);
        }

        public static DataTable GetEmployeeSalaryReport(int? empId = null)
        {
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@EmpId", empId.HasValue ? (object)empId.Value : DBNull.Value);
            return DBHelper.GetDataTable("SP_GETEMPLOYEESALARYREPORT", sqlparam);
        }
    }
}