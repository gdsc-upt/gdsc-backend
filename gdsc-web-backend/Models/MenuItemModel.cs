using gdsc_web_backend.Models.Enums;

namespace gdsc_web_backend.Models
{
    public class MenuItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TypeEnum Type { get; set; }
        public string Link { get; set; }
    }
}