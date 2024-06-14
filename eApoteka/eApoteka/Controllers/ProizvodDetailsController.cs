using Microsoft.AspNetCore.Mvc;
using eApoteka.Data;
using eApoteka.Models;
using System.Threading.Tasks;

namespace eApoteka.Controllers
{
    public class ProizvodDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var product = await _context.Proizvodi.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
