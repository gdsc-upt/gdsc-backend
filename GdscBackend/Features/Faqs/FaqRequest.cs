using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.Faqs;

public class FaqRequest : Request
{
    public string Question { get; set; }

    public string Answer { get; set; }
}
