using System;
using System.Collections.Generic;
using Faker;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class EventsControllerTests : TestingBase
    {
        private static readonly IEnumerable<EventModel> TestData = _getTestData();

        public EventsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async void Post_ReturnsCreatedEventObject()
        {
            // Arrange
            var repository = new Repository<EventModel>(new TestDbContext<EventModel>().Object);
            var controller = new EventsController(repository);
            var example1 = new EventModel
            {
                Title = Lorem.Words(1).ToString(),
                Description = Lorem.Words(5).ToString(),
                Image = Lorem.Words(1).ToString()
            };
            var example2 = new EventModel
            {
                Title = Lorem.Words(1).ToString(),
                Description = Lorem.Words(5).ToString(),
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

            var entity1 = result1.Value as EventModel;
            var entity2 = result2.Value as EventModel;

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
            var repository = new Repository<EventModel>(new TestDbContext<EventModel>(TestData).Object);
            var controller = new EventsController(repository);

            // Act
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<EventModel>>(result.Value);
            Assert.Equal(TestData, items);
        }

        private static IEnumerable<EventModel> _getTestData()
        {
            var models = new List<EventModel>();
            for (var _ = 0; _ < 10; _++)
                models.Add(new EventModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = Lorem.Words(1).ToString(),
                    Description = Lorem.Words(5).ToString(),
                    Image = Lorem.Words(1).ToString()
                });

            return models;
        }
    }
}