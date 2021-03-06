﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebMVCAppSandbox.Models;
using WebMVCAppSandbox.ViewModels.SPA;
using OldViewModel = WebMVCAppSandbox.ViewModels;
using WebMVCAppSandbox.Filters;

namespace WebMVCAppSandbox.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            var v = new MainViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "StepByStepSchools";
            v.FooterData.Year = DateTime.Now.Year.ToString();

            return View("Index", v);
        }

        public ActionResult EmployeeList()
        {
            var empListVM = new EmployeeListViewModel();
            var empBal = new EmployeeBusinessLayer();
            var employees = empBal.GetEmployees();

            var empsVM = new List<EmployeeViewModel>();
            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.Value.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empsVM.Add(empViewModel);
            }

            empListVM.Employees = empsVM;

            return View("EmployeeList", empListVM);
        }

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

        [AdminFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel v = new CreateEmployeeViewModel();
            return PartialView("CreateEmployee", v);
        }

        [AdminFilter]
        public ActionResult SaveEmployee(Employee emp)
        {
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            empBal.SaveEmployee(emp);

            EmployeeViewModel empViewModel = new EmployeeViewModel();
            empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
            empViewModel.Salary = emp.Salary.Value.ToString("C");
            if (emp.Salary > 15000)
            {
                empViewModel.SalaryColor = "yellow";
            }
            else
            {
                empViewModel.SalaryColor = "green";
            }

            return Json(empViewModel);
        }
    }
}