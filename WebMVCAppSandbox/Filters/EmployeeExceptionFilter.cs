using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVCAppSandbox.Logger;

namespace WebMVCAppSandbox.Filters
{
    public class EmployeeExceptionFilter  : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var fl = new FileLogger();
            fl.LogException(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}