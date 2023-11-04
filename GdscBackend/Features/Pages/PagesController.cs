using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Utils;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Features.Pages;

[ApiController]
[ApiVersion("1")]
[Authorize(AuthorizeConstants.CoreTeam)]
[Route("v1/pages")]
public class PagesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<PageModel> _repository;

    public PagesController(IRepository<PageModel> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
    public async Task<ActionResult<PageModel>> Post(PageRequest entity)
    {
        var newEntity = await _repository.AddAsync(Map(entity));

        return Created("v1/page", newEntity);
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

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageModel>> Update([FromRoute]string id ,PageRequest entity)
    {
        var newEntity = await _repository.UpdateAsync(id ,Map(entity));

        return Created("v1/page", newEntity);
    }

    private PageModel Map(PageRequest entity)
    {
        return _mapper.Map<PageModel>(entity);
    }

    private PageRequest Map(PageModel entity)
    {
        return _mapper.Map<PageRequest>(entity);
    }

    private IEnumerable<PageRequest> Map(IEnumerable<PageModel> entity)
    {
        return _mapper.Map<IEnumerable<PageRequest>>(entity);
    }

    private IEnumerable<PageModel> Map(IEnumerable<PageRequest> entity)
    {
        return _mapper.Map<IEnumerable<PageModel>>(entity);
    }
}