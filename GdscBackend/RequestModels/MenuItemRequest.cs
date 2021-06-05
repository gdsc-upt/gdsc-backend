using GdscBackend.Models.Enums;

namespace GdscBackend.RequestModels
{
    public class MenuItemRequest : Request
    {
        public string Name { get; set; }
        public MenuItemTypeEnum Type { get; set; }
        public string Link { get; set; }
    }
}