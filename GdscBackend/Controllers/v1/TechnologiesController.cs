using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/technologies")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class TechnologiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TechnologyModel> _repository;

        public TechnologiesController(IRepository<TechnologyModel> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TechnologyModel>>> Get()
        {
            return Ok(Map((await _repository.GetAsync()).ToList()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TechnologyModel>> Post(TechnologyRequest entity)
        {
            var newEntity = await _repository.AddAsync(Map(entity));
            return Created("v1/technology", newEntity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TechnologyModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);
            return entity is null ? NotFound() : Ok(Map(entity));
        }

        private TechnologyModel Map(TechnologyRequest entity)
        {
            return _mapper.Map<TechnologyModel>(entity);
        }
        private TechnologyRequest Map(TechnologyModel entity)
        {
            return _mapper.Map<TechnologyRequest>(entity);
        }
        private IEnumerable<TechnologyRequest> Map(IEnumerable<TechnologyModel> entity)
        {
            return _mapper.Map<IEnumerable<TechnologyRequest>>(entity);
        }
        private IEnumerable<TechnologyModel> Map(IEnumerable<TechnologyRequest> entity)
        {
            return _mapper.Map<IEnumerable<TechnologyModel>>(entity);
        }
    }
}
