using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FactoryBot;
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

public class MembersControllerTests : TestingBase
{
    private static readonly IEnumerable<MemberModel> TestData = _getTestData();
    private readonly IMapper _mapper;

    public MembersControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        _mapper = mapconfig.CreateMapper();
    }

    [Fact]
    public async void Get_Should_Return_All_Members()
    {
        var repository = new Repository<MemberModel>(new TestDbContext<MemberModel>(TestData).Object);
        var controller = new MembersController(repository, _mapper);

        // Act
        var actionResult = await controller.Get();
        var result = actionResult.Result as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var items = Assert.IsAssignableFrom<IEnumerable<MemberModel>>(result.Value);
        WriteLine(items); // This will print items to console as a json object
    }

    [Fact]
    public async void Post_ReturnsCreatedObject()
    {
        // Arrange
        var repository = new Repository<MemberModel>(new TestDbContext<MemberModel>(TestData).Object);
        var controller = new MembersController(repository, _mapper);

        var member1 = new MemberRequest
        {
            Name = Name.FullName(),
            Email = Lorem.Words(3).ToString(),
            TeamsIds = new[] { Lorem.Words(1).ToString() }
        };
        var member2 = new MemberRequest
        {
            Name = Name.FullName(),
            Email = Lorem.Words(3).ToString(),
            TeamsIds = new[] { Lorem.Words(1).ToString() }
        };

        // Act
        var added1 = await controller.Post(member1);
        var added2 = await controller.Post(member2);

        var result1 = added1.Result as CreatedResult;
        var result2 = added2.Result as CreatedResult;

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        var entity1 = result1.Value as MemberModel;
        var entity2 = result2.Value as MemberModel;

        Assert.NotNull(entity1);
        Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
        Assert.Equal(member1.Email, entity1.Email);
        Assert.Equal(member1.Name, entity1.Name);
        // Assert.Equal(member1.TeamId, entity1.TeamId);

        Assert.NotNull(entity2);
        Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
        Assert.Equal(member2.Email, entity2.Email);
        Assert.Equal(member2.Name, entity2.Name);
        // Assert.Equal(member2.TeamId, entity2.TeamId);
    }

    [Fact]
    public async void Get_Returns_Member_by_ID()
    {
        // Arrange
        var repository = new Repository<MemberModel>(new TestDbContext<MemberModel>(TestData).Object);
        var controller = new MembersController(repository, _mapper);

        //Act

        var anElementById = TestData.First();
        var actionResult = await controller.Get(anElementById.Id);
        var result = actionResult.Result as OkObjectResult;

        //Assert

        Assert.NotNull(result);
        var entity = result.Value as MemberModel;
        Assert.NotNull(entity);
        Assert.Equal(anElementById.Email, entity.Email);
        Assert.Equal(anElementById.Name, entity.Name);
        // Assert.Equal(anElementById.TeamId, entity.TeamId);
    }

    [Fact]
    public async void Delete_Should_Delete_Member_By_ID()
    {
        //
        var repository = new Repository<MemberModel>(new TestDbContext<MemberModel>(TestData).Object);
        var controller = new MembersController(repository, _mapper);

        // Act
        var deleted = await controller.Delete(TestData.First().Id);
        var result = deleted.Result as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var entity = result.Value as MemberModel;
        Assert.NotNull(entity);
        Assert.Equal(TestData.First().Email, entity.Email);
        Assert.Equal(TestData.First().Name, entity.Name);
        // Assert.Equal(TestData.First().TeamId, entity.TeamId);
    }

    private static IEnumerable<MemberModel> _getTestData()
    {
        Bot.Define(x => new MemberModel
        {
            Id = x.Strings.Guid(),
            Name = x.Names.FullName(),
            Email = x.Strings.Any(),
            // TeamId = x.Strings.Any(),
            Created = x.Dates.Any(),
            Updated = x.Dates.Any()
        });

        return Bot.BuildSequence<MemberModel>().Take(10).ToList();
    }
}