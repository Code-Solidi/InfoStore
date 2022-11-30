using Microsoft.AspNetCore.Mvc;

namespace InfoStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
