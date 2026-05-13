using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Domain;
using Nexus.Models.Customers;

namespace Nexus.Controllers
{
    public class CustomersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.ProfileType)
            {
                return Forbid();
            }

            var users = _userManager.Users.Select(x => new CustomersIndexViewModel
            {
                Id = x.Id,
                Email = x.Email,
                DisplayName = x.DisplayName,
                ProfileType = x.ProfileType
            }).ToList();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.ProfileType)
            {
                return Forbid();
            }

            var user1 = await _userManager.FindByIdAsync(id);

            if (user1 == null)
            {
                return NotFound();
            }

            return View(user1);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.ProfileType)
            {
                return Forbid();
            }

            var user1 = await _userManager.FindByIdAsync(id);

            if (user1 == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user1);

            return RedirectToAction(nameof(Index));
        }
    }
}
