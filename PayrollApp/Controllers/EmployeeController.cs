using PayrollApp.DAL;
using PayrollApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayrollApp.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult EmployeeMaster()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeMaster(EmployeeMasterModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View(Model);
            }

            try
            {
                RegistrationDAL.InsertEmployeeMaster(Model);
                TempData["SuccessMessage"] = "Employee Detail added successfully!";
            }
            catch (SqlException ex)
            {
                // This catches the THROW from SQL Server
                ModelState.AddModelError("", "Database Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Catches general C# errors (like connection issues)
                ModelState.AddModelError("", "System Error: " + ex.Message);
            }
            return RedirectToAction("EmployeeMaster");
        }

        public ActionResult EmployeeNames()
        {
            // 1. Get data from DAL
            DataTable dt = RegistrationDAL.GetEmployeeNames();
            List<SelectListItem> items = new List<SelectListItem>();

            // 2. Loop through rows and populate the list
            foreach (DataRow row in dt.Rows)
            {
                items.Add(new SelectListItem
                {
                    Value = row["EMPID"].ToString(),
                    Text = row["EMPNAME"].ToString()
                });
            }

            // 3. Assign to ViewBag
            ViewBag.EmployeeNamesList = new SelectList(items, "Value", "Text");
            return View(new EmployeeDetailsModel());
        }

        public ActionResult EmployeeDetails()
        {
            EmployeeNames();
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeDetails(EmployeeDetailsModel Model)
        {
            if (!ModelState.IsValid)
            {
                EmployeeNames();
                return View(Model);
            }

            try
            {
                RegistrationDAL.InsertEmployeeDetails(Model);
                TempData["SuccessMessage"] = "Employee salary details added successfully!";
            }
            catch (SqlException ex)
            {
                ModelState.AddModelError("", "Database Error: " + ex.Message);
                EmployeeNames();
                return View(Model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "System Error: " + ex.Message);
                EmployeeNames();
                return View(Model);
            }
            return RedirectToAction("EmployeeDetails");
        }

        public ActionResult SalaryReport()
        {
            // Get employee names for dropdown
            DataTable dt = RegistrationDAL.GetEmployeeNames();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select Employee --" });

            foreach (DataRow row in dt.Rows)
            {
                items.Add(new SelectListItem
                {
                    Value = row["EMPID"].ToString(),
                    Text = row["EMPNAME"].ToString()
                });
            }

            ViewBag.EmployeeList = new SelectList(items, "Value", "Text");
            return View(new SalaryReportFilterModel());
        }

        [HttpPost]
        public ActionResult SalaryReport(SalaryReportFilterModel model)
        {
            // Get employee names for dropdown
            DataTable dt = RegistrationDAL.GetEmployeeNames();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select Employee --" });

            foreach (DataRow row in dt.Rows)
            {
                items.Add(new SelectListItem
                {
                    Value = row["EMPID"].ToString(),
                    Text = row["EMPNAME"].ToString()
                });
            }

            ViewBag.EmployeeList = new SelectList(items, "Value", "Text", model.SelectedEmployeeId);

            try
            {
                // Get salary data based on selection
                int? empId = model.ShowAllEmployees ? null : model.SelectedEmployeeId;
                DataTable salaryData = RegistrationDAL.GetEmployeeSalaryReport(empId);

                model.EmployeeSalaries = new List<SalaryReportModel>();
                foreach (DataRow row in salaryData.Rows)
                {
                    var salaryModel = new SalaryReportModel
                    {
                        EMPID = Convert.ToInt32(row["EMPID"]),
                        EMPNAME = row["EMPNAME"].ToString(),
                        EMPDESIGNATION = row["EMPDESGNATION"].ToString(),
                        MOBILE = row["MOBILE"].ToString(),
                        BASICSALARY = Convert.ToDecimal(row["BASICSALARY"]),
                        HRA = Convert.ToDecimal(row["HRA"]),
                        DA = Convert.ToDecimal(row["DA"]),
                        DEDUCTION = Convert.ToDecimal(row["DEDUCTION"])
                    };
                    
                    // Calculate gross and net salary
                    salaryModel.GROSSSALARY = salaryModel.BASICSALARY + salaryModel.HRA + salaryModel.DA;
                    salaryModel.NETSALARY = salaryModel.GROSSSALARY - salaryModel.DEDUCTION;
                    
                    model.EmployeeSalaries.Add(salaryModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error retrieving salary data: " + ex.Message);
            }

            return View(model);
        }

        public ActionResult SalaryReportPDF(int? empId, bool showAll = false)
        {
            try
            {
                int? employeeId = showAll ? null : empId;
                DataTable salaryData = RegistrationDAL.GetEmployeeSalaryReport(employeeId);

                var salaryList = new List<SalaryReportModel>();
                foreach (DataRow row in salaryData.Rows)
                {
                    var salaryModel = new SalaryReportModel
                    {
                        EMPID = Convert.ToInt32(row["EMPID"]),
                        EMPNAME = row["EMPNAME"].ToString(),
                        EMPDESIGNATION = row["EMPDESIGNATION"].ToString(),
                        MOBILE = row["MOBILE"].ToString(),
                        BASICSALARY = Convert.ToDecimal(row["BASICSALARY"]),
                        HRA = Convert.ToDecimal(row["HRA"]),
                        DA = Convert.ToDecimal(row["DA"]),
                        DEDUCTION = Convert.ToDecimal(row["DEDUCTION"])
                    };
                    
                    salaryModel.GROSSSALARY = salaryModel.BASICSALARY + salaryModel.HRA + salaryModel.DA;
                    salaryModel.NETSALARY = salaryModel.GROSSSALARY - salaryModel.DEDUCTION;
                    
                    salaryList.Add(salaryModel);
                }

                return View(salaryList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error generating PDF report: " + ex.Message;
                return RedirectToAction("SalaryReport");
            }
        }

    }
}