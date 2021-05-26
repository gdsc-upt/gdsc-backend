using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using FactoryBot;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.Models.Enums;
using GdscBackend.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;
using Xunit.Abstractions;
using PageModel = GdscBackend.Models.PageModel;

namespace gdsc_web_backend.tests
{
    public class PagesControllerTests : TestingBase
    {
        private readonly IEnumerable<PageModel> _testData = _getTestData();

        public PagesControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task Post_ReturnsCreatedObject()
        {
            var repository = new Repository<PageModel>(new TestDbContext<PageModel>().Object);
            var controller = new PagesController(repository);
            var example1 = new PageModel
            {
                Title = Faker.Lorem.Words(3).ToString(),
                Body = Faker.Lorem.Words(3).ToString(),
                isPublished = Faker.Boolean.Random(),
                Slug = Faker.Lorem.Words(3).ToString(),
                ShortDescription = Faker.Lorem.Words(10).ToString(),
                Image = Faker.Lorem.Words(3).ToString(),
            };
            var example2 = new PageModel
            {
                Title = Faker.Lorem.Words(3).ToString(),
                Body = Faker.Lorem.Words(3).ToString(),
                isPublished = Faker.Boolean.Random(),
                Slug = Faker.Lorem.Words(3).ToString(),
                ShortDescription = Faker.Lorem.Words(10).ToString(),
                Image = Faker.Lorem.Words(3).ToString(),
            };

            var added1 = await controller.Post(example1);
            var added2 = await controller.Post(example2);

            var result1 = added1.Result as CreatedAtActionResult;
            var result2 = added2.Result as CreatedAtActionResult;

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as PageModel;
            var entity2 = result2.Value as PageModel;

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
            var repostitory = new Repository<PageModel>(new TestDbContext<PageModel>(_testData).Object);
            var controller = new PagesController(repostitory);

            // Act
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<PageModel>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
            Assert.Equal(_testData, items);
        }

        private static IEnumerable<PageModel> _getTestData()
        {
            Bot.Define(x => new PageModel()
            {
                Id = x.Strings.Guid(),
                Title = x.Names.FullName(),
                Body = x.Strings.Any(),
                isPublished = x.Boolean.Any(),
                Slug = x.Strings.Any(),
                ShortDescription = x.Strings.Any(),
                Image = x.Strings.Any(),
                Created = x.Dates.Any(),
                Updated = x.Dates.Any()
            });

            return Bot.BuildSequence<PageModel>().Take(10).ToList();
        }
    }
}
