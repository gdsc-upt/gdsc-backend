using GdscBackend.Models;

namespace GdscBackend.Features.Redirects;

public class RedirectModel : Model
{
    public String path { get; set; }
    
    public String redirectTo { get; set; }
}