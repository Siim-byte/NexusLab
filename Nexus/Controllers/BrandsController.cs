using Microsoft.AspNetCore.Mvc;

namespace Nexus.Controllers
{
    public class BrandsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
