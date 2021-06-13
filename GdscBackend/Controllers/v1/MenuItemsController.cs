using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize(Roles = "admin")]
    [Route("v1/menu-items")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MenuItemModel> _repository;

        public MenuItemsController(IRepository<MenuItemModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MenuItemRequest>>> Get()
        {
            return Ok(Map((await _repository.GetAsync()).ToList()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MenuItemRequest>> Get([FromRoute] string id)
        {
            var entity = Map(await _repository.GetAsync(id));

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MenuItemRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MenuItemRequest>> Post(MenuItemRequest entity)
        {
            entity = Map(await _repository.AddAsync(Map(entity)));

            return CreatedAtAction(nameof(Post), new {Map(entity).Id}, entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MenuItemRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MenuItemRequest>> Delete([FromRoute] string id)
        {
            var entity = Map(await _repository.DeleteAsync(id));

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MenuItemRequest>> Update(MenuItemRequest entity)
        {
            entity = await _repository.UpdateAsync(entity);
            return Ok(entity);
        }

        private MenuItemModel Map(MenuItemRequest entity)
        {
            return _mapper.Map<MenuItemModel>(entity);
        }

        private MenuItemRequest Map(MenuItemModel entity)
        {
            return _mapper.Map<MenuItemRequest>(entity);
        }

        private IEnumerable<MenuItemRequest> Map(IEnumerable<MenuItemModel> entity)
        {
            return _mapper.Map<IEnumerable<MenuItemRequest>>(entity);
        }

        private IEnumerable<MenuItemModel> Map(IEnumerable<MenuItemRequest> entity)
        {
            return _mapper.Map<IEnumerable<MenuItemModel>>(entity);
        }
    }
}