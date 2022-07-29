using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Web_P04_5.Models;

namespace WEB_P04_5.Models
{
    public class Response
    {
        public int ResponseID { get; set; }

        public int FeedbackID { get; set; }

        [StringLength(9)]
        public string? MemberID { get; set; }

        [StringLength(20)]
        public string StaffID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd MMMM yyyy}")]
        public DateTime DateTimePosted { get; set; }

        public string Text { get; set; }
    }
}
