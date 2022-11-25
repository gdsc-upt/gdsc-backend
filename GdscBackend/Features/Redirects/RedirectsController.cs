using System.Net.Mime;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Features.Redirects;

[ApiController]
[ApiVersion("1")]
[Authorize(Roles = "admin")]
[Route("v1/redirects")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class RedirectsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<RedirectModel> _repository;

    public RedirectsController(IRepository<RedirectModel> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
        var dict = new Dictionary<string, string>();
        var redirects = (await _repository.GetAsync()).ToList();
        foreach (var redirectModel in redirects)
        {
            dict.Add(redirectModel.Path, redirectModel.RedirectTo);
        }

        return Ok(dict);
    }


    [HttpGet("admin")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetForAdmin()
    {
        var redirects = (await _repository.GetAsync()).ToList();
        return Ok(redirects);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RedirectModel>> Post(RedirectRequest entity)
    {
        var newEntity = await _repository.AddAsync(Map(entity));

        return Created("v1/redirects", newEntity);
    }

    [HttpDelete("{path}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RedirectResponse>> Delete([FromRoute] string path)
    {
        var all = await _repository.GetAsync();
        var newEntity = all.FirstOrDefault(entity => entity.Path == path);
        if (newEntity is null)
        {
            return NotFound();
        }

        var result = await _repository.DeleteAsync(newEntity.Id);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("{path}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RedirectResponse>> Update([FromRoute] string path,
        [FromBody] RedirectRequest request)
    {
        var all = await _repository.GetAsync();
        var newEntity = all.FirstOrDefault(entity => entity.Path == path);
        if (newEntity is null)
        {
            return NotFound();
        }

        newEntity.Path = request.Path;
        newEntity.RedirectTo = request.RedirectTo;
        newEntity.Updated = DateTime.UtcNow;

        var result = await _repository.UpdateAsync(newEntity.Id, newEntity);
        return Ok(result);
    }


    private RedirectModel Map(RedirectRequest entity)
    {
        return _mapper.Map<RedirectModel>(entity);
    }
}