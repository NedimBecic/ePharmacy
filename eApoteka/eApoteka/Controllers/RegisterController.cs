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

namespace eApoteka.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;


        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: Register
        public IActionResult Index()
        {
            var viewModel = new RegisterViewModel();

            return View(viewModel);
        }


        // GET: Register/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerViewModel = await _context.RegisterViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerViewModel == null)
            {
                return NotFound();
            }

            return View(registerViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Name, string Surname, string Username, string Password, string PhoneNumber, string Adress)
        {
            var model = new Korisnik();

            if (ModelState.IsValid)
            {

                //model.Id= Id;
                model.Ime = Name;
                model.Prezime = Surname;
                model.Username = Username;
                model.Password = Password;
                model.BrojTelefona = PhoneNumber;
                model.Adresa = Adress;
                model.Role = "Korisnik";

                HttpContext.Session.SetString("Username", model.Username);
                HttpContext.Session.SetInt32("UserId", model.Id);
                HttpContext.Session.SetString("Ime", model.Ime);
                HttpContext.Session.SetString("Prezime", model.Prezime);
                HttpContext.Session.SetString("Adresa", model.Adresa);
                HttpContext.Session.SetString("BrojTelefona", model.BrojTelefona);
                HttpContext.Session.SetString("Role", "Korisnik");

                _context.Korisnici.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Login");
            }
            return View(model);
        }

        // GET: Register/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerViewModel = await _context.RegisterViewModel.FindAsync(id);
            if (registerViewModel == null)
            {
                return NotFound();
            }
            return View(registerViewModel);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Username,Password,PhoneNumber,Adress")] RegisterViewModel registerViewModel)
        {
            if (id != registerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterViewModelExists(registerViewModel.Id))
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
            return View(registerViewModel);
        }

        // GET: Register/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerViewModel = await _context.RegisterViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerViewModel == null)
            {
                return NotFound();
            }

            return View(registerViewModel);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registerViewModel = await _context.RegisterViewModel.FindAsync(id);
            if (registerViewModel != null)
            {
                _context.RegisterViewModel.Remove(registerViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterViewModelExists(int id)
        {
            return _context.RegisterViewModel.Any(e => e.Id == id);
        }
    }
}
