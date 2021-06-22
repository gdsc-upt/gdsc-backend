using System.Collections.Generic;

namespace GdscBackend.Models
{
    public class TeamModel : Model
    {
        public string Name { get; set; }
        public ICollection<MemberModel> Members { get; set; }
    }
}