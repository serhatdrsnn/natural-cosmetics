using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NaturalCosmeticsECommerce.Models;
using NaturalCosmeticsECommerce.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace NaturalCosmeticsECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            var products = await _context.Products
                                .OrderByDescending(p => p.ProductId)
                                .Take(4) // Sadece 4 ürün göstermek istediğim için 4 yaptımmm
                                .ToListAsync();

            ViewBag.Products = products;

            return View(categories);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
