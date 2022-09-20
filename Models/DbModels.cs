using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eviTest.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required,StringLength(50),Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, StringLength(150), Display(Name = "Address")]
        public string Address { get; set; }
        [StringLength(5), Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Date Of Birth"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required, Display(Name = "Marital Status")]
        public bool IsMarried { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        //nev
        public virtual Country Country { get; set; }
        public virtual CustomerPhoto CustomerPhoto { get; set; }
    }
    public class Country
    {
        public int CountryId { get; set; }
        [Required, StringLength(50), Display(Name = "Country Name")]
        public string CountryName { get; set; }

        //nev
        public virtual Customer Customer { get; set; }
    }
    public class CustomerPhoto
    {
        [Key,ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public string Image { get; set; }
        //nev
        public virtual Customer Customer { get; set; }
    }
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CustomerPhoto> CustomerPhotos { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }

}
