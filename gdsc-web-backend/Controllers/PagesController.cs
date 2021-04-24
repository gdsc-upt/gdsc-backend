using System.Collections.Generic;
using System.Net;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagesController : ControllerBase
    {
        private readonly List<PageModel> _mockPages = new();


        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(IEnumerable<PageModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PageModel>> Get(string slug)
        {
            return Ok(_mockPages.Find(page => page.Slug == slug));
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
            var doesExist = _mockPages.Find(model => model.Id == entity.Id);
            if (doesExist is not null)
            {
                return BadRequest(new ErrorViewModel {Message = $"{entity} already exists"});
            }

            _mockPages.Add(entity);
            entity = _mockPages.Find(m => m == entity);
            return Ok(entity);
        }
    }
}