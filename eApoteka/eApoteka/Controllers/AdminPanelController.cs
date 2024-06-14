using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eApoteka.Data;
using eApoteka.Models;
using eApoteka.Models.ViewModels;

namespace eApoteka.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminPanel
        public async Task<IActionResult> Index()
        {
            var users = await _context.Korisnici.ToListAsync();
            var pharmacists = await _context.Apotekari.ToListAsync();
            var deliveryPersons = await _context.Dostavljaci.ToListAsync();

            var viewModel = new AdminPanelViewModel
            {
                korisnici = users,
                apotekari = pharmacists,
                dostavljaci = deliveryPersons
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Korisnici.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Korisnici.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePharmacist(int id)
        {
            var pharmacist = await _context.Apotekari.FindAsync(id);
            if (pharmacist == null)
            {
                return NotFound();
            }
            _context.Apotekari.Remove(pharmacist);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDeliveryPerson(int id)
        {
            var deliveryPerson = await _context.Dostavljaci.FindAsync(id);
            if (deliveryPerson == null)
            {
                return NotFound();
            }
            _context.Dostavljaci.Remove(deliveryPerson);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
