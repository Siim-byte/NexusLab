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

        public async Task<IActionResult> Index(string sortOrder)
        {
            var news = _context.News.AsQueryable();

            switch (sortOrder)
            {
                case "date_asc":
                    news = news.OrderBy(x => x.CreatedAt);
                    break;

                case "date_desc":
                    news = news.OrderByDescending(x => x.CreatedAt);
                    break;

                case "title_asc":
                    news = news.OrderBy(x => x.Title);
                    break;

                case "title_desc":
                    news = news.OrderByDescending(x => x.Title);
                    break;
            }

            var model = await news
                .Select(x => new NewsIndexViewModel
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    Content = x.Content,
                    CreatedAt = x.CreatedAt,
                    Likes = x.Likes,
                })
                .ToListAsync();

            return View(model);
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
