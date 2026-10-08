using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KUTTAPPAN_Midterm_Store.Data;
using KUTTAPPAN_Midterm_Store.Models;

public class ProductsController : Controller
    {
private readonly ApplicationDbContext _context;

 public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
      public async Task<IActionResult> Index()
        {
         return View(await _context.Products.ToListAsync());
        }

     public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

      
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

      
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

     
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
