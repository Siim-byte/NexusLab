using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.ApplicationServices.Services;
using Nexus.Core.Dto;
using Nexus.Core.SeviceInterfrace;
using Nexus.Data;
using System.IO;
using Nexus.Models.Brands;
using Nexus.Models.Products;

namespace Nexus.Controllers
{
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBrandsServices _brandsServices;
        public BrandsController
            (
            ApplicationDbContext context,
            IBrandsServices IBrandsServices
            )
        {
            _context = context;
            _brandsServices = IBrandsServices;
        }
        public IActionResult Index()
        {
            var result = _context.Brands.Select(x => new BrandsIndexViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Slogan = x.Slogan
            }).ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            BrandsCreateViewModel result = new();
            return View("Create", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandsCreateViewModel vm)
        {
            var brandId = Guid.NewGuid();
            if (vm.LogoFile != null && vm.LogoFile.Length > 0)
            {
                // Küsime veebiserverilt otse käigu pealt wwwroot kausta ametliku asukoha
                var env = (IWebHostEnvironment)HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment));

                if (env != null)
                {
                    string uploadsFolder = Path.Combine(env.WebRootPath, "uploads", "logos");

                    // Kui kausta pole olemas, teeme selle automaatselt veebijuurikasse
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Salvestame faili alati .jpg laiendiga
                    string fileName = $"{brandId}.jpg";
                    string absoluteFilePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(absoluteFilePath, FileMode.Create))
                    {
                        await vm.LogoFile.CopyToAsync(fileStream);
                    }
                }
            }
            var dto = new BrandsDTO()
            {
                Id = brandId,
                Name = vm.Name,
                Slogan = vm.Slogan,
                LogoFile = vm.LogoFile
            };
            var result = await _brandsServices.Create(dto);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "VIGA");
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var brand = await _brandsServices.DetailsAsync(id);

            if (brand == null)
            {
                return NotFound();
            }


            var vm = new BrandsDeleteViewModel();
            vm.Id = brand.Id;
            vm.Name = brand.Name;
            vm.Slogan = brand.Slogan;

            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid Id)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string wwwrootPath = Path.GetFullPath(Path.Combine(baseDir, "../../../wwwroot"));
                string uploadsFolder = Path.Combine(wwwrootPath, "uploads", "logos");

                if (Directory.Exists(uploadsFolder))
                {
                    var fileToDelete = Directory.GetFiles(uploadsFolder, $"{Id}.*").FirstOrDefault();

                    if (fileToDelete != null && System.IO.File.Exists(fileToDelete))
                    {
                        System.IO.File.Delete(fileToDelete);
                    }
                }
            }
            catch (Exception)
            {

            }
            var brand = await _brandsServices.Delete(Id);
            if (brand == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var brand = await _brandsServices.DetailsAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            var vm = new BrandsDetailsViewModel()
            {
                Id = brand.Id,
                Name = brand.Name,
                Slogan = brand.Slogan
            };

            return View(vm);
        }
    }
}
