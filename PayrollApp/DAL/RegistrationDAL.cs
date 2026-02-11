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
    }
}