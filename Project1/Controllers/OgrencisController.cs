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
    public class OgrencisController : Controller
    {
        private readonly KutuphaneyeniContext _context;

        public OgrencisController(KutuphaneyeniContext context)
        {
            _context = context;
        }

        // GET: Ogrencis
        public async Task<IActionResult> Index()
        {
              return _context.Ogrencis != null ? 
                          View(await _context.Ogrencis.ToListAsync()) :
                          Problem("Entity set 'KutuphaneyeniContext.Ogrencis'  is null.");
        }

        // GET: Ogrencis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ogrencis == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrencis
                .FirstOrDefaultAsync(m => m.Ogrno == id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        // GET: Ogrencis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ogrencis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ogrno,Ad,Soyad,Dtarih,Cinsiyet,Sinif,Puan")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogrenci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ogrenci);
        }

        // GET: Ogrencis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ogrencis == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrencis.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);
        }

        // POST: Ogrencis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ogrno,Ad,Soyad,Dtarih,Cinsiyet,Sinif,Puan")] Ogrenci ogrenci)
        {
            if (id != ogrenci.Ogrno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogrenci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgrenciExists(ogrenci.Ogrno))
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
            return View(ogrenci);
        }

        // GET: Ogrencis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ogrencis == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrencis
                .FirstOrDefaultAsync(m => m.Ogrno == id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        // POST: Ogrencis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ogrencis == null)
            {
                return Problem("Entity set 'KutuphaneyeniContext.Ogrencis'  is null.");
            }
            var ogrenci = await _context.Ogrencis.FindAsync(id);
            if (ogrenci != null)
            {
                _context.Ogrencis.Remove(ogrenci);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgrenciExists(int id)
        {
          return (_context.Ogrencis?.Any(e => e.Ogrno == id)).GetValueOrDefault();
        }
    }
}
