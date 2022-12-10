using GdscBackend.Common.Models;

namespace GdscBackend.Features.FIles;

public class FileModel : Model
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }
}