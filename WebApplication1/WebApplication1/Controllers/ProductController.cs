using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {

        private AppDbContext _context { get; }

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            ViewBag.ProductCount = _context.Products.Where(p => p.IsDeleted == false).Count();
            ViewBag.Categories = _context.Categories.Where(p => p.IsDeleted == false).Include(c => c.Products);
            return View(_context.Products.Where(p => p.IsDeleted == false).OrderByDescending(p => p.Id).Take(10).Include(p => p.ProdutcImages).Include(p => p.Category));
        }
        public IActionResult LoadMore(int skip)
        {

            IQueryable<Product> p = _context.Products.Where(p => p.IsDeleted == false);
            int productCount = p.Count();
            if (productCount <= skip)
            {
                return Json(new
                {
                    message = "Agilli ol"
                });
            }
            return PartialView("_ProductParial", p
                                    .OrderByDescending(p => p.Id)
                                    .Skip(skip)
                                    .Take(10)
                                    .Include(p => p.ProdutcImages)
                                    .Include(p => p.Category));

            //return Json(_context.Products.Select(p=> new { p.Name, p.Id,p.IsDeleted,p.StockCount,p.Raiting,p.Price})
            //    .Where(p => p.IsDeleted == false)
            //    .OrderByDescending(p => p.Id).Skip(10).Take(10));
        }
        public IActionResult CategoryFilter(int CategoryId)
        {
            if (_context.Categories.Find(CategoryId) == null) return NotFound();
            return PartialView("_ProductParial", _context.Products.Where(p => p.IsDeleted == false && p.CategoryId == CategoryId)
                                .OrderByDescending(p => p.Id)
                                .Include(p => p.ProdutcImages)
                                .Include(p => p.Category));
        }
    }
}
