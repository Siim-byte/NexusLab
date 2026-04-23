using Nexus.Core.Domain;
using Nexus.Core.Dto;
using Nexus.Core.SeviceInterfrace;
using Nexus.Data;
using Nexus.Nexus.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.ApplicationServices.Services
{
    public class CommentsServices : ICommentsServices
    {
        private readonly ApplicationDbContext _context;
        public CommentsServices(ApplicationDbContext context)
        {
               _context = context;
        }
        public async Task<Comment> Create(CommentsDTO dto)
        {
            Comment comment = new Comment();
            comment.CommentId = dto.CommentId;
            comment.Content = dto.Content;
            comment.ProductId = dto.ProductId;
            comment.EntryCreatedAt = dto.EntryCreatedAt;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}
