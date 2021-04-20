using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    // This marks this controller as a public one that can be called from the internet
    [ApiController]
    // This sets the URL that we can enter to call the controller's methods
    // ex: https://localhost:5000/api/Example
    [Route("api/[controller]")]
    [Consumes("application/json")] // specifies which type of data this controller accepts
    [Produces("application/json")] // specifies which type of data this conrtoller returns
    public class ExamplesController : ControllerBase
    {
        private readonly List<ExampleModel> _mockExamples = new();

        /// <summary>
        /// This method is called when someone makes a GET request
        /// </summary>
        /// <example>GET http://localhost:5000/api/Example</example>
        /// <returns><code>List of ExampleModel</code></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExampleModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ExampleModel>> Get()
        {
            return Ok(_mockExamples);
        }

        /// <summary>
        /// This method is called when someone makes a POST request with a new ExampleModel in body
        /// </summary>
        /// <example>POST http://localhost:5000/api/Example</example>
        /// <returns>ExampleModel</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExampleModel), StatusCodes.Status201Created)]
        public ActionResult<ExampleModel> Post([FromBody] ExampleModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            var existing = _mockExamples.Find(e => e.Id == entity.Id);
            if (existing != null)
            {
                return BadRequest(new ErrorViewModel {Message = "An object with the same ID already exists"});
            }

            _mockExamples.Add(entity);
            entity = _mockExamples.Find(example => example == entity);

            return Created("api/Examples/" + entity!.Id, entity);
        }
    }
}