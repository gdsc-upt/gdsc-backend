using System.Collections.Generic;
using gdsc_web_backend.Controllers;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace gdsc_web_backend.tests
{
    public class TeamsControllerTests : TestingBase
    {
        public TeamsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper) {}

        [Fact]
        public void Post_ReturnsCreatedObject()
        {
            var controller = new TeamsController();
            var team1 = new TeamModel
            {
                Id = "1",
                Name = "FCSB"
            };
            var team2 = new TeamModel
            {
                Id = "2",
                Name = "ASU"
            };

            var added1 = controller.Post(team1).Result as CreatedResult;
            var added2 = controller.Post(team2).Result as CreatedResult;

            Assert.NotNull(added1);
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
            Assert.Equal(team1, added1.Value as TeamModel);
            
            Assert.NotNull(added2);
            Assert.Equal(StatusCodes.Status201Created, added2.StatusCode);
            Assert.Equal(team2, added2.Value as TeamModel);
        }

        [Fact]
        public void Post_ReturnsError_WhenIdNotUnique()
        {
            var controller = new TeamsController();
            var team1 = new TeamModel
            {
                Id = "1",
                Name = "FCSB"
            };
            var team2 = new TeamModel
            {
                Id = "2",
                Name = "ASU"
            };
            
            var added1 = controller.Post(team1).Result as CreatedResult;
            var added2 = controller.Post(team2).Result as BadRequestObjectResult;
            Assert.NotNull(added2);
            var error = added2.Value as ErrorViewModel;
            
            Assert.NotNull(added1);
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
            Assert.Equal(team1, added1.Value as TeamModel);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status400BadRequest, added2.StatusCode);
            Assert.Equal("An object with the same ID already exists", error.Message);
        }
        
        [Fact]
        public void Get_ReturnsAllExamples()
        {
            var controller = new TeamsController();
            var teams = new List<TeamModel>
            {
                new()
                {
                    Id = "1",
                    Name = "FCSB"
                },
                new()
                {
                    Id = "2",
                    Name = "ASU"
                }
            };

            controller.Post(teams[0]);
            controller.Post(teams[1]);
            
            var result = controller.Get().Result as OkObjectResult;
            
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<List<TeamModel>>(result.Value);
            WriteLine(items);
            Assert.Equal(teams, items);
        }
    }
}