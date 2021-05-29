using System.Collections.Generic;

namespace GdscBackend.Auth
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
