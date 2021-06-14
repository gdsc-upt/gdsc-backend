namespace GdscBackend.RequestModels
{
    public class MemberRequest : Request
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string TeamId { get; set; }
    }
}