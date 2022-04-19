using Microsoft.AspNetCore.Mvc;

namespace _911Medical.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
