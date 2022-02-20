namespace GdscBackend.Models;

public class MemberModel : Model
{
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<TeamModel> Teams { get; set; }
}