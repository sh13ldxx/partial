using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModel.Products;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get; }
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> prdcts = await _context.Products.Include(p => p.ProdutcImages).Include(p => p.Category).ToListAsync();
            List<ProductVm> productVMs = new List<ProductVm>();
            foreach (var item in prdcts)
            {
                ProductVm product = new ProductVm
                {
                    Id = item.Id,
                    Name = item.Name,
                    Category = item.Category.Name,
                    Price = item.Price,
                    Image = item.ProdutcImages.FirstOrDefault(pi => pi.IsFront == true).Image,
                    IsDeleted = item.IsDeleted
                };
                productVMs.Add(product);
            }
            return View(productVMs);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.Where(c => c.IsDeleted == false).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.Where(c => c.IsDeleted == false).ToList();
                return View();
            }
            if (_context.Products.Any(p => p.Name.Trim().ToLower() == product.Name.Trim().ToLower()))
            {
                ViewBag.Categories = _context.Categories.Where(c => c.IsDeleted == false).ToList();
                ModelState.AddModelError("Name", "This name already exist");
                return View();
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null) return NotFound();
            if (product.IsDeleted == true)
            {
                _context.Products.Remove(product);
            }
            else
            {
                product.IsDeleted = true;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {

            Product Product = _context.Products.FirstOrDefault(x => x.Id == id);
            ViewBag.Categories = _context.Categories.Where(c => c.IsDeleted == false).ToList();
            if (Product == null) return NotFound();
            return View(Product);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(Product Products)
        {
            Product LocalProduct = _context.Products.FirstOrDefault(x => x.Id == Products.Id);
            if (LocalProduct == null) return NotFound();
            LocalProduct.ProdutcImages = Products.ProdutcImages;
            LocalProduct.Price = Products.Price;
            LocalProduct.Description = Products.Description;
            LocalProduct.Name = Products.Name;
            LocalProduct.Rating = Products.Rating;
            LocalProduct.IsDeleted = Products.IsDeleted ;
            _context.SaveChanges();
            return RedirectToAction("index");
        }


    }
}
