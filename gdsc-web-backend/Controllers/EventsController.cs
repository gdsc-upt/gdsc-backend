using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class EventsController : ControllerBase
    {
        public List<EventModel> EventModels = new();

        // HTTP Get method without any ID, returning the whole list of events
        [HttpGet]
        public ActionResult<List<EventModel>> Get()
        {
            return Ok(EventModels);
        }
        
        // HTTP Get method with a specific ID, which will return the event having that ID
        [HttpGet("{id}")]
        public EventModel Get(string id)
        {
            return EventModels.Find(eventElement => eventElement.Id == id);
        }
        
        // HTTP Post method
        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EventModel), StatusCodes.Status201Created)]
        public ActionResult<ContactModel> Post([FromBody] EventModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            // See if the entity given does already exist
            var doesExist = EventModels.Find(element => element.Id == entity.Id);
            if (doesExist != null)
            {
                return BadRequest(new ErrorViewModel {Message = $"{entity} already exists"});
            }

            EventModels.Add(entity);

            entity = EventModels.Find(e => e == entity);
            return Created("api/Event/" + entity!.Id, entity);
        }
    }
}