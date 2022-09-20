using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eviTest.Models.ViewModels
{
    public class CustomerRetriveVM
    {
        public int CustomerId { get; set; }
        [Required, StringLength(50), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, StringLength(150), Display(Name = "Address")]
        public string Address { get; set; }
        [StringLength(5), Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Date Of Birth"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime DateOfBirth { get; set; }
        [Required, Display(Name = "Marital Status")]
        public bool IsMarried { get; set; }
        [Display(Name = "Country")]
        public string  CountryName { get; set; }
        public string Image { get; set; }

    }
}
