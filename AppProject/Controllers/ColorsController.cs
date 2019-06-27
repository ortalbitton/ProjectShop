using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppProject.Models;
using AppProject.ViewModel;

namespace AppProject.Controllers
{
    public class ColorsController : Controller
    {
        private readonly AppProjectContext _context;

        public ColorsController(AppProjectContext context)
        {
            _context = context;    
        }

        // GET: Colors
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Colors.Include(m => m.Details);


            return PartialView(await databaseContext.ToListAsync());
        }

        public async Task<IActionResult> Search(int? colorid)
        {

            var product = await _context.Colors.Include(m => m.Details)
                .SingleOrDefaultAsync(m => m.Id == colorid);

            //��� ��� ��� �����
            var Productes = from pro in _context.Productes
                            join connect in product.Details
                            on pro.Id equals connect.ColorId
                            select new ColorSizeProductVM() { Id=connect.ProductesId,ImgId=pro.ImgId,ProductName=pro.ProductName,Price=pro.Price };
        

            return View(await Productes.ToListAsync());
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colors = await _context.Colors.Include(m=>m.Details)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (colors == null)
            {
                return NotFound();
            }

            return View(colors);
        
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColorName")] Colors colors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colors);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(colors);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colors = await _context.Colors.SingleOrDefaultAsync(m => m.Id == id);
            if (colors == null)
            {
                return NotFound();
            }
            return View(colors);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ColorName")] Colors colors)
        {
            if (id != colors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorsExists(colors.Id))
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
            return View(colors);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colors = await _context.Colors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (colors == null)
            {
                return NotFound();
            }

            return View(colors);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colors = await _context.Colors.SingleOrDefaultAsync(m => m.Id == id);
            _context.Colors.Remove(colors);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ColorsExists(int id)
        {
            return _context.Colors.Any(e => e.Id == id);
        }
    }
}
