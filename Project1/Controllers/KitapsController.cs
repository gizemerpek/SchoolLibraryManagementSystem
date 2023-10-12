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
    public class KitapsController : Controller
    {
        private readonly KutuphaneyeniContext _context;

        public KitapsController(KutuphaneyeniContext context)
        {
            _context = context;
        }

        // GET: Kitaps
        public async Task<IActionResult> Index()
        {
            var kutuphaneyeniContext = _context.Kitaps.Include(k => k.TurnoNavigation).Include(k => k.YazarnoNavigation);
            List<SelectListItem> values = (from x in _context.Turs.ToList() select new SelectListItem { Text = x.Ad, Value = x.Turno.ToString() })
                .ToList();
            ViewBag.vTry = values;
                return View(await kutuphaneyeniContext.ToListAsync());
        }

        // GET: Kitaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kitaps == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaps
                .Include(k => k.TurnoNavigation)
                .Include(k => k.YazarnoNavigation)
                .FirstOrDefaultAsync(m => m.Kitapno == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // GET: Kitaps/Create
        public IActionResult Create()
        {
            ViewData["Turno"] = new SelectList(_context.Turs, "Turno", "Turno");
            ViewData["Yazarno"] = new SelectList(_context.Yazars, "Yazarno", "Yazarno");
            return View();
        }

        // POST: Kitaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Kitapno,Ad,Sayfasayisi,Puan,Yazarno,Turno")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Turno"] = new SelectList(_context.Turs, "Turno", "Turno", kitap.Turno);
            ViewData["Yazarno"] = new SelectList(_context.Yazars, "Yazarno", "Yazarno", kitap.Yazarno);
            return View(kitap);
        }

        // GET: Kitaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kitaps == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaps.FindAsync(id);
            if (kitap == null)
            {
                return NotFound();
            }
            ViewData["Turno"] = new SelectList(_context.Turs, "Turno", "Turno", kitap.Turno);
            ViewData["Yazarno"] = new SelectList(_context.Yazars, "Yazarno", "Yazarno", kitap.Yazarno);
            return View(kitap);
        }

        // POST: Kitaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Kitapno,Ad,Sayfasayisi,Puan,Yazarno,Turno")] Kitap kitap)
        {
            if (id != kitap.Kitapno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapExists(kitap.Kitapno))
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
            ViewData["Turno"] = new SelectList(_context.Turs, "Turno", "Turno", kitap.Turno);
            ViewData["Yazarno"] = new SelectList(_context.Yazars, "Yazarno", "Yazarno", kitap.Yazarno);
            return View(kitap);
        }

        // GET: Kitaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kitaps == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaps
                .Include(k => k.TurnoNavigation)
                .Include(k => k.YazarnoNavigation)
                .FirstOrDefaultAsync(m => m.Kitapno == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // POST: Kitaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kitaps == null)
            {
                return Problem("Entity set 'KutuphaneyeniContext.Kitaps'  is null.");
            }
            var kitap = await _context.Kitaps.FindAsync(id);
            if (kitap != null)
            {
                _context.Kitaps.Remove(kitap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapExists(int id)
        {
          return (_context.Kitaps?.Any(e => e.Kitapno == id)).GetValueOrDefault();
        }
    }
}
