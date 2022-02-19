namespace GdscBackend.RequestModels;

public class FaqRequest : Request
{
    public string Question { get; set; }

    public string Answer { get; set; }
}
