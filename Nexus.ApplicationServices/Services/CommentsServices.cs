using Microsoft.EntityFrameworkCore;
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
            comment.CommentId = Guid.NewGuid();
            comment.Content = dto.Content;
            comment.ProductId = dto.ProductId;
            comment.EntryCreatedAt = dto.EntryCreatedAt;
            comment.AuthorName = dto.AuthorName;
            comment.UserId = dto.UserId;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
        public async Task<Comment> DetailsAsync(Guid id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
            return comment;
        }
        public async Task<Comment> Delete(Guid id)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(m => m.CommentId == id);
            if (result == null)
            {
                return null;
            }
            _context.Comments.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
        public async Task<Comment> Update(CommentsDTO dto)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == dto.CommentId);
            if (comment == null)
            {
                return null;
            }
            comment.Content = dto.Content;
            comment.ProductId = dto.ProductId;
            comment.EntryCreatedAt = dto.EntryCreatedAt;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
