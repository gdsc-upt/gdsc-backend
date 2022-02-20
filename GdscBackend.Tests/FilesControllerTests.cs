using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests;

public class FilesControllerTests : TestingBase
{
    private const string MediaDirectory = "media";
    private static readonly List<IFormFile> TestData = _getTestData();
    private readonly FilesController _controller;

    public FilesControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        var repository = new Repository<FileModel>(new TestDbContext<FileModel>().Object);
        var rootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "../../..");
        _controller = new FilesController(new HostingEnvironment { ContentRootPath = rootDirectory }, repository);
    }

    [Fact]
    public async void Upload_ShouldReturnFilePaths()
    {
        var upload = await _controller.Upload(TestData);

        var result = upload.Result as CreatedResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);

        var files = Assert.IsAssignableFrom<IEnumerable<FileModel>>(result.Value).ToList();
        WriteLine(files);
        Assert.Equal(3, files.Count);

        Assert.Contains(MediaDirectory, files.ElementAt(0).Path);
        Assert.Contains(MediaDirectory, files.ElementAt(1).Path);
        Assert.Contains(MediaDirectory, files.ElementAt(2).Path);

        Assert.Equal(TestData.ElementAt(0).Length, files.ElementAt(0).Size);
        Assert.Equal(TestData.ElementAt(1).Length, files.ElementAt(1).Size);
        Assert.Equal(TestData.ElementAt(2).Length, files.ElementAt(2).Size);
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

        return new List<IFormFile> { file1, file2, file3 };
    }
}