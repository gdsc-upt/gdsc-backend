using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.Teams;

public class TeamRequest : Request
{
    public string Name { get; set; }
}