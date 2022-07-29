using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WEB_P04_5.Models
{
    public class FeedbackViewModel
    {
        [Display(Name = "ID")]
        public int FeedbackID { get; set; }

        [StringLength(9)]
        public string MemberID { get; set; }

        [Display(Name = "Date Time Posted")]
        [DataType(DataType.Date)]
        public DateTime DateTimePosted { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public string? Text { get; set; }

        [StringLength(255)]

        public string? ImageFileName { get; set; }

    }
}
