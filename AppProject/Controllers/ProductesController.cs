using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppProject.Models;
using AppProject.ViewModel;
using Microsoft.AspNetCore.Http;

namespace AppProject.Controllers
{
    public class ProductesController : Controller
    {
        private readonly AppProjectContext _context;

        public ProductesController(AppProjectContext context)
        {
            _context = context;
        }

        // GET: Productes
        public async Task<IActionResult> Index(int id)
        {
            var databaseContext = _context.Productes.Include(p => p.SubCategory);


            if (id != 0)
                return PartialView(await databaseContext.Where(s => s.SubCategory.Id == id).ToListAsync());

            //בשביל הגרף
            var q = from u in databaseContext
                    //where u.SubCategory.Id==id
                    select u.SubCategory.Id;

            ViewBag.data = "[" + string.Join(",", q.Distinct().ToList()) + "]";

           return View(await databaseContext.ToListAsync());


        }

        public async Task<IActionResult> List()
        {

            return View(await _context.Productes.Include(p=>p.SubCategory).ToListAsync());

        }


        //חיפוש לפי שם מוצר
        public async Task<IActionResult> Search(string name)
        {
            if (name != null)
                return Json(await _context.Productes.Where(s => s.ProductName.Contains(name)).ToListAsync());
            return Json(await _context.Productes.ToListAsync());
        }

        //חיפוש לפי טווח של מחיר מוצר
        public async Task<IActionResult> SearchByPrice(double? minp, double? maxp)
        {
            var products = _context.Productes.Where(p => p.Price >= minp && p.Price <= maxp).ToListAsync();
            return new JsonResult(await products);

        }

        public async Task<IActionResult> GroupByPrice()
        {
          //group by
            var model = await _context.Productes
                .GroupBy(o => new
                {
                    ImgId=o.ImgId,
                    ProductName=o.ProductName,
                    Price = o.Price
                })
                .Select(g => new Productes
                {
                    ImgId=g.Key.ImgId,
                    ProductName=g.Key.ProductName,
                    Price = g.Key.Price
                })

                .ToListAsync();

            return View(model);

        }

        // GET: Productes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Productes.Include(m => m.Details)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var Colors = from color in _context.Colors
                         select color.ColorName;


            var sizes = from size in _context.Sizes
                        select size.SizeName;

            var Amount = from a in _context.Productes
                         where a.Id==id && a.AmountInStock!=0
                         select a.AmountInStock;

            //כאשר  מוצר נמצא במלאי
            if (Amount.ToList().Count>0)            
                ViewBag.AmountInStock = true;
              else
                ViewBag.AmountInStock = false;
            

            ViewData["ColorId"] = new SelectList(Colors);
            ViewData["SizeId"] = new SelectList(sizes);

            return View(product);
        }


        // GET: Productes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SubId"] = new SelectList(await _context.SubCategory.ToListAsync(), "Id", "SubName");
            return View();
        }

 
        // POST: Productes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Price,AmountInStock,AmountOfOrders,DeliveryPrice,ImgId,SubCategoryId")] Productes productes)
        {

  
          if (ModelState.IsValid)
            { 
              _context.Add(productes);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            ViewData["SubId"] = new SelectList(await _context.SubCategory.ToListAsync(), "Id", "SubName", productes.SubCategory.SubName);
            return View(productes);
        }

        [HttpPost]
        public IActionResult TestFunction(Productes productes)
        {
         
            return View();

        }

        // GET: Productes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productes = await _context.Productes.Include(m=>m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
            if (productes == null)
            {
                return NotFound();
            }


            ViewData["SubIdE"] = new SelectList(_context.SubCategory.Where(c => c.Id == productes.SubCategoryId), "Id", "SubName");
            return View(productes);
        }

        // POST: Productes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Price,AmountInStock,AmountOfOrders,DeliveryPrice,ImgId,SubCategoryId")] Productes productes)
        {
            if (id != productes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductesExists(productes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("List");
            }
            ViewData["SubIdE"] = new SelectList(await _context.SubCategory.ToListAsync(), "Id", "Name", productes.SubCategory.SubName);
            return View(productes);
        }

        // GET: Productes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productes = await _context.Productes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productes == null)
            {
                return NotFound();
            }

            return View(productes);
        }

        // POST: Productes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productes = await _context.Productes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Productes.Remove(productes);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        private bool ProductesExists(int id)
        {
            return _context.Productes.Any(e => e.Id == id);
        }
    }
}
