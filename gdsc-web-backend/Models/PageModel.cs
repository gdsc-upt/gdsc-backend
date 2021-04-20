namespace gdsc_web_backend.Models
{
    public class PageModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool isPublished { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
    }
}