using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using GdscBackend.Database;
using GdscBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/ideas")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class IdeasController : ControllerBase 
    {
        private readonly IRepository<IdeaModel> _repository;

        public IdeasController(IRepository<IdeaModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IdeaModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdeaModel>> Post(IdeaModel entity)
        {
            entity = await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Post), new { entity.Id }, entity);
        }   
    }
}
