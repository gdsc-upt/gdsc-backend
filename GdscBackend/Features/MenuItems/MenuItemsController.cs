using System.Net.Mime;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Features.MenuItems;

[ApiController]
[ApiVersion("1")]
[Authorize(AuthorizeConstants.CoreTeam)]
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
    public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get()
    {
        return Ok((await _repository.GetAsync()).ToList());
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
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
    public async Task<ActionResult<MenuItemModel>> Post(MenuItemRequest entity)
    {
        var newEntity = await _repository.AddAsync(Map(entity));

        return Created("v1/menuitem", newEntity);
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

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MenuItemModel>> Update([FromRoute]string id ,MenuItemRequest entity)
    {
        var newEntity = await _repository.UpdateAsync(id,Map(entity));

        return Ok(newEntity);
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