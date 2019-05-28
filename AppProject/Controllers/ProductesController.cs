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
            return View(await _context.Productes.ToListAsync());
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
                        join connect in product.Details
                        on color.Id equals connect.ColorId
                        select color.ColorName;

            var sizes = from size in _context.Sizes
                       join connect in product.Details
                       on size.Id equals connect.SizeId
                       select size.SizeName;


            ViewData["ColorId"] = new SelectList(Colors);
            ViewData["SizeId"] = new SelectList(sizes);

            return View(product);
        }

        // GET: Productes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Price,AmountInStock,AmountOfOrders,DeliveryPrice,ImgId")] Productes productes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productes);
        }

        // GET: Productes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productes = await _context.Productes.SingleOrDefaultAsync(m => m.Id == id);
            if (productes == null)
            {
                return NotFound();
            }
            return View(productes);
        }

        // POST: Productes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Price,AmountInStock,AmountOfOrders,DeliveryPrice,ImgId")] Productes productes)
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
                return RedirectToAction("Index");
            }
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
            return RedirectToAction("Index");
        }

        private bool ProductesExists(int id)
        {
            return _context.Productes.Any(e => e.Id == id);
        }
    }
}
