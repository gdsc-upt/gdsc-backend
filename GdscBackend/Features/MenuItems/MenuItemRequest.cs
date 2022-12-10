using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.MenuItems;

public class MenuItemRequest : Request
{
    public string Name { get; set; }
    public MenuItemTypeEnum Type { get; set; }
    public string Link { get; set; }
}