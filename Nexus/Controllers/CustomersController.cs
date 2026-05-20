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
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || !currentUser.ProfileType)
            {
                return Forbid();
            }

            var userToDelete = await _userManager.FindByIdAsync(id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            
            if (currentUser.Id == userToDelete.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            
            if (userToDelete.ProfileType)
            {
                return RedirectToAction(nameof(Index));
            }

            var vm = new CustomersDeleteViewModel()
            {
                Id = userToDelete.Id,
                DisplayName = userToDelete.DisplayName,
                Email = userToDelete.Email
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(CustomersDeleteViewModel vm)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || !currentUser.ProfileType)
            {
                return Forbid();
            }

            var userToDelete = await _userManager.FindByIdAsync(vm.Id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            if (currentUser.Id == userToDelete.Id)
            {
                return Forbid();
            }
  
            if (userToDelete.ProfileType)
            {
                return Forbid();
            }

            await _userManager.DeleteAsync(userToDelete);

            return RedirectToAction(nameof(Index));
        }
    }
}
