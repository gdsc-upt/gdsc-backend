using System.Net.Mime;
using AutoMapper;
using GdscBackend.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Features.Articles;

[ApiController]
[ApiVersion("1")]
[Authorize(Roles = "admin")]
[Route("v1/Articles")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class ArticleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<ArticleModel> _repository;

    public ArticleController(IRepository<ArticleModel> repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleModel>> Post(ArticleRequest request)
    {
        var entity = Map(request);
        return Created("v1/Articles", await _repository.AddAsync(entity));
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ArticleResponse>>> Get()
    {
        return Ok(Map(await _repository.GetAsync()).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArticleResponse>> Get([FromRoute] string id)
    {
        var entity = await _repository.GetAsync(id);
        return entity is null ? NotFound("Article not found!") : Ok(Map(entity));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> Delete([FromRoute] string id)
    {
        var entity = await _repository.DeleteAsync(id);
        return entity is null ? NotFound("Article could not be deleted!") : Ok(Map(entity));
    }

    [HttpPatch("Change author/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeAuthor([FromRoute] string id, [FromBody] string authorid)
    {
        var entity = await _repository.GetAsync(id);
        if (entity is null)
        {
            return NotFound("Article not found!");
        }

        entity.AuthorId = authorid;
        entity.Updated = DateTime.UtcNow;
        await _repository.UpdateAsync(id, entity);
        return Ok(Map(entity));
    }
    
    [HttpPatch("Change content/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeContent([FromRoute] string id, [FromBody] string content)
    {
        var entity = await _repository.GetAsync(id);
        if (entity is null)
        {
            return NotFound("Article not found!");
        }

        entity.Content = content;
        entity.Updated = DateTime.UtcNow;
        await _repository.UpdateAsync(id, entity);
        return Ok(Map(entity));
    }
    
    [HttpPatch("Change title/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeTitle([FromRoute] string id, [FromBody] string title)
    {
        var entity = await _repository.GetAsync(id);
        if (entity is null)
        {
            return NotFound("Article not found!");
        }

        entity.Title = title;
        entity.Updated = DateTime.UtcNow;
        await _repository.UpdateAsync(id, entity);
        return Ok(Map(entity));
    }
    
    private ArticleModel Map(ArticleRequest entity)
    {
        return _mapper.Map<ArticleModel>(entity);
    }
    private ArticleResponse Map(ArticleModel entity)
    {
        return _mapper.Map<ArticleResponse>(entity);
    }
    private IEnumerable<ArticleResponse> Map(IEnumerable<ArticleModel> entity)
    {
        return _mapper.Map<IEnumerable<ArticleResponse>>(entity);
    }
    private IEnumerable<ArticleModel> Map(IEnumerable<ArticleRequest> entity)
    {
        return _mapper.Map<IEnumerable<ArticleModel>>(entity);
    }
}