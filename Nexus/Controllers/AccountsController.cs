using Microsoft.AspNetCore.Mvc;

namespace Nexus.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
