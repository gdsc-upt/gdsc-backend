using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/events")]
    public class EventsController : ControllerBase
    {
        public List<EventModel> EventModels = new()
        {
            new EventModel
            {
                Id = "1",
                Title = "example 1",
                Description = "description 1",
                Image = "image link 1"
            },
            new EventModel()
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