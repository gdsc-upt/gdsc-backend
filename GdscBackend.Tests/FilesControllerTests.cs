using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            _controller = new FilesController(new HostingEnvironment { ContentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "../../..")}, _repository);
        }

        [Fact]
        public async void Upload_ShouldReturnFilePaths()
        {
            var upload = await _controller.Upload(TestData);

            var result = upload.Result as CreatedResult;
            
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            
            var files = Assert.IsAssignableFrom<IEnumerable<FileModel>>(result.Value);
            WriteLine(files);
            Assert.Equal( 3, files.ToList().Count);
            Assert.Contains("media", files.ElementAt(0).Path);
            Assert.Contains("media", files.ElementAt(1).Path);
            Assert.Contains("media", files.ElementAt(2).Path);

        }

        private static List<IFormFile> _getTestData()
        {
            var stream1 = new MemoryStream(Encoding.UTF8.GetBytes("x are mere"));
            var stream2 = new MemoryStream(Encoding.UTF8.GetBytes("y are mere"));
            var stream3 = new MemoryStream(Encoding.UTF8.GetBytes("y are pere"));
            
            var file1 = new FormFile(
                stream1,
                0,
                stream1.Length,
                "Data",
                "test1.txt"
            );
            
            var file2 = new FormFile(
                stream2,
                0,
                stream2.Length,
                "Data",
                "test1.txt"
            );
            
            var file3 = new FormFile(
                stream3,
                0,
                stream3.Length,
                "Data",
                "test2.txt"
            );

            var data = new List<IFormFile> {file1, file2, file3};

            return data;
        }
    }
}