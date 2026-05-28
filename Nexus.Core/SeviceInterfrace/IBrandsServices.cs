using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexus.Core.Domain;
using Nexus.Core.Dto;

namespace Nexus.Core.SeviceInterfrace
{
    public interface IBrandsServices
    {
        Task<Brand> Create(BrandsDTO dto);
    }
}
