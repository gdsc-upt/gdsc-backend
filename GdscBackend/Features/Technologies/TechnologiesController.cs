using System.Net.Mime;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Features.FIles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscBackend.Features.Technologies;

[ApiController]
[ApiVersion("1")]
[Route("v1/technologies")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class TechnologiesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<TechnologyModel> _repository;
    private readonly IRepository<FileModel> _fileRepository;

    public TechnologiesController(IRepository<TechnologyModel> repository, IMapper mapper, IRepository<FileModel> fileRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _fileRepository = fileRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TechnologyModel>>> Get()
    {
        var technologiesQueryable = _repository.DbSet.AsQueryable().Include(t => t.Icon);
        return Ok(await technologiesQueryable.ToListAsync());
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TechnologyModel>> Post(TechnologyRequest entity)
    {
        var icon = await _fileRepository.GetAsync(entity.IconId);
        if (icon is null)
        {
            return BadRequest("Icon not found");
        }

        var technology = Map(entity);
        technology.Icon = icon;

        var newEntity = await _repository.AddAsync(technology);
        return Ok(newEntity);
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TechnologyModel>> Delete([FromRoute] string id)
    {
        var entity = await _repository.DeleteAsync(id);
        return entity is null ? NotFound() : Ok(Map(entity));
    }

    private TechnologyModel Map(TechnologyRequest entity)
    {
        return _mapper.Map<TechnologyModel>(entity);
    }

    private TechnologyRequest Map(TechnologyModel entity)
    {
        return _mapper.Map<TechnologyRequest>(entity);
    }

    private IEnumerable<TechnologyRequest> Map(IEnumerable<TechnologyModel> entity)
    {
        return _mapper.Map<IEnumerable<TechnologyRequest>>(entity);
    }

    private IEnumerable<TechnologyModel> Map(IEnumerable<TechnologyRequest> entity)
    {
        return _mapper.Map<IEnumerable<TechnologyModel>>(entity);
    }
}
