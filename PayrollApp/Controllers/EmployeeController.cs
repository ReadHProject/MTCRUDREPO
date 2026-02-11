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

    }
}