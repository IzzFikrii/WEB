using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Web_P04_5.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [StringLength(255)]
        public string ProductTitle { get; set; }

        [StringLength(255)]
        public string? ProductImage { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd MMMM yyyy}")]
        public DateTime EffectiveDate { get; set; }

        public char Obsolete { get; set; }
    }
}
