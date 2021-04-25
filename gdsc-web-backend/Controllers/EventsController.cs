using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        public List<EventModel> EventModels = new()
        {
            new()
            {
                Id = "1",
                Title = "example 1",
                Description = "description 1",
                Image = "image link 1"
            },
            new()
            {
                Id = "1",
                Title = "example 2",
                Description = "description 2",
                Image = "image link 2"
            }
        };

        // HTTP Get method without any ID, returning the whole list of events
        [HttpGet]
        public List<EventModel> Get()
        {
            return EventModels;
        }

        // HTTP Get method with a specific ID, which will return the event having that ID
        [HttpGet("{id}")]
        public EventModel Get(string id)
        {
            return EventModels.Find(eventElement => eventElement.Id == id);
        }
    }
}