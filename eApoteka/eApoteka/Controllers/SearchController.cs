using System.Linq;
using Microsoft.AspNetCore.Mvc;
using eApoteka.Data;
using eApoteka.Models.ViewModels;
using eApoteka.Models;

namespace eApoteka.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchViewModel
            {
                Search = string.Empty
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetSuggestions(string term)
        {
            var suggestions = _context.Proizvodi
                .Where(p => p.Naziv.Contains(term))
                .Select(p => new { id = p.Id, name = p.Naziv, price = p.Cijena })
                .ToList();

            return Json(suggestions);
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Search))
            {
                model.Results = _context.Proizvodi
                    .Where(p => p.Naziv.Contains(model.Search) || p.Opis.Contains(model.Search))
                    .ToList();
            }
            else
            {
                model.Results = new List<Proizvod>();
            }

            return View("Index", model);
        }
    }
}
