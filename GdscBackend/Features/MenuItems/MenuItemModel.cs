using GdscBackend.Common.Models;

namespace GdscBackend.Features.MenuItems;

public class MenuItemModel : Model
{
    public string Name { get; set; }
    public MenuItemTypeEnum Type { get; set; }
    public string Link { get; set; }
}