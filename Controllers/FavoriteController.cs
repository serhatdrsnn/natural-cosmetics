using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalCosmeticsECommerce.Data;
using NaturalCosmeticsECommerce.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NaturalCosmeticsECommerce.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public FavoriteController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Favorite
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var favorites = await _context.Favorites
                .Include(f => f.Product)
                .Where(f => f.UserId == userId)
                .ToListAsync();

            return View(favorites);
        }

        // POST: /Favorite/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, string returnUrl = null)
        {
            var userId = _userManager.GetUserId(User);
            var exists = await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.ProductId == productId);

            if (!exists)
            {
                _context.Favorites.Add(new Favorite { UserId = userId, ProductId = productId });
                await _context.SaveChangesAsync();
                TempData["Message"] = "Product added to favorites.";
            }
            else
            {
                TempData["Message"] = "Product is already in your favorites.";
            }

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("List", "Product");
        }

        // POST: /Favorite/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int productId, string returnUrl = null)
        {
            var userId = _userManager.GetUserId(User);
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Product removed from favorites.";
            }

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }
    }
}
