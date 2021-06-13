using GdscBackend.Models.Enums;

namespace GdscBackend.RequestModels
{
    public class SettingRequest : Request
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public SettingTypeEnum Type { get; set; }
        public bool Value { get; set; }
        public string Image { get; set; }
    }
}