using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nexus.Core.Domain;
using Nexus.Core.Dto.AccountsDTOs;
using Nexus.Core.SeviceInterfrace;

namespace Nexus.ApplicationServices.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailsServices _emailsServices;

        public AccountServices(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> Register(ApplicationUserDTO userDTO)
        {
            var user = new ApplicationUser
            {
                UserName = userDTO.Email,
                Email = userDTO.Email,
                ProfileType = userDTO.ProfileType,
                DisplayName = userDTO.DisplayName,

            };
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            //if (result.Succeeded)
            //{
            //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //}
            return user;
        }

        //public async Task<ApplicationUser> Login(LoginDTO userDTO)
        //{
        //    var user = await _userManager.FindByEmailAsync(userDTO.Email);
        //    return user;
        //}
    }
}
