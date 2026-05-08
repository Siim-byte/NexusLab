using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Domain;
using Nexus.Core.Dto;
using Nexus.Data;
using Nexus.Models.Accounts;

namespace Nexus.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        //private readonly IEmailsServices _emailsServices; 

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AddPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            var userHasPassword = await _userManager.HasPasswordAsync(user);
            if (userHasPassword == true)
            {
                RedirectToAction("ChangePassword");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(AddPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                return View("AddPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }
            return View(model);
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user != null && await _userManager.IsEmailConfirmedAsync(user))
        //        {
        //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //            var passwordResetLink = Url.Action("ResetPassword", "Accounts", new { email = user.Email, token = token }, Request.Scheme);
        //            return RedirectToAction("ForgotPasswordConfirmation");
        //        }
        //        return RedirectToAction("ForgotPasswordConfirmation");
        //    }
        //    return View(model);
        //}


        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    if (token == null || user.Email == null)
        //    {
        //        ModelState.AddModelError("", "Invalid password, reset token");
        //    }
        //    var model = new ResetPasswordViewModel
        //    {
        //        Token = token,
        //        Email = user.Email
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user != null)
        //        {
        //            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        //            if (result.Succeeded)
        //            {
        //                if (await _userManager.IsLockedOutAsync(user))
        //                {
        //                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
        //                }
        //                await _signInManager.SignOutAsync();
        //                await _userManager.DeleteAsync(user);
        //                return RedirectToAction("ResetPasswordConfirmaton", "Accounts");
        //            }
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //            return RedirectToAction("ResetPasswordConfirmaton", "Accounts");
        //        }
        //        await _userManager.DeleteAsync(user);
        //        return RedirectToAction("ResetPasswordConfirmaton", "Accounts");
        //    }
        //    return RedirectToAction("ResetPasswordConfirmaton", "Accounts");
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult AccountCreated()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    ProfileType = model.ProfileType,
                    DisplayName = model.DisplayName,

                };
                var result = await _userManager.CreateAsync(user, model.Password);

                return RedirectToAction("Index", "Home");
            }
            return BadRequest();
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    if (userId == null || token == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var result = await _userManager.ConfirmEmailAsync(user, token);
        //    if (result.Succeeded)
        //    {
        //        return View("ConfirmEmail");
        //    }
        //    return BadRequest();
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnURL)
        {
            LoginViewModel vm = new()
            {
                ReturnUrl = returnURL,
            };

            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("", "Kasutajanimi või parool on vale");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}