using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eviTest.Models.ViewModels
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        [Required, StringLength(50), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, StringLength(150), Display(Name = "Address")]
        public string Address { get; set; }
        [StringLength(5), Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required, Display(Name = "Marital Status")]
        public bool IsMarried { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public IFormFile Image { get; set; }
        public string ImageFile { get; set; }

    }
}
