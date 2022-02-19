using GdscBackend.Models.Enums;

namespace GdscBackend.Models;

public class SettingModel : Model
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public SettingTypeEnum Type { get; set; }
    public bool Value { get; set; }
    public string Image { get; set; }
}