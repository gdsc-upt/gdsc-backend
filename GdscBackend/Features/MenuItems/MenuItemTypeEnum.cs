using System.Runtime.Serialization;

namespace GdscBackend.Features.MenuItems;

public enum MenuItemTypeEnum
{
    [EnumMember(Value = "InternalLink")] InternalLink,
    [EnumMember(Value = "ExternalLink")] ExternalLink
}