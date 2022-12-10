using GdscBackend.Common.Models;

namespace GdscBackend.Features.Settings;

public class SettingModel : Model
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public SettingTypeEnum Type { get; set; }
    public bool Value { get; set; }
    public string Image { get; set; }
}