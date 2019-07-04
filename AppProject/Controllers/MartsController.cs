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
        public List<string> Imgs;

        public MartsController(AppProjectContext context)
        {
            _context = context;   
          
        }

        // GET: Marts
        public async Task<IActionResult> Index()
        {

            var appProjectContext = _context.Mart.Include(m => m.Customer).Include(p => p.Details).ThenInclude(ps => ps.Productes);   
                                  
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
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mart == null)
            {
                return NotFound();
            }

            return View(mart);
        }

        // GET: Marts/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Customer, "Id", "Id");
            return View();
        }

        // POST: Marts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Mart mart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mart);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Id"] = new SelectList(_context.Customer, "Id", "Id", mart.Id);
            return View(mart);
        }

        // GET: Marts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mart = await _context.Mart.SingleOrDefaultAsync(m => m.Id == id);
            if (mart == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Customer, "Id", "Id", mart.Id);
            return View(mart);
        }

        // POST: Marts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Mart mart)
        {
            if (id != mart.Id)
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
                    if (!MartExists(mart.Id))
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
            ViewData["Id"] = new SelectList(_context.Customer, "Id", "Id", mart.Id);
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
                .SingleOrDefaultAsync(m => m.Id == id);
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
            var mart = await _context.Mart.SingleOrDefaultAsync(m => m.Id == id);
            _context.Mart.Remove(mart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MartExists(int id)
        {
            return _context.Mart.Any(e => e.Id == id);
        }
    }
}
