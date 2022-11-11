using GdscBackend.Models;

namespace GdscBackend.Features.Redirects;

public class RedirectModel : Model
{
    public string Path { get; set; }
    
    public string RedirectTo { get; set; }
}