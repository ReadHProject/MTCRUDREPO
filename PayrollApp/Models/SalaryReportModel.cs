using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayrollApp.Models
{
    public class SalaryReportModel
    {
        public int EMPID { get; set; }
        public string EMPNAME { get; set; }
        public string EMPDESIGNATION { get; set; }
        public string MOBILE { get; set; }
        public decimal BASICSALARY { get; set; }
        public decimal HRA { get; set; }
        public decimal DA { get; set; }
        public decimal DEDUCTION { get; set; }
        public decimal GROSSSALARY { get; set; }
        public decimal NETSALARY { get; set; }
    }

    public class SalaryReportFilterModel
    {
        public int? SelectedEmployeeId { get; set; }
        public bool ShowAllEmployees { get; set; }
        public List<SalaryReportModel> EmployeeSalaries { get; set; }

        public SalaryReportFilterModel()
        {
            EmployeeSalaries = new List<SalaryReportModel>();
        }
    }
}