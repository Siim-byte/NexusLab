using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Domain;
using Nexus.Data;
using Nexus.Models.News;

namespace Nexus.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NewsController
        (
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var result = _context.News
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new NewsIndexViewModel()
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    Content = x.Content,
                    CreatedAt = x.CreatedAt
                })
                .ToList();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || !user.ProfileType)
            {
                return Forbid();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsCreateViewModel vm)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || !user.ProfileType)
            {
                return Forbid();
            }

            News news = new()
            {
                NewsId = Guid.NewGuid(),
                Title = vm.Title,
                Content = vm.Content,
                CreatedAt = DateTime.Now
            };

            _context.News.Add(news);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || !user.ProfileType)
            {
                return Forbid();
            }

            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return View(new NewsDeleteViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                Content = news.Content
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(NewsDeleteViewModel vm)
        {
            var news = await _context.News.FindAsync(vm.NewsId);

            _context.News.Remove(news);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return View(new NewsUpdateViewModel()
            {
                NewsId = news.NewsId,
                Title = news.Title,
                Content = news.Content
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(NewsUpdateViewModel vm)
        {
            var news = await _context.News.FindAsync(vm.NewsId);

            news.Title = vm.Title;
            news.Content = vm.Content;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}