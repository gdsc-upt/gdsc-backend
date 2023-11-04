using System.Collections.Generic;
using System.Linq;
using FactoryBot;
using Faker;
using GdscBackend.Database;
using GdscBackend.Features.Examples;
using GdscBackend.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests;

public class ExampleTests : TestingBase
{
    private static readonly IEnumerable<ExampleModel> TestData = _getTestData();

    public ExampleTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public async void Post_ReturnsCreatedObject()
    {
        // Arrange
        var repository = new Repository<ExampleModel>(new TestDbContext<ExampleModel>().Object);
        var controller = new ExamplesController(repository);
        var example1 = new ExampleModel
        {
            Number = RandomNumber.Next(),
            Title = Lorem.Words(3).ToString(),
            Type = ExampleTypeEnum.EasyExample
        };
        var example2 = new ExampleModel
        {
            Number = RandomNumber.Next(),
            Title = Lorem.Words(3).ToString(),
            Type = ExampleTypeEnum.WtfExample
        };

        // Act
        var added1 = await controller.Post(example1);
        var added2 = await controller.Post(example2);

        var result1 = added1.Result as CreatedAtActionResult;
        var result2 = added2.Result as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        var entity1 = result1.Value as ExampleModel;
        var entity2 = result2.Value as ExampleModel;

        Assert.NotNull(entity1);
        Assert.NotNull(entity1.Id);
        Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
        Assert.Equal(example1, entity1);

        Assert.NotNull(entity1);
        Assert.NotNull(entity1.Id);
        Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
        Assert.Equal(example2, entity2);
    }

    [Fact]
    public async void Get_ReturnsAllExamples()
    {
        // Arrange
        var repostitory = new Repository<ExampleModel>(new TestDbContext<ExampleModel>(TestData).Object);
        var controller = new ExamplesController(repostitory);

        // Act
        var actionResult = await controller.Get();
        var result = actionResult.Result as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var items = Assert.IsAssignableFrom<IEnumerable<ExampleModel>>(result.Value);
        WriteLine(items); // This will print items to console as a json object
        Assert.Equal(TestData, items);
    }

    private static IEnumerable<ExampleModel> _getTestData()
    {
        // Bot.Define<ExampleModel>(x => new ExampleModel
        // {
        //     Id = x.Strings.Guid(),
        //     Title = x.Names.FullName(),
        //     Number = x.Integer.Any(),
        //     Type = x.Enums.Any<ExampleTypeEnum>(),
        //     Created = x.Dates.Any(),
        //     Updated = x.Dates.Any()
        // });

        Bot.DefineAuto<ExampleModel>();

        return Bot.BuildSequence<ExampleModel>().Take(10).ToList();
    }
}
