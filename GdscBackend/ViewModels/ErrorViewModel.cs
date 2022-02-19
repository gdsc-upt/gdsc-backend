namespace GdscBackend.ViewModels;

// This model should not be added as a DbSet
// It's used only to return errors
public class ErrorViewModel
{
    public string Message { get; set; }
}