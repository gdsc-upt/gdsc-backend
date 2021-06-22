using System.Collections.Generic;
using GdscBackend.Models;

namespace GdscBackend.RequestModels
{
    public class TeamRequest : Request
    {
        public string Name { get; set; }
    }
}