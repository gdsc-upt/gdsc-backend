using System.Collections.Generic;
using System.Linq;
using FactoryBot;
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
    public class IdeasControllerTests : TestingBase
    {
        private readonly IEnumerable<IdeaModel> _testData = _getTestData();

        public IdeasControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            var repository = new Repository<IdeaModel>(new TestDbContext<IdeaModel>().Object);
            var controller = new IdeasController(repository);
            var example1 = new IdeaModel
            {
                Name = Lorem.Words(3).ToString(),
                Email = Lorem.Words(1).ToString(),
                Branch = Lorem.Words(2).ToString(),
                Year = RandomNumber.Next(),
                Description = Lorem.Sentence(7),
                
                
            };
            var example2 = new IdeaModel
            {
                Name = Lorem.Words(3).ToString(),
                Email = Lorem.Words(1).ToString(),
                Branch = Lorem.Words(2).ToString(),
                Year = RandomNumber.Next(),
                Description = Lorem.Sentence(7),
            };

            var added1 = await controller.Post(example1);
            var added2 = await controller.Post(example2);

            var result1 = added1.Result as CreatedAtActionResult;
            var result2 = added2.Result as CreatedAtActionResult;

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as IdeaModel;
            var entity2 = result2.Value as IdeaModel;

            Assert.NotNull(entity1);
            Assert.NotNull(entity1.Id);
            Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
            Assert.Equal(example1, entity1);

            Assert.NotNull(entity2);
            Assert.NotNull(entity2.Id);
            Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
            Assert.Equal(example2, entity2);
        }

        [Fact]
        public async void Get_ReturnsAllIdeas()
        {
            var repository = new Repository<IdeaModel>(new TestDbContext<IdeaModel>(_testData).Object);
            var controller = new IdeasController(repository);

            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<IdeaModel>>(result.Value);
            WriteLine(items);
            Assert.Equal(_testData, items);
        }
        
        [Fact]
        public async void Get_ReturnsIdeaByID()
        {
            var repository = new Repository<IdeaModel>(new TestDbContext<IdeaModel>(_testData).Object);
            var controller = new IdeasController(repository);
            
            var anElementById = _testData.First();
            var actionResult = await controller.Get(anElementById.Id);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            var entity = result.Value as IdeaModel;
            WriteLine(entity);
            Assert.Equal(anElementById, entity);
        }
        
        private static IEnumerable<IdeaModel> _getTestData()
        {
            Bot.Define(x => new IdeaModel
            {
                Id = x.Strings.Guid(),
                Name = x.Names.FullName(),
                Email = x.Strings.Any(),
                Branch = x.Strings.Any(),
                Year = x.Integer.Any(),
                Description = x.Strings.Any(),
                Created = x.Dates.Any(),
                Updated = x.Dates.Any()
            });

            return Bot.BuildSequence<IdeaModel>().Take(10).ToList();
        }
    }
}