using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eApoteka.Data;
using eApoteka.Models;
using Microsoft.AspNetCore.Identity;

namespace eApoteka.Controllers
{
    public class ShowOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShowOrdersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var orders = await _context.Narudzbe
                .Include(n => n.Stavke)
                    .ThenInclude(s => s.Proizvod)
                .Include(n => n.StatusNarudzbe)
                .Include(n => n.DetaljDostave)
                .Where(n => n.KorisnikId == (int)HttpContext.Session.GetInt32("UserId") && n.StatusNarudzbe.TrenutniStatus == "Placeno")
                .OrderBy(n => n.Timestamp)
                .ToListAsync();

            

            return View(orders);
        }

        
    }
}
