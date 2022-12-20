using Microsoft.AspNetCore.Mvc;

namespace Birdnest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}