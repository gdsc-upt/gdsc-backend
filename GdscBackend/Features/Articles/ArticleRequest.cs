using System.ComponentModel.DataAnnotations;

namespace GdscBackend.Features.Articles;

public class ArticleRequest
{
    [Required]public string Title { get; set; }
    
    public string Content { get; set; }
    
    [Required]public string AuthorId { get; set; }
}