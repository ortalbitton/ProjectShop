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
    public class ConnectTablesController : Controller
    {
        private readonly AppProjectContext _context;

        public ConnectTablesController(AppProjectContext context)
        {
            _context = context;    
        }

        // GET: ConnectTables
        public async Task<IActionResult> Index(int? id)
        {

            var appProjectContext = _context.ConnectTable.Include(c => c.Color).Include(c => c.Mart).Include(c => c.Productes).Include(c => c.Size).Where(c=>c.Productes.Id==id);

            return View(await appProjectContext.ToListAsync());
        }

        // GET: ConnectTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectTable = await _context.ConnectTable
                .Include(c => c.Color)
                .Include(c => c.Mart)
                .Include(c => c.Productes)
                .Include(c => c.Size)
                .SingleOrDefaultAsync(m => m.ProductesId == id);
            if (connectTable == null)
            {
                return NotFound();
            }



            return View(connectTable);
        }

        // GET: ConnectTables/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id");
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id");
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id");
            return View();
        }

        // POST: ConnectTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductesId,SizeId,ColorId,MartId")] ConnectTable connectTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(connectTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", connectTable.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", connectTable.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", connectTable.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", connectTable.SizeId);
            return View(connectTable);
        }

        // GET: ConnectTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectTable = await _context.ConnectTable.SingleOrDefaultAsync(m => m.ProductesId == id);
            if (connectTable == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", connectTable.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", connectTable.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", connectTable.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", connectTable.SizeId);
            return View(connectTable);
        }

        // POST: ConnectTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductesId,SizeId,ColorId,MartId")] ConnectTable connectTable)
        {
            if (id != connectTable.ProductesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(connectTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConnectTableExists(connectTable.ProductesId))
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
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", connectTable.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", connectTable.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", connectTable.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", connectTable.SizeId);
            return View(connectTable);
        }

        // GET: ConnectTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectTable = await _context.ConnectTable
                .Include(c => c.Color)
                .Include(c => c.Mart)
                .Include(c => c.Productes)
                .Include(c => c.Size)
                .SingleOrDefaultAsync(m => m.ProductesId == id);
            if (connectTable == null)
            {
                return NotFound();
            }

            return View(connectTable);
        }

        // POST: ConnectTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var connectTable = await _context.ConnectTable.SingleOrDefaultAsync(m => m.ProductesId == id);
            _context.ConnectTable.Remove(connectTable);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ConnectTableExists(int id)
        {
            return _context.ConnectTable.Any(e => e.ProductesId == id);
        }
    }
}
