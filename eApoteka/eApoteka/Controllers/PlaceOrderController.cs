using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eApoteka.Data;
using eApoteka.Models;
using eApoteka.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;

namespace eApoteka.Controllers
{
    public class PlaceOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string OrderSessionKey = "CurrentOrder";

        public PlaceOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PlaceOrderViewModel model)
        {
            
                var orderId = HttpContext.Session.GetInt32(OrderSessionKey);
                if (orderId == null)
                {
                    return NotFound();
                }

                var narudzba = await _context.Narudzbe
                    .Include(n => n.Stavke)
                        .ThenInclude(s => s.Proizvod)
                    .Include(n => n.DetaljDostave)
                    .Include(n => n.DetaljPlacanja)
                    .Include(n => n.StatusNarudzbe)
                    .FirstOrDefaultAsync(n => n.Id == orderId.Value);

                if (narudzba == null)
                {
                    return NotFound();
                }

                if (model.DeliveryOptions == "Delivery")
                {
                    narudzba.DetaljDostave.AdresaDostave = $"{model.Street} {model.Number}, {model.City}, {model.PostalCode}";
                } else if(model.DeliveryOptions == "In-person")
                {
                    narudzba.DetaljDostave.AdresaDostave = "Delivery in-person.";
                }
                narudzba.DetaljDostave.NacinDostave = model.DeliveryOptions;
                narudzba.DetaljDostave.brojTelefona = Int32.Parse(model.Phone);

                narudzba.DetaljPlacanja.TipPlacanja = model.PaymentMethod;

                narudzba.KorisnikId = (int)HttpContext.Session.GetInt32("UserId");
                narudzba.Timestamp = DateTime.Now;
                narudzba.StatusNarudzbe.TrenutniStatus = "Placed";

                await _context.SaveChangesAsync();

                if (model.PaymentMethod == "Card")
                {
                    var lineItems = GetLineItemsForOrder(narudzba);

                    if (!lineItems.Any())
                    {
                        return NotFound("No items found for the order.");
                    }

                    var options = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                        LineItems = lineItems,
                        Mode = "payment",
                        SuccessUrl = Url.Action("ShowOrders", "PlaceOrder", new { id = narudzba.Id }, Request.Scheme),
                        CancelUrl = Url.Action("Details", "PlaceOrder", null, Request.Scheme),
                    };
                    var service = new SessionService();
                    Session session = service.Create(options);

                    narudzba.StatusNarudzbe.TrenutniStatus = "Placeno"; 

                    await _context.SaveChangesAsync();

                    return Json(new { url = session.Url });
                } else
                {
                    narudzba.StatusNarudzbe.TrenutniStatus = "Placeno";
                await _context.SaveChangesAsync();
            }

                HttpContext.Session.Remove(OrderSessionKey);

                return RedirectToAction("ShowOrders", new { id = narudzba.Id });
            
            return View(model);
        }

        private List<SessionLineItemOptions> GetLineItemsForOrder(Narudzba narudzba)
        {
            return narudzba.Stavke.Select(stavka => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(stavka.Proizvod.Cijena * 100), 
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = stavka.Proizvod.Naziv,
                    },
                },
                Quantity = stavka.Kolicina,
            }).ToList();
        }

        public async Task<IActionResult> OrderSummary(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe
                .Include(n => n.Stavke)
                    .ThenInclude(s => s.Proizvod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }
    }
}
