using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Manage.Controllers
{
    public class HomeController : Controller
    {
        [Area("Manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
