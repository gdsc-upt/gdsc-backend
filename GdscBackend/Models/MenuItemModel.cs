using GdscBackend.Models.Enums;

namespace GdscBackend.Models
{
    public class MenuItemModel : Model
    {
        public string Name { get; set; }
        public MenuItemTypeEnum Type { get; set; }
        public string Link { get; set; }
    }
}