using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GdscBackend.Models.Enums;

namespace GdscBackend.Models
{
    public class ExampleModel : Model
    {
        [Required] public string Title { get; set; }

        [DefaultValue(ExampleTypeEnum.EasyExample)]
        public ExampleTypeEnum Type { get; set; }

        [Required] public int Number { get; set; }
    }
}