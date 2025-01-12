using System.ComponentModel.DataAnnotations;

namespace ElasticSearch.Web.ViewModel
{
    public class CreateBlogViewModel
    {

        [Display(Name = "Blog Title")]
        [Required]
        public string Title { get; set; } = null!;

        [Display(Name = "Blog Content")]
        [Required]
        public string Content { get; set; } = null!;

        [Display(Name = "Blog Tags")]
        [Required]
        public string[] Tags { get; set; } = null!;

        public Guid UserId { get; set; } = Guid.NewGuid();

    }
}
