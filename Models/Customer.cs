using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Web_P04_5.Models;

namespace WEB_P04_5.Models
{
    public class Customer
    {
        [StringLength(9)]
        public string MemberID { get; set; }

        [StringLength(255)]
        public string MName { get; set; }

        public char MGender { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "MM/dd/yyyy")]
        public DateTime MBirthDate { get; set; }

        [StringLength(250)]
        public string? MAddress { get; set; }

        [StringLength(50)]
        public string MCountry { get; set; }

        [StringLength(20)]
        [ValidatePhoneNumberExists]
        // [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        [Display(Name = "Telephone Number")]
        [Phone]
        public string? MTelNo { get; set; }


        [StringLength(50)]
        [Display(Name = "Email Address")]
        [EmailAddress]
        [ValidateEmailExists]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string? MEmailAddr { get; set; }



        [StringLength(20)]
        public string MPassword { get; set; }
    }
}

