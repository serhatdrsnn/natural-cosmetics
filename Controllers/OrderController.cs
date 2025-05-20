using Microsoft.AspNetCore.Mvc;
using NaturalCosmeticsECommerce.Data;
using NaturalCosmeticsECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Security.Claims;

namespace NaturalCosmeticsECommerce.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: /Order/ConfirmOrder
        [HttpGet]
        public async Task<IActionResult> ConfirmOrder()
        {
            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var cartItems = await _context.CartItems
                .Where(c => c.OrderId == null && c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
                return RedirectToAction("Index", "Cart");

            var total = cartItems.Sum(item => item.Product.Price * item.Quantity);

            var model = new Order
            {
                ShippingAddress = "",
                TotalAmount = total,
                Items = cartItems
            };

            return View("~/Views/Cart/Checkout.cshtml", model);
        }

        // POST: /Order/ConfirmOrder 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder(Order model)
        {
            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var cartItems = await _context.CartItems
                .Where(c => c.OrderId == null && c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
                return RedirectToAction("Index", "Cart");

            // Toplam tutarı tekrar hesapla (güvenlik için formdan değil db'den)
            var total = cartItems.Sum(item => item.Product.Price * item.Quantity);

            var order = new Order
            {
                UserId = userId,
                ShippingAddress = model.ShippingAddress,
                TotalAmount = total,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Sipariş IDsi oluşturulduktan sonra CartItemları güncelle:
            foreach (var item in cartItems)
            {
                item.OrderId = order.OrderId;
                _context.CartItems.Update(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmation", new { id = order.OrderId });
        }

        // Sipariş Onay Sayfası: Sipariş detaylarını göster
        public async Task<IActionResult> Confirmation(int id)
        {
            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id && o.UserId == userId);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // Sipariş Geçmişi: Kullanıcının tüm siparişlerini listeler
        public async Task<IActionResult> History()
        {
            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .ThenInclude(ci => ci.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }
        // Sipariş Durumu Güncelleme (Admin Yetkili)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = newStatus;
            await _context.SaveChangesAsync();

            return RedirectToAction("History");
        }
    }
}
