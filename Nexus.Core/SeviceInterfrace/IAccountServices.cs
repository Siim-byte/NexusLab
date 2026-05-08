using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexus.Core.Domain;
using Nexus.Core.Dto.AccountsDTOs;

namespace Nexus.Core.SeviceInterfrace
{
    public interface IAccountServices
    {
        Task<ApplicationUser> Register(ApplicationUserDTO userDTO);
        Task<ApplicationUser> Login(LoginDTO userDTO);
    }
}
