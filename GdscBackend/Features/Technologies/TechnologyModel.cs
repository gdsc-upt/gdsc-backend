using GdscBackend.Common.Models;

namespace GdscBackend.Features.Technologies;

public class TechnologyModel : Model
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}