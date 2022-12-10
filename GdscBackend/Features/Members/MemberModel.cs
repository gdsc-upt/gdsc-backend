using GdscBackend.Common.Models;
using GdscBackend.Features.Teams;

namespace GdscBackend.Features.Members;

public class MemberModel : Model
{
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<TeamModel> Teams { get; set; }
}