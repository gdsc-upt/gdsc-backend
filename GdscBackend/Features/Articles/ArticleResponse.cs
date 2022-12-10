using GdscBackend.Auth;

namespace GdscBackend.Features.Articles;

public class ArticleResponse
{
    public DateTime Created { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public UserViewModel Author { get; set; }
}