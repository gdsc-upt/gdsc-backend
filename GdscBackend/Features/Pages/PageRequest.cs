using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.Pages;

public class PageRequest : Request
{
    public string Title { get; set; }
    public string Body { get; set; }
    public bool isPublished { get; set; }
    public string Slug { get; set; }
    public string ShortDescription { get; set; }
    public string Image { get; set; }
}