using System.Net.Mime;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Features.Faqs;

[ApiController]
[ApiVersion("1")]
[Authorize(AuthorizeConstants.CoreTeam)]
[Route("v1/faqs")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class FaqsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<FaqModel> _repository;

    public FaqsController(IRepository<FaqModel> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FaqModel>>> Get()
    {
        return Ok((await _repository.GetAsync()).ToList());
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FaqModel>> Get([FromRoute] string id)
    {
        var entity = await _repository.GetAsync(id);

        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<FaqModel>> Post(FaqRequest entity)
    {
        var newEntity = await _repository.AddAsync(Map(entity));

        return Created("v1/faq", newEntity);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<FaqModel>> Delete([FromRoute] string id)
    {
        var entity = await _repository.DeleteAsync(id);

        return entity is null ? NotFound() : Ok(entity);
    }

    private FaqModel Map(FaqRequest entity)
    {
        return _mapper.Map<FaqModel>(entity);
    }

    private FaqRequest Map(FaqModel entity)
    {
        return _mapper.Map<FaqRequest>(entity);
    }

    private IEnumerable<FaqRequest> Map(IEnumerable<FaqModel> entity)
    {
        return _mapper.Map<IEnumerable<FaqRequest>>(entity);
    }

    private IEnumerable<FaqModel> Map(IEnumerable<FaqRequest> entity)
    {
        return _mapper.Map<IEnumerable<FaqModel>>(entity);
    }
}