using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Manage.Controllers
{
        [Area("Manage")]
    public class CategoryController : Controller
    {
        private AppDbContext _context { get; }
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories.Include(c => c.Products).ToListAsync();
            return View(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (_context.Categories.FirstOrDefault(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim()) != null)
            {
                return RedirectToAction(nameof(Index));
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

