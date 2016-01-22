using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVCAppSandbox.Filters;
using WebMVCAppSandbox.Models;
using WebMVCAppSandbox.ViewModels;

namespace WebMVCAppSandbox.Controllers
{
    public class EmployeeController : Controller
    {
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            var empListViewModel = new EmployeeListViewModel();
            empListViewModel.Employees = new List<EmployeeViewModel>();
            var employees = new EmployeeBusinessLayer().GetEmployees();
            empListViewModel.UserName = User.Identity.Name;

            foreach (var emp in employees)
            {
                var empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.Value.ToString("C");
                if (emp.Salary >= 100000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }

                empListViewModel.Employees.Add(empViewModel);
            }

            return View("Index", empListViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            var vm = new CreateEmployeeViewModel();

            return View("CreateEmployee", vm);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save":
                    if (ModelState.IsValid)
                    {
                        var salesBL = new EmployeeBusinessLayer();
                        salesBL.SaveEmployee(e);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        if (e.Salary.HasValue)
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }

                        return View("CreateEmployee", vm);
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }

            return new EmptyResult();
        }

        [ChildActionOnly]
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}
