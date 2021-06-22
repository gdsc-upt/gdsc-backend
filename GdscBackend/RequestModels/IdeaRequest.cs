namespace GdscBackend.RequestModels
{
    public class IdeaRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
    }
}