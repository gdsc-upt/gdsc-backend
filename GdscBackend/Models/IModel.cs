using System;

namespace gdsc_web_backend.Models
{
    public interface IModel
    {
        string Id { get; set; }
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
