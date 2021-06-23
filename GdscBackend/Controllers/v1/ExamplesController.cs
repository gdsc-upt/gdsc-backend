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
    // This marks this controller as a public one that can be called from the internet
    [ApiController]
    [ApiVersion("1")]
    // This sets the URL that we can enter to call the controller's methods
    // ex: https://localhost:5000/v1/examples
    [Route("v1/examples")]
    [Consumes(MediaTypeNames.Application.Json)] // specifies which type of data this controller accepts
    [Produces(MediaTypeNames.Application.Json)] // specifies which type of data this controller returns
    public class ExamplesController : ControllerBase
    {
        private readonly IRepository<ExampleModel> _repository;

        public ExamplesController(IRepository<ExampleModel> repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///     This method is called when someone makes a GET request
        /// </summary>
        /// <example>GET http://localhost:5000/v1/examples</example>
        /// <returns>
        ///     List of ExampleModel
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ExampleModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }

        /// <summary>
        ///     This method is called when someone makes a GET request with an Id
        /// </summary>
        /// <example>GET http://localhost:5000/v1/examples/1</example>
        /// <returns>
        ///     ExampleModel
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExampleModel>> Get([FromRoute] string id)
        {
            var entity = await _repository.GetAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }

        /// <summary>
        ///     This method is called when someone makes a POST request with a new ExampleModel in body
        /// </summary>
        /// <example>POST http://localhost:5000/v1/examples</example>
        /// <returns>ExampleModel</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExampleModel>> Post(ExampleModel entity)
        {
            entity = await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Post), new { entity.Id }, entity);
        }

        /// <summary>
        ///     This method is called when someone makes a DELETE request
        ///     with the Id of the entity that he wants to remove
        /// </summary>
        /// <example>DELETE http://localhost:5000/v1/examples/1</example>
        /// <returns>ExampleModel</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExampleModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }
    }
}