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
    public class IslemsController : Controller
    {
        private readonly KutuphaneyeniContext _context;

        public IslemsController(KutuphaneyeniContext context)
        {
            _context = context;
        }

        // GET: Islems
        public async Task<IActionResult> Index()
        {
            var kutuphaneyeniContext = _context.Islems.Include(i => i.KitapnoNavigation).Include(i => i.OgrnoNavigation);
            return View(await kutuphaneyeniContext.ToListAsync());
        }

        // GET: Islems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Islems == null)
            {
                return NotFound();
            }

            var islem = await _context.Islems
                .Include(i => i.KitapnoNavigation)
                .Include(i => i.OgrnoNavigation)
                .FirstOrDefaultAsync(m => m.Islemno == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // GET: Islems/Create
        public IActionResult Create()
        {
            ViewData["Kitapno"] = new SelectList(_context.Kitaps, "Kitapno", "Kitapno");
            ViewData["Ogrno"] = new SelectList(_context.Ogrencis, "Ogrno", "Ogrno");
            return View();
        }

        // POST: Islems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Islemno,Ogrno,Kitapno,Atarih,Vtarih")] Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(islem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Kitapno"] = new SelectList(_context.Kitaps, "Kitapno", "Kitapno", islem.Kitapno);
            ViewData["Ogrno"] = new SelectList(_context.Ogrencis, "Ogrno", "Ogrno", islem.Ogrno);
            return View(islem);
        }

        // GET: Islems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Islems == null)
            {
                return NotFound();
            }

            var islem = await _context.Islems.FindAsync(id);
            if (islem == null)
            {
                return NotFound();
            }
            ViewData["Kitapno"] = new SelectList(_context.Kitaps, "Kitapno", "Kitapno", islem.Kitapno);
            ViewData["Ogrno"] = new SelectList(_context.Ogrencis, "Ogrno", "Ogrno", islem.Ogrno);
            return View(islem);
        }

        // POST: Islems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Islemno,Ogrno,Kitapno,Atarih,Vtarih")] Islem islem)
        {
            if (id != islem.Islemno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(islem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslemExists(islem.Islemno))
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
            ViewData["Kitapno"] = new SelectList(_context.Kitaps, "Kitapno", "Kitapno", islem.Kitapno);
            ViewData["Ogrno"] = new SelectList(_context.Ogrencis, "Ogrno", "Ogrno", islem.Ogrno);
            return View(islem);
        }

        // GET: Islems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Islems == null)
            {
                return NotFound();
            }

            var islem = await _context.Islems
                .Include(i => i.KitapnoNavigation)
                .Include(i => i.OgrnoNavigation)
                .FirstOrDefaultAsync(m => m.Islemno == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // POST: Islems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Islems == null)
            {
                return Problem("Entity set 'KutuphaneyeniContext.Islems'  is null.");
            }
            var islem = await _context.Islems.FindAsync(id);
            if (islem != null)
            {
                _context.Islems.Remove(islem);
            }
            
            await _context.SaveChangesAsync();
            TempData["KeyMessage"] = "Record deleted succesfully";
            return RedirectToAction(nameof(Index));
        }

        private bool IslemExists(int id)
        {
          return (_context.Islems?.Any(e => e.Islemno == id)).GetValueOrDefault();
        }
    }
}
