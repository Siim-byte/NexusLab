using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.Models;
using Nexus.Models.News;
using System.Diagnostics;

namespace Nexus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var news = _context.News
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .Select(x => new NewsIndexViewModel
            {
                NewsId = x.NewsId,
                Title = x.Title,
                Content = x.Content,
                CreatedAt = x.CreatedAt
            })
            .ToList();

            return View(news);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
