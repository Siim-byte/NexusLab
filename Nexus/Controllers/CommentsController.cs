using Microsoft.AspNetCore.Mvc;
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
            _commentsServices = _commentsServices;
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
        public IActionResult Create()
        {
            CommentsCreateViewModel result = new();
            return View("Create", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CommentsCreateViewModel vm)
        {
            var dto = new CommentsDTO()
            {
                CommentId = Guid.NewGuid(),
                EntryCreatedAt = DateTime.,
                Content = dto.Content
                
            };
        }
    }
}
