using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FactoryBot;
using Faker;
using GdscBackend.Database;
using GdscBackend.Features.Members;
using GdscBackend.Features.Teams;
using GdscBackend.Tests.Mocks;
using GdscBackend.Utils.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests;

public class TeamsControllerTests : TestingBase
{
    private readonly IMapper _mapper;
    private readonly IEnumerable<TeamModel> _testData = _getTestData();

    public TeamsControllerTests(ITestOutputHelper helper) : base(helper)
    {
        var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        _mapper = mapconfig.CreateMapper();
    }

    [Fact]
    public async void Delete_Returns_OkResult()
    {
        var repos = new Repository<TeamModel>(new TestDbContext<TeamModel>(_testData).Object);
        var controller = new TeamsController(repos, _mapper);
        var deletedVar = _testData.First();

        var actionResult = await controller.Delete(deletedVar.Id);
        var actionResultVal = actionResult.Result as OkObjectResult;

        var expectedResult = await controller.Get(deletedVar.Id);
        var badRequestVal = expectedResult.Result as NotFoundResult;

        Assert.NotNull(actionResult);
        Assert.NotNull(actionResultVal);
        Assert.Equal(deletedVar, actionResultVal.Value);
        Assert.Null(expectedResult.Value);
        Assert.Equal(StatusCodes.Status200OK, actionResultVal.StatusCode);
        Assert.NotNull(badRequestVal);
        Assert.Equal(StatusCodes.Status404NotFound, badRequestVal.StatusCode);
    }

    [Fact]
    public async void Post_ReturnsCreatedObject()
    {
        var repos = new Repository<TeamModel>(new TestDbContext<TeamModel>(_testData).Object);
        var controller = new TeamsController(repos, _mapper);
        var team1 = new TeamRequest
        {
            Name = Lorem.Words(3).ToString()
            // Members = new List<MemberModel>()
        };
        var team2 = new TeamRequest
        {
            Name = Lorem.Words(3).ToString()
            // Members = new List<MemberModel>()
        };

        var added1 = await controller.Post(team1);
        var added2 = await controller.Post(team2);

        var result1 = added1.Result as CreatedResult;
        var result2 = added2.Result as CreatedResult;

        Assert.NotNull(result1);
        Assert.NotNull(result2);
        var entity1 = result1.Value as TeamModel;
        var entity2 = result2.Value as TeamModel;

        Assert.NotNull(entity1);
        Assert.NotNull(entity2);

        Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
        Assert.Equal(team1.Name, entity1.Name);
        Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
        Assert.Equal(team2.Name, entity2.Name);
    }

    [Fact]
    public async void Get_ReturnsAllExamples()
    {
        var repos = new Repository<TeamModel>(new TestDbContext<TeamModel>(_testData).Object);
        var controller = new TeamsController(repos, _mapper);

        var actionResult = await controller.Get();
        var result = actionResult.Result as OkObjectResult;

        Assert.NotNull(result);
        var items = Assert.IsAssignableFrom<IEnumerable<TeamModel>>(result.Value);
        Assert.Equal(_testData, items);
    }

    [Fact]
    public async void Get_ReturnsAllExamplesById()
    {
        var repos = new Repository<TeamModel>(new TestDbContext<TeamModel>(_testData).Object);
        var controller = new TeamsController(repos, _mapper);

        var actionResult = await controller.Get(_testData.First().Id);
        var result = actionResult.Result as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(result.Value, _testData.First());
    }

    private static IEnumerable<TeamModel> _getTestData()
    {
        Bot.Define(x => new TeamModel
        {
            Id = x.Strings.Guid(),
            Name = Lorem.Words(1).ToString(),
            // Members = _getMemberData(id),
            Created = x.Dates.Any(),
            Updated = x.Dates.Any()
        });

        return Bot.BuildSequence<TeamModel>().Take(10).ToList();
    }

    private static IEnumerable<MemberModel> _getMemberData(string teamId)
    {
        Bot.Define(x => new MemberModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = Lorem.Words(1).ToString(),
            Email = Lorem.Words(1) + "@" + Lorem.Words(1) + ".com",
            // TeamId = teamId,
            Created = x.Dates.Any(),
            Updated = x.Dates.Any()
        });
        return Bot.BuildSequence<MemberModel>().Take(10).ToList();
    }
}
