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

        // GET: Register/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,Password,PhoneNumber,Adress")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registerViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registerViewModel);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Email,Password,PhoneNumber,Adress")] RegisterViewModel registerViewModel)
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
