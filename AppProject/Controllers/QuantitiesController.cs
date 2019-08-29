using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppProject.Models;
using Microsoft.AspNetCore.Http;

namespace AppProject.Controllers
{
    public class QuantitiesController : Controller
    {
        private readonly AppProjectContext _context;

        public QuantitiesController(AppProjectContext context)
        {
            _context = context;    
        }

        // GET: Quantities
        public async Task<IActionResult> Index()
        {
            var appProjectContext = _context.Quantities.Include(q => q.Color).Include(q => q.Mart).Include(q => q.Productes).Include(q => q.Size).Where(p => p.Mart.Customer.Mail == HttpContext.Session.GetString("Mail"));

            ViewBag.Mail = HttpContext.Session.GetString("Mail");

            if (ViewBag.Mail == null)
                ViewBag.ConnectClient = false;
            else
                ViewBag.ConnectClient = true;

            ViewBag.total = appProjectContext.Sum(p => p.Productes.Price * p.AmountOfOrders);


            return View(await appProjectContext.ToListAsync());
        }

        // GET: Quantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantities = await _context.Quantities
                .Include(q => q.Color)
                .Include(q => q.Mart)
                .Include(q => q.Productes)
                .Include(q => q.Size)
                .SingleOrDefaultAsync(m => m.ProductesId == id);
            if (quantities == null)
            {
                return NotFound();
            }

            return View(quantities);
        }

        // GET: Quantities/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id");
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id");
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id");
            return View();
        }

        // POST: Quantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductesId,SizeId,ColorId,MartId,AmountOfOrders")] Quantities quantities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quantities);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", quantities.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", quantities.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", quantities.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", quantities.SizeId);
            return View(quantities);
        }

        //הוספת מוצר לעגלה
        public IActionResult Add(int? id)
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName");

            ViewBag.ProductId = _context.Productes.Where(p => p.Id == id);

            ViewBag.MartId = (from u in _context.Customer
                              where u.Mail == HttpContext.Session.GetString("Mail")
                              select u.Id);
            return View();
        }

        // POST: ConnectTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("ProductesId,SizeId,ColorId,MartId,AmountOfOrders")] Quantities quantities)
        {
            if (quantities.MartId != 0)
            {
                _context.Add(quantities);
                await _context.SaveChangesAsync();

                if (!ConnectTableExist(quantities.ProductesId, quantities.SizeId, quantities.ColorId))
                {
                    //כאשר אופציה זו לא נמצאת במלאי(בטבלה המקשרת 
                    quantities.AmountOfOrders = 0;
                    _context.Update(quantities);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                if (ConnectTableExist(quantities.ProductesId, quantities.SizeId, quantities.ColorId))
                {
                    return RedirectToAction("Details", "ConnectTables", quantities);
                }
            }

            return RedirectToAction("LogIn", "Customers");

        }


        // GET: Quantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantities = await _context.Quantities.SingleOrDefaultAsync(m => m.ProductesId == id);
            if (quantities == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", quantities.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", quantities.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", quantities.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", quantities.SizeId);
            return View(quantities);
        }

        // POST: Quantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductesId,SizeId,ColorId,MartId,AmountOfOrders")] Quantities quantities)
        {
            if (id != quantities.ProductesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quantities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuantitiesExists(quantities.ProductesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", quantities.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", quantities.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", quantities.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", quantities.SizeId);
            return View(quantities);
        }

        // GET: Quantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantities = await _context.Quantities
                .Include(q => q.Color)
                .Include(q => q.Mart)
                .Include(q => q.Productes)
                .Include(q => q.Size)
                .SingleOrDefaultAsync(m => m.ProductesId == id);
            if (quantities == null)
            {
                return NotFound();
            }

            return View(quantities);
        }

        //מחיקת מוצר מהעגלה
        // POST: Quantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Productid,int ColorId,int SizeId,int MartId)
        {
            var quantities = await _context.Quantities.SingleOrDefaultAsync(m => m.ProductesId == Productid && m.SizeId==SizeId && m.ColorId==ColorId && m.MartId== MartId);
            _context.Quantities.Remove(quantities);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool QuantitiesExists(int id)
        {
            return _context.Quantities.Any(e => e.ProductesId == id);
        }

        private bool ConnectTableExist(int id, int sizeid, int colorid)
        {
            return _context.ConnectTable.Any(e => e.ProductesId == id && e.SizeId == sizeid && e.ColorId == colorid);
        }
    }
}
