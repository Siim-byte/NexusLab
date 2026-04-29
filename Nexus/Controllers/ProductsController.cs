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
                Upvotes = x.Upvotes,
                Downvotes = x.Downvotes,
            }).ToList();
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
            //if (ModelState.IsValid)
            //{
                var dto = new ProductsDTO()
                {
                    ProductId = Guid.NewGuid(),
                    Name = vm.Name,
                    Price = vm.Price,
                    Quality = vm.Quality,
                    Stock = vm.Stock,
                    Description = vm.Description
                };
                var result = await _productsServices.Create(dto);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "VIGA");
            //}
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productsServices.DetailsAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            

            var vm = new ProductsDeleteViewModel();
            vm.ProductId = product.ProductId;
            vm.Name = product.Name;
            vm.Price = product.Price;
            vm.Quality = product.Quality;
            vm.Stock = product.Stock;
            vm.Description = product.Description;

            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid ProductId)
        {
            var product = await _productsServices.Delete(ProductId);
            if (product == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productsServices.DetailsAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var vm = new ProductsDetailsViewModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Quality = product.Quality,
                Stock = product.Stock,
                Description = product.Description,
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var product = await _productsServices.DetailsAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var vm = new ProductUpdateViewModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Quality = product.Quality,
                Stock = product.Stock,
                Description = product.Description,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new ProductsDTO()
                {
                    ProductId = vm.ProductId,
                    Name = vm.Name,
                    Price = vm.Price,
                    Quality = vm.Quality,
                    Stock = vm.Stock,
                    Description = vm.Description
                };
                var result = await _productsServices.Update(dto);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "VIGA");
            }
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upvote(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                // See rida garanteerib, et Votes suureneb, isegi kui see on andmebaasis 0
                product.Upvotes++;

                _context.Update(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Downvote(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.Downvotes++;

                _context.Update(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
