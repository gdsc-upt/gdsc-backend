using System;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.ViewModels
{
    public abstract class ViewModel
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}