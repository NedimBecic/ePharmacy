using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eApoteka.Data;
using eApoteka.Models;
using Microsoft.AspNetCore.Http;

namespace eApoteka.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string OrderSessionKey = "CurrentOrder";

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> AddToOrder(int? id)
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

           
            Narudzba narudzba;
            if (HttpContext.Session.GetInt32(OrderSessionKey) != null)
            {
                int orderId = HttpContext.Session.GetInt32(OrderSessionKey).Value;
                narudzba = await _context.Narudzbe
                    .Include(n => n.Stavke)
                    .FirstOrDefaultAsync(n => n.Id == orderId);

                if (narudzba == null)
                {
                    return NotFound();
                }
            }
            else
            {
                
                var detaljDostave = new DetaljDostave
                {
                    AdresaDostave = "Default Address",
                    NacinDostave = "Default Method",
                    DostavljacId = 1 
                };
                _context.DetaljiDostave.Add(detaljDostave);
                await _context.SaveChangesAsync();

                var detaljPlacanja = new DetaljPlacanja
                {
                    TipPlacanja = "Default Payment",
                    StatusPlacanja = "Pending",
                    DatumPlacanja = DateTime.Now
                };
                _context.DetaljiPlacanja.Add(detaljPlacanja);
                await _context.SaveChangesAsync();

                var statusNarudzbe = new StatusNarudzbe
                {
                    TrenutniStatus = "Pending"
                };
                _context.StatusiNarudzbi.Add(statusNarudzbe);
                await _context.SaveChangesAsync();

                narudzba = new Narudzba
                {
                    DetaljPlacanjaId = detaljPlacanja.Id,
                    DetaljDostaveId = detaljDostave.Id,
                    KorisnikId = (int)HttpContext.Session.GetInt32("UserId"), 
                    ApotekarId = 1, 
                    StatusNarudzbeId = statusNarudzbe.Id,
                    Timestamp = DateTime.Now
                };

                _context.Narudzbe.Add(narudzba);
                await _context.SaveChangesAsync();

                
                HttpContext.Session.SetInt32(OrderSessionKey, narudzba.Id);
            }

            
            var existingStavka = narudzba.Stavke.FirstOrDefault(s => s.ProizvodId == proizvod.Id);
            if (existingStavka != null)
            {
                existingStavka.Kolicina += 1;
            }
            else
            {
                var stavkaNarudzbe = new StavkaNarudzbe
                {
                    ProizvodId = proizvod.Id,
                    Kolicina = 1
                };
                narudzba.Stavke.Add(stavkaNarudzbe);
            }

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Details), new { id = narudzba.Id });
        }


        public IActionResult Index()
        {

            return View();
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe
                .Include(n => n.Stavke)
                    .ThenInclude(s => s.Proizvod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            ViewBag.OrderTotal = narudzba.Stavke.Sum(s => s.Proizvod.Cijena * s.Kolicina);

            return View(narudzba);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int stavkaId, int quantity)
        {
            var stavka = await _context.StavkeNarudzbe.FindAsync(stavkaId);
            if (stavka == null)
            {
                return NotFound();
            }

            stavka.Kolicina = quantity;
            await _context.SaveChangesAsync();

            var narudzba = await _context.Narudzbe
                .Include(n => n.Stavke)
                    .ThenInclude(s => s.Proizvod)
                .FirstOrDefaultAsync(n => n.Stavke.Any(s => s.Id == stavkaId));

            var orderTotal = narudzba.Stavke.Sum(s => s.Proizvod.Cijena * s.Kolicina);

            return Json(new { orderTotal });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int stavkaId)
        {
            var stavka = await _context.StavkeNarudzbe.FindAsync(stavkaId);
            if (stavka == null)
            {
                return NotFound();
            }

            _context.StavkeNarudzbe.Remove(stavka);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
