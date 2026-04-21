namespace Nexus.Models.Comments
{
    public class CommentsCreateViewModel
    {
        public Guid? CommentId { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public string Content { get; set; }
        public Guid? ProductId { get; set; }
    }
}
