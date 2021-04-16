using System.Collections.Generic;
using gdsc_web_backend.Models;
using gdsc_web_backend.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    // This marks this controller as a public one that can be called from the internet
    [ApiController]  
    // This sets the URL that we can enter to call the controller's methods
    // ex: https://localhost:5000/api/Example
    [Route("api/[controller]")]  
    public class EventController : ControllerBase
    {
        /// <summary>
        /// This method is called when someone makes a GET request for an Event
        /// </summary>
        /// <example>GET http://localhost:5000/api/Event</example>
        /// <returns>EventModel</returns>
        /// 
        public List<EventModel> EventModels = new List<EventModel>
        {
            new EventModel
            {
                Id = "1",
                Title = "example 1",
                Description = "description 1",
                Image = "image link 1"
            },
            new EventModel
            {
                Id = "1",
                Title = "example 2",
                Description = "description 2",
                Image = "image link 2"
            }
        };
        [HttpGet]
        public List<EventModel> Get()
        {
            return EventModels;
        }
        
        [HttpGet("{id}")]
        public EventModel Get(string id)
        {
            return EventModels.Find(x => x.Id == id);
        }
    }
}