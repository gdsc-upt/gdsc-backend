using GdscBackend.Common.Models;
using GdscBackend.Features.Members;

namespace GdscBackend.Features.Teams;

public class TeamModel : Model
{
    public string Name { get; set; }
    public ICollection<MemberModel> Members { get; set; }
}