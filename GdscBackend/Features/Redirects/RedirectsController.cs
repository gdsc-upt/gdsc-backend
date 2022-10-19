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
            dict.Add(redirectModel.path, redirectModel.redirectTo);
        }

        return Ok(dict);
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
    
    private RedirectModel Map(RedirectRequest entity)
    {
        return _mapper.Map<RedirectModel>(entity);
    }
}