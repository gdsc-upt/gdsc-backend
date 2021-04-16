using System.IO;
using gdsc_web_backend.Models.Enums;

namespace gdsc_web_backend.Models
{
    public class SettingModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public SettingTypeEnum Type { get; set; }
        public bool Value { get; set; }
        public string Image { get; set; }
    }
}