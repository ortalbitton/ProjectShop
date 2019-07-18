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
            var appProjectContext = _context.ConnectTable.Include(c => c.Color).Include(c => c.Mart).Include(c => c.Productes).Include(c => c.Size);

            return View(await appProjectContext.ToListAsync());
        }

        public async Task<IActionResult> List()
        {
            var appProjectContext = _context.ConnectTable.Include(c => c.Color).Include(c => c.Mart).Include(c => c.Productes).Include(c => c.Size);

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


        public IActionResult Add(int? id)
        {

            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName");
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id");

            var Amount = from a in _context.Productes
                         where a.Id==id && a.AmountInStock!=0
                         select a.AmountInStock;

            //כאשר  מוצר נמצא במלאי
            if (Amount.ToList().Count>0)            
                ViewBag.AmountInStock = true;
              else
                ViewBag.AmountInStock = false;

            ViewBag.ProductId = _context.Productes.Where(p => p.Id == id);

            return View();
        }

        // POST: ConnectTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("ProductesId,SizeId,ColorId,MartId")] ConnectTable connectTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(connectTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Marts");
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName", connectTable.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", connectTable.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "ProductName", connectTable.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName", connectTable.SizeId);
            return RedirectToAction("Index,Marts");
        }



        // GET: ConnectTables/Create
        public IActionResult Create()
        {

            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName");
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id");
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "ProductName");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName");
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
                return RedirectToAction("Index","Marts");
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName", connectTable.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", connectTable.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "ProductName", connectTable.ProductesId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName", connectTable.SizeId);
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
            //ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName", connectTable.ColorId);
            ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", connectTable.MartId);
            ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "ProductName", connectTable.ProductesId);
            //ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName", connectTable.SizeId);
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
                return RedirectToAction("List");
            }


            //ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName", connectTable.Color.ColorName);
            //ViewData["MartId"] = new SelectList(_context.Mart, "Id", "Id", connectTable.MartId);
            //ViewData["ProductesId"] = new SelectList(_context.Productes, "Id", "ProductName", connectTable.ProductesId);
            //ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName", connectTable.Size.SizeName);
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
                .FirstOrDefaultAsync(m => m.ProductesId == id);
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
            var connectTable = await _context.ConnectTable.FirstOrDefaultAsync(m => m.ProductesId == id);
            _context.ConnectTable.Remove(connectTable);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Marts");
        }

        private bool ConnectTableExists(int id)
        {
            return _context.ConnectTable.Any(e => e.ProductesId == id);
        }
    }
}
