using GdscBackend.Common.Models;
using GdscBackend.Features.FIles;

namespace GdscBackend.Features.Technologies;

public class TechnologyModel : Model
{
    public string Name { get; set; }
    public string Description { get; set; }
    public FileModel Icon { get; set; }
}
