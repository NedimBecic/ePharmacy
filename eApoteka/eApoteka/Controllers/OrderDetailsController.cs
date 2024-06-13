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
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public IActionResult Index()
        {
            return View();
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe
                .Include(n => n.Apotekar)
                .Include(n => n.DetaljDostave)
                .Include(n => n.DetaljPlacanja)
                .Include(n => n.Korisnik)
                .Include(n => n.StatusNarudzbe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime");
            ViewData["DetaljDostaveId"] = new SelectList(_context.DetaljiDostave, "Id", "AdresaDostave");
            ViewData["DetaljPlacanjaId"] = new SelectList(_context.DetaljiPlacanja, "Id", "StatusPlacanja");
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa");
            ViewData["StatusNarudzbeId"] = new SelectList(_context.StatusiNarudzbi, "Id", "TrenutniStatus");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DetaljPlacanjaId,DetaljDostaveId,KorisnikId,ApotekarId,StatusNarudzbeId,Timestamp")] Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(narudzba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", narudzba.ApotekarId);
            ViewData["DetaljDostaveId"] = new SelectList(_context.DetaljiDostave, "Id", "AdresaDostave", narudzba.DetaljDostaveId);
            ViewData["DetaljPlacanjaId"] = new SelectList(_context.DetaljiPlacanja, "Id", "StatusPlacanja", narudzba.DetaljPlacanjaId);
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa", narudzba.KorisnikId);
            ViewData["StatusNarudzbeId"] = new SelectList(_context.StatusiNarudzbi, "Id", "TrenutniStatus", narudzba.StatusNarudzbeId);
            return View(narudzba);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe.FindAsync(id);
            if (narudzba == null)
            {
                return NotFound();
            }
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", narudzba.ApotekarId);
            ViewData["DetaljDostaveId"] = new SelectList(_context.DetaljiDostave, "Id", "AdresaDostave", narudzba.DetaljDostaveId);
            ViewData["DetaljPlacanjaId"] = new SelectList(_context.DetaljiPlacanja, "Id", "StatusPlacanja", narudzba.DetaljPlacanjaId);
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa", narudzba.KorisnikId);
            ViewData["StatusNarudzbeId"] = new SelectList(_context.StatusiNarudzbi, "Id", "TrenutniStatus", narudzba.StatusNarudzbeId);
            return View(narudzba);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DetaljPlacanjaId,DetaljDostaveId,KorisnikId,ApotekarId,StatusNarudzbeId,Timestamp")] Narudzba narudzba)
        {
            if (id != narudzba.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.Id))
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
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime", narudzba.ApotekarId);
            ViewData["DetaljDostaveId"] = new SelectList(_context.DetaljiDostave, "Id", "AdresaDostave", narudzba.DetaljDostaveId);
            ViewData["DetaljPlacanjaId"] = new SelectList(_context.DetaljiPlacanja, "Id", "StatusPlacanja", narudzba.DetaljPlacanjaId);
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa", narudzba.KorisnikId);
            ViewData["StatusNarudzbeId"] = new SelectList(_context.StatusiNarudzbi, "Id", "TrenutniStatus", narudzba.StatusNarudzbeId);
            return View(narudzba);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe
                .Include(n => n.Apotekar)
                .Include(n => n.DetaljDostave)
                .Include(n => n.DetaljPlacanja)
                .Include(n => n.Korisnik)
                .Include(n => n.StatusNarudzbe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudzba = await _context.Narudzbe.FindAsync(id);
            if (narudzba != null)
            {
                _context.Narudzbe.Remove(narudzba);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudzbaExists(int id)
        {
            return _context.Narudzbe.Any(e => e.Id == id);
        }
    }
}
