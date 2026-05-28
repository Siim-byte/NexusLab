using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.SeviceInterfrace;
using Nexus.Data;
using Nexus.Models.Brands;

namespace Nexus.Controllers
{
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BrandsController
            (
            ApplicationDbContext context
            )
        {
            _context = context;
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
    }
}
