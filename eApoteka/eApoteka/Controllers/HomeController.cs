using eApoteka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eApoteka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult RegisterView()
        {
            return View();
        }

        public ActionResult PretragaView()
        {
            return View();
        }

        public ActionResult TicketView()
        {
            return View();
        }

        public ActionResult PlaceOrderView()
        {
            return View();
        }
        public ActionResult ProductDetailsView()
        {
            return View();
        }
        public ActionResult ShowOrdersView()
        {
            return View();
        }
        public ActionResult ShowOrderDetailsView()
        {
            return View();
        }
        public ActionResult PharmacistPanelView()
        {
            return View();
        }
        public ActionResult AdminPanelView()
        {
            return View();
        }
        public ActionResult ProductView()
        {
            return View();
        }
    }
}

