using GdscBackend.Auth;
using GdscBackend.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscBackend.Features.Articles;

[ApiController]
[ApiVersion("1")]
[Authorize(Roles = "admin")]
[Route("v1/Articles")]

public class ArticleController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ArticleController(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleModel>> Post(ArticleRequest request)
    {
        var author = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == request.AuthorId);
        if (author is null)
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
            Author = author
        };

        var result = await _dbContext.Articles.AddAsync(article);
        await _dbContext.SaveChangesAsync();

        return Created("v1/Articles", result.Entity);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ArticleResponse>>> Get()
    {
        var result = _dbContext.Articles.Include(a => a.Author).Select(
            article => new ArticleResponse
            {
                Id = article.Id,
                Created = article.Created,
                Title = article.Title,
                Content = article.Content,
                Author = new UserViewModel
                {
                    Id = article.Author.Id,
                    UserName = article.Author.UserName,
                    Email = article.Author.Email
                }
            }).ToList();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleModel>> Delete([FromRoute] string id)
    {
        var articol = await _dbContext.Articles.Include(a => a.Author).FirstOrDefaultAsync(entity => entity.Id == id);
        if (articol is null)
        {
            return NotFound("Article not found!");
        }

        var result = _dbContext.Articles.Remove(articol);
        await _dbContext.SaveChangesAsync();
        return Ok(result.Entity);
    }

    [HttpPatch("Change author/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeAuthor([FromRoute] string id, [FromBody] string authorid)
    {
        var article = await _dbContext.Articles.Include(a => a.Author).FirstOrDefaultAsync(entity => entity.Id == id);
        if (article is null)
        {
            return NotFound("Article not found!");
        }

        var author = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == authorid);
        if (author is null)
        {
            return NotFound("Author not found!");
        }

        article.Author = author;
        article.Updated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return Ok(new ArticleResponse
        {
            Id = article.Id,
            Created = article.Created,
            Title = article.Title,
            Content = article.Content,
            Author = new UserViewModel
            {
                Id = article.Author.Id,
                UserName = article.Author.UserName,
                Email = article.Author.Email
            }
        });
    }
    
    [HttpPatch("Change content/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeContent([FromRoute] string id, [FromBody] string content)
    {
        var article = await _dbContext.Articles.Include(a => a.Author).FirstOrDefaultAsync(entity => entity.Id == id);
        if (article is null)
        {
            return NotFound("Article not found!");
        }
        
        article.Content = content;
        article.Updated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return Ok(new ArticleResponse
        {
            Id = article.Id,
            Created = article.Created,
            Title = article.Title,
            Content = article.Content,
            Author = new UserViewModel
            {
                Id = article.Author.Id,
                UserName = article.Author.UserName,
                Email = article.Author.Email
            }
        });
    }
    
    [HttpPatch("Change title/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticleResponse>> ChangeTitle([FromRoute] string id, [FromBody] string title)
    {
        var article = await _dbContext.Articles.Include(a => a.Author).FirstOrDefaultAsync(entity => entity.Id == id);
        if (article is null)
        {
            return NotFound("Article not found!");
        }
        
        article.Title = title;
        article.Updated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return Ok(new ArticleResponse
        {
            Id = article.Id,
            Created = article.Created,
            Title = article.Title,
            Content = article.Content,
            Author = new UserViewModel
            {
                Id = article.Author.Id,
                UserName = article.Author.UserName,
                Email = article.Author.Email
            }
        });
    }
}