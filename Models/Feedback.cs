using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_P04_5.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }

        [StringLength(9)]
        public string MemberID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MMMM/yyyy}")]
        public DateTime DateTimePosted { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public string? Text { get; set; }

        [StringLength(255)]

        public string? ImageFileName { get; set; }


    }
}

