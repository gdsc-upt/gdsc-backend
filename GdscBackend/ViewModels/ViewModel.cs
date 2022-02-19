namespace GdscBackend.ViewModels;

public abstract class ViewModel
{
    public string Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}