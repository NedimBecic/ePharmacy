using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eApoteka.Data;
using eApoteka.Models.ViewModels;
using eApoteka.Models;
using System.Net.NetworkInformation;

namespace eApoteka.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _context.Korisnici
                .Select(u => new LoginViewModel
                {
                    username = u.Username,
                    password = u.Password
                })
                .FirstOrDefaultAsync();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Username, string Password)
        {   

            var viewModel = new LoginViewModel();
            var admin = _context.Admins.Where(m => m.Password == Password && m.Username == Username).FirstOrDefault();
            if (admin != null)
            {
                HttpContext.Session.SetString("Username", admin.Username);
                HttpContext.Session.SetInt32("UserId", admin.Id);
                HttpContext.Session.SetString("Ime", admin.Ime);
                HttpContext.Session.SetString("Prezime", admin.Prezime);
                HttpContext.Session.SetString("Role", "Admin");
                return RedirectToAction("Index", "AdminPanel");
            }
            var apotekar = _context.Apotekari.Where(m => m.Password == Password && m.Username == Username).FirstOrDefault();
            if (apotekar != null)
            {
                HttpContext.Session.SetString("Username", apotekar.Username);
                HttpContext.Session.SetInt32("UserId", apotekar.Id);
                HttpContext.Session.SetString("Ime", apotekar.Ime);
                HttpContext.Session.SetString("Prezime", apotekar.Prezime);
                HttpContext.Session.SetString("Role", "Apotekar");
                return RedirectToAction("Index", "PharmacistPanel");
            }
            var korisnik = _context.Korisnici.Where(m => m.Password == Password && m.Username == Username).FirstOrDefault();
            if (korisnik != null)
            {
                HttpContext.Session.SetString("Username", korisnik.Username);
                HttpContext.Session.SetInt32("UserId", korisnik.Id);
                HttpContext.Session.SetString("Ime", korisnik.Ime);
                HttpContext.Session.SetString("Prezime", korisnik.Prezime);
                HttpContext.Session.SetString("Adresa", korisnik.Adresa);
                HttpContext.Session.SetString("BrojTelefona", korisnik.BrojTelefona);
                HttpContext.Session.SetString("Role", "Korisnik");

                return RedirectToAction("Index", "Search");
            }


            return RedirectToAction("Index", "Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}
