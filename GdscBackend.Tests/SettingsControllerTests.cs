using System.Collections.Generic;
using System.Linq;
using FactoryBot;
using Faker;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class SettingsControllerTests : TestingBase
    {
        private static readonly IEnumerable<SettingModel> TestData = _getTestData();

        public SettingsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            // Arrange
            var repository = new Repository<SettingModel>(new TestDbContext<SettingModel>().Object);
            var controller = new SettingsController(repository);
            var example1 = new SettingModel
            {
                Name = Lorem.Words(1).ToString(),
                Slug = Lorem.Words(1).ToString(),
                Type = Enum.Random<SettingTypeEnum>(),
                Value = Boolean.Random(),
                Image = Lorem.Words(1).ToString()
            };
            var example2 = new SettingModel
            {
                Name = Lorem.Words(1).ToString(),
                Slug = Lorem.Words(1).ToString(),
                Type = Enum.Random<SettingTypeEnum>(),
                Value = Boolean.Random(),
                Image = Lorem.Words(1).ToString()
            };

            // Act
            var added1 = await controller.Post(example1);
            var added2 = await controller.Post(example2);

            var result1 = added1.Result as CreatedAtActionResult;
            var result2 = added2.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as SettingModel;
            var entity2 = result2.Value as SettingModel;

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
            var repostitory = new Repository<SettingModel>(new TestDbContext<SettingModel>(TestData).Object);
            var controller = new SettingsController(repostitory);

            // Act
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<SettingModel>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
            Assert.Equal(TestData, items);
        }

        private static IEnumerable<SettingModel> _getTestData()
        {
            Bot.Define(x => new SettingModel
            {
                Id = x.Strings.Guid(),
                Name = x.Strings.Any(),
                Slug = x.Strings.Any(),
                Type = x.Enums.Any<SettingTypeEnum>(),
                Value = x.Boolean.Any(),
                Image = x.Strings.Filename()
            });

            return Bot.BuildSequence<SettingModel>().Take(10).ToList();
        }
    }
}