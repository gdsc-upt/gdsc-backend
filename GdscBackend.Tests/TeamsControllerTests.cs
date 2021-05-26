using System.Collections.Generic;
using System.Linq;
using FactoryBot;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class TeamsControllerTests : TestingBase
    {
        private readonly IEnumerable<TeamModel> _testData = _getTestData();
        
        public TeamsControllerTests(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public async void Delete()
        {
            var repos = new Repository<TeamModel>(new TestDbContext<TeamModel>(_testData).Object);
            var controller = new TeamsController(repos);
            
            var actionResult = await controller.Delete(_testData.First().Id);

            Assert.Equal(9,repos.DbSet.Count());

        }
        
        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            var repos = new Repository<TeamModel>(new TestDbContext<TeamModel>(_testData).Object);
            var controller = new TeamsController(repos);
            var team1 = new TeamModel
            {
                Name = Faker.Lorem.Words(3).ToString()
            };
            var team2 = new TeamModel
            {
                Name = Faker.Lorem.Words(3).ToString()
            };
            
            var added1 = await controller.Post(team1);
            var added2 = await controller.Post(team2);
            
            var result1=added1.Result as CreatedAtActionResult;
            var result2=added2.Result as CreatedAtActionResult;
            
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as TeamModel;
            var entity2 = result2.Value as TeamModel;
            
            Assert.NotNull(entity1);
            Assert.NotNull(entity1.Id);
            Assert.NotNull(entity2);
            Assert.NotNull(entity2.Id);
            
            Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
            Assert.Equal(team1, entity1);
            Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
            Assert.Equal(team2, entity2);
        }

        [Fact]
        public async void Get_ReturnsAllExamples()
        {
            var repos = new Repository<TeamModel>(new TestDbContext<TeamModel>(_testData).Object);
            var controller = new TeamsController(repos);
            
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<TeamModel>>(result.Value);
            WriteLine(items);
            Assert.Equal(_testData, items);
        }
        
        [Fact]
        public async void Get_ReturnsAllExamplesById()
        {
            var repos = new Repository<TeamModel>(new TestDbContext<TeamModel>(_testData).Object);
            var controller = new TeamsController(repos);
            
            var actionResult = await controller.Get(_testData.First().Id);
            var result = actionResult.Result as OkObjectResult;
            Assert.Equal(result.Value,_testData.First());
            WriteLine(result.Value);
            WriteLine(_testData.First());
        }
        
        private static IEnumerable<TeamModel> _getTestData()
        {
            Bot.Define(x => new TeamModel
            {
                Id = x.Strings.Guid(),
                Name = x.Names.LastName(),
                Created =x.Dates.AfterNow(),
                Updated = x.Dates.AfterNow()
            });
            return Bot.BuildSequence<TeamModel>().Take(10).ToList();
        }
    }
}