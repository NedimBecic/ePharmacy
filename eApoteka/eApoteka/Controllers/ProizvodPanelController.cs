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
    public class ProizvodPanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodPanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProizvodPanel
        public IActionResult Index()
        {

            return View();
        }

        // GET: ProizvodPanel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi
                .Include(p => p.Admin)
                .Include(p => p.Apotekar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // GET: ProizvodPanel/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Ime");
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime");
            return View();
        }

        // POST: ProizvodPanel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Vrsta,Opis,Kolicina,Cijena,ApotekarId,AdminId,Dostupan")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proizvod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Ime", proizvod.AdminId);
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", proizvod.ApotekarId);
            return View(proizvod);
        }

        // GET: ProizvodPanel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi.FindAsync(id);
            if (proizvod == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Ime", proizvod.AdminId);
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", proizvod.ApotekarId);
            return View(proizvod);
        }

        // POST: ProizvodPanel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Vrsta,Opis,Kolicina,Cijena,ApotekarId,AdminId,Dostupan")] Proizvod proizvod)
        {
            if (id != proizvod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.Id))
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
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Ime", proizvod.AdminId);
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", proizvod.ApotekarId);
            return View(proizvod);
        }

        // GET: ProizvodPanel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi
                .Include(p => p.Admin)
                .Include(p => p.Apotekar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // POST: ProizvodPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvod = await _context.Proizvodi.FindAsync(id);
            if (proizvod != null)
            {
                _context.Proizvodi.Remove(proizvod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(int id)
        {
            return _context.Proizvodi.Any(e => e.Id == id);
        }
    }
}
