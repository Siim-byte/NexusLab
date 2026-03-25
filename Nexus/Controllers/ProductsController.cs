using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Dto;
using Nexus.Core.SeviceInterfrace;
using Nexus.Data;
using Nexus.Models.Products;

namespace Nexus.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductsServices _productsServices;
        public ProductsController
            (
            ApplicationDbContext context,
            IProductsServices productServices
            )
        {
            _context = context;
            _productsServices = productServices;
        }

        public IActionResult Index()
        {
            var result = _context.Products.Select(x => new ProductsIndexViewModel()
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Price = x.Price,
                Quality = x.Quality,
                Stock = x.Stock,
                Description = x.Description,
            });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ProductsCreateViewModel result = new();
            return View("Create", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductsCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new ProductsDTO()
                {
                    ProductId = (Guid)vm.ProductId,
                    Name = vm.Name,
                    Price = vm.Price,
                    Quality = vm.Quality,
                    Stock = vm.Stock,
                    Description = vm.Description
                };
                var result = await _productsServices.Create(dto);
                if (result == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}
