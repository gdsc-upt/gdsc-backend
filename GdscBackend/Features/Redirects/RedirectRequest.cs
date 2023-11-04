using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.Redirects;

public class RedirectRequest : Request
{
    public string Path { get; set; }
    
    public string RedirectTo { get; set; }
    
}