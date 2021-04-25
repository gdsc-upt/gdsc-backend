using gdsc_web_backend.Models.Enums;

namespace gdsc_web_backend.Models
{
    public class ExampleModel: Model
    {
        public string Title { get; set; }
        public ExampleTypeEnum Type { get; set; }
        public int Number { get; set; }
    }
}