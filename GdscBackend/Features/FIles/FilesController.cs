using GdscBackend.Database;
using GdscBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Features.FIles;

[ApiController]
[ApiVersion("1")]
[Authorize(AuthorizeConstants.CoreTeam)]
[Route("v1/files")]
public class FilesController : ControllerBase
{
    private const string MediaDirectory = "media";
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IRepository<FileModel> _repository;

    public FilesController(IHostEnvironment webHostEnvironment, IRepository<FileModel> repository)
    {
        _hostEnvironment = webHostEnvironment;
        _repository = repository;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<IEnumerable<FileModel>>> Upload([FromForm] List<IFormFile> files)
    {
        var fileModels = new List<FileModel>();

        foreach (var formFile in files)
        {
            if (formFile.Length <= 0)
            {
                continue;
            }

            var fileName = formFile.FileName.Split(".")[0];
            var fileExtension = formFile.FileName.Split(".")[1];

            var tag = Guid.NewGuid().ToString().Split("-")[0];
            var relativePath = Path.Combine(MediaDirectory, $"{fileName}_{tag}.{fileExtension}");

            Directory.CreateDirectory(Path.Combine(_hostEnvironment.ContentRootPath, "..", MediaDirectory));
            var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "..", relativePath);

            await using (var stream = System.IO.File.Create(filePath))
            {
                await formFile.CopyToAsync(stream);
            }

            var fileModel = new FileModel
            {
                Name = fileName,
                Extension = fileExtension,
                Path = relativePath,
                Size = formFile.Length
            };

            fileModels.Add(await _repository.AddAsync(fileModel));
        }

        return Created("v1/files", fileModels);
    }
}