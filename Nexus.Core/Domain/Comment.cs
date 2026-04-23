namespace Nexus.Core.Domain
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public DateTime EntryCreatedAt { get; set; }
        public string Content { get; set; }
        public Guid ProductId { get; set; }
    }
}
