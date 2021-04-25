namespace gdsc_web_backend.Models
{
    public class  ContactModel : Model
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}