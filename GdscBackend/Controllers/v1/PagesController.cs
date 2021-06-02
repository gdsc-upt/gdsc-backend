using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize(Roles = "admin")]
    [Route("v1/pages")]
    public class PagesController : ControllerBase
    {
        private readonly IRepository<PageModel> _repository;

        public PagesController(IRepository<PageModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PageModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }


        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PageModel), StatusCodes.Status201Created)]
        public async Task<ActionResult<PageModel>> Post(PageModel entity)
        {
            entity = await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Post), new { entity.Id }, entity);
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

            return CreatedAtAction(nameof(Update), new { entity.Id }, entity);
        }
    }
}