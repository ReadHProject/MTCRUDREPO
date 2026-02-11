using PayrollApp.DAL;
using PayrollApp.Models;
using System;
using System.Collections.Generic;
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

            int intVal = RegistrationDAL.InsertEmployeeMaster(Model);
            if (intVal <= 0)
            {
                return ViewBag.ErrorMessage = "Employee data insertion failed";
            }
            return RedirectToAction("EmployeeDetails");
        }
    }
}