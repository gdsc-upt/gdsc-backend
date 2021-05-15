using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/pages")]
    public class PagesController : ControllerBase
    {
        private readonly List<PageModel> _mockFaq = new();


        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(IEnumerable<PageModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PageModel>> Get(string slug)
        {
            return Ok(_mockFaq.Find(page => page.Slug == slug));
        }


        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PageModel), StatusCodes.Status201Created)]
        public ActionResult<PageModel> Post([FromBody] PageModel entity)
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
