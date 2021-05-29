using System.Runtime.Serialization;

namespace GdscBackend.Models.Enums
{
    public enum MenuItemTypeEnum
    {
        [EnumMember(Value = "InternalLink")] InternalLink,
        [EnumMember(Value = "ExternalLink")] ExternalLink
    }
}