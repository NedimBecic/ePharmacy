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
    public class PlaceOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaceOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlaceOrder
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: PlaceOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeOrderViewModel = await _context.PlaceOrderViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeOrderViewModel == null)
            {
                return NotFound();
            }

            return View(placeOrderViewModel);
        }

        // GET: PlaceOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlaceOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PaymentMethod,DeliveryOptions,Street,Number,City,PostalCode,Email,Phone,Message")] PlaceOrderViewModel placeOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placeOrderViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(placeOrderViewModel);
        }

        // GET: PlaceOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeOrderViewModel = await _context.PlaceOrderViewModel.FindAsync(id);
            if (placeOrderViewModel == null)
            {
                return NotFound();
            }
            return View(placeOrderViewModel);
        }

        // POST: PlaceOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PaymentMethod,DeliveryOptions,Street,Number,City,PostalCode,Email,Phone,Message")] PlaceOrderViewModel placeOrderViewModel)
        {
            if (id != placeOrderViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placeOrderViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceOrderViewModelExists(placeOrderViewModel.Id))
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
            return View(placeOrderViewModel);
        }

        // GET: PlaceOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeOrderViewModel = await _context.PlaceOrderViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeOrderViewModel == null)
            {
                return NotFound();
            }

            return View(placeOrderViewModel);
        }

        // POST: PlaceOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placeOrderViewModel = await _context.PlaceOrderViewModel.FindAsync(id);
            if (placeOrderViewModel != null)
            {
                _context.PlaceOrderViewModel.Remove(placeOrderViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceOrderViewModelExists(int id)
        {
            return _context.PlaceOrderViewModel.Any(e => e.Id == id);
        }
    }
}
