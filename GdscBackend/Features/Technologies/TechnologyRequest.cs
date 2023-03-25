using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.Technologies;

public class TechnologyRequest : Request
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string IconId { get; set; }
}
