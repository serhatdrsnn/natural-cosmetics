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
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ProductController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Ürün Listesi
        public async Task<IActionResult> List(int? categoryId, string searchString)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId.HasValue)
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var lowerSearch = searchString.ToLower();
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(lowerSearch) ||
                    (p.Description != null && p.Description.ToLower().Contains(lowerSearch)));
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CurrentFilter = searchString;
            ViewBag.SelectedCategory = categoryId;

            return View(await productsQuery.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var favoriteProductIds = await _context.Favorites
                    .Where(f => f.UserId == userId)
                    .Select(f => f.ProductId)
                    .ToListAsync();

                ViewBag.FavoriteProductIds = favoriteProductIds;
            }

            return View(product);
        }

        // Yeni Ürün Oluşturma (Admin) - GET
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        // Yeni Ürün Oluşturma (Admin) - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product model)
        {
            // Resim URL kontrolü
            model.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ?
                           "/img/default.png" :
                           model.ImageUrl.Trim();

            if (ModelState.IsValid)
            {
                try
                {
                    // Kategori kontrolü
                    var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == model.CategoryId);
                    if (!categoryExists)
                    {
                        ModelState.AddModelError("CategoryId", "Geçersiz kategori seçimi");
                        ViewBag.Categories = await _context.Categories.ToListAsync();
                        return View(model);
                    }

                    _context.Products.Add(model);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"{model.Name} ürünü başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(List));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Ürün kaydedilirken bir hata oluştu: " + ex.Message);
                }
            }

            // Hata durumunda kategorileri tekrar yükle
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(model);
        }

        // Ürün Düzenleme - GET
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(product);
        }

        // Ürün Düzenleme - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            if (id != model.ProductId)
                return BadRequest();

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound();

            if (string.IsNullOrEmpty(model.ImageUrl))
                model.ImageUrl = "/img/default.png";

            if (ModelState.IsValid)
            {
                try
                {
                    existingProduct.Name = model.Name;
                    existingProduct.Description = model.Description ?? string.Empty;
                    existingProduct.Price = model.Price;
                    existingProduct.CategoryId = model.CategoryId;
                    existingProduct.StockQuantity = model.StockQuantity;
                    existingProduct.ImageUrl = model.ImageUrl;

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Ürün başarıyla güncellendi!";
                    return RedirectToAction(nameof(List));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExistsAsync(id))
                        return NotFound();
                    throw;
                }
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(model);
        }

        private async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(e => e.ProductId == id);
        }

        // Ürün Silme Onayı
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // Ürün Silme İşlemi
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Ürün başarıyla silindi.";
            }

            return RedirectToAction(nameof(List));
        }
    }
}