using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayrollApp.Models
{
    public class EmployeeDetailsModel
    {
        [Required(ErrorMessage = "Employee is required")]
        public int EMPID { get; set; }

        [Required(ErrorMessage = "Basic Salary is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Basic salary must be a positive value")]
        public decimal BASICSALARY { get; set; }

        [Required(ErrorMessage = "HRA is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "HRA must be a positive value")]
        public decimal HRA { get; set; }

        [Required(ErrorMessage = "DA is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "DA must be a positive value")]
        public decimal DA { get; set; }

        [Required(ErrorMessage = "Deduction is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Deduction must be a positive value")]
        public decimal DEDUCTION { get; set; }
    }
}