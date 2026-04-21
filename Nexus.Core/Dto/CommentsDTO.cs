using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Core.Dto
{
    public class CommentsDTO
    {
        public Guid CommentId { get; set; }
        public DateTime EntryCreatedAt { get; set; }
        public string Content { get; set; }
        public Guid ProductId { get; set; }
    }
}
