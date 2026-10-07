using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KUTTAPPAN_Midterm_Store.Data;
using KUTTAPPAN_Midterm_Store.Models;

namespace KUTTAPPAN_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ - show the cart
        public async Task<IActionResult> Index()
        {
            var items = await _context.Cart.ToListAsync();
            return View(items);
        }

        // CREATE - add product to cart
        [HttpPost]
        public async Task<IActionResult> Add(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            // if already in cart, just add 1 to quantity
            var existing = await _context.Cart
                .FirstOrDefaultAsync(c => c.ProductId == productId);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                var cart= new Cart
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                };
                _context.Cart.Add(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // UPDATE - change quantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            var item = await _context.Cart.FindAsync(id);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // DELETE - remove item
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.Cart.FindAsync(id);
            if (item != null)
            {
                _context.Cart.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}