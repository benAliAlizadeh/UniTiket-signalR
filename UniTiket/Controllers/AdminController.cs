using Microsoft.AspNetCore.Mvc;

namespace UniTiket.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
