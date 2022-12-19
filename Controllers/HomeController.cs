using Birdnest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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