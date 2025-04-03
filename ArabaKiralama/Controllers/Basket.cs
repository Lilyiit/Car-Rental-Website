using Microsoft.AspNetCore.Mvc;

namespace internetProgramcılığı.Controllers
{
    public class Basket : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}