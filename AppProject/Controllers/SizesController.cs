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
    public class SizesController : Controller
    {
        private readonly AppProjectContext _context;

        public SizesController(AppProjectContext context)
        {
            _context = context;    
        }

        // GET: Sizes
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Sizes.Include(m => m.Details);

            return PartialView(await databaseContext.Where(s=>s.SizeName=="XS" || s.SizeName=="S" || s.SizeName=="M" || s.SizeName=="L" || s.SizeName=="XL").ToListAsync());
        }

        public async Task<IActionResult> Search(int? sizeid)
        {

            //קשר בין מידה למוצר
            var q = from pro in _context.Productes
                    join connect in _context.ConnectTable on pro.Id equals connect.ProductesId
                    where connect.SizeId == sizeid
                    select new ColorSizeProductVM()
                    {
                        Id = connect.SizeId,
                        ProductName = pro.ProductName,
                        ImgId = pro.ImgId,
                        Price = pro.Price
                    };

            return View(await q.ToListAsync());
        }

        // GET: Sizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizes = await _context.Sizes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sizes == null)
            {
                return NotFound();
            }

            return View(sizes);
        }

        // GET: Sizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SizeName")] Sizes sizes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sizes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sizes);
        }

        // GET: Sizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizes = await _context.Sizes.SingleOrDefaultAsync(m => m.Id == id);
            if (sizes == null)
            {
                return NotFound();
            }
            return View(sizes);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SizeName")] Sizes sizes)
        {
            if (id != sizes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sizes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizesExists(sizes.Id))
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
            return View(sizes);
        }

        // GET: Sizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizes = await _context.Sizes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sizes == null)
            {
                return NotFound();
            }

            return View(sizes);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sizes = await _context.Sizes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sizes.Remove(sizes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SizesExists(int id)
        {
            return _context.Sizes.Any(e => e.Id == id);
        }
    }
}
