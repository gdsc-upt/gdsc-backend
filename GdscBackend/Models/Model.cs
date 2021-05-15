using System;

namespace gdsc_web_backend.Models
{
    public abstract class Model : IModel
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
