using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Web_P04_5.DAL;
using WEB_P04_5.Models;
using WEB_P04_5.DAL;

namespace Web_P04_5.Models
{

    public class ValidateEmailExists : ValidationAttribute
    {
        
        private CustomerDAL CustomerContext = new CustomerDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            // Get the email value to validate
            string MEmailAddr = Convert.ToString(value);
            // Casting the validation context to the "Staff" model class
            Customer customer = (Customer)validationContext.ObjectInstance;
            // Get the Staff Id from the staff instance
           string memberID = customer.MemberID;
            if (CustomerContext.IsEmailExist(MEmailAddr, memberID))
                // validation failed
                return new ValidationResult
                ("Email address already exists!");
            else
                // validation passed
                return ValidationResult.Success;
        }
        
    }
}