using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eviTest.Models;
using eviTest.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.Server;

namespace eviTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerDbContext db;
        private readonly IWebHostEnvironment _hostEnv;
        public HomeController(CustomerDbContext db, IWebHostEnvironment hostEnv)
        {
            this.db = db;
            _hostEnv = hostEnv;
        }
        public IActionResult Index()
        {
            var customerInformation = (from c in db.Customers
                                       join cp in db.CustomerPhotos on c.CustomerId equals cp.CustomerId
                                       join cnt in db.Countries on c.CountryId equals cnt.CountryId
                                       select new CustomerRetriveVM
                                       {
                                           CustomerId = c.CustomerId,
                                           CustomerName = c.CustomerName,
                                           Address = c.Address,
                                           PostCode = c.PostCode,
                                           DateOfBirth = c.DateOfBirth,
                                           IsMarried = c.IsMarried,
                                           CountryName = cnt.CountryName,
                                           Image = cp.Image
                                       }).ToList();
            return View(customerInformation);
        }
        public IActionResult Create()
        {

            ViewBag.countries = db.Countries.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CustomerVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Image != null)
                {
                    string newFileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                    string newFilePath = Path.Combine("Images", newFileName);
                    string file = Path.Combine(_hostEnv.WebRootPath,newFilePath);
                    vm.Image.CopyTo(new FileStream(file, FileMode.Create));

                    Customer customer = new Customer
                    {
                        CustomerName = vm.CustomerName,
                        Address = vm.Address,
                        PostCode = vm.PostCode,
                        DateOfBirth = vm.DateOfBirth,
                        IsMarried = vm.IsMarried,
                        CountryId = vm.CountryId
                    };
                    CustomerPhoto cus = new CustomerPhoto
                    {
                        Customer = customer,
                        Image = newFileName
                    };

                    db.CustomerPhotos.Add(cus);
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            else
            {
                return PartialView("_error");
            }

            ViewBag.countries = db.Countries.ToList();
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var cus = db.Customers.FirstOrDefault(x => x.CustomerId == id);
                var cp = db.CustomerPhotos.FirstOrDefault(x => x.CustomerId == id);
                var cnt = db.Countries.FirstOrDefault(x => x.CountryId == cus.CountryId);
                CustomerVM vm = new CustomerVM
                {
                    CustomerId = cus.CustomerId,
                    CustomerName = cus.CustomerName,
                    Address = cus.Address,
                    PostCode = cus.PostCode,
                    DateOfBirth = cus.DateOfBirth,
                    IsMarried = cus.IsMarried,
                    CountryId = cnt.CountryId,
                    ImageFile = cp.Image
                };
                ViewBag.countries = db.Countries.ToList();
                return View(vm);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(CustomerVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Image != null)
                {
                    string newFileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                    string newFilePath = Path.Combine("Images", newFileName);
                    string file = Path.Combine(_hostEnv.WebRootPath, newFilePath);
                    vm.Image.CopyTo(new FileStream(file, FileMode.Create));

                    Customer customer = new Customer
                    {
                        CustomerId=vm.CustomerId,
                        CustomerName = vm.CustomerName,
                        Address = vm.Address,
                        PostCode = vm.PostCode,
                        DateOfBirth = vm.DateOfBirth,
                        IsMarried = vm.IsMarried,
                        CountryId = vm.CountryId
                    };
                    CustomerPhoto cus = new CustomerPhoto
                    {
                        CustomerId=vm.CustomerId,
                        Customer = customer,
                        Image = newFileName
                    };
                    db.Entry(customer).State = EntityState.Modified;
                    db.Entry(cus).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_success");
                }
                else
                {
                    Customer customer = new Customer
                    {
                        CustomerId = vm.CustomerId,
                        CustomerName = vm.CustomerName,
                        Address = vm.Address,
                        PostCode = vm.PostCode,
                        DateOfBirth = vm.DateOfBirth,
                        IsMarried = vm.IsMarried,
                        CountryId = vm.CountryId
                    };
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            return View();
        }
        public IActionResult Delete(string newFilePath)
        {
            newFilePath = Path.Combine(_hostEnv.WebRootPath, "Images", newFilePath);
            FileInfo fi = new FileInfo(newFilePath);
            if (fi != null)
            {
                System.IO.File.Delete(newFilePath);
                fi.Delete();
            }
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            
            if (id != null)
            {
                var cus = db.Customers.FirstOrDefault(x => x.CustomerId == id);
                var ca = db.Countries.FirstOrDefault(x => x.CountryId == id);
                var cp = db.CustomerPhotos.FirstOrDefault(x => x.CustomerId == id);

                db.CustomerPhotos.Remove(cp);
               // db.Countries.Remove(ca);
                db.Customers.Remove(cus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
