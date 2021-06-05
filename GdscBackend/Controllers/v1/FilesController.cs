using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GdscBackend.Database;
using GdscBackend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/files")]
    public class FilesController : ControllerBase
    {
        private readonly IRepository<FileModel> _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilesController(IWebHostEnvironment webHostEnvironment, IRepository<FileModel> repository)
        {
            _webHostEnvironment = webHostEnvironment;
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Upload([FromForm] List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            List<FileModel> fileModels = new List<FileModel>();
            
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    String fileName, fileExtension;
                    
                    fileName = formFile.FileName.Split(".")[0];
                    fileExtension = formFile.FileName.Split(".")[1];

                    var relativePath = Path.Combine("media",
                        fileName + System.Guid.NewGuid().ToString().Split("-")[0] + "." + fileExtension);
                    
                    var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "..", relativePath);
                    
                    using (var stream = System.IO.File.Create(filePath))
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

                    fileModel = await _repository.AddAsync(fileModel);
                    
                    fileModels.Add(fileModel);
                }
            }

            return Created("api/v1/files", fileModels);
        }
    }
}