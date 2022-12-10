using GdscBackend.Auth;
using GdscBackend.Database;
using Microsoft.AspNetCore.Authorization;
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

        return Created("v1/Articles", result);
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
}