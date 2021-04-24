using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaqsController: ControllerBase
    {
        private readonly List<FaqModel> _mockFaq = new();
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FaqModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<FaqModel>> Get()
        {
            return Ok(_mockFaq);
        }


        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FaqModel), StatusCodes.Status201Created)]
        public ActionResult<FaqModel> Post([FromBody] FaqModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            //create a variable where we return the value of the find function applied on the _mockFaq
            var doesExist = _mockFaq.Find(model => model.Id == entity.Id);
            if (doesExist is not null)
            {
                return BadRequest(new ErrorViewModel {Message = $"{entity} already exists"});
            }

            _mockFaq.Add(entity);
            entity = _mockFaq.Find(m => m == entity);
            return Ok(entity);
        }
    }
}