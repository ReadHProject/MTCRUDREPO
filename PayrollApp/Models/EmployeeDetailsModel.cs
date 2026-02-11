using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayrollApp.Models
{
    public class EmployeeDetailsModel
    {
        [Required]
        public int EMPID { get; set; }

        [Required(ErrorMessage = "Basic Salary is required")]
        [Range(1, 1000000)]
        public decimal BASICSALARY { get; set; }

        [Required(ErrorMessage = "Hra is required")]
        public decimal HRA { get; set; }

        [Required(ErrorMessage = "Da is required")]
        public decimal DA { get; set; }

        [Required(ErrorMessage = "Deduction is required")]
        public decimal DEDUCTION { get; set; }
    }
}