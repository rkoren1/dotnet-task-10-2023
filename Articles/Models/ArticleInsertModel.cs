using System.ComponentModel.DataAnnotations;

namespace Articles.Models
{
    public class ArticleInsertModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool Published { get; set; }
    }
}
