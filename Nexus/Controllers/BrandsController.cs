using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.ApplicationServices.Services;
using Nexus.Core.Dto;
using Nexus.Core.SeviceInterfrace;
using Nexus.Data;
using Nexus.Models.Brands;

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
            //if (ModelState.IsValid)
            //{
            var dto = new BrandsDTO()
            {
                Id = Guid.NewGuid(),
                Name = vm.Name,
                Slogan = vm.Slogan
            };
            var result = await _brandsServices.Create(dto);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "VIGA");
            //}
            return RedirectToAction(nameof(Index));
        }
    }
}
