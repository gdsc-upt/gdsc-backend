using System;

namespace GdscBackend.Models
{
    public class EventModel : Model
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
    }
}