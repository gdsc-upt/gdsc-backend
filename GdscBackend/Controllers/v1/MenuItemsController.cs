using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using gdsc_web_backend.Database;
using gdsc_web_backend.Models;
using gdsc_web_backend.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/menu-items")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class MenuItemsController : ControllerBase
    {
        private readonly IRepository<MenuItemModel> _repository;

        public MenuItemsController(IRepository<MenuItemModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<MenuItemModel>> Get([FromRoute] string id)
        {
            var entity = await _repository.GetAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MenuItemModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<MenuItemModel>> Post(MenuItemModel entity)
        {
            entity = await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Post), new {entity.Id}, entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MenuItemModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MenuItemModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);
            
            return entity is null ? NotFound() : Ok(entity);
        }
    }
}