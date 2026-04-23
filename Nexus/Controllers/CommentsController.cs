using Microsoft.AspNetCore.Mvc;
using Nexus.ApplicationServices.Services;
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
        public CommentsController
            (
            ApplicationDbContext context,
            ICommentsServices commentsServices
            )
        {
            _context = context;
            _commentsServices = commentsServices;
        }
        public IActionResult Index(Guid id)
        {
            var result = _context.Comments.Where(x => x.ProductId == id).Select(x => new CommentsIndexViewModel()
            {
                CommentId = x.CommentId,
                EntryCreatedAt = x.EntryCreatedAt,
                Content = x.Content
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
            var dto = new CommentsDTO()
            {
                CommentId = Guid.NewGuid(),
                Content = vm.Content,
                ProductId = (Guid)vm.ProductId,
                EntryCreatedAt = DateTime.Now

            };
            var result = await _commentsServices.Create(dto);
            if (result != null)
            {
                return RedirectToAction(nameof(Index), new {id = vm.ProductId});
            }
            ModelState.AddModelError("", "VIGA");

            return View(vm);
        }
    }
}
