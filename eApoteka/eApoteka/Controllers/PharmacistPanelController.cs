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
    public class PharmacistPanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PharmacistPanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PharmacistPanel
        public IActionResult Index()
        {
            var upitnici = _context.Upiti.Where(x => x.Odgovor == "Upit nije pregledan.").ToList();
            return View(upitnici);
        }

        // GET: PharmacistPanel/Details/5
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








        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateOdgovor(int id, string odg)

        //{
        //    var upitnici = _context.Upiti.ToList();
        //    Upit pomocna = upitnici[id];
        //    if (ModelState.IsValid)
        //    {
        //        pomocna.Odgovor = odg;
        //        _context.Update(pomocna);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(pomocna);
        //}


        [HttpPost]
        public ActionResult UpdateInstance(string newText, int id)
        {

            var upitnik = _context.Upiti.FirstOrDefault(x => x.Id == id);

            if (upitnik != null)
            {
                upitnik.Odgovor = newText;

                _context.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, errorMessage = "Instance not found" });
        }













        // GET: PharmacistPanel/Create
        public IActionResult Create()
        {
            ViewData["ApotekarId"] = new SelectList(_context.Apotekari, "Id", "Ime");
            ViewData["KorisnikId"] = new SelectList(_context.Korisnici, "Id", "Adresa");
            return View();
        }

        // POST: PharmacistPanel/Create
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

        // GET: PharmacistPanel/Edit/5
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

        // POST: PharmacistPanel/Edit/5
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

        // GET: PharmacistPanel/Delete/5
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

        // POST: PharmacistPanel/Delete/5
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
