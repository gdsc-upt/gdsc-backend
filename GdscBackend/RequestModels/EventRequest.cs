namespace GdscBackend.RequestModels
{
    public class EventRequest : Request
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}