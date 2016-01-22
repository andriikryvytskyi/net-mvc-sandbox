using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVCAppSandbox.ViewModels
{
    public class EmployeeListViewModel : BaseViewModel
    {
        /// <summary>
        /// List of emploees
        /// </summary>
        public List<EmployeeViewModel> Employees { get; set; }
    }
}