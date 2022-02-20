using System;
using System.Collections.Generic;
using AutoMapper;
using Faker;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using GdscBackend.Tests.Mocks;
using GdscBackend.Utils.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests;

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
        var imageId1 = Guid.NewGuid().ToString();
        var imageId2 = Guid.NewGuid().ToString();
        var filesRepository = new Repository<FileModel>(
            new TestDbContext<FileModel>(new[] { new FileModel { Id = imageId1 }, new FileModel { Id = imageId2 } })
               .Object);
        var controller = new EventsController(repository, _mapper, filesRepository);

        var example1 = new EventRequest
        {
            Title = Lorem.Words(1).ToString(),
            Description = Lorem.Words(5).ToString(),
            ImageId = imageId1,
            Start = Identification.DateOfBirth(),
            End = Identification.DateOfBirth()
        };
        var example2 = new EventRequest
        {
            Title = Lorem.Words(1).ToString(),
            Description = Lorem.Words(5).ToString(),
            ImageId = imageId2,
            Start = Identification.DateOfBirth(),
            End = Identification.DateOfBirth()
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
        Assert.Equal(example1.ImageId, entity1.Image.Id);
        Assert.Equal(example1.Title, entity1.Title);
        Assert.Equal(example1.Start, entity1.Start);
        Assert.Equal(example1.End, entity1.End);

        Assert.NotNull(entity2);
        Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
        Assert.Equal(example2.Description, entity2.Description);
        Assert.Equal(example2.ImageId, entity2.Image.Id);
        Assert.Equal(example2.Title, entity2.Title);
        Assert.Equal(example2.Start, entity2.Start);
        Assert.Equal(example2.End, entity2.End);
    }

    [Fact]
    public async void Get_ReturnsAllExamples()
    {
        // Arrange
        var repository = new Repository<EventModel>(new TestDbContext<EventModel>(TestData).Object);
        var filesRepository = new Repository<FileModel>(
            new TestDbContext<FileModel>(new[] { new FileModel { Id = Guid.NewGuid().ToString() } }).Object);
        var controller = new EventsController(repository, _mapper, filesRepository);

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
                Image = new FileModel
                {
                    Name = Lorem.Words(1).ToString(), Extension = ".png", Size = 30,
                    Path = Lorem.Words(1).ToString(), Id = Guid.NewGuid().ToString()
                },
                Start = Identification.DateOfBirth(),
                End = Identification.DateOfBirth()
            });
        return models;
    }
}