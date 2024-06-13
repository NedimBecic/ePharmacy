using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eApoteka.Data;
using eApoteka.Models;

namespace eApoteka.Controllers
{
    public class UpitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UpitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Upits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Upiti.Include(u => u.Apotekar).Include(u => u.Korisnik);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Upits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upit = await _context.Upiti
                .Include(u => u.Apotekar)
                .Include(u => u.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upit == null)
            {
                return NotFound();
            }

            return View(upit);
        }

        // GET: Upits/Create
        public IActionResult Create()
        {
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime");
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa");
            return View();
        }

        // POST: Upits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KorisnikId,ApotekarId,Text,Odgovor")] Upit upit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(upit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", upit.ApotekarId);
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa", upit.KorisnikId);
            return View(upit);
        }

        // GET: Upits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upit = await _context.Upiti.FindAsync(id);
            if (upit == null)
            {
                return NotFound();
            }
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", upit.ApotekarId);
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa", upit.KorisnikId);
            return View(upit);
        }

        // POST: Upits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KorisnikId,ApotekarId,Text,Odgovor")] Upit upit)
        {
            if (id != upit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpitExists(upit.Id))
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
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", upit.ApotekarId);
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa", upit.KorisnikId);
            return View(upit);
        }

        // GET: Upits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upit = await _context.Upiti
                .Include(u => u.Apotekar)
                .Include(u => u.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upit == null)
            {
                return NotFound();
            }

            return View(upit);
        }

        // POST: Upits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upit = await _context.Upiti.FindAsync(id);
            if (upit != null)
            {
                _context.Upiti.Remove(upit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpitExists(int id)
        {
            return _context.Upiti.Any(e => e.Id == id);
        }
    }
}
