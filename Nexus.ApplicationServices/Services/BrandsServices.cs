using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexus.Core.Domain;
using Nexus.Core.Dto;
using Nexus.Core.SeviceInterfrace;
using Nexus.Data;
using Nexus.Nexus.Core.Domain;

namespace Nexus.ApplicationServices.Services
{
    public class BrandsServices : IBrandsServices
    {
        private readonly ApplicationDbContext _context;
        public BrandsServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Brand> Create(BrandsDTO dto)
        {
            Brand brand = new Brand();
            brand.Id = Guid.NewGuid();
            brand.Name = dto.Name;
            brand.Slogan = dto.Slogan;

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return brand;
        }
    }
}
