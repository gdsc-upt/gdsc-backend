using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.Events;

public class EventRequest : Request
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}