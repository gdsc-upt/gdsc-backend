namespace GdscBackend.RequestModels;

public class RedirectRequest : Request
{
    public string Path { get; set; }
    
    public string RedirectTo { get; set; }
    
}