namespace GdscBackend.Models
{
    public class FileModel : Model
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
    }
}