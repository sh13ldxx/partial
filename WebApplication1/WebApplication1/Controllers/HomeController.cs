using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get;}
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
           HomeVM homvm = new HomeVM
           {
               Sliders = await _context.Sliders.ToListAsync(),
               Categories = await _context.Categories.ToListAsync(),
               Products = await _context.Products.Include(p => p.ProdutcImages).Include(p => p.Category).ToListAsync(),
           };return View(homvm);
        }
    }
}
