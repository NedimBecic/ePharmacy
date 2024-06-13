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
    public class AdminPanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminPanel
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminPanel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminPanelViewModel = await _context.AdminPanelViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminPanelViewModel == null)
            {
                return NotFound();
            }

            return View(adminPanelViewModel);
        }

        // GET: AdminPanel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] AdminPanelViewModel adminPanelViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminPanelViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminPanelViewModel);
        }

        // GET: AdminPanel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminPanelViewModel = await _context.AdminPanelViewModel.FindAsync(id);
            if (adminPanelViewModel == null)
            {
                return NotFound();
            }
            return View(adminPanelViewModel);
        }

        // POST: AdminPanel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] AdminPanelViewModel adminPanelViewModel)
        {
            if (id != adminPanelViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminPanelViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminPanelViewModelExists(adminPanelViewModel.Id))
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
            return View(adminPanelViewModel);
        }

        // GET: AdminPanel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminPanelViewModel = await _context.AdminPanelViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminPanelViewModel == null)
            {
                return NotFound();
            }

            return View(adminPanelViewModel);
        }

        // POST: AdminPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminPanelViewModel = await _context.AdminPanelViewModel.FindAsync(id);
            if (adminPanelViewModel != null)
            {
                _context.AdminPanelViewModel.Remove(adminPanelViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminPanelViewModelExists(int id)
        {
            return _context.AdminPanelViewModel.Any(e => e.Id == id);
        }
    }
}
