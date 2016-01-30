using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebMVCAppSandbox.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [FirstNameValidation]
        public string FirstName { get; set; }
        
        [StringLength(5, ErrorMessage="Last Name should not be greater than 5")]
        public string LastName { get; set; }
        
        [Range(50000, 200000)]
        public int? Salary { get; set; }
    }

    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please provide First Name");
            }
            else
            {
                if (value.ToString().Contains("@"))
                {
                    return new ValidationResult("First Name should not contain @ symbol");
                }
            }

            return ValidationResult.Success;
        }
    }
}