using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.ApplicationServices.Services;
using Nexus.Core.Domain;
using Nexus.Core.Dto;
using Nexus.Core.SeviceInterfrace;
using Nexus.Data;
using Nexus.Models.Comments;

namespace Nexus.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommentsServices _commentsServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentsController
            (
            ApplicationDbContext context,
            ICommentsServices commentsServices,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _commentsServices = commentsServices;
            _userManager = userManager;
        }
        public IActionResult Index(Guid id)
        {
            var result = _context.Comments.Where(x => x.ProductId == id).Select(x => new CommentsIndexViewModel()
            {
                CommentId = x.CommentId,
                EntryCreatedAt = x.EntryCreatedAt,
                Content = x.Content,
                AuthorName = x.AuthorName,
                UserId = x.UserId
            }).ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create(Guid productId)
        {
            CommentsCreateViewModel result = new()
            {
                ProductId = productId
            };
            return View("Create", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CommentsCreateViewModel vm)
        {
            var user = await _userManager.GetUserAsync(User);
            var dto = new CommentsDTO()
            {
                CommentId = Guid.NewGuid(),
                Content = vm.Content,
                ProductId = (Guid)vm.ProductId,
                EntryCreatedAt = DateTime.Now,
                AuthorName = user.DisplayName,
                UserId = _userManager.GetUserId(User),
            };
            var result = await _commentsServices.Create(dto);
            if (result != null)
            {
                return RedirectToAction(nameof(Index), new { id = vm.ProductId });
            }
            ModelState.AddModelError("", "VIGA");

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _commentsServices.DetailsAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            var vm = new CommentsDeleteViewModel()
            {
                CommentId = result.CommentId,
                Content = result.Content,
                ProductId = result.ProductId
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(CommentsDeleteViewModel vm)
        {
            var result = await _commentsServices.Delete(vm.CommentId);
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index), new { id = vm.ProductId });
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var comment = await _commentsServices.DetailsAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var vm = new CommentsUpdateViewModel()
            {
                CommentId = comment.CommentId,
                Content = comment.Content,
                ProductId = comment.ProductId
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CommentsUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }    
            {
                var dto = new CommentsDTO()
                {
                    CommentId = vm.CommentId,
                    Content = vm.Content,
                    ProductId = vm.ProductId,
                };
                var result = await _commentsServices.Update(dto);
                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index), new { id = vm.ProductId });
            }
        }
    }
}