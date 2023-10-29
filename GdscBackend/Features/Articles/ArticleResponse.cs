namespace GdscBackend.Features.Articles;

public class ArticleResponse
{
    public string Id { get; set; }
    public DateTime Created { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public string AuthorId { get; set; }
}