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
    public class ItemSizesController : Controller
    {
        private readonly AppDbContext _context;

        public ItemSizesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ItemSizes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ItemSizes.Include(i => i.Product).Include(i => i.Size);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ItemSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ItemSizes == null)
            {
                return NotFound();
            }

            var itemSize = await _context.ItemSizes
                .Include(i => i.Product)
                .Include(i => i.Size)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (itemSize == null)
            {
                return NotFound();
            }

            return View(itemSize);
        }

        // GET: ItemSizes/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId");
            return View();
        }

        // POST: ItemSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,SizeId")] ItemSize itemSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", itemSize.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", itemSize.SizeId);
            return View(itemSize);
        }

        // GET: ItemSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ItemSizes == null)
            {
                return NotFound();
            }

            var itemSize = await _context.ItemSizes.FindAsync(id);
            if (itemSize == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", itemSize.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", itemSize.SizeId);
            return View(itemSize);
        }

        // POST: ItemSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,SizeId")] ItemSize itemSize)
        {
            if (id != itemSize.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemSizeExists(itemSize.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", itemSize.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", itemSize.SizeId);
            return View(itemSize);
        }

        // GET: ItemSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ItemSizes == null)
            {
                return NotFound();
            }

            var itemSize = await _context.ItemSizes
                .Include(i => i.Product)
                .Include(i => i.Size)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (itemSize == null)
            {
                return NotFound();
            }

            return View(itemSize);
        }

        // POST: ItemSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ItemSizes == null)
            {
                return Problem("Entity set 'AppDbContext.ItemSizes'  is null.");
            }
            var itemSize = await _context.ItemSizes.FindAsync(id);
            if (itemSize != null)
            {
                _context.ItemSizes.Remove(itemSize);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemSizeExists(int id)
        {
          return (_context.ItemSizes?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
