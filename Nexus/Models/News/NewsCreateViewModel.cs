using System.ComponentModel.DataAnnotations;

namespace Nexus.Models.News
{
    public class NewsCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
