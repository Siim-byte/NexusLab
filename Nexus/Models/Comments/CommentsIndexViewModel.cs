namespace Nexus.Models.Comments
{
    public class CommentsIndexViewModel
    {
        public Guid CommentId { get; set; }
        public DateTime EntryCreatedAt { get; set; }
        public string Content { get; set; }
    }
}
