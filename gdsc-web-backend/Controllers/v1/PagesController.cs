using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gdsc_web_backend.Database;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageModel = gdsc_web_backend.Models.PageModel;

namespace gdsc_web_backend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/pages")]
    public class PagesController : ControllerBase
    {
        private readonly IRepository<PageModel> _repository;

        public PagesController(IRepository<PageModel> repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PageModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }

        // [HttpGet("{slug}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<ActionResult<PageModel>> Get([FromRoute] string slug)
        // {
        //     var entity = await _repository.GetAsync(slug);
        //
        //     return entity is null ? NotFound() : Ok(entity);
        // }
        
        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PageModel), StatusCodes.Status201Created)]
        public async Task<ActionResult<PageModel>> Post(PageModel entity)
        {
            entity = await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Post), new {entity.Id}, entity);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PageModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MenuItemModel>> Update(PageModel entity)
        {
            entity = await _repository.UpdateAsync(entity);

            return CreatedAtAction(nameof(Update), new {entity.Id}, entity);
        }
    }
}