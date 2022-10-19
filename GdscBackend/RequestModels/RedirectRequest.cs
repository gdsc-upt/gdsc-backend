namespace GdscBackend.RequestModels;

public class RedirectRequest : Request
{
    public String path { get; set; }
    
    public String redirectTo { get; set; }
    
}