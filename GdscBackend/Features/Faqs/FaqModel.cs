using GdscBackend.Common.Models;

namespace GdscBackend.Features.Faqs;

public class FaqModel : Model
{
    public string Question { get; set; }
    public string Answer { get; set; }
}