using System.Collections.Generic;
using gdsc_web_backend.Controllers;
using gdsc_web_backend.Models;
using gdsc_web_backend.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace gdsc_web_backend.tests
{
    public class SettingsControllerTests : TestingBase
    {
        public SettingsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public void Post_ReturnsError_WhenIdNotUnique()
        {
            // Arrange
            var controller = new SettingsController();
            var example1 = new ExampleModel
            {
                Id = "1",
                Name = "Just a setting",
                Slug = "just-a-setting",
                Type = SettingTypeEnum.Image,
                Value = false,
                Image = "o descriere random :)"
            };
            var example2 = new ExampleModel
            {
                Id = "1",
                Name = "Some second setting here",
                Slug = "some-second-setting",
                Type = SettingTypeEnum.Image,
                Value = true,
                Image = "alta descriere random, I guess"
            };

            // Act
            var added1 = controller.Post(example1).Result as CreatedResult;
            var added2 = controller.Post(example2).Result as BadRequestObjectResult;
            Assert.NotNull(added2);
            var error = added2.Value as ErrorViewModel;

            // Assert
            Assert.NotNull(added1);
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
            Assert.Equal(example1, added1.Value as SettingModel);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status400BadRequest, added2.StatusCode);
            Assert.Equal("An object with the same ID already exists", error.Message);
        }

        [Fact]
        public void Post_ReturnsNewElement()
        {
            // Arrange
            var controller = new SettingsController();
            var example = new ExampleModel
            {
                Id = "1",
                Name = "Just a setting",
                Slug = "just-a-setting",
                Type = SettingTypeEnum.Image,
                Value = false,
                Image = "o descriere random :)"
            };

            // Act
            var added = controller.Post(example1).Result as CreatedResult;

            // Assert
            Assert.NotNull(added);
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
            Assert.Equal(example, added.Value as SettingModel);
        }

        [Fact]
        public void Get_ReturnsAllExamples()
        {
            // Arrange
            var controller = new SettingsController();
            var examples = new List<SettingModel>
            {
                new()
                {
                    Id = "1",
                    Name = "Just a setting",
                    Slug = "just-a-setting",
                    Type = SettingTypeEnum.Image,
                    Value = false,
                    Image = "o descriere random :)"
                },
                new()
                {
                    Id = "2",
                    Name = "Some second setting here",
                    Slug = "some-second-setting",
                    Type = SettingTypeEnum.Image,
                    Value = true,
                    Image = "alta descriere random, I guess"
                },
            };
            controller.Post(examples[0]);
            controller.Post(examples[1]);

            // Act
            var result = controller.Get().Result as OkObjectResult; //BadRequestObjectResult

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<SettingModel>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
            Assert.Equal(examples, items);
        }

        [Fact]
        public void Get_ReturnsError_IdDoesntExist()
        {
            // Arrange
            var controller = new SettingsController();
            var examples = new List<SettingModel>
            {
                new()
                {
                    Id = "1",
                    Name = "Just a setting",
                    Slug = "just-a-setting",
                    Type = SettingTypeEnum.Image,
                    Value = false,
                    Image = "o descriere random :)"
                },
                new()
                {
                    Id = "2",
                    Name = "Some second setting here",
                    Slug = "some-second-setting",
                    Type = SettingTypeEnum.Image,
                    Value = true,
                    Image = "alta descriere random, I guess"
                },
            };
            controller.Post(examples[0]);
            controller.Post(examples[1]);

            // Act
            var result = controller.Get("3").Result as BadRequestObjectResult; //BadRequestObjectResult

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<SettingModel>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
            Assert.Equal(examples, items);
        }
}