using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayrollApp.Models
{
    public class EmployeeMasterModel
    {
        public int EMPID { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(100)]
        public string EMPNAME { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string EMPDESIGNATION { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter valid 10 digit mobile")]
        public string  MOBILE { get; set; }
    }
}