using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppProjectContext _context;

        public HomeController(AppProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            ViewBag.Mail = HttpContext.Session.GetString("Mail");

            if (ViewBag.Mail == "Manager@gmail.com")
                ViewBag.ConnectManager = true;
            else
                ViewBag.ConnectManager = false;
            

            if (ViewBag.Mail == null)
                ViewBag.ConnectClient = false;
            else
            {
                ViewBag.ConnectClient = true;

                ViewBag.CustomerId = (from u in _context.Customer
                                  where u.Mail == HttpContext.Session.GetString("Mail")
                                  select u.FirstName);
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult API()
        {
   
            return View();
        }

        public IActionResult Graph()
        {
            //בשביל הגרף
            var q = from u in _context.Productes.Include(p => p.SubCategory)
                    select u.SubCategory.Id;

            ViewBag.data = "[" + string.Join(",", q.Distinct().ToList()) + "]";


            return View();
        }

        public IActionResult Graph1()
        {
            var q = from u in _context.Productes.Include(p => p.SubCategory).ThenInclude(c => c.Categories)
                    select u.SubCategory.Categories.Id;

            ViewBag.data = "[" + string.Join(",", q.Distinct().ToList()) + "]";


            return View();

        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
