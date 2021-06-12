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
        private readonly IRepository<TechnologyModel> _repository;

        public TechnologiesController(IRepository<TechnologyModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TechnologyModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TechnologyRequest>> Post(TechnologyRequest entity)
        {
            var newentity = await _repository.AddAsync(Map(entity));
            return CreatedAtAction(nameof(Post), new {newentity.Id}, newentity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TechnologyModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);
            return entity is null ? NotFound() : Ok(entity);
        }
        
        protected static TechnologyModel Map(TechnologyRequest entity)
        {
            return Mapper.Map<TechnologyModel>(entity);
        }

    }

}