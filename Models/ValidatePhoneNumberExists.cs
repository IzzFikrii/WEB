using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Web_P04_5.DAL;
using WEB_P04_5.Models;
using WEB_P04_5.DAL;

namespace WEB_P04_5.Models

{
    public class ValidatePhoneNumberExists : ValidationAttribute
    {

        private CustomerDAL CustomerContext = new CustomerDAL();
        protected override ValidationResult IsValid(
        object number, ValidationContext validationContext)
        {
            // Get the Telephone Number value to validate
            string MTelNo = Convert.ToString(number);
            // Casting the validation context to the "Staff" model class
            Customer customer = (Customer)validationContext.ObjectInstance;
            // Get the Staff Id from the staff instance
            string memberID = customer.MemberID;
            if (CustomerContext.IsTelNoExist(MTelNo, memberID))
                // validation failed
                return new ValidationResult
                ("Telephone Number already exists!");
            else
                // validation passed
                return ValidationResult.Success;
        }

    }
}
