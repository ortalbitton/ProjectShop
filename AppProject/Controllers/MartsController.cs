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
    public class MartsController : Controller
    {
        private readonly AppProjectContext _context;

        public MartsController(AppProjectContext context)
        {
            _context = context;    
        }

        // GET: Marts
        public async Task<IActionResult> Index()
        {
            var appProjectContext = _context.Mart.Include(m => m.Customer);
            return View(await appProjectContext.ToListAsync());
        }

        // GET: Marts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mart = await _context.Mart
                .Include(m => m.Customer)
                .SingleOrDefaultAsync(m => m.MartId == id);
            if (mart == null)
            {
                return NotFound();
            }

            return View(mart);
        }

        // GET: Marts/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId");
            return View();
        }

        // POST: Marts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MartId,CustomerId")] Mart mart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mart);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", mart.CustomerId);
            return View(mart);
        }

        // GET: Marts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mart = await _context.Mart.SingleOrDefaultAsync(m => m.MartId == id);
            if (mart == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", mart.CustomerId);
            return View(mart);
        }

        // POST: Marts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MartId,CustomerId")] Mart mart)
        {
            if (id != mart.MartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MartExists(mart.MartId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", mart.CustomerId);
            return View(mart);
        }

        // GET: Marts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mart = await _context.Mart
                .Include(m => m.Customer)
                .SingleOrDefaultAsync(m => m.MartId == id);
            if (mart == null)
            {
                return NotFound();
            }

            return View(mart);
        }

        // POST: Marts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mart = await _context.Mart.SingleOrDefaultAsync(m => m.MartId == id);
            _context.Mart.Remove(mart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MartExists(int id)
        {
            return _context.Mart.Any(e => e.MartId == id);
        }
    }
}
