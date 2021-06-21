using System.Collections.Generic;

namespace GdscBackend.Models
{
    public class TeamModel : Model
    {
        public string Name { get; set; }
        public IEnumerable<MemberModel> Members { get; set; }
    }
}
