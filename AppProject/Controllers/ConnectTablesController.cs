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
            var appProjectContext = _context.ConnectTable.Include(c => c.Color).Include(c => c.Mart).ThenInclude(x => x.Customer).Include(c => c.Productes).Include(c => c.Size).Where(p=>p.Mart.Customer.Mail== HttpContext.Session.GetString("Mail"));

            ViewBag.Mail = HttpContext.Session.GetString("Mail");

            if (ViewBag.Mail == null)
                ViewBag.ConnectClient = false;
            else
                ViewBag.ConnectClient = true;

            ViewBag.total = appProjectContext.Where(p=>p.AmountInStock > 0).Sum(p => p.Productes.Price * p.AmountOfOrders);
             

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
        public async Task<IActionResult> Create([Bind("ProductesId,SizeId,ColorId,MartId,AmountOfOrders")] ConnectTable connectTable)
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

        // GET: ConnectTables/Add
        public IActionResult Add(int? id)
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "ColorName");             
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "SizeName");

            ViewBag.ProductId = _context.Productes.Where(p => p.Id == id);

            ViewBag.MartId = (from u in _context.Customer
                              where u.Mail == HttpContext.Session.GetString("Mail")
                              select u.Id);

            //ViewBag.AmountInStock = _context.ConnectTable.Select(p => p.AmountInStock);
            return View();
        }

        // POST: ConnectTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("ProductesId,SizeId,ColorId,MartId,AmountOfOrders,AmountInStock")] ConnectTable connectTable)
        {
            if (!ConnectTableExist(connectTable.ProductesId, connectTable.SizeId, connectTable.ColorId) && connectTable.MartId != 0)
            {
                //כאשר אופציה זו לא נמצאת במלאי(בטבלה המקשרת 
                //אני יוסיף אבל בטבלה של העגלה יהיה רשום חחסר במלאי
                _context.Add(connectTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            if (ConnectTableExist(connectTable.ProductesId, connectTable.SizeId, connectTable.ColorId) && connectTable.MartId != 0)
            {
                //כשאופציה זו קיימת יחסר מכמות המלאי??
                ViewBag.MaxAmountOfOrders = _context.ConnectTable.Where(p => p.ProductesId == connectTable.ProductesId && p.SizeId == connectTable.SizeId && p.ColorId == connectTable.ColorId && p.MartId == connectTable.MartId).Sum(c=>c.AmountInStock-connectTable.AmountOfOrders);
                _context.Update(connectTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            return RedirectToAction("LogIn", "Customers");

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
        public async Task<IActionResult> Edit(int id, [Bind("ProductesId,SizeId,ColorId,MartId,AmountOfOrders")] ConnectTable connectTable)
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
        public async Task<IActionResult> DeleteConfirmed(int id,string N_Color, string N_Size)
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

                private bool ConnectTableExist(int id,int sizeid,int colorid)
        {
            return _context.ConnectTable.Any(e => e.ProductesId == id && e.SizeId==sizeid && e.ColorId==colorid);
        }
    }
}
