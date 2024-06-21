using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin_microservice_v2.Data;
using Admin_microservice_v2.Models;

namespace Admin_microservice_v2.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products.Include(i => i.Gender).Include(i => i.Subcategory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var item = await _context.Products
                .Include(i => i.Gender)
                .Include(i => i.Subcategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["FK_GenderId"] = new SelectList(_context.Genders, "GenderId", "GenderId");
            ViewData["FK_SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryId");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,ProductPrice,FK_SubcategoryId,FK_GenderId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_GenderId"] = new SelectList(_context.Genders, "GenderId", "GenderId", item.FK_GenderId);
            ViewData["FK_SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryId", item.FK_SubcategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["FK_GenderId"] = new SelectList(_context.Genders, "GenderId", "GenderId", item.FK_GenderId);
            ViewData["FK_SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryId", item.FK_SubcategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductDescription,ProductPrice,FK_SubcategoryId,FK_GenderId")] Item item)
        {
            if (id != item.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_GenderId"] = new SelectList(_context.Genders, "GenderId", "GenderId", item.FK_GenderId);
            ViewData["FK_SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryId", item.FK_SubcategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var item = await _context.Products
                .Include(i => i.Gender)
                .Include(i => i.Subcategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            var item = await _context.Products.FindAsync(id);
            if (item != null)
            {
                _context.Products.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
