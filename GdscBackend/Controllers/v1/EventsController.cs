using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize(Roles = "admin")]
    [Route("v1/events")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class EventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<EventModel> _repository;

        public EventsController(IRepository<EventModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EventModel>>> Get()
        {
            return Ok(Map((await _repository.GetAsync()).ToList()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventModel>> Get([FromRoute] string id)
        {
            var entity = Map(await _repository.GetAsync(id));
            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventModel>> Post(EventRequest entity)
        {
            var newEntity = await _repository.AddAsync(Map(entity));
            return Created("v1/event", newEntity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventModel>> Delete([FromRoute] string id)
        {
            var entity = Map(await _repository.DeleteAsync(id));
            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventModel>> Update(EventRequest entity)
        {
            entity = Map(await _repository.UpdateAsync(Map(entity)));
            return entity is null ? BadRequest() : Ok(entity);
        }

        private EventModel Map(EventRequest entity)
        {
            return _mapper.Map<EventModel>(entity);
        }

        private EventRequest Map(EventModel entity)
        {
            return _mapper.Map<EventRequest>(entity);
        }

        private IEnumerable<EventRequest> Map(IEnumerable<EventModel> entity)
        {
            return _mapper.Map<IEnumerable<EventRequest>>(entity);
        }

        private IEnumerable<EventModel> Map(IEnumerable<EventRequest> entity)
        {
            return _mapper.Map<IEnumerable<EventModel>>(entity);
        }
    }
}