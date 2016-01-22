using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMVCAppSandbox.DataAccessLayer;

namespace WebMVCAppSandbox.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            var salesDal = new SalesERPDAL();

            return salesDal.Employees.ToList();
        }

        public Employee SaveEmployee(Employee e)
        {
            var salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();

            return e;
        }

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "admin" && u.Password == "admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "andrii" && u.Password == "inetpass")
            {
                return UserStatus.AuthenticatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }
    }
}