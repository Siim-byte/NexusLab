namespace Nexus.Models.News
{
    public class NewsIndexViewModel
    {
        public Guid NewsId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }
        public string ShareUrl { get; set; }
    }
}
