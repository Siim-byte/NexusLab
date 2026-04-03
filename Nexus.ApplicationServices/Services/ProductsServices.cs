using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Product> DetailsAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            return product;
        }
        public async Task<Product> Delete(Guid id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (result == null)
            {
                return null;
            }
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
        public async Task<Product> Update(ProductsDTO dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == dto.ProductId);
            if (product == null)
            {
                return null;
            }
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Quality = dto.Quality;
            product.Stock = dto.Stock;
            product.Description = dto.Description;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }
}
