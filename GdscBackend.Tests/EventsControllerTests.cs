using System;
using System.Collections.Generic;
using AutoMapper;
using Faker;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using GdscBackend.Utils.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class EventsControllerTests : TestingBase
    {
        private static readonly IEnumerable<EventModel> TestData = _getTestData();
        private readonly IMapper _mapper;

        public EventsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            _mapper = mapconfig.CreateMapper();
        }

        [Fact]
        public async void Post_ReturnsCreatedEventObject()
        {
            // Arrange
            var repository = new Repository<EventModel>(new TestDbContext<EventModel>().Object);
            var controller = new EventsController(repository, _mapper);

            var example1 = new EventRequest
            {
                Title = Lorem.Words(1).ToString(),
                Description = Lorem.Words(5).ToString(),
                Image = Lorem.Words(1).ToString()
            };
            var example2 = new EventRequest
            {
                Title = Lorem.Words(1).ToString(),
                Description = Lorem.Words(5).ToString(),
                Image = Lorem.Words(1).ToString()
            };

            // Act
            var added1 = await controller.Post(example1);
            var added2 = await controller.Post(example2);

            var result1 = added1.Result as CreatedResult;
            var result2 = added2.Result as CreatedResult;

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);

            var entity1 = result1.Value as EventModel;
            var entity2 = result2.Value as EventModel;

            Assert.NotNull(entity1);
            Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
            Assert.Equal(example1.Description, entity1.Description);
            Assert.Equal(example1.Image, entity1.Image);
            Assert.Equal(example1.Title, entity1.Title);

            Assert.NotNull(entity2);
            Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
            Assert.Equal(example2.Description, entity2.Description);
            Assert.Equal(example2.Image, entity2.Image);
            Assert.Equal(example2.Title, entity2.Title);
        }

        [Fact]
        public async void Get_ReturnsAllExamples()
        {
            // Arrange
            var repository = new Repository<EventModel>(new TestDbContext<EventModel>(TestData).Object);
            var controller = new EventsController(repository, _mapper);

            // Act
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<EventModel>>(result.Value);
            Assert.NotNull(items);
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
