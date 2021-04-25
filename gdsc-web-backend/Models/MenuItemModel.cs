using gdsc_web_backend.Models.Enums;

namespace gdsc_web_backend.Models
{
    public class MenuItemModel: Model
    {
        public string Name { get; set; }
        public MenuItemTypeEnum Type { get; set; }
        public string Link { get; set; }
    }
}