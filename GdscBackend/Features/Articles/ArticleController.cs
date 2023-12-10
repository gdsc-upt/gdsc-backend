using System.Security.Claims;
using System.Xml.Linq;
using GdscBackend.Common.Extensions;
using GdscBackend.Database;
using GdscBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscBackend.Features.Articles;

[ApiController]
[ApiVersion("1")]
[Authorize(AuthorizeConstants.CoreTeam)]
[Route("v1/Articles")]
public class ArticleController : ControllerBase
{
    private readonly IRepository<ArticleModel> _repo;

    public ArticleController( IRepository<ArticleModel> repo )
    {
        _repo = repo;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleModel>> Post(ArticleRequest request)
    {

        var authorid = User.GetUserId();
        if (authorid is null)
        { 
            return NotFound("User not found!");
        }
        
        var article = new ArticleModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Title = request.Title,
            Content = request.Content,
            AuthorId = authorid
        };
        
        var result = await _repo.AddAsync(article);

        return Created("v1/Articles", result);
    }
    

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ArticleResponse>>> Get()
    {
        var result = await _repo.DbSet.Select(article => new ArticleResponse
        {
            Id = article.Id,
            Created = article.Created,
            Title = article.Title,
            Content = article.Content,
            AuthorId = article.AuthorId
        }).ToListAsync();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleModel>> Delete([FromRoute] string id)
    {
      
        var articol = await _repo.GetAsync(id);
        
        if (articol is null) return NotFound("Article not found!");
        var result = await _repo.DeleteAsync(articol.Id);

        return Ok(result);
    }


    [HttpPatch("Change author/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeAuthor([FromRoute] string id)
    {
        var article = await _repo.GetAsync(id);
        if (article is null) return NotFound("Article not found!");

        var authorid = User.GetUserId();
        if (authorid is null) return NotFound("Author not found!");

        article.AuthorId = authorid;
        article.Updated = DateTime.UtcNow;

        await _repo.UpdateAsync(article.Id,article);
        
        return Ok(new ArticleResponse
        {
            Id = article.Id,
            Created = article.Created,
            Title = article.Title,
            Content = article.Content,
            AuthorId = article.AuthorId // from keycloak
        });
    }

    [HttpPatch("Change content/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeContent([FromRoute] string id, [FromBody] string content)
    {
        var article = await _repo.GetAsync(id);
        if (article is null) return NotFound("Article not found!");

        article.Content = content;
        article.Updated = DateTime.UtcNow;
        
        await _repo.UpdateAsync(article.Id, article);
       
        return Ok(new ArticleResponse
        {
            Id = article.Id,
            Created = article.Created,
            Title = article.Title,
            Content = article.Content,
            AuthorId = article.AuthorId
        });
    }

    [HttpPatch("Change title/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeTitle([FromRoute] string id, [FromBody] string title)
    {
        var article = await _repo.GetAsync(id);
        if (article is null) return NotFound("Article not found!");

        article.Title = title;
        article.Updated = DateTime.UtcNow;
        
        await _repo.UpdateAsync(article.Id, article);
        
       
        return Ok(new ArticleResponse
        {
            Id = article.Id,
            Created = article.Created,
            Title = article.Title,
            Content = article.Content,
            AuthorId = article.AuthorId
        });
    }
}