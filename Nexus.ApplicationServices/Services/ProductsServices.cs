using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexus.Core.Dto;
using Nexus.Core.SeviceInterfrace;
using Nexus.Data;
using Nexus.Nexus.Core.Domain;

namespace Nexus.ApplicationServices.Services
{
    public class ProductsServices : IProductsServices
    {
        private readonly ApplicationDbContext _context;
        public ProductsServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> Create(ProductsDTO dto)
        {
            Product product = new Product();
            product.ProductId = Guid.NewGuid();
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Quality = dto.Quality;
            product.Stock = dto.Stock;
            product.Description = dto.Description;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }
        
    }
}
