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
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Search
        public IActionResult Index()
        {
            var viewModel = new SearchViewModel
            {
                Search = string.Empty
            };

            return View(viewModel);
        }

        // GET: Search/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchViewModel = await _context.SearchViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (searchViewModel == null)
            {
                return NotFound();
            }

            return View(searchViewModel);
        }

        // GET: Search/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Search")] SearchViewModel searchViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(searchViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(searchViewModel);
        }

        // GET: Search/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchViewModel = await _context.SearchViewModel.FindAsync(id);
            if (searchViewModel == null)
            {
                return NotFound();
            }
            return View(searchViewModel);
        }

        // POST: Search/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Search")] SearchViewModel searchViewModel)
        {
            if (id != searchViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(searchViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SearchViewModelExists(searchViewModel.Id))
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
            return View(searchViewModel);
        }

        // GET: Search/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchViewModel = await _context.SearchViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (searchViewModel == null)
            {
                return NotFound();
            }

            return View(searchViewModel);
        }

        // POST: Search/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var searchViewModel = await _context.SearchViewModel.FindAsync(id);
            if (searchViewModel != null)
            {
                _context.SearchViewModel.Remove(searchViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SearchViewModelExists(int id)
        {
            return _context.SearchViewModel.Any(e => e.Id == id);
        }
    }
}
