using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVCAppSandbox.ViewModels;

namespace WebMVCAppSandbox.Filters
{
    public class HeaderFooterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var v = filterContext.Result as ViewResult;
            if (v != null)
            {
                BaseViewModel bvm = v.Model as BaseViewModel;
                if (bvm != null)
                {
                    bvm.UserName = HttpContext.Current.User.Identity.Name;
                    bvm.FooterData = new FooterViewModel();
                    bvm.FooterData.CompanyName = "StepByStepSchools";
                    bvm.FooterData.Year = DateTime.Now.Year.ToString();
                }
            }
        }
    }
}