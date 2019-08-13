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
    public class ConnectTablesController : Controller
    {
        private readonly AppProjectContext _context;

        public ConnectTablesController(AppProjectContext context)
        {
            _context = context;    
        }

        // GET: ConnectTables
        public async Task<IActionResult> Index()
        {
            var appProjectContext = _context.ConnectTable.Include(c => c.Color).Include(c => c.Productes).Include(c => c.Size);
            
            return View(await appProjectContext.ToListAsync());
        }

        public async Task<IActionResult> List()
        {
            var appProjectContext = _context.ConnectTable.Include(c => c.Color).Include(c => c.Productes).Include(c => c.Size);

            return View(await appProjectContext.ToListAsync());
        }

        //עדכון כמות המלאי בעת שהלקוח מוסיפה אחת מהאופציות שבמלאי לכמויות שלו
        // GET: ConnectTables/Details/quantities
        public async Task<IActionResult> Details(Quantities quantities)
        {
            if (quantities.ProductesId == 0)
            {
                return NotFound();
            }

            var connectTable = await _context.ConnectTable
                .Include(c => c.Color)
                .Include(c => c.Productes)
                .Include(c => c.Size)
                .SingleOrDefaultAsync(m => m.ProductesId == quantities.ProductesId && m.ColorId==quantities.ColorId && m.SizeId==quantities.SizeId);      

            if (connectTable.AmountInStock < quantities.AmountOfOrders)
            {
                quantities.AmountOfOrders = 0;
                _context.Update(quantities);
                await _context.SaveChangesAsync();
            }
            else
            {
                connectTable.AmountInStock = connectTable.AmountInStock - quantities.AmountOfOrders;
                _context.Update(connectTable);
                await _context.SaveChangesAsync();
            }

            if (connectTable == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Quantities");
        }

        // GET: ConnectTables/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName");
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "ProductName");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName");
            return View();
        }

        // POST: ConnectTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductesId,SizeId,ColorId,AmountInStock")] ConnectTable connectTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(connectTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", connectTable.ColorId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", connectTable.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", connectTable.SizeId);
            return View(connectTable);
        }


       
        // GET: ConnectTables/Edit/5
        public async Task<IActionResult> Edit(int? ProductesId, int? ColorId, int? SizeId)
        {
            if (ProductesId == null)
            {
                return NotFound();
            }

            var connectTable = await _context.ConnectTable.SingleOrDefaultAsync(m => m.ProductesId == ProductesId && m.SizeId==SizeId && m.ColorId==ColorId);
            if (connectTable == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", connectTable.ColorId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", connectTable.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", connectTable.SizeId);
            return View(connectTable);
        }

        // POST: ConnectTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ProductesId,int SizeId,int ColorId, [Bind("ProductesId,SizeId,ColorId,AmountInStock")] ConnectTable connectTable)
        {
            if (ProductesId != connectTable.ProductesId)
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
                return RedirectToAction("List");
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", connectTable.ColorId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "Id", connectTable.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", connectTable.SizeId);
            return View(connectTable);
        }



        // GET: ConnectTables/Delete/5
        public async Task<IActionResult> Delete(int? ProductesId, int? ColorId, int? SizeId)
        {
            if (ProductesId == null)
            {
                return NotFound();
            }

            var connectTable = await _context.ConnectTable
                .Include(c => c.Color)
                .Include(c => c.Productes)
                .Include(c => c.Size)
                .SingleOrDefaultAsync(m => m.ProductesId == ProductesId && m.SizeId == SizeId && m.ColorId == ColorId);
            if (connectTable == null)
            {
                return NotFound();
            }

            return View(connectTable);
        }

        // POST: ConnectTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProductesId,int SizeId,int ColorId)
        {
            var connectTable = await _context.ConnectTable.SingleOrDefaultAsync(m => m.ProductesId == ProductesId && m.ColorId==ColorId && m.SizeId==SizeId);
            _context.ConnectTable.Remove(connectTable);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        private bool ConnectTableExists(int id)
        {
            return _context.ConnectTable.Any(e => e.ProductesId == id);
        }

    }
}
