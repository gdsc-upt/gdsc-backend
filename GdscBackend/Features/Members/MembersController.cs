using System.Net.Mime;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Utils;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Features.Members;

[ApiController]
[ApiVersion("1")]
[Authorize(AuthorizeConstants.CoreTeam)]
[Route("v1/members")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class MembersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<MemberModel> _repository;

    public MembersController(IRepository<MemberModel> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MemberModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MemberModel>>> Get()
    {
        return Ok((await _repository.GetAsync()).ToList());
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberModel>> Get([FromRoute] string id)
    {
        var entity = await _repository.GetAsync(id);

        return entity is null ? NotFound() : Ok(entity);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MemberModel), StatusCodes.Status201Created)]
    public async Task<ActionResult<MemberModel>> Post(MemberRequest entity)
    {
        var newEntity = await _repository.AddAsync(Map(entity));

        return Created("v1/member", newEntity);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(MemberRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberModel>> Delete([FromBody] string id)
    {
        var entity = await _repository.DeleteAsync(id);

        return entity is null ? NotFound() : Ok(entity);
    }

    private MemberModel Map(MemberRequest entity)
    {
        return _mapper.Map<MemberModel>(entity);
    }

    private MemberRequest Map(MemberModel entity)
    {
        return _mapper.Map<MemberRequest>(entity);
    }

    private IEnumerable<MemberRequest> Map(IEnumerable<MemberModel> entity)
    {
        return _mapper.Map<IEnumerable<MemberRequest>>(entity);
    }

    private IEnumerable<MemberModel> Map(IEnumerable<MemberRequest> entity)
    {
        return _mapper.Map<IEnumerable<MemberModel>>(entity);
    }
}