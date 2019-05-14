using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppProject.Models;

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
        public async Task<IActionResult> Index()
        {
            var appProjectContext = _context.Productes.Include(p => p.SubCategory);
            return View(await appProjectContext.ToListAsync());
        }


        // GET: Productes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productes = await _context.Productes
                .Include(p => p.SubCategory)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (productes == null)
            {
                return NotFound();
            }

            return View(productes);
        }

        // GET: Productes/Create
        public IActionResult Create()
        {
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "SubCategoryId", "SubCategoryId");
            return View();
        }

        // POST: Productes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,AmountInStock,AmountOfOrders,DeliveryPrice,ImgId,SubCategoryId")] Productes productes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "SubCategoryId", "SubCategoryId", productes.SubCategoryId);
            return View(productes);
        }

        // GET: Productes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productes = await _context.Productes.SingleOrDefaultAsync(m => m.ProductId == id);
            if (productes == null)
            {
                return NotFound();
            }
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "SubCategoryId", "SubCategoryId", productes.SubCategoryId);
            return View(productes);
        }

        // POST: Productes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Price,AmountInStock,AmountOfOrders,DeliveryPrice,ImgId,SubCategoryId")] Productes productes)
        {
            if (id != productes.ProductId)
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
                    if (!ProductesExists(productes.ProductId))
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
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "SubCategoryId", "SubCategoryId", productes.SubCategoryId);
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
                .Include(p => p.SubCategory)
                .SingleOrDefaultAsync(m => m.ProductId == id);
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
            var productes = await _context.Productes.SingleOrDefaultAsync(m => m.ProductId == id);
            _context.Productes.Remove(productes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductesExists(int id)
        {
            return _context.Productes.Any(e => e.ProductId == id);
        }
    }
}
