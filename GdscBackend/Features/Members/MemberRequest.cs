using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.Members;

public class MemberRequest : Request
{
    public string Name { get; set; }

    public string Email { get; set; }
    public string[] TeamsIds { get; set; }
}