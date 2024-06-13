using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eApoteka.Data;
using eApoteka.Models.ViewModels;

namespace eApoteka.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            var users = await _context.Korisnici
                .Select(u => new LoginViewModel
                {
                    username = u.Username,
                    password = u.Password
                })
                .ToListAsync();

            return View(users);
        }

        // GET: Login/Details/5
        public IActionResult PlaceOrder()
        {
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginViewModel);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginViewModel = await _context.LoginViewModel.FindAsync(id);
            if (loginViewModel == null)
            {
                return NotFound();
            }
            return View(loginViewModel);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] LoginViewModel loginViewModel)
        {
            if (id != loginViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginViewModelExists(loginViewModel.Id))
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
            return View(loginViewModel);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginViewModel = await _context.LoginViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginViewModel == null)
            {
                return NotFound();
            }

            return View(loginViewModel);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loginViewModel = await _context.LoginViewModel.FindAsync(id);
            if (loginViewModel != null)
            {
                _context.LoginViewModel.Remove(loginViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginViewModelExists(int id)
        {
            return _context.LoginViewModel.Any(e => e.Id == id);
        }
    }
}
