using System.Collections.Generic;
using System.IO;
using System.Text;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class FilesControllerTests : TestingBase
    {
        private static readonly List<IFormFile> TestData = _getTestData();
        private FilesController _controller;
        private Repository<FileModel> _repository;
        
        public FilesControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _repository = new Repository<FileModel>(new TestDbContext<FileModel>().Object);
            _controller = new FilesController(new HostingEnvironment { EnvironmentName = Environments.Development }, _repository);
        }

        [Fact]
        public async void Post_ShouldUploadOneOrMoreFiles()
        {
            var upload = await _controller.Upload(TestData);

            var result = upload.Result as CreatedResult;
            
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);

            WriteLine(result.Value);

        }

        private static List<IFormFile> _getTestData()
        {
            var file1 = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("x are mere")),
                0,
                0,
                "Data",
                "test1.txt"
            );
            
            var file2 = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("y are mere")),
                0,
                0,
                "Data",
                "test1.txt"
            );
            
            var file3 = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("y are pere")),
                0,
                0,
                "Data",
                "test2.txt"
            );

            var data = new List<IFormFile> {file1, file2, file3};

            return data;
        }
    }
}