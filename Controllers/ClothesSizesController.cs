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
    public class ClothesSizesController : Controller
    {
        private readonly AppDbContext _context;

        public ClothesSizesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ClothesSizes
        public async Task<IActionResult> Index()
        {
              return _context.Sizes != null ? 
                          View(await _context.Sizes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Sizes'  is null.");
        }

        // GET: ClothesSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sizes == null)
            {
                return NotFound();
            }

            var clothesSize = await _context.Sizes
                .FirstOrDefaultAsync(m => m.SizeId == id);
            if (clothesSize == null)
            {
                return NotFound();
            }

            return View(clothesSize);
        }

        // GET: ClothesSizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClothesSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SizeId,SizeValue")] ClothesSize clothesSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothesSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothesSize);
        }

        // GET: ClothesSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sizes == null)
            {
                return NotFound();
            }

            var clothesSize = await _context.Sizes.FindAsync(id);
            if (clothesSize == null)
            {
                return NotFound();
            }
            return View(clothesSize);
        }

        // POST: ClothesSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SizeId,SizeValue")] ClothesSize clothesSize)
        {
            if (id != clothesSize.SizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothesSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothesSizeExists(clothesSize.SizeId))
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
            return View(clothesSize);
        }

        // GET: ClothesSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sizes == null)
            {
                return NotFound();
            }

            var clothesSize = await _context.Sizes
                .FirstOrDefaultAsync(m => m.SizeId == id);
            if (clothesSize == null)
            {
                return NotFound();
            }

            return View(clothesSize);
        }

        // POST: ClothesSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sizes == null)
            {
                return Problem("Entity set 'AppDbContext.Sizes'  is null.");
            }
            var clothesSize = await _context.Sizes.FindAsync(id);
            if (clothesSize != null)
            {
                _context.Sizes.Remove(clothesSize);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothesSizeExists(int id)
        {
          return (_context.Sizes?.Any(e => e.SizeId == id)).GetValueOrDefault();
        }
    }
}
