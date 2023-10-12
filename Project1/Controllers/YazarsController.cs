using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Controllers
{
    public class YazarsController : Controller
    {
        private readonly KutuphaneyeniContext _context;

        public YazarsController(KutuphaneyeniContext context)
        {
            _context = context;
        }

        // GET: Yazars
        public async Task<IActionResult> Index()
        {
              return _context.Yazars != null ? 
                          View(await _context.Yazars.ToListAsync()) :
                          Problem("Entity set 'KutuphaneyeniContext.Yazars'  is null.");
        }

        // GET: Yazars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Yazars == null)
            {
                return NotFound();
            }

            var yazar = await _context.Yazars
                .FirstOrDefaultAsync(m => m.Yazarno == id);
            if (yazar == null)
            {
                return NotFound();
            }

            return View(yazar);
        }

        // GET: Yazars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yazars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Yazarno,Ad,Soyad")] Yazar yazar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yazar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yazar);
        }

        // GET: Yazars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Yazars == null)
            {
                return NotFound();
            }

            var yazar = await _context.Yazars.FindAsync(id);
            if (yazar == null)
            {
                return NotFound();
            }
            return View(yazar);
        }

        // POST: Yazars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Yazarno,Ad,Soyad")] Yazar yazar)
        {
            if (id != yazar.Yazarno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yazar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YazarExists(yazar.Yazarno))
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
            return View(yazar);
        }

        // GET: Yazars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Yazars == null)
            {
                return NotFound();
            }

            var yazar = await _context.Yazars
                .FirstOrDefaultAsync(m => m.Yazarno == id);
            if (yazar == null)
            {
                return NotFound();
            }

            return View(yazar);
        }

        // POST: Yazars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Yazars == null)
            {
                return Problem("Entity set 'KutuphaneyeniContext.Yazars'  is null.");
            }
            var yazar = await _context.Yazars.FindAsync(id);
            if (yazar != null)
            {
                _context.Yazars.Remove(yazar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YazarExists(int id)
        {
          return (_context.Yazars?.Any(e => e.Yazarno == id)).GetValueOrDefault();
        }
    }
}
