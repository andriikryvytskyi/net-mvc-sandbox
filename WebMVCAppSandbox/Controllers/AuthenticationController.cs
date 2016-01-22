using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMVCAppSandbox.Models;

namespace WebMVCAppSandbox.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        //HttpPost attribute is used to allow onnly post http request to method
        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
                var userStatus = bal.GetUserValidity(u);
                bool isAdmin = false;
                if (userStatus == UserStatus.AuthenticatedAdmin)
                {
                    isAdmin = true;
                }
                else if (userStatus == UserStatus.AuthenticatedUser)
                {
                    isAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }

                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = isAdmin;
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}